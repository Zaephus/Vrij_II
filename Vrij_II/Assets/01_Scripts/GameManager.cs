
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SaveSystem;

public class GameManager : MonoBehaviour {

    public static Action PlayerDied;
    public static Action FinishReached;

    [HideInInspector]
    public SaveManager saveManager;

    [ReadOnly]
    public GameState gameState;

    private BaseState<GameManager> currentState;

    public GameObject mainMenu;
    public GameObject level;

    private void Start() {
        saveManager = GetComponent<SaveManager>();

        SwitchState(GameState.Initialization);
    }

    private void Update() {

        currentState.OnUpdate();
        
        if(Input.GetKeyDown(KeyCode.Escape)) {
            Application.Quit();
        }
    
    }

    // TODO: Make this more loosely coupled to the amount of states.
    public void SwitchState(GameState _state) {
        if(currentState != null) {
            currentState.OnEnd();
        }

        // TODO: Add null-check.
        switch(_state) {
            case GameState.Initialization:
                currentState = GetComponent<InitializationState>();
                break;
            case GameState.MainMenu:
                currentState = GetComponent<MainMenuState>();
                break;
            case GameState.Running:
                currentState = GetComponent<RunningState>();
                break;
            case GameState.Finished:
                currentState = GetComponent<EndState>();
                break;
            case GameState.GameOver:
                currentState = GetComponent<GameOverState>();
                break;
            default:
                return;
        }

        gameState = _state;

        Debug.Log("Switched State to " + _state, this);

        currentState.OnStart();
    }

}