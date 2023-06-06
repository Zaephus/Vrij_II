
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class FootstepColor : MonoBehaviour {

    [SerializeField]
    private Terrain terrain;
    [SerializeField]
    private TextureColor[] textureColors;
    [SerializeField]
    private VisualEffect footsteps;
    [SerializeField]
    private bool blendTerrain;

    private void Awake() {
        if (terrain == null) {
            terrain = FindObjectOfType<Terrain>();
        }
    }

    private void Update() {

        Vector3 terrainPosition = transform.position - terrain.transform.position;
        Vector3 splatMapPosition = new Vector3(terrainPosition.x / terrain.terrainData.size.x, 0, terrainPosition.z / terrain.terrainData.size.z);

        int x = Mathf.FloorToInt(splatMapPosition.x * terrain.terrainData.alphamapWidth);
        int z = Mathf.FloorToInt(splatMapPosition.z * terrain.terrainData.alphamapHeight);

        float[,,] alphaMap = terrain.terrainData.GetAlphamaps(x, z, 1, 1);
        if (!blendTerrain) {
            int primaryIndex = 0;
            for (int i = 1; i < alphaMap.Length; i++) {
                if (alphaMap[0, 0, i] > alphaMap[0, 0, primaryIndex]) {
                   primaryIndex = i;
                }
            }

            foreach (TextureColor textureColor in textureColors) {
                if (textureColor.albedo == terrain.terrainData.terrainLayers[primaryIndex].diffuseTexture) {
                    Vector4 colorVector = new Vector4(textureColor.color.r, textureColor.color.g, textureColor.color.b, 0.8f);
                    footsteps.SetVector4("FootStepColor", colorVector);
                }
            }
        }
    }

    [System.Serializable]
    public class TextureColor {

        public Texture albedo;
        public Color color;
    }

}
