
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuState : BaseState<GameManager> {

    [SerializeField]
    private GameObject mainMenu;

    public override void OnStart() {
        mainMenu.SetActive(true);
    }

    public override void OnUpdate() {

    }

    public override void OnEnd() {
        mainMenu.SetActive(false);
    }

    public void StartGame() {
        runner.SwitchState(GameState.Running);
    }
    
}