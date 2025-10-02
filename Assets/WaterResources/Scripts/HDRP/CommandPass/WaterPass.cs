using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.HighDefinition;

namespace  KWS
{
    internal abstract class WaterPass : CustomPass
    {
        public struct WaterPassContext
        {
            public Camera        cam;
            public CommandBuffer cmd;
            public RTHandle      cameraDepth;
            public RTHandle      cameraColor;
            //public int RequiredFixedUpdateCount;
            internal CustomFixedUpdates      FixedUpdates;
            public   ScriptableRenderContext RenderContext;
            public   HDAdditionalCameraData  AdditionalCameraData;
        }


        WaterPassContext _waterContext;

        public void SetWaterContext(WaterPassContext waterContext)
        {
            _waterContext = waterContext;
            name          = PassName;
        }

        protected override void Execute(CustomPassContext ctx)
        {
            _waterContext.cmd         = ctx.cmd;
            _waterContext.cam         = ctx.hdCamera.camera;
            _waterContext.cameraColor = ctx.cameraColorBuffer;
            _waterContext.cameraDepth = ctx.cameraDepthBuffer;

            ExecuteCommandBuffer(_waterContext);
        }

        internal virtual string PassName                                                                        { get; }
        public virtual   void   ExecuteCommandBuffer(WaterPassContext waterContext)                             { }
        public virtual   void   ExecuteBeforeCameraRendering(Camera   cam)                                      { }
        public virtual   void   ExecutePerFrame(HashSet<Camera>       cameras, CustomFixedUpdates fixedUpdates) { }
        public abstract  void   Release();
    }
}
