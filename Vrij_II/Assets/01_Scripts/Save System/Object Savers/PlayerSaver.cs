using System.IO;
using System.Xml.Serialization;
using System.Runtime.Serialization;
using UnityEngine;

namespace SaveSystem {

    public class PlayerSaver : ObjectSaver<PlayerData> {

        private PlayerTest playerTest;

        protected override void Start() {
            base.Start();
            playerTest = GetComponent<PlayerTest>();
        }

        protected override void SaveObject(ObjectSavedCallback _callback) {

            PlayerData data = GetObjectData();

            using(StringWriter stringWriter = new StringWriter()) {
                xmlSerializer.Serialize(stringWriter, data);
                _callback(stringWriter.ToString());
            }
            
        }

        protected override void LoadObject(string _text) {

            PlayerData data;

            using(StringReader stringReader = new StringReader(_text)) {
                data = xmlSerializer.Deserialize(stringReader) as PlayerData;
            }

            SetObjectData(data);

        }

        protected override PlayerData GetObjectData() {

            PlayerData data = new PlayerData();

            data.id = uniqueId.Id;

            data.x = playerTest.transform.position.x;
            data.y = playerTest.transform.position.y;
            data.z = playerTest.transform.position.z;

            data.rotX = playerTest.transform.rotation.x;
            data.rotY = playerTest.transform.rotation.y;
            data.rotZ = playerTest.transform.rotation.z;
            data.rotW = playerTest.transform.rotation.w;

            data.hasSpear = playerTest.hasSpear;

            return data;

        }

        protected override void SetObjectData(PlayerData _data) {

            playerTest.transform.position = new Vector3(_data.x, _data.y, _data.z);
            playerTest.transform.rotation = new Quaternion(_data.rotX, _data.rotY, _data.rotZ, _data.rotW);

            playerTest.hasSpear = _data.hasSpear;

        }

    }

}