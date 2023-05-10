using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerMovement{

    private Vector3 targetPosition;

    [Header("Movement Settings")]
    [SerializeField]
    public float walkSpeed;
    [SerializeField]
    public float runSpeed;
    [SerializeField]
    public float aimSpeed;

    [Header ("Components")]
    [SerializeField]
    public Rigidbody rb;  
    [SerializeField]
    public Animator animator;
    [SerializeField]
    public Transform target;

    public void Move(Transform _playerTransform, float _horizontalInput, float _verticalInput){
        Vector3 dir = Vector3.ClampMagnitude(new Vector3(_horizontalInput, 0, _verticalInput), 1.0f);
        float angle = Mathf.Atan2(dir.x, dir.z) * Mathf.Rad2Deg;

        if (dir.magnitude >= 0.5f) {
            rb.AddForce(_playerTransform.forward * runSpeed * 10, ForceMode.Force);
            _playerTransform.rotation = Quaternion.Euler(0, angle, 0);
        }
        else if (dir.magnitude >= 0.01f && dir.magnitude < 0.5f) {
            rb.AddForce(_playerTransform.forward * walkSpeed * 10, ForceMode.Force);
            _playerTransform.rotation = Quaternion.Euler(0, angle, 0);
        }
        
        animator.SetFloat("VelocityZ", dir.magnitude);
        animator.SetFloat("VelocityX", dir.magnitude);
    }

    // TODO: Add a slope limit.

}
