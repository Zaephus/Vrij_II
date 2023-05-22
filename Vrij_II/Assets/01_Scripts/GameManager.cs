using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SaveSystem;

public class GameManager : MonoBehaviour {

    public static System.Action PlayerDied;

    [HideInInspector]
    public SaveManager saveManager;

    [ReadOnly]
    public GameState gameState;

    private BaseState<GameManager> currentState;

    public GameObject mainMenu;
    public GameObject level;

    // [SerializeField]
    // private GameObject gameOverCanvas;

    private void Start() {
        saveManager = GetComponent<SaveManager>();

        // PlayerDied += EndGame;

        SwitchState(GameState.Initialization);
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
                currentState = GetComponent<InitializationState>();
                break;
            case GameState.MainMenu:
                currentState = GetComponent<MainMenuState>();
                break;
            case GameState.Running:
                currentState = GetComponent<RunningState>();
                break;
            default:
                return;
        }

        gameState = _state;

        Debug.Log("Switched State to " + _state, this);

        currentState.OnStart();
    }

    // private void EndGame() {
    //     level.SetActive(false);
    //     gameOverCanvas.SetActive(true);
    // }

}