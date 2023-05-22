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

    public static System.Action<Spear> SpearInRangeCall;

    private void Start() {
        SpearInRangeCall += PickUpSpear;
    }

    private void Update() {
        aimingTarget.SetActive(playerInput.isAiming);
        playerInput.hasSpear = hasSpear;
        playerMovement.spear.SetActive(hasSpear);
    }

    private void FixedUpdate() {
        playerMovement.Move(transform, playerInput.leftHorizontalInput, playerInput.leftVerticalInput);
        if (playerInput.isAiming && hasSpear) {
            playerMovement.Aim(transform, playerInput.rightHorizontalInput, playerInput.rightVerticalInput);
        }
    }

    public void Throw() {
        if (hasSpear) {
            hasSpear = playerMovement.Throw();
        }
    }

    public void Hit() {
        GameManager.PlayerDied?.Invoke();
    }

    public void PickUpSpear(Spear _spearToPickup) {

            Debug.Log("PAK OP LUL");
        if (playerInput.isInteracting) {
            Debug.Log("OKEE LUL");
        }
    }

}
