using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmissionsBehaviour : MonoBehaviour
{
    public static Stack<MeshRenderer> emissionUndoStack;
    public static Stack<MeshRenderer> emissionRedoStack;

    private static Material defaultMat;
    [SerializeField] private Material emissionUndoMat;
    [SerializeField] private Material emissionRedoMat;

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
                mesh.material = emissionRedoMat;
            }
            
            foreach (MeshRenderer mesh in emissionUndoStack)
            {
                mesh.material = emissionUndoMat;
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
