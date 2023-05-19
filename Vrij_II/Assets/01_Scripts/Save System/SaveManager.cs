using System;
using System.IO;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SaveSystem {

    public delegate void ObjectSavedCallback(string _result);
    public delegate void FileLoadedCallback(string _data);

    // TODO: Refactor SaveManager class.
    // Structure needs to be clearer, it is not always apparent which function belongs to saving, and which to loading.
    public class SaveManager : MonoBehaviour {

        public static Action SaveGameCall;
        public static Action<ObjectSavedCallback> SerializationCall;
        public static Action<string, FileLoadedCallback> ObjectSaverRegistration;

        private ObjectSavedCallback savedCallback;

        private List<string> dataToWrite = new List<string>();
        private Dictionary<string, FileLoadedCallback> objectSavers = new Dictionary<string, FileLoadedCallback>();

        private int objectSaverAmount;

        private string path = "Assets/Resources/saveFile.txt";
        private string dataSeparator = "~~File Separator~~";

        private void Start() {
            savedCallback = AddDataToWrite;
            SaveGameCall += TrySaveGame;
            ObjectSaverRegistration += RegisterObjectSaver;
        }

        private void Update() {
            if(Input.GetKeyDown(KeyCode.F5)) {
                SaveGameCall?.Invoke();
            }
            if(Input.GetKeyDown(KeyCode.F6)) {
                ReadFromFile();
            }
        }

        private void TrySaveGame() {
            objectSaverAmount = SerializationCall.GetInvocationList().Length;
            SerializationCall?.Invoke(savedCallback);
        }

        private void WriteToFile() {

            string data = String.Join(dataSeparator, dataToWrite);

            if(File.Exists(path)) {
                File.Delete(path);
            }

            using(FileStream fileStream = File.Create(path)) {
                using(StreamWriter writer = new StreamWriter(fileStream)) {
                    writer.Write(data);
                }
            }

        }

        private void ReadFromFile() {

            string data;

            using(FileStream fileStream = File.OpenRead(path)) {
                using(StreamReader reader = new StreamReader(fileStream)) {
                    data = reader.ReadToEnd();
                }
            }

            string[] extracts = data.Split(dataSeparator);

            foreach(string s in extracts) {
                foreach(KeyValuePair<string, FileLoadedCallback> kvp in objectSavers) {
                    // Debug.Log(kvp.Key);
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
                WriteToFile();
            }
        }

        private void RegisterObjectSaver(string _id, FileLoadedCallback _callback) {
            objectSavers.Add(_id, _callback);
        }

    }

}