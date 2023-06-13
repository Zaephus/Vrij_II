using UnityEngine;

public class MaterialPropertyChanger : MonoBehaviour
{
    public string propertyName = "_MyProperty";
    public float targetValue = 1f;
    public float duration = 1f;

    private SpriteRenderer spriteRenderer;
    private Material material;
    private float currentValue;
    private float timer;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        material = spriteRenderer.material;
        currentValue = material.GetFloat(propertyName);
        timer = 0f;
    }

    private void Update()
    {
       // Desolve();
    }


    private void Desolve()
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
