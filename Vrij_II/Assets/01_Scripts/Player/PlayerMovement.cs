
using UnityEngine;
using UnityEngine.VFX;

[System.Serializable]
public class PlayerMovement {

    [Header("Movement Settings")]
    [SerializeField]
    public float walkSpeed;
    [SerializeField]
    public float runSpeed;
    [SerializeField]
    public float footstepModifier;
    [SerializeField]
    public float aimDistance;
    [SerializeField]
    public float holdingSpearClamp;

    [Header("Components")]
    [SerializeField]
    public Rigidbody rb;
    [SerializeField]
    public Animator animator;
    [SerializeField]
    public Transform target;
    [SerializeField]
    public VisualEffect footsteps;

    [Header("Spear")]
    [SerializeField]
    private GameObject spearPrefab;
    [SerializeField]
    public GameObject spear;
    [SerializeField]
    private float throwStrength;

    public void Move(Transform _playerTransform, float _horizontalInput, float _verticalInput, bool _hasSpear, bool _isAiming) {

        float clampValue = 1.0f;

        if (_hasSpear) {
            clampValue = holdingSpearClamp;
        }

        Vector3 dir = Vector3.ClampMagnitude(new Vector3(_horizontalInput, 0, _verticalInput), clampValue);
        float angle = Mathf.Atan2(dir.x, dir.z) * Mathf.Rad2Deg;

        if (!_isAiming) {

            if (dir.magnitude >= 0.5f) {
                rb.AddForce(_playerTransform.forward * runSpeed * 10, ForceMode.Force);
                _playerTransform.rotation = Quaternion.Euler(0, angle, 0);
            }
            else if (dir.magnitude >= 0.01f && dir.magnitude < 0.5f) {
                rb.AddForce(_playerTransform.forward * walkSpeed * 10, ForceMode.Force);
                _playerTransform.rotation = Quaternion.Euler(0, angle, 0);
            }
            animator.SetFloat("RegularMovement", dir.magnitude);
            footsteps.SetFloat("FootStepRate", dir.magnitude * footstepModifier);
        }
        else {
            rb.velocity = Vector3.zero;
            animator.SetFloat("RegularMovement", 0);
            footsteps.SetFloat("FootStepRate", 0 * footstepModifier);
        }

        //animator.SetFloat("VelocityZ", dir.z * 2);
        //animator.SetFloat("VelocityX", dir.x *2);
    }

    // TODO: Add a slope limit.

    public void Aim(Transform _playerTransform, float _horizontalInput, float _verticalInput) {

        Vector3 dir = Vector3.ClampMagnitude(new Vector3(_horizontalInput, 0, _verticalInput), 1.0f);
        if (_horizontalInput == 0 && _verticalInput == 0) {
            dir = new Vector3(0, 0, aimDistance);
        }

        Vector3 targetPosition = target.position;

        if (_horizontalInput >= 0.01f || _verticalInput >= 0.01f) {
            targetPosition = _playerTransform.position + (dir).normalized * aimDistance + new Vector3(0, 1.5f, 0);
            target.position = targetPosition;
        }

        Debug.DrawLine(targetPosition, _playerTransform.position);
        //target.position = targetPosition;


        Vector3 angleDir = (targetPosition - _playerTransform.position);
        //check angle from player
        float angle = Mathf.Atan2(angleDir.x, angleDir.z) * Mathf.Rad2Deg;
        //rotate player if angle outside of bounds

        _playerTransform.rotation = Quaternion.Euler(0, angle, 0);
    }

    public bool Throw(GameObject _spearToThrow) {

        _spearToThrow.transform.position = spear.transform.position;
        _spearToThrow.transform.rotation = spear.transform.rotation;
        Spear _spear = _spearToThrow.GetComponent<Spear>();
        _spearToThrow.SetActive(true);
        _spear.Fire(throwStrength);
        return false;
    }

}
