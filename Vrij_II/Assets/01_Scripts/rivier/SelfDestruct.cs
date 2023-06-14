
using UnityEngine;

public class SelfDestruct : MonoBehaviour {
    public float destroyDelay = 2f;

    private void Start() {
        Destroy(gameObject, destroyDelay);
    }

}
