using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CleverCrow.Fluid.UniqueIds;

namespace SaveSystem {

    public class ObjectSaver : MonoBehaviour {

        [SerializeField]
        private string stringToDisplay;

        private UniqueId uniqueId;

        private void Start() {
            SaveManager.SaveCallMade += SaveObject;
            uniqueId = GetComponent<UniqueId>();

        }

        private void SaveObject(ObjectSavedCallback _callback) {
            _callback(uniqueId.Id);
        }
        
    }

}