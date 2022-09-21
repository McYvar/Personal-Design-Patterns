using System.Collections.Generic;
using UnityEngine;

public class MoveCommand : ICommand
{
    public Vector3 direction;
    
    public MoveCommand(Vector3 _direction)
    {
        direction = _direction;
    }
    
    public void Execute(GameObject _obj, Stack<ICommand> _undoStack, Stack<ICommand> _redoStack)
    {
        RaycastHit hit;
        if (!Physics.Raycast(_obj.transform.position - _obj.transform.forward + direction, _obj.transform.forward,
                out hit)) return;

        if (hit.collider.gameObject.GetComponent<TileCasting>() == null) return;
        _obj.transform.position += direction;
        _undoStack.Push(this);
        _redoStack.Clear();
        EmissionsBehaviour.ClearRedoStack();
    }

    public void Undo(GameObject _obj, Stack<ICommand> _redoStack)
    {
        _obj.transform.position -= direction;
        _redoStack.Push(this);
        EmissionsBehaviour.PopUndoStack();
    }

    public void Redo(GameObject _obj, Stack<ICommand> _undoStack)
    {
        _obj.transform.position += direction;
        _undoStack.Push(this);
        EmissionsBehaviour.PushUndoStack(EmissionsBehaviour.emissionRedoStack.Pop());
    }
}