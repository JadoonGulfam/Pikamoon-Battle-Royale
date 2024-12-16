#if URP
using System;
using UnityEditor;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

namespace StylizedWater2.DynamicEffects
{
    [CustomEditor(typeof(WaterDynamicEffectsRenderFeature))]
    public class RenderFeatureEditor : Editor
    {
        private SerializedProperty settings;
        
        private SerializedProperty renderRange;
        private SerializedProperty fadePercentage;
        
        private SerializedProperty texelsPerUnit;
        private SerializedProperty maxResolution;
        private SerializedProperty highPrecision;

        private SerializedProperty enableDisplacement;
        private SerializedProperty enableNormals;
        private SerializedProperty halfResolutionNormals;
        private SerializedProperty normalMipmaps;

        private SerializedProperty ignoreSceneView;
        
        private SerializedProperty displacementNormalShader;

        [MenuItem("Window/Stylized Water 2/Set up Dynamic Effects", false, 3000)]
        private static void SetupRenderers()
        {
            if (PipelineUtilities.RenderFeatureMissing<WaterDynamicEffectsRenderFeature>(out ScriptableRendererData[] renderers))
            {
                string[] rendererNames = new string[renderers.Length];
                for (int i = 0; i < rendererNames.Length; i++)
                {
                    rendererNames[i] = "• " + renderers[i].name;
                }

                if (EditorUtility.DisplayDialog("Dynamic Effects", $"The Dynamic Effects render feature hasn't been added to the following renderers:\n\n" +
                                                                   String.Join(Environment.NewLine, rendererNames) +
                                                                   $"\n\nThis is required for rendering to take effect.", "Setup", "Ignore"))
                {
                    PipelineUtilities.SetupRenderFeature<WaterDynamicEffectsRenderFeature>(name:"Stylized Water 2: Dynamic Effects");
                }
            }
            else
            {
                EditorUtility.DisplayDialog(AssetInfo.ASSET_NAME, "The Dynamic Effects render feature has already been added to your default renderers", "Ok");
            }
        }

        [MenuItem("Window/Analysis/Dynamic Effects debugger", false, 0)]
        private static void OpenDebugger()
        {
            DynamicEffectsBufferDebugger.Open();
        }

        void OnEnable()
        {
            settings = serializedObject.FindProperty("settings");
            
            renderRange = settings.FindPropertyRelative("renderRange");
            fadePercentage = settings.FindPropertyRelative("fadePercentage");
            
            texelsPerUnit = settings.FindPropertyRelative("texelsPerUnit");
            maxResolution = settings.FindPropertyRelative("maxResolution");
            highPrecision = settings.FindPropertyRelative("highPrecision");
            
            enableDisplacement = settings.FindPropertyRelative("enableDisplacement");
            enableNormals = settings.FindPropertyRelative("enableNormals");
            halfResolutionNormals = settings.FindPropertyRelative("halfResolutionNormals");
            normalMipmaps = settings.FindPropertyRelative("normalMipmaps");
            
            ignoreSceneView = settings.FindPropertyRelative("ignoreSceneView");

            displacementNormalShader = serializedObject.FindProperty("displacementNormalShader");

            if (target.name == "NewWaterDynamicEffectsRenderFeature") target.name = "Stylized Water 2: Dynamic Effects";
        }
        
        public override void OnInspectorGUI()
        {
            using (new EditorGUILayout.HorizontalScope())
            {
                EditorGUILayout.LabelField($"Version {WaterDynamicEffectsRenderFeature.Version}", EditorStyles.miniLabel);

                if (GUILayout.Button(new GUIContent(" Documentation", EditorGUIUtility.FindTexture("_Help"))))
                {
                    Application.OpenURL("https://staggart.xyz/unity/stylized-water-2/sw2-dynamic-effects-docs/");
                }
                if (GUILayout.Button(new GUIContent(" Debugger", EditorGUIUtility.IconContent("Profiler.Rendering").image, "Inspect the render buffer output")))
                {
                    OpenDebugger();
                }
            }
            EditorGUILayout.Space();
            
            serializedObject.Update();
            EditorGUI.BeginChangeCheck();

            EditorGUILayout.PropertyField(renderRange);
            EditorGUI.indentLevel++;
            EditorGUILayout.PropertyField(fadePercentage, new GUIContent("Fade range (%)", fadePercentage.tooltip));
            EditorGUI.indentLevel--;

            EditorGUILayout.Space();
            
            EditorGUILayout.PropertyField(texelsPerUnit);
            EditorGUILayout.PropertyField(maxResolution);
            using (new EditorGUILayout.HorizontalScope())
            {
                GUILayout.Space(EditorGUIUtility.labelWidth);
                
                EditorGUILayout.HelpBox($"Current resolution: {WaterDynamicEffectsRenderFeature.CalculateResolution(renderRange.floatValue, texelsPerUnit.intValue, maxResolution.intValue)}px", MessageType.None);
            }
            EditorGUILayout.PropertyField(highPrecision);

            EditorGUILayout.Space();
                
            EditorGUILayout.PropertyField(enableDisplacement);

            EditorGUILayout.PropertyField(enableNormals);
            using (new EditorGUI.DisabledGroupScope(enableNormals.boolValue == false))
            {
                EditorGUI.indentLevel++;
                EditorGUILayout.PropertyField(normalMipmaps);
                EditorGUILayout.PropertyField(halfResolutionNormals);
                EditorGUI.indentLevel--;
            }
            
            EditorGUILayout.Space();
            
            EditorGUILayout.PropertyField(ignoreSceneView);

            if (EditorGUI.EndChangeCheck())
            {
                serializedObject.ApplyModifiedProperties();
            }
            
            //base.OnInspectorGUI();

            if (displacementNormalShader.objectReferenceValue == null)
            {
                EditorGUILayout.HelpBox("Internal shader not referenced!", MessageType.Error);
                if (GUILayout.Button("Find & assign"))
                {
                    displacementNormalShader.objectReferenceValue = Shader.Find("Hidden/StylizedWater2/DisplacementToNormals");
                    serializedObject.ApplyModifiedProperties();
                }
            }
            
            UI.DrawFooter();
        }
    }
    
    public class DynamicEffectsBufferDebugger : EditorWindow
    {
        private const int m_width = 550;
        private const int m_height = 300;
        
        #if SWS_DEV
        [MenuItem("SWS/Debug/Dynamic Effects Buffers")]
        #endif
        public static void Open()
        {
            DynamicEffectsBufferDebugger window = GetWindow<DynamicEffectsBufferDebugger>(false);
            window.titleContent = new GUIContent("Dynamic Effects Debugger", Resources.Load<Texture>("dynamic-effect-icon-256px"));

            window.autoRepaintOnSceneChange = true;
            window.minSize = new Vector2(m_width, m_height);
            //window.maxSize = new Vector2(m_width, m_height);
            window.Show();
        }

        private float width = 300f;
        private Vector2 scrollPos;

        private ColorWriteMask colorMask = ColorWriteMask.All;
        private int colorChannel = 1;

        private void OnGUI()
        {
            Repaint();
            
            width = (Mathf.Min(this.position.height, this.position.width) * 0.8f) - 10f;
            scrollPos = EditorGUILayout.BeginScrollView(scrollPos);

            RenderTexture interactionData = Shader.GetGlobalTexture("_WaterDynamicEffectsBuffer") as RenderTexture;
            RenderTexture interactionNormals = Shader.GetGlobalTexture("_WaterDynamicEffectsNormals") as RenderTexture;
            
            using (new EditorGUILayout.HorizontalScope())
            {
                using (new EditorGUILayout.VerticalScope())
                {
                    DrawTexture("Data", interactionData);
                }

                using (new EditorGUILayout.VerticalScope())
                {
                    DrawTexture("Normals", interactionNormals);
                }
            }

            EditorGUILayout.EndScrollView();
        }

        private void DrawTexture(string label, Texture texture)
        {
            if (!texture) return;
            
            EditorGUILayout.LabelField($"{label} ({texture.width}px @ {(UnityEngine.Profiling.Profiler.GetRuntimeMemorySizeLong(texture) / 1024f / 1024f).ToString("F2")}mb)", EditorStyles.boldLabel);
            
            Rect rect = EditorGUILayout.GetControlRect();

            Rect position = EditorGUI.PrefixLabel(rect, GUIUtility.GetControlID(FocusType.Passive), GUIContent.none);
            position.width = width;

            //colorChannel = EditorGUI.Popup(position, "Channel mask", colorChannel, new string[] { "RGB", "R", "G", "B", "A" }); 
            colorChannel = (int)GUI.Toolbar(position, colorChannel, new GUIContent[] { new GUIContent("RGBA"), new GUIContent("RGB"), new GUIContent("R"), new GUIContent("G"), new GUIContent("B"), new GUIContent("A") });

            switch (colorChannel)
            {
                case 1: colorMask = ColorWriteMask.All;
                    break;
                case 2: colorMask = ColorWriteMask.Red;
                    break;
                case 3: colorMask = ColorWriteMask.Green;
                    break;
                case 4: colorMask = ColorWriteMask.Blue;
                    break;
                case 5: colorMask = ColorWriteMask.Alpha;
                    break;
            }

            rect.y += 17f;
            rect.height = width;
            rect.width = width;

            if (colorChannel == 0) //RGBA
            {
                EditorGUI.DrawTextureTransparent(rect, texture, ScaleMode.ScaleToFit, 1f);
            }
            else if (colorMask == ColorWriteMask.Alpha)
            {
                EditorGUI.DrawTextureAlpha(rect, texture, ScaleMode.ScaleToFit, 1f, 0);
            }
            else
            {
                EditorGUI.DrawPreviewTexture(rect, texture, null, ScaleMode.ScaleToFit, 1f, 0, colorMask);
            }
            GUILayout.Space(rect.height);
        }
    }
}
#endif