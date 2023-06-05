
using System;
using System.IO;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SaveSystem {

    public delegate void ObjectSerializedCallback(string _result);
    public delegate void FileLoadedCallback(string _data);

    public class SaveManager : MonoBehaviour {

        public static Action SaveGameCall;
        public static Action ManualSaveGameCall;
        public static Action LoadGameCall;
        public static Action<ObjectSerializedCallback> SerializationCall;
        public static Action InitializeSaveSystem;
        public static Action<string, FileLoadedCallback> RegisterSaver;

        private ObjectSerializedCallback serializedCallback;

        private List<string> dataToWrite = new List<string>();
        private Dictionary<string, FileLoadedCallback> objectSavers = new Dictionary<string, FileLoadedCallback>();

        private int objectSaverAmount;

        private string path = "Assets/Resources/saveFile.txt";
        private string dataSeparator = "~~File Separator~~";

        public void Initialize() {
            serializedCallback = AddDataToWrite;
            SaveGameCall += TrySaveGame;
            LoadGameCall += TryLoadGame;
            RegisterSaver += RegisterObjectSaver;
            InitializeSaveSystem?.Invoke();
        }

        public void OnUpdate() {
            if(Input.GetKeyDown(KeyCode.F5)) {
                TrySaveGame();
            }
            if(Input.GetKeyDown(KeyCode.F6)) {
                TryLoadGame();
            }
        }

        private void TrySaveGame() {
            objectSaverAmount = SerializationCall.GetInvocationList().Length;
            SerializationCall?.Invoke(serializedCallback);
        }

        private void TryLoadGame() {

            string data = FileManager.ReadFromFile(path);

            string[] extracts = data.Split(dataSeparator);

            foreach(string s in extracts) {
                foreach(KeyValuePair<string, FileLoadedCallback> kvp in objectSavers) {
                    if(s.Contains(kvp.Key)) {
                        FileLoadedCallback callback = kvp.Value;
                        callback(s);
                    }
                }
            }

        }

        private void AddDataToWrite(string _result) {
            dataToWrite.Add(_result);
            objectSaverAmount--;
            if(objectSaverAmount <= 0) {
                FileManager.WriteToFile(String.Join(dataSeparator, dataToWrite), path);
            }
        }

        private void RegisterObjectSaver(string _id, FileLoadedCallback _callback) {
            objectSavers.Add(_id, _callback);
        }

    }

}