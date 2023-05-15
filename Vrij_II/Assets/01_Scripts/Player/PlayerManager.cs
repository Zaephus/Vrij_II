using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour, IDamageable {

    [SerializeField]
    private PlayerInputManager playerInput;
    [SerializeField]
    private PlayerMovement playerMovement;
    [SerializeField]
    private GameObject aimingTarget;
    [SerializeField]
    private bool hasSpear;

    private void Start() {

    }

    private void Update() {
        aimingTarget.SetActive(playerInput.isAiming);
        playerInput.hasSpear = hasSpear;
        playerMovement.spear.SetActive(hasSpear);
    }

    private void FixedUpdate(){
        playerMovement.Move(transform, playerInput.leftHorizontalInput, playerInput.leftVerticalInput);
        if (playerInput.isAiming && hasSpear)
        {
            playerMovement.Aim(transform, playerInput.rightHorizontalInput, playerInput.rightVerticalInput);
        }
    }

    public void Throw()
    {
        if (hasSpear)
        {
            hasSpear = playerMovement.Throw();
        }
    }

    public void Hit() {
        GameManager.PlayerDied?.Invoke();
    }

}
