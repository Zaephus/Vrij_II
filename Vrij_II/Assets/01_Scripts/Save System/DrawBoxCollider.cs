
#if UNITY_EDITOR

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class DrawBoxCollider : MonoBehaviour {

    private BoxCollider boxCollider;

    [SerializeField]
    private bool showCollider;

    [SerializeField]
    private Color color;

    private void Start() {
        boxCollider = GetComponent<BoxCollider>();
    }

    private void Update() {

        if(showCollider) {
            DrawCollider();
        }

    }

    private void DrawCollider() {

        Vector3 size = Vector3.Scale(boxCollider.size/2, transform.localScale);
         
        Vector3[] colliderVertices = {
            transform.position + transform.rotation * (boxCollider.center + new Vector3(size.x, size.y, size.z)),
            transform.position + transform.rotation * (boxCollider.center + new Vector3(size.x, size.y, -size.z)),
            transform.position + transform.rotation * (boxCollider.center + new Vector3(-size.x, size.y, -size.z)),
            transform.position + transform.rotation * (boxCollider.center + new Vector3(-size.x, size.y, size.z)),

            transform.position + transform.rotation * (boxCollider.center + new Vector3(size.x, -size.y, size.z)),
            transform.position + transform.rotation * (boxCollider.center + new Vector3(size.x, -size.y, -size.z)),
            transform.position + transform.rotation * (boxCollider.center + new Vector3(-size.x, -size.y, -size.z)),
            transform.position + transform.rotation * (boxCollider.center + new Vector3(-size.x, -size.y, size.z))
        };

        Debug.DrawLine(colliderVertices[0], colliderVertices[1], color);
        Debug.DrawLine(colliderVertices[1], colliderVertices[2], color);
        Debug.DrawLine(colliderVertices[2], colliderVertices[3], color);
        Debug.DrawLine(colliderVertices[3], colliderVertices[0], color);

        Debug.DrawLine(colliderVertices[4], colliderVertices[5], color);
        Debug.DrawLine(colliderVertices[5], colliderVertices[6], color);
        Debug.DrawLine(colliderVertices[6], colliderVertices[7], color);
        Debug.DrawLine(colliderVertices[7], colliderVertices[4], color);

        Debug.DrawLine(colliderVertices[0], colliderVertices[4], color);
        Debug.DrawLine(colliderVertices[1], colliderVertices[5], color);
        Debug.DrawLine(colliderVertices[2], colliderVertices[6], color);
        Debug.DrawLine(colliderVertices[3], colliderVertices[7], color);

    }
    
}

#endif