using UnityEngine;

[CreateAssetMenu(menuName = "ChessObjects/NoPiece")]
public class NoPieceObject : PieceBaseStateObject
{
    public override void EnterState()
    {
        Debug.Log("Nothing is selected");
    }
}