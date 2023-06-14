
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class WurmBody : MonoBehaviour, IDamageable {

    [SerializeField]
    private WurmController parent;

    [SerializeField]
    private Animator animator;

    [SerializeField]
    private VisualEffect deathEffect;

    private void Start() {
        deathEffect.Reinit();
    }

    public void Hit(float _dmg) {
        parent.health -= _dmg;
        if(parent.health <= 0.0f) {
            StartCoroutine(Die());
        }
    }

    public IEnumerator Die() {
        deathEffect.Play();
        animator.StopPlayback();
        yield return new WaitForSeconds(5.0f);
        
        Destroy(parent.gameObject);
    }

}