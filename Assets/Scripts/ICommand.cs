using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public interface ICommand
{
    // Things Execute must do, execute action, save action to Undo list, delete Redo list
    public abstract void Execute(GameObject _obj, Stack<ICommand> _undoStack, Stack<ICommand> _redoStack);

    // Things Undo must do, Undo previous action retrieved from the list, then add it to the Redo list
    public abstract void Undo(GameObject _obj, Stack<ICommand> _redoStack);
    
    // Things Redo must do, Redo an Undo action, then add this action to the Undo list
    public abstract void Redo(GameObject _obj, Stack<ICommand> _undoStack);
}
