
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuState : BaseState<GameManager> {

    [SerializeField]
    private GameObject mainMenu;
    [SerializeField]
    private GameObject startVideo;

    public override void OnStart() {
        mainMenu.SetActive(true);
    }

    public override void OnUpdate() {

    }

    public override void OnEnd() {
        mainMenu.SetActive(false);
    }

    public void StartVideo() {
        mainMenu.SetActive(false);
        startVideo.SetActive(true);
    }

    public void StartGame() {
        startVideo.SetActive(false);
        runner.SwitchState(GameState.Running);
    }
    
}