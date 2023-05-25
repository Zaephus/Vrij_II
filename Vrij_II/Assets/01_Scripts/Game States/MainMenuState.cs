
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuState : BaseState<GameManager> {

    public override void OnStart() {
        runner.mainMenu.SetActive(true);
        runner.level.SetActive(false);
    }

    public override void OnUpdate() {

    }

    public override void OnEnd() {
        
    }

    public void StartGame() {
        runner.mainMenu.SetActive(false);
        runner.SwitchState(GameState.Running);
    }
    
}