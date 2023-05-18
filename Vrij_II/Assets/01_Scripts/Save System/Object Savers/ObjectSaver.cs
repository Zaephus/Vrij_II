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

        protected virtual void Start() {

            uniqueId = GetComponent<UniqueId>();

            fileLoadedCallback = LoadObject;
            SaveManager.ObjectSaverRegistration?.Invoke(uniqueId.Id, fileLoadedCallback);
            SaveManager.SaveCallMade += SaveObject;

        }

        protected virtual void SaveObject(ObjectSavedCallback _callback) {}
        protected virtual void LoadObject(string _text) {}

        protected virtual T GetObjectData() { return default(T); }
        protected virtual void SetObjectData(T _data) {}
        
    }

}