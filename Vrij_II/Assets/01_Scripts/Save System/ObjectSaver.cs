using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SaveSystem {

    public class ObjectSaver : MonoBehaviour {

        [SerializeField]
        private string stringToDisplay;

        private void Start() {
            SaveManager.SaveCallMade += SaveObject;
        }

        private void SaveObject(ObjectSavedCallback _callback) {
            _callback(stringToDisplay);
        }
        
    }

}