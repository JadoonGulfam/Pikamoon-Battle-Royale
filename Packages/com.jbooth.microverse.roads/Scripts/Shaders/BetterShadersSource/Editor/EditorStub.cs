
//////////////////////////////////////////////////////
// MicroVerse - Roads
// Copyright (c) Jason Booth
//////////////////////////////////////////////////////

using UnityEngine;
using UnityEditor;
using System.Collections.Generic;


namespace JBooth.Road
{
    public class LitBaseStub
    {
        Material mat;
        public LitBaseStub(ShaderGUI sg, Material m)
        {
            mat = m;
            rolloutKeywordStates.Clear();
        }

        MaterialProperty FindProperty(string name, MaterialProperty[] props)
        {
            foreach (var p in props)
            {
                if (p != null && p.name == name)
                    return p;
            }
            return null;
        }

        static System.Collections.Generic.Dictionary<string, bool> rolloutStates = new System.Collections.Generic.Dictionary<string, bool>();
        static GUIStyle rolloutStyle;
        public static bool DrawRollup(string text, bool defaultState = true, bool inset = false)
        {
            if (rolloutStyle == null)
            {
                rolloutStyle = new GUIStyle(GUI.skin.box);
                rolloutStyle.normal.textColor = EditorGUIUtility.isProSkin ? Color.white : Color.black;
            }
            var oldColor = GUI.contentColor;
            GUI.contentColor = EditorGUIUtility.isProSkin ? Color.white : Color.black;
            if (inset == true)
            {
                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.GetControlRect(GUILayout.Width(40));
            }

            if (!rolloutStates.ContainsKey(text))
            {
                rolloutStates[text] = defaultState;
                string key = text;
                if (EditorPrefs.HasKey(key))
                {
                    rolloutStates[text] = EditorPrefs.GetBool(key);
                }
            }
            if (GUILayout.Button(text, rolloutStyle, new GUILayoutOption[] { GUILayout.ExpandWidth(true), GUILayout.Height(20) }))
            {
                rolloutStates[text] = !rolloutStates[text];
                string key = text;
                EditorPrefs.SetBool(key, rolloutStates[text]);
            }
            if (inset == true)
            {
                EditorGUILayout.GetControlRect(GUILayout.Width(40));
                EditorGUILayout.EndHorizontal();
            }
            GUI.contentColor = oldColor;
            return rolloutStates[text];
        }

        static Dictionary<string, bool> rolloutKeywordStates = new System.Collections.Generic.Dictionary<string, bool>();

        public static bool DrawRollupKeywordToggle(Material mat, string text, string keyword)
        {
            if (rolloutStyle == null)
            {
                rolloutStyle = new GUIStyle(GUI.skin.box);
                rolloutStyle.normal.textColor = EditorGUIUtility.isProSkin ? Color.white : Color.black;
            }
            var oldColor = GUI.contentColor;

            bool toggle = mat.IsKeywordEnabled(keyword);

            if (mat.HasProperty("_HideUnused"))
            {
                bool hideUnused = mat.GetFloat("_HideUnused") > 0.5;
                if (!toggle && hideUnused)
                    return toggle;
            }

            EditorGUILayout.BeginHorizontal(rolloutStyle);

            if (!rolloutKeywordStates.ContainsKey(keyword))
            {
                rolloutKeywordStates[keyword] = toggle;
            }

            var nt = EditorGUILayout.Toggle(toggle, GUILayout.Width(18));
            if (nt != toggle)
            {
                mat.DisableKeyword(keyword);
                if (nt)
                {
                    mat.EnableKeyword(keyword);
                    rolloutKeywordStates[keyword] = true;
                }
                EditorUtility.SetDirty(mat);
            }

            if (GUILayout.Button(text, rolloutStyle, new GUILayoutOption[] { GUILayout.ExpandWidth(true), GUILayout.Height(20) }))
            {
                rolloutKeywordStates[keyword] = !rolloutKeywordStates[keyword];
            }
            EditorGUILayout.EndHorizontal();
            GUI.contentColor = oldColor;

            return rolloutKeywordStates[keyword];
        }


        public enum Packing
        {
            Unity,
            Fastest
        }

        public Packing GetPacking()
        {
            Packing packing = Packing.Unity;
            if (mat.IsKeywordEnabled("_PACKEDFAST"))
            {
                packing = Packing.Fastest;
            }
            return packing;
        }

        GUIContent CPacking = new GUIContent("Texture Packing", "Unity : PBR Data is packed into 3 textures, Fastest : Packed into 2 textures. FastMetal : 2 texture packing with metal instead of AO. See docs for packing format");
        Packing DoPacking(Material mat)
        {
            Packing packing = GetPacking();

            var np = (Packing)EditorGUILayout.EnumPopup(CPacking, packing);

            if (np != packing)
            {
                mat.DisableKeyword("_PACKEDFAST");
                if (np == Packing.Fastest)
                {
                    mat.EnableKeyword("_PACKEDFAST");
                }
                EditorUtility.SetDirty(mat);
            }
            return np;
        }

        public static bool DrawRollupToggle(Material mat, string text, ref bool toggle)
        {
            if (rolloutStyle == null)
            {
                rolloutStyle = new GUIStyle(GUI.skin.box);
                rolloutStyle.normal.textColor = EditorGUIUtility.isProSkin ? Color.white : Color.black;
            }
            var oldColor = GUI.contentColor;

            EditorGUILayout.BeginHorizontal(rolloutStyle);
            if (!rolloutStates.ContainsKey(text))
            {
                rolloutStates[text] = true;
                string key = text;
                if (EditorPrefs.HasKey(key))
                {
                    rolloutStates[text] = EditorPrefs.GetBool(key);
                }
            }

            var nt = EditorGUILayout.Toggle(toggle, GUILayout.Width(18));
            if (nt != toggle && nt == true)
            {
                // open when changing toggle state to true
                rolloutStates[text] = true;
                EditorPrefs.SetBool(text, rolloutStates[text]);
            }
            toggle = nt;
            if (GUILayout.Button(text, rolloutStyle, new GUILayoutOption[] { GUILayout.ExpandWidth(true), GUILayout.Height(20) }))
            {
                rolloutStates[text] = !rolloutStates[text];
                string key = text;
                EditorPrefs.SetBool(key, rolloutStates[text]);
            }
            EditorGUILayout.EndHorizontal();
            GUI.contentColor = oldColor;
            return rolloutStates[text];
        }


        public enum NoiseSpace
        {
            UV,
            Local,
            World
        }

        static GUIContent CNoiseSpace = new GUIContent("Noise Space", "Space used to generate the noise - 3d noise used in world and local space");
        NoiseSpace DoNoiseSpace(string prefix, string def)
        {
            NoiseSpace noiseSpace = NoiseSpace.UV;

            if (mat.IsKeywordEnabled(prefix + "NOISELOCAL" + def))
                noiseSpace = NoiseSpace.Local;
            if (mat.IsKeywordEnabled(prefix + "NOISEWORLD" + def))
                noiseSpace = NoiseSpace.World;


            EditorGUI.BeginChangeCheck();
            noiseSpace = (NoiseSpace)EditorGUILayout.EnumPopup(CNoiseSpace, noiseSpace);

            if (EditorGUI.EndChangeCheck())
            {
                mat.DisableKeyword(prefix + "NOISEWORLD" + def);
                mat.DisableKeyword(prefix + "NOISELOCAL" + def);
                if (noiseSpace == NoiseSpace.World)
                {
                    mat.EnableKeyword(prefix + "NOISEWORLD" + def);
                }
                else if (noiseSpace == NoiseSpace.Local)
                {
                    mat.EnableKeyword(prefix + "NOISELOCAL" + def);
                }
                EditorUtility.SetDirty(mat);
            }
            return noiseSpace;

        }

        enum NoiseQuality
        {
            Texture,
            ProceduralLow,
            ProceduralHigh,
            Worley,

        }

        GUIContent CNoiseQuality = new GUIContent("Noise Quality", "Texture based (fastest), 1 octave of value noise, 3 octaves of value noise, worley noise");
        NoiseQuality DoNoiseQuality(string prefix, string ext, string def, string texprefix, MaterialEditor materialEditor, MaterialProperty[] props, bool noiseForced = false)
        {
            NoiseQuality noiseQuality = NoiseQuality.ProceduralLow;
            if (noiseForced)
            {
                noiseQuality = NoiseQuality.Texture;
            }

            if (mat.IsKeywordEnabled(prefix + "NOISETEXTURE" + def))
            {
                noiseQuality = NoiseQuality.Texture;
            }
            else if (mat.IsKeywordEnabled(prefix + "NOISEHQ" + def))
            {
                noiseQuality = NoiseQuality.ProceduralHigh;
            }
            else if (mat.IsKeywordEnabled(prefix + "NOISEWORLEY" + def))
            {
                noiseQuality = NoiseQuality.Worley;
            }
            else if (mat.IsKeywordEnabled(prefix + "NOISELQ" + def))
            {
                noiseQuality = NoiseQuality.ProceduralLow;
            }

            var nq = (NoiseQuality)EditorGUILayout.EnumPopup(CNoiseQuality, noiseQuality);
            if (nq != noiseQuality)
            {
                mat.DisableKeyword(prefix + "NOISETEXTURE" + def);
                mat.DisableKeyword(prefix + "NOISEHQ" + def);
                mat.DisableKeyword(prefix + "NOISEWORLEY" + def);
                mat.DisableKeyword(prefix + "NOISELQ" + def);
                if (nq == NoiseQuality.Texture && noiseForced == false)
                {
                    mat.EnableKeyword(prefix + "NOISETEXTURE" + def);
                }
                else if (nq == NoiseQuality.ProceduralLow && noiseForced == true)
                {
                    mat.EnableKeyword(prefix + "NOISELQ" + def);
                }
                else if (nq == NoiseQuality.ProceduralHigh)
                {
                    mat.EnableKeyword(prefix + "NOISEHQ" + def);
                }
                else if (nq == NoiseQuality.Worley)
                {
                    mat.EnableKeyword(prefix + "NOISEWORLEY" + def);
                }
            }

            if (nq == NoiseQuality.Texture && noiseForced == false)
            {
                var prop = FindProperty(texprefix + "NoiseTex" + ext, props);
                if (prop.textureValue == null)
                {
                    prop.textureValue = FindDefaultTexture("betterlit_default_noise");
                }
                materialEditor.TexturePropertySingleLine(new GUIContent("Noise Texture"), prop);
            }
            return nq;
        }


        Texture2D FindDefaultTexture(string name)
        {
            var guids = AssetDatabase.FindAssets("t:Texture2D roads_default_");
            foreach (var g in guids)
            {
                var path = AssetDatabase.GUIDToAssetPath(g);
                if (path.Contains(name))
                {
                    return AssetDatabase.LoadAssetAtPath<Texture2D>(path);
                }
            }
            return null;
        }

        public static void DrawSeparator()
        {
            EditorGUILayout.Separator();
            GUILayout.Box("", new GUILayoutOption[] { GUILayout.ExpandWidth(true), GUILayout.Height(1) });
            EditorGUILayout.Separator();
        }

        public static void WarnLinear(Texture tex)
        {
            if (tex != null)
            {
                AssetImporter ai = AssetImporter.GetAtPath(AssetDatabase.GetAssetPath(tex));
                if (ai != null)
                {
                    TextureImporter ti = ai as TextureImporter;
                    if (ti != null && ti.sRGBTexture != false)
                    {
                        EditorGUILayout.BeginHorizontal();
                        EditorGUILayout.HelpBox("Texture is sRGB! Should be linear!", MessageType.Error);
                        if (GUILayout.Button("Fix"))
                        {
                            ti.sRGBTexture = false;
                            ti.SaveAndReimport();
                        }
                        EditorGUILayout.EndHorizontal();
                    }
                }
            }
        }

        public static void WarnNormal(Texture tex)
        {
            if (tex != null)
            {
                AssetImporter ai = AssetImporter.GetAtPath(AssetDatabase.GetAssetPath(tex));
                if (ai != null)
                {
                    TextureImporter ti = ai as TextureImporter;
                    if (ti != null && ti.textureType != TextureImporterType.NormalMap)
                    {
                        EditorGUILayout.BeginHorizontal();
                        EditorGUILayout.HelpBox("Texture is set to type normal!", MessageType.Error);
                        if (GUILayout.Button("Fix"))
                        {
                            ti.textureType = TextureImporterType.NormalMap;
                            ti.SaveAndReimport();
                        }
                        EditorGUILayout.EndHorizontal();
                    }
                }
            }
        }




        GUIContent CStochastic = new GUIContent("Stochastic", "Prevents visible tiling on surfaces");
        GUIContent CStochatsicContrast = new GUIContent("Stochastic Contrast", "How tight the blend between stochastic clusers is");
        GUIContent CStochasticScale = new GUIContent("Stochastic Scale", "How large the patches of texture are before blending into the next area");
        bool DoStochastic(Material mat, MaterialEditor materialEditor, MaterialProperty[] props, string keyword, string prop, string prop2)
        {
            bool mode = false;
            if (mat.IsKeywordEnabled(keyword))
            {
                mode = true;
            }
            EditorGUI.BeginChangeCheck();
            mode = EditorGUILayout.Toggle(CStochastic, mode);
            if (EditorGUI.EndChangeCheck())
            {
                if (mode)
                {
                    mat.EnableKeyword(keyword);
                }
                else
                {
                    mat.DisableKeyword(keyword);
                }
                EditorUtility.SetDirty(mat);
            }

            var old = GUI.enabled;
            GUI.enabled = mode;
            if (mode)
            {
                EditorGUI.indentLevel++;
                materialEditor.ShaderProperty(FindProperty(prop, props), CStochatsicContrast);
                materialEditor.ShaderProperty(FindProperty(prop2, props), CStochasticScale);
                EditorGUI.indentLevel--;
            }
            GUI.enabled = old;

            return mode;
        }

        GUIContent CTraxPackedNormal = new GUIContent("Packed Map", "Normal in fastest packed format, see docs for details");
        public void DoTrax(MaterialEditor materialEditor, MaterialProperty[] props)
        {
            if (DrawRollupKeywordToggle(mat, "Trax", "_TRAX_ON"))
            {
                materialEditor.TexturePropertySingleLine(new GUIContent("Albedo"), FindProperty("_TraxAlbedo", props), FindProperty("_TraxTint", props));
                materialEditor.TextureScaleOffsetProperty(FindProperty("_TraxAlbedo", props));

                materialEditor.TexturePropertySingleLine(CTraxPackedNormal, FindProperty("_TraxPackedNormal", props), FindProperty("_TraxNormalStrength", props));

                materialEditor.RangeProperty(FindProperty("_TraxInterpContrast", props), "Interpolation Contrast");
                materialEditor.RangeProperty(FindProperty("_TraxHeightContrast", props), "Height Blend Contrast");
                if (mat.HasProperty("_TessellationMaxSubdiv"))
                {
                    materialEditor.FloatProperty(FindProperty("_TraxDisplacementDepth", props), "Trax Depression Depth");
                    materialEditor.RangeProperty(FindProperty("_TraxDisplacementStrength", props), "Trax Displacement Strength");
                    materialEditor.RangeProperty(FindProperty("_TraxMipBias", props), "Trax Mip Bias");
                }
            }
        }

        enum RainMode
        {
            Off,
            Local,
            Global
        }

        enum RainUV
        {
            UV,
            World
        }

        public void DoRainDrops(MaterialEditor materialEditor, MaterialProperty[] props)
        {
            RainMode mode = (RainMode)mat.GetInt("_RainMode");

            var nm = (RainMode)EditorGUILayout.EnumPopup("Rain Drops", mode);

            if (nm != mode)
            {
                mode = nm;
                mat.DisableKeyword("_RAINDROPS");
                mat.SetFloat("_RainMode", 0);
                if (mode == RainMode.Local)
                {
                    mat.EnableKeyword("_RAINDROPS");
                    mat.SetFloat("_RainMode", 1);
                }
                else if (mode == RainMode.Global)
                {
                    mat.EnableKeyword("_RAINDROPS");
                    mat.SetFloat("_RainMode", 2);
                }
                EditorUtility.SetDirty(mat);
            }
            if (mode != RainMode.Off)
            {
                EditorGUI.indentLevel++;
                var prop = FindProperty("_RainDropTexture", props);
                if (prop.textureValue == null)
                {
                    prop.textureValue = FindDefaultTexture("raindrops");
                }
                RainUV uvMode = (RainUV)mat.GetInt("_RainUV");

                var ruv = (RainUV)EditorGUILayout.EnumPopup("UV Mode", uvMode);

                if (uvMode != ruv)
                {
                    mat.SetInt("_RainUV", ruv == RainUV.UV ? 0 : 1);
                    EditorUtility.SetDirty(mat);
                }

                materialEditor.TexturePropertySingleLine(new GUIContent("Rain Texture"), FindProperty("_RainDropTexture", props));
                Vector4 data = mat.GetVector("_RainIntensityScale");
                EditorGUI.BeginChangeCheck();
                if (mode != RainMode.Global)
                {
                    data.x = EditorGUILayout.Slider("Intensity", data.x, 0, 2);
                }
                data.y = EditorGUILayout.FloatField("UV Scale", data.y);
                data.z = EditorGUILayout.Slider("Effect Wet Areas", data.z, 0, 1);
                float oldW = data.w;
                data.w = EditorGUILayout.FloatField("Distance Falloff", data.w);
                // revision
                if (oldW == data.w && data.w == 0)
                {
                    data.w = 200;
                    mat.SetVector("_RainIntensityScale", data);
                    EditorUtility.SetDirty(mat);
                }
                if (EditorGUI.EndChangeCheck())
                {
                    mat.SetVector("_RainIntensityScale", data);
                    EditorUtility.SetDirty(mat);
                }
                EditorGUI.indentLevel--;
            }

        }

        public void DoPuddles(MaterialEditor materialEditor, MaterialProperty[] props)
        {
            if (!mat.HasProperty("_PuddleMode"))
                return;
            if (DrawRollupKeywordToggle(mat, "Puddles", "_PUDDLES"))
            {
                LocalGlobalMode mode = (LocalGlobalMode)mat.GetInt("_PuddleMode");

                EditorGUI.BeginChangeCheck();
                mode = (LocalGlobalMode)EditorGUILayout.EnumPopup("Puddle Mode", mode);

                if (EditorGUI.EndChangeCheck())
                {
                    mat.SetFloat("_PuddleMode", (int)mode);
                }
                EditorGUI.indentLevel++;
                if (mode == LocalGlobalMode.Local)
                {
                    materialEditor.ShaderProperty(FindProperty("_PuddleAmount", props), "Puddle Amount");
                }


                materialEditor.ShaderProperty(FindProperty("_PuddleColor", props), "Puddle Color");
                materialEditor.ShaderProperty(FindProperty("_PuddleAngleMin", props), "Puddle Angle Filter");
                materialEditor.ShaderProperty(FindProperty("_PuddleFalloff", props), "Puddle Contrast");
                materialEditor.ShaderProperty(FindProperty("_PuddleHeightDampening", props), "Height Dampening");

                bool noiseOn = mat.IsKeywordEnabled("_PUDDLENOISE");


                EditorGUI.BeginChangeCheck();
                noiseOn = EditorGUILayout.Toggle("Puddle Noise", noiseOn);
                if (EditorGUI.EndChangeCheck())
                {
                    mat.DisableKeyword("_PUDDLENOISE");
                    if (noiseOn)
                    {
                        mat.EnableKeyword("_PUDDLENOISE");
                    }
                    EditorUtility.SetDirty(mat);
                }

                if (noiseOn)
                {
                    EditorGUI.indentLevel++;
                    EditorGUI.BeginChangeCheck();

                    DoNoiseSpace("_PUDDLE", "");
                    DoNoiseQuality("_PUDDLE", "", "", "_Puddle", materialEditor, props);
                    materialEditor.ShaderProperty(FindProperty("_PuddleNoiseFrequency", props), "Noise Frequency");
                    materialEditor.ShaderProperty(FindProperty("_PuddleNoiseAmplitude", props), "Noise Amplitude");
                    materialEditor.ShaderProperty(FindProperty("_PuddleNoiseCenter", props), "Noise Center");
                    materialEditor.ShaderProperty(FindProperty("_PuddleNoiseOffset", props), "Noise Offset");

                    if (EditorGUI.EndChangeCheck())
                    {
                        EditorUtility.SetDirty(mat);
                    }
                    EditorGUI.indentLevel--;
                }

                DoRainDrops(materialEditor, props);
                EditorGUI.indentLevel--;
            }


        }


        enum LocalGlobalMode
        {
            Local = 0,
            Global = 1
        }

        public void DoWetness(MaterialEditor materialEditor, MaterialProperty[] props)
        {
            if (!mat.HasProperty("_WetnessAmount"))
                return;
            if (DrawRollupKeywordToggle(mat, "Wetness", "_WETNESS"))
            {
                LocalGlobalMode mode = (LocalGlobalMode)mat.GetInt("_WetnessMode");
                EditorGUI.BeginChangeCheck();
                mode = (LocalGlobalMode)EditorGUILayout.EnumPopup("Wetness Mode", mode);

                if (EditorGUI.EndChangeCheck())
                {
                    mat.SetInt("_WetnessMode", (int)mode);
                    EditorUtility.SetDirty(mat);
                }
                EditorGUI.indentLevel++;
                materialEditor.ShaderProperty(FindProperty("_WetnessAmount", props), "Wetness Amount");

                materialEditor.ShaderProperty(FindProperty("_WetnessMin", props), "Wetness Min");
                materialEditor.ShaderProperty(FindProperty("_WetnessMax", props), "Wetness Max");
                materialEditor.ShaderProperty(FindProperty("_WetnessFalloff", props), "Wetness Falloff");
                materialEditor.ShaderProperty(FindProperty("_WetnessAngleMin", props), "Wetness Angle Minimum");
                var shore = FindProperty("_WetnessShoreline", props);
                EditorGUILayout.BeginHorizontal();
                bool on = false;
                if (shore.floatValue > -9990)
                {
                    on = true;
                }
                var newOn = EditorGUILayout.Toggle("Wetness Shore Height", on);
                if (newOn != on)
                {
                    if (newOn)
                        shore.floatValue = 0;
                    else
                        shore.floatValue = -9999;
                    on = newOn;
                }
                var oldEnabled = GUI.enabled;
                GUI.enabled = on;
                if (on)
                {
                    float nv = EditorGUILayout.FloatField(shore.floatValue);
                    if (nv != shore.floatValue)
                    {
                        shore.floatValue = nv;
                    }
                }
                else
                {
                    EditorGUILayout.FloatField(0);
                }
                GUI.enabled = oldEnabled;
                EditorGUILayout.EndHorizontal();
                materialEditor.ShaderProperty(FindProperty("_Porosity", props), "Porosity");

                EditorGUI.indentLevel--;
            }
        }

        public void DoSparkle(MaterialEditor materialEditor, MaterialProperty[] props,
           string keyword, string noiseTex, string paramProp)
        {
            bool enabled = mat.IsKeywordEnabled(keyword);
            bool newEnabled = EditorGUILayout.Toggle("Sparkle", enabled);
            if (newEnabled != enabled)
            {
                mat.DisableKeyword(keyword);
                if (newEnabled)
                {
                    mat.EnableKeyword(keyword);
                }
            }
            if (newEnabled)
            {
                EditorGUI.indentLevel++;

                var prop = FindProperty(noiseTex, props);
                if (prop.textureValue == null)
                {
                    prop.textureValue = FindDefaultTexture("sparkle_noise");
                }
                materialEditor.TexturePropertySingleLine(new GUIContent("Noise Texture"), prop);
                var dataProp = FindProperty(paramProp, props);
                Vector4 v4 = dataProp.vectorValue;
                EditorGUI.BeginChangeCheck();
                v4.x = EditorGUILayout.FloatField("Tiling", v4.x);
                v4.y = EditorGUILayout.Slider("Cutoff", v4.y, 0, 1);
                v4.z = EditorGUILayout.Slider("Intensity", v4.z, 0, 1);
                v4.w = EditorGUILayout.Slider("Emission", v4.w, 0, 100);
                if (EditorGUI.EndChangeCheck())
                {
                    dataProp.vectorValue = v4;
                }
                EditorGUI.indentLevel--;
            }
        }

        enum SnowWorldFadeMode
        {
            Off = 0,
            On,
            Global
        }

        enum AsphaltSpace
        {
            UV,
            WorldTriplanar
        }

        public void DoLitGUI(MaterialEditor materialEditor, MaterialProperty[] props)
        {
            materialEditor.TexturePropertySingleLine(new GUIContent("Albedo"), FindProperty("_MainTex", props), FindProperty("_Tint", props));
            materialEditor.TextureScaleOffsetProperty(FindProperty("_MainTex", props));
            materialEditor.ShaderProperty(FindProperty("_Alpha", props), "Alpha Cut");

            if (mat.IsKeywordEnabled("_ALPHA_DITHERFADE"))
            {
                materialEditor.ShaderProperty(FindProperty("_DitherFade", props), "Distance Fade Start / Disatnce");
            }
            else if (mat.IsKeywordEnabled("_ALPHA_CUT"))
            {
                materialEditor.ShaderProperty(FindProperty("_AlphaThreshold", props), new GUIContent("Alpha Threshold"));
            }
            materialEditor.TexturePropertySingleLine(new GUIContent("Normal Map"), FindProperty("_NormalMap", props), FindProperty("_NormalStrength", props));

            materialEditor.ShaderProperty(FindProperty("_UseMaskMap", props), "Use Mask Map");
            bool maskMap = mat.IsKeywordEnabled("_MASKMAP");
            if (maskMap)
                materialEditor.TexturePropertySingleLine(new GUIContent("Mask Map"), FindProperty("_MaskMap", props));

            materialEditor.ShaderProperty(FindProperty("_UseEmission", props), "Use Emission");
            bool emission = mat.IsKeywordEnabled("_EMISSION");
            if (emission)
            {
                materialEditor.TexturePropertySingleLine(new GUIContent("Emission Map"), FindProperty("_EmissionMap", props), FindProperty("_EmissionTint", props));
                materialEditor.ShaderProperty(FindProperty("_EmissionStrength", props), new GUIContent("Emission Strength"));
            }

            materialEditor.ShaderProperty(FindProperty("_UseDetail", props), "Use Detail Texture");

            bool detail = mat.IsKeywordEnabled("_DETAIL");
            if (detail)
            {
                materialEditor.TexturePropertySingleLine(new GUIContent("Detail Map"), FindProperty("_DetailMap", props));
                materialEditor.ShaderProperty(FindProperty("_DetailAlbedoStrength", props), new GUIContent("Detail Albedo Strength"));
                materialEditor.ShaderProperty(FindProperty("_DetailNormalStrength", props), new GUIContent("Detail Normal Strength"));
                materialEditor.ShaderProperty(FindProperty("_DetailSmoothnessStrength", props), new GUIContent("Detail Smoothness Strength"));
            }

        }

        public void DoRoadGUI(MaterialEditor materialEditor, MaterialProperty[] props)
        {
            DoPacking(mat);
            if (DrawRollup("Main"))
            {
                AsphaltSpace uvSpace = AsphaltSpace.UV;
                if (mat.IsKeywordEnabled("_ASPHALT_WORLDTRIPLANAR"))
                    uvSpace = AsphaltSpace.WorldTriplanar;
                var newUVSpace = (AsphaltSpace)EditorGUILayout.EnumPopup("Asphalt UV Space", uvSpace);
                if (newUVSpace != uvSpace)
                {
                    mat.DisableKeyword("_ASPHALT_WORLDTRIPLANAR");
                    if (newUVSpace == AsphaltSpace.WorldTriplanar)
                        mat.EnableKeyword("_ASPHALT_WORLDTRIPLANAR");
                }

                materialEditor.TexturePropertySingleLine(new GUIContent("Asphalt Albedo"), FindProperty("_Asphalt_Albedo", props), FindProperty("_Asphalt_Tint", props));
                materialEditor.TextureScaleOffsetProperty(FindProperty("_Asphalt_Albedo", props));

                if (mat.IsKeywordEnabled("_PACKEDFAST"))
                {
                    WarnLinear(FindProperty("_Asphalt_NormalSAO", props).textureValue);
                    materialEditor.TexturePropertySingleLine(new GUIContent("Aslphalt Packed NormalSAO"), FindProperty("_Asphalt_NormalSAO", props), FindProperty("_Asphalt_NormalStrength", props));
                }
                else
                {
                    WarnNormal(FindProperty("_Asphalt_NormalSAO", props).textureValue);
                    materialEditor.TexturePropertySingleLine(new GUIContent("Asphalt Normal"), FindProperty("_Asphalt_NormalSAO", props), FindProperty("_Asphalt_NormalStrength", props));
                }

                if (!mat.IsKeywordEnabled("_PACKEDFAST"))
                {
                    WarnLinear(FindProperty("_Asphalt_MaskMap", props).textureValue);
                    materialEditor.TexturePropertySingleLine(new GUIContent("Asphalt Mask"), FindProperty("_Asphalt_MaskMap", props));
                }


                materialEditor.DefaultShaderProperty(FindProperty("_Asphalt_Smoothness", props), "Smoothness Modifier");
                materialEditor.DefaultShaderProperty(FindProperty("_Asphalt_Metallic", props), "Metallic Modifier");
                DoStochastic(mat, materialEditor, props, "_ASPHALTSTOCHASTIC", "_AsphaltStochasticContrast", "_AsphaltStochasticScale");

                DrawSeparator();
                materialEditor.TexturePropertySingleLine(new GUIContent("Line/Wear Mask"), FindProperty("_Mask", props));
                materialEditor.DefaultShaderProperty(FindProperty("_LineColorA", props), "Line Color A");
                materialEditor.DefaultShaderProperty(FindProperty("_LineColorB", props), "Line Color B");
                materialEditor.DefaultShaderProperty(FindProperty("_LineEmissiveA", props), "Line Emissive A");
                materialEditor.DefaultShaderProperty(FindProperty("_LineEmissiveB", props), "Line Emissive B");


                materialEditor.TexturePropertySingleLine(new GUIContent("Wear Noise"), FindProperty("_WearNoise", props));
                materialEditor.TextureScaleOffsetProperty(FindProperty("_WearNoise", props));
                materialEditor.DefaultShaderProperty(FindProperty("_LineWear", props), "Line Wear");

                DrawSeparator();
                if (DrawRollupKeywordToggle(mat, "Wear A", "_WEAR_A"))
                {
                    materialEditor.TexturePropertySingleLine(new GUIContent("Wear Map A"), FindProperty("_WearMapA", props), FindProperty("_WearA_Tint", props));
                    materialEditor.TextureScaleOffsetProperty(FindProperty("_WearMapA", props));

                    Vector4 propsA = FindProperty("_WearA_PBR", props).vectorValue;
                    Vector4 propsA2 = FindProperty("_WearA_NoiseParams", props).vectorValue;

                    EditorGUI.BeginChangeCheck();
                    propsA.w = EditorGUILayout.Slider("Weight", propsA.w, 0, 16);
                    propsA.x = EditorGUILayout.Slider("Smoothness", propsA.x, -1, 1);
                    propsA.y = EditorGUILayout.Slider("Occlusion", propsA.y, 0, 1);
                    propsA.z = EditorGUILayout.Slider("Normal", propsA.z, 0, 3);
                    propsA2.x = EditorGUILayout.Slider("Noise Contrast", propsA2.x, 0.2f, 20);
                    propsA2.y = EditorGUILayout.Toggle("Mask Invert", propsA2.y > 0.5f ? true : false) ? 1 : 0;
                    if (EditorGUI.EndChangeCheck())
                    {
                        FindProperty("_WearA_PBR", props).vectorValue = propsA;
                        FindProperty("_WearA_NoiseParams", props).vectorValue = propsA2;
                    }
                }

                if (DrawRollupKeywordToggle(mat, "Wear B", "_WEAR_B"))
                {
                    materialEditor.TexturePropertySingleLine(new GUIContent("Wear Map B"), FindProperty("_WearMapB", props), FindProperty("_WearB_Tint", props));
                    materialEditor.TextureScaleOffsetProperty(FindProperty("_WearMapB", props));

                    Vector4 propsB = FindProperty("_WearB_PBR", props).vectorValue;
                    Vector4 propsB2 = FindProperty("_WearB_NoiseParams", props).vectorValue;

                    EditorGUI.BeginChangeCheck();
                    propsB.w = EditorGUILayout.Slider("Weight", propsB.w, 0, 16);
                    propsB.x = EditorGUILayout.Slider("Smoothness", propsB.x, -1, 1);
                    propsB.y = EditorGUILayout.Slider("Occlusion", propsB.y, 0, 1);
                    propsB.z = EditorGUILayout.Slider("Normal", propsB.z, 0, 3);
                    propsB2.x = EditorGUILayout.Slider("Noise Contrast", propsB2.x, 0.2f, 20);
                    propsB2.y = EditorGUILayout.Toggle("Mask Invert", propsB2.y > 0.5f ? true : false) ? 1 : 0;
                    if (EditorGUI.EndChangeCheck())
                    {
                        FindProperty("_WearB_PBR", props).vectorValue = propsB;
                        FindProperty("_WearB_NoiseParams", props).vectorValue = propsB2;
                    }
                }
                if (DrawRollupKeywordToggle(mat, "Overlay", "_OVERLAY"))
                {
                    materialEditor.TexturePropertySingleLine(new GUIContent("Albedo"), FindProperty("_Overlay_Albedo", props));
                    materialEditor.TextureScaleOffsetProperty(FindProperty("_Overlay_Albedo", props));
                    materialEditor.TexturePropertySingleLine(new GUIContent("Normal"), FindProperty("_Overlay_Normal", props));
                    materialEditor.TexturePropertySingleLine(new GUIContent("Mask Map"), FindProperty("_Overlay_Mask", props));
                    materialEditor.TexturePropertySingleLine(new GUIContent("Variation", "World Space mask in G channel"), FindProperty("_Overlay_VariationMask", props));
                    materialEditor.TextureScaleOffsetProperty(FindProperty("_Overlay_VariationMask", props));
                }
            }
        }

        public void DoSnow(MaterialEditor materialEditor, MaterialProperty[] props)
        {
            if (!mat.HasProperty("_SnowMode"))
                return;
            if (DrawRollupKeywordToggle(mat, "Snow", "_SNOW"))
            {
                LocalGlobalMode mode = (LocalGlobalMode)mat.GetInt("_SnowMode");
                EditorGUI.BeginChangeCheck();
                mode = (LocalGlobalMode)EditorGUILayout.EnumPopup("Snow Mode", mode);

                if (EditorGUI.EndChangeCheck())
                {
                    mat.SetInt("_SnowMode", (int)mode);
                }
                EditorGUI.indentLevel++;
                EditorGUI.BeginChangeCheck();

                materialEditor.TexturePropertySingleLine(new GUIContent("Snow Albedo"), FindProperty("_SnowAlbedo", props), FindProperty("_SnowTint", props));
                materialEditor.TextureScaleOffsetProperty(FindProperty("_SnowAlbedo", props));

                if (!mat.IsKeywordEnabled("_AUTONORMAL"))
                {
                    if (mat.IsKeywordEnabled("_PACKEDFAST"))
                    {
                        WarnLinear(FindProperty("_SnowNormal", props).textureValue);
                        materialEditor.TexturePropertySingleLine(new GUIContent("Snow Packed"), FindProperty("_SnowNormal", props));
                    }
                    else
                    {
                        WarnNormal(FindProperty("_SnowNormal", props).textureValue);
                        materialEditor.TexturePropertySingleLine(new GUIContent("Snow Normal"), FindProperty("_SnowNormal", props));
                    }

                }
                if (!mat.IsKeywordEnabled("_PACKEDFAST"))
                {
                    WarnLinear(FindProperty("_SnowMaskMap", props).textureValue);
                    materialEditor.TexturePropertySingleLine(new GUIContent("Snow Mask"), FindProperty("_SnowMaskMap", props));
                }

                if (mode == LocalGlobalMode.Local)
                {
                    materialEditor.ShaderProperty(FindProperty("_SnowAmount", props), "Snow Amount");
                }

                DoStochastic(mat, materialEditor, props, "_SNOWSTOCHASTIC", "_SnowStochasticContrast", "_SnowStochasticScale");
                materialEditor.ShaderProperty(FindProperty("_SnowAngle", props), "Snow Angle Falloff");
                materialEditor.ShaderProperty(FindProperty("_SnowContrast", props), "Snow Contrast");

                Vector3 worldData = mat.GetVector("_SnowWorldFade");
                EditorGUI.BeginChangeCheck();
                SnowWorldFadeMode sfm = (SnowWorldFadeMode)worldData.z;
                sfm = (SnowWorldFadeMode)EditorGUILayout.EnumPopup("World Height Fade", sfm);
                worldData.z = (int)sfm;
                bool old = GUI.enabled;
                GUI.enabled = sfm == SnowWorldFadeMode.On;
                EditorGUI.indentLevel++;
                worldData.x = EditorGUILayout.FloatField("Start Height", worldData.x);
                worldData.y = EditorGUILayout.FloatField("Fade In Range", worldData.y);
                EditorGUI.indentLevel--;
                if (EditorGUI.EndChangeCheck())
                {
                    mat.SetVector("_SnowWorldFade", worldData);
                    EditorUtility.SetDirty(mat);
                }
                GUI.enabled = old;
                //materialEditor.ShaderProperty(FindProperty("_SnowVertexHeight", props), "Snow Vertex Offset");
                if (EditorGUI.EndChangeCheck())
                {
                    if (mat.GetTexture("_SnowMaskMap") != null)
                    {
                        mat.EnableKeyword("_SNOWMASKMAP");
                    }
                    else
                    {
                        mat.DisableKeyword("_SNOWMASKMAP");
                    }
                    if (mat.GetTexture("_SnowNormal") != null)
                    {
                        mat.EnableKeyword("_SNOWNORMALMAP");
                    }
                    else
                    {
                        mat.DisableKeyword("_SNOWNORMALMAP");
                    }

                }

                bool noise = mat.IsKeywordEnabled("_SNOWNOISE");
                EditorGUI.BeginChangeCheck();
                noise = EditorGUILayout.Toggle("Transition Noise", noise);
                if (EditorGUI.EndChangeCheck())
                {
                    mat.DisableKeyword("_SNOWNOISE");
                    if (noise)
                    {
                        mat.EnableKeyword("_SNOWNOISE");
                    }
                }
                if (noise)
                {
                    EditorGUI.indentLevel++;
                    materialEditor.FloatProperty(FindProperty("_SnowNoiseFreq", props), "Frequency");
                    materialEditor.FloatProperty(FindProperty("_SnowNoiseAmp", props), "Amplitude");
                    materialEditor.FloatProperty(FindProperty("_SnowNoiseOffset", props), "Offset");
                    EditorGUI.indentLevel--;
                }

                DoSparkle(materialEditor, props, "_SNOWSPARKLES", "_SnowSparkleNoise", "_SnowSparkleTCI");

                if (mat.IsKeywordEnabled("_TRAX_ON"))
                {
                    materialEditor.TexturePropertySingleLine(new GUIContent("Trax Snow Albedo"), FindProperty("_SnowTraxAlbedo", props), FindProperty("_SnowTraxTint", props));
                    materialEditor.TextureScaleOffsetProperty(FindProperty("_SnowTraxAlbedo", props));

                    if (!mat.IsKeywordEnabled("_AUTONORMAL"))
                    {
                        if (mat.IsKeywordEnabled("_PACKEDFAST"))
                        {
                            WarnLinear(FindProperty("_SnowTraxNormal", props).textureValue);
                            materialEditor.TexturePropertySingleLine(new GUIContent("Trax Snow Packed"), FindProperty("_SnowTraxNormal", props));
                        }
                        else
                        {
                            WarnNormal(FindProperty("_SnowTraxNormal", props).textureValue);
                            materialEditor.TexturePropertySingleLine(new GUIContent("Trax Snow Normal"), FindProperty("_SnowTraxNormal", props));
                        }

                    }
                    if (!mat.IsKeywordEnabled("_PACKEDFAST"))
                    {
                        WarnLinear(FindProperty("_SnowTraxMaskMap", props).textureValue);
                        materialEditor.TexturePropertySingleLine(new GUIContent("Trax Snow Mask"), FindProperty("_SnowTraxMaskMap", props));
                    }

                }
                EditorGUI.indentLevel--;
            }
        }



        enum ColorSideMode
        {
            None,
            Color,
            Gradient,
            Texture
        }

        enum ColorSideSpace
        {
            UV = 0,
            Local,
            World
        }

        void DoColorSide(MaterialEditor materialEditor, MaterialProperty[] props,
              string label, string colorKeyword, string texkeyword, string gradkeyword,
             string colorProp, string colorProp2, string texProp, string rangeProp, string clampProp)
        {
            ColorSideMode csm = ColorSideMode.None;
            if (mat.IsKeywordEnabled(colorKeyword))
            {
                csm = ColorSideMode.Color;
            }
            else if (mat.IsKeywordEnabled(texkeyword))
            {
                csm = ColorSideMode.Texture;
            }
            else if (mat.IsKeywordEnabled(gradkeyword))
            {
                csm = ColorSideMode.Gradient;
            }

            var ncsm = (ColorSideMode)EditorGUILayout.EnumPopup(label, csm);
            if (ncsm != csm)
            {
                csm = ncsm;
                mat.DisableKeyword(texkeyword);
                mat.DisableKeyword(gradkeyword);
                mat.DisableKeyword(colorKeyword);
                if (ncsm == ColorSideMode.Color)
                {
                    mat.EnableKeyword(colorKeyword);
                }
                else if (ncsm == ColorSideMode.Texture)
                {
                    mat.EnableKeyword(texkeyword);
                }
                else if (ncsm == ColorSideMode.Gradient)
                {
                    mat.EnableKeyword(gradkeyword);
                }
            }
            EditorGUI.indentLevel++;
            if (csm == ColorSideMode.Color)
            {
                materialEditor.ColorProperty(FindProperty(colorProp, props), "Color");
            }
            else if (csm == ColorSideMode.Gradient)
            {
                EditorGUILayout.BeginHorizontal();
                materialEditor.ColorProperty(FindProperty(colorProp, props), "Color");
                materialEditor.ColorProperty(FindProperty(colorProp2, props), "");
                EditorGUILayout.EndHorizontal();
            }
            else if (csm == ColorSideMode.Texture)
            {
                materialEditor.TexturePropertySingleLine(new GUIContent("Albedo"), FindProperty(texProp, props));
            }
            if (csm != ColorSideMode.Color && csm != ColorSideMode.None)
            {
                var prop = FindProperty(rangeProp, props);
                var data = prop.vectorValue;
                ColorSideSpace space = (ColorSideSpace)(int)data.w;
                EditorGUI.indentLevel++;
                EditorGUI.BeginChangeCheck();
                space = (ColorSideSpace)EditorGUILayout.EnumPopup("Space", space);
                data.w = (int)space;
                data.x = EditorGUILayout.FloatField("Start", data.x);
                data.y = EditorGUILayout.FloatField("Size", data.y);
                data.z = EditorGUILayout.Slider("Rotation", data.z, -Mathf.PI, Mathf.PI);
                if (EditorGUI.EndChangeCheck())
                {
                    prop.vectorValue = data;
                }
                if (csm == ColorSideMode.Texture)
                {
                    Vector2 vals = FindProperty(clampProp, props).vectorValue;
                    bool clamped = vals.x > 0 && vals.y < 1.0f;
                    bool newClamped = EditorGUILayout.Toggle("Clamp UV Range", clamped);
                    if (newClamped != clamped)
                    {
                        if (newClamped)
                        {
                            FindProperty(clampProp, props).vectorValue = new Vector4(0.001f, 0.999f);
                        }
                        else
                        {
                            FindProperty(clampProp, props).vectorValue = new Vector4(-99999, 99999);
                        }
                    }
                }
                EditorGUI.indentLevel--;
            }
            EditorGUI.indentLevel--;

        }

        public void OnEffectsGUI(MaterialEditor materialEditor, MaterialProperty[] props)
        {
            DoWetness(materialEditor, props);
            DoPuddles(materialEditor, props);
            DoSnow(materialEditor, props);
        }
    }
}