
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SaveSystem {

    public class SaveCheckpoint : MonoBehaviour {

        public void OnTriggerEnter(Collider _other) {
            if(_other.GetComponent<PlayerSaver>() != null) {
                SaveManager.SaveGameCall?.Invoke();
            }
        }

    }

}