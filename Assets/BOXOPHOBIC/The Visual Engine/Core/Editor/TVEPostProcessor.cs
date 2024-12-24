#if !THE_VISUAL_ENGINE_DEVELOPMENT
#if !THE_VISUAL_ENGINE_NOPROCESSING

using Boxophobic.Utils;
using System.IO;
using UnityEditor;
using UnityEngine;

namespace TheVisualEngine
{
    class TVEPostProcessor : AssetPostprocessor
    {
#if UNITY_2022_3_OR_NEWER
        // Unity 2021.3 seems to have a bug with post processing assets
        static void OnPostprocessAllAssets(string[] importedAssets, string[] deletedAssets, string[] movedAssets, string[] movedFromAssetPaths)
        {
            foreach (var path in importedAssets)
            {
                if (path.EndsWith(".shader"))
                {
                    if (TVEUtils.IsValidShader(path))
                    {
                        string userFolder = TVEUtils.GetUserFolder();

                        var shader = AssetDatabase.LoadAssetAtPath<Shader>(path);
                        var engine = SettingsUtils.LoadSettingsData(userFolder + "/Shaders/Engine " + shader.name.Replace("/", "__") + ".asset", "Unity Default Renderer");
                        var model = SettingsUtils.LoadSettingsData(userFolder + "/Shaders/Model " + shader.name.Replace("/", "__") + ".asset", "From Shader");

                        var shaderSettings = new TVEShaderSettings();
                        shaderSettings.renderEngine = engine;
                        shaderSettings.shaderModel = model;

                        TVEUtils.SetShaderSettings(path, shaderSettings);

                        AssetDatabase.SaveAssets();
                        AssetDatabase.Refresh();
                    }
                }

                if (path.EndsWith(".mat"))
                {
                    var name = Path.GetFileNameWithoutExtension(path);

                    if (TVEUtils.HasLabel(path) || name.Contains("_Impostor"))
                    {
                        var material = AssetDatabase.LoadAssetAtPath<Material>(path);

                        TVEUtils.SetMaterialSettings(material);
                    }
                }

                if (path.EndsWith(".tvecollection"))
                {
                    EditorApplication.ExecuteMenuItem("Window/BOXOPHOBIC/The Visual Engine/Collection Manager");
                }
            }
        }
#endif
    }
}

#endif
#endif