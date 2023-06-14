
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WurmBody : MonoBehaviour, IDamageable {

    [SerializeField]
    private WurmController parent;

    public void Hit(float _dmg) {
        parent.health -= _dmg;
        if(parent.health <= 0.0f) {
            Die();
        }
    }

    public void Die() {
        Destroy(parent.gameObject);
    }

}