using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(menuName = "ChessObjects/Pawn")]
public class PawnPieceObject : PieceBaseStateObject
{
    private MoveCommand moveCommand;

    public override void Initialize(GameObject _gameObject)
    {
        base.Initialize(_gameObject);
        moveCommand = new MoveCommand(thisObject, Vector3.up);
    }

    public override void DoMove(Vector3 _position)
    {
        moveCommand.ChangeDirection(_position);
        foreach (Vector3 _pos in AllowedPositions())
        {
            if (_position - thisObject.transform.position == _pos)
            {
                moveCommand.Execute(Commandos.undoStack, Commandos.redoStack);
            }
        }
    }

    public override Vector3[] AllowedPositions()
    {
        // A normal pawn can only move forward and attack foward left/right. Thus array of 3.
        Vector3[] allowed = new Vector3[3];
        allowed[0] = thisObject.transform.up;
        allowed[1] = thisObject.transform.up - thisObject.transform.right;
        allowed[2] = thisObject.transform.up + thisObject.transform.right;
        return allowed;
    }
}