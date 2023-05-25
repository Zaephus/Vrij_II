
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndGameCheckpoint : MonoBehaviour {

    private void OnTriggerEnter(Collider _other) {
        if(_other.GetComponent<PlayerManager>() != null) {
            GameManager.FinishReached?.Invoke();
        }
    }
    
}