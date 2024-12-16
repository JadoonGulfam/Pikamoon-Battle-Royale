using System;
using StylizedWater2;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;
#if UNITY_EDITOR
using UnityEditor;
#endif
#if URP
using UnityEngine.Rendering.Universal;
#endif

namespace StylizedWater2.DynamicEffects
{
    [ExecuteInEditMode]
    public class DemoController : MonoBehaviour
    {
        #pragma warning disable 0108
        private Camera camera;
        private FreeCamera freeCamera;
        #pragma warning restore 0108

        public GameObject[] exhibits = Array.Empty<GameObject>();

        private int exhibitIndex;

        [Header("UI References")]
        public Text headerText;
        public Slider renderRangeSlider;
        public Slider texelsPerUnitSlider;
        public Text renderFeatureText;

        private WaterDynamicEffectsRenderFeature.Settings renderFeatureSettings;

        private void Start()
        {
            camera = Camera.main;
            freeCamera = camera.GetComponentInChildren<FreeCamera>(true);

            renderFeatureSettings = WaterDynamicEffectsRenderFeature.Settings.GetCurrent();
            renderRangeSlider.value = renderFeatureSettings.renderRange;
            texelsPerUnitSlider.value = renderFeatureSettings.texelsPerUnit;

            if (Application.isPlaying == true) SwitchToExhibit(0);

            Canvas canvas = GetComponentInChildren<Canvas>(true);
            canvas.gameObject.SetActive(true);

            Camera[] cams = this.GetComponentsInChildren<Camera>(true);
            for (int i = 0; i < cams.Length; i++)
            {
                cams[i].enabled = false;
            }
        }

        private void OnEnable()
        {
            #if URP
            PipelineUtilities.ValidateRenderFeatureSetup<WaterDynamicEffectsRenderFeature>("Dynamic Effects");
            #endif
        }

        //UI-controlled
        public void ToggleFreeCamera()
        {
            freeCamera.enabled = !freeCamera.enabled;
        }

        public void OpenDebugger()
        {
            #if UNITY_EDITOR
            EditorApplication.ExecuteMenuItem("Window/Analysis/Dynamic Effects debugger");
            #endif
        }

        private void Update()
        {
            if (Application.isPlaying)
            {
                renderFeatureSettings.renderRange = renderRangeSlider.value;
                renderFeatureSettings.texelsPerUnit = (int)texelsPerUnitSlider.value;

                renderFeatureText.text = $"Range: {Math.Round(renderFeatureSettings.renderRange, 0)}m" +
                                         $"\nTexels per unit: {renderFeatureSettings.texelsPerUnit}" +
                                         $"\nFinal resolution: {WaterDynamicEffectsRenderFeature.CalculateResolution(renderFeatureSettings.renderRange, renderFeatureSettings.texelsPerUnit, renderFeatureSettings.maxResolution)}px";
            }
        }

        //UI-controlled
        public void MoveNext()
        {
            exhibitIndex++;

            if (exhibitIndex == exhibits.Length) exhibitIndex = 0;

            SwitchToExhibit(exhibitIndex);

        }

        //UI-controlled
        public void MovePrevious()
        {
            exhibitIndex--;

            if (exhibitIndex < 0) exhibitIndex = exhibits.Length-1;

            SwitchToExhibit(exhibitIndex);
        }

        private void SwitchToExhibit(int index)
        {
            for (int i = 0; i < exhibits.Length; i++)
            {
                if (i == index)
                {
                    headerText.text = exhibits[i].name;
                    
                    exhibits[i].SetActive(true);
                    
                    Camera cam = (Camera)exhibits[i].GetComponentInChildren(typeof(Camera), true);
                    
                    if (cam)
                    {
                        camera.transform.SetPositionAndRotation(cam.transform.position, cam.transform.rotation);
                        camera.fieldOfView = cam.fieldOfView;
                    }

                }
                else
                {
                    exhibits[i].SetActive(false);
                }
            }
        }
    }
}
