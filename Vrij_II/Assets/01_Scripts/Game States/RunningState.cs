
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunningState : BaseState<GameManager> {

    public override void OnStart() {
        runner.level.SetActive(true);
    }

    public override void OnUpdate() {
        runner.saveManager.OnUpdate();
    }

    public override void OnEnd() {

    }
    
}