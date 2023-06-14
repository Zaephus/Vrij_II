using System.Collections.Generic;
using UnityEngine;

public class MaterialChanger : MonoBehaviour
{
    // Store the original materials of the mesh renderers
    private Dictionary<MeshRenderer, Material[]> originalMaterials = new Dictionary<MeshRenderer, Material[]>();

    // The material to apply to the mesh renderers
    public Material targetMaterial;

    // List of mesh renderers to modify
    public List<MeshRenderer> meshRenderers;

    // Flag to indicate whether to use the target material or the original materials
    public bool useTargetMaterial = false;

    // Apply the target material or revert to the original materials based on the flag
    public void Update()
    {
        if (useTargetMaterial)
        {
            ApplyTargetMaterial();
        }
        else
        {
            RevertToOriginalMaterials();
        }

        // Toggle the flag
       // useTargetMaterial = !useTargetMaterial;
    }

    // Apply the target material to all mesh renderers
    private void ApplyTargetMaterial()
    {
        // Store the original materials if not already stored
        if (originalMaterials.Count == 0)
        {
            foreach (MeshRenderer renderer in meshRenderers)
            {
                originalMaterials.Add(renderer, renderer.sharedMaterials);
            }
        }

        // Apply the target material to all mesh renderers
        foreach (MeshRenderer renderer in meshRenderers)
        {
            Material[] newMaterials = new Material[renderer.sharedMaterials.Length];
            for (int i = 0; i < newMaterials.Length; i++)
            {
                newMaterials[i] = targetMaterial;
            }
            renderer.sharedMaterials = newMaterials;
        }
    }

    // Revert the materials of the mesh renderers to their original materials
    private void RevertToOriginalMaterials()
    {
        foreach (KeyValuePair<MeshRenderer, Material[]> kvp in originalMaterials)
        {
            kvp.Key.sharedMaterials = kvp.Value;
        }
    }
}
