
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndState : BaseState<GameManager> {

    [SerializeField]
    private GameObject endMenu;

    public override void OnStart() {
        runner.level.SetActive(false);
        endMenu.SetActive(true);
    }

    public override void OnUpdate() {}

    public override void OnEnd() {}
    
}