
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunningState : BaseState<GameManager> {

    public override void OnStart() {
        runner.level.SetActive(true);

        GameManager.PlayerDied += GameOver;
        GameManager.FinishReached += EndGame;

    }

    public override void OnUpdate() {
        runner.saveManager.OnUpdate();
    }

    public override void OnEnd() {

    }

    public void EndGame() {
        runner.SwitchState(GameState.Finished);
    }

    public void GameOver() {
        
        runner.SwitchState(GameState.GameOver);
    }
    
}