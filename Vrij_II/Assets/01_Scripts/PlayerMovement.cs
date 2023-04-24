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
        Vector3 dir = new Vector3(leftHorizontalInput, 0, leftVerticalInput).normalized;
        Vector3 inputdir = transform.right * leftHorizontalInput + transform.forward * leftVerticalInput;
        float angle = Mathf.Atan2(dir.x, dir.z) * Mathf.Rad2Deg;

        Debug.Log("dir: " + dir + " inputdir: " + inputdir);

        if (inputdir.magnitude >= 0.5f)
        {
            rb.AddForce(inputdir * runSpeed * 10, ForceMode.Force);
            transform.rotation = Quaternion.Euler(0, angle, 0);
        }
        else if (inputdir.magnitude >= 0.01f && inputdir.magnitude < 0.5f)
        {
            rb.AddForce(inputdir * walkSpeed * 10, ForceMode.Force);
            transform.rotation = Quaternion.Euler(0, angle, 0);
        }
        
        animator.SetFloat("VelocityZ", inputdir.magnitude);
        animator.SetFloat("VelocityX", inputdir.magnitude);

        //looking
        //targetPosition = target.localPosition;
        //targetPosition = new Vector3(rightHorizontalInput, 0, rightVerticalInput);
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
