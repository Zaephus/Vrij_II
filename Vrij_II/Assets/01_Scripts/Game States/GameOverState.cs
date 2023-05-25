
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverState : BaseState<GameManager> {

    [SerializeField]
    private GameObject gameOverMenu;

    public override void OnStart() {
        runner.level.SetActive(false);
        gameOverMenu.SetActive(true);
    }

    public override void OnUpdate() {}

    public override void OnEnd() {}

}