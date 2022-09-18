using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileCasting : MonoBehaviour
{
    public void UpdateTiles()
    {
        if (!Physics.Raycast(transform.position, -transform.forward)) return;
        EmissionsBehaviour.PushUndoStack(GetComponent<MeshRenderer>());
    }
}
