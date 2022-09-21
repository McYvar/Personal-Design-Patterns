using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class TileCasting : MonoBehaviour
{
    private Material myMaterial;
    private void Awake()
    {
        GetComponent<BoxCollider>().isTrigger = true;
        myMaterial = GetComponent<MeshRenderer>().material;
    }

    public void EnableEmission()
    {
        myMaterial.EnableKeyword("_EMISSION");
    }

    public void DisableEmission()
    {
        myMaterial.DisableKeyword("_EMISSION");
    }

    public void SetEmissionColor(Color _color)
    {
        myMaterial.SetColor("_EmissionColor", _color);
    }
}
