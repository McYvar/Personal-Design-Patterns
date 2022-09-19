using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerBehaviour : MonoBehaviour
{
    #region variables

    private TileCasting[] tileCasting;
    
    private readonly Command moveUpCommand = new MoveUpCommand();
    private readonly Command moveDownCommand = new MoveDownCommand();
    private readonly Command moveLeftCommand = new MoveLeftCommand();
    private readonly Command moveRightCommand = new MoveRightCommand();
    
    private Stack<Command> undoStack;
    private Stack<Command> redoStack;

    [SerializeField] private KeyCode upButton;
    [SerializeField] private KeyCode downButton;
    [SerializeField] private KeyCode leftButton;
    [SerializeField] private KeyCode rightButton;

    private bool didMove = false;
    #endregion

    private void Start()
    {
        undoStack = new Stack<Command>();
        redoStack = new Stack<Command>();

        tileCasting = FindObjectsOfType<TileCasting>();
    }

    private void Update()
    {
        DoMove();
        
        if (Input.GetKeyDown(KeyCode.Z))
        {
            if      (undoStack.Count > 0) undoStack.Pop().Undo(gameObject, redoStack);
            else    Debug.Log("Nothing left to Undo");
        }

        if (Input.GetKeyDown(KeyCode.Y))
        {
            if      (redoStack.Count > 0) redoStack.Pop().Redo(gameObject, undoStack);
            else    Debug.Log("Nothing left to Redo");
        }
    }

    private void DoMove()
    {
        if (Input.GetKeyDown(upButton))
        {
            moveUpCommand.Execute(gameObject, undoStack, redoStack);
            didMove = true;
        }

        if (Input.GetKeyDown(downButton))
        {
            moveDownCommand.Execute(gameObject, undoStack, redoStack);
            didMove = true;
        }

        if (Input.GetKeyDown(leftButton))
        {
            moveLeftCommand.Execute(gameObject, undoStack, redoStack);
            didMove = true;
        }

        if (Input.GetKeyDown(rightButton))
        {
            moveRightCommand.Execute(gameObject, undoStack, redoStack);
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
