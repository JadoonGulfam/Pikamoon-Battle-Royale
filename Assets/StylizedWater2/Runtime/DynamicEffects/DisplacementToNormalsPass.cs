//Stylized Water 2
//Staggart Creations (http://staggart.xyz)
//Copyright protected under Unity Asset Store EULA

using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

namespace StylizedWater2.DynamicEffects
{
    internal class DisplacementToNormalsPass : ScriptableRenderPass
    {
        private const string profilerTag = "Water Dynamic Effects: Normals";
        private static readonly ProfilingSampler profilerSampler = new ProfilingSampler(profilerTag);

        private RTHandle renderTarget;
        private Material Material;

        private int resolution;
        private int m_resolution;
        private bool mipmaps;
        private bool m_mipmaps;

        private static readonly string WaterDynamicEffectsNormalsName = "_WaterDynamicEffectsNormals";
        private static readonly int _WaterDynamicEffectsNormalsID = Shader.PropertyToID(WaterDynamicEffectsNormalsName);
        private static readonly int _TexelSizeID = Shader.PropertyToID("_TexelSize");
        
        public void Setup(int targetResolution, bool mipmapsEnabled, Shader shader)
        {
            this.resolution = targetResolution;
            this.mipmaps = mipmapsEnabled;

            if (!Material && shader) Material = CoreUtils.CreateEngineMaterial(shader);
        }

        public override void Configure(CommandBuffer cmd, RenderTextureDescriptor cameraTextureDescriptor)
        {
            if (resolution != m_resolution || mipmaps != m_mipmaps || renderTarget == null)
            {
                RTHandles.Release(renderTarget);

                renderTarget = RTHandles.Alloc(resolution, resolution, 1, DepthBits.None,
                    UnityEngine.Experimental.Rendering.GraphicsFormat.R8G8_UNorm,
                    filterMode: FilterMode.Bilinear,
                    wrapMode: TextureWrapMode.Clamp,
                    useMipMap: mipmaps,
                    autoGenerateMips: true,
                    name: WaterDynamicEffectsNormalsName);
            }
            m_resolution = resolution;
            m_mipmaps = mipmaps;

            cmd.SetGlobalTexture(_WaterDynamicEffectsNormalsID, renderTarget);

            ConfigureTarget(renderTarget);
            ConfigureClear(ClearFlag.Color, Color.clear);
        }

        public override void Execute(ScriptableRenderContext context, ref RenderingData renderingData)
        {
            CommandBuffer cmd = CommandBufferPool.Get();

            using (new ProfilingScope(cmd, profilerSampler))
            {
                cmd.SetGlobalFloat(_TexelSizeID, 1f / resolution);
                cmd.Blit(null, renderTarget, Material);
            }

            context.ExecuteCommandBuffer(cmd);
            CommandBufferPool.Release(cmd);
        }

        public void Dispose()
        {
            RTHandles.Release(renderTarget);
            UnityEngine.Object.DestroyImmediate(Material);
        }
    }
}