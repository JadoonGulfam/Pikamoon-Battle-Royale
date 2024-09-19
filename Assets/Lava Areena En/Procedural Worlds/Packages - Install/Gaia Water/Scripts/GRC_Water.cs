using UnityEditor;
using UnityEngine;

namespace Gaia
{
    public class GRC_Water : GaiaRuntimeComponent
    {
        private GameObject m_currentWaterPrefab;
        private GUIContent m_generalHelpLink;

        private GRC_WaterSettings m_waterSettings;
        public GRC_WaterSettings WaterSettings
        {
            get
            {
                if (m_waterSettings == null)
                {
                    m_waterSettings = GetWaterSettings();
                }
                return m_waterSettings;
            }
        }

        private GUIContent m_panelLabel;
        public override GUIContent PanelLabel
        {
            get
            {
                if (m_panelLabel == null || m_panelLabel.text == "")
                {
                    m_panelLabel = new GUIContent("Gaia Water", "Add a water surface & underwater effects at the sea level in your scene.");
                }
                return m_panelLabel;
            }
        }

        public override void Initialize()
        {
            m_orderNumber = 200;

            if (m_generalHelpLink == null || m_generalHelpLink.text == "")
            {
                m_generalHelpLink = new GUIContent("Gaia Water Module on Canopy", "Opens the Canopy Online Help Article for the Gaia Water Module");
            }
            if (WaterSettings != null)
            {
#if HDPipeline
                m_currentWaterPrefab = WaterSettings.m_HDRPWaterPrefab;
#elif UPPipeline
                m_currentWaterPrefab = WaterSettings.m_URPWaterPrefab;
#else
                m_currentWaterPrefab = WaterSettings.m_builtInWaterPrefab;
#endif
            }
        }

        public override void DrawUI()
        {
#if UNITY_EDITOR
            DisplayHelp("You can add an ocean water surface to your scene to simulate water at the sea level. This can be used to render an ocean or a lake in your scene. You will be able to customize the look of the water on the Water Surface Game Object." ,m_generalHelpLink, "https://canopy.procedural-worlds.com/library/tools/gaia-pro-2021/written-articles/creating_runtime/runtime-module-gaia-water-r166/");

            bool originalGUIState = GUI.enabled;
            EditorGUI.BeginChangeCheck();
            { 
                GUILayout.BeginHorizontal();
                GUILayout.FlexibleSpace();
                if (GUILayout.Button("Remove"))
                {
                    RemoveFromScene();
                }
                GUILayout.Space(15);
                if (GUILayout.Button("Apply"))
                {
                    AddToScene();
                }
                GUILayout.EndHorizontal();
            }
            if (EditorGUI.EndChangeCheck())
            {
                EditorUtility.SetDirty(this);
            }
#endif
        }

        public override void AddToScene()
        {
            //Remove any old versions first
            RemoveFromScene();
            GameObject gaiaRuntimeObject = GaiaUtils.GetRuntimeSceneObject(true);
            if (m_currentWaterPrefab != null)
            {
                GameObject newWaterGO = GameObject.Instantiate(m_currentWaterPrefab, gaiaRuntimeObject.transform);
                newWaterGO.name = newWaterGO.name.Replace("(Clone)", "");
            }

            GaiaUtils.RefreshPlayerSetup();


        }

        public override void RemoveFromScene()
        {
            GameObject gaiaWaterObject = GaiaUtils.GetWaterObject();
            if (gaiaWaterObject != null)
            {
                if (Application.isPlaying)
                {
                    GameObject.Destroy(gaiaWaterObject);
                }
                else
                {
                    GameObject.DestroyImmediate(gaiaWaterObject);
                }
            }
        }


        /// <summary>
        /// Return WaterSettings or null;
        /// </summary>
        /// <returns>Gaia settings or null if not found</returns>
        public static GRC_WaterSettings GetWaterSettings()
        {
            return GaiaUtils.GetAsset("Water Settings.asset", typeof(GRC_WaterSettings)) as GRC_WaterSettings;
        }
    }
}
