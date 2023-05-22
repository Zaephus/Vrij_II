using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class Spear : MonoBehaviour
{
    [SerializeField]
    private Rigidbody rb;
    [SerializeField]
    private Transform boneTransform;
    [SerializeField]
    private GameObject pickupCanvas;
    [SerializeField]
    private VisualEffect spearTrails;

    private bool playerInRange;

    private void Update(){

        pickupCanvas.SetActive(playerInRange);
        pickupCanvas.transform.position = transform.position + new Vector3(0,1,0);


        if (rb.velocity == Vector3.zero)
        {
            spearTrails.Stop();
        }

        if (playerInRange)
        {
            PlayerManager.SpearInRangeCall?.Invoke(this);
        }
    }


    public void Fire(float _throwStrenght)
    {
        rb.AddForce(-boneTransform.up * _throwStrenght, ForceMode.Impulse);
    }

    private void OnTriggerStay(Collider other)
    {
        playerInRange = other.gameObject.GetComponent<PlayerManager>();
        
    }
}
