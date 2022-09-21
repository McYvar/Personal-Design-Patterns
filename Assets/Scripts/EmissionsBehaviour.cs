using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmissionsBehaviour : MonoBehaviour
{
    public static Stack<MeshRenderer> emissionRedoStack;
    private static Stack<MeshRenderer> emissionUndoStack;

    private static Material defaultMat;

    private static bool canUpdate;

    private void Start()
    {
        defaultMat = GetComponent<MeshRenderer>().material;
        emissionUndoStack = new Stack<MeshRenderer>();
        emissionRedoStack = new Stack<MeshRenderer>();
    }

    private void Update()
    {
        if (canUpdate)
        {
            foreach (MeshRenderer mesh in emissionRedoStack)
            {
                mesh.material.color += new Color(0, emissionRedoStack.Count * 0.05f, 0);
            }
            
            foreach (MeshRenderer mesh in emissionUndoStack)
            {
                mesh.material.color += new Color(emissionUndoStack.Count * 0.05f, 0, 0);
            }
            canUpdate = false;
        }
    }

    public static void PushUndoStack(MeshRenderer _mesh)
    {
        emissionUndoStack.Push(_mesh);
        canUpdate = true;
    }
    
    public static void PopUndoStack()
    {
        emissionRedoStack.Push(emissionUndoStack.Pop());
        canUpdate = true;
    }

    public static void ClearRedoStack()
    {
        foreach (MeshRenderer mesh in emissionRedoStack)
        {
            mesh.material = defaultMat;
        }
        emissionRedoStack.Clear();
        canUpdate = true;
    }
}
