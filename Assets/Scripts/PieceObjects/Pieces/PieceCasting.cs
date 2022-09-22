using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;

[RequireComponent(typeof(BoxCollider))]
public class PieceCasting : MonoBehaviour
{   
    // Every object needs a monobehaviour to cast the scriptable object to the state machine
    [SerializeField] public PieceBaseStateObject pieceType;

    private void Awake()
    {
        GetComponent<BoxCollider>().isTrigger = true;
    }

    private void Start()
    {
        pieceType.Initialize(gameObject);
    }
}
