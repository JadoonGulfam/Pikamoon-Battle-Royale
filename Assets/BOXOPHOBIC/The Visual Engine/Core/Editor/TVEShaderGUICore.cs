//Cristian Pop - https://boxophobic.com/

using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using TheVisualEngine;
using Boxophobic.StyledGUI;

public class TVEShaderGUICore : ShaderGUI
{
    bool multiSelection = false;
    bool showInternalProperties = false;
    bool showHiddenProperties = false;
    bool showActiveKeywords = false;
    bool showAdditionalInfo = false;

    bool advancedEnabled = true;

    //string searchText = "";
    string[] searchResult;

    GUIStyle subTitleStyle;

    public override void AssignNewShaderToMaterial(Material material, Shader oldShader, Shader newShader)
    {
        base.AssignNewShaderToMaterial(material, oldShader, newShader);

        TVEUtils.SetMaterialSettings(material);
    }

    public override void OnClosed(Material material)
    {
        base.OnClosed(material);
    }

    public override void OnGUI(MaterialEditor materialEditor, MaterialProperty[] props)
    {
        //GUILayout.Space(5);
        //TVEUtils.DrawToolbar(-34, -4);

        var material0 = materialEditor.target as Material;
        var materials = materialEditor.targets;

        if (materials.Length > 1)
            multiSelection = true;

        // Used for impostors only
        if (material0.HasProperty("_IsInitialized"))
        {
            if (material0.GetFloat("_IsInitialized") > 0)
            {
                DrawDynamicInspector(material0, materialEditor, props);
            }
            else
            {
                DrawInitInspector(material0);
            }
        }
        else
        {
            DrawDynamicInspector(material0, materialEditor, props);
        }

        foreach (Material material in materials)
        {
            TVEUtils.SetMaterialSettings(material);
        }
    }

    void DrawDynamicInspector(Material material, MaterialEditor materialEditor, MaterialProperty[] props)
    {
        subTitleStyle = new GUIStyle("label")
        {
            richText = true,
            alignment = TextAnchor.MiddleRight
        };

        bool showCategory = true;

        TVEUtils.DrawShaderBanner(material);

        GUILayout.Space(5);

        TVEGlobals.searchMaterial = TVEUtils.DrawSearchField(TVEGlobals.searchMaterial, out searchResult, 2);

        GUILayout.Space(10);

        TVEUtils.DrawCopySettingsFromObject(material);

        GUILayout.Space(15);

        var customPropsList = new List<MaterialProperty>();

        if (multiSelection)
        {
            for (int i = 0; i < props.Length; i++)
            {
                var prop = props[i];

                if (prop.flags == MaterialProperty.PropFlags.HideInInspector)
                    continue;

                if (prop.name == "unity_Lightmaps")
                    continue;

                if (prop.name == "unity_LightmapsInd")
                    continue;

                if (prop.name == "unity_ShadowMasks")
                    continue;

                customPropsList.Add(prop);
            }
        }
        else
        {
            for (int i = 0; i < props.Length; i++)
            {
                var prop = props[i];
                var displayName = prop.displayName;
                var internalName = prop.name;

                if (prop.flags == MaterialProperty.PropFlags.HideInInspector && !showHiddenProperties)
                {
                    continue;
                }

                //if (MHUtils.GetPropertyVisibility(material, internalName))
                //{
                //    customPropsList.Add(prop);
                //}

                bool searchValid = false;

                foreach (var tag in searchResult)
                {
                    if (displayName.ToUpper().Contains(tag))
                    {
                        searchValid = true;
                        break;
                    }

                    if (internalName.ToUpper().Contains(tag))
                    {
                        searchValid = true;
                        break;
                    }
                }

                if (searchValid)
                {
                    if (internalName.Contains("Category"))
                    {
                        customPropsList.Add(prop);

                        if (material.GetInt(internalName) == 0)
                        {
                            showCategory = false;
                        }
                        else
                        {
                            showCategory = true;
                        }
                    }
                    else
                    {
                        if (showCategory)
                        {
                            if (TVEUtils.GetPropertyVisibility(material, internalName))
                            {
                                customPropsList.Add(prop);
                            }
                        }
                    }
                }
            }
        }

        //Draw Custom GUI
        for (int i = 0; i < customPropsList.Count; i++)
        {
            var property = customPropsList[i];
            var internalName = property.name;
            var displayName = TVEUtils.GetPropertyDisplay(material, property.name, property.displayName);

            var debug = "";

            if (showInternalProperties)
            {
                debug = "  (" + customPropsList[i].name + ")";
            }

            if (customPropsList[i].name == "_Albedo" || customPropsList[i].name == "_Normals")
            {
                materialEditor.TexturePropertySingleLine(new GUIContent(displayName + debug, ""), customPropsList[i]);
            }
            else
            {
                materialEditor.ShaderProperty(customPropsList[i], displayName + debug);
            }

            TVEUtils.GetActiveDisplay(material, internalName, "_LayerCategory", "_SecondIntensityValue", "79D0FF", subTitleStyle);
            TVEUtils.GetActiveDisplay(material, internalName, "_DetailCategory", "_ThirdIntensityValue", "8FFF79", subTitleStyle);
            TVEUtils.GetActiveDisplay(material, internalName, "_TerrainCategory", "_TerrainIntensityValue", "FFAF5A", subTitleStyle);
            TVEUtils.GetActiveDisplay(material, internalName, "_OcclusionCategory", "_OcclusionIntensityValue", "60E87F", subTitleStyle);
            TVEUtils.GetActiveDisplay(material, internalName, "_GradientCategory", "_GradientIntensityValue", "FFBC5B", subTitleStyle);
            TVEUtils.GetActiveDisplay(material, internalName, "_VariationCategory", "_VariationIntensityValue", "FF79CC", subTitleStyle);
            TVEUtils.GetActiveDisplay(material, internalName, "_TintingCategory", "_TintingIntensityValue", "FF8971", subTitleStyle);
            TVEUtils.GetActiveDisplay(material, internalName, "_DrynessCategory", "_DrynessIntensityValue", "FFBE71", subTitleStyle);
            TVEUtils.GetActiveDisplay(material, internalName, "_OverlayCategory", "_OverlayIntensityValue", "98C8FF", subTitleStyle);
            TVEUtils.GetActiveDisplay(material, internalName, "_WetnessCategory", "_WetnessIntensityValue", "72FBD4", subTitleStyle);
            TVEUtils.GetActiveDisplay(material, internalName, "_CutoutCategory", "_CutoutIntensityValue", "D2D2D2", subTitleStyle);
            TVEUtils.GetActiveDisplay(material, internalName, "_DitherCategory", new string[] { "_DitherConstantValue", "_DitherDistanceValue", "_DitherProximityValue", "_DitherGlancingValue" }, "D2D2D2", subTitleStyle);
            TVEUtils.GetActiveDisplay(material, internalName, "_EmissiveCategory", "_EmissiveIntensityValue", "FFF700", subTitleStyle);
            TVEUtils.GetActiveDisplay(material, internalName, "_SubsurfaceCategory", "_SubsurfaceIntensityValue", "CFFF75", subTitleStyle);
            TVEUtils.GetActiveDisplay(material, internalName, "_PerspectiveCategory", "_PerspectiveIntensityValue", "CD75FF", subTitleStyle);
            TVEUtils.GetActiveDisplay(material, internalName, "_SizeFadeCategory", "_SizeFadeIntensityValue", "9FA2FF", subTitleStyle);
            TVEUtils.GetActiveDisplay(material, internalName, "_HeightCategory", "_HeightIntensityValue", "9FA2FF", subTitleStyle);
            TVEUtils.GetActiveDisplay(material, internalName, "_BlanketCategory", new string[] { "_BlanketConformValue", "_BlanketOrientationValue" }, "FFF300", subTitleStyle);
            TVEUtils.GetActiveDisplay(material, internalName, "_MotionWindCategory", new string[] { "_MotionHighlightValue", "_MotionBaseIntensityValue", "_MotionSmallIntensityValue", "_MotionTinyIntensityValue" }, "7FFF79", subTitleStyle);
            TVEUtils.GetActiveDisplay(material, internalName, "_MotionInteractionCategory", "_MotionPushIntensityValue", "16E0AF", subTitleStyle);
            TVEUtils.GetActiveDisplay(material, internalName, "_NormalCategory", new string[] { "_NormalFlattenValue", "_NormalSphereValue", "_NormalComputeValue", "_NormalBlanketValue" }, "9393FF", subTitleStyle);
        }

        //GUILayout.Space(10);

        advancedEnabled = StyledGUI.DrawInspectorCategory("Advanced Settings", advancedEnabled, true, 0, 0);

        if (advancedEnabled)
        {
            if (!material.shader.name.Contains("Terrain"))
            {
                GUILayout.Space(10);

                materialEditor.EnableInstancingField();

                if (!material.shader.name.Contains("Impostor"))
                {
                    GUILayout.Space(10);

                    TVEUtils.DrawRenderQueue(material, materialEditor);
                    TVEUtils.DrawBakeGIMode(material);
                }
            }

            GUILayout.Space(10);

            showInternalProperties = EditorGUILayout.Toggle("Show Internal Properties", showInternalProperties);
            showHiddenProperties = EditorGUILayout.Toggle("Show Hidden Properties", showHiddenProperties);
            showActiveKeywords = EditorGUILayout.Toggle("Show Active Keywords", showActiveKeywords);
            showAdditionalInfo = EditorGUILayout.Toggle("Show Additional Info", showAdditionalInfo);

            if (showActiveKeywords)
            {
                TVEUtils.DrawActiveKeywords(material);
            }

            if (showAdditionalInfo)
            {
                TVEUtils.DrawTechnicalDetails(material);
            }
        }

        GUILayout.Space(15);

        TVEUtils.DrawPoweredBy();
    }

    void DrawInitInspector(Material material)
    {
        TVEUtils.DrawShaderBanner(material);

        GUILayout.Space(5);

        EditorGUILayout.HelpBox("The original material properties are not copied to the Impostor material. Drag the game object the impostor is baked from to the field below to copy the properties!", MessageType.Error, true);

        GUILayout.Space(10);

        TVEUtils.DrawCopySettingsFromObject(material);

        GUILayout.Space(10);
    }
}

