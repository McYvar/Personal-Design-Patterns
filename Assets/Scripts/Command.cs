using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public abstract class Command
{
    // Things Execute must do, execute action, save action to Undo list, delete Redo list
    public virtual void Execute(GameObject _obj, Stack<Command> _undoStack, Stack<Command> _redoStack) {}

    // Things Undo must do, Undo previous action retrieved from the list, then add it to the Redo list
    public virtual void Undo(GameObject _obj, Stack<Command> _redoStack) {}
    
    // Things Redo must do, Redo an Undo action, then add this action to the Undo list
    public virtual void Redo(GameObject _obj, Stack<Command> _undoStack) {}
}


public class MoveUpCommand : Command
{
    public override void Execute(GameObject _obj, Stack<Command> _undoStack, Stack<Command> _redoStack)
    {
        base.Execute(_obj, _undoStack, _redoStack);
        _obj.transform.position += _obj.transform.up;
        _undoStack.Push(this);
        _redoStack.Clear();
        EmissionsBehaviour.ClearRedoStack();
    }

    public override void Undo(GameObject _obj, Stack<Command> _redoStack)
    {
        base.Undo(_obj, _redoStack);
        _obj.transform.position -= _obj.transform.up;
        _redoStack.Push(this);
        EmissionsBehaviour.PopUndoStack();
    }

    public override void Redo(GameObject _obj, Stack<Command> _undoStack)
    {
        base.Redo(_obj, _undoStack);
        _obj.transform.position += _obj.transform.up;
        _undoStack.Push(this);
        EmissionsBehaviour.PushUndoStack(EmissionsBehaviour.emissionRedoStack.Pop());
    }
}


public class MoveDownCommand : Command
{
    public override void Execute(GameObject _obj, Stack<Command> _undoStack, Stack<Command> _redoStack)
    {
        base.Execute(_obj, _undoStack, _redoStack);
        _obj.transform.position -= _obj.transform.up;
        _undoStack.Push(this);
        _redoStack.Clear();
        EmissionsBehaviour.ClearRedoStack();
    }

    public override void Undo(GameObject _obj, Stack<Command> _redoStack)
    {
        base.Undo(_obj, _redoStack);
        _obj.transform.position += _obj.transform.up;
        _redoStack.Push(this);
        EmissionsBehaviour.PopUndoStack();
    }

    public override void Redo(GameObject _obj, Stack<Command> _undoStack)
    {
        base.Redo(_obj, _undoStack);
        _obj.transform.position -= _obj.transform.up;
        _undoStack.Push(this);
        EmissionsBehaviour.PushUndoStack(EmissionsBehaviour.emissionRedoStack.Pop());
    }
}


public class MoveLeftCommand : Command
{
    public override void Execute(GameObject _obj, Stack<Command> _undoStack, Stack<Command> _redoStack)
    {
        base.Execute(_obj, _undoStack, _redoStack);
        _obj.transform.position -= _obj.transform.right;
        _undoStack.Push(this);
        _redoStack.Clear();
        EmissionsBehaviour.ClearRedoStack();
    }

    public override void Undo(GameObject _obj, Stack<Command> _redoStack)
    {
        base.Undo(_obj, _redoStack);
        _obj.transform.position += _obj.transform.right;
        _redoStack.Push(this);
        EmissionsBehaviour.PopUndoStack();
    }

    public override void Redo(GameObject _obj, Stack<Command> _undoStack)
    {
        base.Redo(_obj, _undoStack);
        _obj.transform.position -= _obj.transform.right;
        _undoStack.Push(this);
        EmissionsBehaviour.PushUndoStack(EmissionsBehaviour.emissionRedoStack.Pop());
    }
}


public class MoveRightCommand : Command
{
    public override void Execute(GameObject _obj, Stack<Command> _undoStack, Stack<Command> _redoStack)
    {
        base.Execute(_obj, _undoStack, _redoStack);
        _obj.transform.position += _obj.transform.right;
        _undoStack.Push(this);
        _redoStack.Clear();
        EmissionsBehaviour.ClearRedoStack();
    }

    public override void Undo(GameObject _obj, Stack<Command> _redoStack)
    {
        base.Undo(_obj, _redoStack);
        _obj.transform.position -= _obj.transform.right;
        _redoStack.Push(this);
        EmissionsBehaviour.PopUndoStack();
    }

    public override void Redo(GameObject _obj, Stack<Command> _undoStack)
    {
        base.Redo(_obj, _undoStack);
        _obj.transform.position += _obj.transform.right;
        _undoStack.Push(this);
        EmissionsBehaviour.PushUndoStack(EmissionsBehaviour.emissionRedoStack.Pop());
    }
}

