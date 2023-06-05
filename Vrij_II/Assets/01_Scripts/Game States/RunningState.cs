
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunningState : BaseState<GameManager> {

    [SerializeField]
    private GameObject level;

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
        level.SetActive(false);
    }

    public void PauseGame() {
        runner.SwitchState(GameState.Paused);
    }

    public void EndGame() {
        runner.SwitchState(GameState.Finished);
    }

    public void GameOver() {
        runner.SwitchState(GameState.GameOver);
    }
    
}