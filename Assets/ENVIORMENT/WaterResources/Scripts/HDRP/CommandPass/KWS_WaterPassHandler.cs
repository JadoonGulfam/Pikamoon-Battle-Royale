using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.HighDefinition;

namespace KWS
{

    internal class KWS_WaterPassHandler 
    {
        List<WaterPass>    _waterPasses       = new List<WaterPass>();
       
        OrthoDepthPass   _orthoDepthPass ;
        FftWavesPass     _fftWavesPass;
        BuoyancyPass     _buoyancyPass;
        FlowPass         _flowPass;
        DynamicWavesPass _dynamicWavesPass;

        ShorelineWavesPass     _shorelineWavesPass;
        WaterPrePass           _waterPrePass;
        MotionVectorsPass      _motionVectorsPass;
        CausticPrePass         _causticPrePass;
        VolumetricLightingPass _volumetricLightingPass;
        CausticDecalPass       _causticDecalPass;

        ScreenSpaceReflectionPass  _ssrPass;
        ReflectionFinalPass        _reflectionFinalPass;
        DrawMeshPass               _drawMeshPass;
        ShorelineFoamPass          _shorelineFoamPass;
        UnderwaterPass             _underwaterPass;
        DrawToPosteffectsDepthPass _drawToDepthPass;

        Dictionary<CustomPassInjectionPoint, CustomPassVolume> _volumes = new Dictionary<CustomPassInjectionPoint, CustomPassVolume>();

        internal KWS_WaterPassHandler()
        {
            CreateVolume(CustomPassInjectionPoint.BeforePreRefraction);
            CreateVolume(CustomPassInjectionPoint.BeforeTransparent);
            CreateVolume(CustomPassInjectionPoint.BeforePostProcess);
        }

        public void Release()
        {
            foreach (var customPassVolume in _volumes)
            {
                KW_Extensions.SafeDestroy(customPassVolume.Value.GetComponent<GameObject>());
            }

            foreach (var waterPass in _waterPasses) waterPass?.Release();
            _waterPasses.Clear();
        }

        internal void OnBeforeFrameRendering(HashSet<Camera> cameras, CustomFixedUpdates fixedUpdates)
        {
            foreach (var waterPass in _waterPasses) waterPass.ExecutePerFrame(cameras, fixedUpdates);
        }


        internal void OnBeforeCameraRendering(Camera cam, ScriptableRenderContext ctx)
        {
            //"volume.customCamera" and "volumePass.enabled" just ignored for other cameras in the current frame... Typical unity HDRiP rendering.
            try
            {
                var data = cam.GetComponent<HDAdditionalCameraData>();
                //if (data == null) return;

                var cameraSize = KWS_CoreUtils.GetScreenSizeLimited(KWS_CoreUtils.SinglePassStereoEnabled);
                KWS_CoreUtils.RTHandles.SetReferenceSize(cameraSize.x, cameraSize.y);

                WaterPass.WaterPassContext waterContext = default;

                waterContext.cam = cam;
                waterContext.RenderContext        = ctx;
                waterContext.AdditionalCameraData = data;

                if (_waterPasses.Count == 0)
                {
                    InitializePass(ref _orthoDepthPass,   CustomPassInjectionPoint.BeforePreRefraction);
                    InitializePass(ref _fftWavesPass,     CustomPassInjectionPoint.BeforePreRefraction);
                    InitializePass(ref _buoyancyPass,     CustomPassInjectionPoint.BeforePreRefraction);
                    InitializePass(ref _flowPass,         CustomPassInjectionPoint.BeforePreRefraction);
                    InitializePass(ref _dynamicWavesPass, CustomPassInjectionPoint.BeforePreRefraction);

                    InitializePass(ref _shorelineWavesPass,     CustomPassInjectionPoint.BeforePreRefraction);
                    InitializePass(ref _waterPrePass,           CustomPassInjectionPoint.BeforePreRefraction);
                    InitializePass(ref _motionVectorsPass,      CustomPassInjectionPoint.BeforePreRefraction);
                    InitializePass(ref _causticPrePass,         CustomPassInjectionPoint.BeforePreRefraction);
                    InitializePass(ref _volumetricLightingPass, CustomPassInjectionPoint.BeforePreRefraction);
                    InitializePass(ref _causticDecalPass,       CustomPassInjectionPoint.BeforePreRefraction);

                    InitializePass(ref _ssrPass,             CustomPassInjectionPoint.BeforeTransparent);
                    InitializePass(ref _reflectionFinalPass, CustomPassInjectionPoint.BeforeTransparent);
                    InitializePass(ref _drawMeshPass,        CustomPassInjectionPoint.BeforeTransparent);
                    InitializePass(ref _shorelineFoamPass,   CustomPassInjectionPoint.BeforeTransparent);
                    InitializePass(ref _underwaterPass,      CustomPassInjectionPoint.BeforeTransparent);

                    InitializePass(ref _drawToDepthPass, CustomPassInjectionPoint.BeforePostProcess);
                }

                foreach (var waterPass in _waterPasses)
                {
                    waterPass.SetWaterContext(waterContext);
                    waterPass.ExecuteBeforeCameraRendering(cam);
                }
              
            }
            catch (Exception e)
            {
                Debug.LogError("Water rendering error: " + e.InnerException);
            }
        }

        void CreateVolume(CustomPassInjectionPoint injectionPoint)
        {
            var tempGO = new GameObject("WaterVolume_" + injectionPoint) { hideFlags = HideFlags.DontSave };
            tempGO.transform.parent = WaterSystem.UpdateManagerObject.transform;
            var volume = tempGO.AddComponent<CustomPassVolume>();
            volume.injectionPoint = injectionPoint;
            _volumes.Add(injectionPoint, volume);
        }


        void InitializePass<T>(ref T pass, CustomPassInjectionPoint injectionPoint) where T : WaterPass
        {
            var volumePass = _volumes[injectionPoint];
            pass = (T)volumePass.AddPassOfType<T>();
            _waterPasses.Add(pass);
        }

    }
}