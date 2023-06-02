using System.IO;
using System.Xml.Serialization;
using System.Runtime.Serialization;
using UnityEngine;
using CleverCrow.Fluid.UniqueIds;

namespace SaveSystem {

    public class ObjectSaver<T> : MonoBehaviour {

        protected XmlSerializer xmlSerializer = new XmlSerializer(typeof(T));

        protected UniqueId uniqueId;

        protected FileLoadedCallback fileLoadedCallback;

        private void Awake() {
            uniqueId = GetComponent<UniqueId>();
            SaveManager.InitializeSaveSystem += Initialize;
        }

        private void Initialize() {
            SaveManager.SerializationCall += SaveObject;
            fileLoadedCallback = LoadObject;
            SaveManager.RegisterSaver?.Invoke(uniqueId.Id, fileLoadedCallback);
        }

        protected virtual void SaveObject(ObjectSerializedCallback _callback) {}
        protected virtual void LoadObject(string _text) {}

        protected virtual T GetObjectData() { return default(T); }
        protected virtual void SetObjectData(T _data) {}
        
    }

}