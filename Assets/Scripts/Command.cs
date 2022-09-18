using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public abstract class Command
{
    // Things Execute must do, execute action, save action to Undo list, delete Redo list
    public virtual void Execute(GameObject obj, Stack<Command> undoStack, Stack<Command> redoStack)
    {
        EmissionsBehaviour.ClearRedoStack();
    }

    // Things Undo must do, Undo previous action retrieved from the list, then add it to the Redo list
    public virtual void Undo(GameObject obj, Stack<Command> redoStack)
    {
        EmissionsBehaviour.PopUndoStack();
    }
    
    // Things Redo must do, Redo an Undo action, then add this action to the Undo list
    public virtual void Redo(GameObject obj, Stack<Command> undoStack)
    {
        EmissionsBehaviour.PushUndoStack(EmissionsBehaviour.emissionRedoStack.Pop());
    }
}


