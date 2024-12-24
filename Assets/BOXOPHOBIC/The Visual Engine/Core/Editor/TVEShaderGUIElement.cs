//Cristian Pop - https://boxophobic.com/

using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using TheVisualEngine;
using Boxophobic.StyledGUI;

public class TVEShaderGUIElement : ShaderGUI
{
    bool multiSelection = false;
    bool showInternalProperties = false;
    bool showHiddenProperties = false;
    bool showAdditionalInfo = false;


    bool advancedEnabled = true;

    public override void AssignNewShaderToMaterial(Material material, Shader oldShader, Shader newShader)
    {
        base.AssignNewShaderToMaterial(material, oldShader, newShader);

        AssignDefaultSettings(material, newShader);

        TheVisualEngine.TVEUtils.SetElementSettings(material);
    }

    public override void OnGUI(MaterialEditor materialEditor, MaterialProperty[] props)
    {
        //GUILayout.Space(5);
        //TVEUtils.DrawToolbar(-34, -4);

        var material0 = materialEditor.target as Material;
        var materials = materialEditor.targets;

        if (materials.Length > 1)
            multiSelection = true;

        DrawDynamicInspector(material0, materialEditor, props);

        foreach (Material material in materials)
        {
            TheVisualEngine.TVEUtils.SetElementSettings(material);
        }
    }

    void DrawDynamicInspector(Material material, MaterialEditor materialEditor, MaterialProperty[] props)
    {
        bool showCategory = true;

        TVEUtils.DrawShaderBanner(material);

        GUILayout.Space(5);

        var customPropsList = new List<MaterialProperty>();

        if (multiSelection)
        {
            for (int i = 0; i < props.Length; i++)
            {
                var prop = props[i];

                if (prop.flags == MaterialProperty.PropFlags.HideInInspector && !showHiddenProperties)
                {
                    continue;
                }

                customPropsList.Add(prop);
            }
        }
        else
        {
            for (int i = 0; i < props.Length; i++)
            {
                var prop = props[i];

                if (prop.flags == MaterialProperty.PropFlags.HideInInspector && !showHiddenProperties)
                {
                    continue;
                }

                if (material.HasProperty("_ElementMode"))
                {
                    if (material.GetInt("_ElementMode") == 1 && prop.name == "_MainColor")
                        continue;

                    if (material.GetInt("_ElementMode") == 0 && prop.name == "_AdditionalColor1")
                        continue;

                    if (material.GetInt("_ElementMode") == 0 && prop.name == "_AdditionalColor2")
                        continue;

                    if (material.GetInt("_ElementMode") == 0 && prop.name == "_AdditionalColor3")
                        continue;

                    if (material.GetInt("_ElementMode") == 0 && prop.name == "_AdditionalColor4")
                        continue;

                    if (material.GetInt("_ElementMode") == 1 && prop.name == "_MainValue")
                        continue;

                    if (material.GetInt("_ElementMode") == 0 && prop.name == "_AdditionalValue1")
                        continue;

                    if (material.GetInt("_ElementMode") == 0 && prop.name == "_AdditionalValue2")
                        continue;

                    if (material.GetInt("_ElementMode") == 0 && prop.name == "_AdditionalValue3")
                        continue;

                    if (material.GetInt("_ElementMode") == 0 && prop.name == "_AdditionalValue4")
                        continue;

                    if (material.GetInt("_ElementMode") == 0 && prop.name == "_SeasonRemap")
                        continue;
                }

                //if (material.HasProperty("_ElementMotionMode"))
                //{
                //    var mode = material.GetInt("_ElementMotionMode");

                //    if (mode == 13)
                //    {
                //        if (prop.name == "_MotionPower")
                //            continue;
                //    }
                //}

                if (material.HasProperty("_MotionDirectionMode"))
                {
                    var mode = material.GetInt("_MotionDirectionMode");

                    if (mode != 2)
                    {
                        if (prop.name == "_SpeedTresholdValue")
                            continue;
                    }
                }
                else
                {
                    if (prop.name == "_SpeedTresholdValue")
                        continue;
                }

                //if (material.HasProperty("_ElementRaycastMode"))
                //{
                //    if (material.GetInt("_ElementRaycastMode") == 0 && prop.name == "_RaycastLayerMask")
                //        continue;
                //    if (material.GetInt("_ElementRaycastMode") == 0 && prop.name == "_RaycastDistanceMaxValue")
                //        continue;
                //}

                if (prop.name.Contains("Category"))
                {
                    customPropsList.Add(prop);

                    if (material.GetInt(prop.name) == 0)
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
                        customPropsList.Add(prop);
                    }
                }
            }
        }

        //Draw Custom GUI
        for (int i = 0; i < customPropsList.Count; i++)
        {
            var displayName = customPropsList[i].displayName;

            // Temp replace name
            if (material.shader.name.Contains("Push"))
            {
                displayName = displayName.Replace("Wind", "Push");
            }

            var debug = "";

            if (showInternalProperties)
            {
                debug = "  (" + customPropsList[i].name + ")";
            }

            materialEditor.ShaderProperty(customPropsList[i], displayName + debug);
        }

        advancedEnabled = StyledGUI.DrawInspectorCategory("Advanced Settings", advancedEnabled, true, 0, 0);

        if (advancedEnabled)
        {
            GUILayout.Space(10);

            if (EditorUtility.IsPersistent(material) /*&& (material.HasProperty("_ElementRaycastMode") && material.GetFloat("_ElementRaycastMode") < 0.5f)*/)
            {
                materialEditor.EnableInstancingField();
                GUILayout.Space(10);
            }
            else
            {
                material.enableInstancing = false;
            }

            showInternalProperties = EditorGUILayout.Toggle("Show Internal Properties", showInternalProperties);
            showHiddenProperties = EditorGUILayout.Toggle("Show Hidden Properties", showHiddenProperties);
            showAdditionalInfo = EditorGUILayout.Toggle("Show Additional Info", showAdditionalInfo);

            if (showAdditionalInfo)
            {
                TheVisualEngine.TVEUtils.DrawTechnicalDetails(material);
            }
        }

        GUILayout.Space(15);

        TheVisualEngine.TVEUtils.DrawPoweredBy();
    }

    void AssignDefaultSettings(Material material, Shader shader)
    {
        if (shader.name.Contains("Cutout"))
        {
            material.SetInt("_ElementBlendA", 1);
        }

        if (shader.name.Contains("Size Fade"))
        {
            material.SetInt("_ElementBlendA", 0);
        }

        if (!shader.name.Contains("Wind") && !shader.name.Contains("Push"))
        {
            if (material.HasTexture("_MainTex"))
            {
                material.SetTexture("_MainTex", Resources.Load<Material>("Internal Colors").GetTexture("_MainTex"));
            }
        }
        else
        {

            if (material.HasTexture("_MainTex"))
            {
                material.SetTexture("_MainTex", Resources.Load<Material>("Internal Motion").GetTexture("_MainTex"));
            }

            material.SetTexture("_MotionTex", Resources.Load<Material>("Internal Motion").GetTexture("_MotionTex"));

            if (shader.name.Contains("Wind"))
            {
                material.SetFloat("_MotionDirectionMode", 0);
            }

            if (shader.name.Contains("Push"))
            {
                material.SetFloat("_MotionDirectionMode", 1);
            }
        }
    }
}

