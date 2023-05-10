using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputManager: MonoBehaviour
{
    public float leftHorizontalInput;
    public float leftVerticalInput;
    public float rightHorizontalInput;
    public float rightVerticalInput;
    public bool isAiming;

    // A method that receives input from input manager, that lets you move around
    public void OnMove(InputAction.CallbackContext context){

        Vector2 inputVector = context.ReadValue<Vector2>();
        leftHorizontalInput = inputVector.x;
        leftVerticalInput = inputVector.y;
    }

    // A method that receives input from input manager, when you look around/aim
    public void OnLook(InputAction.CallbackContext context){

        Vector2 inputVector = context.ReadValue<Vector2>();
        rightHorizontalInput = inputVector.x;
        rightVerticalInput = inputVector.y;
    }

    public void OnAim(InputAction.CallbackContext context){
        if (context.performed)
        {
            isAiming = true;
        }
        if (context.canceled)
        {
            isAiming = false;
        }

        GetComponent<AnimatorLayerWeight>().isAiming = isAiming;
    }
}
