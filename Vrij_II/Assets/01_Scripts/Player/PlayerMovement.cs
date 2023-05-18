using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerMovement{

    [Header("Movement Settings")]
    [SerializeField]
    public float walkSpeed;
    [SerializeField]
    public float runSpeed;
    [SerializeField]
    public float aimDistance;

    [Header ("Components")]
    [SerializeField]
    public Rigidbody rb;  
    [SerializeField]
    public Animator animator;
    [SerializeField]
    public Transform target;

    [Header("Spear")]
    [SerializeField]
    private GameObject spearPrefab;
    [SerializeField]
    public GameObject spear;
    [SerializeField]
    private float throwStrenght;

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

    public void Aim(Transform _playerTransform, float _horizontalInput, float _verticalInput){

        Vector3 dir = Vector3.ClampMagnitude(new Vector3(_horizontalInput, 0, _verticalInput), 1.0f);
        if (_horizontalInput == 0 && _verticalInput == 0)
        {
            dir = new Vector3(0, 0, aimDistance);
        }

        Vector3 targetPosition = _playerTransform.position + (dir).normalized * aimDistance + new Vector3(0,1.5f,0);

        Debug.DrawLine(targetPosition, _playerTransform.position);

        target.position = targetPosition;

        Vector3 angleDir = (targetPosition - _playerTransform.position);
        //check angle from player
        float angle = Mathf.Atan2(angleDir.x, angleDir.z) * Mathf.Rad2Deg;
        //rotate player if angle outside of bounds
        if (angle < -90 || angle > 90)
        {
            _playerTransform.rotation = Quaternion.Euler(0, angle, 0);
        }
    }

    public bool Throw() {

        Debug.Log("hallo");
        Spear _spear = GameObject.Instantiate(spearPrefab, spear.transform.position, spear.transform.rotation, null).GetComponent<Spear>();
        _spear.Fire(throwStrenght);
        return false;
    }

}
