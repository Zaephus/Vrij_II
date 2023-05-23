
#if UNITY_EDITOR

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class DrawWaypoint : MonoBehaviour {

    [SerializeField]
    private Color color;

    [SerializeField]
    private float radius;

    private void OnDrawGizmos() {
        Gizmos.color = color;
        Gizmos.DrawSphere(transform.position, radius);
    }
    
}

#endif