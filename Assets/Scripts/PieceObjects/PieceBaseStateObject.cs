using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PieceBaseStateObject : ScriptableObject
{
    // Since each piece will be a scriptable object, we can easily make new objects for the state machine
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

    // Specifically this method needs to be overwritten, since each different type of piece has different move conditions
    public virtual Vector3[] AllowedPositions() 
    {
        return null;
    }

    // Last check if we can execute, check if the tile is free
    public bool CanExecute(Vector3 _position)
    {
        RaycastHit hit;
        if (Physics.Raycast(_position + thisObject.transform.forward * 10, -thisObject.transform.forward, out hit))
        {
            if (hit.collider == null) return false;
            if (hit.collider.GetComponent<TileCasting>() != null) return true;
            Debug.Log(hit.collider.gameObject);
        }
        return false;
    }
}