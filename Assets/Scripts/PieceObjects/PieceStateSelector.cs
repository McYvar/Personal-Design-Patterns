using UnityEngine;
using UnityEngine.Serialization;

// I use two selector types, when MoveSelection is on after selecting a piece, the piece spot is highlighted
// and then you can select a new spot to place down the piece
public enum SelectorType { PieceSelection, MoveSelection }

[RequireComponent(typeof(Rigidbody))]
public class PieceStateSelector : MonoBehaviour
{
    [SerializeField] private PieceBaseStateObject noPiece;

    // CurrentPiece is the state machine, less formally here, piece selector
    private PieceBaseStateObject currentPiece;

    // Tiles are used for higlighting and seeking empty spaces
    private TileCasting currentTile;
    private TileCasting selectedTile;

    private SelectorType selectorType;

    private void Awake()
    {
        GetComponent<Rigidbody>().isKinematic = true;
    }

    private void Start()
    {
        currentPiece = noPiece;
        currentPiece.EnterState();

        selectorType = SelectorType.PieceSelection;
    }

    private void Update()
    {
        EmptySpaceCheck();

        if (selectorType == SelectorType.PieceSelection)
        {
            currentTile.SetEmissionColor(Color.cyan);
            selectedTile = null;
        }
        else
        {
            currentTile.SetEmissionColor(Color.yellow);
            selectedTile.SetEmissionColor(Color.blue);
            selectedTile.EnableEmission();
        }

        // input, I used command for undoing moves, so making this a command wasn't needed
        if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.Space)) SelectorToggle();
        if (Input.GetKeyDown(KeyCode.Escape)) MoveSelectorDisable();

        if (Input.GetKeyDown(KeyCode.UpArrow)) SelectorMovement(transform.up);
        if (Input.GetKeyDown(KeyCode.DownArrow)) SelectorMovement(-transform.up);
        if (Input.GetKeyDown(KeyCode.LeftArrow)) SelectorMovement(-transform.right);
        if (Input.GetKeyDown(KeyCode.RightArrow)) SelectorMovement(transform.right);

        if (Input.GetKeyDown(KeyCode.Z))
        {
            if (CommandHistory.undoStack.Count > 0) CommandHistory.undoStack.Pop().Undo(CommandHistory.redoStack);
            else Debug.Log("Nothing left to Undo");
        }

        if (Input.GetKeyDown(KeyCode.Y))
        {
            if (CommandHistory.redoStack.Count > 0) CommandHistory.redoStack.Pop().Redo(CommandHistory.undoStack);
            else Debug.Log("Nothing left to Redo");
        }
    }

    // Upon pressing "return/space" and the selector collides with a piece, then we highlight the tile and select this piece for moving
    private void SelectorToggle()
    {
        switch (selectorType)
        {
            case SelectorType.PieceSelection:
                if (currentPiece == noPiece) return;
                selectorType = SelectorType.MoveSelection;
                selectedTile = currentTile;
                break;

            case SelectorType.MoveSelection:
                currentPiece.DoMove(new Vector3(transform.position.x, transform.position.y, currentPiece.GetPosition().z));
                MoveSelectorDisable();
                break;
        }
    }

    // Upon pressing "escape" we go back to the (if selected) selected tile... and we can select things again
    private void MoveSelectorDisable()
    {
        if (selectorType == SelectorType.PieceSelection) return;
        selectedTile.DisableEmission();
        currentTile.EnableEmission();
        selectorType = SelectorType.PieceSelection;
        transform.position = new Vector3(currentPiece.GetPosition().x, currentPiece.GetPosition().y);
    }

    private void SelectorMovement(Vector3 _direction)
    {
        transform.position += _direction;
    }

    private void OnTriggerEnter(Collider _trigger)
    {
        TileTriggerBehaviour(_trigger);
        PieceTriggerBehaviour(_trigger);
    }

    // When selector collides with a tile, it's highlighted
    private void TileTriggerBehaviour(Collider _trigger)
    {
        TileCasting colliderTile = _trigger.GetComponent<TileCasting>();
        if (colliderTile == null) return;

        currentTile?.DisableEmission();
        currentTile = colliderTile;
        currentTile.SetEmissionColor(Color.cyan);
        currentTile.EnableEmission();
    }

    // When selector collides with a piece, you can select it
    private void PieceTriggerBehaviour(Collider _trigger)
    {
        PieceCasting castPiece = _trigger.GetComponent<PieceCasting>();
        if (castPiece == null) return;

        PieceBaseStateObject targetPiece = castPiece.pieceType;
        if (selectorType == SelectorType.PieceSelection)
        {
            if (targetPiece == currentPiece) return;
            SwitchPiece(targetPiece);
        }
    }

    // If the hovered space doesn't contain a piece, switch to noPiece
    private void EmptySpaceCheck()
    {
        if (selectorType == SelectorType.MoveSelection) return;
        RaycastHit hit;
        if (Physics.Raycast(transform.position - transform.forward * 10, transform.forward, out hit))
        {
            if (hit.collider.gameObject.GetComponent<PieceCasting>() != null) return;
            SwitchPiece(noPiece);
        }
    }

    private void SwitchPiece(PieceBaseStateObject _pieceBaseStateObject)
    {
        currentPiece = _pieceBaseStateObject;
        currentPiece.EnterState();
    }
}
