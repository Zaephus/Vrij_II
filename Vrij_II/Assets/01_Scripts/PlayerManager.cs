using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour, IDamageable {

    private void Start() {

    }

    private void Update() {

    }

    public void Hit() {
        GameManager.PlayerDied?.Invoke();
    }

}
