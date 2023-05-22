using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputManager : MonoBehaviour
{

    public float leftHorizontalInput;
    public float leftVerticalInput;
    public float rightHorizontalInput;
    public float rightVerticalInput;
    public bool isAiming;
    public bool hasSpear;
    public bool isInteracting;

    // A method that receives input from input manager, that lets you move around
    public void OnMove(InputAction.CallbackContext context)
    {
        Vector2 inputVector = context.ReadValue<Vector2>();
        leftHorizontalInput = inputVector.x;
        leftVerticalInput = inputVector.y;
    }

    // A method that receives input from input manager, when you look around/aim
    public void OnLook(InputAction.CallbackContext context)
    {

        Vector2 inputVector = context.ReadValue<Vector2>();
        rightHorizontalInput = inputVector.x;
        rightVerticalInput = inputVector.y;
    }

    public void OnAim(InputAction.CallbackContext context)
    {
        if (hasSpear)
        {
            if (context.performed)
            {
                isAiming = true;
            }
            if (context.canceled)
            {
                isAiming = false;
            }

        }
        else
        {
            isAiming = false;
        }

        GetComponent<AnimatorLayerWeight>().isAiming = isAiming;
        GetComponent<Animator>().SetBool("isAiming", isAiming);
    }

    public void OnFire(InputAction.CallbackContext context)
    {
        if (context.started && isAiming)
        {
            GetComponent<Animator>().SetTrigger("ThrowSpear");
        }

    }

    public void OnInteract(InputAction.CallbackContext context)
    {

        if (context.started)
        {
            isInteracting = true;
        }
        else
        {
            isInteracting = false;
        }

        Debug.Log(isInteracting);
        //isInteracting = context.started;
    }
}
