using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public static class CommandHistory
{
    // Just to keep track of the moves we made...
    public static readonly Stack<ICommand> undoStack = new Stack<ICommand>();
    public static readonly Stack<ICommand> redoStack = new Stack<ICommand>();
}