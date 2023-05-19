
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public abstract class BaseState<T> {

    [HideInInspector]
    public T runner;
    
    public abstract void OnStart();
    public abstract void OnUpdate();
    public abstract void OnEnd();

}