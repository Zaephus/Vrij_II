using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseState {

    public static System.Action<string> ExitState;

    protected EnemyController enemyController;

    public BaseState(EnemyController _e) {
        enemyController = _e;
    }
    
    public abstract void OnStart();
    public abstract void OnUpdate();
    public abstract void OnEnd();

}