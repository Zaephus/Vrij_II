
using System.Collections;
using UnityEngine;
using UnityEngine.Rendering;

public class PlayerManager : MonoBehaviour, IDamageable {

    [SerializeField]
    private PlayerInputManager playerInput;
    [SerializeField]
    private PlayerMovement playerMovement;
    [SerializeField]
    private GameObject aimingTarget;
    [SerializeField]
    private bool hasSpear;

    [SerializeField]
    private Volume globalVolume;

    [SerializeField]
    private float vignetteFadeInSpeed;
    [SerializeField]
    private float vignetteFadeOutSpeed;

    private int grassAmount;

    public static System.Action<Spear> SpearInRangeCall;
    private GameObject currentOverworldSpear;

    private void Start() {
        SpearInRangeCall += PickUpSpear;
    }

    private void Update() {
        aimingTarget.SetActive(playerInput.isAiming);
        playerInput.hasSpear = hasSpear;
        playerMovement.spear.SetActive(hasSpear);
    }

    private void FixedUpdate() {
        playerMovement.Move(transform, playerInput.leftHorizontalInput, playerInput.leftVerticalInput, hasSpear);
        if (playerInput.isAiming && hasSpear) {
            playerMovement.Aim(transform, playerInput.rightHorizontalInput, playerInput.rightVerticalInput);
        }
    }

    public void Throw() {
        if (hasSpear) {
            hasSpear = playerMovement.Throw(currentOverworldSpear);
        }
    }

    public void Hit() {
        GameManager.PlayerDied?.Invoke();
    }

    public void PickUpSpear(Spear _spearToPickup) {

        if (playerInput.isInteracting && !hasSpear) {
            _spearToPickup.gameObject.SetActive(false);
            hasSpear = true;
            currentOverworldSpear = _spearToPickup.gameObject;
        }
    }

    private void OnTriggerEnter(Collider _other) {
        if(_other.tag == "TallGrass") {
            grassAmount++;
            StartCoroutine(FadeInVignette());
        }
    }

    private void OnTriggerExit(Collider _other) {
        if(_other.tag == "TallGrass") {
            grassAmount--;
            if(grassAmount <= 0) {
                StartCoroutine(FadeOutVignette());
            }
        }
    }

    private IEnumerator FadeInVignette() {
        while(globalVolume.weight < 1) {
            globalVolume.weight += vignetteFadeInSpeed * Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
    }

    private IEnumerator FadeOutVignette() {
        while(globalVolume.weight > 0) {
            globalVolume.weight -= vignetteFadeInSpeed * Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
    }

}