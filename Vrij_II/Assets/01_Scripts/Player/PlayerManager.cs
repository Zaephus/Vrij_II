using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour, IDamageable {

    [SerializeField]
    private PlayerInputManager playerInput;
    [SerializeField]
    private PlayerMovement playerMovement;


    private void Start() {

    }

    private void Update() {

    }

    private void FixedUpdate(){
        playerMovement.Move(transform, playerInput.leftHorizontalInput, playerInput.leftVerticalInput);
        if (playerInput.isAiming)
        {
            playerMovement.Aim(transform, playerInput.rightHorizontalInput, playerInput.rightVerticalInput);
        }
    }

    public void Hit() {
        GameManager.PlayerDied?.Invoke();
    }

}
