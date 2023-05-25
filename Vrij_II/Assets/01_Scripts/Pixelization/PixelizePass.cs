using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

namespace Pixelization {

    public class PixelizePass : ScriptableRenderPass {

        private CustomPassSettings settings;

        private RenderTargetIdentifier colorBuffer;
        private RenderTargetIdentifier pixelBuffer;

        private int pixelBufferID = Shader.PropertyToID("_PixelBuffer");
    
        private Material material;

        private int pixelScreenHeight;
        private int pixelScreenWidth;

        public PixelizePass(CustomPassSettings _settings) {

            settings = _settings;
            renderPassEvent = _settings.renderPassEvent;

            if(material == null) {
                material = CoreUtils.CreateEngineMaterial(settings.shader);
            }

        }

        public override void OnCameraSetup(CommandBuffer _cmd, ref RenderingData _renderingData) {

            colorBuffer = _renderingData.cameraData.renderer.cameraColorTarget;
            RenderTextureDescriptor descriptor = _renderingData.cameraData.cameraTargetDescriptor;

            pixelScreenHeight = settings.screenHeight;
            pixelScreenWidth = (int)(pixelScreenHeight * _renderingData.cameraData.camera.aspect + 0.5f);

            material.SetVector("_BlockCount", new Vector2(pixelScreenWidth, pixelScreenHeight));
            material.SetVector("_BlockSize", new Vector2(1.0f / pixelScreenWidth, 1.0f / pixelScreenHeight));
            material.SetVector("_HalfBlockSize", new Vector2(0.5f / pixelScreenWidth, 0.5f / pixelScreenHeight));

            descriptor.height = pixelScreenHeight;
            descriptor.width = pixelScreenWidth;

            _cmd.GetTemporaryRT(pixelBufferID, descriptor, FilterMode.Point);
            pixelBuffer = new RenderTargetIdentifier(pixelBufferID);

        }

        public override void Execute(ScriptableRenderContext _context, ref RenderingData _renderingData) {

            CommandBuffer cmd = CommandBufferPool.Get();

            using(new ProfilingScope(cmd, new ProfilingSampler("Pixelize Pass"))) {
                Blit(cmd, colorBuffer, pixelBuffer, material);
                Blit(cmd, pixelBuffer, colorBuffer);
            }

            _context.ExecuteCommandBuffer(cmd);
            CommandBufferPool.Release(cmd);

        }

        public override void OnCameraCleanup(CommandBuffer _cmd) {

            if(_cmd == null) {
                throw new System.ArgumentNullException("Command Buffer is null.");
            }

            _cmd.ReleaseTemporaryRT(pixelBufferID);
            
        }
        
    }

}