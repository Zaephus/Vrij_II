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

    [System.Serializable]
    public class PropertyBinding
    {
        public string propertyName;
        public bool isHDRColor;
        public bool isFloat;
        [ColorUsageAttribute(true, true)]
        public Color colorValueHDR;
        public float floatValue;
    }

    public List<PropertyBinding> propertyBindings = new List<PropertyBinding>();
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

        foreach (var binding in propertyBindings)
        {
            string propertyName = binding.propertyName;
            List<Material> materials = bindingDict[propertyName];
            object value;
            if (binding.isHDRColor)
            {
                value = (object)binding.colorValueHDR;
            }
            else if (binding.isFloat)
            {
                value = (object)binding.floatValue;
            }
            else
            {
                value = (object)targetObject.position;
            }

            if (materials != null && materials.Count > 0)
            {
                foreach (var material in materials)
                {
                    if (material != null && material.HasProperty(propertyName))
                    {
                        if (binding.isHDRColor)
                        {
                            material.SetColor(propertyName, binding.colorValueHDR);
                        }
                        else if (binding.isFloat)
                        {
                            material.SetFloat(propertyName, binding.floatValue);
                        }
                        else
                        {
                            material.SetVector(propertyName, targetObject.position);
                        }
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
        foreach (var binding in propertyBindings)
        {
            string propertyName = binding.propertyName;
            bindingDict[propertyName] = new List<Material>();
            foreach (var materialBinding in materialBindings)
            {
                Material material = materialBinding.material;
                if (material != null && material.HasProperty(propertyName))
                {
                    bindingDict[propertyName].Add(material);
                }
            }
        }
    }
}
