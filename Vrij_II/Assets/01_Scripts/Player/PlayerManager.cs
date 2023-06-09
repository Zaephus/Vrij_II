
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Rendering;
using Cinemachine;

public class PlayerManager : MonoBehaviour {

    public static Action<float> ZoomCamera;

    public static Action StabAction;
    public static Action ThrowAction;

    [SerializeField]
    private PlayerInputManager playerInput;
    [SerializeField]
    private PlayerMovement playerMovement;
    [SerializeField]
    private GameObject aimingTarget;
    [SerializeField]
    private bool hasSpear;

    [SerializeField]
    private CinemachineVirtualCamera playerCamera;
    [SerializeField]
    private float zoomDuration;

    [SerializeField]
    private Volume globalVolume;

    [SerializeField]
    private float vignetteFadeInSpeed;
    [SerializeField]
    private float vignetteFadeOutSpeed;

    private int grassAmount;
    private bool isInGrass;

    public static System.Action<Spear> SpearInRangeCall;
    private GameObject currentOverworldSpear;

    private void Start() {
        SpearInRangeCall += PickUpSpear;
        ZoomCamera += StartCamZoom;
        FindAnyObjectByType<GameManager>().respawnPoint = transform.position;
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

    private void OnDestroy() {
        SpearInRangeCall -= PickUpSpear;
        ZoomCamera -= StartCamZoom;
    }

    public void Throw() {
        if (hasSpear) {
            hasSpear = playerMovement.Throw(currentOverworldSpear, GetComponent<AnimatorLayerWeight>().spearAimTransform);
        }
    }

    public void Stab() {
        StartCoroutine(playerMovement.spear.GetComponent<SpearHeld>().Stab());
    }

    public void Hit() {
        GameManager.PlayerDied?.Invoke();
        gameObject.SetActive(false);
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
            isInGrass = true;
            StartCoroutine(FadeInVignette());
        }
    }

    private void OnTriggerExit(Collider _other) {
        if(_other.tag == "TallGrass") {
            grassAmount--;
            if(grassAmount <= 0) {
                isInGrass = false;
                StartCoroutine(FadeOutVignette());
            }
        }
    }

    private IEnumerator FadeInVignette() {
        while(globalVolume.weight < 1 && isInGrass) {
            globalVolume.weight += vignetteFadeInSpeed * Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        if(!isInGrass) {
            StartCoroutine(FadeOutVignette());
        }
    }

    private IEnumerator FadeOutVignette() {
        yield return new WaitForEndOfFrame();
        while(globalVolume.weight > 0) {
            globalVolume.weight -= vignetteFadeInSpeed * Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
    }

    private void StartCamZoom(float _targetZoom) {
        StartCoroutine(CameraZoom(_targetZoom));
    }
    private IEnumerator CameraZoom(float _targetZoom) {

        float startTime = Time.time;
        float startZoom = playerCamera.m_Lens.OrthographicSize;

        while(_targetZoom - playerCamera.m_Lens.OrthographicSize > Mathf.Epsilon || _targetZoom - playerCamera.m_Lens.OrthographicSize < -Mathf.Epsilon) {

            float t = (Time.time - startTime) / zoomDuration;
            playerCamera.m_Lens.OrthographicSize = Mathf.SmoothStep(startZoom, _targetZoom, t);

            yield return new WaitForEndOfFrame();

        }

    }

}