using UnityEngine;
using UnityEngine.Experimental.Rendering;
using UnityEngine.Rendering;
using UnityEngine.Rendering.HighDefinition;
using UnityEngine.XR;

namespace KWS
{
    internal static partial class KWS_CoreUtils
    {
        static bool CanRenderWaterForCurrentCamera_PlatformSpecific(Camera cam)
        {
            return true;
        }

        public static Vector2Int GetCameraRTHandleViewPortSize(Camera cam)
        {
#if ENABLE_VR
            if (XRSettings.enabled)
            {
                return new Vector2Int(XRSettings.eyeTextureWidth, XRSettings.eyeTextureHeight);
            }
            else
#endif
            {
                var viewPortSize = RTHandles.rtHandleProperties.currentViewportSize;
                if (viewPortSize.x == 0 || viewPortSize.y == 0) return new Vector2Int(cam.pixelWidth, cam.pixelHeight);
                else return viewPortSize;
            }

        }

        public static bool CanRenderSinglePassStereo(Camera cam)
        {
#if ENABLE_VR
            return XRSettings.enabled &&
                   (XRSettings.stereoRenderingMode == XRSettings.StereoRenderingMode.SinglePassInstanced && cam.cameraType != CameraType.SceneView);
#else
            return false;
#endif
        }

        public static bool IsSinglePassStereoActive()
        {
#if ENABLE_VR
            return XRSettings.enabled && XRSettings.stereoRenderingMode == XRSettings.StereoRenderingMode.SinglePassInstanced;
#else
            return false;
#endif
        }

        public static void UniversalCameraRendering(WaterSystem waterInstance, Camera camera)
        {
            camera.Render();
        }

        public static void SetPlatformSpecificPlanarReflectionParams(Camera reflCamera)
        {
            var camData = reflCamera.GetComponent<HDAdditionalCameraData>();
            if (camData == null) camData = reflCamera.gameObject.AddComponent<HDAdditionalCameraData>();

            camData.invertFaceCulling = true;
        }

        public static void UpdatePlatformSpecificPlanarReflectionParams(Camera reflCamera, WaterSystem waterInstance)
        {
            //if (waterInstance.Settings.UseScreenSpaceReflection && waterInstance.Settings.UseAnisotropicReflections)
            //{
            //    reflCamera.clearFlags = CameraClearFlags.Color;
            //    reflCamera.backgroundColor = Color.black;
            //}
            //else
            //{
            //    reflCamera.clearFlags = CameraClearFlags.Skybox;
            //}
        }

        public static void SetPlatformSpecificCubemapReflectionParams(Camera reflCamera)
        {
            var cameraData = reflCamera.GetComponent<HDAdditionalCameraData>();
            if (cameraData == null) cameraData = reflCamera.gameObject.AddComponent<HDAdditionalCameraData>();

            cameraData.DisableAllCameraFrameSettings();
            cameraData.customRenderingSettings = true;
            reflCamera.SetCameraFrameSetting(FrameSettingsField.VolumetricClouds, true);
            reflCamera.SetCameraFrameSetting(FrameSettingsField.OpaqueObjects, true);
        }

        public static void SetComputeShadersDefaultPlatformSpecificValues(this CommandBuffer cmd, ComputeShader cs, int kernel)
        {
            cmd.SetComputeTextureParam(cs, kernel, "_AirSingleScatteringTexture", KWS_CoreUtils.DefaultBlack3DTexture);
            cmd.SetComputeTextureParam(cs, kernel, "_AerosolSingleScatteringTexture", KWS_CoreUtils.DefaultBlack3DTexture);
            cmd.SetComputeTextureParam(cs, kernel, "_MultipleScatteringTexture", KWS_CoreUtils.DefaultBlack3DTexture);

        }

        public static void RenderDepth(Camera depthCamera, RenderTexture depthRT)
        {
            var data               = depthCamera.GetComponent<HDAdditionalCameraData>();
            if (data == null) data = depthCamera.gameObject.AddComponent<HDAdditionalCameraData>();
            data.DisableAllCameraFrameSettings();
            data.SetCameraFrameSetting(FrameSettingsField.OpaqueObjects, true);
            data.clearColorMode          = HDAdditionalCameraData.ClearColorMode.None;
            data.customRenderingSettings = true;


            var currentShadowDistance = QualitySettings.shadowDistance;
            var lodBias               = QualitySettings.lodBias;

            var terrains                                            = Terrain.activeTerrains;
            var pixelError                                          = new float[terrains.Length];
            for (var i = 0; i < terrains.Length; i++) pixelError[i] = terrains[i].heightmapPixelError;

            try
            {
                QualitySettings.shadowDistance = 0;
                QualitySettings.lodBias        = 10;
                foreach (var terrain in terrains) terrain.heightmapPixelError = 1;

                depthCamera.targetTexture = depthRT;
                depthCamera.Render();
               // KW_Extensions.WaterLog(this, "Render ortho depth");
            }
            finally
            {
                for (var i = 0; i < terrains.Length; i++)
                {
                    terrains[i].heightmapPixelError = pixelError[i];
                }

                QualitySettings.shadowDistance = currentShadowDistance;
                QualitySettings.lodBias        = lodBias;
            }
        }
    }
}