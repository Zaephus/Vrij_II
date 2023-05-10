using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour, IDamageable {

    [SerializeField]
    private PlayerInputManager playerInput;
    [SerializeField]
    private PlayerMovement playerMovement;
    private bool isAiming;


    private void Start() {

    }

    private void Update() {

    }

    private void FixedUpdate()
    {
        playerMovement.Move(transform, playerInput.leftHorizontalInput, playerInput.leftVerticalInput);
    }

    public void Hit() {
        GameManager.PlayerDied?.Invoke();
    }

}
