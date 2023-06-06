
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WurmAttacker : MonoBehaviour {

    public void OnTriggerEnter(Collider _other) {
        if(_other.GetComponent<PlayerManager>() != null) {
            _other.GetComponent<PlayerManager>().Hit();
        }
    }
    
}