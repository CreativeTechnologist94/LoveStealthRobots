using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaterialApplier : MonoBehaviour
{
    [SerializeField] private Material _originalMaterial;
    [SerializeField] private Material _otherMaterial;

    public void ApplyOriginal()
    {
        ApplyMaterial(_originalMaterial);
    }

    public void ApplyOther()
    {
        ApplyMaterial(_otherMaterial);
    }

    private void ApplyMaterial(Material newMaterial)
    {
        if (TryGetComponent(out MeshRenderer meshRenderer))
            meshRenderer.material = newMaterial;
    }
}
