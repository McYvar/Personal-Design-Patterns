using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(menuName = "ChessObjects/Pawn")]
public class PawnPieceObject : PieceBaseStateObject
{
    public override void DoMove(Vector3 _position)
    {
        Commandos.moveUpCommand.Execute(thisObject, Commandos.undoStack, Commandos.redoStack);
    }
}