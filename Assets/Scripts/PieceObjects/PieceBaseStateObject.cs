using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Networking;

public class PieceBaseStateObject : ScriptableObject
{
    [SerializeField] private string pieceName;
    public GameObject thisObject;

    public virtual void Initialize(GameObject _gameObject)
    {
        thisObject = _gameObject;
    }

    public Vector3 GetPosition()
    {
        return thisObject.transform.position;
    }
    
    public virtual void EnterState()
    {
        Debug.Log("This is a " + pieceName + "!");
    }

    public virtual void DoMove(Vector3 _position) {}

    public virtual Vector3[] AllowedPositions() 
    {
        return null;
    }
}