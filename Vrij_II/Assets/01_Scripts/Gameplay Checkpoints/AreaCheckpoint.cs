
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaCheckpoint : GameCheckpoint {

    [SerializeField]
    private float previousZoomTarget;
    [SerializeField]
    private float nextZoomTarget;

    protected override void DoBehaviour() {
        if(!playerIsPassed) {
            PlayerManager.ZoomCamera?.Invoke(nextZoomTarget);
        }
        else {
            PlayerManager.ZoomCamera?.Invoke(previousZoomTarget);
        }
    }

}