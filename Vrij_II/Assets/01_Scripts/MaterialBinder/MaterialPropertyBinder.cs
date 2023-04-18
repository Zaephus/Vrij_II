using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class MaterialPropertyBinder : MonoBehaviour
{
    [System.Serializable]
    public class MaterialBinding
    {
        public Material material;
    }

    public List<string> propertyNames = new List<string>();
    public List<MaterialBinding> materialBindings = new List<MaterialBinding>();
    public Transform targetObject;

    private Dictionary<string, List<Material>> bindingDict = new Dictionary<string, List<Material>>();

    void Start()
    {
        UpdateBindingDict();
    }

    void Update()
    {
        if (targetObject == null) return;

        Vector4 position = targetObject.position;

        foreach (var kvp in bindingDict)
        {
            string propertyName = kvp.Key;
            List<Material> materials = kvp.Value;

            if (materials != null && materials.Count > 0)
            {
                foreach (var material in materials)
                {
                    if (material != null && material.HasProperty(propertyName))
                    {
                        material.SetVector(propertyName, position);
                    }
                    else
                    {
                        Debug.LogError($"Material {material.name} does not have property {propertyName}");
                    }
                }
            }
            else
            {
                if (materials == null || materials.Count == 0)
                {
                    Debug.LogWarning($"No materials found for property {propertyName}");
                }
            }
        }
    }

    void OnValidate()
    {
        UpdateBindingDict();
    }

    void UpdateBindingDict()
    {
        bindingDict.Clear();
        foreach (var propertyName in propertyNames)
        {
            bindingDict[propertyName] = new List<Material>();
            foreach (var binding in materialBindings)
            {
                if (binding.material != null && binding.material.HasProperty(propertyName))
                {
                    bindingDict[propertyName].Add(binding.material);
                }
            }
        }
    }
}
