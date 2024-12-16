//Stylized Water 2
//Staggart Creations (http://staggart.xyz)
//Copyright protected under Unity Asset Store EULA

using System;
using UnityEngine;

namespace StylizedWater2
{
    [HelpURL("https://staggart.xyz/unity/stylized-water-2/sw2-dynamic-effects-docs/")]
    [AddComponentMenu("Stylized Water 2/Dynamic Water Effect")]
    public class DynamicEffect : MonoBehaviour
    {
        #pragma warning disable 108,114 //New keyword
        public Renderer renderer; 
        #pragma warning restore 108,114

        [Tooltip("Higher layers are always drawn over lower layers. Use this to override other effects on a lower layer.\n\nThis is effectively the render queue")]
        public int sortingLayer = 0;

        public float displacementScale = 1f;
        public float foamAmount = 1f;
        public float normalStrength = 1f;
        
        private void Reset()
        {
            renderer = GetComponent<Renderer>();

            if (!renderer)
            {
                DestroyImmediate(this);
                throw new Exception("Component must only be added to a GameObject with a renderer (Mesh Renderer, Trail Renderer, Line Renderer or Particle System)");
            }
        }
        
        private MaterialPropertyBlock _props;
        public MaterialPropertyBlock props
        {
            get
            {
                //Fetch when required, execution order makes it unreliable otherwise
                if (_props == null)
                {
                    _props = new MaterialPropertyBlock();
                    renderer.GetPropertyBlock(_props);
                }
                return _props;
            }
            private set => _props = value;
        }

        void Start()
        {
            UpdateProperties();
        }
        
        private void OnValidate()
        {
            UpdateProperties();
        }

        private static readonly int _HeightScale = Shader.PropertyToID("_HeightScale");
        private static readonly int _FoamStrength = Shader.PropertyToID("_FoamStrength");
        private static readonly int _NormalStrength = Shader.PropertyToID("_NormalStrength");
        
        public void UpdateProperties()
        {
            if (!renderer) return;
            
            renderer.sortingOrder = sortingLayer;
            
            props.SetFloat(_HeightScale, displacementScale);
            props.SetFloat(_FoamStrength, foamAmount);
            props.SetFloat(_NormalStrength, normalStrength);
   
            renderer.SetPropertyBlock(props);
        }

        #if UNITY_EDITOR
        [ContextMenu("Open Rendering Debugger")]
        private void OpenDebugger()
        {
            UnityEditor.EditorApplication.ExecuteMenuItem("Window/Analysis/Dynamic Effects debugger");
        }
        #endif
    }
}