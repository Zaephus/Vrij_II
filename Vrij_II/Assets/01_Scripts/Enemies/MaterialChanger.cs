
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaterialChanger : MonoBehaviour
{
    // Store the original materials of the renderers
    private Dictionary<Renderer, Material[]> originalMaterials = new Dictionary<Renderer, Material[]>();

    // The material to apply to the renderers
    public Material targetMaterial;

    // List of renderers to modify
    public List<Renderer> renderers;

    [SerializeField]
    private float betweenSwapDelay = 0.1f;

    public IEnumerator SwapMaterials() {
        ApplyTargetMaterial();
        yield return new WaitForSeconds(betweenSwapDelay);
        RevertToOriginalMaterials();
    }

    // Apply the target material to all renderers
    private void ApplyTargetMaterial()
    {
        // Store the original materials if not already stored
        if (originalMaterials.Count == 0)
        {
            foreach (Renderer renderer in renderers)
            {
                originalMaterials.Add(renderer, renderer.sharedMaterials);
            }
        }

        // Apply the target material to all renderers
        foreach (Renderer renderer in renderers)
        {
            Material[] newMaterials = new Material[renderer.sharedMaterials.Length];
            for (int i = 0; i < newMaterials.Length; i++)
            {
                newMaterials[i] = targetMaterial;
            }
            renderer.sharedMaterials = newMaterials;
        }
    }

    // Revert the materials of the renderers to their original materials
    private void RevertToOriginalMaterials()
    {
        foreach (KeyValuePair<Renderer, Material[]> kvp in originalMaterials)
        {
            kvp.Key.sharedMaterials = kvp.Value;
        }
    }
}
