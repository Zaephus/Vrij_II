using UnityEngine;
using UnityEngine.VFX;

public class VFXPositionBinder : MonoBehaviour {

    public VisualEffect vfxGraph; // Reference to the VFX graph component
    public Transform playerCharacter; // Reference to the PlayerCharacter object

    public string positionParameterName = "Position"; // Name of the Vector3 parameter in the VFX graph

    private void Start() {

        playerCharacter = FindObjectOfType<PlayerManager>().transform;

        if(vfxGraph == null) {
            Debug.LogError("VFX graph component not assigned!");
            return;
        }

        if(playerCharacter == null) {
            Debug.LogError("PlayerCharacter object not assigned!");
            return;
        }

        // Check if the position parameter exists in the VFX graph
        if(!vfxGraph.HasVector3(positionParameterName)) {
            Debug.LogError("Position parameter not found in the VFX graph!");
            return;
        }

    }

    private void Update() {
        // Get the position of the PlayerCharacter object
        Vector3 playerPosition = playerCharacter.position;

        // Set the position parameter in the VFX graph
        vfxGraph.SetVector3(positionParameterName, playerPosition);
    }

}
