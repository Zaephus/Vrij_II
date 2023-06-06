
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameCheckpoint : MonoBehaviour {

    public static Action<Vector3> CheckpointReached;
    
    protected virtual void DoBehaviour() {}

    private void OnTriggerEnter(Collider _other) {
        if(_other.GetComponent<PlayerManager>() != null) {
            DoBehaviour();
            CheckpointReached?.Invoke(transform.position);
        }
    }

}