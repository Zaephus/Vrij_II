
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseState<T> : MonoBehaviour {

    [HideInInspector]
    public T runner;

    private void Awake() {
        runner = GetComponent<T>();
    }
    
    public abstract void OnStart();
    public abstract void OnUpdate();
    public abstract void OnEnd();

}