using System.IO;
using System.Xml.Serialization;
using System.Runtime.Serialization;
using UnityEngine;

namespace SaveSystem {

    public class EnemySaver : ObjectSaver<EnemyData> {

        private EnemyController enemy;

        protected override void Start() {
            base.Start();
            enemy = GetComponent<EnemyController>();
        }

        protected override void SaveObject(ObjectSavedCallback _callback) {

            EnemyData data = GetObjectData();

            using(StringWriter stringWriter = new StringWriter()) {
                xmlSerializer.Serialize(stringWriter, data);
                _callback(stringWriter.ToString());
            }

        }

        protected override void LoadObject(string _text) {

            EnemyData data;

            using(StringReader stringReader = new StringReader(_text)) {
                data = xmlSerializer.Deserialize(stringReader) as EnemyData;
            }

            SetObjectData(data);

        }

        protected override EnemyData GetObjectData() {

            EnemyData data = new EnemyData();

            data.id = uniqueId.Id;

            data.x = enemy.transform.position.x;
            data.y = enemy.transform.position.y;
            data.z = enemy.transform.position.z;

            data.rotX = enemy.transform.rotation.x;
            data.rotY = enemy.transform.rotation.y;
            data.rotZ = enemy.transform.rotation.z;
            data.rotW = enemy.transform.rotation.w;

            data.hasDied = !enemy.isActiveAndEnabled;

            return data;

        }

        protected override void SetObjectData(EnemyData _data) {

            enemy.transform.position = new Vector3(_data.x, _data.y, _data.z);
            enemy.transform.rotation = new Quaternion(_data.rotX, _data.rotY, _data.rotZ, _data.rotW);

            enemy.gameObject.SetActive(!_data.hasDied);

        }
        
    }

}