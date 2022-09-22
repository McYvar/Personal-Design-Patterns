using System.Collections.Generic;
using UnityEngine;

// Command interface, inherit this and you can create objects that execute a specific command that can be undone/redone
public class MoveCommand : ICommand
{
    private Vector3 position;

    // This command keeps track of it's own position history
    private Stack<Vector3> oldPositions;
    private Stack<Vector3> oldUndoPositions;

    private GameObject thisObject;
    
    public MoveCommand(GameObject _obj ,Vector3 _direction)
    {
        thisObject = _obj;
        position = _direction;

        oldPositions = new Stack<Vector3>();
        oldUndoPositions = new Stack<Vector3>();
    }

    public void ChangeDirection(Vector3 _newDir)
    {
        position = _newDir;
    }
    
    public void Execute(Stack<ICommand> _undoStack, Stack<ICommand> _redoStack)
    {
        oldPositions.Push(thisObject.transform.position);
        oldUndoPositions.Clear();
        thisObject.transform.position = position;
        _undoStack.Push(this);
        _redoStack.Clear();
    }

    public void Undo(Stack<ICommand> _redoStack)
    {
        Vector3 tempPos = oldPositions.Pop();
        oldUndoPositions.Push(thisObject.transform.position);
        thisObject.transform.position = tempPos;
        _redoStack.Push(this);
    }

    public void Redo(Stack<ICommand> _undoStack)
    {
        Vector3 tempPos = oldUndoPositions.Pop();
        oldPositions.Push(thisObject.transform.position);
        thisObject.transform.position = tempPos;
        _undoStack.Push(this);
    }
}