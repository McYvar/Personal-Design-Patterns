using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;

[RequireComponent(typeof(BoxCollider))]
public class PieceCasting : MonoBehaviour
{
    [FormerlySerializedAs("stateObject")] [SerializeField] public PieceBaseStateObject pieceBaseStateObject;
    private void Awake()
    {
        GetComponent<BoxCollider>().isTrigger = true;
    }

    private void Start()
    {
        pieceBaseStateObject.Initialize(transform);
    }
}
