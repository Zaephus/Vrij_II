
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IKTarget : MonoBehaviour {

    private RaycastHit hit;

    private void Update() {
        if (Physics.Raycast(new Ray(transform.position + Vector3.up, -Vector3.up), out hit)) {
            if (hit.collider.tag == "Ground") {
                if (hit.distance > 0.01f) {
                transform.position = hit.point;
                }
            }
        }
    }

    private void OnDrawGizmos() {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position, 0.1f);

        Gizmos.color = Color.blue;
        Gizmos.DrawRay(new Ray(transform.position, -Vector3.up));
    }

}
