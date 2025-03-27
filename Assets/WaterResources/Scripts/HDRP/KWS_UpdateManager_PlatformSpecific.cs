using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

namespace KWS
{

    internal partial class KWS_UpdateManager
    {
        private KWS_WaterPassHandler _passHandler;

        void OnEnablePlatformSpecific()
        {
            RenderPipelineManager.beginCameraRendering  += OnBeforeCameraRendering;
            if (_passHandler == null) _passHandler = new KWS_WaterPassHandler();
        }

       
        void OnDisablePlatformSpecific()
        {
            RenderPipelineManager.beginCameraRendering  -= OnBeforeCameraRendering;
            _passHandler?.Release();

            KWS_CoreUtils.ReleaseRTHandles();
        }


        private void OnBeforeCameraRendering(ScriptableRenderContext context, Camera cam)
        {
          
            ExecutePerCamera(cam, context);
        }
    }
}