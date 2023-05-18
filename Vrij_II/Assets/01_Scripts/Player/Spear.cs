using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spear : MonoBehaviour
{
    [SerializeField]
    private Rigidbody rb;
    [SerializeField]
    private Transform boneTransform; 

    public void Fire(float _throwStrenght)
    {
        rb.AddForce(-boneTransform.up * _throwStrenght, ForceMode.Impulse);         
    }
}
