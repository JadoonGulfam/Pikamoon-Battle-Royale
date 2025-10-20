#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;
using System.IO;

namespace IndieImpulseAssets
{
    [InitializeOnLoad]
    public class AddLayerOnImport
    {
        private const string scriptPath = "Assets/IndieImpulse_Assets/Panel UI Effects/AddLayerOnImport.cs";
        private const int maxRuns = 2;

        static AddLayerOnImport()
        {
            int runCount = PlayerPrefs.GetInt("AddLayerRunCount", 0);

            if (runCount < maxRuns)
            {
                runCount++;
                PlayerPrefs.SetInt("AddLayerRunCount", runCount);

                EditorApplication.delayCall += AddLayer;

                if (runCount == maxRuns)
                {
                    // Schedule the script deletion after a short delay, but only if running in the editor
                    if (Application.isEditor)
                    {
                        EditorApplication.delayCall += () => DeleteScript(scriptPath);
                    }
                }
            }
        }

        private static void AddLayer()
        {
            // Add your layer name here
            string layerName = "UIEffects";

            // Check if the layer already exists
            int existingLayerIndex = LayerMask.NameToLayer(layerName);
            if (existingLayerIndex != -1)
            {
                Debug.Log("Layer already exists: " + layerName);
                return;
            }

            // Find an available layer index
            int newLayerIndex = FindAvailableLayerIndex();
            if (newLayerIndex == -1)
            {
                Debug.LogError("Failed to add layer. All available layers are in use.");
                return;
            }

            // Add the new layer
            SerializedObject tagManager = new SerializedObject(AssetDatabase.LoadAllAssetsAtPath("ProjectSettings/TagManager.asset")[0]);
            SerializedProperty layersProp = tagManager.FindProperty("layers");
            layersProp.GetArrayElementAtIndex(newLayerIndex).stringValue = layerName;

            // Apply changes to the TagManager.asset file
            tagManager.ApplyModifiedProperties();

            Debug.Log("Layer added: " + layerName);
        }

        private static void DeleteScript(string scriptPath)
        {
            // Delete the script file
            File.Delete(scriptPath);

            // Refresh the AssetDatabase to reflect the changes
            AssetDatabase.Refresh();

            Debug.Log("Script deleted: " + scriptPath);
        }

        private static int FindAvailableLayerIndex()
        {
            SerializedObject tagManager = new SerializedObject(AssetDatabase.LoadAllAssetsAtPath("ProjectSettings/TagManager.asset")[0]);
            SerializedProperty layersProp = tagManager.FindProperty("layers");

            for (int i = 8; i < layersProp.arraySize; i++) // Start at index 8 (user layers)
            {
                SerializedProperty layerProp = layersProp.GetArrayElementAtIndex(i);
                if (string.IsNullOrEmpty(layerProp.stringValue))
                {
                    return i;
                }
            }

            return -1; // No available layer index found
        }
    }
}
#endif