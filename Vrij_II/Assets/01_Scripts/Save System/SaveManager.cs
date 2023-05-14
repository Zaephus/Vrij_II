
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SaveSystem {

    public delegate void ObjectSavedCallback(string _result);

    public class SaveManager : MonoBehaviour {

        // TODO: Fix Action name.
        public static Action<ObjectSavedCallback> SaveCallMade;

        private ObjectSavedCallback savedCallback;

        private void Start() {
            savedCallback = AddData;
        }

        private void Update() {
            if(Input.GetKeyDown(KeyCode.F5)) {
                SaveGame();
            }
            Debug.Log(SaveCallMade.GetInvocationList().Length);
        }

        private void SaveGame() {
            SaveCallMade?.Invoke(savedCallback);
        }

        private void AddData(string _result) {
            Debug.Log(_result);
        }

    }

}