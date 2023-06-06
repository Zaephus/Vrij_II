
using UnityEngine;
using UnityEngine.VFX;


public enum SpearType {One, Two};

public class Spear : MonoBehaviour {

    public SpearType type;
    [SerializeField]
    private Rigidbody rb;
    [SerializeField]
    private Transform boneTransform;
    [SerializeField]
    private GameObject pickupCanvas;
    [SerializeField]
    private VisualEffect spearTrails;
    [SerializeField]
    private Collider triggerCollider;

    public bool playerInRange;

    private void Update() {

        pickupCanvas.SetActive(playerInRange);
        pickupCanvas.transform.position = transform.position + new Vector3(0, 1, 0);

        if (rb.velocity == Vector3.zero) {
            spearTrails.Stop();
            triggerCollider.enabled = false;
        }
        else {
            playerInRange = false;
            triggerCollider.enabled = true;
        }

    }

    public void Fire(float _throwStrength) {
        rb.AddForce(transform.forward * _throwStrength, ForceMode.Impulse);
    }

    private void OnTriggerStay(Collider _other) {
        playerInRange = _other.gameObject.GetComponent<PlayerManager>();

        if (_other.gameObject.GetComponent<PlayerManager>()) {
            PlayerManager.SpearInRangeCall?.Invoke(this);
        }
    }

    private void OnTriggerEnter(Collider other) {
        IDamageable target;
        if (other.TryGetComponent<IDamageable>(out target)) {
            target.Hit();
        }
    }
}
