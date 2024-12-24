// Cristian Pop - https://boxophobic.com/

using UnityEngine;
using UnityEditor;
using UnityEditor.SceneManagement;
using Boxophobic.StyledGUI;
using Boxophobic.Utils;
using System;
using System.IO;
using System.Globalization;
using System.Collections.Generic;

namespace TheVisualEngine
{
    public class TVEMaterialManager : EditorWindow
    {
        const int MOTION_INDEX = 8;
        const int GUI_SMALL_WIDTH = 50;
        const int GUI_HEIGHT = 18;
        const int GUI_SELECTION_HEIGHT = 24;
        float GUI_HALF_EDITOR_WIDTH = 200;

        string[] materialOptions = new string[]
        {
        "All Material Settings", "Render Settings", "Shading Settings", "Coloring Settings", "Weather Settings", "Clipping Settings", "Lighting Settings", "Transform Settings", "Motion Settings", "Normal Settings",
        };

        string[] savingOptions = new string[]
        {
        "Save All Settings", "Save Current Settings",
        };

        List<TVEPropertyData> propertyDataSet = new List<TVEPropertyData>
        {

        };

        List<TVEPropertyData> savingDataSet = new List<TVEPropertyData>
        {

        };

        List<TVEPropertyData> renderData = new List<TVEPropertyData>
        {
            new TVEPropertyData("_RenderCategory", "Render Settings"),

            new TVEPropertyData("_RenderMode", "Render Mode", -9000, "Opaque 0 Transparent 1", false),
            new TVEPropertyData("_RenderCull", "Render Faces", -9000, "Both 0 Back 1 Front 2", false),
            new TVEPropertyData("_RenderNormal", "Render Normals", -9000, "Flip 0 Mirror 1 Same 2", false),
            new TVEPropertyData("_RenderSSR", "Render SSR", -9000, "Off 0 On 1", false),
            new TVEPropertyData("_RenderDecals", "Render Decals", -9000, "Off 0 On 1", false),
            new TVEPropertyData("_RenderMotion", "Render Motion", -9000, "Auto 0 Off 1 On 2", false),
            new TVEPropertyData("_RenderSpecular", "Render Specular", -9000, "Off 0 On 1", false),

            new TVEPropertyData("_RenderClip", "Render Clipping", -9000, "Off 0 On 1", false),

            //new TVEPropertyData("_RenderDirect", "Render Lighting", -9000, 0, 1, false, true),
            //new TVEPropertyData("_RenderAmbient", "Render Ambient", -9000, 0, 1, false, false),
            //new TVEPropertyData("_RenderShadow", "Render Shadow", -9000, 0, 1, false, false),


            //new TVEPropertyData("_RenderCoverage", "Alpha To Mask", -9000, "Off 0 On 1", false),
            //new TVEPropertyData("_AlphaClipValue", "Alpha Treshold", -9000, 0, 1, false, false),
            //new TVEPropertyData("_AlphaFeatherValue", "Alpha Feather", -9000, 0, 2, false, false),

            //new TVEPropertyData("_AI_Parallax", "Impostor Parallax", -9000, 0, 1, false, true),
            //new TVEPropertyData("_AI_ShadowView", "Impostor Shadow View", -9000, 0, 1, false, false),
            //new TVEPropertyData("_AI_ShadowBias", "Impostor Shadow Bias", -9000, 0, 1, false, false),
            //new TVEPropertyData("_AI_TextureBias", "Impostor Texture Bias", -9000, false, false),
            //new TVEPropertyData("_AI_Clip", "Impostor Alpha Treshold", -9000, 0, 1, false, false),
        };

        List<TVEPropertyData> globalData = new List<TVEPropertyData>
        {
            new TVEPropertyData("_GlobalCategory", "Global Settings"),

            new TVEPropertyData("_GlobalCoatLayerValue", "Global Coat Layer", -9000, "Coat Layers", "Default 0 Layer_1 1 Layer_2 2 Layer_3 3 Layer_4 4 Layer_5 5 Layer_6 6 Layer_7 8 Layer_8 8", false),
            new TVEPropertyData("_GlobalPaintLayerValue", "Global Paint Layer", -9000, "Paint Layers", "Default 0 Layer_1 1 Layer_2 2 Layer_3 3 Layer_4 4 Layer_5 5 Layer_6 6 Layer_7 8 Layer_8 8", false),
            new TVEPropertyData("_GlobalAtmoLayerValue", "Global Atmo Layer", -9000, "Atmo Layers", "Default 0 Layer_1 1 Layer_2 2 Layer_3 3 Layer_4 4 Layer_5 5 Layer_6 6 Layer_7 8 Layer_8 8", false),
            new TVEPropertyData("_GlobalGlowLayerValue", "Global Glow Layer", -9000, "Glow Layers", "Default 0 Layer_1 1 Layer_2 2 Layer_3 3 Layer_4 4 Layer_5 5 Layer_6 6 Layer_7 8 Layer_8 8", false),
            new TVEPropertyData("_GlobalWindLayerValue", "Global Wind Layer", -9000, "Wind Layers", "Default 0 Layer_1 1 Layer_2 2 Layer_3 3 Layer_4 4 Layer_5 5 Layer_6 6 Layer_7 8 Layer_8 8", false),
            new TVEPropertyData("_GlobalPushLayerValue", "Global Push Layer", -9000, "Push Layers", "Default 0 Layer_1 1 Layer_2 2 Layer_3 3 Layer_4 4 Layer_5 5 Layer_6 6 Layer_7 8 Layer_8 8", false),

            new TVEPropertyData("_GlobalCoatPivotValue", "Global Coat Pivots", -9000, 0, 1, false, false),
            new TVEPropertyData("_GlobalPaintPivotValue", "Global Paint Pivots", -9000, 0, 1, false, false),
            new TVEPropertyData("_GlobalAtmoPivotValue", "Global Atmo Pivots", -9000, 0, 1, false, false),
            new TVEPropertyData("_GlobalGlowPivotValue", "Global Glow Pivots", -9000, 0, 1, false, false),
            new TVEPropertyData("_GlobalFormPivotValue", "Global Form Pivots", -9000, 0, 1, false, false),
        };

        List<TVEPropertyData> surfaceDataGlobal = new List<TVEPropertyData>
        {
            new TVEPropertyData("_GlobalCategory", "Global Settings"),

            new TVEPropertyData("_GlobalCoatLayerValue", "Global Coat Layer", -9000, "Coat Layers", "Default 0 Layer_1 1 Layer_2 2 Layer_3 3 Layer_4 4 Layer_5 5 Layer_6 6 Layer_7 8 Layer_8 8", false),
            new TVEPropertyData("_GlobalCoatPivotValue", "Global Coat Pivots", -9000, 0, 1, false, false),
        };

        List<TVEPropertyData> surfaceData = new List<TVEPropertyData>
        {
            new TVEPropertyData("_MainCategory", "Main Settings"),

            new TVEPropertyData("_MainMultiMaskInfo", "Use the Multi Mask remap sliders to mask out the branches from the leaves when using Dual Colors or for Global Effects. The mask is stored in the Shader texture blue channel.", 0, 10, MessageType.Info),

            new TVEPropertyData("_MainAlbedoTex", "Main Albedo", null, false),
            new TVEPropertyData("_MainNormalTex", "Main Normal", null, false),
            new TVEPropertyData("_MainShaderTex", "Main Shader", null, false),

            //new TVEPropertyData("_MessageMainMask", "Use the Main Mask remap sliders to control the mask for Global Color, Main Colors, Gradient Tinting and Subsurface Effect. The mask is stored in Main Mask Blue channel.", 0, 10, MessageType.Info),
            new TVEPropertyData("_MainSampleMode", "Main Sampling", -9000, "Main_UV 0 Extra_UV 1 Planar 2 Triplanar 3 Stochastic 4 Stochastic_Triplanar 5", true),
            new TVEPropertyData("_MainCoordMode", "Main UV Mode", -9000, "Tilling_and_Offset 0 Scale_and_Offset 1", false),
            new TVEPropertyData("_MainCoordValue", "Main UV Value", new Vector4(-9000, 0,0,0), false),

            new TVEPropertyData("_MainColorMode", "Main Color", -9000, "Constant 0 Dual_Colors 1", true),
            new TVEPropertyData("_MainColor", "Main Color", new Color(-9000, 0,0,0), true, false),
            new TVEPropertyData("_MainColorTwo", "Main ColorB", new Color(-9000, 0,0,0), true, false),
            new TVEPropertyData("_MainAlphaClipValue", "Main Alpha", -9000, 0, 1, false, false),
            new TVEPropertyData("_MainAlbedoValue", "Main Albedo", -9000, 0, 1, false, false),
            new TVEPropertyData("_MainNormalValue", "Main Normal", -9000, -8, 8, false, false),
            new TVEPropertyData("_MainMetallicValue", "Main Metallic", -9000, 0, 1, false, false),
            new TVEPropertyData("_MainOcclusionValue", "Main Occlusion", -9000, 0, 1, false, false),
            new TVEPropertyData("_MainMultiRemap", "Main Multi Mask", new Vector4(-9000, -9000, -9000, -9000), 0, 1, false),
            new TVEPropertyData("_MainSmoothnessValue", "Main Smoothness", -9000, 0, 1, false, false),

            new TVEPropertyData("_ImpostorCategory", "Impostor Settings"),

            new TVEPropertyData("_ImpostorMaskOffInfo", "When the Maps option is disabled, only the Albedo and Normal textures are used. Metallic, Occlusion or Emissive, Multi Mask, Smoothness and Vertex Color masks are not supported.", 0, 10, MessageType.Info),
            new TVEPropertyData("_ImpostorMaskDefaultInfo", "The Default maps mode uses the Shader texture for Metallic R, Occlusion or Emissive G, Multi Mask B, and Smoothness A and the Vertex texture for Vertex Color used for mesh masks. Confider using the Packed option if Metallic, Emissive, Vertex B and Vertex A masks are not used to save performance and texture memory.", 0, 10, MessageType.Info),
            new TVEPropertyData("_ImpostorMaskPackedInfo", "The Packed maps mode uses the Packed texture for Vertex R, Vertex G, Multi Mask B, and Smoothness A. The packed texture stores the most common masks used for vegetation assets.", 0, 10, MessageType.Info),
            new TVEPropertyData("_ImpostorMaskShadingInfo", "The Shading maps mode uses the Shader texture for Metallic R, Occlusion or Emissive G, Multi Mask B, and Smoothness A. Using the mesh masks for globals will have no effect on the shading.", 0, 10, MessageType.Info),

            new TVEPropertyData("_ImpostorMaskMode", "Impostor Maps", -9000, "Off 0 Default 1 Packed 2 Shading 3", false),
            new TVEPropertyData("_ImpostorColorMode", "Impostor Color", -9000, "Constant 0 Dual_Colors 1", false),
            new TVEPropertyData("_ImpostorColor", "Impostor Color", new Color(-9000, 0,0,0), true, false),
            new TVEPropertyData("_ImpostorColorTwo", "Impostor ColorB", new Color(-9000, 0,0,0), true, false),
            new TVEPropertyData("_ImpostorAlphaClipValue", "Impostor Alpha", -9000, 0, 1, false, false),
            new TVEPropertyData("_ImpostorMetallicValue", "Impostor Metallic", -9000, 0, 1, false, false),
            new TVEPropertyData("_ImpostorOcclusionValue", "Impostor Occlusion", -9000, 0, 1, false, false),
            new TVEPropertyData("_ImpostorSmoothnessValue", "Impostor Smoothness", -9000, 0, 1, false, false),

            new TVEPropertyData("_DetailCategory", "Layer Settings"),

            new TVEPropertyData("_SecondIntensityValue", "Layer Intensity", -9000, 0, 1, false, false),
            new TVEPropertyData("_SecondBakeMode", "Layer Baking", -9000, "Off 0 Bake_Settings_To_Impostors 1", false),

            new TVEPropertyData("_SecondAlbedoTex", "Layer Albedo", null, true),
            new TVEPropertyData("_SecondNormalTex", "Layer Normal", null, false),
            new TVEPropertyData("_SecondShaderTex", "Layer Shader", null, false),

            new TVEPropertyData("_SecondSampleMode", "Layer Sampling", -9000, "Main_UV 0 Extra_UV 1 Planar 2 Triplanar 3 Stochastic 4 Stochastic_Triplanar 5", true),
            new TVEPropertyData("_SecondCoordMode", "Layer UV Mode", -9000, "Tilling_And_Offset 0 Scale_And_Offset 1", false),
            new TVEPropertyData("_SecondCoordValue", "Layer UV Value", new Vector4(-9000, 0,0,0), false),

            new TVEPropertyData("_SecondColorMode", "Layer Color", -9000, "Constant 0 Dual_Colors 1", true),
            new TVEPropertyData("_SecondColor", "Layer Color", new Color(-9000, 0,0,0), true, false),
            new TVEPropertyData("_SecondColorTwo", "Layer ColorB", new Color(-9000, 0,0,0), true, false),
            new TVEPropertyData("_SecondAlphaClipValue", "Layer Alpha", -9000, 0, 1, false, false),
            new TVEPropertyData("_SecondAlbedoValue", "Layer Albedo", -9000, 0, 1, false, false),
            new TVEPropertyData("_SecondNormalValue", "Layer Normal", -9000, -8, 8, false, false),
            new TVEPropertyData("_SecondMetallicValue", "Layer Metallic", -9000, 0, 1, false, false),
            new TVEPropertyData("_SecondOcclusionValue", "Layer Occlusion", -9000, 0, 1, false, false),
            new TVEPropertyData("_SecondMultiRemap", "Layer Multi Mask", new Vector4(-9000, -9000, -9000, -9000), 0, 1, false),
            new TVEPropertyData("_SecondSmoothnessValue", "Layer Smoothness", -9000, 0, 1, false, false),
            
            new TVEPropertyData("_SecondBlendIntensityValue", "Layer Blend Intensity", -9000, 0, 1, false, true),
            new TVEPropertyData("_SecondBlendAlbedoValue", "Layer Blend Albedos", -9000, 0, 1, false, false),
            new TVEPropertyData("_SecondBlendNormalValue", "Layer Blend Normals", -9000, 0, 1, false, false),
            new TVEPropertyData("_SecondBlendShaderValue", "Layer Blend Shaders", -9000, 0, 1, false, false),

            new TVEPropertyData("_SecondMaskTex", "Layer Mask", null, true),

            new TVEPropertyData("_SecondMaskSampleMode", "Mask Sampling", -9000, "Main_UV 0 Extra_UV 1 Planar 2 Triplanar 3", true),
            new TVEPropertyData("_SecondMaskCoordMode", "Mask UV Mode", -9000, "Tilling_And_Offset 0 Scale_And_Offset 1", false),
            new TVEPropertyData("_SecondMaskCoordValue", "Mask UV Value", new Vector4(-9000, 0,0,0), false),

            //new TVEPropertyData("_SecondMaskMode", "Layer TexB Mask", -9000, "Main_Shader_B 0 Layer_Shader_B 1 Layer_Mask_B 2", true),
            new TVEPropertyData("_SecondMaskValue", "Layer TexB Mask", -9000, 0, 1, false, true),
            new TVEPropertyData("_SecondMaskRemap", "Layer TexB Mask", new Vector4(-9000, -9000, -9000, -9000), 0, 1, false),
            new TVEPropertyData("_SecondProjValue", "Layer ProjY Mask", -9000, 0, 1, false, false),
            new TVEPropertyData("_SecondProjRemap", "Layer ProjY Mask", new Vector4(-9000, -9000, -9000, -9000), 0, 1, false),
            new TVEPropertyData("_SecondMeshMode", "Layer Mesh Mask", -9000, "Vertex_R 0 Vertex_G 1 Vertex_B 2 Vertex_A 3", false),
            new TVEPropertyData("_SecondMeshValue", "Layer Mesh Mask", -9000, 0, 1, false, false),
            new TVEPropertyData("_SecondMeshRemap", "Layer Mesh Mask", new Vector4(-9000, -9000, -9000, -9000), 0, 1, false),
            new TVEPropertyData("_SecondBlendRemap", "Layer Blend Mask", new Vector4(-9000, -9000, -9000, -9000), 0, 1, false),

            new TVEPropertyData("_SecondElementMode", "Use Coat Globals / Elements", -9000, true),

            new TVEPropertyData("_DetailCategory", "Detail Settings"),

            new TVEPropertyData("_ThirdIntensityValue", "Detail Intensity", -9000, 0, 1, false, false),
            new TVEPropertyData("_ThirdBakeMode", "Detail Baking", -9000, "Off 0 Bake_Settings_To_Impostors 1", false),

            new TVEPropertyData("_ThirdAlbedoTex", "Detail Albedo", null, true),
            new TVEPropertyData("_ThirdNormalTex", "Detail Normal", null, false),
            new TVEPropertyData("_ThirdShaderTex", "Detail Shader", null, false),

            new TVEPropertyData("_ThirdSampleMode", "Detail Sampling", -9000, "Main_UV 0 Extra_UV 1 Planar 2 Triplanar 3 Stochastic 4 Stochastic_Triplanar 5", true),
            new TVEPropertyData("_ThirdCoordMode", "Detail UV Mode", -9000, "Tilling_And_Offset 0 Scale_And_Offset 1", false),
            new TVEPropertyData("_ThirdCoordValue", "Detail UV Value", new Vector4(-9000, 0,0,0), false),

            new TVEPropertyData("_ThirdColorMode", "Detail Color", -9000, "Constant 0 Dual_Colors 1", true),
            new TVEPropertyData("_ThirdColor", "Detail Color", new Color(-9000, 0,0,0), true, false),
            new TVEPropertyData("_ThirdColorTwo", "Detail ColorB", new Color(-9000, 0,0,0), true, false),
            new TVEPropertyData("_ThirdAlphaClipValue", "Detail Alpha", -9000, 0, 1, false, false),
            new TVEPropertyData("_ThirdAlbedoValue", "Detail Albedo", -9000, 0, 1, false, false),
            new TVEPropertyData("_ThirdNormalValue", "Detail Normal", -9000, -8, 8, false, false),
            new TVEPropertyData("_ThirdMetallicValue", "Detail Metallic", -9000, 0, 1, false, false),
            new TVEPropertyData("_ThirdOcclusionValue", "Detail Occlusion", -9000, 0, 1, false, false),
            new TVEPropertyData("_ThirdMultiRemap", "Detail Multi Mask", new Vector4(-9000, -9000, -9000, -9000), 0, 1, false),
            new TVEPropertyData("_ThirdSmoothnessValue", "Detail Smoothness", -9000, 0, 1, false, false),

            new TVEPropertyData("_ThirdBlendIntensityValue", "Detail Blend Intensity", -9000, 0, 1, false, true),
            new TVEPropertyData("_ThirdBlendAlbedoValue", "Detail Blend Albedos", -9000, 0, 1, false, false),
            new TVEPropertyData("_ThirdBlendNormalValue", "Detail Blend Normals", -9000, 0, 1, false, false),
            new TVEPropertyData("_ThirdBlendShaderValue", "Detail Blend Shaders", -9000, 0, 1, false, false),

            new TVEPropertyData("_ThirdMaskTex", "Detail Mask", null, true),

            new TVEPropertyData("_ThirdMaskSampleMode", "Mask Sampling", -9000, "Main_UV 0 Extra_UV 1 Planar 2 Triplanar 3", true),
            new TVEPropertyData("_ThirdMaskCoordMode", "Mask UV Mode", -9000, "Tilling_And_Offset 0 Scale_And_Offset 1", false),
            new TVEPropertyData("_ThirdMaskCoordValue", "Mask UV Value", new Vector4(-9000, 0,0,0), false),

            new TVEPropertyData("_ThirdMaskValue", "Detail TexG Mask", -9000, 0, 1, false, true),
            new TVEPropertyData("_ThirdMaskRemap", "Detail TexG Mask", new Vector4(-9000, -9000, -9000, -9000), 0, 1, false),
            new TVEPropertyData("_ThirdProjValue", "Detail ProjY Mask", -9000, 0, 1, false, false),
            new TVEPropertyData("_ThirdProjRemap", "Detail ProjY Mask", new Vector4(-9000, -9000, -9000, -9000), 0, 1, false),
            new TVEPropertyData("_ThirdMeshMode", "Detail Mesh Mask", -9000, "Vertex_R 0 Vertex_G 1 Vertex_B 2 Vertex_A 3", false),
            new TVEPropertyData("_ThirdMeshValue", "Detail Mesh Mask", -9000, 0, 1, false, false),
            new TVEPropertyData("_ThirdMeshRemap", "Detail Mesh Mask", new Vector4(-9000, -9000, -9000, -9000), 0, 1, false),
            new TVEPropertyData("_ThirdBlendRemap", "Detail Blend Mask", new Vector4(-9000, -9000, -9000, -9000), 0, 1, false),

            new TVEPropertyData("_ThirdElementMode", "Use Coat Globals / Elements", -9000, true),
        };

        List<TVEPropertyData> coloringDataGlobal = new List<TVEPropertyData>
        {
            new TVEPropertyData("_GlobalCategory", "Global Settings"),

            new TVEPropertyData("_GlobalPaintLayerValue", "Global Paint Layer", -9000, "Paint Layers", "Default 0 Layer_1 1 Layer_2 2 Layer_3 3 Layer_4 4 Layer_5 5 Layer_6 6 Layer_7 8 Layer_8 8", false),
            new TVEPropertyData("_GlobalPaintPivotValue", "Global Paint Pivots", -9000, 0, 1, false, false),
        };

        List<TVEPropertyData> coloringData = new List<TVEPropertyData>
        {
            new TVEPropertyData("_OcclusionCategory", "Occlusion Settings"),

            new TVEPropertyData("_OcclusionIntensityValue", "Occlusion Intensity", -9000, 0, 1, false, false),
            new TVEPropertyData("_OcclusionBakeMode", "Occlusion Baking", -9000, "Keep_Dynamic_On_Impostors 0 Bake_Settings_To_Impostors 1", false),
            new TVEPropertyData("_OcclusionColorOne", "Occlusion ColorA", new Color(-9000, 0,0,0), true, false),
            new TVEPropertyData("_OcclusionColorTwo", "Occlusion ColorB", new Color(-9000, 0,0,0), true, false),

            new TVEPropertyData("_OcclusionMeshMode", "Occlusion Mesh Mask", -9000, "Vertex_R 0 Vertex_G 1 Vertex_B 2 Vertex_A 3", true),
            new TVEPropertyData("_OcclusionMeshRemap", "Occlusion Mesh Mask", new Vector4(-9000, 0, 0, -9000), 0, 1, false),

            new TVEPropertyData("_GradientCategory", "Gradient Settings"),

            new TVEPropertyData("_GradientIntensityValue", "Gradient Intensity", -9000, 0, 1, false, false),
            new TVEPropertyData("_GradientBakeMode", "Gradient Baking", -9000, "Keep_Dynamic_On_Impostors 0 Bake_Settings_To_Impostors 1", false),
            new TVEPropertyData("_GradientColorOne", "Gradient ColorA", new Color(-9000, 0,0,0), true, false),
            new TVEPropertyData("_GradientColorTwo", "Gradient ColorB", new Color(-9000, 0,0,0), true, false),

            new TVEPropertyData("_GradientMultiValue", "Gradient Multi Mask", -9000, 0, 1, false, true),
            new TVEPropertyData("_GradientMeshMode", "Gradient Mesh Mask", -9000, "Vertex_R 0 Vertex_G 1 Vertex_B 2 Vertex_A 3", false),
            new TVEPropertyData("_GradientMeshRemap", "Gradient Mesh Mask", new Vector4(-9000, 0, 0, -9000), 0, 1, false),

            new TVEPropertyData("_VariationCategory", "Variation Settings"),

            new TVEPropertyData("_VariationIntensityValue", "Variation Intensity", -9000, 0, 1, false, false),
            new TVEPropertyData("_VariationColorOne", "Variation ColorA", new Color(-9000, 0,0,0), true, false),
            new TVEPropertyData("_VariationColorTwo", "Variation ColorB", new Color(-9000, 0,0,0), true, false),

            new TVEPropertyData("_VariationMultiValue", "Variation Multi Mask", -9000, 0, 1, false, true),
            new TVEPropertyData("_VariationNoiseValue", "Variation Noise Mask", -9000, 0, 1, false, false),
            new TVEPropertyData("_VariationNoiseRemap", "Variation Noise Mask", new Vector4(-9000, 0, 0, -9000), 0, 1, false),
            new TVEPropertyData("_VariationNoiseTillingValue", "Variation Noise Tilling", -9000, 0, 100, false, false),
            new TVEPropertyData("_VariationNoisePivotValue", "Variation Noise Pivots", -9000, 0, 1, false, false),

            new TVEPropertyData("_TintingCategory", "Tinting Settings"),

            new TVEPropertyData("_TintingIntensityValue", "Tinting Intensity", -9000, 0, 1, false, false),
            new TVEPropertyData("_TintingBakeMode", "Tinting Baking", -9000, "Keep_Dynamic_On_Impostors 0 Bake_Settings_To_Impostors 1", false),
            new TVEPropertyData("_TintingGrayValue", "Tinting Gray", -9000, 0, 1, false, false),
            new TVEPropertyData("_TintingColor", "Tinting Color", new Color(-9000, 0,0,0), true, false),

            new TVEPropertyData("_TintingMultiValue", "Tinting Multi Mask", -9000, 0, 1, false, true),
            new TVEPropertyData("_TintingLumaValue", "Tinting Luma Mask", -9000, 0, 1, false, false),
            new TVEPropertyData("_TintingLumaRemap", "Tinting Luma Mask", new Vector4(-9000, 0, 0, -9000), 0, 1, false),
            new TVEPropertyData("_TintingMeshValue", "Tinting Mesh Mask", -9000, 0, 1, false, false),
            new TVEPropertyData("_TintingMeshMode", "Tinting Mesh Mask", -9000, "Vertex_R 0 Vertex_G 1 Vertex_B 2 Vertex_A 3", false),
            new TVEPropertyData("_TintingMeshRemap", "Tinting Mesh Mask", new Vector4(-9000, 0, 0, -9000), 0, 1, false),
            new TVEPropertyData("_TintingNoiseRemap", "Tinting Noise Mask", new Vector4(-9000, 0, 0, -9000), 0, 1, false),
            new TVEPropertyData("_TintingNoiseTillingValue", "Tinting Noise Tilling", -9000, 0, 100, false, false),
            new TVEPropertyData("_TintingBlendRemap", "Tinting Blend Mask", new Vector4(-9000, 0, 0, -9000), 0, 1, false),

            new TVEPropertyData("_TintingElementMode", "Use Paint Globals / Elements", -9000, true),
        };

        List<TVEPropertyData> weatherDataGlobal = new List<TVEPropertyData>
        {
            new TVEPropertyData("_GlobalCategory", "Global Settings"),

            new TVEPropertyData("_GlobalAtmoLayerValue", "Global Atmo Layer", -9000, "Atmo Layers", "Default 0 Layer_1 1 Layer_2 2 Layer_3 3 Layer_4 4 Layer_5 5 Layer_6 6 Layer_7 8 Layer_8 8", false),
            new TVEPropertyData("_GlobalAtmoPivotValue", "Global Atmo Pivots", -9000, 0, 1, false, false),
        };

        List<TVEPropertyData> weatherData = new List<TVEPropertyData>
        {
            new TVEPropertyData("_DrynessCategory", "Dryness Settings"),

            new TVEPropertyData("_DrynessIntensityValue", "Dryness Intensity", -9000, 0, 1, false, false),
            new TVEPropertyData("_DrynessBakeMode", "Dryness Baking", -9000, "Keep_Dynamic_On_Impostors 0 Bake_Settings_To_Impostors 1", false),
            new TVEPropertyData("_DrynessGrayValue", "Dryness Gray", -9000, 0, 1, false, false),
            new TVEPropertyData("_DrynessShiftValue", "Dryness Shift", -9000, 0, 1, false, false),
            new TVEPropertyData("_DrynessColor", "Dryness Color", new Color(-9000, 0,0,0), true, false),

            new TVEPropertyData("_DrynessMultiValue", "Dryness Multi Mask", -9000, 0, 1, false, true),
            new TVEPropertyData("_DrynessLumaValue", "Dryness Luma Mask", -9000, 0, 1, false, false),
            new TVEPropertyData("_DrynessLumaRemap", "Dryness Luma Mask", new Vector4(-9000, 0, 0, -9000), 0, 1, false),
            new TVEPropertyData("_DrynessMeshValue", "Dryness Mesh Mask", -9000, 0, 1, false, false),
            new TVEPropertyData("_DrynessMeshMode", "Dryness Mesh Mask", -9000, "Vertex_R 0 Vertex_G 1 Vertex_B 2 Vertex_A 3", false),
            new TVEPropertyData("_DrynessMeshRemap", "Dryness Mesh Mask", new Vector4(-9000, 0, 0, -9000), 0, 1, false),
            new TVEPropertyData("_DrynessNoiseRemap", "Dryness Noise Mask", new Vector4(-9000, 0, 0, -9000), 0, 1, false),
            new TVEPropertyData("_DrynessNoiseTillingValue", "Dryness Noise Tilling", -9000, 0, 100, false, false),
            new TVEPropertyData("_DrynessBlendRemap", "Dryness Blend Mask", new Vector4(-9000, 0, 0, -9000), 0, 1, false),
            
            new TVEPropertyData("_DrynessElementMode", "Use Atmo Globals / Elements", -9000, true),

            new TVEPropertyData("_OverlayCategory", "Overlay Settings"),

            new TVEPropertyData("_OverlayIntensityValue", "Overlay Intensity", -9000, 0, 1, false, false),
            new TVEPropertyData("_OverlayBakeMode", "Overlay Baking", -9000, "Keep_Dynamic_On_Impostors 0 Bake_Settings_To_Impostors 1", false),
            new TVEPropertyData("_OverlayTextureMode", "Overlay Maps", -9000, "Off 0 On 1", false),

            new TVEPropertyData("_OverlayAlbedoTex", "Overlay Albedo", null, true),
            new TVEPropertyData("_OverlayNormalTex", "Overlay Normal", null, false),

            new TVEPropertyData("_OverlaySampleMode", "Overlay Sampling", -9000, "Planar 0 Triplanar 1 Stochastic 2 Stochastic_Triplanar 3", true),
            new TVEPropertyData("_OverlayCoordMode", "Overlay UV Mode", -9000, "Tilling_and_Offset 0 Scale_and_Offset 1", false),
            new TVEPropertyData("_OverlayCoordValue", "Overlay UV Value", new Vector4(-9000, 0,0,0), false),

            new TVEPropertyData("_OverlayColor", "Overlay Color", new Color(-9000, 0,0,0), true, true),
            new TVEPropertyData("_OverlayNormalValue", "Overlay Normal", -9000, -8, 8, false, false),
            new TVEPropertyData("_OverlaySubsurfaceValue", "Overlay Subsurface", -9000, 0, 1, false, false),
            new TVEPropertyData("_OverlaySmoothnessValue", "Overlay Smoothness", -9000, 0, 1, false, false),

            new TVEPropertyData("_OverlayGlitterTex", "Overlay Glitter", null, true),

            new TVEPropertyData("_OverlayGlitterIntensityValue", "Overlay Glitter Intensity", -9000, 0, 1, false, true),
            new TVEPropertyData("_OverlayGlitterColor", "Overlay Glitter Color", new Color(-9000, 0,0,0), true, false),
            new TVEPropertyData("_OverlayGlitterTillingValue", "Overlay Glitter Tilling", -9000, 0, 10, false, false),
            new TVEPropertyData("_OverlayGlitterDistValue", "Overlay Glitter Limit", -9000, 0, 200, false, false),

            new TVEPropertyData("_OverlayMaskTex", "Overlay Mask", null, true),

            new TVEPropertyData("_OverlayMaskSampleMode", "Mask Sampling", -9000, "Main_UV 0 Extra_UV", true),
            new TVEPropertyData("_OverlayMaskCoordMode", "Mask UV Mode", -9000, "Tilling_And_Offset 0 Scale_And_Offset 1", false),
            new TVEPropertyData("_OverlayMaskCoordValue", "Mask UV Value", new Vector4(-9000, 0,0,0), false),

            new TVEPropertyData("_OverlayMaskValue", "Overlay TexB Mask", -9000, 0, 1, false, true),
            new TVEPropertyData("_OverlayMaskRemap", "Overlay TexB Mask", new Vector4(-9000, 0, 0, -9000), 0, 1, false),
            new TVEPropertyData("_OverlayLumaValue", "Overlay Luma Mask", -9000, 0, 1, false, false),
            new TVEPropertyData("_OverlayLumaRemap", "Overlay Luma Mask", new Vector4(-9000, 0, 0, -9000), 0, 1, false),
            new TVEPropertyData("_OverlayProjValue", "Overlay ProjY Mask", -9000, 0, 1, false, false),
            new TVEPropertyData("_OverlayProjRemap", "Overlay ProjY Mask", new Vector4(-9000, 0, 0, -9000), 0, 1, false),
            new TVEPropertyData("_OverlayMeshValue", "Overlay Mesh Mask", -9000, 0, 1, false, false),
            new TVEPropertyData("_OverlayMeshMode", "Overlay Mesh Mask", -9000, "Vertex_R 0 Vertex_G 1 Vertex_B 2 Vertex_A 3", false),
            new TVEPropertyData("_OverlayMeshRemap", "Overlay Mesh Mask", new Vector4(-9000, 0, 0, -9000), 0, 1, false),
            new TVEPropertyData("_OverlayNoiseRemap", "Overlay Noise Mask", new Vector4(-9000, 0, 0, -9000), 0, 1, false),
            new TVEPropertyData("_OverlayNoiseTillingValue", "Overlay Noise Tilling", -9000, 0, 100, false, false),
            new TVEPropertyData("_OverlayBlendRemap", "Overlay Blend Mask", new Vector4(-9000, 0, 0, -9000), 0, 1, false),

            new TVEPropertyData("_OverlayElementMode", "Use Atmo Globals / Elements", -9000, true),

            new TVEPropertyData("_WetnessCategory", "Wetness Settings"),

            new TVEPropertyData("_WetnessIntensityValue", "Wetness Intensity", -9000, 0, 1, false, false),
            new TVEPropertyData("_WetnessBakeMode", "Wetness Baking", -9000, "Keep_Dynamic_On_Impostors 0 Bake_Settings_To_Impostors 1", false),
            new TVEPropertyData("_WetnessContrastValue", "Wetness Contrast", -9000, 0, 1, false, false),
            new TVEPropertyData("_WetnessSmoothnessValue", "Wetness Smoothness", -9000, 0, 1, false, false),

            new TVEPropertyData("_WetnessMeshValue", "Wetness Mesh Mask", -9000, 0, 1, false, false),
            new TVEPropertyData("_WetnessMeshMode", "Wetness Mesh Mask", -9000, "Vertex_R 0 Vertex_G 1 Vertex_B 2 Vertex_A 3", false),
            new TVEPropertyData("_WetnessMeshRemap", "Wetness Mesh Mask", new Vector4(-9000, -9000, -9000, -9000), 0, 1, false),

            new TVEPropertyData("_WetnessWaterIntensityValue", "Wetness Water Intensity", -9000, 0, 1, false, true),
            new TVEPropertyData("_WetnessWaterColor", "Wetness Water Color", new Color(-9000, 0,0,0), true, false),
            new TVEPropertyData("_WetnessWaterBaseValue", "Wetness Water Base Mask", -9000, 0, 1, false, false),
            new TVEPropertyData("_WetnessWaterMeshValue", "Wetness Water Mesh Mask", -9000, 0, 1, false, false),
            new TVEPropertyData("_WetnessWaterMeshMode", "Wetness Water Mesh Mask", -9000, "Vertex_R 0 Vertex_G 1 Vertex_B 2 Vertex_A 3", false),
            new TVEPropertyData("_WetnessWaterMeshRemap", "Wetness Water Mesh Mask", new Vector4(-9000, -9000, -9000, -9000), 0, 1, false),
            new TVEPropertyData("_WetnessWaterBlendRemap", "Wetness Water Blend Mask", new Vector4(-9000, 0, 0, -9000), 0, 1, false),

            new TVEPropertyData("_WetnessDropsTex", "Wetness Drops", null, true),

            new TVEPropertyData("_WetnessDropsIntensityValue", "Wetness Drops Intensity", -9000, 0, 1, false, true),
            new TVEPropertyData("_WetnessDropsNormalValue", "Wetness Drops Normal", -9000, -8, 8, false, false),
            new TVEPropertyData("_WetnessDropsTillingValue", "Wetness Drops Tilling", -9000, 0, 10, false, false),
            new TVEPropertyData("_WetnessDropsDistValue", "Wetness Drops Limit", -9000, 0, 40, false, false),
            new TVEPropertyData("_WetnessDropsMeshValue", "Wetness Drops Mesh Mask", -9000, 0, 1, false, false),
            new TVEPropertyData("_WetnessDropsMeshMode", "Wetness Drops Mesh Mask", -9000, "Vertex_R 0 Vertex_G 1 Vertex_B 2 Vertex_A 3", false),
            new TVEPropertyData("_WetnessDropsMeshRemap", "Wetness Drops Mesh Mask", new Vector4(-9000, -9000, -9000, -9000), 0, 1, false),
            //new TVEPropertyData("_WetnessDropsProjRemap", "Wetness Drops ProjY Mask", new Vector4(-9000, 0, 0, -9000), 0, 1, false),

            new TVEPropertyData("_WetnessElementMode", "Use Atmo Globals / Elements", -9000, true),
        };

        List<TVEPropertyData> clippingDataGlobal = new List<TVEPropertyData>
        {
            new TVEPropertyData("_GlobalCategory", "Global Settings"),

            new TVEPropertyData("_GlobalFadeLayerValue", "Global Fade Layer", -9000, "Fade Layers", "Default 0 Layer_1 1 Layer_2 2 Layer_3 3 Layer_4 4 Layer_5 5 Layer_6 6 Layer_7 8 Layer_8 8", false),
            new TVEPropertyData("_GlobalFadePivotValue", "Global Fade Pivots", -9000, 0, 1, false, false),
        };

        List<TVEPropertyData> clippingData = new List<TVEPropertyData>
        {
            new TVEPropertyData("_CutoutCategory", "Cutout Settings"),

            new TVEPropertyData("_CutoutIntensityValue", "Cutout Intensity", -9000, 0, 1, false, false),
            new TVEPropertyData("_CutoutShadowMode", "Cutout Shadow", -9000, "Off 0 Affect_Shadow_Pass 1", false),

            new TVEPropertyData("_CutoutMultiValue", "Cutout Multi Mask", -9000, 0, 1, false, true),
            new TVEPropertyData("_CutoutAlphaValue", "Cutout Alpha Mask", -9000, 0, 1, false, false),
            new TVEPropertyData("_CutoutMeshValue", "Cutout Mesh Mask", -9000, 0, 1, false, false),
            new TVEPropertyData("_CutoutMeshMode", "Cutout Mesh Mask", -9000, "Vertex_R 0 Vertex_G 1 Vertex_B 2 Vertex_A 3", false),
            new TVEPropertyData("_CutoutMeshRemap", "Cutout Mesh Mask", new Vector4(-9000, 0, 0, -9000), 0, 1, false),
            new TVEPropertyData("_CutoutNoiseValue", "Cutout Noise Mask", -9000, 0, 1, false, false),
            new TVEPropertyData("_CutoutNoiseTillingValue", "Cutout Noise Tilling", -9000, 0, 100, false, false),

            new TVEPropertyData("_CutoutElementMode", "Use Atmo Globals / Elements", -9000, true),

            new TVEPropertyData("_DitherCategory", "Dither Settings"),

            new TVEPropertyData("_DitherConstantValue", "Dither Constant", -9000, 0, 1, false, false),
            new TVEPropertyData("_DitherDistanceValue", "Dither Distance", -9000, 0, 1, false, false),
            new TVEPropertyData("_DitherDistanceMinValue", "Dither Distance Start", -9000, 0, 1000, false, false),
            new TVEPropertyData("_DitherDistanceMaxValue", "Dither Distance Limit", -9000, 0, 1000, false, false),
            new TVEPropertyData("_DitherProximityValue", "Dither Proximity", -9000, 0, 1, false, false),
            new TVEPropertyData("_DitherProximityMinValue", "Dither Proximity Start", -9000, 0, 40, false, false),
            new TVEPropertyData("_DitherProximityMaxValue", "Dither Proximity Limit", -9000, 0, 40, false, false),
            new TVEPropertyData("_DitherGlancingValue", "Dither Glancing", -9000, 0, 1, false, false),
            new TVEPropertyData("_DitherGlancingAngleValue", "Dither Glancing Angle", -9000, 0, 8, false, false),
            new TVEPropertyData("_DitherShadowMode", "Dither Shadow", -9000, "Off 0 Affect_Shadow_Pass 1", false),

            new TVEPropertyData("_DitherMultiValue", "Dither Multi Mask", -9000, 0, 1, false, true),
            new TVEPropertyData("_DitherNoiseMode", "Dither Noise Mode", -9000, "Noise_3D 0 Noise_Screen_Space 1", false),
            new TVEPropertyData("_DitherNoiseTillingValue", "Dither Noise Tilling", -9000, 0, 100, false, false),
        };

        List<TVEPropertyData> glowingDataGlobal = new List<TVEPropertyData>
        {
            new TVEPropertyData("_GlobalCategory", "Global Settings"),

            new TVEPropertyData("_GlobalGlowLayerValue", "Global Glow Layer", -9000, "Atmo Layers", "Default 0 Layer_1 1 Layer_2 2 Layer_3 3 Layer_4 4 Layer_5 5 Layer_6 6 Layer_7 8 Layer_8 8", false),
            new TVEPropertyData("_GlobalGlowPivotValue", "Global Glow Pivots", -9000, 0, 1, false, false),
        };

        List<TVEPropertyData> glowingData = new List<TVEPropertyData>
        {
            new TVEPropertyData("_EmissiveCategory", "Emissive Settings"),

            new TVEPropertyData("_EmissiveIntensityValue", "Emissive Intensity", -9000, 0, 1, false, false),
            new TVEPropertyData("_EmissiveFlagMode", "Emissive GI Mode", -9000, "None 0 Any 1 Baked 2 Realtime 3", false),
            new TVEPropertyData("_EmissiveColorMode", "Emissive Color", -9000, "Constant 0 Multiply_With_Base_Albedo 1", false),
            new TVEPropertyData("_EmissiveColor", "Emissive Color", new Color(-9000, 0,0,0), true, false),
            new TVEPropertyData("_EmissivePowerMode", "Emissive Value", -9000, "Nits 0 EV100 1", false),
            new TVEPropertyData("_EmissivePowerValue", "Emissive Power", -9000, false, false),
            new TVEPropertyData("_EmissiveExposureValue", "Emissive Weight", -9000, 0, 1, false, false),

            new TVEPropertyData("_EmissiveMaskTex", "Emissive Mask", null, true),

            new TVEPropertyData("_EmissiveSampleMode", "Emissive Sampling", -9000, "Main_UV 0 Extra_UV 1 Planar 2 Triplanar 3", true),
            new TVEPropertyData("_EmissiveCoordMode", "Emissive UV Mode", -9000, "Tilling_And_Offset 0 Scale_And_Offset 1", false),
            new TVEPropertyData("_EmissiveCoordValue", "Emissive UV Value", new Vector4(-9000, 0,0,0), false),

            new TVEPropertyData("_EmissiveMaskValue", "Emissive TexR Mask", -9000, 0, 1, false, true),
            new TVEPropertyData("_EmissiveMaskRemap", "Emissive TexR Mask", new Vector4(-9000, -9000, -9000, -9000), 0, 1, false),
            new TVEPropertyData("_EmissiveMeshValue", "Emissive Mesh Mask", -9000, 0, 1, false, false),
            new TVEPropertyData("_EmissiveMeshMode", "Emissive Mesh Mask", -9000, "Vertex_R 0 Vertex_G 1 Vertex_B 2 Vertex_A 3", false),
            new TVEPropertyData("_EmissiveMeshRemap", "Emissive Mesh Mask", new Vector4(-9000, -9000, -9000, -9000), 0, 1, false),

            new TVEPropertyData("_EmissiveElementMode", "Use Glow Globals / Elements", -9000, true),

            new TVEPropertyData("_SubsurfaceCategory", "Subsurface Settings"),

            new TVEPropertyData("_SubsurfaceHDRPInfo", "When using HDRP, the Subsurface Color and Power are fake effects used for artistic control. For physically correct subsurface scattering the Power slider need to be set to 0.", 0, 10, MessageType.Info),
            new TVEPropertyData("_SubsurfaceAproxInfo", "When using Standard Lit shaders, the Subsurface feature is an approximation effect designed to work in deferred rendering path.", 0, 10, MessageType.Info),

            new TVEPropertyData("_SubsurfaceIntensityValue", "Subsurface Intensity", -9000, 0, 1, false, false),
            new TVEPropertyData("_SubsurfaceColor", "Subsurface Color", new Color(-9000, 0,0,0), true, false),
            new TVEPropertyData("_SubsurfaceScatteringValue", "Subsurface Power", -9000, 0, 16, false, false),
            new TVEPropertyData("_SubsurfaceAngleValue", "Subsurface Angle", -9000, 1, 16, false, false),
            new TVEPropertyData("_SubsurfaceNormalValue", "Subsurface Normal", -9000, 0, 1, false, false),
            new TVEPropertyData("_SubsurfaceDirectValue", "Subsurface Direct", -9000, 0, 1, false, false),
            new TVEPropertyData("_SubsurfaceAmbientValue", "Subsurface Ambient", -9000, 0, 1, false, false),
            new TVEPropertyData("_SubsurfaceShadowValue", "Subsurface Shadow", -9000, 0, 1, false, false),
            new TVEPropertyData("_SubsurfaceThicknessValue", "Subsurface Thickness", -9000, 0, 16, false, false),

            new TVEPropertyData("_SubsurfaceMultiValue", "Subsurface Multi Mask", -9000, 0, 1, false, true),

            new TVEPropertyData("_SubsurfaceElementMode", "Use Glow Globals / Elements", -9000, true),
        };

        List<TVEPropertyData> transformDataGlobal = new List<TVEPropertyData>
        {
            new TVEPropertyData("_GlobalCategory", "Global Settings"),

            new TVEPropertyData("_GlobalFormLayerValue", "Global Form Layer", -9000, "Form Layers", "Default 0 Layer_1 1 Layer_2 2 Layer_3 3 Layer_4 4 Layer_5 5 Layer_6 6 Layer_7 8 Layer_8 8", false),
            new TVEPropertyData("_GlobalFormPivotValue", "Global Form Pivots", -9000, 0, 1, false, false),
        };

        List<TVEPropertyData> transformData = new List<TVEPropertyData>
        {
            new TVEPropertyData("_PerspectiveCategory", "Perspective Settings"),

            new TVEPropertyData("_PerspectiveIntensityValue", "Perspective Intensity", -9000, 0, 4, false, false),
            new TVEPropertyData("_PerspectiveAngleValue", "Perspective Angle Mask", -9000, 0, 8, false, false),

            new TVEPropertyData("_SizeFadeCategory", "Size Fade Settings"),

            new TVEPropertyData("_SizeFadeIntensityValue", "Size Fade Intensity", -9000, 0, 1, false, false),
            new TVEPropertyData("_SizeFadeScaleMode", "Size Fade Mode", -9000, "NULL", "All_Axis 0 Y_Axis 1", false),
            new TVEPropertyData("_SizeFadeScaleValue", "Size Fade Scale", -9000, 0, 1, false, false),
            new TVEPropertyData("_SizeFadeDistMinValue", "Size Fade Start", -9000, 0, 1000, false, false),
            new TVEPropertyData("_SizeFadeDistMaxValue", "Size Fade Limit", -9000, 0, 1000, false, false),

            new TVEPropertyData("_SizeFadeElementMode", "Use Form Globals / Elements", -9000, true),

            new TVEPropertyData("_BlanketCategory", "Perspective Settings"),

            new TVEPropertyData("_BlanketInfo", "The Conform features require elements to work. Use Form Surface or Form Height and Form Normal elements for conforming and aligning the objects to terrain surfaces. Please note, the conform effect is only visual and it does not affect the object collision and bounds.", 0, 10, MessageType.Info),

            new TVEPropertyData("_BlanketConformValue", "Blanket Conform", -9000, 0, 1, false, false),
            new TVEPropertyData("_BlanketConformOffsetMode", "Blanket Conform Mode", -9000, "NULL", "Freeform_Object_Position 0 Lock_Position_With_Conform 1", false),
            new TVEPropertyData("_BlanketConformOffsetValue", "Blanket Conform Offset", -9000, false, false),
            new TVEPropertyData("_BlanketOrientationValue", "Blanket Orientation", -9000, 0, 1, false, false),
        };

        List<TVEPropertyData> objectData = new List<TVEPropertyData>
        {
            new TVEPropertyData("_ObjectCategory", "Object Settings"),

            new TVEPropertyData("_ObjectBoundsInfo", "Use the Object Height and Radius to remap the procedural height and spherical masks when used for motion.", 0, 10, MessageType.Info),
            new TVEPropertyData("_ObjectModelMode", "Object Model Mode", -9000, "NULL", "Legacy 0 Standard 1", false),
            new TVEPropertyData("_ObjectPivotMode", "Object Pivots Mode", -9000, "NULL", "Off 0 Baked 1 Procedural 2", false),
            new TVEPropertyData("_ObjectPhaseMode", "Object Phase Mask", -9000, "Vertex_R 0 Vertex_G 1 Vertex_B 2 Vertex_A 3", false),
            new TVEPropertyData("_ObjectHeightValue", "Object Height Value", -9000, 0, 40, false, false),
            new TVEPropertyData("_ObjectRadiusValue", "Object Radius Value", -9000, 0, 40, false, false),
        };

        List<TVEPropertyData> motionDataGlobal = new List<TVEPropertyData>
        {
            new TVEPropertyData("_GlobalCategory", "Global Settings"),

            new TVEPropertyData("_GlobalWindLayerValue", "Global Wind Layer", -9000, "Flow Layers", "Default 0 Layer_1 1 Layer_2 2 Layer_3 3 Layer_4 4 Layer_5 5 Layer_6 6 Layer_7 8 Layer_8 8", false),
            new TVEPropertyData("_GlobalPushLayerValue", "Global Push Layer", -9000, "Push Layers", "Default 0 Layer_1 1 Layer_2 2 Layer_3 3 Layer_4 4 Layer_5 5 Layer_6 6 Layer_7 8 Layer_8 8", false),
        };

        List<TVEPropertyData> motionData = new List<TVEPropertyData>
        {
            new TVEPropertyData("_MotionWindCategory", "Motion Settings"),

            new TVEPropertyData("_MotionWindOffInfo", "Use the disabled wind mode when the wind flow is controlled exclusivly by global elements.", 0, 10, MessageType.Info),
            new TVEPropertyData("_MotionWindOptimizedInfo", "The Optimized wind mode uses the pre-computed Motion Texture RT which is only calculated once and reused for all materials.", 0, 10, MessageType.Info),
            new TVEPropertyData("_MotionWindAdvancedInfo", "The Advanced wind mode allows the setting of the wind Noise, Tilling, and Speed per motion layer for advanced control. Use the Noise value to randomize the wind direction.", 0, 10, MessageType.Info),

            new TVEPropertyData("_MotionNoiseTex", "Motion Texture", null, false),
            new TVEPropertyData("_MotionNoiseTexRT", "Motion Texture RT", null, false),
            //new TVEPropertyData("_MotionFlutterTexRT", "Motion Flutter RT", null, false),

            new TVEPropertyData("_MotionHighlightValue", "Motion Waves Intensity", -9000, 0, 1, false, true),
            new TVEPropertyData("_MotionHighlightColor", "Motion Waves Color", new Color(-9000, 0,0,0), true, false),
            new TVEPropertyData("_MotionHighlightDelayValue", "Motion Waves Wind Drag", -9000, 0, 1, false, false),

            new TVEPropertyData("_MotionBaseIntensityValue", "Motion LayerA Intensity", -9000, 0, 10, false, true),
            new TVEPropertyData("_MotionBasePivotValue", "Motion LayerA Pivots", -9000, 0, 1, false, false),
            new TVEPropertyData("_MotionBasePhaseValue", "Motion LayerA Phase", -9000, 0, 1, false, false),
            new TVEPropertyData("_MotionBaseNoiseValue", "Motion LayerA Noise", -9000, 0, 1, false, false),
            new TVEPropertyData("_MotionBaseTillingValue", "Motion LayerA Tilling", -9000, 0, 100, false, false),
            new TVEPropertyData("_MotionBaseSpeedValue", "Motion LayerA Speed", -9000, 0, 50, false, false),
            new TVEPropertyData("_MotionBaseDelayValue", "Motion LayerA Wind Delay", -9000, 0, 1, false, false),
            new TVEPropertyData("_MotionBaseMaskMode", "Motion LayerA Anim Mask", -9000, "Vertex_R 0 Vertex_G 1 Vertex_B 2 Vertex_A 3 Height 4 Sphere 5", false),
            new TVEPropertyData("_MotionBaseMaskRemap", "Motion LayerA Anim Mask", new Vector4(-9000, 0, 0, -9000), 0, 1, false),

            new TVEPropertyData("_MotionSmallIntensityValue", "Motion LayerB Intensity", -9000, 0, 8, false, true),
            new TVEPropertyData("_MotionSmallPivotValue", "Motion LayerB Pivots", -9000, 0, 1, false, false),
            new TVEPropertyData("_MotionSmallPhaseValue", "Motion LayerB Phase", -9000, 0, 1, false, false),
            new TVEPropertyData("_MotionSmallNoiseValue", "Motion LayerB Noise", -9000, 0, 1, false, false),
            new TVEPropertyData("_MotionSmallTillingValue", "Motion LayerB Tilling", -9000, 0, 100, false, false),
            new TVEPropertyData("_MotionSmallSpeedValue", "Motion LayerB Speed", -9000, 0, 50, false, false),
            new TVEPropertyData("_MotionSmallDelayValue", "Motion LayerB Wind Delay", -9000, 0, 1, false, false),
            new TVEPropertyData("_MotionSmallMaskMode", "Motion LayerB Anim Mask", -9000, "Vertex_R 0 Vertex_G 1 Vertex_B 2 Vertex_A 3 Height 4 Sphere 5", false),
            new TVEPropertyData("_MotionSmallMaskRemap", "Motion LayerB Anim Mask", new Vector4(-9000, 0, 0, -9000), 0, 1, false),

            new TVEPropertyData("_MotionTinyIntensityValue", "Motion Flutter Intensity", -9000, 0, 8, false, true),
            new TVEPropertyData("_MotionTinyTillingValue", "Motion Flutter Tilling", -9000, 0, 100, false, false),
            new TVEPropertyData("_MotionTinySpeedValue", "Motion Flutter Speed", -9000, 0, 50, false, false),
            new TVEPropertyData("_MotionTinyPhaseValue", "Motion Flutter Phase", -9000, 0, 1, false, false),
            //new TVEPropertyData("_MotionTinyDelayValue", "Motion Flutter Anim Delay", -9000, 0, 1, false, false),
            new TVEPropertyData("_MotionTinyMaskMode", "Motion Flutter Anim Mask", -9000, "Vertex_R 0 Vertex_G 1 Vertex_B 2 Vertex_A 3 Height 4 Sphere 5", false),
            new TVEPropertyData("_MotionTinyMaskRemap", "Motion Flutter Anim Mask", new Vector4(-9000, 0, 0, -9000), 0, 1, false),

            new TVEPropertyData("_MotionIntensityValue", "Motion Wind Intensity", -9000, 0, 1, false, true),
            new TVEPropertyData("_MotionWindMode", "Motion Wind Mode", -9000, "Off 0 Optimized 1 Advanced 2", false),
            //new TVEPropertyData("_MotionDelayValue", "Motion Wind Delay", -9000, 0, 1, false, false),
            new TVEPropertyData("_MotionTillingValue", "Motion Wind Tilling", -9000, 0, 100, false, false),
            //new TVEPropertyData("_MotionSpeedValue", "Motion Wind Speed", -9000, 0, 40, false, false),
            new TVEPropertyData("_MotionDistValue", "Motion Wind Waves Limit", -9000, 0, 1000, false, false),
            new TVEPropertyData("_MotionFrontValue", "Motion Wind Planar Mask", -9000, 0, 1, false, false),


            new TVEPropertyData("_MotionWindElementMode", "Use Wind Elements", -9000, true),

            new TVEPropertyData("_MotionInteractionCategory", "Interaction Settings"),

            new TVEPropertyData("_MotionPushIntensityValue", "Interaction Intensity", -9000, 0, 8, false, false),
            new TVEPropertyData("_MotionPushPivotValue", "Interaction Pivots", -9000, 0, 1, false, false),
            new TVEPropertyData("_MotionPushPhaseValue", "Interaction Phase", -9000, 0, 1, false, false),
            new TVEPropertyData("_MotionPushMaskMode", "Interaction Anim Mask", -9000, "Vertex_R 0 Vertex_G 1 Vertex_B 2 Vertex_A 3 Height 4 Sphere 5", false),
            new TVEPropertyData("_MotionPushMaskRemap", "Interaction Anim Mask", new Vector4(-9000, 0, 0, -9000), 0, 1, false),

            new TVEPropertyData("_MotionPushElementMode", "Use Push Elements", -9000, true),
        };

        List<TVEPropertyData> normalData = new List<TVEPropertyData>
        {
            new TVEPropertyData("_NormalCategory", "Normal Settings"),

            new TVEPropertyData("_NormalBlanketInfo", "The Normal Blanket feature requires elements to work. Use Form Surface or Form Normal elements to send global normals to the shaders.", 0, 10, MessageType.Info),

            new TVEPropertyData("_NormalFlattenValue", "Normal Flattening", -9000, 0, 1, false, false),
            new TVEPropertyData("_NormalSphereValue", "Normal Spherical", -9000, 0, 1, false, false),
            new TVEPropertyData("_NormalSphereOffsetValue", "Normal Spherical Offset", new Vector4(-9000, 0,0,0), false),
            new TVEPropertyData("_NormalComputeValue", "Normal Compute", -9000, 0, 1, false, false),
            new TVEPropertyData("_NormalBlanketValue", "Normal Blanket", -9000, 0, 1, false, false),

            new TVEPropertyData("_NormalProjValue", "Normal ProjY Mask", -9000, 0, 1, false, true),
            new TVEPropertyData("_NormalProjRemap", "Normal ProjY Mask", new Vector4(-9000, 0, 0, -9000), 0, 1, true),
            new TVEPropertyData("_NormalMeshValue", "Normal Mesh Mask", -9000, 0, 1, false, false),
            new TVEPropertyData("_NormalMeshMode", "Normal Mesh Mask", -9000, "Vertex_R 0 Vertex_G 1 Vertex_B 2 Vertex_A 3", false),
            new TVEPropertyData("_NormalMeshRemap", "Normal Mesh Mask", new Vector4(-9000, -9000, -9000, -9000), 0, 1, false),

            new TVEPropertyData("_NormalLandMode", "Normal Land Mode", -9000, "Multiply 0 Additive 1", false),
            new TVEPropertyData("_NormalLandValue", "Normal Land Offset", -9000, false, false),
        };

        List<GameObject> selectedObjects = new List<GameObject>();
        List<Material> selectedMaterials = new List<Material>();

        string[] allPresetPaths;
        List<string> presetPaths;
        List<string> presetLines;
        string[] PresetsEnum;

        int presetIndex;
        int settingsIndex;
        int savingIndex = 1;
        string savePath = "";

        bool isValid = true;
        bool showSelection = true;
        bool refreshManager = false;

        bool useLine;
        List<bool> useLines;

        float motionControl = 0.5f;

        string userFolder = "Assets/BOXOPHOBIC/User";

        //string searchText = "";
        string[] searchResult;
        //string searchInvariant = "";

        GUIStyle stylePopup;
        GUIStyle stylePopupMini;
        GUIStyle styleCenteredHelpBox;

        Color bannerColor;
        string bannerText;
        static TVEMaterialManager window;
        Vector2 scrollPosition = Vector2.zero;

        [MenuItem("Window/BOXOPHOBIC/The Visual Engine/Material Manager", false, 2006)]
        public static void ShowWindow()
        {
            window = GetWindow<TVEMaterialManager>(false, "Material Manager", true);
            window.minSize = new Vector2(400, 300);
        }

        void OnEnable()
        {
            bannerColor = new Color(0.890f, 0.745f, 0.309f);
            bannerText = "Material Manager";

            if (TVEManager.Instance == null)
            {
                isValid = false;
            }

            Initialize();

            if (selectedMaterials.Count > 15)
            {
                showSelection = false;
            }

            userFolder = TVEUtils.GetUserFolder();

            settingsIndex = Convert.ToInt16(SettingsUtils.LoadSettingsData(userFolder + "/Material Settings.asset", MOTION_INDEX));
        }

        void OnSelectionChange()
        {
            Initialize();
            Repaint();
        }

        void OnFocus()
        {
            Initialize();
            Repaint();
        }

        void OnLostFocus()
        {
            ResetEditorWind();

            Shader.SetGlobalInt("TVE_ShowIcons", 0);
        }

        void OnDisable()
        {
            ResetEditorWind();

            Shader.SetGlobalInt("TVE_ShowIcons", 0);
        }

        void OnDestroy()
        {
            ResetEditorWind();

            Shader.SetGlobalInt("TVE_ShowIcons", 0);
        }

        void OnGUI()
        {
            scrollPosition = GUILayout.BeginScrollView(scrollPosition, false, false);

            SetGUIStyles();

            Shader.SetGlobalInt("TVE_ShowIcons", 1);

            GUI_HALF_EDITOR_WIDTH = this.position.width / 2.0f;

            GUILayout.Space(10);
            TVEUtils.DrawToolbar(0, -1);
            StyledGUI.DrawWindowBanner(bannerColor, bannerText);

            GUILayout.BeginHorizontal();
            GUILayout.Space(10);
            GUILayout.BeginVertical();

            if (isValid && selectedMaterials.Count > 0)
            {
                EditorGUILayout.HelpBox("The Material Manager tool allows to set the same values to all selected material. Please note that Undo is not supported for the Material Manager window!", MessageType.Info, true);
            }
            else
            {
                if (TVEManager.Instance == null)
                {
                    GUILayout.Button("\n<size=14>The Visual Engine manager is missing from your scene!</size>\n", styleCenteredHelpBox);

                    GUILayout.Space(10);

                    if (GUILayout.Button("Create Scene Manager", GUILayout.Height(24)))
                    {
                        GameObject manager = new GameObject();
                        manager.AddComponent<TVEManager>();

                        EditorSceneManager.MarkSceneDirty(EditorSceneManager.GetActiveScene());

                        isValid = true;
                    }
                }
                else if (selectedMaterials.Count == 0)
                {
                    GUILayout.Button("\n<size=14>Select one or multiple gameobjects or materials to get started!</size>\n", styleCenteredHelpBox);
                }
            }

            if (isValid)
            {
                if (selectedMaterials.Count == 0)
                {
                    GUI.enabled = false;
                }

                //DrawWindPower();
                SetEditorWind();

                if (selectedMaterials.Count > 0)
                {
                    GUILayout.Space(5);
                }

                DrawMaterials();

                GUILayout.Space(10);

                DrawSettings();

                StyledGUI.DrawWindowBanner(materialOptions[settingsIndex]);

                TVEGlobals.searchManager = TVEUtils.DrawSearchField(TVEGlobals.searchManager, out searchResult, 6);

                //if (settingsIndex == 0 || settingsIndex == 8)
                //{
                //    GUILayout.Space(15);
                //    StyledGUI.DrawWindowCategory("Motion Control");
                //    DrawWindPower();
                //}

                GUILayout.Space(7);

                DrawProperties();

                GUILayout.Space(20);

                DrawSaving();

                SetMaterialProperties();

                if (refreshManager)
                {
                    ResetMaterialDataSet();
                    PopulateMaterialDataSet();
                    GetMaterialProperties();

                    refreshManager = false;
                }
            }

            GUILayout.EndVertical();
            GUILayout.Space(10);
            GUILayout.EndHorizontal();
            GUILayout.EndScrollView();
            GUILayout.Space(15);
        }

        void SetGUIStyles()
        {
            stylePopup = new GUIStyle(EditorStyles.popup)
            {
                alignment = TextAnchor.MiddleCenter
            };

            stylePopupMini = new GUIStyle(EditorStyles.popup)
            {
                fontSize = 9
            };

            styleCenteredHelpBox = new GUIStyle(GUI.skin.GetStyle("HelpBox"))
            {
                richText = true,
                alignment = TextAnchor.MiddleCenter,
            };
        }

        void DrawWindPower()
        {
            GUIStyle styleMid = new GUIStyle();
            styleMid.alignment = TextAnchor.MiddleCenter;
            styleMid.normal.textColor = Color.gray;
            styleMid.fontSize = 7;

            //EditorGUILayout.HelpBox("Always test the motion settings in various wind conditions!", MessageType.Info, true);

            //GUILayout.Space(10);

            GUILayout.BeginHorizontal();
            GUILayout.Space(8);
            motionControl = GUILayout.HorizontalSlider(motionControl, 0.0f, 1.0f);
            GUILayout.EndHorizontal();

            int maxWidth = 20;

            GUILayout.Space(15);

            GUILayout.BeginHorizontal();
            GUILayout.Space(8);
            GUILayout.Label("None", styleMid, GUILayout.Width(maxWidth));
            GUILayout.Label("", styleMid);
            GUILayout.Space(4);
            GUILayout.Label("Windy", styleMid, GUILayout.Width(maxWidth));
            GUILayout.Label("", styleMid);
            GUILayout.Label("Strong", styleMid, GUILayout.Width(maxWidth));
            GUILayout.Space(5);
            GUILayout.EndHorizontal();

            GUILayout.Space(10);
        }

        void DrawMaterials()
        {
            if (selectedMaterials.Count > 0)
            {
                GUILayout.Space(10);
            }

            if (showSelection)
            {
                if (StyledButton("Hide Material Selection"))
                    showSelection = !showSelection;
            }
            else
            {
                if (StyledButton("Show Material Selection"))
                    showSelection = !showSelection;
            }
            if (showSelection)
            {
                for (int i = 0; i < selectedMaterials.Count; i++)
                {
                    if (selectedMaterials[i] != null)
                    {
                        StyledMaterial(selectedMaterials[i]);
                    }
                }
            }
        }

        void DrawSettings()
        {
            EditorGUI.BeginChangeCheck();

            presetIndex = StyledPopup("Material Preset", presetIndex, PresetsEnum);

            if (presetIndex > 0)
            {
                GetPresetLines();

                for (int i = 0; i < selectedMaterials.Count; i++)
                {
                    var material = selectedMaterials[i];

                    if (material.GetTag("UseExternalSettings", false) == "False")
                    {
                        continue;
                    }

                    GetMaterialConversionFromPreset(material);
                    TVEUtils.SetMaterialSettings(material);
                }

                presetIndex = 0;

                Debug.Log("<b>[The Visual Engine]</b> " + "The selected preset has been applied!");
            }

            settingsIndex = StyledPopup("Material Settings", settingsIndex, materialOptions);

            if (EditorGUI.EndChangeCheck())
            {
                ResetMaterialDataSet();
                PopulateMaterialDataSet();
                GetMaterialProperties();

#if !THE_VISUAL_ENGINE_DEVELOPMENT
                SettingsUtils.SaveSettingsData(userFolder + "/Material Settings.asset", settingsIndex);
#endif
            }
        }

        void DrawProperties()
        {
            for (int i = 0; i < propertyDataSet.Count; i++)
            {
                var propertyData = propertyDataSet[i];

                if (propertyData.isVisible == 0)
                {
                    continue;
                }

                bool searchValid = false;

                foreach (var tag in searchResult)
                {
                    if (propertyData.prop.ToUpper().Contains(tag))
                    {
                        searchValid = true;
                        break;
                    }

                    if (propertyData.name.ToUpper().Contains(tag))
                    {
                        searchValid = true;
                        break;
                    }

                    if (propertyData.tag.ToUpper().Contains(tag))
                    {
                        searchValid = true;
                        break;
                    }
                }

                if (!searchValid)
                {
                    continue;
                }

                if (propertyData.space)
                {
                    GUILayout.Space(10);
                }

                if (settingsIndex == 0 || settingsIndex == MOTION_INDEX)
                {
                    if (propertyData.prop.Contains("Motion") && propertyData.prop.Contains("Info"))
                    {
                        DrawWindPower();
                    }
                }

                if (propertyData.type == TVEPropertyData.TVEPropertyType.Value)
                {
                    propertyData.value = StyledValue(propertyData);
                }
                else if (propertyData.type == TVEPropertyData.TVEPropertyType.Range)
                {
                    propertyData.value = StyledSlider(propertyData);
                }
                else if (propertyData.type == TVEPropertyData.TVEPropertyType.Vector)
                {
                    propertyData.vector = StyledVector(propertyData);
                }
                else if (propertyData.type == TVEPropertyData.TVEPropertyType.Remap)
                {
                    propertyData.vector = StyledRemap(propertyData);
                }
                else if (propertyData.type == TVEPropertyData.TVEPropertyType.Color)
                {
                    propertyData.vector = StyledColor(propertyData);
                }
                else if (propertyData.type == TVEPropertyData.TVEPropertyType.Enum)
                {
                    EditorGUI.BeginChangeCheck();

                    propertyData.value = StyledEnum(propertyData);

                    if (EditorGUI.EndChangeCheck())
                    {
                        refreshManager = true;
                    }
                }
                else if (propertyData.type == TVEPropertyData.TVEPropertyType.Toggle)
                {
                    propertyData.value = StyledToggle(propertyData);
                }
                else if (propertyData.type == TVEPropertyData.TVEPropertyType.Texture)
                {
                    propertyData.texture = StyledTexture(propertyData);
                }
                else if (propertyData.type == TVEPropertyData.TVEPropertyType.Space)
                {
                    StyledSpace(propertyData);
                }
                else if (propertyData.type == TVEPropertyData.TVEPropertyType.Category)
                {
                    StyledCategory(propertyData);
                }
                else if (propertyData.type == TVEPropertyData.TVEPropertyType.Message)
                {
                    StyledMessage(propertyData);
                }

                if (propertyData.snap)
                {
                    propertyData.value = Mathf.Round(propertyData.value);
                }
                else
                {
                    propertyData.value = Mathf.Round(propertyData.value * 1000f) / 1000f;
                }
            }
        }

        void StyledMaterial(Material material)
        {
            string color;
            bool useExternalSettings = true;

            if (EditorGUIUtility.isProSkin)
            {
                color = "<color=#87b8ff>";
            }
            else
            {
                color = "<color=#0b448b>";
            }

            if (material.GetTag("UseExternalSettings", false) == "False")
            {
                color = "<color=#808080>";
            }

            GUILayout.Label("<size=10><b>" + color + material.name.Replace(" (TVE Material)", "") + "</color></b></size>", styleCenteredHelpBox, GUILayout.Height(GUI_SELECTION_HEIGHT));

            var lastRect = GUILayoutUtility.GetLastRect();

            var buttonRect = new Rect(lastRect.x, lastRect.y, lastRect.width - 20, lastRect.height);

            if (GUI.Button(buttonRect, "", GUIStyle.none))
            {
                EditorGUIUtility.PingObject(material);
            }

            var toogleRect = new Rect(lastRect.width - 5, lastRect.y + 6, 12, 12);

            if (material.GetTag("UseExternalSettings", false) == "False")
            {
                useExternalSettings = false;
            }

            EditorGUI.BeginChangeCheck();

            useExternalSettings = EditorGUI.Toggle(toogleRect, useExternalSettings);
            GUI.Label(toogleRect, new GUIContent("", "Should the Prefab Settings tool affect the material?"));

            if (EditorGUI.EndChangeCheck())
            {
                if (useExternalSettings)
                {
                    material.SetOverrideTag("UseExternalSettings", "True");
                }
                else
                {
                    material.SetOverrideTag("UseExternalSettings", "False");
                }

                ResetMaterialDataSet();
                PopulateMaterialDataSet();
                GetMaterialProperties();
            }
        }

        int StyledPopup(string name, int index, string[] options)
        {
            if (index > options.Length)
            {
                index = 0;
            }

            GUILayout.BeginHorizontal();
            GUILayout.Label(name, GUILayout.MaxWidth(GUI_HALF_EDITOR_WIDTH));
            index = EditorGUILayout.Popup(index, options, stylePopup, GUILayout.MaxWidth(GUI_HALF_EDITOR_WIDTH));
            GUILayout.EndHorizontal();

            return index;
        }

        void StyledLabel(TVEPropertyData propertyData)
        {
            GUI.color = new Color(1, 1, 1, 0.9f);
            GUILayout.Label(propertyData.name, GUILayout.Width(GUI_HALF_EDITOR_WIDTH - 24));
            GUI.color = Color.white;
        }

        float StyledValue(TVEPropertyData propertyData)
        {
            GUILayout.BeginHorizontal();

            StyledLabel(propertyData);

            if (propertyData.isVisible == propertyData.isLocked)
            {
                GUI.enabled = false;

                EditorGUILayout.FloatField(0, GUILayout.MaxWidth(GUI_HALF_EDITOR_WIDTH));
            }
            else
            {
                if (propertyData.value == -8000)
                {
                    EditorGUI.showMixedValue = true;
                }

                propertyData.value = EditorGUILayout.FloatField(propertyData.value, GUILayout.MaxWidth(GUI_HALF_EDITOR_WIDTH));
            }

            GUILayout.EndHorizontal();

            GUI.enabled = true;
            EditorGUI.showMixedValue = false;

            return propertyData.value;
        }

        float StyledSlider(TVEPropertyData propertyData)
        {
            GUILayout.BeginHorizontal();

            StyledLabel(propertyData);

            if (propertyData.isVisible == propertyData.isLocked)
            {
                GUI.enabled = false;

                GUILayout.HorizontalSlider(propertyData.min, propertyData.min, propertyData.max, GUILayout.MaxWidth(GUI_HALF_EDITOR_WIDTH - 58));
                EditorGUILayout.FloatField(propertyData.min, GUILayout.MaxWidth(GUI_SMALL_WIDTH));
            }
            else
            {
                float equalValue = propertyData.value;
                float mixedValue = 0;

                if (propertyData.value == -8000)
                {
                    EditorGUI.showMixedValue = true;

                    mixedValue = GUILayout.HorizontalSlider(mixedValue, propertyData.min, propertyData.max, GUILayout.MaxWidth(GUI_HALF_EDITOR_WIDTH - 58));

                    if (mixedValue != 0)
                    {
                        propertyData.value = mixedValue;
                    }

                    float floatVal = EditorGUILayout.FloatField(propertyData.value, GUILayout.MaxWidth(GUI_SMALL_WIDTH));

                    if (propertyData.value != floatVal)
                    {
                        propertyData.value = Mathf.Clamp(floatVal, propertyData.min, propertyData.max);
                    }
                }
                else
                {
                    equalValue = GUILayout.HorizontalSlider(equalValue, propertyData.min, propertyData.max, GUILayout.MaxWidth(GUI_HALF_EDITOR_WIDTH - 58));

                    propertyData.value = equalValue;

                    float floatVal = EditorGUILayout.FloatField(propertyData.value, GUILayout.MaxWidth(GUI_SMALL_WIDTH));

                    if (propertyData.value != floatVal)
                    {
                        propertyData.value = Mathf.Clamp(floatVal, propertyData.min, propertyData.max);
                    }
                }
            }

            GUILayout.EndHorizontal();

            GUI.enabled = true;
            EditorGUI.showMixedValue = false;

            return propertyData.value;
        }

        Vector4 StyledVector(TVEPropertyData propertyData)
        {
            GUILayout.BeginHorizontal();

            StyledLabel(propertyData);

            if (propertyData.isVisible == propertyData.isLocked)
            {
                GUI.enabled = false;

                EditorGUILayout.Vector4Field(new GUIContent(""), Vector4.zero, GUILayout.MaxWidth(GUI_HALF_EDITOR_WIDTH));
            }
            else
            {
                if (propertyData.vector.x == -8000 || propertyData.vector.w == -8000)
                {
                    EditorGUI.showMixedValue = true;
                }

                propertyData.vector = EditorGUILayout.Vector4Field(new GUIContent(""), propertyData.vector, GUILayout.MaxWidth(GUI_HALF_EDITOR_WIDTH));
            }

            GUILayout.EndHorizontal();

            GUI.enabled = true;
            EditorGUI.showMixedValue = false;

            return propertyData.vector;
        }

        Vector4 StyledRemap(TVEPropertyData propertyData)
        {
            GUILayout.BeginHorizontal();

            StyledLabel(propertyData);

            GUILayout.Space(2);

            if (propertyData.isVisible == propertyData.isLocked)
            {
                GUI.enabled = false;

                float dummy = 0;

                GUILayout.BeginHorizontal();
                EditorGUILayout.MinMaxSlider(ref dummy, ref dummy, 0, 1, GUILayout.MaxWidth(GUI_HALF_EDITOR_WIDTH - 60));
                GUILayout.Space(2);
                EditorGUILayout.Popup(0, new string[] { "Remap", "Invert" }, stylePopupMini, GUILayout.Width(GUI_SMALL_WIDTH));
                GUILayout.EndHorizontal();
            }
            else
            {
                if (propertyData.vector.x == -8000 || propertyData.vector.w == -8000)
                {
                    EditorGUI.showMixedValue = true;
                }

                float internalValueMin;
                float internalValueMax;

                if (propertyData.vector.w == 0)
                {
                    internalValueMin = propertyData.vector.x;
                    internalValueMax = propertyData.vector.y;
                }
                else
                {
                    internalValueMin = propertyData.vector.y;
                    internalValueMax = propertyData.vector.x;
                }

                GUILayout.BeginHorizontal();

                EditorGUI.BeginChangeCheck();

                EditorGUILayout.MinMaxSlider(ref internalValueMin, ref internalValueMax, propertyData.min, propertyData.max, GUILayout.MaxWidth(GUI_HALF_EDITOR_WIDTH - 60));

                GUILayout.Space(2);

                propertyData.vector.w = (float)EditorGUILayout.Popup((int)propertyData.vector.w, new string[] { "Remap", "Invert" }, stylePopupMini, GUILayout.Width(GUI_SMALL_WIDTH));

                if (EditorGUI.EndChangeCheck())
                {
                    EditorGUI.showMixedValue = false;
                }

                if (propertyData.vector.w == 0)
                {
                    propertyData.vector.x = internalValueMin;
                    propertyData.vector.y = internalValueMax;
                }
                else
                {
                    propertyData.vector.y = internalValueMin;
                    propertyData.vector.x = internalValueMax;
                }

                GUILayout.EndHorizontal();
            }

            GUILayout.EndHorizontal();

            GUI.enabled = true;
            EditorGUI.showMixedValue = false;

            return propertyData.vector;
        }

        Color StyledColor(TVEPropertyData propertyData)
        {
            GUILayout.BeginHorizontal();

            StyledLabel(propertyData);

            if (propertyData.isVisible == propertyData.isLocked)
            {
                GUI.enabled = false;

                EditorGUILayout.ColorField(new GUIContent(""), Color.gray, true, true, propertyData.hdr, GUILayout.MaxWidth(GUI_HALF_EDITOR_WIDTH - 19));
            }
            else
            {
                if (propertyData.vector.x == -8000 || propertyData.vector.w == -8000)
                {
                    EditorGUI.showMixedValue = true;
                }

                propertyData.vector = EditorGUILayout.ColorField(new GUIContent(""), propertyData.vector, true, true, propertyData.hdr, GUILayout.MaxWidth(GUI_HALF_EDITOR_WIDTH - 19));
            }

            GUILayout.EndHorizontal();

            GUI.enabled = true;
            EditorGUI.showMixedValue = false;

            return propertyData.vector;
        }

        Texture StyledTexture(TVEPropertyData propertyData)
        {
            GUILayout.BeginHorizontal();

            StyledLabel(propertyData);

            if (propertyData.isVisible == propertyData.isLocked)
            {
                GUI.enabled = false;

                propertyData.texture = (Texture)EditorGUILayout.ObjectField(propertyData.texture, typeof(Texture), false, GUILayout.MaxWidth(GUI_HALF_EDITOR_WIDTH - 6));
            }
            else
            {
                if (propertyData.isMixed)
                {
                    EditorGUI.showMixedValue = true;

                    EditorGUI.BeginChangeCheck();

                    propertyData.texture = (Texture)EditorGUILayout.ObjectField(propertyData.texture, typeof(Texture), false, GUILayout.MaxWidth(GUI_HALF_EDITOR_WIDTH - 6));

                    if (EditorGUI.EndChangeCheck())
                    {
                        propertyData.isMixed = false;
                        EditorGUI.showMixedValue = false;
                    }
                }
                else
                {
                    propertyData.texture = (Texture)EditorGUILayout.ObjectField(propertyData.texture, typeof(Texture), false, GUILayout.MaxWidth(GUI_HALF_EDITOR_WIDTH - 6));
                }
            }

            GUILayout.EndHorizontal();

            GUI.enabled = true;
            EditorGUI.showMixedValue = false;

            return propertyData.texture;
        }

        float StyledEnum(TVEPropertyData propertyData)
        {
            if (Resources.Load<TextAsset>(propertyData.file) != null)
            {
                var layersPath = AssetDatabase.GetAssetPath(Resources.Load<TextAsset>(propertyData.file));

                StreamReader reader = new StreamReader(layersPath);

                propertyData.options = reader.ReadLine();

                reader.Close();
            }

            string[] enumSplit = propertyData.options.Split(char.Parse(" "));
            List<string> enumOptions = new List<string>(enumSplit.Length / 2);
            List<int> enumIndices = new List<int>(enumSplit.Length / 2);

            for (int i = 0; i < enumSplit.Length; i++)
            {
                if (i % 2 == 0)
                {
                    enumOptions.Add(enumSplit[i].Replace("_", " "));
                }
                else
                {
                    enumIndices.Add(int.Parse(enumSplit[i]));
                }
            }

            int index = (int)propertyData.value;
            int realIndex = enumIndices[0];

            for (int i = 0; i < enumIndices.Count; i++)
            {
                if (enumIndices[i] == index)
                {
                    realIndex = i;
                }
            }

            GUILayout.BeginHorizontal();

            StyledLabel(propertyData);

            if (propertyData.isVisible == propertyData.isLocked)
            {
                GUI.enabled = false;

                propertyData.value = EditorGUILayout.Popup(-8000, enumOptions.ToArray(), GUILayout.MaxWidth(GUI_HALF_EDITOR_WIDTH - 5));
            }
            else
            {
                if (propertyData.value == -8000)
                {
                    EditorGUI.showMixedValue = true;

                    propertyData.value = EditorGUILayout.Popup(-8000, enumOptions.ToArray(), GUILayout.MaxWidth(GUI_HALF_EDITOR_WIDTH - 5));
                }
                else
                {
                    propertyData.value = EditorGUILayout.Popup(realIndex, enumOptions.ToArray(), GUILayout.MaxWidth(GUI_HALF_EDITOR_WIDTH - 5));
                }
            }

            GUILayout.EndHorizontal();

            GUI.enabled = true;
            EditorGUI.showMixedValue = false;

            return propertyData.value;
        }

        float StyledToggle(TVEPropertyData propertyData)
        {
            GUILayout.BeginHorizontal();

            StyledLabel(propertyData);

            if (propertyData.isVisible == propertyData.isLocked)
            {
                GUI.enabled = false;
                EditorGUI.showMixedValue = true;

                EditorGUILayout.Toggle(false);
            }
            else
            {
                bool boolValue = false;

                if (propertyData.value > 0.5f)
                {
                    boolValue = true;
                }

                if (propertyData.value == -8000)
                {
                    EditorGUI.showMixedValue = true;

                    EditorGUI.BeginChangeCheck();

                    boolValue = EditorGUILayout.Toggle(boolValue);

                    if (EditorGUI.EndChangeCheck())
                    {
                        if (boolValue)
                        {
                            propertyData.value = 1;
                        }
                        else
                        {
                            propertyData.value = 0;
                        }
                    }

                    EditorGUI.showMixedValue = false;
                }
                else
                {
                    boolValue = EditorGUILayout.Toggle(boolValue, GUILayout.MaxWidth(GUI_HALF_EDITOR_WIDTH));

                    if (boolValue)
                    {
                        propertyData.value = 1;
                    }
                    else
                    {
                        propertyData.value = 0;
                    }
                }
            }

            GUILayout.EndHorizontal();

            GUI.enabled = true;
            EditorGUI.showMixedValue = false;

            return propertyData.value;
        }

        void StyledSpace(TVEPropertyData propertyData)
        {
            GUILayout.Space(10);
        }

        void StyledCategory(TVEPropertyData propertyData)
        {
            GUILayout.Space(10);
            StyledGUI.DrawWindowCategory(propertyData.category);
            GUILayout.Space(10);
        }

        void StyledMessage(TVEPropertyData propertyData)
        {
            GUILayout.Space(propertyData.spaceTop);
            EditorGUILayout.HelpBox(propertyData.message, propertyData.messageType, true);
            GUILayout.Space(propertyData.spaceDown);
        }

        bool StyledButton(string text)
        {
            bool value = GUILayout.Button("<b>" + text + "</b>", styleCenteredHelpBox, GUILayout.Height(GUI_SELECTION_HEIGHT));

            return value;
        }

        void Initialize()
        {
            GetPresets();

            GetGlobalWind();

            GetSelectedObjects();
            GetPrefabMaterials();
            ResetMaterialDataSet();
            PopulateMaterialDataSet();
            GetMaterialProperties();
        }

        void PopulateMaterialDataSet()
        {
            propertyDataSet = new List<TVEPropertyData>();

            if (settingsIndex == 0)
            {
                propertyDataSet.AddRange(renderData);
                propertyDataSet.AddRange(objectData);
                propertyDataSet.AddRange(globalData);
                propertyDataSet.AddRange(surfaceData);
                propertyDataSet.AddRange(coloringData);
                propertyDataSet.AddRange(weatherData);
                propertyDataSet.AddRange(clippingData);
                propertyDataSet.AddRange(glowingData);
                propertyDataSet.AddRange(transformData);
                propertyDataSet.AddRange(motionData);
                propertyDataSet.AddRange(normalData);
            }
            else if (settingsIndex == 1)
            {
                propertyDataSet.AddRange(renderData);
            }
            else if (settingsIndex == 2)
            {
                propertyDataSet.AddRange(surfaceDataGlobal);
                propertyDataSet.AddRange(surfaceData);
            }
            else if (settingsIndex == 3)
            {
                propertyDataSet.AddRange(coloringDataGlobal);
                propertyDataSet.AddRange(coloringData);
            }
            else if (settingsIndex == 4)
            {
                propertyDataSet.AddRange(weatherDataGlobal);
                propertyDataSet.AddRange(weatherData);
            }
            else if (settingsIndex == 5)
            {
                propertyDataSet.AddRange(clippingDataGlobal);
                propertyDataSet.AddRange(clippingData);
            }
            else if (settingsIndex == 6)
            {
                propertyDataSet.AddRange(glowingDataGlobal);
                propertyDataSet.AddRange(glowingData);
            }
            else if (settingsIndex == 7)
            {
                propertyDataSet.AddRange(transformDataGlobal);
                propertyDataSet.AddRange(transformData);
            }
            else if (settingsIndex == 8)
            {
                propertyDataSet.AddRange(objectData);
                propertyDataSet.AddRange(motionDataGlobal);
                propertyDataSet.AddRange(motionData);
            }
            else if (settingsIndex == 9)
            {
                propertyDataSet.AddRange(normalData);
            }
        }

        void ResetMaterialDataSet()
        {
            for (int d = 0; d < propertyDataSet.Count; d++)
            {
                var propertyData = propertyDataSet[d];

                if (propertyData.setter == TVEPropertyData.TVEPropertySetter.Display)
                {
                    propertyData.value = -9000;
                    propertyData.isVisible = 0;
                    propertyData.isLocked = 0;
                }
                else if (propertyData.setter == TVEPropertyData.TVEPropertySetter.Value)
                {
                    propertyData.value = -9000;
                    propertyData.isVisible = 0;
                    propertyData.isLocked = 0;
                }
                else if (propertyData.setter == TVEPropertyData.TVEPropertySetter.Vector)
                {
                    propertyData.vector = new Vector4(-9000, -9000, -9000, -9000);
                    propertyData.isVisible = 0;
                    propertyData.isLocked = 0;
                }
                else if (propertyData.setter == TVEPropertyData.TVEPropertySetter.Texture)
                {
                    propertyData.texture = null;
                    propertyData.isMixed = false;
                    propertyData.isVisible = 0;
                    propertyData.isLocked = 0;
                }
            }
        }

        void GetSelectedObjects()
        {
            selectedObjects = new List<GameObject>();
            selectedMaterials = new List<Material>();

            for (int i = 0; i < Selection.objects.Length; i++)
            {
                var selection = Selection.objects[i];

                if (selection.GetType() == typeof(GameObject))
                {
                    selectedObjects.Add((GameObject)selection);
                }

                if (selection.GetType() == typeof(Material))
                {
                    selectedMaterials.Add((Material)selection);
                }
            }
        }

        void GetPrefabMaterials()
        {
            var gameObjects = new List<GameObject>();
            var meshRenderers = new List<MeshRenderer>();

            for (int i = 0; i < selectedObjects.Count; i++)
            {
                gameObjects.Add(selectedObjects[i]);
                TVEUtils.GetChildRecursive(selectedObjects[i], gameObjects);
            }

            for (int i = 0; i < gameObjects.Count; i++)
            {
                if (gameObjects[i].GetComponent<MeshRenderer>() != null)
                {
                    meshRenderers.Add(gameObjects[i].GetComponent<MeshRenderer>());
                }
            }

            for (int i = 0; i < meshRenderers.Count; i++)
            {
                if (meshRenderers[i].sharedMaterials != null)
                {
                    for (int j = 0; j < meshRenderers[i].sharedMaterials.Length; j++)
                    {
                        var material = meshRenderers[i].sharedMaterials[j];

                        if (material != null)
                        {
                            if (!selectedMaterials.Contains(material))
                            {
                                selectedMaterials.Add(material);
                            }
                        }
                    }
                }
            }
        }

        void GetMaterialProperties()
        {
            for (int i = 0; i < selectedMaterials.Count; i++)
            {
                var material = selectedMaterials[i];

                for (int d = 0; d < propertyDataSet.Count; d++)
                {
                    var propertyData = propertyDataSet[d];

                    if (material.GetTag("UseExternalSettings", false) == "False")
                    {
                        continue;
                    }

                    if (material.HasProperty(propertyData.prop))
                    {
                        if (!TVEUtils.GetPropertyVisibility(material, propertyData.prop))
                        {
                            continue;
                        }
                    }
                    else
                    {
                        continue;
                    }

                    propertyData.isVisible++;

                    if (propertyData.setter == TVEPropertyData.TVEPropertySetter.Display)
                    {
                        propertyData.value = 1;
                    }

                    if (propertyData.setter == TVEPropertyData.TVEPropertySetter.Value)
                    {
#if UNITY_2022_1_OR_NEWER
                        if (material.IsPropertyLocked(propertyData.prop))
                        {
                            propertyData.isLocked++;
                        }
                        else
#endif
                        {
                            var value = material.GetFloat(propertyData.prop);

                            if (propertyData.value != -9000 && propertyData.value != value)
                            {
                                propertyData.value = -8000;
                            }
                            else
                            {
                                propertyData.value = value;
                            }
                        }
                    }

                    if (propertyData.setter == TVEPropertyData.TVEPropertySetter.Vector)
                    {
#if UNITY_2022_1_OR_NEWER
                        if (material.IsPropertyLocked(propertyData.prop))
                        {
                            propertyData.isLocked++;
                        }
                        else
#endif
                        {
                            var vector = material.GetVector(propertyData.prop);

                            if (propertyData.vector.x != -9000 && propertyData.vector.w != -9000 && propertyData.vector != vector)
                            {
                                propertyData.vector = new Vector4(-8000, -8000, -8000, -8000);
                            }
                            else
                            {
                                propertyData.vector = vector;
                            }
                        }
                    }

                    if (propertyData.setter == TVEPropertyData.TVEPropertySetter.Texture)
                    {
#if UNITY_2022_1_OR_NEWER
                        if (material.IsPropertyLocked(propertyData.prop))
                        {
                            propertyData.isLocked++;
                        }
                        else
#endif
                        {
                            var texture = material.GetTexture(propertyData.prop);

                            if (propertyData.texture != null && propertyData.texture != texture)
                            {
                                propertyData.isMixed = true;
                            }
                            else
                            {
                                propertyData.texture = texture;
                            }
                        }
                    }
                }
            }
        }

        void SetMaterialProperties()
        {
            for (int i = 0; i < selectedMaterials.Count; i++)
            {
                var material = selectedMaterials[i];

                // Maybe a better check for unfocus on Converter Convert button pressed
                if (material != null)
                {
                    TVEUtils.SetMaterialSettings(material);

                    if (material.GetTag("UseExternalSettings", false) == "False")
                    {
                        continue;
                    }

                    for (int d = 0; d < propertyDataSet.Count; d++)
                    {
                        var propertyData = propertyDataSet[d];

                        bool isValid = true;

                        if (!material.HasProperty(propertyData.prop))
                        {
                            isValid = false;
                        }

#if UNITY_2022_1_OR_NEWER
                        if (material.IsPropertyLocked(propertyData.prop))
                        {
                            isValid = false;
                        }
#endif
                        //if (propertyData.isMixed)
                        //{
                        //    isValid = false;
                        //}

                        if (isValid)
                        {
                            if (propertyData.setter == TVEPropertyData.TVEPropertySetter.Value)
                            {
                                if (propertyData.value > -90)
                                {
                                    material.SetFloat(propertyData.prop, propertyData.value);
                                }
                            }

                            if (propertyData.setter == TVEPropertyData.TVEPropertySetter.Vector)
                            {
                                if (propertyData.vector.x > -90)
                                {
                                    material.SetVector(propertyData.prop, propertyData.vector);
                                }
                            }

                            if (propertyData.setter == TVEPropertyData.TVEPropertySetter.Texture)
                            {
                                if (propertyData.texture != null && !propertyData.isMixed)
                                {
                                    material.SetTexture(propertyData.prop, propertyData.texture);
                                }
                            }
                        }
                    }
                }
            }
        }

        void GetPresets()
        {
            presetPaths = new List<string>();
            presetPaths.Add("");

            allPresetPaths = TVEUtils.FindAssets("*.tvepreset", false);

            for (int i = 0; i < allPresetPaths.Length; i++)
            {
                string assetPath = allPresetPaths[i];

                if (assetPath.Contains("[SETTINGS]") == true)
                {
                    presetPaths.Add(assetPath);
                }
            }

            PresetsEnum = new string[presetPaths.Count];
            PresetsEnum[0] = "Choose a preset";

            for (int i = 1; i < presetPaths.Count; i++)
            {
                PresetsEnum[i] = Path.GetFileNameWithoutExtension(presetPaths[i]);
                //PresetsEnum[i] = AssetDatabase.LoadAssetAtPath<UnityEngine.Object>(presetPaths[i]).name;
                PresetsEnum[i] = PresetsEnum[i].Replace("[SETTINGS] ", "");
                PresetsEnum[i] = PresetsEnum[i].Replace(" - ", "/");
            }
        }

        void GetPresetLines()
        {
            StreamReader reader = new StreamReader(presetPaths[presetIndex]);

            presetLines = new List<string>();

            while (!reader.EndOfStream)
            {
                var line = reader.ReadLine().TrimStart();

                presetLines.Add(line);
            }

            reader.Close();

            //for (int i = 0; i < presetLines.Count; i++)
            //{
            //    Debug.Log(presetLines[i]);
            //}
        }

        void GetMaterialConversionFromPreset(Material material)
        {
            InitConditionFromLine();

            for (int i = 0; i < presetLines.Count; i++)
            {
                useLine = GetConditionFromLine(presetLines[i], material);

                if (useLine)
                {
                    if (presetLines[i].StartsWith("Material"))
                    {
                        string[] splitLine = presetLines[i].Split(char.Parse(" "));

                        var type = "";
                        var src = "";
                        var dst = "";
                        var val = "";
                        var set = "";

                        var x = "0";
                        var y = "0";
                        var z = "0";
                        var w = "0";

                        if (splitLine.Length > 1)
                        {
                            type = splitLine[1];
                        }

                        if (splitLine.Length > 2)
                        {
                            src = splitLine[2];
                            set = splitLine[2];
                        }

                        if (splitLine.Length > 3)
                        {
                            dst = splitLine[3];
                            val = splitLine[3];
                            x = splitLine[3];
                        }

                        if (splitLine.Length > 4)
                        {
                            y = splitLine[4];
                        }

                        if (splitLine.Length > 5)
                        {
                            z = splitLine[5];
                        }

                        if (splitLine.Length > 6)
                        {
                            w = splitLine[6];
                        }

                        if (type == "SET_FLOAT")
                        {
                            material.SetFloat(set, float.Parse(x, CultureInfo.InvariantCulture));
                        }
                        else if (type == "SET_COLOR")
                        {
                            material.SetColor(set, new Color(float.Parse(x, CultureInfo.InvariantCulture), float.Parse(y, CultureInfo.InvariantCulture), float.Parse(z, CultureInfo.InvariantCulture), float.Parse(w, CultureInfo.InvariantCulture)));
                        }
                        else if (type == "SET_VECTOR")
                        {
                            material.SetVector(set, new Vector4(float.Parse(x, CultureInfo.InvariantCulture), float.Parse(y, CultureInfo.InvariantCulture), float.Parse(z, CultureInfo.InvariantCulture), float.Parse(w, CultureInfo.InvariantCulture)));
                        }
                        else if (type == "SET_TEX")
                        {
                            val = val.Replace("__", " ");

                            material.SetTexture(set, Resources.Load<Texture>(val));
                        }
                        else if (type == "SET_TEX_BY_PATH")
                        {
                            val = val.Replace("__", " ");

                            material.SetTexture(set, AssetDatabase.LoadAssetAtPath<Texture>(val));
                        }
                        else if (type == "SET_TEX_BY_GUID")
                        {
                            var path = AssetDatabase.GUIDToAssetPath(val);
;
                            material.SetTexture(set, AssetDatabase.LoadAssetAtPath<Texture>(path));
                        }
                        else if (type == "ENABLE_INSTANCING")
                        {
                            material.enableInstancing = true;
                        }
                        else if (type == "DISABLE_INSTANCING")
                        {
                            material.enableInstancing = false;
                        }
                        else if (type == "SET_SHADER_BY_NAME")
                        {
                            var shader = presetLines[i].Replace("Material SET_SHADER_BY_NAME ", "");

                            if (Shader.Find(shader) != null)
                            {
                                material.shader = Shader.Find(shader);
                            }
                        }
                        else if (type == "SET_SHADER_BY_LIGHTING")
                        {
                            var lighting = presetLines[i].Replace("Material SET_SHADER_BY_LIGHTING ", "");

                            var newShaderName = material.shader.name;
                            newShaderName = newShaderName.Replace("Vertex", "XXX");
                            newShaderName = newShaderName.Replace("Simple", "XXX");
                            newShaderName = newShaderName.Replace("Standard", "XXX");
                            newShaderName = newShaderName.Replace("Subsurface", "XXX");
                            newShaderName = newShaderName.Replace("XXX", lighting);

                            if (Shader.Find(newShaderName) != null)
                            {
                                material.shader = Shader.Find(newShaderName);
                            }
                        }
                        else if (type == "SET_SHADER_BY_REPLACE")
                        {
                            var shader = material.shader.name.Replace(src, dst);

                            if (Shader.Find(shader) != null)
                            {
                                material.shader = Shader.Find(shader);
                            }
                        }
                    }
                }
            }
        }

        void InitConditionFromLine()
        {
            useLines = new List<bool>();
            useLines.Add(true);
        }

        bool GetConditionFromLine(string line, Material material)
        {
            var valid = true;

            if (line.StartsWith("if"))
            {
                valid = false;

                string[] splitLine = line.Split(char.Parse(" "));

                var type = "";
                var check = "";
                var val = splitLine[splitLine.Length - 1];

                if (splitLine.Length > 1)
                {
                    type = splitLine[1];
                }

                if (splitLine.Length > 2)
                {
                    for (int i = 2; i < splitLine.Length; i++)
                    {
                        if (!float.TryParse(splitLine[i], out _))
                        {
                            check = check + splitLine[i] + " ";
                        }
                    }

                    check = check.TrimEnd();
                }

                if (type.Contains("SHADER_PATH_CONTAINS"))
                {
                    var path = AssetDatabase.GetAssetPath(material.shader).ToUpperInvariant();

                    if (path.Contains(check.ToUpperInvariant()))
                    {
                        valid = true;
                    }
                }
                else if (type.Contains("SHADER_NAME_CONTAINS"))
                {
                    var name = material.shader.name.ToUpperInvariant();

                    if (name.Contains(check.ToUpperInvariant()))
                    {
                        valid = true;
                    }
                }
                else if (type.Contains("MATERIAL_NAME_CONTAINS"))
                {
                    var name = material.name.ToUpperInvariant();

                    if (name.Contains(check.ToUpperInvariant()))
                    {
                        valid = true;
                    }
                }
                else if (type.Contains("MATERIAL_PATH_CONTAINS"))
                {
                    var path = AssetDatabase.GetAssetPath(material).ToUpperInvariant();

                    if (path.Contains(check.ToUpperInvariant()))
                    {
                        valid = true;
                    }
                }
                else if (type.Contains("MATERIAL_NAME_CONTAINS"))
                {
                    if (material.name.Contains(check))
                    {
                        valid = true;
                    }
                }
                else if (type.Contains("MATERIAL_PIPELINE_IS_STANDARD"))
                {
                    if (material.GetTag("RenderPipeline", false) == "")
                    {
                        valid = true;
                    }
                }
                else if (type.Contains("MATERIAL_PIPELINE_IS_UNIVERSAL"))
                {
                    if (material.GetTag("RenderPipeline", false) == "UniversalPipeline")
                    {
                        valid = true;
                    }
                }
                else if (type.Contains("MATERIAL_PIPELINE_IS_HD"))
                {
                    if (material.GetTag("RenderPipeline", false) == "HDRenderPipeline")
                    {
                        valid = true;
                    }
                }
                else if (type.Contains("MATERIAL_RENDERTYPE_TAG_CONTAINS"))
                {
                    if (material.GetTag("RenderType", false).Contains(check))
                    {
                        valid = true;
                    }
                }
                else if (type.Contains("MATERIAL_HAS_PROP"))
                {
                    if (material.HasProperty(check))
                    {
                        valid = true;
                    }
                }
                else if (type.Contains("MATERIAL_FLOAT_EQUALS"))
                {
                    var min = float.Parse(val, CultureInfo.InvariantCulture) - 0.1f;
                    var max = float.Parse(val, CultureInfo.InvariantCulture) + 0.1f;

                    if (material.HasProperty(check) && material.GetFloat(check) > min && material.GetFloat(check) < max)
                    {
                        valid = true;
                    }
                }
                else if (type.Contains("MATERIAL_FLOAT_SMALLER"))
                {
                    var value = float.Parse(val, CultureInfo.InvariantCulture);

                    if (material.HasProperty(check) && material.GetFloat(check) < value)
                    {
                        valid = true;
                    }
                }
                else if (type.Contains("MATERIAL_FLOAT_GREATER"))
                {
                    var value = float.Parse(val, CultureInfo.InvariantCulture);

                    if (material.HasProperty(check) && material.GetFloat(check) > value)
                    {
                        valid = true;
                    }
                }
                else if (type.Contains("MATERIAL_KEYWORD_ENABLED"))
                {
                    if (material.IsKeywordEnabled(check))
                    {
                        valid = true;
                    }
                }

                useLines.Add(valid);
            }

            if (line.StartsWith("if") && line.Contains("!"))
            {
                valid = !valid;
                useLines[useLines.Count - 1] = valid;
            }

            if (line.StartsWith("endif") || line.StartsWith("}"))
            {
                useLines.RemoveAt(useLines.Count - 1);
            }

            var useLine = true;

            for (int i = 1; i < useLines.Count; i++)
            {
                if (useLines[i] == false)
                {
                    useLine = false;
                    break;
                }
            }

            return useLine;
        }

        void DrawSaving()
        {
            GUILayout.BeginHorizontal();

            savingIndex = EditorGUILayout.Popup(savingIndex, savingOptions, stylePopup, GUILayout.Height(GUI_HEIGHT), GUILayout.MaxWidth(GUI_HALF_EDITOR_WIDTH));

            if (GUILayout.Button("Save Preset", GUILayout.Height(GUI_HEIGHT), GUILayout.MaxWidth(GUI_HALF_EDITOR_WIDTH)))
            {
                savePath = EditorUtility.SaveFilePanelInProject("Save Preset", "Custom - My Preset", "tvepreset", "Use the ' - ' simbol to create categories!");

                if (savePath != "")
                {
                    savePath = savePath.Replace("[SETTINGS] ", "");
                    savePath = savePath.Replace(Path.GetFileName(savePath), "[SETTINGS] " + Path.GetFileName(savePath));

                    StreamWriter writer = new StreamWriter(savePath);
                    
                    savingDataSet = new List<TVEPropertyData>();

                    if (savingIndex == 0)
                    {
                        savingDataSet.AddRange(renderData);
                        savingDataSet.AddRange(objectData);
                        savingDataSet.AddRange(globalData);
                        savingDataSet.AddRange(surfaceData);
                        savingDataSet.AddRange(coloringData);
                        savingDataSet.AddRange(weatherData);
                        savingDataSet.AddRange(clippingData);
                        savingDataSet.AddRange(glowingData);
                        savingDataSet.AddRange(transformData);
                        savingDataSet.AddRange(motionData);
                        savingDataSet.AddRange(normalData);
                    }
                    else if (savingIndex == 1)
                    {
                        savingDataSet = propertyDataSet;
                    }

                    for (int i = 0; i < savingDataSet.Count; i++)
                    {
                        bool searchValid = false;

                        foreach (var tag in searchResult)
                        {
                            if (savingDataSet[i].prop.ToUpper().Contains(tag))
                            {
                                searchValid = true;
                                break;
                            }

                            if (savingDataSet[i].name.ToUpper().Contains(tag))
                            {
                                searchValid = true;
                                break;
                            }

                            if (savingDataSet[i].tag.ToUpper().Contains(tag))
                            {
                                searchValid = true;
                                break;
                            }
                        }

                        if (savingDataSet[i].space == true)
                        {
                            writer.WriteLine("");
                        }

                        if (savingDataSet[i].type == TVEPropertyData.TVEPropertyType.Space)
                        {
                            writer.WriteLine("");
                        }

                        if (savingDataSet[i].type == TVEPropertyData.TVEPropertyType.Category)
                        {
                            writer.WriteLine("");
                            writer.WriteLine("// " + savingDataSet[i].category);
                            writer.WriteLine("");
                        }

                        if (savingDataSet[i].type == TVEPropertyData.TVEPropertyType.Value || savingDataSet[i].type == TVEPropertyData.TVEPropertyType.Range || savingDataSet[i].type == TVEPropertyData.TVEPropertyType.Enum || savingDataSet[i].type == TVEPropertyData.TVEPropertyType.Toggle)
                        {
                            if (savingDataSet[i].value > -99 && searchValid)
                            {
                                writer.WriteLine("Material SET_FLOAT " + savingDataSet[i].prop + " " + savingDataSet[i].value.ToString(CultureInfo.InvariantCulture));
                            }
                            else
                            {
                                writer.WriteLine("//Material SET_FLOAT " + savingDataSet[i].prop + " " + "0");
                            }
                        }

                        if (savingDataSet[i].type == TVEPropertyData.TVEPropertyType.Vector || savingDataSet[i].type == TVEPropertyData.TVEPropertyType.Remap || savingDataSet[i].type == TVEPropertyData.TVEPropertyType.Color)
                        {
                            if (savingDataSet[i].vector.x > -99 && searchValid)
                            {
                                writer.WriteLine("Material SET_VECTOR " + savingDataSet[i].prop + " " + savingDataSet[i].vector.x.ToString(CultureInfo.InvariantCulture) + " " + savingDataSet[i].vector.y.ToString(CultureInfo.InvariantCulture) + " " + savingDataSet[i].vector.z.ToString(CultureInfo.InvariantCulture) + " " + savingDataSet[i].vector.w.ToString(CultureInfo.InvariantCulture));
                            }
                            else
                            {
                                writer.WriteLine("//Material SET_VECTOR " + savingDataSet[i].prop + " " + "0 0 0 0");
                            }
                        }

                        if (savingDataSet[i].type == TVEPropertyData.TVEPropertyType.Texture)
                        {
                            if (savingDataSet[i].texture != null && searchValid)
                            {
                                writer.WriteLine("Material SET_TEX_BY_GUID " + savingDataSet[i].prop + " " + AssetDatabase.AssetPathToGUID(AssetDatabase.GetAssetPath(savingDataSet[i].texture)));
                            }
                            else
                            {
                                writer.WriteLine("//Material SET_TEX_BY_GUID " + savingDataSet[i].prop + " " + "NONE");
                            }
                        }
                    }

                    writer.Close();

                    AssetDatabase.Refresh();

                    GetPresets();

                    Debug.Log("<b>[The Visual Engine]</b> " + "Material preset saved!");

                    GUIUtility.ExitGUI();
                }
            }

            GUILayout.EndHorizontal();
        }

        void GetGlobalWind()
        {
            motionControl = Shader.GetGlobalVector("TVE_MotionParams").z;
        }

        void ResetEditorWind()
        {
            Shader.SetGlobalFloat("TVE_MaterialManagerActive", 0);
        }

        void SetEditorWind()
        {
            Shader.SetGlobalFloat("TVE_MaterialManagerActive", 1);
            Shader.SetGlobalFloat("TVE_MotionControlEditor", motionControl);
        }
    }
}
