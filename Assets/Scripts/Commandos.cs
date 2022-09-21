using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public static class Commandos
{
    public static readonly Stack<ICommand> undoStack = new Stack<ICommand>();
    public static readonly Stack<ICommand> redoStack = new Stack<ICommand>();
    public static readonly ICommand moveUpCommand = new MoveCommand(Vector3.up);
    public static readonly ICommand moveDownCommand = new MoveCommand(Vector3.down);
    public static readonly ICommand moveLeftCommand = new MoveCommand(Vector3.left);
    public static readonly ICommand moveRightCommand = new MoveCommand(Vector3.right);
}