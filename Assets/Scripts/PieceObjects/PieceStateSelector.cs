using UnityEngine;
using UnityEngine.Serialization;

public enum SelectorType { PieceSelection, MoveSelection }

[RequireComponent(typeof(Rigidbody))]
public class PieceStateSelector : MonoBehaviour
{
    [FormerlySerializedAs("startPieceBaseState")] [FormerlySerializedAs("startState")] [SerializeField] private PieceBaseStateObject noPiece;
    private PieceBaseStateObject currentPiece;
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

        // input
        if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.Space)) SelectorToggle();
        if (Input.GetKeyDown(KeyCode.Escape)) MoveSelectorDisable();

        if (Input.GetKeyDown(KeyCode.UpArrow)) SelectorMovement(transform.up);
        if (Input.GetKeyDown(KeyCode.DownArrow)) SelectorMovement(-transform.up);
        if (Input.GetKeyDown(KeyCode.LeftArrow)) SelectorMovement(-transform.right);
        if (Input.GetKeyDown(KeyCode.RightArrow)) SelectorMovement(transform.right);

        if (Input.GetKeyDown(KeyCode.Z))
        {
            if (Commandos.undoStack.Count > 0) Commandos.undoStack.Pop().Undo(Commandos.redoStack);
            else Debug.Log("Nothing left to Undo");
        }

        if (Input.GetKeyDown(KeyCode.Y))
        {
            if (Commandos.redoStack.Count > 0) Commandos.redoStack.Pop().Redo(Commandos.undoStack);
            else Debug.Log("Nothing left to Redo");
        }

        Debug.Log(Commandos.undoStack.Count);
    }

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

    private void TileTriggerBehaviour(Collider _trigger)
    {
        TileCasting colliderTile = _trigger.GetComponent<TileCasting>();
        if (colliderTile == null) return;

        currentTile?.DisableEmission();
        currentTile = colliderTile;
        currentTile.SetEmissionColor(Color.cyan);
        currentTile.EnableEmission();
    }

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
