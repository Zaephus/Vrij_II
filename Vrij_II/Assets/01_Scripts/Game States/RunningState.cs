
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunningState : BaseState<GameManager> {

    public GameObject level;

    public override void OnStart() {
        level.SetActive(true);

        GameManager.PlayerDied += GameOver;
        GameManager.FinishReached += EndGame;

        PlayerInputManager.GamePaused += PauseGame;

    }

    public override void OnUpdate() {

    }

    public override void OnEnd() {
        PlayerInputManager.GamePaused -= PauseGame;
    }

    public void PauseGame() {
        level.SetActive(false);
        runner.SwitchState(GameState.Paused);
    }

    public void EndGame() {
        level.SetActive(false);
        runner.SwitchState(GameState.Finished);
    }

    public void GameOver() {
        runner.SwitchState(GameState.GameOver);
    }

    private void OnDestroy() {
        GameManager.PlayerDied -= GameOver;
        GameManager.FinishReached -= EndGame;

        PlayerInputManager.GamePaused -= PauseGame;
    }
    
}