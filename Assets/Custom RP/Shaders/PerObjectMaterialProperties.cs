using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;


[DisallowMultipleComponent]
public class PerObjectMaterialProperties : MonoBehaviour
{
    private static int baseColorId = Shader.PropertyToID("_BaseColor");
    private static int cutoffId = Shader.PropertyToID("_Cutoff");
    private static int metallicId = Shader.PropertyToID("_Metallic");
    private static int smoothnessId = Shader.PropertyToID("_Smoothness");

    [SerializeField] private Color baseColor = Color.white;

    [SerializeField, Range(0f, 1f)] private float
        cutoff = 0.5f,
        metallic = 0f,
        smoothness = 0.5f;
    static MaterialPropertyBlock block;

    private void OnValidate()
    {
        if (block == null)
        {
            block = new MaterialPropertyBlock();
        }

        // baseColor = Random.Range(0.0f, 1.0f) > 0.5f ? Color.red : Color.yellow;
        block.SetColor(baseColorId, baseColor);
        block.SetFloat(cutoffId, cutoff);
        block.SetFloat(metallicId, metallic);
        block.SetFloat(smoothnessId, smoothness);

        GetComponent<Renderer>().SetPropertyBlock(block);
    }

    void Awake () {
        OnValidate();
    }
}
