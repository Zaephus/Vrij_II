
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SaveSystem {

    public class SaveSlotManager : MonoBehaviour {

        public Transform slotContainer;
        public GameObject saveSlotPrefab;

        [SerializeField]
        private bool createAutoSaveSlots;

        private Dictionary<int, string> manualSaveFiles = new Dictionary<int, string>();

        private void Awake() {
            SaveManager.InitializeSaveSystem += Initialize;
        }

        private void Initialize() {

            string pathPrefix = "";
            #if UNITY_EDITOR
            pathPrefix = Application.dataPath;
            #else
            pathPrefix = Application.persistentDataPath;
            #endif

            int i = 0;
            while(true) {
                string manualPath = pathPrefix + $"m_SaveSlot{i}";
                string autoPath = pathPrefix + $"a_SaveSlot{i}";
                if(File.Exists(manualPath) || (createAutoSaveSlots && File.Exists(autoPath))) {
                    if(File.Exists(manualPath)) {
                        CreateSaveSlot(i, FileManager.ReadFromFile(manualPath));
                    }
                    if(File.Exists(autoPath)) {
                        CreateSaveSlot(i, FileManager.ReadFromFile(autoPath));
                    }
                }
                else {
                    break;
                }
                i++;
            }

        }

        private void CreateSaveSlot(int _index, string _data) {

            SaveSlot saveSlot = Instantiate(saveSlotPrefab, slotContainer.position, Quaternion.identity, slotContainer).GetComponent<SaveSlot>();



        }

    }

}