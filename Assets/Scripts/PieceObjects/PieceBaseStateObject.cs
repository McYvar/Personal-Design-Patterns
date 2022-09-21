using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Networking;

public class PieceBaseStateObject : ScriptableObject
{
    [SerializeField] private string pieceName;
    public Transform thisObject;
    public bool isSelected;

    public void Initialize(Transform _transform)
    {
        thisObject = _transform;
    }
    
    public virtual void EnterState()
    {
        isSelected = false;
        Debug.Log("This is a " + pieceName + "!");
    }
    
    public virtual void Update() {}
    
    public virtual void ExitState() {}
}