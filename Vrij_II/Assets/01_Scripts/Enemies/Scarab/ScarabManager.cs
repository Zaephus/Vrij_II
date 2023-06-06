
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScarabManager : MonoBehaviour {

    [HideInInspector]
    public float globalRange;

    [HideInInspector]
    public bool playerInRange = false;

    private void Start() {
        globalRange = GetComponent<SphereCollider>().radius;
    }

    private void OnTriggerEnter(Collider _other) {
        if(_other.GetComponent<PlayerManager>() != null) {
            playerInRange = true;
        }
    }

    private void OnTriggerExit(Collider _other) {
        if(_other.GetComponent<PlayerManager>() != null) {
            playerInRange = false;
        }
    }
    
}