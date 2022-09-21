using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerBehaviour : MonoBehaviour
{
    #region variables

    private TileCasting[] tileCasting;

    [SerializeField] private KeyCode upButton;
    [SerializeField] private KeyCode downButton;
    [SerializeField] private KeyCode leftButton;
    [SerializeField] private KeyCode rightButton;

    private bool didMove = false;
    #endregion

    private void Start()
    {
        tileCasting = FindObjectsOfType<TileCasting>();
    }

    private void Update()
    {
        DoMove();
        
        if (Input.GetKeyDown(KeyCode.Z))
        {
            if      (Commandos.undoStack.Count > 0) Commandos.undoStack.Pop().Undo(gameObject, Commandos.redoStack);
            else    Debug.Log("Nothing left to Undo");
        }

        if (Input.GetKeyDown(KeyCode.Y))
        {
            if      (Commandos.redoStack.Count > 0) Commandos.redoStack.Pop().Redo(gameObject, Commandos.undoStack);
            else    Debug.Log("Nothing left to Redo");
        }
    }

    private void DoMove()
    {
        if (Input.GetKeyDown(upButton))
        {
            Commandos.moveUpCommand.Execute(gameObject, Commandos.undoStack, Commandos.redoStack);
            didMove = true;
        }

        if (Input.GetKeyDown(downButton))
        {
            Commandos.moveDownCommand.Execute(gameObject, Commandos.undoStack, Commandos.redoStack);
            didMove = true;
        }

        if (Input.GetKeyDown(leftButton))
        {
            Commandos.moveLeftCommand.Execute(gameObject, Commandos.undoStack, Commandos.redoStack);
            didMove = true;
        }

        if (Input.GetKeyDown(rightButton))
        {
            Commandos.moveRightCommand.Execute(gameObject, Commandos.undoStack, Commandos.redoStack);
            didMove = true;
        }

        if (!didMove) return;
        foreach (var tile in tileCasting)
        {
            tile.UpdateTiles();
        }

        didMove = false;
    }
}
