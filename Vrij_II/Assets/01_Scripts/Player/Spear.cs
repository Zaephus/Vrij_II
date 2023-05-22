using UnityEngine;
using UnityEngine.VFX;

public class Spear : MonoBehaviour {
    [SerializeField]
    private Rigidbody rb;
    [SerializeField]
    private Transform boneTransform;
    [SerializeField]
    private GameObject pickupCanvas;
    [SerializeField]
    private VisualEffect spearTrails;

    public bool playerInRange;

    private void Update() {

        pickupCanvas.SetActive(playerInRange);
        pickupCanvas.transform.position = transform.position + new Vector3(0, 1, 0);

        if (rb.velocity == Vector3.zero) {
            spearTrails.Stop();
        }
        else {
            playerInRange = false;
        }

    }

    public void Fire(float _throwStrenght) {
        rb.AddForce(-boneTransform.up * _throwStrenght, ForceMode.Impulse);
    }

    private void OnTriggerStay(Collider _other) {
        playerInRange = _other.gameObject.GetComponent<PlayerManager>();

        if (_other.gameObject.GetComponent<PlayerManager>()) {
            PlayerManager.SpearInRangeCall?.Invoke(this);
        }
    }
}
