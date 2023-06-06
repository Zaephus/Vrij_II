
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndGameCheckpoint : GameCheckpoint {

    protected override void DoBehaviour() {
        GameManager.FinishReached?.Invoke();
    }
    
}