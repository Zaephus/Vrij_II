
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndState : BaseState<GameManager> {

    [SerializeField]
    private GameObject endMenu;

    [SerializeField]
    private GameObject endVideo;

    public override void OnStart() {
        endVideo.SetActive(true);
    }

    public override void OnUpdate() {}

    public override void OnEnd() {
        endMenu.SetActive(false);
    }

    public void ShowEndMenu() {
        endMenu.SetActive(true);
    }
    
}