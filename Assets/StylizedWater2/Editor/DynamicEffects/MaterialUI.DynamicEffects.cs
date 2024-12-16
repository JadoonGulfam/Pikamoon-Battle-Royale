using StylizedWater2.DynamicEffects;
using UnityEditor;
using UnityEngine;

namespace StylizedWater2
{
    #if URP
    partial class MaterialUI : ShaderGUI
    {
        private bool dynamicEffectsInitialized;
        
        private bool deRenderFeaturePresent;
        private bool deRenderFeatureEnabled;
        
        partial void DrawDynamicEffectsUI()
        {
            if (!dynamicEffectsInitialized)
            {
                deRenderFeaturePresent = PipelineUtilities.GetRenderFeature<WaterDynamicEffectsRenderFeature>();
                if(deRenderFeaturePresent) deRenderFeatureEnabled = PipelineUtilities.IsRenderFeatureEnabled<WaterDynamicEffectsRenderFeature>();
                
                dynamicEffectsInitialized = true;
            }
            
            using (new EditorGUI.DisabledGroupScope(Application.isPlaying))
            {
                UI.DrawNotification(!deRenderFeaturePresent, "The Dynamic Effects extension is installed, but the render feature hasn't been setup on the default renderer", "Add", () =>
                {
                    PipelineUtilities.AddRenderFeature<WaterDynamicEffectsRenderFeature>(name:"Stylized Water 2: Dynamic Effects");
                    deRenderFeaturePresent = true;
                    deRenderFeatureEnabled = true;
                }, MessageType.Error);
            }
            if(Application.isPlaying && !deRenderFeaturePresent) EditorGUILayout.HelpBox("Exit play mode to perform this action", MessageType.Warning);
        
            UI.DrawNotification(deRenderFeaturePresent && !deRenderFeatureEnabled, "The Dynamic Effects render feature is disabled", "Enable", () => 
            { 
                PipelineUtilities.ToggleRenderFeature<WaterDynamicEffectsRenderFeature>(true);
                deRenderFeatureEnabled = true; 
            }, MessageType.Warning);
        } 
    }
    #endif
}