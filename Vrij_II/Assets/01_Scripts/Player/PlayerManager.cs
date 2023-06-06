
using System.Collections;
using UnityEngine;
using UnityEngine.Rendering;

public class PlayerManager : MonoBehaviour{

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
        playerMovement.Move(transform, playerInput.leftHorizontalInput, playerInput.leftVerticalInput, hasSpear, playerInput.isAiming);
        if (playerInput.isAiming && hasSpear) {
            playerMovement.Aim(transform, playerInput.rightHorizontalInput, playerInput.rightVerticalInput);
        }
    }

    public void Throw() {
        if (hasSpear) {
            hasSpear = playerMovement.Throw(currentOverworldSpear, GetComponent<AnimatorLayerWeight>().spearAimTransform);
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
            playerMovement.spear.GetComponent<SpearHeld>().SwitchSpear(_spearToPickup.type);
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
        StopCoroutine(FadeInVignette());
        yield return new WaitForEndOfFrame();
        while(globalVolume.weight > 0) {
            globalVolume.weight -= vignetteFadeInSpeed * Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
    }

}