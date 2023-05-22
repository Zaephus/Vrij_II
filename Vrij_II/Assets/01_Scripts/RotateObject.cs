using UnityEngine;
using UnityEditor;

[ExecuteInEditMode]
public class RotateObject : MonoBehaviour
{
    public float rotationSpeed = 30f; // Angle to add per second

    private void OnEnable()
    {
        EditorApplication.update += UpdateRotation;
    }

    private void OnDisable()
    {
        EditorApplication.update -= UpdateRotation;
    }

    private void UpdateRotation()
    {
        float angleToAdd = rotationSpeed * Time.deltaTime; // Calculate the angle to add based on time

        // Rotate the object around its own transform by the calculated angle
        transform.Rotate(Vector3.up, angleToAdd);
    }
}
