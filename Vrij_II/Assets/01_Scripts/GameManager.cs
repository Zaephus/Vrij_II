
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    public static Action PlayerDied;
    public static Action FinishReached;

    public GameState gameState;

    [HideInInspector]
    public Vector3 respawnPoint;

    private BaseState<GameManager> currentState;

    private void Start() {
        GameCheckpoint.CheckpointReached += SetRespawnPoint;
        SwitchState(gameState);
    }

    private void Update() {
        currentState.OnUpdate();
    }

    public void SwitchState(GameState _state) {
        if(currentState != null) {
            currentState.OnEnd();
        }

        switch(_state) {
            case GameState.MainMenu:
                currentState = GetComponent<MainMenuState>();
                break;
            case GameState.Running:
                currentState = GetComponent<RunningState>();
                break;
            case GameState.Paused:
                currentState = GetComponent<PausedState>();
                break;
            case GameState.Finished:
                currentState = GetComponent<EndState>();
                break;
            case GameState.GameOver:
                currentState = GetComponent<GameOverState>();
                break;
            default:
                currentState.OnStart();
                return;
        }

        gameState = _state;

#if UNITY_EDITOR
        Debug.Log("Switched State to " + _state, this);
#endif

        currentState.OnStart();
    }

    public void RestartGame() {
        // SceneManager.LoadScene("0_MainScene", LoadSceneMode.Single);
        ExitGame();
    }

    public void ExitGame() {
        Application.Quit();
    }

    private void SetRespawnPoint(Vector3 _point) {
        respawnPoint = _point;
    }

}