
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TriggerColliderPassthrough : MonoBehaviour {

    public UnityEvent<Collider> TriggerEnterCall;
    public UnityEvent<Collider> TriggerStayCall;
    public UnityEvent<Collider> TriggerExitCall;

    private void OnTriggerEnter(Collider _other) {
        TriggerEnterCall?.Invoke(_other);
    }

    private void OnTriggerStay(Collider _other) {
        TriggerStayCall?.Invoke(_other);
    }

    private void OnTriggerExit(Collider _other) {
        TriggerExitCall?.Invoke(_other);
    }
    
}