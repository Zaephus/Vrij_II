
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EventPassthrough : MonoBehaviour {

    public UnityEvent passthroughEvent;
    
    private void CallEvent() {
        passthroughEvent?.Invoke();
    }
}