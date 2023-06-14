
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleCollider : MonoBehaviour {

    private void OnParticleCollision(GameObject _other) {
        if(_other.GetComponent<PlayerManager>() != null) {
            PlayerManager player = _other.GetComponent<PlayerManager>();
            player.Hit();
        }
    }
    
}