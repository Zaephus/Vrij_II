
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PausedState : BaseState<GameManager> {

    [SerializeField]
    private GameObject pauseMenu;
    
    public override void OnStart() {
        pauseMenu.SetActive(true);
    }

    public override void OnUpdate() {

    }

    public override void OnEnd() {
        pauseMenu.SetActive(false);
    }

}