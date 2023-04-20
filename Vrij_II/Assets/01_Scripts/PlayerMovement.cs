using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    private float leftHorizontalInput;
    private float leftVerticalInput;
    private float rightHorizontalInput;
    private float rightVerticalInput;
    private Vector3 targetPosition;

    [Header("Movement Settings")]
    [SerializeField]
    private float walkSpeed;
    [SerializeField]
    private float runSpeed;
    [SerializeField]
    private float aimSpeed;

    [Header ("Components")]
    [SerializeField]
    private Rigidbody rb;  
    [SerializeField]
    private Animator animator;
    [SerializeField]
    private Transform target;

    private void FixedUpdate(){

        //moving
        Vector3 dir = transform.forward * leftVerticalInput + transform.right * leftHorizontalInput;

        rb.AddForce(dir * walkSpeed * 10, ForceMode.Force);
     
        animator.SetFloat("VelocityZ", dir.normalized.z);
        animator.SetFloat("VelocityX", dir.normalized.x);

        //looking
        targetPosition = target.localPosition;
        targetPosition = new Vector3(rightHorizontalInput, 0, rightVerticalInput);
       
        float angle = Mathf.Atan2(targetPosition.x, targetPosition.z) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, angle, 0);
    }


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
}
