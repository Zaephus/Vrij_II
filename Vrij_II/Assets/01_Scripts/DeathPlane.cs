
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class DeathPlane : MonoBehaviour {

    private void OnTriggerEnter(Collider _other) {

        Debug.Log(_other.name, _other.gameObject);

        if(_other.GetComponent<PlayerManager>() != null) {
            GameManager.PlayerDied?.Invoke();
        }
    }
    
}