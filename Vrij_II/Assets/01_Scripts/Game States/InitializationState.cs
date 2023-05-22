
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitializationState : BaseState<GameManager> {

    [SerializeField]
    private GameObject initMenu;

    public override void OnStart() {

        initMenu.SetActive(true);
        runner.mainMenu.SetActive(false);
        runner.level.SetActive(true);

        runner.saveManager.Initialize();

        initMenu.SetActive(false);

        runner.SwitchState(GameState.MainMenu);

    }

    public override void OnUpdate() {}

    public override void OnEnd() {}
    
}