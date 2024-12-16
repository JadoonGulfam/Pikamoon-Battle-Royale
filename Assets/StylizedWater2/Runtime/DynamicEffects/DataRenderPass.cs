using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering;
using UnityEngine.Rendering;
#if URP
using UnityEngine.Rendering.Universal;
using UnityEngine.VFX;

namespace StylizedWater2.DynamicEffects
{
    public class DataRenderPass : ScriptableRenderPass
    {
        private const string profilerTag = "Water Dynamic Effects: Data";
        private static readonly ProfilingSampler profilerSampler = new ProfilingSampler(profilerTag);

        private const string LIGHTMODE_TAG = "WaterDynamicEffect";

        private float renderRange;
        private float m_renderRange;
        private float fadeRange;

        private int resolution;
        private bool highPrecision;
        private bool m_highPrecision;

        private RTHandle renderTarget;

        private const string WaterDynamicEffectsBufferName = "_WaterDynamicEffectsBuffer";
        private static readonly int _WaterDynamicEffectsBufferID = Shader.PropertyToID(WaterDynamicEffectsBufferName);
        private const string WaterDynamicEffectsCoordsName = "_WaterDynamicEffectsCoords";
        private static readonly int _WaterDynamicEffectsCoords = Shader.PropertyToID(WaterDynamicEffectsCoordsName);

        public static Vector4 rendererCoords;

        private static Matrix4x4 projection { set; get; }
        private static Matrix4x4 view { set; get; }

        private static Vector3 centerPosition;
        private static int CurrentResolution;
        private static float orthoSize;

        private static Color m_clearColor = new Color(0, 0, 0, 1);
        private static readonly Quaternion viewRotation = Quaternion.Euler(new Vector3(90f, 0f, 0f));
        private static readonly Vector3 viewScale = new Vector3(1, 1, -1);
        private static Rect viewportRect;

        //Render pass
        FilteringSettings m_FilteringSettings;
        RenderStateBlock m_RenderStateBlock;
        private readonly List<ShaderTagId> m_ShaderTagIdList = new List<ShaderTagId>()
        {
            new ShaderTagId(LIGHTMODE_TAG),
            //new ShaderTagId("UniversalForward")
        };
        //private static readonly Plane[] frustrumPlanes = new Plane[6];

        #if UNITY_2023_1_OR_NEWER
        private RendererListParams rendererListParams;
        private RendererList rendererList;
        #endif

        private bool enableVFX;
        public static List<VisualEffect> visualEffects = new List<VisualEffect>();

        public DataRenderPass()
        {
            m_FilteringSettings = new FilteringSettings(RenderQueueRange.all, -1);
            m_RenderStateBlock = new RenderStateBlock(RenderStateMask.Nothing);
        }

        public void Setup(ref WaterDynamicEffectsRenderFeature.Settings settings, int targetResolution)
        {
            this.renderRange = settings.renderRange;
            //Percentage to units
            this.fadeRange = (settings.renderRange * 0.5f) * (settings.fadePercentage / 100f);
            this.enableVFX = settings.enableVFXGraphHooks;
            this.resolution = targetResolution;
            this.highPrecision = settings.highPrecision;
        }

        //Important to snap the projection to the nearest texel. Otherwise pixel swimming is introduced when moving, due to bilinear filtering
        private static Vector3 StabilizeProjection(Vector3 pos, float texelSize)
        {
            float Snap(float coord, float cellSize) => Mathf.FloorToInt(coord / cellSize) * (cellSize) + (cellSize * 0.5f);

            return new Vector3(Snap(pos.x, texelSize), Snap(pos.y, texelSize), Snap(pos.z, texelSize));
        }
        
        private void SetupProjection(CommandBuffer cmd, Camera camera)
        {
            centerPosition = camera.transform.position + (camera.transform.forward * (orthoSize - fadeRange));

            centerPosition = StabilizeProjection(centerPosition, (orthoSize * 2f) / resolution);

            //var frustumHeight = 2.0f * renderRange * Mathf.Tan(camera.fieldOfView * 0.5f * Mathf.Deg2Rad); //Still clips, plus doesn't support orthographc
            var frustumHeight = orthoSize * 2f;
            centerPosition += (Vector3.up * frustumHeight * 0.5f);

            projection = Matrix4x4.Ortho(-orthoSize, orthoSize, -orthoSize, orthoSize, 0.03f, frustumHeight);

            view = Matrix4x4.TRS(centerPosition, viewRotation, viewScale).inverse;

            cmd.SetViewProjectionMatrices(view, projection);
            //RenderingUtils.SetViewAndProjectionMatrices(cmd, view, projection, true);

            viewportRect.width = resolution;
            viewportRect.height = resolution;
            cmd.SetViewport(viewportRect);

            //Don't actually need this, not performing any AABB operations
            //GeometryUtility.CalculateFrustumPlanes(projection * view, frustrumPlanes);

            //Position/scale of projection. Converted to a UV in the shader
            rendererCoords.x = centerPosition.x - orthoSize;
            rendererCoords.y = centerPosition.z - orthoSize;
            rendererCoords.z = orthoSize * 2f;
            rendererCoords.w = 1f; //Enable in shader

            cmd.SetGlobalVector(_WaterDynamicEffectsCoords, rendererCoords);
        }

        private void SetupVFX()
        {
            if (enableVFX)
            {
                foreach (VisualEffect vfx in visualEffects)
                {
                    if (!vfx) continue;

                    vfx.SetTexture(WaterDynamicEffectsBufferName, renderTarget);
                    vfx.SetVector4(WaterDynamicEffectsCoordsName, rendererCoords);
                }
            }
        }

        public override void Configure(CommandBuffer cmd, RenderTextureDescriptor cameraTextureDescriptor)
        {
            orthoSize = renderRange * 0.5f;

            if (resolution != CurrentResolution || highPrecision != m_highPrecision ||renderTarget == null)
            {
                RTHandles.Release(renderTarget);

                renderTarget = RTHandles.Alloc(resolution, resolution, 1, DepthBits.None,
                    highPrecision ? GraphicsFormat.R16G16B16A16_SFloat : GraphicsFormat.R8G8B8A8_SNorm,
                    filterMode: FilterMode.Bilinear,
                    wrapMode: TextureWrapMode.Clamp,
                    useMipMap: false, //TODO: Expose option
                    autoGenerateMips: true,
                    name: WaterDynamicEffectsBufferName);
            }
            CurrentResolution = resolution;
            m_highPrecision = highPrecision;

            cmd.SetGlobalTexture(_WaterDynamicEffectsBufferID, renderTarget);
            cmd.SetGlobalInt("_WaterDynamicEffectsHighPrecision", highPrecision ? 1 : 0);

            ConfigureTarget(renderTarget);
            ConfigureClear(ClearFlag.Color, m_clearColor);
        }

        public override void Execute(ScriptableRenderContext context, ref RenderingData renderingData)
        {
            CommandBuffer cmd = CommandBufferPool.Get();

            DrawingSettings drawingSettings = CreateDrawingSettings(m_ShaderTagIdList, ref renderingData, SortingCriteria.RenderQueue | SortingCriteria.SortingLayer | SortingCriteria.CommonTransparent);
            drawingSettings.perObjectData = PerObjectData.None;

            using (new ProfilingScope(cmd, profilerSampler))
            {
                ref CameraData cameraData = ref renderingData.cameraData;

                SetupProjection(cmd, cameraData.camera);

                //Execute current commands first
                context.ExecuteCommandBuffer(cmd);
                cmd.Clear();

                #if UNITY_2023_1_OR_NEWER
                rendererListParams.cullingResults = renderingData.cullResults;
                rendererListParams.drawSettings = drawingSettings;
                rendererListParams.filteringSettings = m_FilteringSettings;
                rendererList = context.CreateRendererList(ref rendererListParams);
                
                cmd.DrawRendererList(rendererList);
                #else
                context.DrawRenderers(renderingData.cullResults, ref drawingSettings, ref m_FilteringSettings, ref m_RenderStateBlock);
                #endif
                
                //context.ExecuteCommandBuffer(cmd);
                
                //Restore
                //cmd.Clear();
                cmd.SetViewProjectionMatrices(cameraData.camera.worldToCameraMatrix, cameraData.camera.projectionMatrix);
                //RenderingUtils.SetViewAndProjectionMatrices(cmd, cameraData.GetViewMatrix(), cameraData.GetGPUProjectionMatrix(), false);
            }

            context.ExecuteCommandBuffer(cmd);
            CommandBufferPool.Release(cmd);
            
            SetupVFX();
        }

        public override void FrameCleanup(CommandBuffer cmd)
        {
            rendererCoords.w = 0f; //Boolean in shader code, disables sampling
            cmd.SetGlobalVector(_WaterDynamicEffectsCoords, rendererCoords);
            cmd.DisableShaderKeyword(WaterDynamicEffectsRenderFeature.KEYWORD);
        }

        public void Dispose()
        {
            Shader.SetGlobalVector(_WaterDynamicEffectsCoords, Vector4.zero);
            RTHandles.Release(renderTarget);
        }
    }
}
#endif