
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
        // TODO: Cannot destroy gameobjects with destroyImmediate here.
        level.SetActive(false);
    }

    public void PauseGame() {
        runner.SwitchState(GameState.Paused);
    }

    public void EndGame() {
        runner.SwitchState(GameState.Finished);
    }

    public void GameOver() {
        Destroy(level);
        runner.SwitchState(GameState.GameOver);
    }

    private void OnDestroy() {
        GameManager.PlayerDied -= GameOver;
        GameManager.FinishReached -= EndGame;

        PlayerInputManager.GamePaused -= PauseGame;
    }
    
}