using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private float horizontalInput;
    private float verticalInput;

    [Header("Movement Settings")]
    [SerializeField]
    private float walkSpeed;
    [SerializeField]
    private float runSpeed;

    [Header ("Components")]
    [SerializeField]
    private Rigidbody rb;  
    [SerializeField]
    private Animator animator;
    [SerializeField]
    private Transform target;

    private void Update()
    {
        GetInput();
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void GetInput()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");

        Debug.Log("Horizontal: " + horizontalInput + " Vertical: " + verticalInput);
    }

    private void Move()
    {
        Vector3 dir = transform.forward * verticalInput + transform.right * horizontalInput;

        rb.AddForce(dir * walkSpeed * 10, ForceMode.Force);
     
        animator.SetFloat("VelocityZ", dir.normalized.z);
        animator.SetFloat("VelocityX", dir.normalized.x);
    }
}
