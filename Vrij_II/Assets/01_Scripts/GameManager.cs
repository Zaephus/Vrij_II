using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public static System.Action PlayerDied;

    public GameState gameState = GameState.Initialization;

    private InitializationState initState;
    private MainMenuState mainMenuState;
    private RunningState runningState;

    private BaseState<GameManager> currentState;

    [SerializeField]
    private GameObject level;

    [SerializeField]
    private GameObject gameOverCanvas;

    private void Start() {
        initState.runner = this;
        mainMenuState.runner = this;
        runningState.runner = this;

        PlayerDied += EndGame;
    }

    private void Update() {

        currentState.OnUpdate();
        
        if(Input.GetKeyDown(KeyCode.Escape)) {
            Application.Quit();
        }
    
    }

    public void SwitchState(GameState _state) {
        if(currentState != null) {
            currentState.OnEnd();
        }

        switch(_state) {
            case GameState.Initialization:
                currentState = initState;
                break;
            case GameState.MainMenu:
                currentState = mainMenuState;
                break;
            case GameState.Running:
                currentState = runningState;
                break;
            default:
                return;
        }

        gameState = _state;

        Debug.Log("Switched State to " + _state);

        currentState.OnStart();
    }

    private void EndGame() {
        level.SetActive(false);
        gameOverCanvas.SetActive(true);
    }

}