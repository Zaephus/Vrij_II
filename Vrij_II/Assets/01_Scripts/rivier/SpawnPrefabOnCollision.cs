
using UnityEngine;

public class SpawnPrefabOnCollision : MonoBehaviour {
    
    public GameObject prefabToSpawn;
    public float destroyDelay = 2f;

    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Player")) {
            // Spawn the prefab at the player's position
            GameObject splash = Instantiate(prefabToSpawn, other.transform.position, Quaternion.identity);

            // Destroy the spawned prefab after the specified delay
            Destroy(splash, destroyDelay);
        }
    }

}
