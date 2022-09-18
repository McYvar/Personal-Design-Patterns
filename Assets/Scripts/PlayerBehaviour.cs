using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerBehaviour : MonoBehaviour
{
    #region variables

    private TileCasting[] tileCasting;
    
    private Command moveUpCommand = new MoveUpCommand();
    private Command moveDownCommand = new MoveDownCommand();
    private Command moveLeftCommand = new MoveLeftCommand();
    private Command moveRightCommand = new MoveRightCommand();
    
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

public class MoveUpCommand : Command
{
    public override void Execute(GameObject obj, Stack<Command> undoStack, Stack<Command> redoStack)
    {
        base.Execute(obj, undoStack, redoStack);
        obj.transform.position += obj.transform.up;
        undoStack.Push(this);
        redoStack.Clear();
    }

    public override void Undo(GameObject obj, Stack<Command> redoStack)
    {
        base.Undo(obj, redoStack);
        obj.transform.position -= obj.transform.up;
        redoStack.Push(this);
    }

    public override void Redo(GameObject obj, Stack<Command> undoStack)
    {
        base.Redo(obj, undoStack);
        obj.transform.position += obj.transform.up;
        undoStack.Push(this);
    }
}


public class MoveDownCommand : Command
{
    public override void Execute(GameObject obj, Stack<Command> undoStack, Stack<Command> redoStack)
    {
        base.Execute(obj, undoStack, redoStack);
        obj.transform.position -= obj.transform.up;
        undoStack.Push(this);
        redoStack.Clear();
    }

    public override void Undo(GameObject obj, Stack<Command> redoStack)
    {
        base.Undo(obj, redoStack);
        obj.transform.position += obj.transform.up;
        redoStack.Push(this);
    }

    public override void Redo(GameObject obj, Stack<Command> undoStack)
    {
        base.Redo(obj, undoStack);
        obj.transform.position -= obj.transform.up;
        undoStack.Push(this);
    }
}


public class MoveLeftCommand : Command
{
    public override void Execute(GameObject obj, Stack<Command> undoStack, Stack<Command> redoStack)
    {
        base.Execute(obj, undoStack, redoStack);
        obj.transform.position -= obj.transform.right;
        undoStack.Push(this);
        redoStack.Clear();
    }

    public override void Undo(GameObject obj, Stack<Command> redoStack)
    {
        base.Undo(obj, redoStack);
        obj.transform.position += obj.transform.right;
        redoStack.Push(this);
    }

    public override void Redo(GameObject obj, Stack<Command> undoStack)
    {
        base.Redo(obj, undoStack);
        obj.transform.position -= obj.transform.right;
        undoStack.Push(this);
    }
}


public class MoveRightCommand : Command
{
    public override void Execute(GameObject obj, Stack<Command> undoStack, Stack<Command> redoStack)
    {
        base.Execute(obj, undoStack, redoStack);
        obj.transform.position += obj.transform.right;
        undoStack.Push(this);
        redoStack.Clear();
    }

    public override void Undo(GameObject obj, Stack<Command> redoStack)
    {
        base.Undo(obj, redoStack);
        obj.transform.position -= obj.transform.right;
        redoStack.Push(this);
    }

    public override void Redo(GameObject obj, Stack<Command> undoStack)
    {
        base.Redo(obj, undoStack);
        obj.transform.position += obj.transform.right;
        undoStack.Push(this);
    }
}