using System;
using UnityEditor;
using UnityEngine;
#if URP
using UnityEngine.Rendering.Universal;
#endif

namespace StylizedWater2.DynamicEffects
{
    [CustomEditor(typeof(DynamicEffect))]
    [CanEditMultipleObjects]
    public class DynamicEffectInspector : Editor
    {
        private SerializedProperty renderer;
        
        private SerializedProperty sortingLayer;
        
        private SerializedProperty displacementScale;
        private SerializedProperty foamAmount;
        private SerializedProperty normalStrength;

        #if URP
        private bool renderFeaturePresent;
        private bool renderFeatureEnabled;
        private WaterDynamicEffectsRenderFeature renderFeature;
        private Editor renderFeatureEditor;
        #endif
        
        private void OnEnable()
        {
            renderer = serializedObject.FindProperty("renderer");
            
            sortingLayer = serializedObject.FindProperty("sortingLayer");
            
            displacementScale = serializedObject.FindProperty("displacementScale");
            foamAmount = serializedObject.FindProperty("foamAmount");
            normalStrength = serializedObject.FindProperty("normalStrength");
            
            #if URP
            renderFeaturePresent = PipelineUtilities.RenderFeatureAdded<WaterDynamicEffectsRenderFeature>();
            renderFeatureEnabled = PipelineUtilities.IsRenderFeatureEnabled<WaterDynamicEffectsRenderFeature>();
            renderFeature = PipelineUtilities.GetRenderFeature<WaterDynamicEffectsRenderFeature>() as WaterDynamicEffectsRenderFeature;
            #endif
        }
        
        private void DrawNotifications()
        {
            #if URP
            UI.DrawNotification( !AssetInfo.MeetsMinimumVersion(WaterDynamicEffectsRenderFeature.MinBaseVersion), "Version mismatch, requires Stylized Water 2 v" + WaterDynamicEffectsRenderFeature.MinBaseVersion +".\n\nUpdate to avoid any issues or resolve (shader) errors", "Update", () => AssetInfo.OpenInPackageManager(), MessageType.Error);
            
            UI.DrawNotification(UniversalRenderPipeline.asset == null, "The Universal Render Pipeline is not active", MessageType.Error);
            
            using (new EditorGUI.DisabledGroupScope(Application.isPlaying))
            {
                UI.DrawNotification(!renderFeaturePresent, "The Dynamic Effects render feature hasn't be added to the default renderer", "Add", () =>
                {
                    PipelineUtilities.AddRenderFeature<WaterDynamicEffectsRenderFeature>();
                    renderFeaturePresent = true;
                    renderFeature = PipelineUtilities.GetRenderFeature<WaterDynamicEffectsRenderFeature>() as WaterDynamicEffectsRenderFeature;
                }, MessageType.Error);
            }
            if(Application.isPlaying && !renderFeaturePresent) EditorGUILayout.HelpBox("Exit play mode to perform this action", MessageType.Warning);
            
            UI.DrawNotification(renderFeaturePresent && !renderFeatureEnabled, "The Dynamic Effects render feature is disabled", "Enable", () => 
            { 
                PipelineUtilities.ToggleRenderFeature<WaterDynamicEffectsRenderFeature>(true);
                renderFeatureEnabled = true; 
            }, MessageType.Warning);
            #else
            UI.DrawNotification("The Universal Render Pipeline package is not installed!", MessageType.Error);
            #endif
        }
        
        public override void OnInspectorGUI()
        {
            EditorGUILayout.LabelField($"{AssetInfo.ASSET_NAME }: Dynamic Effects v{WaterDynamicEffectsRenderFeature.Version}", EditorStyles.centeredGreyMiniLabel);
            EditorGUILayout.Space();
            
            DrawNotifications();

            serializedObject.Update();
            
            EditorGUI.BeginChangeCheck();

            EditorGUILayout.PropertyField(renderer);

            if (renderer.objectReferenceValue)
            {
                EditorGUILayout.BeginHorizontal();
                {
                    EditorGUILayout.PrefixLabel(new GUIContent(sortingLayer.displayName, sortingLayer.tooltip));

                    if (GUILayout.Button("-", EditorStyles.miniButtonLeft, GUILayout.Width(20)))
                    {
                        sortingLayer.intValue--;
                    }

                    EditorGUILayout.PropertyField(sortingLayer, GUIContent.none, GUILayout.MaxWidth(32));

                    if (GUILayout.Button("+", EditorStyles.miniButtonRight, GUILayout.Width(20)))
                    {
                        sortingLayer.intValue++;
                    }
                }
                EditorGUILayout.EndHorizontal();

                EditorGUILayout.Space();

                EditorGUILayout.PropertyField(displacementScale);
                EditorGUILayout.PropertyField(foamAmount);
                EditorGUILayout.PropertyField(normalStrength);
            }
            else
            {
                UI.DrawNotification("A renderer must be assigned", MessageType.Error);
            }

            if (EditorGUI.EndChangeCheck())
            {
                serializedObject.ApplyModifiedProperties();
                DynamicEffect script = (DynamicEffect)target;
                script.UpdateProperties();
            }

            UI.DrawFooter();
        }
    }
}