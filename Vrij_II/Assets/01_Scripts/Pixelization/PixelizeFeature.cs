using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

namespace Pixelization {

    public class PixelizeFeature : ScriptableRendererFeature {

        [SerializeField]
        private bool renderInSceneView;

        [SerializeField]
        private CustomPassSettings settings;

        private PixelizePass customPass;

        public override void Create() {
            customPass = new PixelizePass(settings);
        }

        public override void AddRenderPasses(ScriptableRenderer _renderer, ref RenderingData _renderingData) {
            
            #if UNITY_EDITOR
            if(!renderInSceneView && _renderingData.cameraData.isSceneViewCamera) {
                return;
            }
            #endif

            _renderer.EnqueuePass(customPass);
        }
    }

    [System.Serializable]
    public class CustomPassSettings {
        public RenderPassEvent renderPassEvent = RenderPassEvent.BeforeRenderingPostProcessing;
        public int screenHeight;
    }

}