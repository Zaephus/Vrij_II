using UnityEngine;
using UnityEngine.UI;

public class MaterialPropertyChanger : MonoBehaviour
{
    public string propertyName = "_MyProperty";
    public float targetValue = 1f;
    public float duration = 1f;

    private Image image;
    private Material material;
    private float currentValue;
    private float timer;

    private void Start()
    {
        image = GetComponent<Image>();
        material = image.material;
        currentValue = material.GetFloat(propertyName);
        timer = 0f;
    }

    private void Update()
    {
        timer += Time.deltaTime;

        if (timer >= duration)
        {
            timer = duration;
        }

        float t = timer / duration;
        float newValue = Mathf.Lerp(currentValue, targetValue, t);
        material.SetFloat(propertyName, newValue);
    }
}
