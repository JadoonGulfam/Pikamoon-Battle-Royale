//Cristian Pop - https://boxophobic.com/

using UnityEngine;
using System.IO;
using UnityEngine.Rendering;
using System.Collections.Generic;
using System.Globalization;

#if UNITY_EDITOR
using UnityEditor;
using System.Linq;
using Boxophobic.Utils;
using Boxophobic.Constants;
#endif

namespace TheVisualEngine
{
    public class TVEUtils
    {
        const string minimumVersionFor2021 = "2021.3.35";
        const string minimumVersionFor2022 = "2022.3.18";
        const string minimumVersionFor6000 = "6000.0.23";

        // Settings Utils
        public static void SetMaterialSettings(Material material)
        {
            var shaderName = material.shader.name;
            var projectPipeline = TVEUtils.GetProjectPipeline();

            if (!material.HasProperty("_IsTVEShader"))
            {
                return;
            }

            if (material.HasProperty("_IsVersion"))
            {
                var version = material.GetInt("_IsVersion");

                // fix wrong version added in shader
                if (version == 1200)
                {
                    version = 1050;
                }

                // Chanage shader early
                if (version < 900)
                {
                    // Mobile shaders
                    if (material.shader.name == ("Hidden/BOXOPHOBIC/The Visual Engine/Geometry/Cross Vertex Lit (Mobile)"))
                    {
                        material.shader = Shader.Find("BOXOPHOBIC/The Visual Engine/Geometry/Plant Vertex Lit (Mobile)");

                        material.SetFloat("_SizeFadeStartValue", 0);
                        material.SetFloat("_SizeFadeEndValue", 0);
                        material.SetFloat("_MotionValue_20", 0);
                        material.SetFloat("_MotionValue_30", 0);
                    }

                    if (material.shader.name == ("Hidden/BOXOPHOBIC/The Visual Engine/Geometry/Cross Simple Lit (Mobile)"))
                    {
                        material.shader = Shader.Find("BOXOPHOBIC/The Visual Engine/Geometry/Plant Simple Lit (Mobile)");

                        material.SetFloat("_SizeFadeStartValue", 0);
                        material.SetFloat("_SizeFadeEndValue", 0);
                        material.SetFloat("_MotionValue_20", 0);
                        material.SetFloat("_MotionValue_30", 0);
                    }

                    if (material.shader.name == ("Hidden/BOXOPHOBIC/The Visual Engine/Geometry/Grass Vertex Lit (Mobile)"))
                    {
                        material.shader = Shader.Find("BOXOPHOBIC/The Visual Engine/Geometry/Plant Vertex Lit (Mobile)");

                        material.SetFloat("_MotionValue_20", 0);
                    }

                    if (material.shader.name == ("Hidden/BOXOPHOBIC/The Visual Engine/Geometry/Grass Simple Lit (Mobile)"))
                    {
                        material.shader = Shader.Find("BOXOPHOBIC/The Visual Engine/Geometry/Plant Simple Lit (Mobile)");

                        material.SetFloat("_MotionValue_20", 0);
                    }

                    if (material.shader.name == ("Hidden/BOXOPHOBIC/The Visual Engine/Geometry/Uber Vertex Lit (Mobile)"))
                    {
                        material.shader = Shader.Find("BOXOPHOBIC/The Visual Engine/Geometry/Plant Vertex Lit (Mobile)");

                        material.SetFloat("_SizeFadeStartValue", 0);
                        material.SetFloat("_SizeFadeEndValue", 0);
                    }

                    if (material.shader.name == ("Hidden/BOXOPHOBIC/The Visual Engine/Geometry/Uber Simple Lit (Mobile)"))
                    {
                        material.shader = Shader.Find("BOXOPHOBIC/The Visual Engine/Geometry/Plant Simple Lit (Mobile)");

                        material.SetFloat("_SizeFadeStartValue", 0);
                        material.SetFloat("_SizeFadeEndValue", 0);
                    }

                    // Default Shaders
                    if (material.shader.name == ("Hidden/BOXOPHOBIC/The Visual Engine/Geometry/Cross Standard Lit"))
                    {
                        material.shader = Shader.Find("BOXOPHOBIC/The Visual Engine/Geometry/Plant Standard Lit");

                        material.SetFloat("_SizeFadeStartValue", 0);
                        material.SetFloat("_SizeFadeEndValue", 0);
                        material.SetFloat("_MotionValue_20", 0);
                        material.SetFloat("_MotionValue_30", 0);
                    }

                    if (material.shader.name == ("Hidden/BOXOPHOBIC/The Visual Engine/Geometry/Cross Subsurface Lit"))
                    {
                        material.shader = Shader.Find("BOXOPHOBIC/The Visual Engine/Geometry/Plant Subsurface Lit");

                        material.SetFloat("_SizeFadeStartValue", 0);
                        material.SetFloat("_SizeFadeEndValue", 0);
                        material.SetFloat("_MotionValue_20", 0);
                        material.SetFloat("_MotionValue_30", 0);
                    }

                    if (material.shader.name == ("Hidden/BOXOPHOBIC/The Visual Engine/Geometry/Grass Standard Lit"))
                    {
                        material.shader = Shader.Find("BOXOPHOBIC/The Visual Engine/Geometry/Plant Standard Lit");

                        material.SetFloat("_MotionValue_20", 0);
                        material.SetFloat("_MotionValue_30", 0);
                    }

                    if (material.shader.name == ("Hidden/BOXOPHOBIC/The Visual Engine/Geometry/Grass Subsurface Lit"))
                    {
                        material.shader = Shader.Find("BOXOPHOBIC/The Visual Engine/Geometry/Plant Subsurface Lit");

                        material.SetFloat("_MotionValue_20", 0);
                    }

                    if (material.shader.name == ("Hidden/BOXOPHOBIC/The Visual Engine/Geometry/Uber Standard Lit"))
                    {
                        material.shader = Shader.Find("BOXOPHOBIC/The Visual Engine/Geometry/Plant Standard Lit");

                        material.SetFloat("_SizeFadeStartValue", 0);
                        material.SetFloat("_SizeFadeEndValue", 0);
                    }

                    if (material.shader.name == ("Hidden/BOXOPHOBIC/The Visual Engine/Geometry/Uber Subsurface Lit"))
                    {
                        material.shader = Shader.Find("BOXOPHOBIC/The Visual Engine/Geometry/Plant Subsurface Lit");

                        material.SetFloat("_SizeFadeStartValue", 0);
                        material.SetFloat("_SizeFadeEndValue", 0);
                    }

                    // Upgrade to 14
                    if (material.shader.name == ("Hidden/BOXOPHOBIC/The Visual Engine/Geometry/Prop Standard Lit"))
                    {
                        material.shader = Shader.Find("BOXOPHOBIC/The Visual Engine/Geometry Standard Lit");

                        material.SetFloat("_TintingIntensityValue", 0);
                        material.SetFloat("_DrynessIntensityValue", 0);
                        material.SetFloat("_ScaleIntensityValue", 0);
                        material.SetFloat("_MotionIntenityValue", 0);
                    }

                    if (material.shader.name == ("Hidden/BOXOPHOBIC/The Visual Engine/Geometry/Prop Subsurface Lit"))
                    {
                        material.shader = Shader.Find("BOXOPHOBIC/The Visual Engine/Geometry Subsurface Lit");

                        material.SetFloat("_TintingIntensityValue", 0);
                        material.SetFloat("_DrynessIntensityValue", 0);
                        material.SetFloat("_ScaleIntensityValue", 0);
                        material.SetFloat("_MotionIntenityValue", 0);
                    }
                }

                if (version < 1100)
                {
                    if (material.shader.name == ("Hidden/BOXOPHOBIC/The Visual Engine/Geometry/Bark Standard Lit"))
                    {
                        material.shader = Shader.Find("BOXOPHOBIC/The Visual Engine/Geometry/Plant Standard Lit");

                        material.SetFloat("_GlobalColors", 0);
                        material.SetFloat("_GlobalAlpha", 0);
                        material.SetFloat("_SubsurfaceValue", 0);
                        material.SetFloat("_MotionAmplitude_32", 0);

#if UNITY_2022_1_OR_NEWER && UNITY_EDITOR
                        material.SetPropertyLock("_GlobalColors", true);
                        material.SetPropertyLock("_GlobalAlpha", true);
                        material.SetPropertyLock("_SubsurfaceValue", true);
                        material.SetPropertyLock("_MotionAmplitude_32", true);
#endif
                    }

                    if (material.shader.name == ("Hidden/BOXOPHOBIC/The Visual Engine/Geometry/Bark Vertex Lit (Mobile)"))
                    {
                        material.shader = Shader.Find("BOXOPHOBIC/The Visual Engine/Geometry/Plant Vertex Lit (Mobile)");

                        material.SetFloat("_GlobalColors", 0);
                        material.SetFloat("_GlobalAlpha", 0);
                        material.SetFloat("_SubsurfaceValue", 0);
                        material.SetFloat("_MotionAmplitude_32", 0);

#if UNITY_2022_1_OR_NEWER && UNITY_EDITOR
                        material.SetPropertyLock("_GlobalColors", true);
                        material.SetPropertyLock("_GlobalAlpha", true);
                        material.SetPropertyLock("_SubsurfaceValue", true);
                        material.SetPropertyLock("_MotionAmplitude_32", true);
#endif
                    }

                    if (material.shader.name == ("Hidden/BOXOPHOBIC/The Visual Engine/Geometry/Bark Simple Lit (Mobile)"))
                    {
                        material.shader = Shader.Find("BOXOPHOBIC/The Visual Engine/Geometry/Plant Simple Lit (Mobile)");

                        material.SetFloat("_GlobalColors", 0);
                        material.SetFloat("_GlobalAlpha", 0);
                        material.SetFloat("_SubsurfaceValue", 0);
                        material.SetFloat("_MotionAmplitude_32", 0);

#if UNITY_2022_1_OR_NEWER && UNITY_EDITOR
                        material.SetPropertyLock("_GlobalColors", true);
                        material.SetPropertyLock("_GlobalAlpha", true);
                        material.SetPropertyLock("_SubsurfaceValue", true);
                        material.SetPropertyLock("_MotionAmplitude_32", true);
#endif
                    }

                    if (material.shader.name == ("Hidden/BOXOPHOBIC/The Visual Engine/Geometry/Bark Standard Lit (Blanket)"))
                    {
                        material.shader = Shader.Find("BOXOPHOBIC/The Visual Engine/Geometry/Plant Standard Lit (Blanket)");

                        material.SetFloat("_GlobalColors", 0);
                        material.SetFloat("_GlobalAlpha", 0);
                        material.SetFloat("_SubsurfaceValue", 0);
                        material.SetFloat("_MotionAmplitude_32", 0);

#if UNITY_2022_1_OR_NEWER && UNITY_EDITOR
                        material.SetPropertyLock("_GlobalColors", true);
                        material.SetPropertyLock("_GlobalAlpha", true);
                        material.SetPropertyLock("_SubsurfaceValue", true);
                        material.SetPropertyLock("_MotionAmplitude_32", true);
#endif
                    }

                    // Disable Coloring for Props
                    if (material.shader.name.Contains("Prop"))
                    {
                        material.SetFloat("_GlobalColors", 0);
                        material.SetFloat("_GlobalAlpha", 0);

#if UNITY_2022_1_OR_NEWER && UNITY_EDITOR
                        material.SetPropertyLock("_GlobalColors", true);
                        material.SetPropertyLock("_GlobalAlpha", true);
#endif
                    }
                }

                // Update settings
                if (version < 500)
                {
                    if (material.HasProperty("_RenderPriority"))
                    {
                        if (material.GetInt("_RenderPriority") != 0)
                        {
                            material.SetInt("_RenderQueue", 1);
                        }
                    }

                    material.SetInt("_IsVersion", 500);
                }

                if (version < 600)
                {
                    if (material.HasProperty("_LayerReactValue"))
                    {
                        material.SetInt("_LayerVertexValue", material.GetInt("_LayerReactValue"));
                    }

                    material.SetInt("_IsVersion", 600);
                }

                if (version < 620)
                {
                    if (material.HasProperty("_VertexRollingMode"))
                    {
                        material.SetInt("_MotionValue_20", material.GetInt("_VertexRollingMode"));
                    }

                    material.SetInt("_IsVersion", 620);
                }

                if (version < 630)
                {
                    material.DisableKeyword("TVE_DETAIL_BLEND_OVERLAY");
                    material.DisableKeyword("TVE_DETAIL_BLEND_REPLACE");

                    material.SetInt("_IsVersion", 630);
                }

                if (version < 640)
                {
                    if (material.HasProperty("_Cutoff"))
                    {
                        material.SetFloat("_AlphaCutoffValue", material.GetFloat("_Cutoff"));
                    }

                    material.SetInt("_IsVersion", 640);
                }

                if (version < 650)
                {
                    if (material.HasProperty("_Cutoff"))
                    {
                        material.SetFloat("_AlphaClipValue", material.GetFloat("_Cutoff"));
                    }

                    if (material.HasProperty("_MotionValue_20"))
                    {
                        material.SetFloat("_MotionValue_20", 1);
                    }

                    // Guess best values for squash motion
                    if (material.HasProperty("_MotionScale_20") && material.HasProperty("_MaxBoundsInfo"))
                    {
                        var bounds = material.GetVector("_MaxBoundsInfo");
                        var scale = Mathf.Round((1.0f / bounds.y * 10.0f * 0.5f) * 10) / 10;

                        if (scale > 1)
                        {
                            scale = Mathf.Clamp(Mathf.FloorToInt(scale), 0, 20);
                        }

                        material.SetFloat("_MotionScale_20", scale);
                    }

                    if (material.shader.name.Contains("Bark"))
                    {
                        material.SetFloat("_DetailCoordMode", 1);
                    }

                    material.DisableKeyword("TVE_ALPHA_CLIP");
                    material.DisableKeyword("TVE_DETAIL_MODE_ON");
                    material.DisableKeyword("TVE_DETAIL_MODE_OFF");
                    material.DisableKeyword("TVE_DETAIL_TYPE_VERTEX_BLUE");
                    material.DisableKeyword("TVE_DETAIL_TYPE_PROJECTION");
                    material.DisableKeyword("TVE_IS_VEGETATION_SHADER");
                    material.DisableKeyword("TVE_IS_GRASS_SHADER");

                    material.SetInt("_IsVersion", 650);
                }

                if (version < 710)
                {
                    if (material.HasProperty("_MotionScale_20"))
                    {
                        var scale = material.GetFloat("_MotionScale_20");

                        material.SetFloat("_MotionScale_20", scale * 10.0f);
                    }

                    material.SetInt("_IsVersion", 710);
                }

                if (version < 800)
                {
                    if (material.HasProperty("_ColorsMaskMinValue") && material.HasProperty("_ColorsMaskMaxValue"))
                    {
                        var min = material.GetFloat("_ColorsMaskMinValue");
                        var max = material.GetFloat("_ColorsMaskMaxValue");

                        material.SetFloat("_MainMaskMinValue", min);
                        material.SetFloat("_MainMaskMaxValue", max);
                    }

                    if (material.HasProperty("_LeavesFilterMode") && material.HasProperty("_LeavesFilterColor"))
                    {
                        var mode = material.GetInt("_LeavesFilterMode");
                        var color = material.GetColor("_LeavesFilterColor");

                        if (mode == 1)
                        {
                            if (color.r < 0.1f && color.g < 0.1f && color.b < 0.1f)
                            {
                                material.SetFloat("_GlobalColors", 0);
                                material.SetFloat("_MotionValue_30", 0);
                            }
                        }
                    }

                    if (material.HasProperty("_DetailMeshValue"))
                    {
                        material.SetFloat("_DetailMeshValue", 0);
                        material.SetFloat("_DetailBlendMinValue", 0.4f);
                        material.SetFloat("_DetailBlendMaxValue", 0.6f);
                    }

                    material.SetInt("_IsVersion", 800);
                }

                if (version < 810)
                {
                    if (material.HasProperty("_GlobalColors"))
                    {
                        var value = material.GetFloat("_GlobalColors");

                        material.SetFloat("_GlobalColors", Mathf.Clamp01(value * 2.0f));
                    }

                    if (material.HasProperty("_VertexOcclusionColor"))
                    {
                        var color = material.GetColor("_VertexOcclusionColor");
                        var alpha = (color.r + color.g + color.b + 0.001f) / 3.0f;

                        color.a = Mathf.Clamp01(alpha);

                        material.SetColor("_VertexOcclusionColor", color);
                    }

                    material.SetInt("_IsIdentifier", 0);
                    material.SetInt("_IsVersion", 810);
                }

                if (version < 830)
                {
                    material.SetFloat("_OverlayProjectionValue", 0.6f);

                    material.SetInt("_IsVersion", 830);
                }

                if (version < 850)
                {
                    if (material.HasProperty("_DetailOpaqueMode"))
                    {
                        var mode = material.GetInt("_DetailOpaqueMode");

                        material.SetInt("_DetailFadeMode", 1 - mode);
                    }

                    if (material.HasProperty("_DetailTypeMode"))
                    {
                        var mode = material.GetInt("_DetailTypeMode");

                        if (mode == 1)
                        {
                            material.SetInt("_DetailCoordMode", 2);
                        }

                        // Transfer Type to Mesh variable
                        material.SetInt("_DetailMeshMode", material.GetInt("_DetailTypeMode"));
                    }

                    if (material.HasProperty("_DetailCoordMode"))
                    {
                        // Transfer Detail Coord to Second Coord
                        material.SetInt("_SecondUVsMode", material.GetInt("_DetailCoordMode"));
                    }

                    if (material.HasProperty("_EmissiveFlagMode"))
                    {
                        int mode = material.GetInt("_EmissiveFlagMode");

                        if (mode == 0)
                        {
                            material.SetInt("_EmissiveFlagMode", 0);
                        }
                        else if (mode == 10)
                        {
                            material.SetInt("_EmissiveFlagMode", 1);
                        }
                        else if (mode == 20)
                        {
                            material.SetInt("_EmissiveFlagMode", 2);
                        }
                        else if (mode == 30)
                        {
                            material.SetInt("_EmissiveFlagMode", 3);
                        }
                    }

                    if (material.HasProperty("_EmissiveIntensityParams"))
                    {
                        var value = 1.0f;
                        var param = material.GetVector("_EmissiveIntensityParams");

                        if (param.w == 0)
                        {
                            value = param.y;
                            material.SetInt("_EmissiveIntensityMode", 0);
                        }
                        else
                        {
                            value = param.z;
                            material.SetInt("_EmissiveIntensityMode", 1);
                        }

                        material.SetFloat("_EmissiveIntensityValue", value);
                    }

                    material.SetInt("_IsVersion", 850);
                }

                if (version < 900)
                {
                    material.SetFloat("_DetailMeshValue", 1);
                    material.SetFloat("_DetailMaskValue", 1);

                    material.SetInt("_IsVersion", 900);
                }

                if (version < 1000)
                {
                    material.SetInt("_IsIdentifier", (int)UnityEngine.Random.Range(1, 100));

                    if (material.HasProperty("_DetailMeshInvertMode"))
                    {
                        var mode = material.GetInt("_DetailMeshInvertMode");

                        if (mode == 0)
                        {
                            material.SetFloat("_DetailMeshMinValue", 0);
                            material.SetFloat("_DetailMeshMaxValue", 1);
                        }
                        else
                        {
                            material.SetFloat("_DetailMeshMinValue", 1);
                            material.SetFloat("_DetailMeshMaxValue", 0);
                        }
                    }

                    if (material.HasProperty("_DetailMaskInvertMode"))
                    {
                        var mode = material.GetInt("_DetailMaskInvertMode");

                        if (mode == 0)
                        {
                            material.SetFloat("_DetailMaskMinValue", 0);
                            material.SetFloat("_DetailMaskMaxValue", 1);
                        }
                        else
                        {
                            material.SetFloat("_DetailMaskMinValue", 1);
                            material.SetFloat("_DetailMaskMaxValue", 0);
                        }
                    }

                    material.SetInt("_IsVersion", 1000);
                }

                if (version < 1100)
                {
                    if (material.HasProperty("_MotionValue_20"))
                    {
                        var mode = material.GetInt("_MotionValue_20");

                        if (mode == 0)
                        {
                            material.SetFloat("_MotionAmplitude_20", 0);
                            material.SetFloat("_MotionAmplitude_22", 0);

#if UNITY_2022_1_OR_NEWER && UNITY_EDITOR
                            material.SetPropertyLock("_MotionAmplitude_20", true);
                            material.SetPropertyLock("_MotionAmplitude_22", true);
#endif
                        }
                    }

                    if (material.HasProperty("_MotionValue_30"))
                    {
                        var mode = material.GetInt("_MotionValue_30");

                        if (mode == 0)
                        {
                            material.SetFloat("_MotionAmplitude_32", 0);

#if UNITY_2022_1_OR_NEWER && UNITY_EDITOR
                            material.SetPropertyLock("_MotionAmplitude_32", true);
#endif
                        }
                    }

                    material.SetInt("_IsVersion", 1100);
                }

                // Bumped version because 1200 was used before by mistake
                if (version < 1201)
                {
                    if (material.HasProperty("_EmissiveColor"))
                    {
                        var color = material.GetColor("_EmissiveColor");

                        if (material.GetColor("_EmissiveColor").r > 0 || material.GetColor("_EmissiveColor").g > 0 || material.GetColor("_EmissiveColor").b > 0)
                        {
                            material.SetInt("_EmissiveMode", 1);
                        }
                    }

                    material.SetInt("_IsVersion", 1201);
                }

                // Refresh is needed to apply new keywords
                if (version < 1230)
                {
                    material.SetInt("_IsVersion", 1230);
                }

#if UNITY_EDITOR
                if (version < 1400)
                {
                    int isPlant = 1;

                    string oldShader = material.shader.name;
                    Shader newShader = null;

                    if (oldShader.Contains("Prop"))
                    {
                        isPlant = 0;
                    }

                    if (!oldShader.Contains("Lite"))
                    {
                        if (oldShader.Contains("Blanket"))
                        {
                            if (oldShader.Contains("Standard"))
                            {
                                newShader = Shader.Find("BOXOPHOBIC/The Visual Engine/Geometry/Blanket Standard Lit");
                            }

                            if (oldShader.Contains("Subsurface"))
                            {
                                newShader = Shader.Find("BOXOPHOBIC/The Visual Engine/Geometry/Blanket Subsurface Lit");
                            }
                        }
                        else if (oldShader.Contains("Mobile"))
                        {
                            newShader = Shader.Find("BOXOPHOBIC/The Visual Engine/Geometry/General Simple Lit");
                        }
                        else if (oldShader.Contains("Polygonal"))
                        {
                            newShader = Shader.Find("BOXOPHOBIC/The Visual Engine/Geometry/General Standard Lit");
                        }
                        else if (oldShader.Contains("Impostor"))
                        {
                            if (oldShader.Contains("Standard"))
                            {
                                if (oldShader.Contains("Octa"))
                                {
                                    newShader = Shader.Find("BOXOPHOBIC/The Visual Engine/Impostors/Octa Standard Lit");
                                }

                                if (oldShader.Contains("Hemi"))
                                {
                                    newShader = Shader.Find("BOXOPHOBIC/The Visual Engine/Impostors/Hemi Standard Lit");
                                }

                                if (oldShader.Contains("Spherical"))
                                {
                                    newShader = Shader.Find("BOXOPHOBIC/The Visual Engine/Impostors/Spherical Standard Lit");
                                }
                            }

                            if (oldShader.Contains("Subsurface"))
                            {
                                if (oldShader.Contains("Octa"))
                                {
                                    newShader = Shader.Find("BOXOPHOBIC/The Visual Engine/Impostors/Octa Subsurface Lit");
                                }

                                if (oldShader.Contains("Hemi"))
                                {
                                    newShader = Shader.Find("BOXOPHOBIC/The Visual Engine/Impostors/Hemi Subsurface Lit");
                                }

                                if (oldShader.Contains("Spherical"))
                                {
                                    newShader = Shader.Find("BOXOPHOBIC/The Visual Engine/Impostors/Spherical Subsurface Lit");
                                }
                            }
                        }
                        else
                        {
                            if (oldShader.Contains("Standard"))
                            {
                                newShader = Shader.Find("BOXOPHOBIC/The Visual Engine/Geometry/General Standard Lit");
                            }

                            if (oldShader.Contains("Subsurface"))
                            {
                                newShader = Shader.Find("BOXOPHOBIC/The Visual Engine/Geometry/General Subsurface Lit");
                            }
                        }

                        if (newShader != null)
                        {
                            material.shader = newShader;
                        }
                    }

                    if (oldShader.Contains("Lite"))
                    {
                        if (oldShader.Contains("Impostor"))
                        {
                            if (oldShader.Contains("Standard"))
                            {
                                newShader = Shader.Find("BOXOPHOBIC/The Visual Engine/Impostors/Octa Standard Lit (Lite)");
                            }

                            if (oldShader.Contains("Subsurface"))
                            {
                                newShader = Shader.Find("BOXOPHOBIC/The Visual Engine/Impostors/Octa Subsurface Lit (Lite)");
                            }
                        }
                        else
                        {
                            if (oldShader.Contains("Standard"))
                            {
                                newShader = Shader.Find("BOXOPHOBIC/The Visual Engine/Geometry/General Standard Lit (Lite)");
                            }

                            if (oldShader.Contains("Subsurface"))
                            {
                                newShader = Shader.Find("BOXOPHOBIC/The Visual Engine/Geometry/General Subsurface Lit (Lite)");
                            }
                        }

                        if (newShader != null)
                        {
                            material.shader = newShader;
                        }
                    }

                    // Render
                    material.SetFloat("_RenderNormal", GetMaterialSerializedFloat(material, "_RenderNormals", 0.0f));

                    // Globals
                    material.SetFloat("_GlobalPaintLayerValue", GetMaterialSerializedFloat(material, "_LayerColorsValue", 0.0f));
                    material.SetFloat("_GlobalGlowLayerValue", GetMaterialSerializedFloat(material, "_LayerColorsValue", 0.0f));
                    material.SetFloat("_GlobalAtmoLayerValue", GetMaterialSerializedFloat(material, "_LayerExtrasValue", 0.0f));
                    material.SetFloat("_GLobalFormLayerValue", GetMaterialSerializedFloat(material, "_LayerVertexValue", 0.0f));
                    material.SetFloat("_GlobalWindLayerValue", GetMaterialSerializedFloat(material, "_LayerMotionValue", 0.0f));
                    material.SetFloat("_GlobalPushLayerValue", GetMaterialSerializedFloat(material, "_LayerMotionValue", 0.0f));

                    material.SetFloat("_GlobalPaintPivotValue", GetMaterialSerializedFloat(material, "_ColorsPositionMode", 0.0f));
                    material.SetFloat("_GlobalGlowPivotValue", GetMaterialSerializedFloat(material, "_ColorsPositionMode", 0.0f));
                    material.SetFloat("_GlobalAtmoPivotValue", GetMaterialSerializedFloat(material, "_ExtrasPositionMode", 0.0f));
                    material.SetFloat("_GlobalFormPivotValue", GetMaterialSerializedFloat(material, "_VertexPositionMode", 1.0f));

                    material.SetFloat("_TintingIntensityValue", GetMaterialSerializedFloat(material, "_GlobalColors", 0.0f));
                    material.SetFloat("_OverlayIntensityValue", GetMaterialSerializedFloat(material, "_GlobalOverlay", 0.0f));
                    material.SetFloat("_OverlayProjValue", GetMaterialSerializedFloat(material, "_OverlayProjectionValue", 0.5f));
                    material.SetFloat("_WetnessIntensityValue", GetMaterialSerializedFloat(material, "_GlobalWetness", 0.0f));
                    material.SetFloat("_EmissiveElementMode", GetMaterialSerializedFloat(material, "_GlobalEmissive", 0.0f));
                    material.SetFloat("_CutoutIntensityValue", GetMaterialSerializedFloat(material, "_GlobalAlpha", 0.0f) * isPlant);
                    material.SetFloat("_HeightIntensityValue", GetMaterialSerializedFloat(material, "_GlobalHeight", 0.0f));
                    material.SetFloat("_BlanketConformValue", GetMaterialSerializedFloat(material, "_GlobalConform", 0.0f));
                    material.SetFloat("_BlanketOrientationValue", GetMaterialSerializedFloat(material, "_GlobalOrientation", 0.0f));

                    if (isPlant == 1)
                    {
                        material.SetFloat("_OverlayTextureMode", 0);
                        material.SetFloat("_WetnessDropsIntensityValue", 0);
                    }
                    else
                    {
                        material.SetFloat("_OverlayTextureMode", 1);
                        material.SetFloat("_WetnessDropsIntensityValue", 1);
                    }

                    // Object
                    material.SetFloat("_ObjectModelMode", 0);

                    var pivotsMode = GetMaterialSerializedFloat(material, "_VertexPivotMode", 0.0f);

                    material.SetFloat("_ObjectPivotMode", pivotsMode);

                    var boundsRadius = 1.0f;
                    var boundsHeight = 1.0f;

                    var boundsData = GetMaterialSerializedVector(material, "_MaxBoundsInfo", Vector4.one);

                    if (boundsData.x == 0)
                    {
                        boundsRadius = GetMaterialSerializedFloat(material, "_BoundsRadiusValue", 1.0f);
                    }
                    else
                    {
                        boundsRadius = boundsData.x;
                    }

                    if (boundsData.y == 0)
                    {
                        boundsHeight = GetMaterialSerializedFloat(material, "_BoundsHeightValue", 1.0f);
                    }
                    else
                    {
                        boundsHeight = boundsData.y;
                    }

                    material.SetFloat("_ObjectRadiusValue", Mathf.Round(boundsRadius * 100) / 100);
                    material.SetFloat("_ObjectHeightValue", Mathf.Round(boundsHeight * 100) / 100);

                    // Main
                    material.SetTexture("_MainShaderTex", GetMaterialSerializedTexture(material, "_MainMaskTex", null));
                    material.SetFloat("_MainCoordMode", GetMaterialSerializedFloat(material, "_MainUVScaleMode", 0));
                    material.SetVector("_MainCoordValue", GetMaterialSerializedVector(material, "_MainUVs", new Vector4(1, 1, 0, 0)));
                    material.SetFloat("_MainAlphaClipValue", GetMaterialSerializedFloat(material, "_AlphaClipValue", 0.5f));

                    material.SetVector("_MainMultiRemap", GetMaterialSerializedVector(material, "_MainMaskMinValue", "_MainMaskMaxValue", Vector4.zero));

                    // Second
                    var secondMode = (int)GetMaterialSerializedFloat(material, "_DetailMode", 0.0f);

                    if (secondMode == 0)
                    {
                        material.SetFloat("_SecondIntensityValue", 0);
                    }

                    if (secondMode == 1)
                    {
                        material.SetFloat("_SecondIntensityValue", GetMaterialSerializedFloat(material, "_DetailValue", 0.0f));
                    }

                    material.SetTexture("_SecondShaderTex", GetMaterialSerializedTexture(material, "_SecondMaskTex", null));

                    material.SetFloat("_SecondSampleMode", GetMaterialSerializedFloat(material, "_SecondUVsMode", 0));
                    material.SetFloat("_SecondCoordMode", GetMaterialSerializedFloat(material, "_SecondUVScaleMode", 0));
                    material.SetVector("_SecondCoordValue", GetMaterialSerializedVector(material, "_SecondUVs", new Vector4(1, 1, 0, 0)));

                    material.SetFloat("_SecondAlphaClipValue", GetMaterialSerializedFloat(material, "_AlphaClipValue", 0.5f));
                    material.SetVector("_SecondMultiRemap", GetMaterialSerializedVector(material, "_SecondMaskMinValue", "_SecondMaskMaxValue", Vector4.zero));

                    material.SetFloat("_SecondBlendAlbedoValue", 1 - GetMaterialSerializedFloat(material, "_DetailBlendMode", 1.0f));
                    material.SetFloat("_SecondBlendNormalValue", GetMaterialSerializedFloat(material, "_DetailNormalValue", 0.0f));
                    material.SetFloat("_SecondBlendAlphaValue", 1 - GetMaterialSerializedFloat(material, "_DetailAlphaMode", 1.0f));

                    var secondMeshMode = (int)GetMaterialSerializedFloat(material, "_DetailMeshMode", 0.0f);

                    if (secondMeshMode == 0)
                    {
                        material.SetFloat("_SecondProjValue", 0);
                        material.SetVector("_SecondProjRemap", Vector4.zero);
                        material.SetFloat("_SecondMeshValue", 1);
                        material.SetVector("_SecondMeshRemap", GetMaterialSerializedVector(material, "_SecondMeshMinValue", "_SecondMeshMaxValue", new Vector4(0, 1, 0, 0)));
                    }

                    if (secondMeshMode == 1)
                    {
                        material.SetFloat("_SecondProjValue", 1);
                        material.SetVector("_SecondProjRemap", GetMaterialSerializedVector(material, "_SecondMeshMinValue", "_SecondMeshMaxValue", new Vector4(0, 1, 0, 0)));
                        material.SetFloat("_SecondMeshValue", 0);
                        material.SetVector("_SecondMeshRemap", Vector4.zero);
                    }

                    var secondMaskMode = (int)GetMaterialSerializedFloat(material, "_DetailMaskMode", 0.0f);

                    if (secondMaskMode == 0)
                    {
                        material.SetTexture("_SecondMaskTex", GetMaterialSerializedTexture(material, "_MainMaskTex", null));
                    }

                    if (secondMaskMode == 1)
                    {
                        material.SetTexture("_SecondMaskTex", GetMaterialSerializedTexture(material, "_SecondMaskTex", null));
                    }

                    var secondMaskRemap = GetMaterialSerializedVector(material, "_DetailMaskMinValue", "_DetailMaskMaxValue", new Vector4(0, 1, 0, 0));

                    if (secondMaskRemap.x == 0 && secondMaskRemap.y == 0)
                    {
                        material.SetFloat("_SecondMaskValue", 0);
                        material.SetVector("_SecondMaskRemap", new Vector4(0, 1, 0, 0));
                    }
                    else
                    {
                        material.SetFloat("_SecondMaskValue", 1);
                        material.SetVector("_SecondMaskRemap", GetMaterialSerializedVector(material, "_DetailMaskMinValue", "_DetailMaskMaxValue", new Vector4(0, 1, 0, 0)));
                    }

                    material.SetVector("_SecondBlendRemap", GetMaterialSerializedVector(material, "_DetailBlendMinValue", "_DetailBlendMaxValue", new Vector4(0, 1, 0, 0)));

                    // Terrain
                    material.SetFloat("_TerrainIntensityValue", GetMaterialSerializedFloat(material, "_TerrainMode", 0.0f));
                    material.SetFloat("_TerrainLandValue", GetMaterialSerializedFloat(material, "_TerrainBlendOffsetValue", 0.0f));
                    material.SetFloat("_NormalBlanketLandValue", GetMaterialSerializedFloat(material, "_TerrainBlendOffsetValue", 0.0f));

                    if (material.shader.name.Contains("Blanket") || material.shader.name.Contains("Terrain"))
                    {
                        material.SetTexture("_TerrainHolesTex", GetMaterialSerializedTexture(material, "_HolesTex", null));
                        material.SetTexture("_TerrainControlTex1", GetMaterialSerializedTexture(material, "_ControlTex1", null));
                        material.SetTexture("_TerrainControlTex2", GetMaterialSerializedTexture(material, "_ControlTex2", null));
                        material.SetTexture("_TerrainControlTex3", GetMaterialSerializedTexture(material, "_ControlTex3", null));
                        material.SetTexture("_TerrainControlTex4", GetMaterialSerializedTexture(material, "_ControlTex4", null));

                        for (int i = 1; i < 17; i++)
                        {
                            material.SetTexture("_TerrainAlbedoTex" + i, GetMaterialSerializedTexture(material, "_AlbedoTex" + i, null));
                            material.SetTexture("_TerrainNormalTex" + i, GetMaterialSerializedTexture(material, "_NormalTex" + i, null));
                            material.SetTexture("_TerrainShaderTex" + i, GetMaterialSerializedTexture(material, "_MaskTex" + i, null));
                            material.SetVector("_TerrainShaderMin" + i, GetMaterialSerializedVector(material, "_MaskMin" + i, Vector4.zero));
                            material.SetVector("_TerrainShaderMax" + i, GetMaterialSerializedVector(material, "_MaskMax" + i, Vector4.one));
                            material.SetVector("_TerrainParams" + i, GetMaterialSerializedVector(material, "_Params" + i, Vector4.one));
                            material.SetVector("_TerrainSpecular" + i, GetMaterialSerializedVector(material, "_Specular" + i, Vector4.zero));
                            material.SetVector("_TerrainCoord" + i, GetMaterialSerializedVector(material, "_Coords" + i, new Vector4(1, 1, 0, 0)));

                            material.SetFloat("_TerrainSampleMode" + i, GetMaterialSerializedFloat(material, "_LayerSampleMode" + i, 10) / 10 - 1);
                        }
                    }

                    // Occlusion
                    var occlusionColor = GetMaterialSerializedVector(material, "_VertexOcclusionColor", Vector4.one);
                    var occlusionValue = (occlusionColor.x + occlusionColor.y + occlusionColor.z) / 3;

                    if (occlusionValue != 1)
                    {
                        material.SetFloat("_OcclusionIntensityValue", 1);
                    }

                    material.SetVector("_OcclusionColorTwo", GetMaterialSerializedVector(material, "_VertexOcclusionColor", Vector4.one));
                    material.SetVector("_OcclusionMeshRemap", GetMaterialSerializedVector(material, "_VertexOcclusionMinValue", "_VertexOcclusionMaxValue", new Vector4(0, 1, 0, 0)));

                    // Gradient
                    if (isPlant == 1)
                    {
                        var gradientColor1 = GetMaterialSerializedVector(material, "_GradientColorOne", Vector4.one);
                        var gradientColor2 = GetMaterialSerializedVector(material, "_GradientColorTwo", Vector4.one);
                        var gradientValue = (gradientColor1.x + gradientColor1.y + gradientColor1.z + gradientColor2.x + gradientColor2.y + gradientColor2.z) / 6;

                        if (gradientValue != 1)
                        {
                            material.SetFloat("_GradientIntensityValue", 1);
                        }

                        material.SetVector("_GradientMeshRemap", GetMaterialSerializedVector(material, "_GradientMaskMinValue", "_GradientMaskMaxValue", new Vector4(0, 1, 0, 0)));
                    }

                    // Emissive
                    var emissiveMode = (int)GetMaterialSerializedFloat(material, "_EmissiveMode", 0.0f);

                    if (emissiveMode == 0)
                    {
                        material.SetFloat("_EmissiveIntensityValue", 0);
                    }

                    if (emissiveMode == 1)
                    {
                        material.SetFloat("_EmissiveIntensityValue", GetMaterialSerializedFloat(material, "_EmissivePhaseValue", 0.0f));
                    }

                    material.SetVector("_EmissiveCoordValue", GetMaterialSerializedVector(material, "_EmissiveUVs", new Vector4(1, 1, 0, 0)));

                    material.SetFloat("_EmissivePowerMode", GetMaterialSerializedFloat(material, "_EmissiveIntensityMode", 0.0f));
                    material.SetFloat("_EmissivePowerValue", GetMaterialSerializedFloat(material, "_EmissiveIntensityValue", 1.0f));
                    material.SetTexture("_EmissiveMaskTex", GetMaterialSerializedTexture(material, "_EmissiveTex", null));

                    var emissiveMaskRemap = GetMaterialSerializedVector(material, "_EmissiveTexMinValue", "_EmissiveTexMinValue", new Vector4(0, 1, 0, 0));

                    if (emissiveMaskRemap.x == 0 && emissiveMaskRemap.y == 0)
                    {
                        material.SetFloat("_EmissiveMaskValue", 0);
                        material.SetVector("_EmissiveMaskRemap", new Vector4(0, 1, 0, 0));
                    }
                    else
                    {
                        material.SetFloat("_EmissiveMaskValue", 1);
                        material.SetVector("_EmissiveMaskRemap", GetMaterialSerializedVector(material, "_EmissiveTexMinValue", "_EmissiveTexMaxValue", new Vector4(0, 1, 0, 0)));
                    }

                    // Subsurface
                    material.SetFloat("_SubsurfaceIntensityValue", GetMaterialSerializedFloat(material, "_SubsurfaceValue", 0.0f) * isPlant);
                    material.SetFloat("_SubsurfaceMultiValue", GetMaterialSerializedFloat(material, "_SubsurfaceMaskValue", 1.0f));

                    // Size Fade
                    if (GetMaterialSerializedFloat(material, "_SizeFadeStartValue", 0.0f) > 0 || GetMaterialSerializedFloat(material, "_SizeFadeEndValue", 0.0f) > 0 || GetMaterialSerializedFloat(material, "_GlobalSize", 0.0f) > 0)
                    {
                        material.SetFloat("_SizeFadeIntensityValue", 1);
                    }

                    material.SetFloat("_SizeFadeElementMode", GetMaterialSerializedFloat(material, "_GlobalSize", 1.0f));

                    material.SetFloat("_SizeFadeDistMinValue", GetMaterialSerializedFloat(material, "_SizeFadeStartValue", 0.0f));
                    material.SetFloat("_SizeFadeDistMaxValue", GetMaterialSerializedFloat(material, "_SizeFadeEndValue", 0.0f));

                    // Blanket
                    material.SetFloat("_BlanketConformOffsetMode", GetMaterialSerializedFloat(material, "_ConformOffsetMode", 1.0f));
                    material.SetFloat("_BlanketConformOffsetValue", GetMaterialSerializedFloat(material, "_ConformOffsetValue", 0.0f));

                    material.SetFloat("_NormalBlanketValue", GetMaterialSerializedFloat(material, "_TerrainBlendOffsetValue", 0.0f) * GetMaterialSerializedFloat(material, "_TerrainBlendNormalValue", 0.0f));

                    // Perspective
                    material.SetFloat("_PerspectiveIntensityValue", GetMaterialSerializedFloat(material, "_PerspectivePushValue", 0.0f));

                    // Motion
                    var highlight = GetMaterialSerializedVector(material, "_MotionHighlightColor", Vector4.zero);

                    material.SetFloat("_MotionHighlightValue", Mathf.Max(Mathf.Max(highlight.x, highlight.y), highlight.z) / 10 * isPlant);

                    material.SetFloat("_MotionSmallIntensityValue", GetMaterialSerializedFloat(material, "_MotionAmplitude_20", 0.0f) * isPlant);

                    // Must be tree
                    if (pivotsMode == 0)
                    {
                        material.SetFloat("_MotionBaseIntensityValue", GetMaterialSerializedFloat(material, "_MotionAmplitude_10", 0.0f) * 1.0f * isPlant);

                        //material.SetFloat("_MotionBasePivotValue", Mathf.Clamp01(boundsData.y / 10));
                        //material.SetFloat("_MotionSmallPivotValue", Mathf.Clamp01(boundsData.y / 20));

                        material.SetFloat("_MotionBasePivotValue", 0.8f);
                        material.SetFloat("_MotionSmallPivotValue", 0.6f);
                    }

                    // Must be grass
                    if (pivotsMode == 1)
                    {
                        material.SetFloat("_MotionBaseIntensityValue", GetMaterialSerializedFloat(material, "_MotionAmplitude_10", 0.0f) * 2.0f * isPlant);

                        material.SetFloat("_MotionBasePivotValue", 0.0f);
                        material.SetFloat("_MotionSmallPivotValue", 0.0f);
                    }

                    if (GetMaterialSerializedFloat(material, "_MotionVariation_10", 0.0f) > 0)
                    {
                        material.SetFloat("_MotionBasePhaseValue", 0.2f);
                    }

                    if (GetMaterialSerializedFloat(material, "_MotionVariation_20", 0.0f) > 0)
                    {
                        material.SetFloat("_MotionSmallPhaseValue", 0.5f);
                    }

                    material.SetFloat("_MotionTinyIntensityValue", GetMaterialSerializedFloat(material, "_MotionAmplitude_32", 0.0f) * isPlant);
                    material.SetFloat("_MotionTinyTillingValue", GetMaterialSerializedFloat(material, "_MotionScale_32", 20.0f));
                    material.SetFloat("_MotionTinySpeedValue", GetMaterialSerializedFloat(material, "_MotionSpeed_32", 20.0f));
                    material.SetFloat("_MotionTinyPhaseValue", GetMaterialSerializedFloat(material, "_MotionVariation_32", 0.0f) / 40);

                    material.SetFloat("_MotionPushIntensityValue", GetMaterialSerializedFloat(material, "_InteractionAmplitude", 0.0f) * isPlant);
                    material.SetFloat("_MotionPushElementMode", isPlant);
                    material.SetFloat("_MotionFrontValue", GetMaterialSerializedFloat(material, "_MotionFacingValue", 0.5f));

                    if (material.shader.name.Contains("Lite"))
                    {
                        var occlusionMask = GetMaterialSerializedFloat(material, "_VertexOcclusionMaskMode", 0.0f);

                        if (occlusionMask == 0)
                        {
                            material.SetFloat("_OcclusionMeshMode", 5);
                        }
                        else
                        {
                            material.SetFloat("_OcclusionMeshMode", occlusionMask / 10 - 1);
                        }

                        var gradientMask = GetMaterialSerializedFloat(material, "_GradientMaskMode", 0.0f);

                        if (gradientMask == 0)
                        {
                            material.SetFloat("_GradientMeshMode", 4);
                        }
                        else
                        {
                            material.SetFloat("_GradientMeshMode", occlusionMask / 10 - 1);
                        }

                        var phaseMode = GetMaterialSerializedFloat(material, "_MotionVariationMode", 0.0f);

                        if (phaseMode == 0)
                        {
                            material.SetFloat("_MotionBasePhaseValue", 0);
                            material.SetFloat("_MotionSmallPhaseValue", 0);
                        }
                        else
                        {
                            material.SetFloat("_ObjectPhaseMode", phaseMode / 10 - 1);
                        }

                        // Lite uses automatic height mask
                        material.SetFloat("_MotionBaseMaskMode", 4);

                        var motionSmallMask = GetMaterialSerializedFloat(material, "_MotionMaskMode_20", 0.0f);

                        if (motionSmallMask == 0)
                        {
                            material.SetFloat("_MotionSmallMaskMode", 5);
                        }
                        else
                        {
                            material.SetFloat("_MotionSmallMaskMode", motionSmallMask / 10 - 1);
                        }

                        var motionTinyMask = GetMaterialSerializedFloat(material, "_MotionMaskMode_30", 0.0f);

                        if (motionTinyMask == 0)
                        {
                            material.SetFloat("_MotionTinyMaskMode", 8);
                        }
                        else
                        {
                            material.SetFloat("_MotionTinyMaskMode", motionTinyMask / 10 - 1);
                        }
                    }

                    // Impostors
                    material.SetTexture("_MasksA", GetMaterialSerializedTexture(material, "_Mask", null));
                    material.SetFloat("_ImpostorAlphaClipValue", GetMaterialSerializedFloat(material, "_AI_Clip", 0.5f));

                    material.SetInt("_IsConverted", 1);
                    material.SetInt("_IsVersion", 1400);
                }

                if (version < 1410)
                {
                    // Second Layer
                    var secondMask = GetMaterialSerializedVector(material, "_SecondMaskRemap", Vector4.zero);

                    if (secondMask.x == 0 && secondMask.y == 0)
                    {
                        material.SetFloat("_SecondMaskValue", 0);
                        material.SetVector("_SecondMaskRemap", new Vector4(0, 1, 0, 0));
                    }
                    else
                    {
                        material.SetFloat("_SecondMaskValue", 1);
                    }

                    var secondProj = GetMaterialSerializedVector(material, "_SecondProjRemap", Vector4.zero);

                    if (secondProj.x == 0 && secondProj.y == 0)
                    {
                        material.SetFloat("_SecondProjValue", 0);
                        material.SetVector("_SecondProjRemap", new Vector4(0, 1, 0, 0));
                    }
                    else
                    {
                        material.SetFloat("_SecondProjValue", 1);
                    }

                    var secondMesh = GetMaterialSerializedVector(material, "_SecondMeshRemap", Vector4.zero);

                    if (secondMesh.x == 0 && secondMesh.y == 0)
                    {
                        material.SetFloat("_SecondMeshValue", 0);
                        material.SetVector("_SecondMeshRemap", new Vector4(0, 1, 0, 0));
                    }
                    else
                    {
                        material.SetFloat("_SecondMeshValue", 1);
                    }

                    // Third Layer
                    var thirdMask = GetMaterialSerializedVector(material, "_ThirdMaskRemap", Vector4.zero);

                    if (thirdMask.x == 0 && thirdMask.y == 0)
                    {
                        material.SetFloat("_ThirdMaskValue", 0);
                        material.SetVector("_ThirdMaskRemap", new Vector4(0, 1, 0, 0));
                    }
                    else
                    {
                        material.SetFloat("_ThirdMaskValue", 1);
                    }

                    var thirdProj = GetMaterialSerializedVector(material, "_ThirdProjRemap", Vector4.zero);

                    if (thirdProj.x == 0 && thirdProj.y == 0)
                    {
                        material.SetFloat("_ThirdProjValue", 0);
                        material.SetVector("_ThirdProjRemap", new Vector4(0, 1, 0, 0));
                    }
                    else
                    {
                        material.SetFloat("_ThirdProjValue", 1);
                    }

                    var thirdMesh = GetMaterialSerializedVector(material, "_ThirdMeshRemap", Vector4.zero);

                    if (thirdMesh.x == 0 && thirdMesh.y == 0)
                    {
                        material.SetFloat("_ThirdMeshValue", 0);
                        material.SetVector("_ThirdMeshRemap", new Vector4(0, 1, 0, 0));
                    }
                    else
                    {
                        material.SetFloat("_ThirdMeshValue", 1);
                    }

                    // Terrain Layer
                    var terrainMask = GetMaterialSerializedVector(material, "_TerrainMaskRemap", Vector4.zero);

                    if (terrainMask.x == 0 && terrainMask.y == 0)
                    {
                        material.SetFloat("_TerrainMaskValue", 0);
                        material.SetVector("_TerrainMaskRemap", new Vector4(0, 1, 0, 0));
                    }
                    else
                    {
                        material.SetFloat("_TerrainMaskValue", 1);
                    }

                    var terrainProj = GetMaterialSerializedVector(material, "_TerrainProjRemap", Vector4.zero);

                    if (terrainProj.x == 0 && terrainProj.y == 0)
                    {
                        material.SetFloat("_TerrainProjValue", 0);
                        material.SetVector("_TerrainProjRemap", new Vector4(0, 1, 0, 0));
                    }
                    else
                    {
                        material.SetFloat("_TerrainProjValue", 1);
                    }

                    // Tinting Layer
                    var tintingMesh = GetMaterialSerializedVector(material, "_TintingMeshRemap", Vector4.zero);

                    if (tintingMesh.x == 0 && tintingMesh.y == 0)
                    {
                        material.SetFloat("_TintingMeshValue", 0);
                        material.SetVector("_TintingMeshRemap", new Vector4(0, 1, 0, 0));
                    }
                    else
                    {
                        material.SetFloat("_TintingMeshValue", 1);
                    }

                    // Dryness Layer
                    var drynessMesh = GetMaterialSerializedVector(material, "_DrynessMeshRemap", Vector4.zero);

                    if (drynessMesh.x == 0 && drynessMesh.y == 0)
                    {
                        material.SetFloat("_DrynessMeshValue", 0);
                        material.SetVector("_DrynessMeshRemap", new Vector4(0, 1, 0, 0));
                    }
                    else
                    {
                        material.SetFloat("_DrynessMeshValue", 1);
                    }

                    // Overlay Layer
                    var overlayMesh = GetMaterialSerializedVector(material, "_OverlayMeshRemap", Vector4.zero);

                    if (overlayMesh.x == 0 && overlayMesh.y == 0)
                    {
                        material.SetFloat("_OverlayMeshValue", 0);
                        material.SetVector("_OverlayMeshRemap", new Vector4(0, 1, 0, 0));
                    }
                    else
                    {
                        material.SetFloat("_OverlayMeshValue", 1);
                    }

                    // Cutout Layer
                    var cutoutMesh = GetMaterialSerializedVector(material, "_CutoutMeshRemap", Vector4.zero);

                    if (cutoutMesh.x == 0 && cutoutMesh.y == 0)
                    {
                        material.SetFloat("_CutoutMeshValue", 0);
                        material.SetVector("_CutoutMeshRemap", new Vector4(0, 1, 0, 0));
                    }
                    else
                    {
                        material.SetFloat("_CutoutMeshValue", 1);
                    }

                    // Emissive Layer
                    var emissiveMask = GetMaterialSerializedVector(material, "_EmissiveMaskRemap", Vector4.zero);

                    if (emissiveMask.x == 0 && emissiveMask.y == 0)
                    {
                        material.SetFloat("_EmissiveMaskValue", 0);
                        material.SetVector("_EmissiveMaskRemap", new Vector4(0, 1, 0, 0));
                    }
                    else
                    {
                        material.SetFloat("_EmissiveMaskValue", 1);
                    }

                    var emissiveMesh = GetMaterialSerializedVector(material, "_EmissiveMeshRemap", Vector4.zero);

                    if (emissiveMesh.x == 0 && emissiveMesh.y == 0)
                    {
                        material.SetFloat("_EmissiveMeshValue", 0);
                        material.SetVector("_EmissiveMeshRemap", new Vector4(0, 1, 0, 0));
                    }
                    else
                    {
                        material.SetFloat("_EmissiveMeshValue", 1);
                    }

                    // Decoupe bounds from Small Motion
                    var objectRadius = GetMaterialSerializedFloat(material, "_ObjectRadiusValue", 1);
                    var motionSmall = GetMaterialSerializedFloat(material, "_MotionSmallIntensityValue", 0);

                    material.SetFloat("_MotionSmallIntensityValue", motionSmall * objectRadius);

                    material.SetInt("_IsVersion", 1410);
                }

                if (version < 2020)
                {
                    // Copy Fade Layer
                    material.SetFloat("_GlobalFadeLayerValue", GetMaterialSerializedFloat(material, "_GlobalAtmoLayerValue", 0));

                    // Typo Fix
                    material.SetFloat("_ThirdBlendIntensityValue", GetMaterialSerializedFloat(material, "_ThitdBlendIntensityValue", 1));
                    material.SetFloat("_ThirdBlendShaderValue", GetMaterialSerializedFloat(material, "_ThitdBlendShaderValue", 1));

                    // Terrain Land
                    var terrainLand = GetMaterialSerializedFloat(material, "_TerrainLandValue", 0);

                    if (terrainLand > 0)
                    {
                        material.SetFloat("_TerrainLandValue", 1);
                        material.SetFloat("_TerrainLandOffsetValue", GetMaterialSerializedFloat(material, "_TerrainLandValue", 0));
                    }
                    else
                    {
                        material.SetFloat("_TerrainLandOffsetValue", 0);
                    }

                    // Terrain Height
                    material.SetFloat("_LandscapeHeightValue", GetMaterialSerializedFloat(material, "_HeightIntensityValue", 0));

                    // Normal Proj
                    var normalProj = GetMaterialSerializedVector(material, "_NormalBlanketProjRemap", Vector4.zero);

                    if (normalProj.x == 0 && normalProj.y == 0)
                    {
                        material.SetFloat("_NormalProjValue", 0);
                        material.SetVector("_NormalProjRemap", new Vector4(0, 1, 0, 0));
                    }
                    else
                    {
                        material.SetFloat("_NormalProjValue", 1);

                        //material.SetFloat("_NormalBlanketValue", 1);
                    }

                    // Normal Land
                    var normalLand = GetMaterialSerializedFloat(material, "_NormalBlanketLandValue", 0);

                    if (normalLand > 0)
                    {
                        material.SetFloat("_NormalBlanketValue", 1);
                        material.SetFloat("_NormalLandValue", 1);
                        material.SetFloat("_NormalLandOffsetValue", GetMaterialSerializedFloat(material, "_NormalBlanketLandValue", 0));
                    }

                    material.SetInt("_IsVersion", 2020);
                }

                if (version < 2030)
                {
                    // Optimize tex if not used
                    if (material.HasProperty("_SecondMaskValue"))
                    {
                        var value = material.GetFloat("_SecondMaskValue");

                        if (value == 0)
                        {
                            if (material.HasProperty("_SecondMaskTex"))
                            {
                                material.SetTexture("_SecondMaskTex", null);
                            }
                        }
                    }

                    // Optimize tex if not used
                    if (material.HasProperty("_ThirdMaskValue"))
                    {
                        var value = material.GetFloat("_ThirdMaskValue");

                        if (value == 0)
                        {
                            if (material.HasProperty("_ThirdMaskTex"))
                            {
                                material.SetTexture("_ThirdMaskTex", null);
                            }
                        }
                    }

                    // Typo Fix
                    material.SetFloat("_TerrainMetallicValue", GetMaterialSerializedFloat(material, "_TerrainMetallicValue1", 1));

                    // Assign new RT texture
                    if (material.HasProperty("_MotionNoiseTex"))
                    {
                        material.SetTexture("_MotionNoiseTex", Resources.Load<CustomRenderTexture>("Internal MotionTexRT"));
                    }


                    material.SetInt("_IsVersion", 2030);
                }

                if (version < 2040)
                {
                    if (shaderName.Contains("Impostors"))
                    {
                        var impostorMask = GetMaterialSerializedTexture(material, "_MasksA", null);

                        if (impostorMask != null)
                        {
                            if (impostorMask.name.EndsWith("MasksA"))
                            {
                                // New shading format
                                material.SetInt("_ImpostorMaskMode", 3);
                                material.SetTexture("_Shader", impostorMask);
                            }
                            else
                            {
                                // Old packed format
                                material.SetInt("_ImpostorMaskMode", 2);
                                material.SetTexture("_Packed", impostorMask);
                            }
                        }

                        // Set previously not supported to 0
                        material.SetFloat("_TintingMeshValue", 0);
                        material.SetFloat("_DrynessMeshValue", 0);
                        material.SetFloat("_OverlayMeshValue", 0);
                        material.SetFloat("_CutoutMeshValue", 0);

                        material.SetFloat("_ImpostorOcclusionValue", 0);
                    }

                    // Typo fix
                    material.SetFloat("_GlobalLandPivotValue", GetMaterialSerializedFloat(material, "_GlobalLandPivotValue1", 0));
                    material.SetFloat("_GlobalLandLayerValue", GetMaterialSerializedFloat(material, "_GlobalLandLayerValue1", 0));

                    // Update Proximity
                    material.SetFloat("_DitherProximityMinValue", GetMaterialSerializedFloat(material, "_DitherProximityDistValue", 1));

                    // Set Motion Textures
                    if (material.HasProperty("_MotionNoiseTex"))
                    {
                        material.SetTexture("_MotionNoiseTex", Resources.Load<Texture2D>("Internal MotionTex"));
                    }

                    if (material.HasProperty("_MotionNoiseTexRT"))
                    {
                        material.SetTexture("_MotionNoiseTexRT", Resources.Load<CustomRenderTexture>("Internal MotionTexRT"));
                    }

                    // Copy Motion Settings
                    material.SetFloat("_MotionBaseSpeedValue", GetMaterialSerializedFloat(material, "_MotionSpeedValue", 5));
                    material.SetFloat("_MotionBaseTillingValue", GetMaterialSerializedFloat(material, "_MotionTillingValue", 5));

                    material.SetFloat("_MotionSmallSpeedValue", GetMaterialSerializedFloat(material, "_MotionSpeedValue", 5));
                    material.SetFloat("_MotionSmallTillingValue", GetMaterialSerializedFloat(material, "_MotionTillingValue", 5));

                    material.SetFloat("_MotionBaseDelayValue", GetMaterialSerializedFloat(material, "_MotionDelayValue", 0));

                    material.SetInt("_IsVersion", 2040);
                }
#endif
            }

            if (projectPipeline == "Standard")
            {
                material.EnableKeyword("TVE_PIPELINE_STANDARD");
                material.DisableKeyword("TVE_PIPELINE_UNIVERSAL");
                material.DisableKeyword("TVE_PIPELINE_HD");
            }

            if (projectPipeline == "Universal")
            {
                material.DisableKeyword("TVE_PIPELINE_STANDARD");
                material.EnableKeyword("TVE_PIPELINE_UNIVERSAL");
                material.DisableKeyword("TVE_PIPELINE_HD");
            }

            if (projectPipeline == "High Definition")
            {
                material.DisableKeyword("TVE_PIPELINE_STANDARD");
                material.DisableKeyword("TVE_PIPELINE_UNIVERSAL");
                material.EnableKeyword("TVE_PIPELINE_HD");
            }

            // Set Internal Render Values
            if (material.HasProperty("_RenderMode"))
            {
                material.SetInt("_render_mode", material.GetInt("_RenderMode"));
            }

            if (material.HasProperty("_RenderCull"))
            {
                material.SetInt("_render_cull", material.GetInt("_RenderCull"));
            }

            if (material.HasProperty("_RenderZWrite"))
            {
                material.SetInt("_render_zw", material.GetInt("_RenderZWrite"));
            }

            if (material.HasProperty("_RenderClip"))
            {
                material.SetInt("_render_clip", material.GetInt("_RenderClip"));
            }

            if (material.HasProperty("_RenderSpecular"))
            {
                material.SetInt("_render_specular", material.GetInt("_RenderSpecular"));
            }

            // Set Render Mode
            if (material.HasProperty("_RenderMode"))
            {
                int mode = material.GetInt("_RenderMode");
                int queue = 0;
                int priority = 0;
                int decals = 0;
                int clip = 0;

                if (material.HasProperty("_RenderQueue") && material.HasProperty("_RenderPriority"))
                {
                    queue = material.GetInt("_RenderQueue");
                    priority = material.GetInt("_RenderPriority");
                }

                if (projectPipeline == "High Definition")
                {
                    if (material.HasProperty("_RenderDecals"))
                    {
                        decals = material.GetInt("_RenderDecals");
                    }
                }

                if (material.HasProperty("_RenderClip"))
                {
                    clip = material.GetInt("_RenderClip");
                }

                // User Defined, render type changes needed
                if (queue == 2)
                {
                    if (material.renderQueue == 2000)
                    {
                        material.SetOverrideTag("RenderType", "Opaque");
                    }

                    if (material.renderQueue > 2449 && material.renderQueue < 3000)
                    {
                        material.SetOverrideTag("RenderType", "AlphaTest");
                    }

                    if (material.renderQueue > 2999)
                    {
                        material.SetOverrideTag("RenderType", "Transparent");
                    }
                }

                // Opaque
                if (mode == 0)
                {
                    if (queue != 2)
                    {
                        material.SetOverrideTag("RenderType", "AlphaTest");
                        //material.renderQueue = (int)UnityEngine.Rendering.RenderQueue.AlphaTest + priority;

                        if (clip == 0)
                        {
                            if (decals == 0)
                            {
                                material.renderQueue = 2000 + priority;
                            }
                            else
                            {
                                material.renderQueue = 2225 + priority;
                            }
                        }
                        else
                        {
                            if (decals == 0)
                            {
                                material.renderQueue = 2450 + priority;
                            }
                            else
                            {
                                material.renderQueue = 2475 + priority;
                            }
                        }
                    }

                    // Standard and Universal Render Pipeline
                    material.SetInt("_render_src", (int)UnityEngine.Rendering.BlendMode.One);
                    material.SetInt("_render_dst", (int)UnityEngine.Rendering.BlendMode.Zero);
                    material.SetInt("_render_zw", 1);
                    material.SetInt("_render_premul", 0);

                    // Set Main Color alpha to 1
                    if (material.HasProperty("_MainColor"))
                    {
                        var color = material.GetColor("_MainColor");
                        material.SetColor("_MainColor", new Color(color.r, color.g, color.b, 1.0f));
                    }

                    if (material.HasProperty("_MainColorTwo"))
                    {
                        var color = material.GetColor("_MainColorTwo");
                        material.SetColor("_MainColorTwo", new Color(color.r, color.g, color.b, 1.0f));
                    }

                    // HD Render Pipeline
                    material.DisableKeyword("_SURFACE_TYPE_TRANSPARENT");
                    material.DisableKeyword("_ENABLE_FOG_ON_TRANSPARENT");

                    material.DisableKeyword("_BLENDMODE_ALPHA");
                    material.DisableKeyword("_BLENDMODE_ADD");
                    material.DisableKeyword("_BLENDMODE_PRE_MULTIPLY");

                    material.SetInt("_RenderQueueType", 1);
                    material.SetInt("_SurfaceType", 0);
                    material.SetInt("_BlendMode", 0);
                    material.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.One);
                    material.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.Zero);
                    material.SetInt("_AlphaSrcBlend", (int)UnityEngine.Rendering.BlendMode.One);
                    material.SetInt("_AlphaDstBlend", (int)UnityEngine.Rendering.BlendMode.Zero);
                    material.SetInt("_ZWrite", 1);
                    material.SetInt("_TransparentZWrite", 1);
                    material.SetInt("_ZTestDepthEqualForOpaque", 3);

                    if (clip == 0)
                    {
                        material.SetInt("_ZTestGBuffer", 4);
                    }
                    else
                    {
                        material.SetInt("_ZTestGBuffer", 3);
                    }

                    //material.SetInt("_ZTestGBuffer", 4);
                    material.SetInt("_ZTestTransparent", 4);

                    material.SetShaderPassEnabled("TransparentBackface", false);
                    material.SetShaderPassEnabled("TransparentBackfaceDebugDisplay", false);
                    material.SetShaderPassEnabled("TransparentDepthPrepass", false);
                    material.SetShaderPassEnabled("TransparentDepthPostpass", false);
                }
                // Transparent
                else
                {
                    if (queue != 2)
                    {
                        material.SetOverrideTag("RenderType", "Transparent");
                        material.renderQueue = (int)UnityEngine.Rendering.RenderQueue.Transparent + priority;
                    }

                    int zwrite = 1;

                    if (material.HasProperty("_RenderZWrite"))
                    {
                        zwrite = material.GetInt("_RenderZWrite");
                    }

                    // Standard and Universal Render Pipeline
                    material.SetInt("_render_src", (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
                    material.SetInt("_render_dst", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
                    material.SetInt("_render_premul", 0);

                    // HD Render Pipeline
                    material.EnableKeyword("_SURFACE_TYPE_TRANSPARENT");
                    material.EnableKeyword("_ENABLE_FOG_ON_TRANSPARENT");

                    material.EnableKeyword("_BLENDMODE_ALPHA");
                    material.DisableKeyword("_BLENDMODE_ADD");
                    material.DisableKeyword("_BLENDMODE_PRE_MULTIPLY");

                    material.SetInt("_RenderQueueType", 5);
                    material.SetInt("_SurfaceType", 1);
                    material.SetInt("_BlendMode", 0);
                    material.SetInt("_SrcBlend", 1);
                    material.SetInt("_DstBlend", 10);
                    material.SetInt("_AlphaSrcBlend", 1);
                    material.SetInt("_AlphaDstBlend", 10);
                    material.SetInt("_ZWrite", zwrite);
                    material.SetInt("_TransparentZWrite", zwrite);
                    material.SetInt("_ZTestDepthEqualForOpaque", 4);
                    material.SetInt("_ZTestGBuffer", 4);
                    material.SetInt("_ZTestTransparent", 4);

                    material.SetShaderPassEnabled("TransparentBackface", true);
                    material.SetShaderPassEnabled("TransparentBackfaceDebugDisplay", true);
                    material.SetShaderPassEnabled("TransparentDepthPrepass", true);
                    material.SetShaderPassEnabled("TransparentDepthPostpass", true);
                }
            }

            // Set Receive Mode in HDRP
            if (projectPipeline == "High Definition")
            {
                if (material.HasProperty("_RenderDecals"))
                {
                    int decals = material.GetInt("_RenderDecals");

                    if (decals == 0)
                    {
                        material.EnableKeyword("_DISABLE_DECALS");
                    }
                    else
                    {
                        material.DisableKeyword("_DISABLE_DECALS");
                    }
                }

                if (material.HasProperty("_RenderSSR"))
                {
                    int ssr = material.GetInt("_RenderSSR");

                    if (ssr == 0)
                    {
                        material.EnableKeyword("_DISABLE_SSR");

                        material.SetInt("_StencilRef", 0);
                        material.SetInt("_StencilRefDepth", 0);
                        material.SetInt("_StencilRefDistortionVec", 4);
                        material.SetInt("_StencilRefGBuffer", 2);
                        material.SetInt("_StencilRefMV", 32);
                        material.SetInt("_StencilWriteMask", 6);
                        material.SetInt("_StencilWriteMaskDepth", 8);
                        material.SetInt("_StencilWriteMaskDistortionVec", 4);
                        material.SetInt("_StencilWriteMaskGBuffer", 14);
                        material.SetInt("_StencilWriteMaskMV", 40);
                    }
                    else
                    {
                        material.DisableKeyword("_DISABLE_SSR");

                        material.SetInt("_StencilRef", 0);
                        material.SetInt("_StencilRefDepth", 8);
                        material.SetInt("_StencilRefDistortionVec", 4);
                        material.SetInt("_StencilRefGBuffer", 10);
                        material.SetInt("_StencilRefMV", 40);
                        material.SetInt("_StencilWriteMask", 6);
                        material.SetInt("_StencilWriteMaskDepth", 8);
                        material.SetInt("_StencilWriteMaskDistortionVec", 4);
                        material.SetInt("_StencilWriteMaskGBuffer", 14);
                        material.SetInt("_StencilWriteMaskMV", 40);
                    }
                }
            }

            // Set Cull Mode
            if (material.HasProperty("_RenderCull"))
            {
                int cull = material.GetInt("_RenderCull");

                material.SetInt("_CullMode", cull);
                material.SetInt("_TransparentCullMode", cull);
                material.SetInt("_CullModeForward", cull);

                // Needed for HD Render Pipeline
                material.DisableKeyword("_DOUBLESIDED_ON");
            }

            // Set Clip Mode
            if (material.HasProperty("_RenderClip"))
            {
                int clip = material.GetInt("_RenderClip");
                float cutoff = 0.5f;

                if (material.HasProperty("_AlphaClipValue"))
                {
                    cutoff = material.GetFloat("_AlphaClipValue");
                }

                if (clip == 0)
                {
                    material.DisableKeyword("TVE_ALPHA_CLIP");

                    material.SetInt("_render_coverage", 0);
                }
                else
                {
                    material.EnableKeyword("TVE_ALPHA_CLIP");

                    if (material.HasProperty("_RenderCoverage") && material.HasProperty("_AlphaFeatherValue"))
                    {
                        material.SetInt("_render_coverage", material.GetInt("_RenderCoverage"));
                    }
                    else
                    {
                        material.SetInt("_render_coverage", 0);
                    }
                }

                material.SetFloat("_Cutoff", cutoff);

                // HD Render Pipeline
                material.SetFloat("_AlphaCutoff", cutoff);
                material.SetFloat("_AlphaCutoffPostpass", cutoff);
                material.SetFloat("_AlphaCutoffPrepass", cutoff);
                material.SetFloat("_AlphaCutoffShadow", cutoff);
            }
            else
            {
                // Impostors don't have render clip
                if (!material.HasProperty("_AlphaFeatherValue"))
                {
                    material.SetInt("_RenderCoverage", 0);
                }
            }

            // Set Normals Mode
            if (material.HasProperty("_RenderNormal") && material.HasProperty("_render_normal"))
            {
                int normals = material.GetInt("_RenderNormal");

                // Standard, Universal, HD Render Pipeline
                // Flip 0
                if (normals == 0)
                {
                    material.SetVector("_render_normal", new Vector4(-1, -1, -1, 0));
                    material.SetVector("_DoubleSidedConstants", new Vector4(-1, -1, -1, 0));
                }
                // Mirror 1
                else if (normals == 1)
                {
                    material.SetVector("_render_normal", new Vector4(1, 1, -1, 0));
                    material.SetVector("_DoubleSidedConstants", new Vector4(1, 1, -1, 0));
                }
                // None 2
                else if (normals == 2)
                {
                    material.SetVector("_render_normal", new Vector4(1, 1, 1, 0));
                    material.SetVector("_DoubleSidedConstants", new Vector4(1, 1, 1, 0));
                }
            }

            // Set Specular Mode
            if (material.HasProperty("_RenderSpecular"))
            {
                var mode = material.GetInt("_RenderSpecular");

                if (mode == 0)
                {
                    material.EnableKeyword("_SPECULARHIGHLIGHTS_OFF");
                }
                else
                {
                    material.DisableKeyword("_SPECULARHIGHLIGHTS_OFF");
                }
            }

            // Set Shadows Mode
            if (material.HasProperty("_RenderShadow"))
            {
                var mode = material.GetInt("_RenderShadow");

                if (mode == 0)
                {
                    material.EnableKeyword("_RECEIVE_SHADOWS_OFF");
                }
                else
                {
                    material.DisableKeyword("_RECEIVE_SHADOWS_OFF");
                }
            }

            if (material.HasProperty("_RenderDirect") && material.HasProperty("_render_direct"))
            {
                float value = material.GetFloat("_RenderDirect");

                material.SetFloat("_render_direct", value);
            }

            if (material.HasProperty("_RenderShadow") && material.HasProperty("_render_shadow"))
            {
                float value = material.GetFloat("_RenderShadow");

                material.SetFloat("_render_shadow", value);
            }

            if (material.HasProperty("_RenderAmbient") && material.HasProperty("_render_ambient"))
            {
                float value = material.GetFloat("_RenderAmbient");

                material.SetFloat("_render_ambient", value);
            }

#if UNITY_EDITOR
            // Assign Default HD Foliage profile
            if (material.HasProperty("_SubsurfaceDiffusion"))
            {
                // Workaround when the old HDRP 12 diffusion is not found
                if (material.GetFloat("_SubsurfaceDiffusion") == 3.5648174285888672f && AssetDatabase.GUIDToAssetPath("78322c7f82657514ebe48203160e3f39") == "")
                {
                    material.SetFloat("_SubsurfaceDiffusion", 0);
                }

                // Workaround when the old HDRP 14 diffusion is not found
                if (material.GetFloat("_SubsurfaceDiffusion") == 2.6486763954162598f && AssetDatabase.GUIDToAssetPath("879ffae44eefa4412bb327928f1a96dd") == "")
                {
                    material.SetFloat("_SubsurfaceDiffusion", 0);
                }

                // Search for one of Unity's diffusion profile
                if (material.GetFloat("_SubsurfaceDiffusion") == 0)
                {
                    // HDRP 12 Profile
                    if (AssetDatabase.GUIDToAssetPath("78322c7f82657514ebe48203160e3f39") != "")
                    {
                        material.SetFloat("_SubsurfaceDiffusion", 3.5648174285888672f);
                        material.SetVector("_SubsurfaceDiffusion_Asset", new Vector4(228889264007084710000000000000000000000f, 0.000000000000000000000000012389357880079404f, 0.00000000000000000000000000000000000076932702684439582f, 0.00018220426863990724f));
                    }

                    // HDRP 14 Profile
                    if (AssetDatabase.GUIDToAssetPath("879ffae44eefa4412bb327928f1a96dd") != "")
                    {
                        material.SetFloat("_SubsurfaceDiffusion", 2.6486763954162598f);
                        material.SetVector("_SubsurfaceDiffusion_Asset", new Vector4(-36985449400010195000000f, 20.616847991943359f, -0.00000000000000000000000000052916750040661612f, -1352014335655804900f));
                    }

                    // HDRP 16 Profile
                    if (AssetDatabase.GUIDToAssetPath("2384dbf2c1c420f45a792fbc315fbfb1") != "")
                    {
                        material.SetFloat("_SubsurfaceDiffusion", 3.8956573009490967f);
                        material.SetVector("_SubsurfaceDiffusion_Asset", new Vector4(-8695930962161997000000000000000f, -50949593547561853000000000000000f, -0.010710084810853004f, -0.0000000055696536271909736f));
                    }
                }
            }

            if (material.HasProperty("_IsConverted"))
            {
                var mode = material.GetInt("_IsConverted");

                if (mode == 0)
                {
                    Texture2D albedoTex = null;
                    Texture2D normalTex = null;
                    Texture2D shaderTex = null;

                    if (albedoTex == null)
                    {
                        albedoTex = GetMaterialSerializedTexture(material, "_MainTex", null);
                    }

                    if (albedoTex == null)
                    {
                        albedoTex = GetMaterialSerializedTexture(material, "_BaseMap", null);
                    }

                    if (albedoTex == null)
                    {
                        albedoTex = GetMaterialSerializedTexture(material, "_BaseColorMap", null);
                    }

                    if (normalTex == null)
                    {
                        normalTex = GetMaterialSerializedTexture(material, "_BumpMap", null);
                    }

                    if (normalTex == null)
                    {
                        normalTex = GetMaterialSerializedTexture(material, "_NormalMap", null);
                    }

                    if (shaderTex == null)
                    {
                        shaderTex = GetMaterialSerializedTexture(material, "_SpecGlossMap", null);
                    }

                    if (shaderTex == null)
                    {
                        shaderTex = GetMaterialSerializedTexture(material, "_MaskMap", null);
                    }

                    if (albedoTex != null)
                    {
                        material.SetTexture("_MainAlbedoTex", albedoTex);
                    }

                    if (normalTex != null)
                    {
                        material.SetTexture("_MainNormalTex", normalTex);
                    }

                    if (shaderTex != null)
                    {
                        material.SetTexture("_MainShaderTex", shaderTex);
                    }

                    material.SetInt("_IsConverted", 1);
                }
            }
#endif

            if (shaderName.Contains("Lite"))
            {
                if (material.HasProperty("_NoiseTex3D"))
                {
                    if (material.GetTexture("_NoiseTex3D") == null)
                    {
                        material.SetTexture("_NoiseTex3D", Resources.Load<Texture3D>("Internal NoiseTex3DLite"));
                    }
                }

                if (material.HasProperty("_OverlayNormalTex"))
                {
                    if (material.GetTexture("_OverlayNormalTex") == null)
                    {
                        material.SetTexture("_OverlayNormalTex", Resources.Load<Texture2D>("Internal SnowTexLite"));
                    }
                }

                if (material.HasProperty("_MotionNoiseTex"))
                {
                    if (material.GetTexture("_MotionNoiseTex") == null)
                    {
                        material.SetTexture("_MotionNoiseTex", Resources.Load<Texture2D>("Internal MotionTexLite"));
                    }
                }

                if (material.HasProperty("_MotionNoiseTexRT"))
                {
                    if (material.GetTexture("_MotionNoiseTexRT") == null)
                    {
                        material.SetTexture("_MotionNoiseTexRT", Resources.Load<CustomRenderTexture>("Internal MotionTexRTLite"));
                    }
                }
            }
            else
            {
                if (material.HasProperty("_NoiseTex3D"))
                {
                    if (material.GetTexture("_NoiseTex3D") == null)
                    {
                        material.SetTexture("_NoiseTex3D", Resources.Load<Texture3D>("Internal NoiseTex3D"));
                    }
                }

                if (material.HasProperty("_NoiseTexSS"))
                {
                    if (material.GetTexture("_NoiseTexSS") == null)
                    {
                        material.SetTexture("_NoiseTexSS", Resources.Load<Texture2D>("Internal NoiseTexSS"));
                    }
                }

                if (material.HasProperty("_OverlayNormalTex"))
                {
                    if (material.GetTexture("_OverlayNormalTex") == null)
                    {
                        material.SetTexture("_OverlayNormalTex", Resources.Load<Texture2D>("Internal SnowTex"));
                    }
                }

                if (material.HasProperty("_OverlayGlitterTexRT"))
                {
                    if (material.GetTexture("_OverlayGlitterTexRT") == null)
                    {
                        material.SetTexture("_OverlayGlitterTexRT", Resources.Load<CustomRenderTexture>("Internal GlitterTexRT"));
                    }
                }

                if (material.HasProperty("_WetnessDropsTexRT"))
                {
                    if (material.GetTexture("_WetnessDropsTexRT") == null)
                    {
                        material.SetTexture("_WetnessDropsTexRT", Resources.Load<CustomRenderTexture>("Internal DropsTexRT"));
                    }
                }

                if (material.HasProperty("_MotionNoiseTex"))
                {
                    if (material.GetTexture("_MotionNoiseTex") == null)
                    {
                        material.SetTexture("_MotionNoiseTex", Resources.Load<Texture2D>("Internal MotionTex"));
                    }
                }

                if (material.HasProperty("_MotionNoiseTexRT"))
                {
                    if (material.GetTexture("_MotionNoiseTexRT") == null)
                    {
                        material.SetTexture("_MotionNoiseTexRT", Resources.Load<CustomRenderTexture>("Internal MotionTexRT"));
                    }
                }
            }

            SetMaterialFloat(material, "_ImpostorAlphaClipValue", "_AI_Clip");
            SetMaterialKeyword(material, "_ImpostorMaskMode", new string[] { "TVE_IMPOSTOR_MASK_OFF", "TVE_IMPOSTOR_MASK_DEFAULT", "TVE_IMPOSTOR_MASK_PACKED", "TVE_IMPOSTOR_MASK_SHADING" });

            SetMaterialCoords(material, "_MainCoordMode", "_MainCoordValue", "_main_coord_value");
            SetMaterialCoords(material, "_SecondCoordMode", "_SecondCoordValue", "_second_coord_value");
            SetMaterialCoords(material, "_SecondMaskCoordMode", "_SecondMaskCoordValue", "_second_mask_coord_value");
            SetMaterialCoords(material, "_ThirdCoordMode", "_ThirdCoordValue", "_third_coord_value");
            SetMaterialCoords(material, "_ThirdMaskCoordMode", "_ThirdMaskCoordValue", "_third_mask_coord_value");
            SetMaterialCoords(material, "_TerrainMaskCoordMode", "_TerrainMaskCoordValue", "_terrain_mask_coord_value");
            SetMaterialCoords(material, "_OverlayCoordMode", "_OverlayCoordValue", "_overlay_coord_value");
            SetMaterialCoords(material, "_OverlayMaskCoordMode", "_OverlayMaskCoordValue", "_overlay_mask_coord_value");
            SetMaterialCoords(material, "_WetnessDropsCoordMode", "_WetnessDropsCoordValue", "_wetness_drops_coord_value");
            SetMaterialCoords(material, "_EmissiveCoordMode", "_EmissiveCoordValue", "_emissive_coord_value");

            SetMaterialBounds(material, "_WorldCoordMode", "_WorldCoordValue", "_world_coord_value");

            SetMaterialFloat(material, "_GlobalWindLayerValue", "_global_wind_layer_value");
            SetMaterialFloat(material, "_GlobalPushLayerValue", "_global_push_layer_value");

            SetMaterialOptions(material, "_SecondMeshMode", "_second_vert_mode");
            SetMaterialOptions(material, "_ThirdMeshMode", "_third_vert_mode");
            SetMaterialOptions(material, "_TerrainMeshMode", "_terrain_vert_mode");
            SetMaterialOptions(material, "_EmissiveMeshMode", "_emissive_vert_mode");
            SetMaterialOptions(material, "_OcclusionMeshMode", "_occlusion_vert_mode");
            SetMaterialOptions(material, "_GradientMeshMode", "_gradient_vert_mode");
            SetMaterialOptions(material, "_TintingMeshMode", "_tinting_vert_mode");
            SetMaterialOptions(material, "_DrynessMeshMode", "_dryness_vert_mode");
            SetMaterialOptions(material, "_OverlayMeshMode", "_overlay_vert_mode");
            SetMaterialOptions(material, "_WetnessWaterMeshMode", "_wetness_water_vert_mode");
            SetMaterialOptions(material, "_WetnessDropsMeshMode", "_wetness_drops_vert_mode");
            SetMaterialOptions(material, "_WetnessMeshMode", "_wetness_vert_mode");
            SetMaterialOptions(material, "_CutoutMeshMode", "_cutout_vert_mode");
            SetMaterialOptions(material, "_NormalMeshMode", "_normal_vert_mode");
            SetMaterialVector(material, "_MotionHighlightColor", "_motion_highlight_color");

            SetMaterialOptions(material, "_ObjectPhaseMode", "_object_phase_mode");
            SetMaterialKeyword(material, "_ObjectPivotMode", new string[] { "TVE_PIVOT_OFF", "TVE_PIVOT_BAKED", "TVE_PIVOT_PROC" });

            SetMaterialKeyword(material, "_RenderFilter", new string[] { "TVE_FILTER_DEFAULT", "TVE_FILTER_POINT", "TVE_FILTER_LOW", "TVE_FILTER_MEDIUM", "TVE_FILTER_HIGH" });

            if (material.HasProperty("_ObjectModelMode"))
            {
                var mode = material.GetInt("_ObjectModelMode");

                if (mode == 0)
                {
                    material.EnableKeyword("TVE_LEGACY");
                }
                else
                {
                    material.DisableKeyword("TVE_LEGACY");
                }
            }

            SetMaterialKeyword(material, "_MainSampleMode", new string[] { "TVE_MAIN_SAMPLE_MAIN_UV", "TVE_MAIN_SAMPLE_EXTRA_UV", "TVE_MAIN_SAMPLE_PLANAR_2D", "TVE_MAIN_SAMPLE_PLANAR_3D", "TVE_MAIN_SAMPLE_STOCHASTIC_2D", "TVE_MAIN_SAMPLE_STOCHASTIC_3D" });

            SetMaterialKeyword(material, "_SecondIntensityValue", "TVE_SECOND");
            SetMaterialKeyword(material, "_SecondSampleMode", new string[] { "TVE_SECOND_SAMPLE_MAIN_UV", "TVE_SECOND_SAMPLE_EXTRA_UV", "TVE_SECOND_SAMPLE_PLANAR_2D", "TVE_SECOND_SAMPLE_PLANAR_3D", "TVE_SECOND_SAMPLE_STOCHASTIC_2D", "TVE_SECOND_SAMPLE_STOCHASTIC_3D" });
            SetMaterialKeyword(material, "_SecondMaskSampleMode", new string[] { "TVE_SECOND_MASK_SAMPLE_MAIN_UV", "TVE_SECOND_MASK_SAMPLE_EXTRA_UV", "TVE_SECOND_MASK_SAMPLE_PLANAR_2D", "TVE_SECOND_MASK_SAMPLE_PLANAR_3D" });
            //SetMaterialKeyword(material, "_SecondMaskMode", new string[] { "TVE_SECOND_MASK_MAIN", "TVE_SECOND_MASK_LAYER", "TVE_SECOND_MASK_SPLIT" });
            //SetMaterialKeyword(material, "_SecondMaskValue", "TVE_SECOND_MASK");
            //SetMaterialKeyword(material, "_SecondMeshValue", "TVE_SECOND_VERT");
            //SetMaterialKeyword(material, "_SecondProjValue", "TVE_SECOND_PROJ");
            SetMaterialKeyword(material, "_SecondElementMode", "TVE_SECOND_ELEMENT");

            SetMaterialKeyword(material, "_ThirdIntensityValue", "TVE_THIRD");
            SetMaterialKeyword(material, "_ThirdSampleMode", new string[] { "TVE_THIRD_SAMPLE_MAIN_UV", "TVE_THIRD_SAMPLE_EXTRA_UV", "TVE_THIRD_SAMPLE_PLANAR_2D", "TVE_THIRD_SAMPLE_PLANAR_3D", "TVE_THIRD_SAMPLE_STOCHASTIC_2D", "TVE_THIRD_SAMPLE_STOCHASTIC_3D" });
            SetMaterialKeyword(material, "_ThirdMaskSampleMode", new string[] { "TVE_THIRD_MASK_SAMPLE_MAIN_UV", "TVE_THIRD_MASK_SAMPLE_EXTRA_UV", "TVE_THIRD_MASK_SAMPLE_PLANAR_2D", "TVE_THIRD_MASK_SAMPLE_PLANAR_3D" });
            //SetMaterialKeyword(material, "_ThirdMaskMode", new string[] { "TVE_THIRD_MASK_MAIN", "TVE_THIRD_MASK_LAYER", "TVE_THIRD_MASK_SPLIT" });
            //SetMaterialKeyword(material, "_ThirdMaskValue", "TVE_THIRD_MASK");
            //SetMaterialKeyword(material, "_ThirdMeshValue", "TVE_THIRD_VERT");
            //SetMaterialKeyword(material, "_ThirdProjValue", "TVE_THIRD_PROJ");
            SetMaterialKeyword(material, "_ThirdElementMode", "TVE_THIRD_ELEMENT");

            SetMaterialKeyword(material, "_TerrainIntensityValue", "TVE_TERRAIN");
            SetMaterialKeyword(material, "_TerrainMaskSampleMode", new string[] { "TVE_TERRAIN_MASK_SAMPLE_MAIN_UV", "TVE_TERRAIN_MASK_SAMPLE_EXTRA_UV", "TVE_TERRAIN_MASK_SAMPLE_PLANAR_2D", "TVE_TERRAIN_MASK_SAMPLE_PLANAR_3D" });
            //SetMaterialKeyword(material, "_TerrainMaskValue", "TVE_TERRAIN_MASK");
            //SetMaterialKeyword(material, "_TerrainMeshValue", "TVE_TERRAIN_VERT");
            //SetMaterialKeyword(material, "_TerrainProjValue", "TVE_TERRAIN_PROJ");
            SetMaterialKeyword(material, "_TerrainElementMode", "TVE_TERRAIN_ELEMENT");

            SetMaterialKeyword(material, "_OcclusionIntensityValue", "TVE_OCCLUSION");
            SetMaterialKeyword(material, "_GradientIntensityValue", "TVE_GRADIENT");
            SetMaterialKeyword(material, "_VariationIntensityValue", "TVE_VARIATION");

            SetMaterialKeyword(material, "_TintingIntensityValue", "TVE_TINTING");
            //SetMaterialKeyword(material, "_TintingMeshRemap", "TVE_TINTING_VERT");
            //SetMaterialKeyword(material, "_TintingNoiseRemap", "TVE_TINTING_NOISE");
            SetMaterialKeyword(material, "_TintingElementMode", "TVE_TINTING_ELEMENT");

            SetMaterialKeyword(material, "_DrynessIntensityValue", "TVE_DRYNESS");
            SetMaterialKeyword(material, "_DrynessShiftValue", "TVE_DRYNESS_SHIFT");
            //SetMaterialKeyword(material, "_DrynessMeshRemap", "TVE_DRYNESS_VERT");
            //SetMaterialKeyword(material, "_DrynessNoiseRemap", "TVE_DRYNESS_NOISE");
            SetMaterialKeyword(material, "_DrynessElementMode", "TVE_DRYNESS_ELEMENT");

            SetMaterialKeyword(material, "_OverlayIntensityValue", "TVE_OVERLAY");
            SetMaterialKeyword(material, "_OverlayTextureMode", "TVE_OVERLAY_TEX");
            SetMaterialKeyword(material, "_OverlaySampleMode", new string[] { "TVE_OVERLAY_SAMPLE_PLANAR_2D", "TVE_OVERLAY_SAMPLE_PLANAR_3D", "TVE_OVERLAY_SAMPLE_STOCHASTIC_2D", "TVE_OVERLAY_SAMPLE_STOCHASTIC_3D" });
            SetMaterialKeyword(material, "_OverlayMaskSampleMode", new string[] { "TVE_OVERLAY_MASK_SAMPLE_MAIN_UV", "TVE_OVERLAY_MASK_SAMPLE_EXTRA_UV" });
            SetMaterialKeyword(material, "_OverlayGlitterIntensityValue", "TVE_OVERLAY_GLITTER");
            //SetMaterialKeyword(material, "_OverlayMaskValue", "TVE_OVERLAY_MASK");
            //SetMaterialKeyword(material, "_OverlayProjValue", "TVE_OVERLAY_PROJ");
            //SetMaterialKeyword(material, "_OverlayMeshValue", "TVE_OVERLAY_VERT");
            //SetMaterialKeyword(material, "_OverlayNoiseValue", "TVE_OVERLAY_NOISE");
            SetMaterialKeyword(material, "_OverlayElementMode", "TVE_OVERLAY_ELEMENT");

            SetMaterialKeyword(material, "_WetnessIntensityValue", "TVE_WETNESS");
            SetMaterialKeyword(material, "_WetnessWaterIntensityValue", "TVE_WETNESS_WATER");
            SetMaterialKeyword(material, "_WetnessDropsIntensityValue", "TVE_WETNESS_DROPS");
            SetMaterialKeyword(material, new string[] { "_WetnessMeshValue", "_WetnessWaterMeshValue", "_WetnessDropsMeshValue" }, "TVE_WETNESS_VERT");
            SetMaterialKeyword(material, "_WetnessElementMode", "TVE_WETNESS_ELEMENT");

            SetMaterialKeyword(material, "_CutoutIntensityValue", "TVE_CUTOUT");
            SetMaterialKeyword(material, "_CutoutShadowMode", "TVE_CUTOUT_SHADOW");
            //SetMaterialKeyword(material, "_CutoutMeshRemap", "TVE_CUTOUT_VERT");
            //SetMaterialKeyword(material, "_CutoutNoiseValue", "TVE_CUTOUT_NOISE");
            SetMaterialKeyword(material, "_CutoutElementMode", "TVE_CUTOUT_ELEMENT");

            SetMaterialKeyword(material, new string[] { "_DitherConstantValue", "_DitherDistanceValue", "_DitherProximityValue", "_DitherGlancingValue" }, "TVE_DITHER");
            SetMaterialKeyword(material, "_DitherShadowMode", "TVE_DITHER_SHADOW");

            SetMaterialKeyword(material, "_EmissiveIntensityValue", "TVE_EMISSIVE");
            SetMaterialKeyword(material, "_EmissiveSampleMode", new string[] { "TVE_EMISSIVE_SAMPLE_MAIN_UV", "TVE_EMISSIVE_SAMPLE_EXTRA_UV" });
            //SetMaterialKeyword(material, "_EmissiveMaskValue", "TVE_EMISSIVE_MASK");
            //SetMaterialKeyword(material, "_EmissiveMeshValue", "TVE_EMISSIVE_VERT");
            SetMaterialKeyword(material, "_EmissiveElementMode", "TVE_EMISSIVE_ELEMENT");

            SetMaterialKeyword(material, "_SubsurfaceIntensityValue", "TVE_SUBSURFACE");
            SetMaterialKeyword(material, "_SubsurfaceElementMode", "TVE_SUBSURFACE_ELEMENT");

            SetMaterialKeyword(material, "_PerspectiveIntensityValue", "TVE_PERSPECTIVE");

            SetMaterialKeyword(material, "_SizeFadeIntensityValue", "TVE_SIZEFADE");
            SetMaterialKeyword(material, "_SizeFadeElementMode", "TVE_SIZEFADE_ELEMENT");

            //SetMaterialKeyword(material, "_BlanketConformValue", "TVE_BLANKET_CONFORM");
            SetMaterialKeyword(material, "_BlanketOrientationValue", "TVE_BLANKET_BENDING");
            material.SetFloat("_BlanketElementMode", 1);

            SetMaterialKeyword(material, new string[] { "_LandscapeHeightValue", "_LandscapeNormalValue" }, "TVE_LANDSCAPE");
            material.SetFloat("_LandscapeElementMode", 1);

            SetMaterialKeyword(material, "_MotionHighlightValue", "TVE_MOTION_HIGHLIGHT");
            SetMaterialKeyword(material, "_MotionBaseIntensityValue", "TVE_MOTION_BASE_BENDING");
            SetMaterialKeyword(material, "_MotionSmallIntensityValue", "TVE_MOTION_SMALL_SQUASH");
            SetMaterialKeyword(material, "_MotionTinyIntensityValue", "TVE_MOTION_TINY_FLUTTER");

            SetMaterialKeyword(material, "_MotionWindMode", new string[] { "TVE_MOTION_WIND_OFF", "TVE_MOTION_WIND_OPTIMIZED", "TVE_MOTION_WIND_ADVANCED" });
            SetMaterialKeyword(material, "_MotionWindElementMode", "TVE_MOTION_WIND_ELEMENT");

            SetMaterialKeyword(material, "_MotionPushIntensityValue", "TVE_MOTION_PUSH_BENDING");
            material.SetFloat("_MotionPushElementMode", 1);

            SetMaterialKeyword(material, "_NormalComputeValue", "TVE_NORMAL_COMPUTE");

            //if (material.HasProperty("_GradientMeshMode"))
            //{
            //    var mode = material.GetInt("_GradientMeshMode");

            //    if (mode == 0 || mode == 1 || mode == 2 || mode == 3)
            //    {
            //        SetMaterialKeyword(material, 0, new string[] { "TVE_GRADIENT_VERT", "TVE_GRADIENT_PROC" });
            //        SetMaterialOptions(material, "_GradientMeshMode", "_gradient_vert_mode");
            //    }
            //    else if (mode == 4 || mode == 5)
            //    {
            //        SetMaterialKeyword(material, 1, new string[] { "TVE_GRADIENT_VERT", "TVE_GRADIENT_PROC" });
            //        SetMaterialOptions(material, "_GradientMeshMode", "_gradient_proc_mode", 4);

            //    }
            //}

            //if (material.HasProperty("_OcclusionMeshMode"))
            //{
            //    var mode = material.GetInt("_OcclusionMeshMode");

            //    if (mode == 0 || mode == 1 || mode == 2 || mode == 3)
            //    {
            //        SetMaterialKeyword(material, 0, new string[] { "TVE_OCCLUSION_VERT", "TVE_OCCLUSION_PROC" });
            //        SetMaterialOptions(material, "_OcclusionMeshMode", "_occlusion_vert_mode");
            //    }
            //    else if (mode == 4 || mode == 5)
            //    {
            //        SetMaterialKeyword(material, 1, new string[] { "TVE_OCCLUSION_VERT", "TVE_OCCLUSION_PROC" });
            //        SetMaterialOptions(material, "_OcclusionMeshMode", "_occlusion_proc_mode", 4);

            //    }
            //}

            bool hasMotion = false;

            if (material.HasProperty("_MotionBaseIntensityValue"))
            {
                var mode = material.GetFloat("_MotionBaseIntensityValue");

                if (mode > 0)
                {
                    hasMotion = true;
                }
            }

            if (material.HasProperty("_MotionSmallIntensityValue"))
            {
                var mode = material.GetFloat("_MotionSmallIntensityValue");

                if (mode > 0)
                {
                    hasMotion = true;
                }
            }

            if (material.HasProperty("_MotionTinyIntensityValue"))
            {
                var mode = material.GetFloat("_MotionTinyIntensityValue");

                if (mode > 0)
                {
                    hasMotion = true;
                }
            }

            if (material.HasProperty("_RenderMotion"))
            {
                var mode = material.GetFloat("_RenderMotion");

                if (mode == 0)
                {
                    if (hasMotion)
                    {
                        material.SetShaderPassEnabled("MotionVectors", true);
                    }
                    else
                    {
                        material.SetShaderPassEnabled("MotionVectors", false);
                    }
                }
                else if (mode == 1)
                {
                    material.SetShaderPassEnabled("MotionVectors", false);
                }
                else if (mode == 2)
                {
                    material.SetShaderPassEnabled("MotionVectors", false);
                }
            }
            else
            {
                material.SetShaderPassEnabled("MotionVectors", false);
            }

            if (material.HasProperty("_MotionBaseMaskMode"))
            {
                var mode = material.GetInt("_MotionBaseMaskMode");

                if (mode < 4)
                {
                    material.SetFloat("_motion_base_mask_mode", 0);
                    //SetMaterialKeyword(material, 0, new string[] { "TVE_MOTION_BASE_VERT", "TVE_MOTION_BASE_PROC" });
                    SetMaterialOptions(material, "_MotionBaseMaskMode", "_motion_base_vert_mode");
                }
                else
                {
                    material.SetFloat("_motion_base_mask_mode", 1);
                    //SetMaterialKeyword(material, 1, new string[] { "TVE_MOTION_BASE_VERT", "TVE_MOTION_BASE_PROC" });
                    SetMaterialOptions(material, "_MotionBaseMaskMode", "_motion_base_proc_mode", 4);

                }
            }

            if (material.HasProperty("_MotionSmallMaskMode"))
            {
                var mode = material.GetInt("_MotionSmallMaskMode");

                if (mode < 4)
                {
                    material.SetFloat("_motion_small_mask_mode", 0);
                    //SetMaterialKeyword(material, 0, new string[] { "TVE_MOTION_SMALL_VERT", "TVE_MOTION_SMALL_PROC" });
                    SetMaterialOptions(material, "_MotionSmallMaskMode", "_motion_small_vert_mode");
                }
                else
                {
                    material.SetFloat("_motion_small_mask_mode", 1);
                    //SetMaterialKeyword(material, 1, new string[] { "TVE_MOTION_SMALL_VERT", "TVE_MOTION_SMALL_PROC" });
                    SetMaterialOptions(material, "_MotionSmallMaskMode", "_motion_small_proc_mode", 4);
                }
            }

            if (material.HasProperty("_MotionTinyMaskMode"))
            {
                var mode = material.GetInt("_MotionTinyMaskMode");

                if (mode < 4)
                {
                    material.SetFloat("_motion_tiny_mask_mode", 0);
                    //SetMaterialKeyword(material, 0, new string[] { "TVE_MOTION_TINY_VERT", "TVE_MOTION_TINY_PROC" });
                    SetMaterialOptions(material, "_MotionTinyMaskMode", "_motion_tiny_vert_mode");
                }
                else
                {
                    material.SetFloat("_motion_tiny_mask_mode", 0);
                    //SetMaterialKeyword(material, 1, new string[] { "TVE_MOTION_TINY_VERT", "TVE_MOTION_TINY_PROC" });
                    SetMaterialOptions(material, "_MotionTinyMaskMode", "_motion_tiny_proc_mode", 4);

                }
            }

            if (material.HasProperty("_MotionPushMaskMode"))
            {
                var mode = material.GetInt("_MotionPushMaskMode");

                if (mode < 4)
                {
                    material.SetFloat("_motion_push_mask_mode", 0);
                    //SetMaterialKeyword(material, 0, new string[] { "TVE_MOTION_PUSH_VERT", "TVE_MOTION_PUSH_PROC" });
                    SetMaterialOptions(material, "_MotionPushMaskMode", "_motion_push_vert_mode");
                }
                else
                {
                    material.SetFloat("_motion_push_mask_mode", 0);
                    //SetMaterialKeyword(material, 1, new string[] { "TVE_MOTION_PUSH_VERT", "TVE_MOTION_PUSH_PROC" });
                    SetMaterialOptions(material, "_MotionPushMaskMode", "_motion_push_proc_mode", 4);

                }
            }

            SetMaterialKeyword(material, "_TerrainTextureMode", "TVE_TERRAIN_PACKED");
            SetMaterialKeyword(material, "_TerrainHeightBlendValue", "TVE_TERRAIN_BLEND");

            if (material.HasProperty("_TerrainLayersMode"))
            {
                var mode = material.GetInt("_TerrainLayersMode");

                if (mode == 4)
                {
                    material.EnableKeyword("TVE_TERRAIN_04");
                    material.DisableKeyword("TVE_TERRAIN_08");
                    material.DisableKeyword("TVE_TERRAIN_12");
                    material.DisableKeyword("TVE_TERRAIN_16");
                }

                if (mode == 8)
                {
                    material.DisableKeyword("TVE_TERRAIN_04");
                    material.EnableKeyword("TVE_TERRAIN_08");
                    material.DisableKeyword("TVE_TERRAIN_12");
                    material.DisableKeyword("TVE_TERRAIN_16");
                }

                if (mode == 12)
                {
                    material.DisableKeyword("TVE_TERRAIN_04");
                    material.DisableKeyword("TVE_TERRAIN_08");
                    material.EnableKeyword("TVE_TERRAIN_12");
                    material.DisableKeyword("TVE_TERRAIN_16");
                }

                if (mode == 16)
                {
                    material.DisableKeyword("TVE_TERRAIN_04");
                    material.DisableKeyword("TVE_TERRAIN_08");
                    material.DisableKeyword("TVE_TERRAIN_12");
                    material.EnableKeyword("TVE_TERRAIN_16");
                }
            }

            // Set Terrain Mode
            if (material.HasProperty("_TerrainSampleMode1"))
            {
                for (int i = 1; i < 17; i++)
                {
                    var prop = "_TerrainSampleMode" + i;

                    if (material.HasProperty(prop))
                    {
                        var layer = i.ToString("00");
                        var mode = material.GetInt(prop);

                        if (mode == 0)
                        {
                            material.EnableKeyword("TVE_TERRAIN_SAMPLE_" + layer + "_PLANAR_2D");
                            material.DisableKeyword("TVE_TERRAIN_SAMPLE_" + layer + "_PLANAR_3D");
                            material.DisableKeyword("TVE_TERRAIN_SAMPLE_" + layer + "_STOCHASTIC_2D");
                            material.DisableKeyword("TVE_TERRAIN_SAMPLE_" + layer + "_STOCHASTIC_3D");
                        }

                        if (mode == 1)
                        {
                            material.DisableKeyword("TVE_TERRAIN_SAMPLE_" + layer + "_PLANAR_2D");
                            material.EnableKeyword("TVE_TERRAIN_SAMPLE_" + layer + "_PLANAR_3D");
                            material.DisableKeyword("TVE_TERRAIN_SAMPLE_" + layer + "_STOCHASTIC_2D");
                            material.DisableKeyword("TVE_TERRAIN_SAMPLE_" + layer + "_STOCHASTIC_3D");
                        }

                        if (mode == 2)
                        {
                            material.DisableKeyword("TVE_TERRAIN_SAMPLE_" + layer + "_PLANAR_2D");
                            material.DisableKeyword("TVE_TERRAIN_SAMPLE_" + layer + "_PLANAR_3D");
                            material.EnableKeyword("TVE_TERRAIN_SAMPLE_" + layer + "_STOCHASTIC_2D");
                            material.DisableKeyword("TVE_TERRAIN_SAMPLE_" + layer + "_STOCHASTIC_3D");
                        }

                        if (mode == 3)
                        {
                            material.DisableKeyword("TVE_TERRAIN_SAMPLE_" + layer + "_PLANAR_2D");
                            material.DisableKeyword("TVE_TERRAIN_SAMPLE_" + layer + "_PLANAR_3D");
                            material.DisableKeyword("TVE_TERRAIN_SAMPLE_" + layer + "_STOCHASTIC_2D");
                            material.EnableKeyword("TVE_TERRAIN_SAMPLE_" + layer + "_STOCHASTIC_3D");
                        }
                    }
                }
            }

            if (material.HasProperty("_EmissiveIntensityValue"))
            {
                // Set Intensity Mode
                if (material.HasProperty("_EmissivePowerMode") && material.HasProperty("_EmissivePowerValue"))
                {
                    float power = material.GetInt("_EmissivePowerMode");
                    float value = material.GetFloat("_EmissivePowerValue");

                    if (power == 0)
                    {
                        material.SetFloat("_emissive_power_value", value);
                    }
                    else if (power == 1)
                    {
                        material.SetFloat("_emissive_power_value", (12.5f / 100.0f) * Mathf.Pow(2f, value));
                    }
                }

                // Set GI Mode
                if (material.HasProperty("_EmissiveFlagMode"))
                {
                    int flag = material.GetInt("_EmissiveFlagMode");

                    if (flag == 0)
                    {
                        material.globalIlluminationFlags = MaterialGlobalIlluminationFlags.None;
                    }
                    else if (flag == 1)
                    {
                        material.globalIlluminationFlags = MaterialGlobalIlluminationFlags.AnyEmissive;
                    }
                    else if (flag == 2)
                    {
                        material.globalIlluminationFlags = MaterialGlobalIlluminationFlags.BakedEmissive;
                    }
                    else if (flag == 3)
                    {
                        material.globalIlluminationFlags = MaterialGlobalIlluminationFlags.RealtimeEmissive;
                    }
                }
            }

            // Set Legacy props for external bakers
            if (material.HasProperty("_AlphaClipValue"))
            {
                material.SetFloat("_Cutoff", material.GetFloat("_AlphaClipValue"));
            }

            // Set Legacy props for external bakers
            if (material.HasProperty("_MainColor"))
            {
                material.SetColor("_Color", material.GetColor("_MainColor"));
            }

            // Set BlinnPhong Spec Color
            if (material.HasProperty("_SpecColor"))
            {
                material.SetColor("_SpecColor", Color.white);
            }

            if (material.HasTexture("_MainAlbedoTex"))
            {
                if (material.HasTexture("_MainTex"))
                {
                    material.SetTexture("_MainTex", material.GetTexture("_MainAlbedoTex"));
                }
            }

            if (material.HasTexture("_MainNormalTex"))
            {
                if (material.HasTexture("_BumpMap"))
                {
                    material.SetTexture("_BumpMap", material.GetTexture("_MainNormalTex"));
                }
            }

            if (material.HasProperty("_MainCoordValue"))
            {
                if (material.HasTexture("_MainTex"))
                {
                    material.SetTextureScale("_MainTex", new Vector2(material.GetVector("_MainCoordValue").x, material.GetVector("_MainCoordValue").y));
                    material.SetTextureOffset("_MainTex", new Vector2(material.GetVector("_MainCoordValue").z, material.GetVector("_MainCoordValue").w));
                }

                if (material.HasTexture("_BumpMap"))
                {
                    material.SetTextureScale("_BumpMap", new Vector2(material.GetVector("_MainCoordValue").x, material.GetVector("_MainCoordValue").y));
                    material.SetTextureOffset("_BumpMap", new Vector2(material.GetVector("_MainCoordValue").z, material.GetVector("_MainCoordValue").w));
                }
            }

            if (material.HasProperty("_SubsurfaceIntensityValue"))
            {
                // Legacy Surface Shader
                SetMaterialFloat(material, "_SubsurfaceScatteringValue", "_Translucency");
                SetMaterialFloat(material, "_SubsurfaceNormalValue", "_TransNormalDistortion");

                // Lit Template
                SetMaterialFloat(material, "_SubsurfaceScatteringValue", "_TransStrength");
                SetMaterialFloat(material, "_SubsurfaceNormalValue", "_TransNormal");
                SetMaterialFloat(material, "_SubsurfaceAngleValue", "_TransScattering");
                SetMaterialFloat(material, "_SubsurfaceDirectValue", "_TransDirect");
                SetMaterialFloat(material, "_SubsurfaceAmbientValue", "_TransAmbient");
                SetMaterialFloat(material, "_SubsurfaceShadowValue", "_TransShadow");
            }

#if UNITY_EDITOR
            // Add ID for material sharing debug
            if (material.HasProperty("_IsIdentifier"))
            {
                var id = material.GetInt("_IsIdentifier");

                if (id == 0)
                {
                    material.SetInt("_IsIdentifier", (int)UnityEngine.Random.Range(1, 100));
                }
            }

            // Detect if the shaders is custom compiled
            if (AssetDatabase.GetAssetPath(material.shader).Contains("Core"))
            {
                material.SetInt("_IsCustomShader", 0);
            }
            else
            {
                material.SetInt("_IsCustomShader", 1);
            }

            // Enable Nature Rendered support
            material.SetOverrideTag("NatureRendererInstancing", "True");

            // Set Internal shader type
            if (shaderName.Contains("Vertex Lit"))
            {
                material.SetInt("_IsVertexShader", 1);
                material.SetInt("_IsSimpleShader", 0);
                material.SetInt("_IsStandardShader", 0);
                material.SetInt("_IsSubsurfaceShader", 0);
            }

            if (shaderName.Contains("Simple Lit"))
            {
                material.SetInt("_IsVertexShader", 0);
                material.SetInt("_IsSimpleShader", 1);
                material.SetInt("_IsStandardShader", 0);
                material.SetInt("_IsSubsurfaceShader", 0);
            }

            if (shaderName.Contains("Standard Lit"))
            {
                material.SetInt("_IsVertexShader", 0);
                material.SetInt("_IsSimpleShader", 0);
                material.SetInt("_IsStandardShader", 1);
                material.SetInt("_IsSubsurfaceShader", 0);
            }

            if (shaderName.Contains("Subsurface Lit"))
            {
                material.SetInt("_IsVertexShader", 0);
                material.SetInt("_IsSimpleShader", 0);
                material.SetInt("_IsStandardShader", 0);
                material.SetInt("_IsSubsurfaceShader", 1);
            }
#endif
        }

        public static void SetElementSettings(Material material)
        {
            if (!material.HasProperty("_IsElementShader"))
            {
                return;
            }

            var shaderName = material.shader.name;

            material.SetShaderPassEnabled("VolumePass", false);

            if (material.HasProperty("_IsVersion"))
            {
                var version = material.GetInt("_IsVersion");

                if (version < 600)
                {
                    if (material.HasProperty("_ElementLayerValue"))
                    {
                        var oldLayer = material.GetInt("_ElementLayerValue");

                        if (material.GetInt("_ElementLayerValue") > 0)
                        {
                            material.SetInt("_ElementLayerMask", (int)Mathf.Pow(2, oldLayer));
                            material.SetInt("_ElementLayerValue", -1);
                        }
                    }

                    if (material.HasProperty("_InvertX"))
                    {
                        material.SetInt("_ElementInvertMode", material.GetInt("_InvertX"));
                    }

                    if (material.HasProperty("_ElementFadeSupport"))
                    {
                        material.SetInt("_ElementVolumeFadeMode", material.GetInt("_ElementFadeSupport"));
                    }

                    material.SetInt("_IsVersion", 600);
                }

                if (version < 700)
                {
                    // Requires revalidation
                    material.SetInt("_IsVersion", 700);
                }

                if (version < 800)
                {
                    if (material.shader.name.Contains("Interaction"))
                    {
                        if (material.HasProperty("_ElementDirectionMode"))
                        {
                            if (material.GetInt("_ElementDirectionMode") == 1)
                            {
                                material.shader = Shader.Find("BOXOPHOBIC/The Visual Engine/Elements/Default/Motion Advanced");
                                material.SetInt("_ElementDirectionMode", 30);
                            }
                        }
                    }

                    if (shaderName.Contains("Orientation"))
                    {
                        material.shader = Shader.Find("BOXOPHOBIC/The Visual Engine/Elements/Default/Motion Interaction");
                    }

                    if (shaderName.Contains("Turbulence"))
                    {
                        material.shader = Shader.Find("BOXOPHOBIC/The Visual Engine/Elements/Default/Motion Advanced");
                        material.SetInt("_ElementDirectionMode", 10);
                    }

                    if (shaderName.Contains("Wind Control"))
                    {
                        material.shader = Shader.Find("BOXOPHOBIC/The Visual Engine/Elements/Default/Wind Power");
                    }

                    if (shaderName.Contains("Wind Direction"))
                    {
                        material.shader = Shader.Find("BOXOPHOBIC/The Visual Engine/Elements/Default/Motion Advanced");
                        material.SetInt("_ElementDirectionMode", 10);
                    }

                    material.SetInt("_IsVersion", 800);
                }

                if (version < 810)
                {
                    if (material.HasProperty("_MainTexMinValue") && material.HasProperty("_MainTexMaxValue"))
                    {
                        var min = material.GetFloat("_MainTexMinValue");
                        var max = material.GetFloat("_MainTexMaxValue");

                        material.SetFloat("_MainMaskAlphaMinValue", min);
                        material.SetFloat("_MainMaskAlphaMaxValue", max);
                    }

                    material.SetInt("_IsVersion", 810);
                }

#if UNITY_EDITOR
                // Upgrade to 14
                if (version < 1400)
                {
                    var valueX = GetMaterialSerializedFloat(material, "_MainTexColorMinValue", 0.0f);
                    var valueY = GetMaterialSerializedFloat(material, "_MainTexColorMaxValue", 1.0f);

                    material.SetVector("_MainTexColorRemap", new Vector4(valueX, valueY, 0, 0));

                    var alphaX = GetMaterialSerializedFloat(material, "_MainTexAlphaMinValue", 0.0f);
                    var alphaY = GetMaterialSerializedFloat(material, "_MainTexAlphaMaxValue", 1.0f);

                    material.SetVector("_MainTexAlphaRemap", new Vector4(alphaX, alphaY, 0, 0));

                    var falloffX = GetMaterialSerializedFloat(material, "_MainTexFallofMinValue", 0.0f);
                    var falloffY = GetMaterialSerializedFloat(material, "_MainTexFallofMaxValue", 0.0f);

                    material.SetVector("_MainTexFalloffRemap", new Vector4(falloffX, falloffY, 0, 0));

                    //var seasonX = GetMaterialSerializedFloat(material, "_SeasonMinValue", 0.0f);
                    //var seasonY = GetMaterialSerializedFloat(material, "_SeasonMaxValue", 1.0f);

                    // Reset season curve
                    material.SetVector("_SeasonRemap", new Vector4(0, 1, 0, 0));

                    material.SetFloat("_RaycastDistanceMaxValue", GetMaterialSerializedFloat(material, "_RaycastDistanceEndValue", 0.0f));
                    material.SetFloat("_MotionDirectionMode", GetMaterialSerializedFloat(material, "_ElementDirectionMode", 20) / 10 - 1);

                    material.SetInt("_IsVersion", 1400);
                }
#endif
            }

            var shaderTypeName = Path.GetFileName(shaderName);
            var shaderTypeSplit = shaderTypeName.Split(" ");
            var shaderType = shaderTypeSplit[0];

            material.SetOverrideTag("ElementType", shaderType);

            if (material.HasProperty("_ElementColorsMode"))
            {
                var effect = material.GetInt("_ElementColorsMode");

                material.SetInt("_render_colormask", effect);
            }

            //if (material.HasProperty("_ElementMotionMode"))
            //{
            //    var effect = material.GetInt("_ElementMotionMode");

            //    material.SetInt("_render_colormask", effect);

            //    if (effect == 13)
            //    {
            //        material.SetFloat("_MotionPower", 0);
            //    }
            //}

            if (material.HasProperty("_ElementBlendRGB"))
            {
                var blend = material.GetInt("_ElementBlendRGB");

                if (blend == 0)
                {
                    material.SetInt("_render_src", (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
                    material.SetInt("_render_dst", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
                }
                if (blend == 1)
                {
                    material.SetInt("_render_src", (int)UnityEngine.Rendering.BlendMode.DstColor);
                    material.SetInt("_render_dst", (int)UnityEngine.Rendering.BlendMode.Zero);
                }
                if (blend == 2)
                {
                    material.SetInt("_render_src", (int)UnityEngine.Rendering.BlendMode.One);
                    material.SetInt("_render_dst", (int)UnityEngine.Rendering.BlendMode.One);
                }
            }

            if (material.HasProperty("_ElementBlendA"))
            {
                var blend = material.GetInt("_ElementBlendA");

                if (blend == 0)
                {
                    material.SetInt("_render_src", (int)UnityEngine.Rendering.BlendMode.DstAlpha);
                    material.SetInt("_render_dst", (int)UnityEngine.Rendering.BlendMode.Zero);
                }
                else
                {
                    material.SetInt("_render_src", (int)UnityEngine.Rendering.BlendMode.One);
                    material.SetInt("_render_dst", (int)UnityEngine.Rendering.BlendMode.One);
                }
            }

            TVEUtils.SetMaterialOptions(material, "_MotionDirectionMode", "_motion_direction_mode");

            //if (material.HasProperty("_ElementDirectionMode"))
            //{
            //    var direction = material.GetInt("_ElementDirectionMode");

            //    if (direction == 0)
            //    {
            //        material.SetVector("_element_direction_mode", new Vector4(1, 0, 0, 0));
            //    }

            //    if (direction == 1)
            //    {
            //        material.SetVector("_element_direction_mode", new Vector4(0, 1, 0, 0));
            //    }

            //    if (direction == 2)
            //    {
            //        material.SetVector("_element_direction_mode", new Vector4(0, 0, 1, 0));
            //    }

            //    if (direction == 3)
            //    {
            //        material.SetVector("_element_direction_mode", new Vector4(0, 0, 0, 1));
            //    }
            //}

            //if (material.HasProperty("_ElementRaycastMode"))
            //{
            //    var raycast = material.GetInt("_ElementRaycastMode");

            //    if (raycast == 1)
            //    {
            //        material.enableInstancing = false;
            //    }
            //}

#if UNITY_EDITOR
            if (material.HasProperty("_ElementLayerMask"))
            {
                var layers = material.GetInt("_ElementLayerMask");

                if (layers > 1)
                {
                    material.SetInt("_ElementLayerMessage", 1);
                }
                else
                {
                    material.SetInt("_ElementLayerMessage", 0);
                }

                if (layers == -1)
                {
                    material.SetInt("_ElementLayerWarning", 1);
                }
                else
                {
                    material.SetInt("_ElementLayerWarning", 0);
                }
            }
#endif
        }

        // Element Utils
        public static GameObject CreateElement(Vector3 localPosition, Quaternion localRotation, Vector3 localScale, Transform parent, Material material, bool customMaterial)
        {
            material.name = "Element Default";

            var gameObject = MonoBehaviour.Instantiate(Resources.Load<GameObject>("Internal Element"));
            gameObject.name = "Element " + Path.GetFileNameWithoutExtension(material.shader.name);

            gameObject.transform.localPosition = localPosition;
            gameObject.transform.localRotation = localRotation;
            gameObject.transform.localScale = localScale;

            gameObject.AddComponent<TVEElement>();

            if (customMaterial)
            {
                gameObject.GetComponent<TVEElement>().customMaterial = material;
            }
            else
            {
                gameObject.GetComponent<Renderer>().sharedMaterial = material;
            }

            gameObject.transform.parent = parent;

            return gameObject;
        }

        public static GameObject CreateElement(Vector3 localPosition, Quaternion localRotation, Vector3 localScale, Transform parent, Material material)
        {
            return CreateElement(localPosition, localRotation, localScale, parent, material, false);
        }

        public static GameObject CreateElement(Terrain terrain, Material material, bool customMaterial)
        {
            material.name = "Element Terrain";

            var gameObject = MonoBehaviour.Instantiate(Resources.Load<GameObject>("Internal Element"));
            gameObject.name = "Element " + terrain.name;

            CopyTerrainDataToElement(terrain, TVETerrainTexture.HeightTexture, material);

            gameObject.AddComponent<TVEElement>();

            if (customMaterial)
            {
                gameObject.GetComponent<TVEElement>().customMaterial = material;
            }
            else
            {
                gameObject.GetComponent<Renderer>().sharedMaterial = material;
            }

            var position = terrain.transform.position;
            var bounds = terrain.terrainData.bounds;
            gameObject.transform.localPosition = new Vector3(bounds.center.x + position.x, bounds.min.y + position.y, bounds.center.z + position.z);
            gameObject.transform.localScale = new Vector3(terrain.terrainData.size.x, 1, terrain.terrainData.size.z);

            gameObject.GetComponent<TVEElement>().terrainData = terrain;

            return gameObject;
        }

        public static GameObject CreateElement(Terrain terrain, Material material)
        {
            return CreateElement(terrain, material, false);
        }

        public static GameObject CreateElement(GameObject gameObject, Material material, bool customMaterial)
        {
            material.name = "Element";

            gameObject.AddComponent<TVEElement>();

            if (customMaterial)
            {
                gameObject.GetComponent<TVEElement>().customMaterial = material;
            }
            else
            {
                gameObject.GetComponent<Renderer>().sharedMaterial = material;
            }

            return gameObject;
        }

        public static GameObject CreateElement(GameObject gameObject, Material material)
        {
            return CreateElement(gameObject, material, false);
        }

        public static void CopyTerrainDataToElement(Terrain terrain, TVETerrainTexture terrainMask, Material material)
        {
            if (terrain == null || terrain.terrainData == null)
            {
                return;
            }

            if (terrain.terrainData.heightmapTexture != null)
            {
                material.SetTexture("_TerrainHeightTex", terrain.terrainData.heightmapTexture);

                // Support for regular elements
                if (material.HasProperty("_MainTex") && terrainMask == TVETerrainTexture.HeightTexture)
                {
                    material.SetTexture("_MainTex", terrain.terrainData.heightmapTexture);
                }
            }

            if (terrain.normalmapTexture != null)
            {
                material.SetTexture("_TerrainNormalTex", terrain.normalmapTexture);

                // Support for regular elements
                if (material.HasProperty("_MainTex") && terrainMask == TVETerrainTexture.NormalTexture)
                {
                    material.SetTexture("_MainTex", terrain.normalmapTexture);
                }
            }

            if (terrain.terrainData.holesTexture != null)
            {
                material.SetTexture("_TerrainHolesTex", terrain.terrainData.holesTexture);

                // Support for regular elements
                if (material.HasProperty("_MainTex") && terrainMask == TVETerrainTexture.HolesTexture)
                {
                    material.SetTexture("_MainTex", terrain.terrainData.holesTexture);
                }
            }

            // Support for terrain elements
            material.SetVector("_TerrainPosition", terrain.transform.position);
            material.SetVector("_TerrainSize", terrain.terrainData.size);

            // Support for terrain elements
            if (terrain.terrainData.alphamapTextureCount == 1)
            {
                material.SetTexture("_ControlTex1", terrain.terrainData.alphamapTextures[0]);
            }

            if (terrain.terrainData.alphamapTextureCount == 2)
            {
                material.SetTexture("_ControlTex2", terrain.terrainData.alphamapTextures[1]);
            }

            if (terrain.terrainData.alphamapTextureCount == 3)
            {
                material.SetTexture("_ControlTex3", terrain.terrainData.alphamapTextures[2]);
            }

            if (terrain.terrainData.alphamapTextureCount == 4)
            {
                material.SetTexture("_ControlTex4", terrain.terrainData.alphamapTextures[3]);
            }
        }

        public static void CopyTerrainDataToMaterial(Terrain terrain, Material material)
        {
            if (terrain == null || terrain.terrainData == null || material == null)
            {
                return;
            }

            material.SetVector("_TerrainPosition", terrain.transform.position);
            material.SetVector("_TerrainSize", terrain.terrainData.size);

            if (terrain.terrainData.holesTexture != null)
            {
                material.SetTexture("_TerrainHolesTex", terrain.terrainData.holesTexture);
            }

            for (int i = 0; i < terrain.terrainData.alphamapTextures.Length; i++)
            {
                var splat = terrain.terrainData.alphamapTextures[i];
                var index = i + 1;

                if (splat != null)
                {
                    material.SetTexture("_TerrainControlTex" + index, splat);
                }
            }

            for (int i = 0; i < terrain.terrainData.terrainLayers.Length; i++)
            {
                var layer = terrain.terrainData.terrainLayers[i];
                var index = i + 1;

                if (layer == null)
                {
                    continue;
                }

                if (layer.diffuseTexture != null)
                {
                    material.SetTexture("_TerrainAlbedoTex" + index, layer.diffuseTexture);
                }
                else
                {
                    material.SetTexture("_TerrainAlbedoTex" + index, Texture2D.whiteTexture);
                }

                if (layer.normalMapTexture != null)
                {
                    material.SetTexture("_TerrainNormalTex" + index, layer.normalMapTexture);
                }
                else
                {
                    material.SetTexture("_TerrainNormalTex" + index, Texture2D.normalTexture);
                }

                if (layer.maskMapTexture != null)
                {
                    material.SetTexture("_TerrainShaderTex" + index, layer.maskMapTexture);
                }
                else
                {
                    material.SetTexture("_TerrainShaderTex" + index, Texture2D.whiteTexture);
                }

                material.SetVector("_TerrainShaderMin" + index, layer.maskMapRemapMin);
                material.SetVector("_TerrainShaderMax" + index, layer.maskMapRemapMax);
                material.SetVector("_TerrainParams" + index, new Vector4(layer.metallic, 0, layer.normalScale, layer.smoothness));
                material.SetVector("_TerrainSpecular" + index, layer.specular);
                material.SetVector("_TerrainCoord" + index, new Vector4(1 / layer.tileSize.x, 1 / layer.tileSize.y, layer.tileOffset.x, layer.tileOffset.y));
            }
        }

        public static void CopyTerrainDataToMaterial(TVETerrain tveTerrain, Material material)
        {
            if (tveTerrain == null || tveTerrain.terrain == null || tveTerrain.terrainPropertyBlock == null || material == null)
            {
                return;
            }

            material.SetVector("_TerrainPosition", tveTerrain.terrainPropertyBlock.GetVector("_TerrainPosition"));
            material.SetVector("_TerrainSize", tveTerrain.terrainPropertyBlock.GetVector("_TerrainSize"));

            if (tveTerrain.terrainPropertyBlock.HasProperty("_TerrainHolesTex"))
            {
                material.SetTexture("_TerrainHolesTex", tveTerrain.terrainPropertyBlock.GetTexture("_TerrainHolesTex"));
            }

            if (tveTerrain.terrainPropertyBlock.HasProperty("_TerrainControlTex1"))
            {
                material.SetTexture("_TerrainControlTex1", tveTerrain.terrainPropertyBlock.GetTexture("_TerrainControlTex1"));
            }

            if (tveTerrain.terrainPropertyBlock.HasProperty("_TerrainControlTex2"))
            {
                material.SetTexture("_TerrainControlTex2", tveTerrain.terrainPropertyBlock.GetTexture("_TerrainControlTex2"));
            }

            if (tveTerrain.terrainPropertyBlock.HasProperty("_TerrainControlTex3"))
            {
                material.SetTexture("_TerrainControlTex3", tveTerrain.terrainPropertyBlock.GetTexture("_TerrainControlTex3"));
            }

            if (tveTerrain.terrainPropertyBlock.HasProperty("_TerrainControlTex4"))
            {
                material.SetTexture("_TerrainControlTex4", tveTerrain.terrainPropertyBlock.GetTexture("_TerrainControlTex4"));
            }

            for (int i = 0; i < tveTerrain.terrain.terrainData.terrainLayers.Length; i++)
            {
                var index = i + 1;

                if (tveTerrain.terrainPropertyBlock.HasProperty("_TerrainAlbedoTex"))
                {
                    material.SetTexture("_TerrainAlbedoTex", tveTerrain.terrainPropertyBlock.GetTexture("_TerrainAlbedoTex"));
                }

                if (tveTerrain.terrainPropertyBlock.HasProperty("_TerrainNormalTex"))
                {
                    material.SetTexture("_TerrainNormalTex", tveTerrain.terrainPropertyBlock.GetTexture("_TerrainNormalTex"));
                }

                if (tveTerrain.terrainPropertyBlock.HasProperty("_TerrainShaderTex"))
                {
                    material.SetTexture("_TerrainShaderTex", tveTerrain.terrainPropertyBlock.GetTexture("_TerrainShaderTex"));
                }

                if (tveTerrain.terrainPropertyBlock.HasProperty("_TerrainShaderMin"))
                {
                    material.SetVector("_TerrainShaderMin", tveTerrain.terrainPropertyBlock.GetVector("_TerrainShaderMin"));
                }

                if (tveTerrain.terrainPropertyBlock.HasProperty("_TerrainShaderMax"))
                {
                    material.SetVector("_TerrainShaderMax", tveTerrain.terrainPropertyBlock.GetVector("_TerrainShaderMax"));
                }

                if (tveTerrain.terrainPropertyBlock.HasProperty("_TerrainParams"))
                {
                    material.SetVector("_TerrainParams", tveTerrain.terrainPropertyBlock.GetVector("_TerrainParams"));
                }

                if (tveTerrain.terrainPropertyBlock.HasProperty("_TerrainSpecular"))
                {
                    material.SetVector("_TerrainSpecular", tveTerrain.terrainPropertyBlock.GetVector("_TerrainSpecular"));
                }

                if (tveTerrain.terrainPropertyBlock.HasProperty("_TerrainCoord"))
                {
                    material.SetVector("_TerrainCoord", tveTerrain.terrainPropertyBlock.GetVector("_TerrainCoord"));
                }
            }
        }

        // Mesh Utils
        public static Mesh CreatePackedMesh(TVEModelData meshData)
        {
            Mesh mesh = UnityEngine.Object.Instantiate(meshData.mesh);

            var vertexCount = mesh.vertexCount;

            var bounds = mesh.bounds;
            var maxX = Mathf.Max(Mathf.Abs(bounds.min.x), Mathf.Abs(bounds.max.x));
            var maxZ = Mathf.Max(Mathf.Abs(bounds.min.z), Mathf.Abs(bounds.max.z));
            var maxR = Mathf.Max(maxX, maxZ) / 100f;
            var maxH = Mathf.Max(Mathf.Abs(bounds.min.y), Mathf.Abs(bounds.max.y)) / 100f;

            if (meshData.height == 0)
            {
                meshData.height = maxH;
            }

            if (meshData.radius == 0)
            {
                meshData.radius = maxR;
            }

            var dummyFloat = new List<float>(vertexCount);
            var dummyVector2 = new List<Vector2>(vertexCount);
            var dummyVector3 = new List<Vector3>(vertexCount);
            var dummyVector4 = new List<Vector4>(vertexCount);

            var colors = new List<Color>(vertexCount);
            var UV0 = new List<Vector4>(vertexCount);
            var UV2 = new List<Vector4>(vertexCount);
            var UV4 = new List<Vector4>(vertexCount);

            for (int i = 0; i < vertexCount; i++)
            {
                dummyFloat.Add(1);
                dummyVector2.Add(Vector2.zero);
                dummyVector3.Add(Vector3.zero);
                dummyVector4.Add(Vector4.zero);
            }

            mesh.GetColors(colors);
            mesh.GetUVs(0, UV0);
            mesh.GetUVs(1, UV2);
            mesh.GetUVs(3, UV4);

            if (UV2.Count == 0)
            {
                UV2 = dummyVector4;
            }

            if (UV4.Count == 0)
            {
                UV4 = dummyVector4;
            }

            if (meshData.variationMask == null)
            {
                meshData.variationMask = dummyFloat;
            }

            if (meshData.occlusionMask == null)
            {
                meshData.occlusionMask = dummyFloat;
            }

            if (meshData.detailMask == null)
            {
                meshData.detailMask = dummyFloat;
            }

            if (meshData.heightMask == null)
            {
                meshData.heightMask = dummyFloat;
            }

            if (meshData.motion2Mask == null)
            {
                meshData.motion2Mask = dummyFloat;
            }

            if (meshData.motion3Mask == null)
            {
                meshData.motion3Mask = dummyFloat;
            }

            if (meshData.detailCoord == null)
            {
                meshData.detailCoord = dummyVector2;
            }

            if (meshData.detailCoord == null)
            {
                meshData.pivotPositions = dummyVector3;
            }

            for (int i = 0; i < vertexCount; i++)
            {
                colors[i] = new Color(meshData.variationMask[i], meshData.occlusionMask[i], meshData.detailMask[i], meshData.heightMask[i]);
                UV0[i] = new Vector4(UV0[i].x, UV0[i].y, MathVector2ToFloat(meshData.motion2Mask[i], meshData.motion3Mask[i]), MathVector2ToFloat(meshData.height / 100f, meshData.radius / 100f));
                UV2[i] = new Vector4(UV2[i].x, UV2[i].y, meshData.detailCoord[i].x, meshData.detailCoord[i].y);
                UV4[i] = new Vector4(meshData.pivotPositions[i].x, meshData.pivotPositions[i].z, meshData.pivotPositions[i].y, 0);
            }

            mesh.SetColors(colors);
            mesh.SetUVs(0, UV0);
            mesh.SetUVs(1, UV2);
            mesh.SetUVs(3, UV4);

            dummyFloat.Clear();
            dummyVector2.Clear();
            dummyVector3.Clear();
            dummyVector4.Clear();

            return mesh;
        }

        public static Mesh CombinePackedMeshes(List<GameObject> gameObjects, bool mergeSubMeshes, bool usePrebakedPivots)
        {
            var mesh = new Mesh();
            mesh.indexFormat = IndexFormat.UInt32;

            var combineInstances = new CombineInstance[gameObjects.Count];

            for (int i = 0; i < gameObjects.Count; i++)
            {
                var instanceMesh = UnityEngine.Object.Instantiate(gameObjects[i].GetComponent<MeshFilter>().sharedMesh);
                var meshRenderer = gameObjects[i].GetComponent<MeshRenderer>();

                var vertexCount = instanceMesh.vertexCount;
                var UV4 = new List<Vector3>(vertexCount);
                var newUV4 = new List<Vector4>(vertexCount);

                instanceMesh.GetUVs(3, UV4);

                if (usePrebakedPivots)
                {
                    for (int v = 0; v < vertexCount; v++)
                    {
                        var currentPivot = new Vector3(UV4[v].x, UV4[v].z, UV4[v].y);
                        var transformedPivot = gameObjects[i].transform.TransformPoint(currentPivot);
                        var swizzeledPivots = new Vector4(transformedPivot.x, transformedPivot.z, transformedPivot.y, 0);

                        newUV4.Add(swizzeledPivots);
                    }
                }
                else
                {
                    for (int v = 0; v < vertexCount; v++)
                    {
                        var currentPivot = gameObjects[i].transform.position;
                        var swizzeledPivots = new Vector4(currentPivot.x, currentPivot.z, currentPivot.y, 0);

                        newUV4.Add(swizzeledPivots);
                    }
                }

                instanceMesh.SetUVs(3, newUV4);

                combineInstances[i].mesh = instanceMesh;
                combineInstances[i].transform = meshRenderer.transform.localToWorldMatrix;
                combineInstances[i].lightmapScaleOffset = meshRenderer.lightmapScaleOffset;
                combineInstances[i].realtimeLightmapScaleOffset = meshRenderer.realtimeLightmapScaleOffset;
            }

            mesh.CombineMeshes(combineInstances, mergeSubMeshes, true, true);

            return mesh;
        }

        public static Mesh CombinePackedMeshes(List<GameObject> gameObjects, bool mergeSubMeshes)
        {
            return CombinePackedMeshes(gameObjects, mergeSubMeshes, true);
        }

        public static Mesh CombineColliderMeshes(List<GameObject> gameObjects)
        {
            var mesh = new Mesh();
            var combineInstances = new CombineInstance[gameObjects.Count];

            for (int i = 0; i < gameObjects.Count; i++)
            {
                var instanceMesh = UnityEngine.Object.Instantiate(gameObjects[i].GetComponent<MeshFilter>().sharedMesh);
                var meshRenderer = gameObjects[i].GetComponent<MeshRenderer>();
                var transformMatrix = meshRenderer.transform.localToWorldMatrix;

                combineInstances[i].mesh = instanceMesh;
                combineInstances[i].transform = transformMatrix;
                combineInstances[i].lightmapScaleOffset = meshRenderer.lightmapScaleOffset;
                combineInstances[i].realtimeLightmapScaleOffset = meshRenderer.realtimeLightmapScaleOffset;
            }

            mesh.CombineMeshes(combineInstances, true, true, false);

            return mesh;
        }

        public static List<Mesh> SplitPackedMesh(Mesh mesh)
        {
            var spliMeshes = new List<Mesh>();

            for (int i = 0; i < mesh.subMeshCount; i++)
            {
                Mesh submesh = GetSubmesh(mesh, i);

                spliMeshes.Add(submesh);
            }

            return spliMeshes;
        }

        public static Mesh GetSubmesh(Mesh mesh, int submeshIndex)
        {
            int[] triangles = mesh.GetTriangles(submeshIndex, true);

            Vector3[] vertices = mesh.vertices;
            Vector3[] normals = mesh.normals;
            Vector4[] tangents = mesh.tangents;
            Color[] colors = mesh.colors;
            List<Vector4> UV0 = new List<Vector4>();
            List<Vector4> UV2 = new List<Vector4>();
            List<Vector4> UV4 = new List<Vector4>();

            mesh.GetUVs(0, UV0);
            mesh.GetUVs(1, UV2);
            mesh.GetUVs(3, UV4);

            // Create a HashSet to store unique vertices and a dictionary to map old indices to new indices
            HashSet<Vector3> uniqueVertices = new HashSet<Vector3>();
            Dictionary<int, int> indexMap = new Dictionary<int, int>();
            Dictionary<Vector3, int> vertexMap = new Dictionary<Vector3, int>();

            // Loop through the triangles and add their vertices to the HashSet
            foreach (int triangleIndex in triangles)
            {
                Vector3 vertex = vertices[triangleIndex];

                if (!uniqueVertices.Contains(vertex))
                {
                    uniqueVertices.Add(vertex);

                    int newIndex = uniqueVertices.Count - 1;

                    indexMap[triangleIndex] = newIndex;
                    vertexMap[vertex] = newIndex;
                }
            }

            Vector3[] newVertices = new Vector3[uniqueVertices.Count];
            Vector3[] newNormals = new Vector3[uniqueVertices.Count];
            Vector4[] newTangents = new Vector4[uniqueVertices.Count];
            Color[] newColors = new Color[uniqueVertices.Count];
            List<Vector4> newUV0 = new List<Vector4>();
            List<Vector4> newUV2 = new List<Vector4>();
            List<Vector4> newUV4 = new List<Vector4>();

            uniqueVertices.CopyTo(newVertices);

            foreach (KeyValuePair<int, int> pair in indexMap)
            {
                int oldIndex = pair.Key;
                int newIndex = pair.Value;
                newNormals[newIndex] = normals[oldIndex];
                newTangents[newIndex] = tangents[oldIndex];
                newColors[newIndex] = colors[oldIndex];
                newUV0.Add(UV0[oldIndex]);
                newUV2.Add(UV2[oldIndex]);
                newUV4.Add(UV4[oldIndex]);
            }

            int[] newTriangles = new int[triangles.Length];

            for (int i = 0; i < triangles.Length; i += 3)
            {
                int newIndex1 = vertexMap[vertices[triangles[i + 0]]];
                int newIndex2 = vertexMap[vertices[triangles[i + 1]]];
                int newIndex3 = vertexMap[vertices[triangles[i + 2]]];
                newTriangles[i + 0] = newIndex1;
                newTriangles[i + 1] = newIndex2;
                newTriangles[i + 2] = newIndex3;
            }

            Mesh newMesh = new Mesh();
            newMesh.vertices = newVertices;
            newMesh.normals = newNormals;
            newMesh.tangents = newTangents;
            newMesh.colors = newColors;
            newMesh.SetUVs(0, newUV0);
            newMesh.SetUVs(1, newUV2);
            newMesh.SetUVs(3, newUV4);
            newMesh.triangles = newTriangles;

            return newMesh;
        }

#if UNITY_EDITOR
        public static void ValidateModel(string path, List<Vector2> subMeshMotion, bool unloadFromMemory)
        {
            // Exclude upgraded meshes or meshes converted with TVE24
            if (TVEUtils.HasLabel(path))
            {
                return;
            }

            if (Path.GetFullPath(path).Length > 256)
            {
                Debug.Log("<b>[The Visual Engine]</b> " + path + " could not be upgraded because the file path is too long! To fix the issue, rename the folders and file names, then go to Hub > Show Advanced Settings > Validate All Project Meshes to re-process the meshes!");
                return;
            }

            var mesh = AssetDatabase.LoadAssetAtPath<Mesh>(path);

            if (mesh == null)
            {
                Debug.Log("<b>[The Visual Engine]</b> " + path + " could not be upgraded because the mesh is null!");
                return;
            }

            var meshName = mesh.name;

            var instanceMesh = UnityEngine.Object.Instantiate(mesh);
            instanceMesh.name = meshName;

            if (instanceMesh == null)
            {
                Debug.Log("<b>[The Visual Engine]</b> " + path + " could not be upgraded because the mesh is null!");
                return;
            }

            //var bounds = mesh.bounds;

            //var maxX = Mathf.Max(Mathf.Abs(bounds.min.x), Mathf.Abs(bounds.max.x));
            //var maxZ = Mathf.Max(Mathf.Abs(bounds.min.z), Mathf.Abs(bounds.max.z));
            //var maxR = Mathf.Max(maxX, maxZ);

            var vertexCount = mesh.vertexCount;
            var vertices = new List<Vector3>(vertexCount);
            var colors = new List<Color>(vertexCount);
            var UV0 = new List<Vector4>(vertexCount);
            var UV2 = new List<Vector4>(vertexCount);
            var UV4 = new List<Vector4>(vertexCount);
            var newColors = new List<Color>(vertexCount);
            var newUV0 = new List<Vector2>(vertexCount);
            var newUV2 = new List<Vector2>(vertexCount);
            var newUV3 = new List<Vector2>(vertexCount);
            var newUV4 = new List<Vector2>(vertexCount);
            //var newUV5 = new List<Vector2>(vertexCount);

            mesh.GetVertices(vertices);
            mesh.GetColors(colors);
            mesh.GetUVs(0, UV0);
            mesh.GetUVs(1, UV2);
            mesh.GetUVs(3, UV4);

            var motion = new List<Vector2>(vertexCount);

            if (UV0.Count != 0)
            {
                for (int i = 0; i < vertexCount; i++)
                {
                    motion.Add(TVEUtils.MathFloatFromVector2(UV0[i].z));
                }
            }
            else
            {
                motion.Add(Vector2.zero);
            }

            var subMeshCount = instanceMesh.subMeshCount;

            for (int s = 0; s < subMeshCount; s++)
            {
                var subMesh = instanceMesh.GetSubMesh(s);
                var baseVertex = subMesh.firstVertex;
                var endVertex = subMesh.firstVertex + subMesh.vertexCount;

                for (int i = baseVertex; i < endVertex; i++)
                {
                    if (colors.Count != 0)
                    {
                        // Submesh count can be different than the material array
                        if (subMeshMotion != null && subMeshMotion.Count >= s)
                        {
                            var vcolor = colors[i];

                            if (subMeshMotion[s].x == 1)
                            {
                                vcolor.g = motion[i].x;
                            }

                            if (subMeshMotion[s].y == 1)
                            {
                                vcolor.b = motion[i].y;
                            }

                            newColors.Add(vcolor);
                        }
                        else
                        {
                            newColors.Add(colors[i]);
                        }
                    }
                    else
                    {
                        newColors.Add(Color.white);
                    }

                    if (UV0.Count != 0)
                    {
                        //var motion = TVEUtils.MathFloatFromVector2(UV0[i].z);

                        newUV0.Add(new Vector2(UV0[i].x, UV0[i].y));
                        //newUV5.Add(new Vector2(motion.x, motion.y));
                    }
                    else
                    {
                        newUV0.Add(Vector2.zero);
                        //newUV5.Add(Vector2.zero);
                    }

                    if (UV2.Count != 0)
                    {
                        newUV2.Add(new Vector2(UV2[i].x, UV2[i].y));
                        newUV3.Add(new Vector2(UV2[i].z, UV2[i].w));
                    }
                    else
                    {
                        if (UV0.Count != 0)
                        {
                            newUV2.Add(new Vector2(UV0[i].x, UV0[i].y));
                            newUV3.Add(new Vector2(UV0[i].z, UV0[i].w));
                        }
                        else
                        {
                            newUV2.Add(Vector2.zero);
                            newUV3.Add(Vector2.zero);
                        }
                    }

                    if (UV4.Count != 0)
                    {
                        newUV4.Add(new Vector2(UV4[i].x, UV4[i].y));
                    }
                    else
                    {
                        newUV4.Add(Vector2.zero);
                    }
                }
            }

            for (int i = 0; i < vertexCount; i++)
            {

            }

            instanceMesh.SetColors(newColors);
            instanceMesh.SetUVs(0, newUV0);
            instanceMesh.SetUVs(1, newUV2);
            instanceMesh.SetUVs(2, newUV3);
            instanceMesh.SetUVs(3, newUV4);
            //newMesh.SetUVs(4, newUV5);

            mesh.Clear();

            EditorUtility.CopySerialized(instanceMesh, mesh);
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();

            TVEUtils.SetLabel(path);

            if (unloadFromMemory)
            {
                Resources.UnloadAsset(mesh);
                Resources.UnloadAsset(Resources.Load<Mesh>(path));
            }

            vertices.Clear();
            colors.Clear();
            UV0.Clear();
            UV2.Clear();
            UV4.Clear();
            newColors.Clear();
            newUV0.Clear();
            newUV2.Clear();
            newUV3.Clear();
            newUV4.Clear();
            //newUV5.Clear();
        }

        public static TVEModelSettings PreProcessMesh(string meshPath)
        {
            TVEModelSettings meshSettings = new TVEModelSettings();
            meshSettings.meshPath = meshPath;

            var modelImporter = AssetImporter.GetAtPath(meshPath) as ModelImporter;

            if (modelImporter != null)
            {
                var doPrecessing = false;

                if (!modelImporter.isReadable)
                {
                    doPrecessing = true;
                }

                if (modelImporter.keepQuads)
                {
                    doPrecessing = true;
                }

                if (modelImporter.meshCompression != ModelImporterMeshCompression.Off)
                {
                    doPrecessing = true;
                }

                if (doPrecessing)
                {
                    meshSettings.isReadable = modelImporter.isReadable;
                    meshSettings.keepQuads = modelImporter.keepQuads;
                    meshSettings.meshCompression = modelImporter.meshCompression;

                    modelImporter.isReadable = true;
                    modelImporter.keepQuads = false;
                    modelImporter.meshCompression = ModelImporterMeshCompression.Off;
                    modelImporter.SaveAndReimport();
                    AssetDatabase.Refresh();

                    meshSettings.requiresProcessing = true;
                }
            }

            //string fileText = File.ReadAllText(meshPath);
            //fileText = fileText.Replace("m_IsReadable: 0", "m_IsReadable: 1");
            //File.WriteAllText(meshPath, fileText);
            //AssetDatabase.SaveAssets();
            //AssetDatabase.Refresh();

            return meshSettings;
        }

        public static void PostProcessMesh(string meshPath, TVEModelSettings meshSettings)
        {
            if (meshSettings.requiresProcessing)
            {
                var modelImporter = AssetImporter.GetAtPath(meshPath) as ModelImporter;

                if (modelImporter != null)
                {
                    modelImporter.isReadable = meshSettings.isReadable;
                    modelImporter.keepQuads = meshSettings.keepQuads;
                    modelImporter.meshCompression = meshSettings.meshCompression;
                    modelImporter.SaveAndReimport();
                }
            }
        }

        public static void CreateModifiableMeshes(Mesh mesh)
        {
            var meshPath = AssetDatabase.GetAssetPath(mesh);

            var meshBase = UnityEngine.Object.Instantiate(mesh);
            var meshMotion = UnityEngine.Object.Instantiate(mesh);

            var vertexCount = mesh.vertexCount;

            var colors = new List<Color>(vertexCount);
            var UV0 = new List<Vector4>(vertexCount);
            var UV2 = new List<Vector4>(vertexCount);

            mesh.GetColors(colors);
            mesh.GetUVs(0, UV0);
            mesh.GetUVs(1, UV2);

            var dataUV3 = new List<Vector2>(vertexCount);
            var dataBase = new List<Color>(vertexCount);
            var dataMotion = new List<Color>(vertexCount);

            for (int i = 0; i < vertexCount; i++)
            {
                // Store Detail UVs
                dataUV3.Add(new Vector4(UV2[i].z, UV2[i].w, 0, 0));

                // Store Variation, Occlusion and Detail Mask
                dataBase.Add(new Color(colors[i].r, colors[i].g, colors[i].b, 0));

                // Store HeightTexture Mask, Branch Mask and Leaves Mask
                var motionMasks = MathFloatFromVector2(UV0[i].z);
                dataMotion.Add(new Color(colors[i].a, motionMasks.x, motionMasks.y, 0));
            }

            meshBase.SetUVs(2, dataUV3);
            meshBase.SetColors(dataBase);
            meshMotion.SetColors(dataMotion);

            var basePath = meshPath.Replace("OO Model", "Modifiable Base");
            var motionPath = meshPath.Replace("OO Model", "Modifiable Motion");

            if (!File.Exists(basePath))
            {
                AssetDatabase.CreateAsset(meshBase, basePath);
            }
            else
            {
                var meshFile = AssetDatabase.LoadAssetAtPath<Mesh>(basePath);
                meshFile.Clear();
                EditorUtility.CopySerialized(meshBase, meshFile);
            }

            if (!File.Exists(motionPath))
            {
                AssetDatabase.CreateAsset(meshMotion, motionPath);
            }
            else
            {
                var meshFile = AssetDatabase.LoadAssetAtPath<Mesh>(motionPath);
                meshFile.Clear();
                EditorUtility.CopySerialized(meshMotion, meshFile);
            }

            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
        }

        public static void CombineModifiableMeshes(Mesh mesh, Mesh meshBase, Mesh meshMotion)
        {
            var newMesh = UnityEngine.Object.Instantiate(mesh);
            newMesh.name = mesh.name;

            var vertexCount = mesh.vertexCount;

            var newColors = new List<Color>(vertexCount);
            var newUV0 = new List<Vector4>(vertexCount);
            var newUV2 = new List<Vector4>(vertexCount);

            mesh.GetColors(newColors);
            mesh.GetUVs(0, newUV0);
            mesh.GetUVs(1, newUV2);

            var dataUV3 = new List<Vector2>(vertexCount);
            var dataBase = new List<Color>(vertexCount);
            var dataMotion = new List<Color>(vertexCount);

            meshBase.GetUVs(3, dataUV3);
            meshBase.GetColors(dataBase);
            meshMotion.GetColors(dataMotion);

            for (int i = 0; i < vertexCount; i++)
            {
                newColors[i] = new Color(dataBase[i].r, dataBase[i].g, dataBase[i].b, dataMotion[i].r);
                newUV0[i] = new Vector4(newUV0[i].x, newUV0[i].y, TVEUtils.MathVector2ToFloat(dataMotion[i].g, dataMotion[i].b), newUV0[i].w);
                newUV2[i] = new Vector4(newUV2[i].x, newUV2[i].y, dataUV3[i].x, dataUV3[i].y);
            }

            newMesh.SetColors(newColors);
            newMesh.SetUVs(0, newUV0);
            newMesh.SetUVs(1, newUV2);

            mesh.Clear();

            if (!mesh.isReadable)
            {
                newMesh.UploadMeshData(true);
            }

            EditorUtility.CopySerialized(newMesh, mesh);
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
        }

        // Packer Utils
        public static TVEPackerData InitPackerData()
        {
            TVEPackerData packerData = new TVEPackerData();

            packerData.blitMaterial = new Material(Shader.Find("Hidden/BOXOPHOBIC/The Visual Engine/Helpers/Packer"));

            packerData.maskChannels = new int[4];
            packerData.maskCoords = new int[4];
            //maskLayers = new int[4];
            packerData.maskActions0 = new int[4];
            packerData.maskActions1 = new int[4];
            packerData.maskActions2 = new int[4];
            packerData.maskTextures = new Texture[4];

            for (int i = 0; i < 4; i++)
            {
                packerData.maskChannels[i] = 0;
                packerData.maskCoords[i] = 0;
                //maskLayers[i] = 0;
                packerData.maskActions0[i] = 0;
                packerData.maskActions1[i] = 0;
                packerData.maskActions2[i] = 0;
                packerData.maskTextures[i] = null;
            }

            return packerData;
        }

        public static Texture PackAndSaveTexture(string savePath, TVEPackerData packerData)
        {
            Texture packedTexture;

            int importSize = GetPackedTextureImportSize(packerData);

            List<TVETextureSettings> uniqueTextureSettings = new List<TVETextureSettings>();

            for (int i = 0; i < packerData.maskTextures.Length; i++)
            {
                var texture = packerData.maskTextures[i];

                if (texture != null)
                {
                    var texturePath = AssetDatabase.GetAssetPath(texture);

                    bool exists = false;

                    for (int s = 0; s < uniqueTextureSettings.Count; s++)
                    {
                        if (texturePath == uniqueTextureSettings[s].texturePath)
                        {
                            exists = true;
                            break;
                        }
                    }

                    if (!exists)
                    {
                        var textureSettings = TVEUtils.PreProcessTexture(texturePath);
                        uniqueTextureSettings.Add(textureSettings);
                    }
                }
            }

            //Set Packer Metallic channel
            if (packerData.maskTextures[0] != null)
            {
                packerData.blitMaterial.SetTexture("_Packer_TexR", packerData.maskTextures[0]);
                packerData.blitMaterial.SetInt("_Packer_ChannelR", packerData.maskChannels[0]);
                packerData.blitMaterial.SetInt("_Packer_CoordR", packerData.maskCoords[0]);
                //blitMaterial.SetInt("_Packer_LayerR", maskLayers[0]);
                packerData.blitMaterial.SetInt("_Packer_Action0R", packerData.maskActions0[0]);
                packerData.blitMaterial.SetInt("_Packer_Action1R", packerData.maskActions1[0]);
                packerData.blitMaterial.SetInt("_Packer_Action2R", packerData.maskActions2[0]);
            }
            else
            {
                packerData.blitMaterial.SetInt("_Packer_ChannelR", 0);
                packerData.blitMaterial.SetFloat("_Packer_FloatR", 1.0f);
            }

            //Set Packer Occlusion channel
            if (packerData.maskTextures[1] != null)
            {
                packerData.blitMaterial.SetTexture("_Packer_TexG", packerData.maskTextures[1]);
                packerData.blitMaterial.SetInt("_Packer_ChannelG", packerData.maskChannels[1]);
                packerData.blitMaterial.SetInt("_Packer_CoordG", packerData.maskCoords[1]);
                //blitMaterial.SetInt("_Packer_LayerG", maskLayers[1]);
                packerData.blitMaterial.SetInt("_Packer_Action0G", packerData.maskActions0[1]);
                packerData.blitMaterial.SetInt("_Packer_Action1G", packerData.maskActions1[1]);
                packerData.blitMaterial.SetInt("_Packer_Action2G", packerData.maskActions2[1]);
            }
            else
            {
                packerData.blitMaterial.SetInt("_Packer_ChannelG", 0);
                packerData.blitMaterial.SetFloat("_Packer_FloatG", 1.0f);
            }

            //Set Packer Mask channel
            if (packerData.maskTextures[2] != null)
            {
                packerData.blitMaterial.SetTexture("_Packer_TexB", packerData.maskTextures[2]);
                packerData.blitMaterial.SetInt("_Packer_ChannelB", packerData.maskChannels[2]);
                packerData.blitMaterial.SetInt("_Packer_CoordB", packerData.maskCoords[2]);
                //blitMaterial.SetInt("_Packer_LayerB", maskLayers[2]);
                packerData.blitMaterial.SetInt("_Packer_Action0B", packerData.maskActions0[2]);
                packerData.blitMaterial.SetInt("_Packer_Action1B", packerData.maskActions1[2]);
                packerData.blitMaterial.SetInt("_Packer_Action2B", packerData.maskActions2[2]);
            }
            else
            {
                packerData.blitMaterial.SetInt("_Packer_ChannelB", 0);
                packerData.blitMaterial.SetFloat("_Packer_FloatB", 1.0f);
            }

            //Set Packer Smothness channel
            if (packerData.maskTextures[3] != null)
            {
                packerData.blitMaterial.SetTexture("_Packer_TexA", packerData.maskTextures[3]);
                packerData.blitMaterial.SetInt("_Packer_ChannelA", packerData.maskChannels[3]);
                packerData.blitMaterial.SetInt("_Packer_CoordA", packerData.maskCoords[3]);
                //blitMaterial.SetInt("_Packer_LayerA", maskLayers[3]);
                packerData.blitMaterial.SetInt("_Packer_Action0A", packerData.maskActions0[3]);
                packerData.blitMaterial.SetInt("_Packer_Action1A", packerData.maskActions1[3]);
                packerData.blitMaterial.SetInt("_Packer_Action2A", packerData.maskActions2[3]);
            }
            else
            {
                packerData.blitMaterial.SetInt("_Packer_ChannelA", 0);
                packerData.blitMaterial.SetFloat("_Packer_FloatA", 1.0f);
            }

            packerData.blitMaterial.SetInt("_Packer_TransformSpace", packerData.transformSpace);

            Vector2 pixelSize = GetPackedTexturePixelSize(packerData);

            RenderTexture renderTexure = new RenderTexture((int)pixelSize.x, (int)pixelSize.y, 0, RenderTextureFormat.ARGBFloat);
            Texture2D packedTexure = new Texture2D(renderTexure.width, renderTexure.height, TextureFormat.RGBAFloat, false);

            if (packerData.blitMesh != null)
            {
                var currentRenderTexture = RenderTexture.active;

                Graphics.SetRenderTarget(renderTexure);

                GL.Clear(false, true, new Color(0.5f, 0.5f, 1f, 0f), 0f);

                GL.PushMatrix();
                GL.LoadOrtho();

                packerData.blitMaterial.SetPass(packerData.blitPass);

                Graphics.DrawMeshNow(packerData.blitMesh, Matrix4x4.identity);

                RenderTexture.active = renderTexure;
                packedTexure.ReadPixels(new Rect(0, 0, renderTexure.width, renderTexure.height), 0, 0);
                packedTexure.Apply();
                RenderTexture.active = currentRenderTexture;

                Graphics.SetRenderTarget(null);
                GL.PopMatrix();
            }
            else
            {
                var currentRenderTexture = RenderTexture.active;

                Graphics.Blit(Texture2D.whiteTexture, renderTexure, packerData.blitMaterial, packerData.blitPass);
                RenderTexture.active = renderTexure;
                packedTexure.ReadPixels(new Rect(0, 0, renderTexure.width, renderTexure.height), 0, 0);
                packedTexure.Apply();
                RenderTexture.active = currentRenderTexture;
            }

            renderTexure.Release();

            if (savePath.EndsWith(".asset"))
            {
                if (File.Exists(savePath))
                {
                    var assetTexture = AssetDatabase.LoadAssetAtPath<Texture>(savePath);

                    EditorUtility.CopySerialized(packedTexure, assetTexture);
                }
                else
                {
                    AssetDatabase.CreateAsset(packedTexure, savePath);
                }

                AssetDatabase.SaveAssets();
                AssetDatabase.Refresh();
            }
            else
            {
                byte[] bytes;

                if (savePath.EndsWith("png"))
                {
                    bytes = packedTexure.EncodeToPNG();
                }
                else if (savePath.EndsWith("tga"))
                {
                    bytes = packedTexure.EncodeToTGA();
                }
                else
                {
                    bytes = packedTexure.EncodeToEXR();
                }

                File.WriteAllBytes(savePath, bytes);

                AssetDatabase.SaveAssets();
                AssetDatabase.Refresh();

                TextureImporter texImporter = AssetImporter.GetAtPath(savePath) as TextureImporter;

                if (packerData.saveAsDefault)
                {
                    texImporter.textureType = TextureImporterType.Default;
                }
                else
                {
                    texImporter.textureType = TextureImporterType.NormalMap;
                }

                texImporter.maxTextureSize = importSize;
                texImporter.sRGBTexture = packerData.saveAsSRGB;
                texImporter.alphaSource = TextureImporterAlphaSource.FromInput;

                texImporter.SaveAndReimport();
                AssetDatabase.SaveAssets();
                AssetDatabase.Refresh();

                TVEUtils.SetLabel(savePath);
            }

            packedTexture = AssetDatabase.LoadAssetAtPath<Texture>(savePath);

            for (int i = 0; i < uniqueTextureSettings.Count; i++)
            {
                var textureSettings = uniqueTextureSettings[i];
                TVEUtils.PostProcessTexture(textureSettings.texturePath, textureSettings);
            }

            return packedTexture;
        }

        static int GetPackedTextureImportSize(TVEPackerData packerData)
        {
            int initSize = 32;

            for (int i = 0; i < packerData.maskTextures.Length; i++)
            {
                var texture = packerData.maskTextures[i];

                if (texture != null)
                {
                    string texPath = AssetDatabase.GetAssetPath(texture);
                    TextureImporter texImporter = AssetImporter.GetAtPath(texPath) as TextureImporter;

                    initSize = Mathf.Max(initSize, texImporter.maxTextureSize);
                }
            }

            return initSize;
        }

        static Vector2 GetPackedTexturePixelSize(TVEPackerData packerData)
        {
            int x = 32;
            int y = 32;

            for (int i = 0; i < packerData.maskTextures.Length; i++)
            {
                var texture = packerData.maskTextures[i];

                if (texture != null)
                {
                    x = Mathf.Max(x, texture.width);
                    y = Mathf.Max(y, texture.height);
                }
            }

            return new Vector2(x, y);
        }

        public static TVETextureSettings PreProcessTexture(string texturePath)
        {
            TVETextureSettings textureSettings = new TVETextureSettings();
            textureSettings.texturePath = texturePath;

            TextureImporter importer = AssetImporter.GetAtPath(texturePath) as TextureImporter;

            textureSettings.textureCompression = importer.textureCompression;
            textureSettings.maxTextureSize = importer.maxTextureSize;

            importer.ReadTextureSettings(textureSettings.textureSettings);

            importer.textureType = TextureImporterType.Default;
            importer.sRGBTexture = false;
            importer.maxTextureSize = 8192;
            importer.textureCompression = TextureImporterCompression.Uncompressed;

            importer.SaveAndReimport();

            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();

            return textureSettings;
        }

        public static void PostProcessTexture(string texturePath, TVETextureSettings textureSettings)
        {
            TextureImporter importer = AssetImporter.GetAtPath(texturePath) as TextureImporter;

            importer.textureCompression = textureSettings.textureCompression;
            importer.maxTextureSize = textureSettings.maxTextureSize;

            importer.SetTextureSettings(textureSettings.textureSettings);

            importer.SaveAndReimport();

            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
        }

        // GameObject Utils
        public static void GetChildRecursive(GameObject go, List<GameObject> gameObjects)
        {
            foreach (Transform child in go.transform)
            {
                if (child == null)
                    continue;

                gameObjects.Add(child.gameObject);
                GetChildRecursive(child.gameObject, gameObjects);
            }
        }

        public static void GetChildRecursive(GameObject go, List<TVEGameObjectData> gameObjectsData)
        {
            foreach (Transform child in go.transform)
            {
                if (child == null)
                    continue;

                var gameObjectData = new TVEGameObjectData();
                gameObjectData.gameObject = child.gameObject;

                gameObjectsData.Add(gameObjectData);
                GetChildRecursive(child.gameObject, gameObjectsData);
            }
        }
#endif

        // Math Utils
        public static float MathRemap(float value, float low1, float high1, float low2, float high2)
        {
            return low2 + (value - low1) * (high2 - low2) / (high1 - low1);
        }

        public static float MathVector2ToFloat(float x, float y)
        {
            Vector2 output;

            output.x = Mathf.Floor(x * (2048 - 1));
            output.y = Mathf.Floor(y * (2048 - 1));

            return (output.x * 2048) + output.y;
        }

        public static Vector2 MathFloatFromVector2(float input)
        {
            Vector2 output;

            output.y = input % 2048f;
            output.x = Mathf.Floor(input / 2048f);

            return output / (2048f - 1);
        }

        // Texture Utils
        public static Color GetGlobalTextureData(string globalTexture, Vector3 position, int layer, Texture2DArray texture2DArray)
        {
            var tveManager = TVEManager.Instance;

            if (tveManager == null)
            {
                return Color.black;
            }

            var texture = Shader.GetGlobalTexture(globalTexture) as RenderTexture;

            if (texture = null)
            {
                return Color.black;
            }

            if (layer > texture.volumeDepth)
            {
                Debug.Log("<b>[The Visual Engine]</b> The requested global texture layer does not exist!");
                return Color.black;
            }

            if (texture2DArray == null || texture2DArray.depth != texture.volumeDepth)
            {
                texture2DArray = new Texture2DArray(1, 1, texture.volumeDepth, TextureFormat.RGBAHalf, false);
            }

            var volumePosition = tveManager.focusPosition;
            var volumeScale = new Vector3(tveManager.renderBaseDistance * 2f, 100000, tveManager.renderBaseDistance * 2f);

            if (globalTexture.Contains("Near"))
            {
                volumeScale = new Vector3(tveManager.renderNearDistance * 2f, 100000, tveManager.renderNearDistance * 2f);
            }

            var normalizedPositionX = Mathf.Clamp(TVEUtils.MathRemap(position.x, volumePosition.x + (-volumeScale.x / 2), volumePosition.x + (volumeScale.x / 2), 0, 1), 0.001f, 1);
            var normalizedPositionZ = Mathf.Clamp(TVEUtils.MathRemap(position.z, volumePosition.z + (-volumeScale.z / 2), volumePosition.z + (volumeScale.z / 2), 0, 1), 0.001f, 1);

            var pixelPositionX = Mathf.RoundToInt(normalizedPositionX * texture.width - 1);
            var pixelPositionZ = Mathf.RoundToInt(normalizedPositionZ * texture.height - 1);

            var asyncGPUReadback = AsyncGPUReadback.Request(texture, 0, pixelPositionX, 1, pixelPositionZ, 1, layer, 1);
            asyncGPUReadback.WaitForCompletion();

            if (!asyncGPUReadback.hasError)
            {
                texture2DArray.SetPixelData(asyncGPUReadback.GetData<byte>(), 0, layer);
                texture2DArray.Apply();

                //AsyncGPUReadback.Request(renderData.texObject, 0, (AsyncGPUReadbackRequest asyncAction) =>
                //{
                //    texture2DArray.SetPixelData(asyncAction.GetData<byte>(), 0, 0);
                //    texture2DArray.Apply();
                //});

                var texture2DPixels = texture2DArray.GetPixels(layer, 0);

                return texture2DPixels[0];
            }
            else
            {
                return Color.black;
            }
        }

        // Material Utils
        public static void SetMaterialFloat(Material material, string valueProp, string internalProp)
        {
            if (material.HasProperty(valueProp))
            {
                material.SetFloat(internalProp, material.GetFloat(valueProp));
            }
        }

        public static void SetMaterialVector(Material material, string valueProp, string internalProp)
        {
            if (material.HasProperty(valueProp))
            {
                material.SetVector(internalProp, material.GetVector(valueProp));
            }
        }

        public static void SetMaterialTexture(Material material, string valueProp, string internalProp)
        {
            if (material.HasProperty(valueProp))
            {
                material.SetTexture(internalProp, material.GetTexture(valueProp));
            }
        }

        public static void SetMaterialCoords(Material material, string modeProp, string valueProp, string internalProp)
        {
            if (material.HasProperty(modeProp) && material.HasProperty(valueProp))
            {
                var mode = material.GetInt(modeProp);
                var value = material.GetVector(valueProp);

                if (mode == 0)
                {
                    material.SetVector(internalProp, value);
                }

                if (mode == 1)
                {
                    material.SetVector(internalProp, new Vector4(1 / value.x, 1 / value.y, value.z, value.w));
                }
            }
        }

        public static void SetMaterialBounds(Material material, string modeProp, string valueProp, string internalProp)
        {
            var offset = 0.0f;

            if (material.HasProperty(modeProp) && material.HasProperty(valueProp))
            {
                var mode = material.GetInt(modeProp);

                if (mode == 1)
                {
                    offset = 0.5f;
                }

                var value = material.GetVector(valueProp);
                var scale = new Vector2(1 / value.z, 1 / value.w);
                var pos = new Vector2(value.x * scale.x - offset, value.y * scale.y - offset) * -1;

                material.SetVector(internalProp, new Vector4(scale.x, scale.y, pos.x, pos.y));
            }
        }

        public static void SetMaterialOptions(Material material, string modeProp, string valueProp, int start)
        {
            if (material.HasProperty(modeProp))
            {
                var mode = material.GetInt(modeProp) - start;

                if (mode == 0)
                {
                    material.SetVector(valueProp, new Vector4(1, 0, 0, 0));
                }

                if (mode == 1)
                {
                    material.SetVector(valueProp, new Vector4(0, 1, 0, 0));
                }

                if (mode == 2)
                {
                    material.SetVector(valueProp, new Vector4(0, 0, 1, 0));
                }

                if (mode == 3)
                {
                    material.SetVector(valueProp, new Vector4(0, 0, 0, 1));
                }
            }
        }

        public static void SetMaterialOptions(Material material, string modeProp, string valueProp)
        {
            SetMaterialOptions(material, modeProp, valueProp, 0);
        }

        public static void SetMaterialKeyword(Material material, string property, string keyword)
        {
            if (material.HasProperty(property))
            {
                if (material.HasFloat(property))
                {
                    var mode = material.GetFloat(property);

                    if (mode == 0)
                    {
                        material.DisableKeyword(keyword);
                    }
                    else
                    {
                        material.EnableKeyword(keyword);
                    }
                }

                if (material.HasVector(property))
                {
                    var mode = material.GetVector(property);

                    if (mode.x == 0 && mode.y == 0)
                    {
                        material.DisableKeyword(keyword);
                    }
                    else
                    {
                        material.EnableKeyword(keyword);
                    }
                }
            }
            else
            {
                material.DisableKeyword(keyword);
            }
        }

        public static void SetMaterialKeyword(Material material, int index, string[] keywords)
        {
            for (int i = 0; i < keywords.Length; i++)
            {
                if (i == index)
                {
                    material.EnableKeyword(keywords[i]);
                }
                else
                {
                    material.DisableKeyword(keywords[i]);
                }
            }
        }

        public static void SetMaterialKeyword(Material material, string property, string[] keywords)
        {
            if (material.HasFloat(property))
            {
                var mode = material.GetFloat(property);

                for (int i = 0; i < keywords.Length; i++)
                {
                    if (i == mode)
                    {
                        material.EnableKeyword(keywords[i]);
                    }
                    else
                    {
                        material.DisableKeyword(keywords[i]);
                    }
                }
            }
        }

        public static void SetMaterialKeyword(Material material, string[] properties, string keyword)
        {
            float enableKeyword = 0;

            for (int i = 0; i < properties.Length; i++)
            {
                var property = properties[i];

                if (material.HasProperty(property))
                {
                    enableKeyword += material.GetFloat(property);
                }
            }

            if (enableKeyword > 0)
            {
                material.EnableKeyword(keyword);
            }
            else
            {
                material.DisableKeyword(keyword);
            }
        }

#if UNITY_EDITOR
        public static void ValidateMaterial(string path, bool unloadFromMemory)
        {
            var material = AssetDatabase.LoadAssetAtPath<Material>(path);

            if (path.Contains("Packages"))
            {
                return;
            }

            if (material == null)
            {
                return;
            }

            if (material.HasProperty("_IsTVEShader"))
            {
                TVEUtils.SetMaterialSettings(material);
                TVEUtils.SetLabel(path);
            }

            if (material.HasProperty("_IsElementShader"))
            {
                TVEUtils.SetElementSettings(material);
                TVEUtils.SetLabel(path);
            }

            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();

            if (unloadFromMemory)
            {
                TVEUtils.UnloadMaterialFromMemory(material);
            }
        }

        public static void UnloadMaterialFromMemory(Material material)
        {
            var shader = material.shader;

            for (int i = 0; i < ShaderUtil.GetPropertyCount(shader); i++)
            {
                if (ShaderUtil.GetPropertyType(shader, i) == ShaderUtil.ShaderPropertyType.TexEnv)
                {
                    var propName = ShaderUtil.GetPropertyName(shader, i);
                    var texture = material.GetTexture(propName);

                    if (texture != null)
                    {
                        Resources.UnloadAsset(texture);
                    }
                }
            }
        }

        public static void CopyMaterialProperties(Material oldMaterial, Material newMaterial)
        {
            var oldShader = oldMaterial.shader;
            var newShader = newMaterial.shader;

            for (int i = 0; i < ShaderUtil.GetPropertyCount(oldShader); i++)
            {
                for (int j = 0; j < ShaderUtil.GetPropertyCount(newShader); j++)
                {
                    var propertyName = ShaderUtil.GetPropertyName(oldShader, i);
                    var propertyType = ShaderUtil.GetPropertyType(oldShader, i);

                    if (propertyName == ShaderUtil.GetPropertyName(newShader, j))
                    {
                        if (propertyType == ShaderUtil.ShaderPropertyType.Color || propertyType == ShaderUtil.ShaderPropertyType.Vector)
                        {
#if UNITY_2022_1_OR_NEWER
                            if (!oldMaterial.IsPropertyLocked(propertyName))
                            {
                                newMaterial.SetVector(propertyName, oldMaterial.GetVector(propertyName));
                            }
#else
                            newMaterial.SetVector(propertyName, oldMaterial.GetVector(propertyName));
#endif
                        }

                        if (propertyType == ShaderUtil.ShaderPropertyType.Float || propertyType == ShaderUtil.ShaderPropertyType.Range)
                        {
#if UNITY_2022_1_OR_NEWER
                            if (!oldMaterial.IsPropertyLocked(propertyName))
                            {
                                newMaterial.SetFloat(propertyName, oldMaterial.GetFloat(propertyName));
                            }
#else
                            newMaterial.SetFloat(propertyName, oldMaterial.GetFloat(propertyName));
#endif
                        }

                        if (propertyType == ShaderUtil.ShaderPropertyType.TexEnv)
                        {
#if UNITY_2022_1_OR_NEWER
                            if (!oldMaterial.IsPropertyLocked(propertyName))
                            {
                                newMaterial.SetTexture(propertyName, oldMaterial.GetTexture(propertyName));
                            }
#else
                            newMaterial.SetTexture(propertyName, oldMaterial.GetTexture(propertyName));
#endif
                        }
                    }
                }
            }
        }

        public static void CopyMaterialPropertiesFromBlock(MaterialPropertyBlock propertyBlock, Material newMaterial)
        {
            var newShader = newMaterial.shader;

            for (int i = 0; i < ShaderUtil.GetPropertyCount(newShader); i++)
            {
                var propertyName = ShaderUtil.GetPropertyName(newShader, i);
                var propertyType = ShaderUtil.GetPropertyType(newShader, i);

                if (propertyType == ShaderUtil.ShaderPropertyType.Color || propertyType == ShaderUtil.ShaderPropertyType.Vector)
                {
                    if (propertyBlock.HasColor(propertyName))
                    {
                        newMaterial.SetVector(propertyName, propertyBlock.GetColor(propertyName));
                    }
                }

                if (propertyType == ShaderUtil.ShaderPropertyType.Float || propertyType == ShaderUtil.ShaderPropertyType.Range)
                {
                    if (propertyBlock.HasFloat(propertyName))
                    {
                        newMaterial.SetFloat(propertyName, propertyBlock.GetFloat(propertyName));
                    }
                }

                if (propertyType == ShaderUtil.ShaderPropertyType.TexEnv)
                {
                    if (propertyBlock.HasTexture(propertyName))
                    {
                        newMaterial.SetTexture(propertyName, propertyBlock.GetTexture(propertyName));
                    }
                }
            }
        }

        public static float GetMaterialSerializedFloat(Material material, string internalName, float defaultValue)
        {
            float value = defaultValue;

            if (EditorUtility.IsPersistent(material))
            {
                var so = new SerializedObject(material);
                var itr = so.GetIterator();

                while (itr.Next(true))
                {
                    if (itr.displayName == internalName)
                    {
                        if (itr.hasChildren)
                        {
                            var itrC = itr.Copy();
                            itrC.Next(true); //Walk into child ("First")
                            itrC.Next(false); //Walk into sibling ("Second")

                            value = itrC.floatValue;
                        }
                    }
                }
            }

            return value;
        }

        public static Vector4 GetMaterialSerializedVector(Material material, string internalName, Vector4 defaultValue)
        {
            Vector4 value = defaultValue;

            if (EditorUtility.IsPersistent(material))
            {
                var so = new SerializedObject(material);
                var itr = so.GetIterator();

                while (itr.Next(true))
                {
                    if (itr.displayName == internalName)
                    {
                        if (itr.hasChildren)
                        {
                            var itrC = itr.Copy();
                            itrC.Next(true); //Walk into child ("First")
                            itrC.Next(false); //Walk into sibling ("Second")

                            value = itrC.colorValue;
                        }
                    }
                }
            }

            return value;
        }

        public static Vector4 GetMaterialSerializedVector(Material material, string internalNameMin, string internalNameMax, Vector4 defaultValue)
        {
            Vector4 value = defaultValue;

            value.x = GetMaterialSerializedFloat(material, internalNameMin, 0);
            value.y = GetMaterialSerializedFloat(material, internalNameMax, 1);

            if (value.x > value.y)
            {
                value.w = 1;
            }

            return value;
        }

        public static Texture2D GetMaterialSerializedTexture(Material material, string internalName, Texture2D defaultValue)
        {
            Texture2D value = defaultValue;

            if (EditorUtility.IsPersistent(material))
            {
                var so = new SerializedObject(material);
                var itr = so.GetIterator();

                while (itr.Next(true))
                {
                    if (itr.displayName == internalName)
                    {
                        if (itr.hasChildren)
                        {
                            var itrC = itr.Copy();
                            itrC.Next(true); //Walk into child ("First")
                            itrC.Next(false); //Walk into sibling ("Second")

                            if (itrC.hasChildren)
                            {
                                var itrT = itrC.Copy();
                                itrT.Next(true); //Walk into child ("First")
                                value = (Texture2D)itrT.objectReferenceValue;
                            }
                        }
                    }
                }
            }

            return value;
        }

        public static void SetImpostorFeatures(Material oldMaterial, Material newMaterial)
        {
            SetImpostorFeature(oldMaterial, newMaterial, "_Occlusion");
            SetImpostorFeature(oldMaterial, newMaterial, "_Gradient");
            SetImpostorFeature(oldMaterial, newMaterial, "_Tinting");
            SetImpostorFeature(oldMaterial, newMaterial, "_Dryness");
            SetImpostorFeature(oldMaterial, newMaterial, "_Overlay");
            SetImpostorFeature(oldMaterial, newMaterial, "_Wetness");
            SetImpostorFeature(oldMaterial, newMaterial, "_Cutout");
        }

        public static void SetImpostorFeature(Material oldMaterial, Material newMaterial, string feature)
        {
            if (oldMaterial.HasProperty(feature + "BakeMode"))
            {
                var bake = oldMaterial.GetInt(feature + "BakeMode");

                if (bake == 0)
                {
                    newMaterial.SetFloat(feature + "BakeInfo", 0);
                }
                else
                {
                    newMaterial.SetFloat(feature + "BakeInfo", 1);
                    newMaterial.SetFloat(feature + "IntensityValue", 0);
                }
            }
        }

        // Shader Utils
        public static bool IsValidShader(string shaderPath)
        {
            bool valid = false;

            if (!shaderPath.Contains("GPUI") && !shaderPath.Contains("Helper") && !shaderPath.Contains("Legacy") && !shaderPath.Contains("Terrain Shaders"))
            {
                var shader = AssetDatabase.LoadAssetAtPath<Shader>(shaderPath);

                if (shader != null && !shader.name.Contains("Landscape"))
                {
                    var material = new Material(shader);

                    if (material.HasProperty("_IsTVEShader"))
                    {
                        valid = true;
                    }
                }
            }

            return valid;
        }

        public static List<string> GetCoreShaderPaths()
        {
            var coreShaderPaths = new List<string>();

            var allShaderPaths = Directory.GetFiles("Assets/", "*.shader", SearchOption.AllDirectories);

            for (int i = 0; i < allShaderPaths.Length; i++)
            {
                if (IsValidShader(allShaderPaths[i]))
                {
                    coreShaderPaths.Add(allShaderPaths[i]);
                }
            }

            return coreShaderPaths;
        }

        public static string[] ShaderModelOptions =
        {
            "Shader Model 2.0",
            "Shader Model 2.5",
            "Shader Model 3.0",
            "Shader Model 3.5",
            "Shader Model 4.0",
            "Shader Model 4.5",
            "Shader Model 4.6",
            "Shader Model 5.0",
        };

        public static string[] RenderEngineOptions =
        {
            "Unity Default Renderer",
            "Vegetation Studio (Instanced Indirect)",
            "Vegetation Studio 1.4.5+ (Instanced Indirect)",
            "Vegetation Studio Beyond (Instanced Indirect)",
            "Nature Renderer (Instanced Indirect)",
            "Foliage Renderer (Instanced Indirect)",
            "Flora Renderer (Instanced Indirect)",
            "Flora Renderer 2 (Instanced Indirect)",
            "GPU Instancer (Instanced Indirect)",
            "GPU Instancer Pro (Instanced Indirect)",
            "Vegetation Instancer (Instanced Indirect)",
            "Renderer Stack (Instanced Indirect)",
            "Instant Renderer (Instanced Indirect)",
            "Disable SRP Batcher Compatibility",
        };

        public static TVEShaderSettings GetShaderSettings(string shaderPath)
        {
            TVEShaderSettings shaderSettings = new TVEShaderSettings();
            shaderSettings.shaderPath = shaderPath;

            StreamReader reader = new StreamReader(shaderPath);

            string lines = reader.ReadToEnd();

            for (int i = 0; i < RenderEngineOptions.Length; i++)
            {
                var renderEngine = RenderEngineOptions[i];

                if (lines.Contains(renderEngine))
                {
                    shaderSettings.renderEngine = renderEngine;
                    break;
                }
            }

            for (int i = 0; i < ShaderModelOptions.Length; i++)
            {
                var shaderModel = ShaderModelOptions[i].Replace("Shader Model", "#pragma target");

                if (lines.Contains(shaderModel))
                {
                    shaderSettings.shaderModel = ShaderModelOptions[i];
                    break;
                }
            }

            reader.Close();

            return shaderSettings;
        }

        public static void SetShaderSettings(string shaderPath, TVEShaderSettings shaderSettings)
        {
            var renderEngine = shaderSettings.renderEngine;
            var shaderModel = shaderSettings.shaderModel.Replace("Shader Model", "#pragma target");

            string[] engineVegetationStudio = new string[]
            {
            "           //Vegetation Studio (Instanced Indirect)",
            "           #include \"XXX/Core/Shaders/Includes/VS_Indirect.cginc\"",
            "           #pragma instancing_options procedural:setup forwardadd",
            "           #pragma multi_compile GPU_FRUSTUM_ON __",
            };

            string[] engineVegetationStudioHD = new string[]
            {
            "           //Vegetation Studio (Instanced Indirect)",
            "           #include \"XXX/Core/Shaders/Includes/VS_IndirectHD.cginc\"",
            "           #pragma instancing_options procedural:setupVSPro forwardadd",
            "           #pragma multi_compile GPU_FRUSTUM_ON __",
            };

            string[] engineVegetationStudio145 = new string[]
            {
            "           //Vegetation Studio 1.4.5+ (Instanced Indirect)",
            "           #include \"XXX/Core/Shaders/Includes/VS_Indirect145.cginc\"",
            "           #pragma instancing_options procedural:setupVSPro forwardadd",
            "           #pragma multi_compile GPU_FRUSTUM_ON __",
            };

            string[] engineVegetationStudioBeyond = new string[]
            {
            "           //Vegetation Studio Beyond (Instanced Indirect)",
            "           #include \"XXX/Core/Shaders/Includes/VSB_Indirect.cginc\"",
            "           #pragma instancing_options procedural:setupVSPro forwardadd",
            "           #pragma multi_compile GPU_FRUSTUM_ON __",
            };

            string[] engineVegetationStudioBeyondHD = new string[]
            {
            "           //Vegetation Studio Beyond (Instanced Indirect)",
            "           #include \"XXX/Core/Shaders/Includes/VSB_IndirectHD.cginc\"",
            "           #pragma instancing_options procedural:setupVSPro forwardadd",
            "           #pragma multi_compile GPU_FRUSTUM_ON __",
            };

            string[] engineNatureRenderer = new string[]
            {
            "           //Nature Renderer (Instanced Indirect)",
            "           #include \"XXX\"",
            "           #pragma instancing_options procedural:SetupNatureRenderer forwardadd",
            };

            string[] engineFoliageRenderer = new string[]
            {
            "           //Foliage Renderer (Instanced Indirect)",
            "           #include \"XXX\"",
            "           #pragma instancing_options procedural:setupFoliageRenderer forwardadd",
            };

            string[] engineFloraRenderer = new string[]
            {
            "           //Flora Renderer (Instanced Indirect)",
            "           #include \"XXX\"",
            "           #pragma instancing_options procedural:FloraInstancingSetup forwardadd",
            };

            string[] engineFloraRenderer2 = new string[]
            {
            "           //Flora Renderer 2 (Instanced Indirect)",
            "           #include_with_pragmas \"XXX\"",
            "           #pragma instancing_options procedural:FloraInstancingSetup forwardadd",
            };

            string[] engineGPUInstancer = new string[]
            {
            "           //GPU Instancer (Instanced Indirect)",
            "           #include \"XXX\"",
            "           #pragma instancing_options procedural:setupGPUI forwardadd",
            };

            string[] engineGPUInstancerPro = new string[]
            {
            "           //GPU Instancer Pro (Instanced Indirect)",
            "           #include_with_pragmas \"XXX\"",
            "           #pragma instancing_options procedural:setupGPUI forwardadd",
            };

            string[] engineVegetationInstancer = new string[]
            {
            "           //Vegetation Instancer (Instanced Indirect)",
            "           #include \"XXX\"",
            "           #pragma instancing_options procedural:setupGPUInstancedIndirect",
            };

            string[] engineRendererStack = new string[]
            {
            "           //Renderer Stack (Instanced Indirect)",
            "           #include \"XXX\"",
            "           #pragma instancing_options procedural:GPUInstancedIndirectInclude forwardadd",
            };

            string[] engineInstantRenderer = new string[]
            {
            "           //Instant Renderer (Instanced Indirect)",
            "           #include \"XXX\"",
            "           #pragma instancing_options procedural:setupInstantRenderer forwardadd",
            };

            var assetFolder = TVEUtils.GetAssetFolder();

            var cgincNatureRenderer = "Assets/Visual Design Cafe/Nature Renderer/Shader Includes/Nature Renderer.templatex";

            if (!File.Exists(cgincNatureRenderer))
            {
                cgincNatureRenderer = TVEUtils.FindAsset("Nature Renderer.templatex");
            }

            var cgincFoliageRenderer = "Packages/com.jbooth.foliagerenderer/Shaders/FoliageRendererInstancing.cginc";

            if (!File.Exists(cgincFoliageRenderer))
            {
                cgincFoliageRenderer = TVEUtils.FindAsset("FoliageRendererInstancing.cginc");
            }

            var cgincFloraRenderer = "Packages/com.ma.flora/ShaderLibrary/Flora.hlsl";

            if (!File.Exists(cgincFloraRenderer))
            {
                cgincFloraRenderer = TVEUtils.FindAsset("Flora.hlsl");
            }

            var cgincFloraRenderer2 = "Packages/com.ma.flora/ShaderLibrary/Instancing.hlsl";

            if (!File.Exists(cgincFloraRenderer2))
            {
                cgincFloraRenderer2 = TVEUtils.FindAsset("Instancing.hlsl");
            }

            var cgincGPUInstancer = "Assets/GPUInstancer/Shaders/Include/GPUInstancerInclude.cginc";

            if (!File.Exists(cgincGPUInstancer))
            {
                cgincGPUInstancer = TVEUtils.FindAsset("GPUInstancerInclude.cginc");
            }

            var cgincGPUInstancerPro = "Packages/com.gurbu.gpui-pro/Runtime/Shaders/Include/GPUInstancerSetup.hlsl";

            if (!File.Exists(cgincGPUInstancerPro))
            {
                cgincGPUInstancerPro = TVEUtils.FindAsset("GPUInstancerSetup.hlsl");
            }

            var cgincVegetationInstancer = "Assets/VegetationInstancer/Shaders/Include/GPUInstancedIndirectInclude.cginc";

            if (!File.Exists(cgincVegetationInstancer))
            {
                cgincVegetationInstancer = TVEUtils.FindAsset("GPUInstancedIndirectInclude.cginc");
            }

            var cgincRendererStack = "Assets/VladislavTsurikov/RendererStack/Shaders/Include/GPUInstancedIndirectInclude.cginc";

            if (!File.Exists(cgincRendererStack))
            {
                cgincRendererStack = TVEUtils.FindAsset("GPUInstancedIndirectInclude.cginc");
            }

            var cgincInstantRenderer = "Assets/VladislavTsurikov/InstantRenderer/Shaders/Include/InstantRendererInclude.cginc";

            if (!File.Exists(cgincInstantRenderer))
            {
                cgincInstantRenderer = TVEUtils.FindAsset("GPUInstancerInclude.cginc");
            }

            // Add correct paths for VSP and GPUI
            engineVegetationStudio[1] = engineVegetationStudio[1].Replace("XXX", assetFolder);
            engineVegetationStudioHD[1] = engineVegetationStudioHD[1].Replace("XXX", assetFolder);
            engineVegetationStudio145[1] = engineVegetationStudio145[1].Replace("XXX", assetFolder);
            engineVegetationStudioBeyond[1] = engineVegetationStudioBeyond[1].Replace("XXX", assetFolder);
            engineVegetationStudioBeyondHD[1] = engineVegetationStudioBeyondHD[1].Replace("XXX", assetFolder);
            engineNatureRenderer[1] = engineNatureRenderer[1].Replace("XXX", cgincNatureRenderer);
            engineFoliageRenderer[1] = engineFoliageRenderer[1].Replace("XXX", cgincFoliageRenderer);
            engineFloraRenderer[1] = engineFloraRenderer[1].Replace("XXX", cgincFloraRenderer);
            engineFloraRenderer2[1] = engineFloraRenderer2[1].Replace("XXX", cgincFloraRenderer2);
            engineGPUInstancer[1] = engineGPUInstancer[1].Replace("XXX", cgincGPUInstancer);
            engineGPUInstancerPro[1] = engineGPUInstancerPro[1].Replace("XXX", cgincGPUInstancerPro);
            engineVegetationInstancer[1] = engineVegetationInstancer[1].Replace("XXX", cgincVegetationInstancer);
            engineRendererStack[1] = engineRendererStack[1].Replace("XXX", cgincRendererStack);
            engineInstantRenderer[1] = engineInstantRenderer[1].Replace("XXX", cgincInstantRenderer);

            var isHDPipeline = false;

            StreamReader reader = new StreamReader(shaderPath);

            List<string> lines = new List<string>();

            while (!reader.EndOfStream)
            {
                lines.Add(reader.ReadLine());
            }

            reader.Close();

            int count = lines.Count;

            if (shaderModel != "From Shader")
            {
                for (int i = 0; i < count; i++)
                {
                    if (lines[i].Contains("#pragma target"))
                    {
                        lines[i] = shaderModel;
                    }
                }
            }

            for (int i = 0; i < count; i++)
            {
                if (lines[i].Contains("SHADER INJECTION POINT BEGIN"))
                {
                    int c = 0;
                    int j = i + 1;

                    while (lines[j].Contains("SHADER INJECTION POINT END") == false)
                    {
                        j++;
                        c++;
                    }

                    lines.RemoveRange(i + 1, c);
                    count = count - c;
                }
            }

            count = lines.Count;

            for (int i = 0; i < count; i++)
            {
                if (lines[i].Contains("HDRenderPipeline"))
                {
                    isHDPipeline = true;
                }

                if (lines[i].Contains("[HideInInspector] _DisableSRPBatcher"))
                {
                    lines.RemoveAt(i);
                    count--;
                }
            }

            //Inject 3rd Party Support
            if (renderEngine.Contains("Vegetation Studio (Instanced Indirect)"))
            {
                for (int i = 0; i < lines.Count; i++)
                {
                    if (lines[i].Contains("SHADER INJECTION POINT BEGIN"))
                    {
                        if (isHDPipeline)
                        {
                            lines.InsertRange(i + 1, engineVegetationStudioHD);
                        }
                        else
                        {
                            lines.InsertRange(i + 1, engineVegetationStudio);
                        }
                    }
                }
            }

            if (renderEngine.Contains("Vegetation Studio 1.4.5+ (Instanced Indirect)"))
            {
                for (int i = 0; i < lines.Count; i++)
                {
                    if (lines[i].Contains("SHADER INJECTION POINT BEGIN"))
                    {
                        lines.InsertRange(i + 1, engineVegetationStudio145);
                    }
                }
            }

            if (renderEngine.Contains("Vegetation Studio Beyond (Instanced Indirect)"))
            {
                for (int i = 0; i < lines.Count; i++)
                {
                    if (lines[i].Contains("SHADER INJECTION POINT BEGIN"))
                    {
                        if (isHDPipeline)
                        {
                            lines.InsertRange(i + 1, engineVegetationStudioBeyondHD);
                        }
                        else
                        {
                            lines.InsertRange(i + 1, engineVegetationStudioBeyond);
                        }
                    }
                }
            }

            if (renderEngine.Contains("Nature Renderer (Instanced Indirect)"))
            {
                for (int i = 0; i < lines.Count; i++)
                {
                    if (lines[i].Contains("SHADER INJECTION POINT BEGIN"))
                    {
                        lines.InsertRange(i + 1, engineNatureRenderer);
                    }
                }
            }

            if (renderEngine.Contains("Foliage Renderer (Instanced Indirect)"))
            {
                for (int i = 0; i < lines.Count; i++)
                {
                    if (lines[i].Contains("SHADER INJECTION POINT BEGIN"))
                    {
                        lines.InsertRange(i + 1, engineFoliageRenderer);
                    }
                }
            }

            if (renderEngine.Contains("Flora Renderer (Instanced Indirect)"))
            {
                for (int i = 0; i < lines.Count; i++)
                {
                    if (lines[i].Contains("SHADER INJECTION POINT BEGIN"))
                    {
                        lines.InsertRange(i + 1, engineFloraRenderer);
                    }
                }
            }

            if (renderEngine.Contains("Flora Renderer 2 (Instanced Indirect)"))
            {
                for (int i = 0; i < lines.Count; i++)
                {
                    if (lines[i].Contains("SHADER INJECTION POINT BEGIN"))
                    {
                        lines.InsertRange(i + 1, engineFloraRenderer2);
                    }
                }
            }

            if (renderEngine.Contains("GPU Instancer (Instanced Indirect)"))
            {
                for (int i = 0; i < lines.Count; i++)
                {
                    if (lines[i].Contains("SHADER INJECTION POINT BEGIN"))
                    {
                        lines.InsertRange(i + 1, engineGPUInstancer);
                    }
                }
            }

            if (renderEngine.Contains("GPU Instancer Pro (Instanced Indirect)"))
            {
                for (int i = 0; i < lines.Count; i++)
                {
                    if (lines[i].Contains("SHADER INJECTION POINT BEGIN"))
                    {
                        lines.InsertRange(i + 1, engineGPUInstancerPro);
                    }
                }
            }

            if (renderEngine.Contains("Vegetation Instancer (Instanced Indirect)"))
            {
                for (int i = 0; i < lines.Count; i++)
                {
                    if (lines[i].Contains("SHADER INJECTION POINT BEGIN"))
                    {
                        lines.InsertRange(i + 1, engineVegetationInstancer);
                    }
                }
            }

            if (renderEngine.Contains("Renderer Stack (Instanced Indirect)"))
            {
                for (int i = 0; i < lines.Count; i++)
                {
                    if (lines[i].Contains("SHADER INJECTION POINT BEGIN"))
                    {
                        lines.InsertRange(i + 1, engineRendererStack);
                    }
                }
            }

            if (renderEngine.Contains("Instant Renderer (Instanced Indirect)"))
            {
                for (int i = 0; i < lines.Count; i++)
                {
                    if (lines[i].Contains("SHADER INJECTION POINT BEGIN"))
                    {
                        lines.InsertRange(i + 1, engineInstantRenderer);
                    }
                }
            }

            if (renderEngine.Contains("Disable SRP Batcher Compatibility"))
            {
                for (int i = 0; i < lines.Count; i++)
                {
                    if (lines[i].EndsWith("Properties"))
                    {
                        lines.Insert(i + 2, "		[HideInInspector] _DisableSRPBatcher(\"_DisableSRPBatcher\", Float) = 0 //Disable SRP Batcher Compatibility");
                    }
                }
            }

            //for (int i = 0; i < lines.Count; i++)
            //{
            //    // Disable ASE Drawers
            //    if (lines[i].Contains("[ASEBegin]"))
            //    {
            //        lines[i] = lines[i].Replace("[ASEBegin]", "");
            //    }

            //    if (lines[i].Contains("[ASEnd]"))
            //    {
            //        lines[i] = lines[i].Replace("[ASEnd]", "");
            //    }
            //}

#if !AMPLIFY_SHADER_EDITOR && !UNITY_2020_2_OR_NEWER

            // Add diffusion profile support for HDRP 7
            if (isHDPipeline)
            {
                if (shaderPath.Contains("Subsurface Lit"))
                {
                    for (int i = 0; i < lines.Count; i++)
                    {
                        if (lines[i].Contains("[DiffusionProfile]"))
                        {
                            lines[i] = lines[i].Replace("[DiffusionProfile]", "[StyledDiffusionMaterial(_SubsurfaceDiffusion)]");
                        }
                    }
                }
            }

#elif AMPLIFY_SHADER_EDITOR && !UNITY_2020_2_OR_NEWER

            // Add diffusion profile support
            if (isHDPipeline)
            {
                if (shaderAssetPath.Contains("Subsurface Lit"))
                {
                    for (int i = 0; i < lines.Count; i++)
                    {
                        if (lines[i].Contains("[HideInInspector][Space(10)][ASEDiffusionProfile(_SubsurfaceDiffusion)]"))
                        {
                            lines[i] = lines[i].Replace("[HideInInspector][Space(10)][ASEDiffusionProfile(_SubsurfaceDiffusion)]", "[Space(10)][ASEDiffusionProfile(_SubsurfaceDiffusion)]");
                        }

                        if (lines[i].Contains("[DiffusionProfile]") && !lines[i].Contains("[HideInInspector]"))
                        {
                            lines[i] = lines[i].Replace("[DiffusionProfile]", "[HideInInspector][DiffusionProfile]");
                        }

                        if (lines[i].Contains("[StyledDiffusionMaterial(_SubsurfaceDiffusion)]"))
                        {
                            lines[i] = lines[i].Replace("[StyledDiffusionMaterial(_SubsurfaceDiffusion)]", "[HideInInspector][StyledDiffusionMaterial(_SubsurfaceDiffusion)]");
                        }
                    }
                }
            }
#endif

            StreamWriter writer = new StreamWriter(shaderPath);

            for (int i = 0; i < lines.Count; i++)
            {
                writer.WriteLine(lines[i]);
            }

            writer.Close();

            lines = new List<string>();

            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();

            AssetDatabase.ImportAsset(shaderPath);
        }

        public static void CopyOrReplaceShader(string oldShaderPath, string newShaderPath, string newShaderName)
        {
            if (File.Exists(newShaderPath))
            {
                // Copy old shader content
                StreamReader reader = new StreamReader(oldShaderPath);

                List<string> lines = new List<string>();

                while (!reader.EndOfStream)
                {
                    lines.Add(reader.ReadLine());
                }

                reader.Close();

                for (int i = 0; i < 10; i++)
                {
                    if (lines[i].StartsWith("Shader"))
                    {
                        lines[i] = "Shader \"" + newShaderName + "\"";
                    }
                }

                StreamWriter writer = new StreamWriter(newShaderPath, false);

                for (int i = 0; i < lines.Count; i++)
                {
                    writer.WriteLine(lines[i]);
                }

                writer.Close();

                lines = new List<string>();

                AssetDatabase.ImportAsset(newShaderPath);
                AssetDatabase.Refresh();
            }
            else
            {
                AssetDatabase.CopyAsset(oldShaderPath, newShaderPath);
                AssetDatabase.Refresh();
            }
        }

        // GUI Utils
        public static void DrawShaderBanner(Material material)
        {
            GUIStyle titleStyle = new GUIStyle("label")
            {
                richText = true,
                alignment = TextAnchor.MiddleCenter
            };

            GUIStyle subTitleStyle = new GUIStyle("label")
            {
                richText = true,
                alignment = TextAnchor.MiddleRight
            };

            var splitLine = material.shader.name.Split(char.Parse("/"));
            var splitCount = splitLine.Length;
            var shaderName = splitLine[splitCount - 1];
            var shaderCategory = splitLine[splitCount - 2];
            var shaderType = splitLine[splitCount - 3];

            var subtitle = "";

            if (shaderType == "The Visual Engine")
            {
                if (AssetDatabase.GetAssetPath(material.shader).Contains("Core"))
                {
                    if (EditorGUIUtility.isProSkin)
                    {
                        subtitle = "<color=#ffdb78>" + "Core" + " / " + shaderCategory + "</color>";
                    }
                    else
                    {
                        subtitle = "Core" + " / " + shaderCategory;
                    }
                }
                else
                {
                    if (EditorGUIUtility.isProSkin)
                    {
                        subtitle = "<color=#8affc4>" + "Custom" + " / " + shaderCategory + "</color>";
                    }
                    else
                    {
                        subtitle = "<b><color=#008c60>" + "Custom" + " / " + shaderCategory + "</color></b>";
                    }
                }
            }
            else
            {
                if (shaderType.Contains("The Visual Engine "))
                {
                    shaderType = shaderType.Replace("The Visual Engine ", "");

                    if (EditorGUIUtility.isProSkin)
                    {
                        subtitle = "<color=#8affc4>" + shaderType + " / " + shaderCategory + "</color>";
                    }
                    else
                    {
                        subtitle = "<b><color=#008c60>" + "Custom" + " / " + shaderCategory + "</color></b>";
                    }
                }
            }

            GUILayout.Space(10);

            var fullRect = GUILayoutUtility.GetRect(0, 0, 36, 0);
            var fillRect = new Rect(0, fullRect.position.y, fullRect.xMax + 3, 36);
            var subRect = new Rect(0, fullRect.position.y - 2, fullRect.xMax, 36);
            var lineRect = new Rect(0, fullRect.position.y, fullRect.xMax + 3, 1);

            Color color;
            Color guiColor;

            if (EditorGUIUtility.isProSkin)
            {
                color = CONSTANT.ColorDarkGray;
                guiColor = CONSTANT.ColorLightGray;
            }
            else
            {
                color = CONSTANT.ColorLightGray;
                guiColor = CONSTANT.ColorDarkGray;
            }

            EditorGUI.DrawRect(fillRect, color);
            EditorGUI.DrawRect(lineRect, CONSTANT.LineColor);

            GUI.Label(fullRect, "<size=16><color=#" + ColorUtility.ToHtmlStringRGB(guiColor) + ">" + shaderName + "</color></size>", titleStyle);
            GUI.Label(subRect, "<size=10>" + subtitle + "</size>", subTitleStyle);

            GUILayout.Space(10);
        }

        public static void DrawTechnicalDetails(Material material)
        {
            GUILayout.Space(10);

            var shaderName = material.shader.name;
            var projectPipeline = TVEUtils.GetProjectPipeline();

            var styleLabel = new GUIStyle(EditorStyles.label)
            {
                richText = true,
                alignment = TextAnchor.MiddleLeft,
                wordWrap = true,
            };


            if (shaderName.Contains("Simple Lit"))
            {
                DrawTechincalLabel("Shader Complexity: Optimized", styleLabel);
            }

            if (shaderName.Contains("Standard Lit") || shaderName.Contains("Subsurface Lit"))
            {
                DrawTechincalLabel("Shader Complexity: Balanced", styleLabel);
            }

            if (!material.HasProperty("_IsElementShader"))
            {
                if (projectPipeline == "High Definition")
                {
                    DrawTechincalLabel("Render Pipeline: High Definition Render Pipeline", styleLabel);
                }
                else if (projectPipeline == "Universal")
                {
                    DrawTechincalLabel("Render Pipeline: Universal Render Pipeline", styleLabel);
                }
                else
                {
                    DrawTechincalLabel("Render Pipeline: Standard Render Pipeline", styleLabel);
                }
            }
            else
            {
                DrawTechincalLabel("Render Pipeline: Any Render Pipeline", styleLabel);
            }

            DrawTechincalLabel("Render Queue: " + material.renderQueue.ToString(), styleLabel);

            if (shaderName.Contains("Standard Lit"))
            {
                DrawTechincalLabel("Render Path: Rendered in both Forward and Deferred path", styleLabel);
            }

            if (shaderName.Contains("Subsurface Lit"))
            {
                DrawTechincalLabel("Render Path: Always rendered in Forward path", styleLabel);
            }

            if (shaderName.Contains("Standard Lit") || shaderName.Contains("Subsurface Lit"))
            {
                DrawTechincalLabel("Lighting Model: Physicaly Based Shading", styleLabel);
            }

            if (shaderName.Contains("Simple Lit"))
            {
                DrawTechincalLabel("Lighting Model: Blinn Phong Shading", styleLabel);
            }

            if (!shaderName.Contains("Terrain"))
            {
                if (shaderName.Contains("Simple Lit") || shaderName.Contains("Standard Lit"))
                {
                    DrawTechincalLabel("Subsurface Model: Subsurface Scattering Approximation", styleLabel);
                }

                if (shaderName.Contains("Subsurface Lit"))
                {
                    DrawTechincalLabel("Subsurface Model: Translucency Subsurface Scattering", styleLabel);
                }

                if (material.HasProperty("_IsImpostorShader") || material.HasProperty("_IsElementShader"))
                {
                    DrawTechincalLabel("Batching Support: No", styleLabel);
                }
                else
                {
                    DrawTechincalLabel("Batching Support: Limited, depending on the used features", styleLabel);
                }
            }

            var elementTag = material.GetTag("ElementType", false, "");

            if (elementTag != "")
            {
                DrawTechincalLabel("Element Type Tag: " + elementTag, styleLabel);
            }
        }

        public static void DrawActiveKeywords(Material material)
        {
            GUILayout.Space(10);

            var styleLabel = new GUIStyle(EditorStyles.label)
            {
                richText = true,
                alignment = TextAnchor.MiddleLeft,
                wordWrap = true,
            };

            var keywords = material.enabledKeywords;

            for (int i = 0; i < keywords.Length; i++)
            {
                DrawTechincalLabel(keywords[i].name, styleLabel);
            }
        }

        public static void DrawTechincalLabel(string label, GUIStyle style)
        {
            GUILayout.Label("<size=10>" + label + "</size>", style);
        }

        public static void DrawCopySettingsFromObject(Material material)
        {
            Object inputObject = null;

            GUILayout.BeginHorizontal();
            inputObject = (Object)EditorGUILayout.ObjectField("Copy Settings From Object", inputObject, typeof(Object), true, GUILayout.Height(17));
            GUILayout.Space(2);
            GUILayout.EndHorizontal();

            if (inputObject != null)
            {
                if (inputObject.GetType() == typeof(GameObject))
                {
                    var gameObject = (GameObject)inputObject;

                    if (gameObject.GetComponent<TVETerrain>() != null)
                    {
                        var terrain = gameObject.GetComponent<TVETerrain>();

                        if (terrain.terrainMaterial != null)
                        {
                            CopyMaterialProperties(terrain.terrainMaterial, material);
                        }

                        if (terrain.terrainPropertyBlock != null)
                        {
                            CopyMaterialPropertiesFromBlock(terrain.terrainPropertyBlock, material);
                        }

                        Debug.Log("<b>[The Visual Engine]</b> " + "Terrain material settings copied to the current material!");
                    }
                    else
                    {
                        List<Material> allMaterials = new();
                        List<GameObject> allGameObjects = new();

                        allGameObjects.Add(gameObject);
                        GetChildRecursive(gameObject, allGameObjects);

                        for (int i = 0; i < allGameObjects.Count; i++)
                        {
                            var currentGameObject = allGameObjects[i];
                            var currentRenderer = currentGameObject.GetComponent<MeshRenderer>();
                            Material[] sharedMaterials = null;

                            if (currentRenderer != null)
                            {
                                sharedMaterials = currentRenderer.sharedMaterials;
                            }

                            if (sharedMaterials != null)
                            {
                                for (int j = 0; j < sharedMaterials.Length; j++)
                                {
                                    var currentMaterial = sharedMaterials[j];

                                    if (currentMaterial != null && currentMaterial != material && !allMaterials.Contains(currentMaterial))
                                    {
                                        allMaterials.Add(currentMaterial);
                                    }
                                }
                            }
                        }

                        if (allMaterials.Count == 0)
                        {
                            Debug.Log("<b>[The Visual Engine]</b> " + "No material to copy from found!");
                        }
                        else if (allMaterials.Count == 1)
                        {
                            var oldMaterial = allMaterials[0];

                            CopyMaterialProperties(oldMaterial, material);
                            SetImpostorFeatures(oldMaterial, material);
                            material.SetFloat("_IsInitialized", 1);

                            Debug.Log("<b>[The Visual Engine]</b> " + "Gameobject material settings copied to the current material!");
                        }
                        else if (allMaterials.Count > 1)
                        {
                            for (int i = 0; i < allMaterials.Count; i++)
                            {
                                var oldMaterial = allMaterials[i];

                                bool copySettings = EditorUtility.DisplayDialog("Copy Material Settings?", "Copy the settings from " + oldMaterial.name.ToUpper() + "?", "Copy Material Settings", "Skip");

                                if (copySettings)
                                {
                                    CopyMaterialProperties(oldMaterial, material);
                                    SetImpostorFeatures(oldMaterial, material);
                                    material.SetFloat("_IsInitialized", 1);
                                }
                            }

                            Debug.Log("<b>[The Visual Engine]</b> " + "Selected material settings copied to the current material!");
                        }
                    }
                }

                if (inputObject.GetType() == typeof(Material))
                {
                    var oldMaterial = (Material)inputObject;

                    if (oldMaterial != null)
                    {
                        CopyMaterialProperties(oldMaterial, material);

                        material.SetFloat("_IsInitialized", 1);

                        Debug.Log("<b>[The Visual Engine]</b> " + "Selected material settings copied to the current material!");
                    }
                }

                inputObject = null;
            }
        }

        public static void DrawRenderQueue(Material material, MaterialEditor materialEditor)
        {
            if (material.HasProperty("_RenderQueue") && material.HasProperty("_RenderPriority"))
            {
                var mode = material.GetInt("_RenderQueue");
                var priority = material.GetInt("_RenderPriority");

                mode = EditorGUILayout.Popup("Render Queue Mode", mode, new string[] { "Auto", "Priority", "User Defined" });

                if (mode == 0)
                {
                    priority = 0;
                }
                else if (mode == 1)
                {
                    priority = EditorGUILayout.IntSlider("Render Priority", priority, -100, 100);
                }
                else
                {
                    priority = 0;
                    materialEditor.RenderQueueField();
                }

                material.SetInt("_RenderQueue", mode);
                material.SetInt("_RenderPriority", priority);
            }
        }

        public static void DrawBakeGIMode(Material material)
        {
            if (material.HasProperty("_RenderBakeGI") && material.HasProperty("_RenderCull"))
            {
                var mode = material.GetInt("_RenderBakeGI");
                var cull = material.GetInt("_RenderCull");

                mode = EditorGUILayout.Popup("Double Sided GI Mode", mode, new string[] { "Auto", "Off", "On" });

                if (mode == 0)
                {
                    if (cull == 0)
                    {
                        material.doubleSidedGI = true;
                    }
                    else
                    {
                        material.doubleSidedGI = false;
                    }
                }
                else if (mode == 1)
                {
                    material.doubleSidedGI = false;
                }
                else
                {
                    material.doubleSidedGI = true;
                }

                material.SetInt("_RenderBakeGI", mode);
            }
        }

        public static string DrawSearchField(string searchText, out string[] searchResult, int space)
        {
            GUILayout.BeginHorizontal();
            //GUI.color = new Color(1, 1, 1, 0.9f);
            GUILayout.Space(space);

            GUIStyle searchStyle = GUI.skin.FindStyle("ToolbarSearchTextField");

            if (searchStyle == null)
            {
                searchStyle = GUI.skin.FindStyle("ToolbarSeachTextField");
            }

            searchText = GUILayout.TextField(searchText, searchStyle);

            //GUI.color = Color.white;
            GUILayout.EndHorizontal();

            var searchInvariant = searchText.ToUpper();
            searchResult = searchInvariant.Split(" ");

            if (searchResult == null)
            {
                searchResult = new string[] { "" };
            }

            return searchText;
        }

        public static void DrawPoweredBy()
        {
            var styleLabelCentered = new GUIStyle(EditorStyles.label)
            {
                richText = true,
                alignment = TextAnchor.MiddleCenter,
            };

            Rect lastRect0 = GUILayoutUtility.GetLastRect();
            EditorGUI.DrawRect(new Rect(0, lastRect0.yMax, 1000, 1), new Color(0, 0, 0, 0.4f));

            GUILayout.Space(10);

            GUILayout.Label("<size=10><color=#808080>Powered by The Visual Engine</color></size>", styleLabelCentered);

            Rect labelRect = GUILayoutUtility.GetLastRect();

            if (GUI.Button(labelRect, "", new GUIStyle()))
            {
                Application.OpenURL("http://u3d.as/1H9u");
            }

            GUILayout.Space(5);
        }

        public static void DrawToolbar(int leftSpace, int rightSpace)
        {
            var styledToolbar = new GUIStyle(EditorStyles.toolbarButton)
            {
                alignment = TextAnchor.MiddleCenter,
                fontStyle = FontStyle.Normal,
                fontSize = 11,
            };

            GUILayout.BeginHorizontal();
            GUILayout.Space(leftSpace);
            if (GUILayout.Button("Discord", styledToolbar))
            {
                Application.OpenURL("https://discord.com/invite/znxuXET");
            }
            GUILayout.Space(-1);

            if (GUILayout.Button("Manual", styledToolbar))
            {
                Application.OpenURL("https://docs.google.com/document/d/1ofHGsicGeyvCQTCky4ec5q96Ttaxub_PuuJ0YEoFpWk");
            }
            GUILayout.Space(-1);

            if (GUILayout.Button("Modules", styledToolbar))
            {
                Application.OpenURL("https://assetstore.unity.com/publishers/20529");
            }

#if UNITY_2020_3_OR_NEWER
            var rectModules = GUILayoutUtility.GetLastRect();
            var iconModules = new Rect(rectModules.xMax - 26, rectModules.y, 20, 20);
            GUI.color = new Color(0.2f, 1.0f, 1.0f);
            GUI.Label(iconModules, EditorGUIUtility.IconContent("d_Collab@2x"));
            GUI.color = Color.white;
#endif
            GUILayout.Space(-1);

            if (GUILayout.Button("Review", styledToolbar))
            {
                Application.OpenURL("https://assetstore.unity.com/packages/tools/utilities/the-visual-engine-286827#reviews");
            }

#if UNITY_2020_3_OR_NEWER
            var rectReview = GUILayoutUtility.GetLastRect();
            var iconReview = new Rect(rectReview.xMax - 26, rectReview.y, 20, 20);
            GUI.color = new Color(1.0f, 0.9f, 0.5f);
            GUI.Label(iconReview, EditorGUIUtility.IconContent("d_Favorite"));
            GUI.color = Color.white;
#endif
            GUILayout.Space(-1);
            GUILayout.Space(rightSpace);
            GUILayout.EndHorizontal();
        }

        // Property Utils
        public static bool GetPropertyVisibility(Material material, string internalName)
        {
            bool valid = true;

            var shaderName = material.shader.name;
            var projectPipeline = TVEUtils.GetProjectPipeline();

            if (internalName == "unity_Lightmaps")
                valid = false;

            if (internalName == "unity_LightmapsInd")
                valid = false;

            if (internalName == "unity_ShadowMasks")
                valid = false;

            if (internalName.Contains("_Banner"))
                valid = false;

            //if (internalName == "_SpecColor")
            //    valid = false;

            if (internalName.Contains("_AI_Clip"))
                valid = false;

            if (material.HasProperty("_RenderMode"))
            {
                if (material.GetInt("_RenderMode") == 0 && internalName == "_RenderZWrite")
                    valid = false;
            }

            bool hasRenderNormals = false;

            if (material.HasProperty("_render_normal"))
            {
                hasRenderNormals = true;
            }

            if (!hasRenderNormals)
            {
                if (internalName == "_RenderNormal")
                    valid = false;
            }

            //if (!shaderName.Contains("Vertex Lit"))
            //{
            //    if (internalName == "_RenderDirect")
            //        valid = false;
            //    if (internalName == "_RenderShadow")
            //        valid = false;
            //    if (internalName == "_RenderAmbient")
            //        valid = false;
            //}

            if (material.HasProperty("_RenderCull"))
            {
                if (material.GetInt("_RenderCull") == 2 && internalName == "_RenderNormal")
                    valid = false;
            }

            //if (!material.HasProperty("_AlphaFeatherValue"))
            //{
            //    if (internalName == "_RenderCoverage")
            //        valid = false;
            //}

            if (projectPipeline != "High Definition")
            {
                if (internalName == "_RenderDecals")
                    valid = false;
                if (internalName == "_RenderSSR")
                    valid = false;
            }

            if (projectPipeline == "Standard")
            {
                if (internalName == "_RenderMotion")
                    valid = false;
            }

            if (projectPipeline != "Universal")
            {
                if (internalName == "_RenderShadow")
                    valid = false;
            }

            if (material.HasProperty("_ObjectModelMode"))
            {
                if (material.GetInt("_ObjectModelMode") == 0)
                {
                    if (internalName == "_MotionBaseMaskMode")
                        valid = false;
                    if (internalName == "_MotionSmallMaskMode")
                        valid = false;
                    if (internalName == "_MotionTinyMaskMode")
                        valid = false;
                }
            }

            //bool showMainMaskMessage = false;

            //if (material.HasProperty("_MainMaskMinValue"))
            //{
            //    showMainMaskMessage = true;
            //}

            //if (!showMainMaskMessage)
            //{
            //    if (internalName == "_MessageMainMask")
            //        valid = false;
            //}

            if (material.HasProperty("_MainColorMode"))
            {
                if (material.GetInt("_MainColorMode") == 0)
                {
                    if (internalName == "_MainColorTwo")
                        valid = false;
                }
            }

            if (material.HasProperty("_SecondColorMode"))
            {
                if (material.GetInt("_SecondColorMode") == 0)
                {
                    if (internalName == "_SecondColorTwo")
                        valid = false;
                }
            }

            if (material.HasProperty("_ThirdColorMode"))
            {
                if (material.GetInt("_ThirdColorMode") == 0)
                {
                    if (internalName == "_ThirdColorTwo")
                        valid = false;
                }
            }

            if (material.HasProperty("_ImpostorColorMode"))
            {
                if (material.GetInt("_ImpostorColorMode") == 0)
                {
                    if (internalName == "_ImpostorColorTwo")
                        valid = false;
                }
            }

            //bool showSecondMaskMessage = false;

            //if (material.HasProperty("_SecondMaskMinValue"))
            //{
            //    showSecondMaskMessage = true;
            //}

            //if (!showSecondMaskMessage)
            //{
            //    if (internalName == "_MessageSecondMask")
            //        valid = false;
            //}

            if (material.HasProperty("_SubsurfaceIntensityValue"))
            {
                if (projectPipeline != "High Definition" || shaderName.Contains("Standard"))
                {
                    if (internalName == "_SubsurfaceDiffusion")
                        valid = false;
                    if (internalName == "_SubsurfaceSpace")
                        valid = false;
                    if (internalName == "_SubsurfaceHDRPInfo")
                        valid = false;
                }

                // Standard Render Pipeline
                if (internalName == "_Translucency")
                    valid = false;
                if (internalName == "_TransNormalDistortion")
                    valid = false;
                if (internalName == "_TransScattering")
                    valid = false;
                if (internalName == "_TransDirect")
                    valid = false;
                if (internalName == "_TransAmbient")
                    valid = false;
                if (internalName == "_TransShadow")
                    valid = false;

                // Universal Render Pipeline
                if (internalName == "_TransStrength")
                    valid = false;
                if (internalName == "_TransNormal")
                    valid = false;

                if (projectPipeline == "High Definition")
                {
                    if (internalName == "_SubsurfaceColor")
                        valid = false;
                    if (internalName == "_SubsurfaceScatteringValue")
                        valid = false;
                    if (internalName == "_SubsurfaceAngleValue")
                        valid = false;
                    if (internalName == "_SubsurfaceNormalValue")
                        valid = false;
                    if (internalName == "_SubsurfaceDirectValue")
                        valid = false;
                    if (internalName == "_SubsurfaceAmbientValue")
                        valid = false;
                    if (internalName == "_SubsurfaceShadowValue")
                        valid = false;
                }
            }

            if (internalName == "_OcclusionBakeInfo")
            {
                if (material.HasProperty("_OcclusionBakeInfo") && material.GetInt("_OcclusionBakeInfo") == 0)
                {
                    valid = false;
                }
            }

            if (internalName == "_GradientBakeInfo")
            {
                if (material.HasProperty("_GradientBakeInfo") && material.GetInt("_GradientBakeInfo") == 0)
                {
                    valid = false;
                }
            }

            if (internalName == "_TintingBakeInfo")
            {
                if (material.HasProperty("_TintingBakeInfo") && material.GetInt("_TintingBakeInfo") == 0)
                {
                    valid = false;
                }
            }

            if (internalName == "_DrynessBakeInfo")
            {
                if (material.HasProperty("_DrynessBakeInfo") && material.GetInt("_DrynessBakeInfo") == 0)
                {
                    valid = false;
                }
            }

            if (internalName == "_OverlayBakeInfo")
            {
                if (material.HasProperty("_OverlayBakeInfo") && material.GetInt("_OverlayBakeInfo") == 0)
                {
                    valid = false;
                }
            }

            if (internalName == "_WetnessBakeInfo")
            {
                if (material.HasProperty("_WetnessBakeInfo") && material.GetInt("_WetnessBakeInfo") == 0)
                {
                    valid = false;
                }
            }

            if (internalName == "_CutoutBakeInfo")
            {
                if (material.HasProperty("_CutoutBakeInfo") && material.GetInt("_CutoutBakeInfo") == 0)
                {
                    valid = false;
                }
            }

            if (material.HasProperty("_IsImpostorShader"))
            {
                if (internalName == "_Shader")
                {
                    if (material.HasProperty("_ImpostorMaskMode") && (material.GetInt("_ImpostorMaskMode") == 0 || material.GetInt("_ImpostorMaskMode") == 2))
                    {
                        valid = false;
                    }
                }

                if (internalName == "_Packed")
                {
                    if (material.HasProperty("_ImpostorMaskMode") && (material.GetInt("_ImpostorMaskMode") == 0 || material.GetInt("_ImpostorMaskMode") == 1 || material.GetInt("_ImpostorMaskMode") == 3))
                    {
                        valid = false;
                    }
                }

                if (internalName == "_Vertex")
                {
                    if (material.HasProperty("_ImpostorMaskMode") && (material.GetInt("_ImpostorMaskMode") == 0 || material.GetInt("_ImpostorMaskMode") == 2 || material.GetInt("_ImpostorMaskMode") == 3))
                    {
                        valid = false;
                    }
                }

                if (internalName == "_ImpostorMaskOffInfo")
                {
                    if (material.HasProperty("_ImpostorMaskMode") && material.GetInt("_ImpostorMaskMode") != 0)
                    {
                        valid = false;
                    }
                }

                if (internalName == "_ImpostorMaskDefaultInfo")
                {
                    if (material.HasProperty("_ImpostorMaskMode") && material.GetInt("_ImpostorMaskMode") != 1)
                    {
                        valid = false;
                    }
                }

                if (internalName == "_ImpostorMaskPackedInfo")
                {
                    if (material.HasProperty("_ImpostorMaskMode") && material.GetInt("_ImpostorMaskMode") != 2)
                    {
                        valid = false;
                    }
                }

                if (internalName == "_ImpostorMaskShadingInfo")
                {
                    if (material.HasProperty("_ImpostorMaskMode") && material.GetInt("_ImpostorMaskMode") != 3)
                    {
                        valid = false;
                    }
                }
            }

            if (material.HasProperty("_MotionWindMode"))
            {
                if (material.GetInt("_MotionWindMode") == 0)
                {
                    if (internalName == "_MotionNoiseTexRT")
                        valid = false;
                    if (internalName == "_MotionBaseNoiseValue")
                        valid = false;
                    if (internalName == "_MotionBaseTillingValue")
                        valid = false;
                    if (internalName == "_MotionBaseSpeedValue")
                        valid = false;
                    if (internalName == "_MotionSmallNoiseValue")
                        valid = false;
                    if (internalName == "_MotionSmallTillingValue")
                        valid = false;
                    if (internalName == "_MotionSmallSpeedValue")
                        valid = false;
                    if (internalName == "_MotionTillingValue")
                        valid = false;
                    if (internalName == "_MotionWindOptimizedInfo")
                        valid = false;
                    if (internalName == "_MotionWindAdvancedInfo")
                        valid = false;
                }
                else if (material.GetInt("_MotionWindMode") == 1)
                {
                    if (internalName == "_MotionBaseNoiseValue")
                        valid = false;
                    if (internalName == "_MotionBaseTillingValue")
                        valid = false;
                    if (internalName == "_MotionBaseSpeedValue")
                        valid = false;
                    if (internalName == "_MotionSmallNoiseValue")
                        valid = false;
                    if (internalName == "_MotionSmallTillingValue")
                        valid = false;
                    if (internalName == "_MotionSmallSpeedValue")
                        valid = false;
                    if (internalName == "_MotionWindOffInfo")
                        valid = false;
                    if (internalName == "_MotionWindAdvancedInfo")
                        valid = false;
                }
                else if (material.GetInt("_MotionWindMode") == 2)
                {
                    if (internalName == "_MotionNoiseTexRT")
                        valid = false;
                    if (internalName == "_MotionTillingValue")
                        valid = false;
                    if (internalName == "_MotionWindOffInfo")
                        valid = false;
                    if (internalName == "_MotionWindOptimizedInfo")
                        valid = false;
                }
            }

            return valid;
        }

        public static string GetPropertyDisplay(Material material, string internalName, string displayName)
        {
            //#if !THE_VISUAL_ENGINE_IMPOSTORS
            //            if (internalName.Contains("BakeMode"))
            //            {
            //                GUI.color = new Color(0.7f, 0.7f, 0.7f, 1f);
            //            }
            //            else
            //            {
            //                GUI.color = Color.white;
            //            }
            //#endif

            if (internalName == "_AI_Parallax")
            {
                GUILayout.Space(10);
            }

            //if (internalName == "_AI_Clip")
            //{
            //    displayName = "Impostor Alpha Treshold";
            //}

            if (internalName == "_Albedo")
            {
                displayName = "Impostor Albedo";
            }

            if (internalName == "_Normals")
            {
                displayName = "Impostor Normal";
            }

            if (material.HasProperty("_MainColorMode"))
            {
                if (material.GetInt("_MainColorMode") == 1 && internalName == "_MainColor")
                {
                    displayName = displayName + "A";
                }
            }

            if (material.HasProperty("_SecondColorMode"))
            {
                if (material.GetInt("_SecondColorMode") == 1 && internalName == "_SecondColor")
                {
                    displayName = displayName + "A";
                }
            }

            if (material.HasProperty("_ThirdColorMode"))
            {
                if (material.GetInt("_ThirdColorMode") == 1 && internalName == "_ThirdColor")
                {
                    displayName = displayName + "A";
                }
            }

            if (material.HasProperty("_ImpostorColorMode"))
            {
                if (material.GetInt("_ImpostorColorMode") == 1 && internalName == "_ImpostorColor")
                {
                    displayName = displayName + "A";
                }
            }

            //if (EditorGUIUtility.currentViewWidth > 550)
            //{
            //    if (internalName == "_MainMetallicValue")
            //    {
            //        if (shaderName.Contains("Standard Lit") || shaderName.Contains("Subsurface Lit"))
            //        {
            //            displayName = displayName + " (Mask Red)";
            //        }
            //    }

            //    if (internalName == "_MainOcclusionValue")
            //    {
            //        displayName = displayName + " (Mask Green)";
            //    }

            //    if (internalName == "_MainSmoothnessValue")
            //    {
            //        if (shaderName.Contains("Standard Lit") || shaderName.Contains("Subsurface Lit"))
            //        {
            //            displayName = displayName + " (Mask Alpha)";
            //        }
            //    }

            //    if (internalName == "_MainMaskRemap")
            //    {
            //        displayName = displayName + " (Mask Blue)";
            //    }

            //    if (internalName == "_SecondMetallicValue")
            //    {
            //        if (shaderName.Contains("Standard Lit") || shaderName.Contains("Subsurface Lit"))
            //        {
            //            displayName = displayName + " (Mask Red)";
            //        }
            //    }

            //    if (internalName == "_SecondOcclusionValue")
            //    {
            //        displayName = displayName + " (Mask Green)";
            //    }

            //    if (internalName == "_SecondSmoothnessValue")
            //    {
            //        if (shaderName.Contains("Standard Lit") || shaderName.Contains("Subsurface Lit"))
            //        {
            //            displayName = displayName + " (Mask Alpha)";
            //        }
            //    }

            //    if (internalName == "_SecondMaskRemap")
            //    {
            //        displayName = displayName + " (Mask Blue)";
            //    }

            //    if (internalName == "_DetailMeshMode" || internalName == "_DetailMeshRemap")
            //    {
            //        if (material.HasProperty("_DetailMeshMode"))
            //        {
            //            if (material.GetInt("_DetailMeshMode") == 0)
            //            {
            //                displayName = displayName + " (Vertex Blue)";
            //            }
            //            else if (material.GetInt("_DetailMeshMode") == 1)
            //            {
            //                displayName = displayName + " (World Normals)";
            //            }
            //        }
            //    }

            //    if (internalName == "_DetailMaskMode" || internalName == "_DetailMaskRemap")
            //    {
            //        displayName = displayName + " (Mask Blue)";
            //    }

            //    if (internalName == "_VertexOcclusionRemap")
            //    {
            //        displayName = displayName + " (Vertex Green)";
            //    }

            //    if (internalName == "_GradientMaskRemap")
            //    {
            //        displayName = displayName + " (Vertex Alpha)";
            //    }
            //}

            return displayName;
        }

        public static void GetActiveDisplay(Material material, string internalName, string categoryName, string intensityName, string color, GUIStyle subTitleStyle)
        {
            if (internalName != categoryName)
            {
                return;
            }

            bool active = false;

            if (material.HasProperty(intensityName))
            {
                if (material.GetFloat(intensityName) > 0)
                {
                    active = true;
                }
            }

            if (active)
            {
                var lastRectYOffset = 32;

                if (material.GetInt(categoryName) == 0)
                {
                    lastRectYOffset = 22;
                }

                TVEUtils.SetActiveDisplay(lastRectYOffset, color, subTitleStyle);
            }
        }

        public static void GetActiveDisplay(Material material, string internalName, string categoryName, string[] intensityNames, string color, GUIStyle subTitleStyle)
        {
            if (internalName != categoryName)
            {
                return;
            }

            bool active = false;

            for (int i = 0; i < intensityNames.Length; i++)
            {
                var intensityName = intensityNames[i];

                if (material.HasProperty(intensityName))
                {
                    if (material.GetFloat(intensityName) > 0)
                    {
                        active = true;
                        break;
                    }
                }
            }

            if (active)
            {
                var lastRectYOffset = 32;

                if (material.GetInt(categoryName) == 0)
                {
                    lastRectYOffset = 22;
                }

                TVEUtils.SetActiveDisplay(lastRectYOffset, color, subTitleStyle);
            }
        }

        public static void SetActiveDisplay(int offset, string color, GUIStyle subTitleStyle)
        {
            var lastRect = EditorGUILayout.GetControlRect(GUILayout.Height(-2));

            var subRect = new Rect(lastRect.xMax - 50, lastRect.y - offset, 50, 20);

            string subtitle;

            if (EditorGUIUtility.isProSkin)
            {
                subtitle = "<size=9><color=#" + color + ">●</color></size>";
            }
            else
            {
                subtitle = "<size=9>●</size>";
            }

            GUI.Label(subRect, "<size=10>" + subtitle + "</size>", subTitleStyle);
        }

#endif

        // Asset Utils
        public static TVEProjectData GetProjectData()
        {
            var projectData = new TVEProjectData();

            string pipeline = "Standard";

            if (GraphicsSettings.defaultRenderPipeline != null)
            {
                if (GraphicsSettings.defaultRenderPipeline.GetType().ToString().Contains("Universal"))
                {
                    pipeline = "Universal";
                }

                if (GraphicsSettings.defaultRenderPipeline.GetType().ToString().Contains("HD"))
                {
                    pipeline = "High Definition";
                }
            }

            if (QualitySettings.renderPipeline != null)
            {
                if (QualitySettings.renderPipeline.GetType().ToString().Contains("Universal"))
                {
                    pipeline = "Universal";
                }

                if (QualitySettings.renderPipeline.GetType().ToString().Contains("HD"))
                {
                    pipeline = "High Definition";
                }
            }

            projectData.pipeline = pipeline;

            if (pipeline != "Standard")
            {
                var version = Application.unityVersion;

                var versionSplit = version.Split(".");

                var version0 = int.Parse(versionSplit[0]);
                var version1 = int.Parse(versionSplit[1]);
                var version2Split = versionSplit[2].Split("f");
                var version2 = int.Parse(version2Split[0]);

                if (version0 == 2021)
                {
                    var minimumSplit = minimumVersionFor2021.Split(".");
                    var minimum2 = int.Parse(minimumSplit[2]);

                    if (version1 != 3)
                    {
                        projectData.isSupported = false;
                    }
                    else
                    {
                        if (version2 < minimum2)
                        {
                            projectData.isSupported = false;
                        }
                    }

                    projectData.version = "2021.3+";
                }

                if (version0 == 2022)
                {
                    var minimumSplit = minimumVersionFor2022.Split(".");
                    var minimum2 = int.Parse(minimumSplit[2]);

                    if (version1 != 3)
                    {
                        projectData.isSupported = false;
                    }
                    else
                    {
                        if (version2 < minimum2)
                        {
                            projectData.isSupported = false;
                        }
                    }

                    projectData.version = "2022.3+";
                }

                if (version0 == 6000)
                {
                    var minimumSplit = minimumVersionFor6000.Split(".");
                    var minimum2 = int.Parse(minimumSplit[2]);

                    if (version2 < minimum2)
                    {
                        projectData.isSupported = false;
                    }

                    projectData.version = "6000.0+";
                }

                var minimum = minimumVersionFor2021;

                if (version0 == 2022)
                {
                    minimum = minimumVersionFor2022;
                }

                if (version0 == 6000)
                {
                    minimum = minimumVersionFor6000;
                }

                projectData.minimum = minimum;
            }

            return projectData;
        }

        public static string GetProjectPipeline()
        {
            string pipeline = "Standard";

            if (GraphicsSettings.defaultRenderPipeline != null)
            {
                if (GraphicsSettings.defaultRenderPipeline.GetType().ToString().Contains("Universal"))
                {
                    pipeline = "Universal";
                }

                if (GraphicsSettings.defaultRenderPipeline.GetType().ToString().Contains("HD"))
                {
                    pipeline = "High Definition";
                }
            }

            if (QualitySettings.renderPipeline != null)
            {
                if (QualitySettings.renderPipeline.GetType().ToString().Contains("Universal"))
                {
                    pipeline = "Universal";
                }

                if (QualitySettings.renderPipeline.GetType().ToString().Contains("HD"))
                {
                    pipeline = "High Definition";
                }
            }

            return pipeline;
        }

#if UNITY_EDITOR

        public static string[] FindAssets(string filter, bool sort)
        {
            var assetPaths = AssetDatabase.FindAssets("glob:\"" + filter + "\"");

            if (sort)
            {
                assetPaths = assetPaths.OrderBy(f => new FileInfo(f).Name).ToArray();
            }

            for (int i = 0; i < assetPaths.Length; i++)
            {
                assetPaths[i] = AssetDatabase.GUIDToAssetPath(assetPaths[i]);
            }

            return assetPaths;
        }

        public static string FindAsset(string filter)
        {
            var assetPath = "";

            var assetGUIDs = AssetDatabase.FindAssets("glob:\"" + filter + "\"");

            if (assetGUIDs != null && assetGUIDs.Length > 0)
            {
                assetPath = AssetDatabase.GUIDToAssetPath(assetGUIDs[0]);
            }

            return assetPath;
        }

        public static string GetAssetFolder()
        {
            var folder = TVEUtils.FindAsset("The Visual Engine.pdf");
            folder = folder.Replace("/The Visual Engine.pdf", "");

            return folder;
        }

        public static string GetUserFolder()
        {
            var folder = TVEUtils.FindAsset("User.pdf");
            folder = folder.Replace("/User.pdf", "");
            folder += "/The Visual Engine";

            return folder;
        }

        public static TVEPathData GetPathData(string assetPath)
        {
            var pathData = new TVEPathData();

            pathData.folder = Path.GetDirectoryName(assetPath);
            pathData.extention = Path.GetExtension(assetPath);

            assetPath = Path.GetFileNameWithoutExtension(assetPath);
            assetPath = assetPath.Replace("(", "");
            assetPath = assetPath.Replace(")", "");

            var splitLine = assetPath.Split(char.Parse(" "));
            var splitCount = splitLine.Length;

            pathData.type = splitLine[splitCount - 1];
            pathData.suffix = splitLine[splitCount - 2];

            assetPath = assetPath.Replace(pathData.type, "");
            assetPath = assetPath.Replace(pathData.suffix, "");

            // Old Assets might not have an ID
            if (splitCount > 3)
            {
                pathData.GUID = splitLine[splitCount - 3];
                assetPath = assetPath.Replace(pathData.GUID, "");
            }

            assetPath = assetPath.TrimEnd();

            pathData.name = assetPath;

            return pathData;
        }

        public static void SetLabel(string path)
        {
            var asset = AssetDatabase.LoadAssetAtPath<UnityEngine.Object>(path);

            AssetDatabase.SetLabels(asset, new string[] { "The Visual Engine" });
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
        }

        public static bool HasLabel(string path)
        {
            bool valid = false;

            var asset = AssetDatabase.LoadAssetAtPath<UnityEngine.Object>(path);
            var labelsArr = AssetDatabase.GetLabels(asset);
            var labeldList = new List<string>();

            labeldList.AddRange(labelsArr);

            if (labeldList.Contains("The Visual Engine"))
            {
                valid = true;
            }

            labeldList.Clear();

            return valid;
        }

        public static bool HasLabel(string path, string check)
        {
            bool valid = false;

            var asset = AssetDatabase.LoadAssetAtPath<UnityEngine.Object>(path);
            var labelsArr = AssetDatabase.GetLabels(asset);
            var labeldList = new List<string>();

            labeldList.AddRange(labelsArr);

            if (labeldList.Contains(check))
            {
                valid = true;
            }

            labeldList.Clear();

            return valid;
        }

        public static void SetDefineSymbols(string symbol)
        {
#if UNITY_2023_1_OR_NEWER
            BuildTarget buildTarget = EditorUserBuildSettings.activeBuildTarget;
            BuildTargetGroup targetGroup = BuildPipeline.GetBuildTargetGroup(buildTarget);
            var namedBuildTarget = UnityEditor.Build.NamedBuildTarget.FromBuildTargetGroup(targetGroup);
            var defineSymbols = PlayerSettings.GetScriptingDefineSymbols(namedBuildTarget);
#else
            var defineSymbols = PlayerSettings.GetScriptingDefineSymbolsForGroup(EditorUserBuildSettings.selectedBuildTargetGroup);
#endif

            if (!defineSymbols.Contains(symbol))
            {
                defineSymbols += ";" + symbol + ";";

#if UNITY_2023_1_OR_NEWER
                PlayerSettings.SetScriptingDefineSymbols(namedBuildTarget, defineSymbols);
#else
                PlayerSettings.SetScriptingDefineSymbolsForGroup(EditorUserBuildSettings.selectedBuildTargetGroup, defineSymbols);
#endif
            }
        }

        public static void SetScriptExecutionOrder()
        {
            MonoScript[] scripts = (MonoScript[])Resources.FindObjectsOfTypeAll(typeof(MonoScript));

            foreach (MonoScript script in scripts)
            {
                if (script.GetClass() == typeof(TVEManager))
                {
                    MonoImporter.SetExecutionOrder(script, -122);
                }

                if (script.GetClass() == typeof(SceneSwitch))
                {
                    MonoImporter.SetExecutionOrder(script, -123);
                }
            }
        }

        public static void SetVertexCompression()
        {
            if (EditorSettings.serializationMode == UnityEditor.SerializationMode.ForceText)
            {
                var projectSettingsPath = Path.Combine(Path.GetDirectoryName(Application.dataPath), "ProjectSettings", "ProjectSettings.asset");

                var requiresCompressionUpgrade = false;
                var vertexLayers = new List<int>();
                var settingsLines = new List<string>();

                if (File.Exists(projectSettingsPath))
                {
                    StreamReader reader = new StreamReader(projectSettingsPath);

                    int bitmask = 0;


                    while (!reader.EndOfStream)
                    {
                        settingsLines.Add(reader.ReadLine());
                    }

                    reader.Close();

                    for (int i = 0; i < settingsLines.Count; i++)
                    {
                        if (settingsLines[i].Contains("VertexChannelCompressionMask"))
                        {
                            string line = settingsLines[i].Replace("  VertexChannelCompressionMask: ", "");
                            bitmask = int.Parse(line, CultureInfo.InvariantCulture);
                        }
                    }

                    for (int i = 0; i < 9; i++)
                    {
                        if (((1 << i) & bitmask) != 0)
                        {
                            vertexLayers.Add(1);
                        }
                        else
                        {
                            vertexLayers.Add(0);
                        }
                    }

                    if (vertexLayers[4] == 1 || vertexLayers[7] == 1)
                    {
                        requiresCompressionUpgrade = true;
                    }
                }

                if (requiresCompressionUpgrade)
                {
                    // Disable layers
                    vertexLayers[4] = 0;
                    vertexLayers[7] = 0;

                    int layerMask = 0;

                    for (int i = 0; i < 9; i++)
                    {
                        if (vertexLayers[i] == 1)
                        {
                            layerMask = layerMask + (int)Mathf.Pow(2, i);
                        }
                    }

                    StreamWriter writer = new StreamWriter(projectSettingsPath);

                    for (int i = 0; i < settingsLines.Count; i++)
                    {
                        if (settingsLines[i].Contains("VertexChannelCompressionMask"))
                        {
                            settingsLines[i] = "  VertexChannelCompressionMask: " + layerMask;
                        }

                        if (settingsLines[i].Contains("StripUnusedMeshComponents"))
                        {
                            settingsLines[i] = "  StripUnusedMeshComponents: 1";
                        }

                        writer.WriteLine(settingsLines[i]);
                    }

                    writer.Close();
                }
            }
        }

#endif
    }

    public class TVEGlobals
    {
        public static string searchMaterial = "";
        public static string searchManager = "";

        public static GameObject[] lastSelection;
    }

    public enum TVEBool
    {
        Off = 0,
        On = 1,
    }

    public enum TVEPropertyType
    {
        Texture = 0,
        Vector = 1,
        Value = 2,
    }

    public enum TVEElementsVisibility
    {
        AlwaysHidden = 0,
        AlwaysVisible = 10,
        HiddenAtRuntime = 20,
    }

    public enum TVEElementVisibility
    {
        UseGlobalSettings = -1,
        AlwaysHidden = 0,
        AlwaysVisible = 10,
        HiddenAtRuntime = 20,
    }

    public enum TVEElementsSorting
    {
        SortInEditMode = 0,
        SortAtRuntime = 10,
    }

    public enum TVETerrainTexture
    {
        Auto = -1,
        HeightTexture = 10,
        NormalTexture = 20,
        HolesTexture = 30,
    }

    public enum TVETerrainRefresh
    {
        Always = 0,
        Selection = 10,
    }

    public enum TVEUVMode
    {
        [InspectorName("Tilling And Offset")]
        Tilling = 0,
        [InspectorName("Scale And Offset")]
        Scale = 1,
    }

    public enum TVETextureRange
    {
        LDR = 0,
        HDR = 10,
    }

    public enum TVETextureSize
    {
        _64 = 64,
        _128 = 128,
        _256 = 256,
        _512 = 512,
        _1024 = 1024,
        _2048 = 2048,
        _4096 = 4096,
    }

#if UNITY_EDITOR
    public enum TVEPrefabMode
    {
        Undefined = -1,
        Converted = 10,
        Supported = 20,
        Backup = 25,
        Unsupported = 30,
        ConvertedMissingBackup = 40,
    }

    [System.Serializable]
    public class TVEPathData
    {
        public string folder = "";
        public string name = "";
        public string GUID = "";
        public string suffix = "";
        public string type = "";
        public string extention = "";

        public TVEPathData()
        {

        }
    }

    [System.Serializable]
    public class TVEPrefabData
    {
        public GameObject prefabObject;
        public GameObject PrefabInstanceInScene;
        public TVEPrefabMode status;
        public string attributes = "";
        public bool isShared = false;
        public bool isNested = false;
        public bool isVariant = false;
        public bool hasOverrides = false;
        //public bool hasConvertedChildren = false;

        public TVEPrefabData()
        {

        }
    }

    [System.Serializable]
    public class TVETransformData
    {
        public Vector3 position = Vector3.zero;
        public Quaternion rotation = Quaternion.identity;
        public Vector3 scale = Vector3.one;

        public TVETransformData()
        {

        }
    }

    [System.Serializable]
    public class TVEGameObjectData
    {
        public GameObject parentPrefab;
        public GameObject gameObject;
        public MeshFilter meshFilter;
        public Mesh originalMesh;
        public Mesh instanceMesh;
        public List<MeshCollider> meshColliders = new List<MeshCollider>();
        public List<Mesh> originalColliders = new List<Mesh>();
        public List<Mesh> instanceColliders = new List<Mesh>();
        public MeshRenderer meshRenderer;
        public Material[] originalMaterials;
        public Material[] instanceMaterials;

        public TVEGameObjectData()
        {

        }
    }

    [System.Serializable]
    public class TVECollectionData
    {
        public string status = "";
        public string online = "";
        public string message = "";
        public float decode = 0.0f;

        public TVECollectionData()
        {

        }
    }

    [System.Serializable]
    public class TVEPropertyData
    {
        public enum TVEPropertyType
        {
            Value,
            Range,
            Vector,
            Color,
            Remap,
            Enum,
            Toggle,
            Texture,
            Space,
            Category,
            Message,
        }

        public enum TVEPropertySetter
        {
            Value,
            Vector,
            Texture,
            Display,
        }

        public TVEPropertyType type;
        public TVEPropertySetter setter;
        public string prop = "";
        public string name = "";
        public string tag = "";
        public float value;
        public float min;
        public float max;
        public bool snap;
        public Vector4 vector;
        public Texture texture;
        public string file;
        public string options;
        public bool hdr;
        public bool space;
        public int spaceTop;
        public int spaceDown;
        public string category;
        public string message;
        public MessageType messageType = MessageType.Info;

        public int isVisible = 0;
        public int isLocked = 0;
        public bool isMixed = false;

        public TVEPropertyData(string prop)
        {
            type = TVEPropertyType.Space;
            setter = TVEPropertySetter.Display;
            this.prop = prop;
        }

        public TVEPropertyData(string prop, string category)
        {
            type = TVEPropertyType.Category;
            setter = TVEPropertySetter.Display;
            this.value = 1;
            this.prop = prop;
            this.category = category;
        }

        public TVEPropertyData(string prop, string message, int spaceTop, int spaceDown, MessageType messageType)
        {
            type = TVEPropertyType.Message;
            setter = TVEPropertySetter.Display;
            this.prop = prop;
            this.message = message;
            this.spaceTop = spaceTop;
            this.spaceDown = spaceDown;
            this.messageType = messageType;
        }

        public TVEPropertyData(string prop, string name, float value, bool snap, bool space)
        {
            type = TVEPropertyType.Value;
            setter = TVEPropertySetter.Value;
            this.prop = prop;
            this.name = name;
            this.value = value;
            this.snap = snap;
            this.space = space;
        }

        public TVEPropertyData(string prop, string name, float value, int min, int max, bool snap, bool space)
        {
            type = TVEPropertyType.Range;
            setter = TVEPropertySetter.Value;
            this.prop = prop;
            this.name = name;
            this.value = value;
            this.min = min;
            this.max = max;
            this.snap = snap;
            this.space = space;
        }

        public TVEPropertyData(string prop, string name, float value, string options, bool space)
        {
            type = TVEPropertyType.Enum;
            setter = TVEPropertySetter.Value;
            this.prop = prop;
            this.name = name;
            this.value = value;
            this.options = options;
            this.space = space;
        }

        public TVEPropertyData(string prop, string name, float value, string file, string options, bool space)
        {
            type = TVEPropertyType.Enum;
            setter = TVEPropertySetter.Value;
            this.prop = prop;
            this.name = name;
            this.value = value;
            this.file = file;
            this.options = options;
            this.space = space;
        }

        public TVEPropertyData(string prop, string name, float value, bool space)
        {
            type = TVEPropertyType.Toggle;
            setter = TVEPropertySetter.Value;
            this.prop = prop;
            this.name = name;
            this.value = value;
            this.space = space;
        }

        public TVEPropertyData(string prop, string name, Vector4 vector, bool space)
        {
            type = TVEPropertyType.Vector;
            setter = TVEPropertySetter.Vector;
            this.prop = prop;
            this.name = name;
            this.vector = vector;
            this.space = space;
        }

        public TVEPropertyData(string prop, string name, Vector4 vector, float min, float max, bool space)
        {
            type = TVEPropertyType.Remap;
            setter = TVEPropertySetter.Vector;
            this.prop = prop;
            this.name = name;
            this.vector = vector;
            this.min = min;
            this.max = max;
            this.space = space;
        }

        public TVEPropertyData(string prop, string name, Vector4 vector, bool hdr, bool space)
        {
            type = TVEPropertyType.Color;
            setter = TVEPropertySetter.Vector;
            this.prop = prop;
            this.name = name;
            this.vector = vector;
            this.hdr = hdr;
            this.space = space;
        }
        public TVEPropertyData(string prop, string name, Texture texture, bool space)
        {
            type = TVEPropertyType.Texture;
            setter = TVEPropertySetter.Texture;
            this.prop = prop;
            this.name = name;
            this.texture = texture;
            this.space = space;
        }
    }

    [System.Serializable]
    public class TVEPackerData
    {
        public Material blitMaterial;
        public Mesh blitMesh;
        public int blitPass = 0;
        public Texture[] maskTextures;
        public int[] maskChannels;
        public int[] maskCoords;
        //int[] maskLayers;
        public int[] maskActions0;
        public int[] maskActions1;
        public int[] maskActions2;
        public bool saveAsSRGB = true;
        public bool saveAsDefault = true;
        public int transformSpace;

        public TVEPackerData()
        {

        }
    }

    public class TVEModelSettings
    {
        public bool requiresProcessing = false;
        public string meshPath;
        public bool isReadable = false;
        public bool keepQuads = false;
        public ModelImporterMeshCompression meshCompression;

        public TVEModelSettings()
        {

        }
    }

    public class TVETextureSettings
    {
        public string texturePath;
        public TextureImporterSettings textureSettings = new TextureImporterSettings();
        public TextureImporterCompression textureCompression;
        public int maxTextureSize;

        public TVETextureSettings()
        {

        }
    }

    public class TVEShaderSettings
    {
        public string shaderPath;
        public string renderEngine = "Unity Default Renderer";
        public string shaderModel = "Shader Model 4.5";

        public TVEShaderSettings()
        {

        }
    }
#endif

    [System.Serializable]
    public class TVEProjectData
    {
        public string pipeline = "";
        public string version = "";
        public string minimum = "";
        public bool isSupported = true;

        public TVEProjectData()
        {

        }
    }

    [System.Serializable]
    public class TVEGlobalCoatData
    {
        [Tooltip("Controls the global Layer intensity.")]
        [Range(0.0f, 1.0f)]
        public float layerIntensity = 1.0f;
        [Tooltip("Controls the global Detail intensity.")]
        [Range(0.0f, 1.0f)]
        public float detailIntensity = 1.0f;
        [Tooltip("Controls the global Terrain blending intensity.")]
        [Range(0.0f, 1.0f)]
        public float terrainIntensity = 1.0f;
        public TVEGlobalCoatData()
        {

        }
    }

    [System.Serializable]
    public class TVEGlobalPaintData
    {
        [Tooltip("Controls the global tinting influence.")]
        [Range(0.0f, 1.0f)]
        public float tintingIntensity = 0.0f;
        [Tooltip("Controls the global tinting color.")]
        [ColorUsage(false, true)]
        public Color tintingColor = new Color(0.5f, 0.5f, 0.5f, 0);

        public TVEGlobalPaintData()
        {

        }
    }

    [System.Serializable]
    public class TVEGlobalGlowData
    {
        [Tooltip("Controls the global emissive color.")]
        [ColorUsage(false, true)]
        public Color emissiveColor = new Color(1f, 1f, 1f, 0);
        [Tooltip("Controls the global subsurface intensity.")]
        [Range(0.0f, 1.0f)]
        public float subsurfaceIntensity = 1.0f;

        public TVEGlobalGlowData()
        {

        }
    }

    [System.Serializable]
    public class TVEGlobalAtmoData
    {
        [Tooltip("Controls the global dryness intensity.")]
        [Range(0.0f, 1.0f)]
        public float drynessIntensity = 0.0f;
        [Tooltip("Controls the global overlay intensity.")]
        [Range(0.0f, 1.0f)]
        public float overlayIntensity = 0.0f;
        [Tooltip("Controls the global Wetness intensity.")]
        [Range(0.0f, 1.0f)]
        public float wetnessIntensity = 0.0f;
        [Tooltip("Controls the global Rain intensity.")]
        [Range(0.0f, 1.0f)]
        public float raindropsIntensity = 0.0f;

        public TVEGlobalAtmoData()
        {

        }
    }

    [System.Serializable]
    public class TVEGlobalFadeData
    {
        [Tooltip("Controls the global Cutout intensity.")]
        [Range(0.0f, 1.0f)]
        public float cutoutIntensity = 0.0f;

        public TVEGlobalFadeData()
        {

        }
    }

    [System.Serializable]
    public class TVEGlobalFormData
    {
        [Tooltip("Controls the global conform value")]
        public float heightConform = 0.0f;
        [Tooltip("Controls the global size fade scale.")]
        [Range(0.0f, 1.0f)]
        public float sizeFadeValue = 1.0f;

        public TVEGlobalFormData()
        {

        }
    }

    [System.Serializable]
    public class TVEGlobalLandData
    {
        [Tooltip("Controls the global height offset for terrains.")]
        public float heightOffset = 0.0f;

        public TVEGlobalLandData()
        {

        }
    }

    [System.Serializable]
    public class TVEGlobalFlowData
    {
        [Tooltip("Controls the global motion intensity.")]
        [Range(0.0f, 1.0f)]
        public float motionIntensity = 1.0f;
        [Tooltip("Controls the global flutter intensity.")]
        [Range(0.0f, 1.0f)]
        public float flutterIntensity = 1.0f;

        public TVEGlobalFlowData()
        {

        }
    }

    [System.Serializable]
    public class TVEElementMaterialData
    {
        public Shader shader;
        public string shaderName = "";
        public List<TVEElementPropertyData> props;

        public TVEElementMaterialData()
        {

        }
    }

    [System.Serializable]
    public class TVEElementPropertyData
    {
        public TVEPropertyType type;
        public string prop;
        public Texture texture;
        public Vector4 vector;
        public float value;

        public TVEElementPropertyData()
        {

        }
    }

    [System.Serializable]
    public class TVEInstanced
    {
        public int instancedDataID = 0;
        public int renderDataID = 0;
        public List<int> renderLayers;
        public int renderPass = 0;
        public Material material;
        public Mesh mesh;
        public List<TVEElement> elements = new List<TVEElement>();
        public List<Renderer> renderers = new List<Renderer>();
        public Matrix4x4[] matrices;
        public Vector4[] parameters;
        public MaterialPropertyBlock propertyBlock = new MaterialPropertyBlock();
        public int propertyBlockCount = -1;

        public TVEInstanced()
        {

        }
    }

    [System.Serializable]
    public class TVERenderData
    {
        [HideInInspector]
        public string name = "";
        [HideInInspector]
        public bool isInitialized = false;

        public TVEBool renderMode = TVEBool.On;
        [Tooltip("The name used for the global shader parameters.")]
        public string renderName = "Custom";

        [Space(10)]
        [Tooltip("Sets the render texture format.")]
        public TVETextureRange textureType = TVETextureRange.HDR;
        public TVEBool textureArray = TVEBool.On;
        [Tooltip("Sets render texture background color.")]
        public Color textureColor = Color.black;

        [Space(10)]
        [Tooltip("When enabled, the elements are rendered in realtime.")]
        public bool isRendering = true;

        [System.NonSerialized]
        public int renderDataID = 0;
        [System.NonSerialized]
        public int bufferSize = -1;
        [System.NonSerialized]
        public float[] bufferUsage;
        [System.NonSerialized]
        public RenderTexture renderTexBase;
        [System.NonSerialized]
        public RenderTexture renderTexNear;
        [System.NonSerialized]
        public CommandBuffer[] commandBuffers;

        [HideInInspector]
        public string texBaseName;
        [HideInInspector]
        public string texNearName;
        //[HideInInspector]
        //public string volCoordsBase;
        //[HideInInspector]
        //public string volCoordsNear;
        //[HideInInspector]
        //public string volPosRadius;
        [HideInInspector]
        public string texParams;
        [HideInInspector]
        public string texLayers;
        //[HideInInspector]
        //public string texLayersMax;

        public TVERenderData()
        {

        }
    }

    [System.Serializable]
    public class TVEMeshData
    {
        public Mesh mesh;
        public List<Vector3> vertices;
        public List<Color> colors;
        public List<Vector3> normals;
        public List<Vector4> tangents;
        public List<Vector4> UV0;
        public List<Vector4> UV2;
        public List<Vector4> UV4;

        public TVEMeshData()
        {

        }
    }

    [System.Serializable]
    public class TVEModelData
    {
        public Mesh mesh;
        public float height = 0;
        public float radius = 0;
        public List<float> variationMask;
        public List<float> occlusionMask;
        public List<float> detailMask;
        public List<float> heightMask;
        public List<Vector2> detailCoord;
        public List<float> motion2Mask;
        public List<float> motion3Mask;
        public List<Vector3> pivotPositions;

        public TVEModelData()
        {

        }
    }

    [System.Serializable]
    public class TVETerrainSettings
    {
        //[Space(10)]
        //public bool overrideAllLayers = true;
        //public bool overrideAllTextures = true;
        //public bool overrideAllSettings = true;

        [Space(10)]
        public bool useCustomTextures = false;

        [Space(10)]
        public Texture terrainControl01;
        public Texture terrainControl02;
        public Texture terrainControl03;
        public Texture terrainControl04;
        public Texture terrainHolesMask;

        [Space(10)]
        public bool useLayersOrderAsID = false;

        [Space(10)]
        public List<TVETerrainLayerSettings> terrainLayers = new List<TVETerrainLayerSettings>();
    }

    [System.Serializable]
    public class TVETerrainLayerSettings
    {
        [HideInInspector]
        public string name = "";
        [HideInInspector]
        public bool isInitialized = false;

        [Space(10)]
        [Range(1, 16)]
        public int layerID = 1;

        [Space(10)]
        [ColorUsage(false, true)]
        public Color layerColor = Color.white;

        [Space(10)]
        public bool useCustomLayer = false;

        [Space(10)]
        public TerrainLayer terrainLayer;

        [Space(10)]
        public bool useCustomTextures = false;

        [Space(10)]
        public Texture layerAlbedo;
        public Texture layerNormal;
        public Texture layerShader;

        [Space(10)]
        public bool useCustomSettings = false;

        [Space(10)]
        public Color layerSpecular = Color.black;
        public Vector4 layerRemapMin = Vector4.zero;
        public Vector4 layerRemapMax = Vector4.one;
        [Range(0, 1)]
        public float layerSmoothness = 1;
        [Range(-8, 8)]
        public float layerNormalScale = 1;

        [Space(10)]
        public bool useCustomCoords = false;

        [Space(10)]
        public TVEUVMode layerUVMode = TVEUVMode.Scale;
        public Vector4 layerUVValue = new Vector4(1, 1, 0, 0);

        public TVETerrainLayerSettings()
        {

        }
    }
}
