using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public enum SelectorType { PieceSelection, MoveSelection }

[RequireComponent(typeof(Rigidbody))]
public class PieceStateSelector : MonoBehaviour
{
    [FormerlySerializedAs("startPieceBaseState")] [FormerlySerializedAs("startState")] [SerializeField] private PieceBaseStateObject startPieceState;
    private PieceBaseStateObject currentPieceBaseState;
    private TileCasting currentTile;

    private SelectorType selectorType = SelectorType.PieceSelection;

    private void Awake()
    {
        GetComponent<Rigidbody>().isKinematic = true;
    }

    private void Start()
    {
        

        currentPieceBaseState = startPieceState;
        currentPieceBaseState.EnterState();
    }

    private void Update()
    {
        currentPieceBaseState.Update();
        
        // input
        if (Input.GetKeyDown(KeyCode.Return)) SelectorToggle();
        if (Input.GetKeyDown(KeyCode.Escape)) MoveSelectorDisable();

        if (Input.GetKeyDown(KeyCode.UpArrow)) SelectorMovement(transform.up);
        if (Input.GetKeyDown(KeyCode.DownArrow)) SelectorMovement(-transform.up);
        if (Input.GetKeyDown(KeyCode.LeftArrow)) SelectorMovement(-transform.right);
        if (Input.GetKeyDown(KeyCode.RightArrow)) SelectorMovement(transform.right);
    }

    private void SelectorToggle()
    {
        currentPieceBaseState.isSelected = !currentPieceBaseState.isSelected;
        selectorType = selectorType == SelectorType.PieceSelection
            ? SelectorType.MoveSelection
            : SelectorType.PieceSelection;
    }

    private void MoveSelectorDisable()
    {
        selectorType = SelectorType.PieceSelection;
    }

    private void SelectorMovement(Vector3 _direction)
    {
        transform.position += _direction;
    }

    private void SwitchState(PieceBaseStateObject _pieceBaseStateObject)
    {
        currentPieceBaseState.ExitState();
        currentPieceBaseState = _pieceBaseStateObject;
        currentPieceBaseState.EnterState();
    }

    private void OnTriggerStay(Collider _trigger)
    {
        TileCasting colliderTile = _trigger.GetComponent<TileCasting>();
        if (colliderTile != null)
        {
            if (currentTile == colliderTile) return;
            if (currentTile != null && selectorType == SelectorType.PieceSelection) currentTile.DisableEmission();
            currentTile = colliderTile;
            currentTile.EnableEmission();

            switch (selectorType)
            {
                case SelectorType.PieceSelection:
                    currentTile.SetEmissionColor(Color.cyan);
                    break;

                case SelectorType.MoveSelection:
                    currentTile.SetEmissionColor(Color.green);
                    break;
            }

        }

        PieceCasting cast = _trigger.GetComponent<PieceCasting>();
        if (cast != null)
        {
            PieceBaseStateObject castPieceState = cast.pieceBaseStateObject;
            if (castPieceState == currentPieceBaseState && selectorType == SelectorType.PieceSelection) return;
            SwitchState(castPieceState);
        }
        else if (selectorType == SelectorType.PieceSelection)
        {
            SwitchState(startPieceState);
        }
    }
}
