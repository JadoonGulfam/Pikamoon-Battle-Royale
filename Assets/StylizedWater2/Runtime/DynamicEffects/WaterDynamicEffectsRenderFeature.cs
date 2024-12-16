//Stylized Water 2
//Staggart Creations (http://staggart.xyz)
//Copyright protected under Unity Asset Store EULA

using System;
using UnityEngine;
using UnityEngine.Rendering;
#if URP
using UnityEngine.Rendering.Universal;

namespace StylizedWater2.DynamicEffects
{
    #if UNITY_2021_1_OR_NEWER
    [DisallowMultipleRendererFeature]
    #endif
    public class WaterDynamicEffectsRenderFeature : ScriptableRendererFeature
    {
        public const string Version = "1.1.0";
        public const string MinBaseVersion = "1.6.0";
        
        private const int MIN_RESOLUTION = 64;
        private static readonly bool NON_POWER_OF_TWO = true;
        
        public const string KEYWORD = "DYNAMIC_EFFECTS_ENABLED";
        
        private class SetupConstants : ScriptableRenderPass
        {
            private Vector4 parameters;
            private readonly int _WaterDynamicEffectsParams = Shader.PropertyToID("_WaterDynamicEffectsParams");

            public void Setup(ref Settings settings)
            {
                parameters.x = settings.enableNormals ? 1 : 0;
                parameters.y = settings.enableDisplacement ? 1 : 0;
                parameters.z = settings.renderRange;
                parameters.w = (settings.fadePercentage / 100f);
            }
            
            public override void Execute(ScriptableRenderContext context, ref RenderingData renderingData)
            {
                CommandBuffer cmd = CommandBufferPool.Get();

                cmd.EnableShaderKeyword(KEYWORD);
                cmd.SetGlobalVector(_WaterDynamicEffectsParams, parameters);

                context.ExecuteCommandBuffer(cmd);
                CommandBufferPool.Release(cmd);
            }

            public override void OnCameraCleanup(CommandBuffer cmd)
            {
                cmd.DisableShaderKeyword(KEYWORD);
            }
        }

        [Serializable]
        public class Settings
        {
            [Min(10f)]
            [Tooltip("Rendering is performed in an area of this size away from the camera. Longer ranges provide distant visibility, but thin out the rendering resolution.")]
            public float renderRange = 200f;
            [Range(0f, 50f)]
            [Tooltip("At the edge of the rendering area, effects fade out by this percentage (of the total area size). This is to avoid them abruptly cutting off")]
            public float fadePercentage = 10f;

            [Range(1, 32)]
            [Tooltip("Render range * Texels per unit = target resolution." +
                     "\n\nIf you were to imagine a grid, this value defines how many cells fit in one unit" +
                     "\n\n" +
                     "Lowering this value will lower the render resolution, at the cost of fine details (such as ripples)")]
            public int texelsPerUnit = 8;
            [Min(MIN_RESOLUTION)]
            [Tooltip("Given the other parameters, cap the maximum render resolution to this")]
            public int maxResolution = 4096;
            [Tooltip("When disabled, render textures use 8-bit precision instead of 16-bit. This halves the graphics memory usage, but introduces banding artifacts.")]
            public bool highPrecision = true;
            
            [Space]

            public bool enableDisplacement = true;
            [Tooltip("From the created displacement, create a new normal map. This is vital for lighting/shading." +
                     "\n\n" +
                     "If targeting a simple lighting setup, you can disable this")]
            public bool enableNormals = true;
            [Tooltip("Render normals at half resolution. This will mainly affect how effects influence the water's reflections")]
            public bool halfResolutionNormals;
            [Tooltip("Mipmaps for render targets will be enabled. At the cost of 33% additional memory a lower resolution texture will be sampled in the distance")]
            public bool normalMipmaps = true;

            [Space]
            
            [Tooltip("Do not execute this render feature for the scene-view camera. Helps to inspect the world while everything is rendering from the main camera's perspective")]
            public bool ignoreSceneView;
            [Tooltip("Pass on the render target and coordinates to any VFX Graph with the DynamicWaterVFX component attached. To be used for things like foam-based particles.")]
            public bool enableVFXGraphHooks = true;

            /// <summary>
            /// Retrieve the settings objects from the current renderer. This may be used to alter settings at runtime.
            /// </summary>
            /// <returns></returns>
            /// <exception cref="Exception">Render feature not present</exception>
            public static Settings GetCurrent()
            {
                var renderFeature = (WaterDynamicEffectsRenderFeature)PipelineUtilities.GetRenderFeature<WaterDynamicEffectsRenderFeature>();

                if (!renderFeature)
                {
                    throw new Exception("Unable to get the current settings instance, render feature not found on the current renderer");
                }

                return renderFeature.settings;
            }
        }
        
        public Settings settings = new Settings();

        private SetupConstants constantsPass;
        private DataRenderPass dataRenderPass;
        private DisplacementToNormalsPass normalsPass;
        
        [SerializeField] [HideInInspector]
        public Shader displacementNormalShader;

        private void OnValidate()
        {
            if(!displacementNormalShader) displacementNormalShader = Shader.Find("Hidden/StylizedWater2/DisplacementToNormals");
        }
        
        public static int CalculateResolution(float size, int texelsPerUnit, int max)
        {
            int res = Mathf.RoundToInt(size * texelsPerUnit);
            if(NON_POWER_OF_TWO == false) res = Mathf.NextPowerOfTwo(res);
            res = Mathf.Clamp(res, MIN_RESOLUTION, max);
            
            return res;
        }
        
        public override void Create()
        {
            constantsPass ??= new SetupConstants();
            dataRenderPass ??= new DataRenderPass();
            normalsPass ??= new DisplacementToNormalsPass();

            //Note: Actually prefer to render before transparents, but this creates a recursive RenderSingleCamera call
            constantsPass.renderPassEvent = RenderPassEvent.BeforeRendering;
            dataRenderPass.renderPassEvent = RenderPassEvent.BeforeRendering;
            normalsPass.renderPassEvent = RenderPassEvent.BeforeRendering;
        }

        public override void AddRenderPasses(ScriptableRenderer renderer, ref RenderingData renderingData)
        {
            var currentCam = renderingData.cameraData.camera;
            
            //Skip for any special use camera's (except scene view camera)
            if (currentCam.cameraType != CameraType.SceneView && (currentCam.cameraType == CameraType.Reflection || currentCam.cameraType == CameraType.Preview || currentCam.hideFlags != HideFlags.None)) return;

            //Skip overlay cameras
            if (renderingData.cameraData.renderType == CameraRenderType.Overlay) return;
            
            #if UNITY_EDITOR
            if (settings.ignoreSceneView && currentCam.cameraType == CameraType.SceneView) return;
            #endif

            int resolution = CalculateResolution(settings.renderRange, settings.texelsPerUnit, settings.maxResolution);

            constantsPass.Setup(ref settings);
            renderer.EnqueuePass(constantsPass);
            
            dataRenderPass.Setup(ref settings, resolution);
            renderer.EnqueuePass(dataRenderPass);

            if (settings.enableNormals)
            {
                #if UNITY_EDITOR || DEVELOPMENT_BUILD
                if (!displacementNormalShader)
                {
                    displacementNormalShader = Shader.Find("Hidden/StylizedWater2/DisplacementToNormals");
                    
                    if(!displacementNormalShader) Debug.LogError("[Stylized Water 2 Dynamic Effects: A shader is missing from the render feature, causing rendering to fail. Check the inspector", this);
                }
                #endif
                
                normalsPass.Setup(resolution / (settings.halfResolutionNormals ? 2 : 1),  settings.normalMipmaps, displacementNormalShader);
                renderer.EnqueuePass(normalsPass);
            }
        }

        private void OnDisable()
        {
            dataRenderPass?.Dispose();
            normalsPass?.Dispose();
        }
    }
}
#else
#error Dynamic Effects extension is imported without either the "Stylized Water 2" asset or the correct "Universal Render Pipeline" version installed. Will not be functional until these are both installed and set up.
#endif