using System.Collections.Generic;
using UnityEngine;

public class MoveCommand : ICommand
{
    private Vector3 position;
    private Stack<Vector3> oldPositions;
    private GameObject thisObject;
    
    public MoveCommand(GameObject _obj ,Vector3 _direction)
    {
        thisObject = _obj;
        position = _direction;

        oldPositions = new Stack<Vector3>();
    }

    public void ChangeDirection(Vector3 _newDir)
    {
        position = _newDir;
    }
    
    public void Execute(Stack<ICommand> _undoStack, Stack<ICommand> _redoStack)
    {
        oldPositions.Push(thisObject.transform.position);
        thisObject.transform.position = position;
        _undoStack.Push(this);
        _redoStack.Clear();
    }

    public void Undo(Stack<ICommand> _redoStack)
    {
        thisObject.transform.position = oldPositions.Pop();
        _redoStack.Push(this);
    }

    public void Redo(Stack<ICommand> _undoStack)
    {
        thisObject.transform.position = position;
        _undoStack.Push(this);
    }
}