using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spear : MonoBehaviour
{
    [SerializeField]
    private Rigidbody rb;

    private void OnEnable()
    {
        rb.AddForce(transform.forward, ForceMode.Impulse);         
    }
}
