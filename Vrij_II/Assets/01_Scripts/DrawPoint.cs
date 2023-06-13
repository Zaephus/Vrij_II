
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class DrawPoint : MonoBehaviour {

    [SerializeField]
    private Color color;

    [SerializeField]
    private float radius;

    private void OnDrawGizmos() {
        Gizmos.color = color;
        Gizmos.DrawSphere(transform.position, radius);
    }
    
}