// Cristian Pop - https://boxophobic.com/

using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using Boxophobic.StyledGUI;
using System.IO;
using UnityEngine.SceneManagement;
using UnityEditor.SceneManagement;

namespace TheVisualEngine
{
    public class TVEMaterialUpgrader : EditorWindow
    {
        float GUI_HALF_EDITOR_WIDTH = 220;

        List<string> activeScenePaths;

        bool checkAllProjectMaterials = true;

        bool requiresSceneRestart = false;
        bool validMaterialsUpgraded = false;

        bool showAdditionalSettings = false;

        Color bannerColor;
        string bannerText;
        static TVEMaterialUpgrader window;
        //Vector2 scrollPosition = Vector2.zero;

        [MenuItem("Window/BOXOPHOBIC/The Visual Engine/Material Upgrader", false, 2007)]
        public static void ShowWindow()
        {
            window = GetWindow<TVEMaterialUpgrader>(false, "Material Upgrader", true);
            window.minSize = new Vector2(600, 300);
        }

        void OnEnable()
        {
            bannerColor = new Color(0.890f, 0.745f, 0.309f);
            bannerText = "Material Upgrader";
        }

        void OnGUI()
        {
            GUI_HALF_EDITOR_WIDTH = this.position.width / 2.0f;

            StyledGUI.DrawWindowBanner(bannerColor, bannerText);

            GUILayout.BeginHorizontal();
            GUILayout.Space(10);
            GUILayout.BeginVertical();

            if (EditorApplication.isCompiling)
            {
                GUI.enabled = false;
            }
            else
            {
                GUI.enabled = true;
            }

            if (validMaterialsUpgraded)
            {
                EditorGUILayout.HelpBox("All processed materials have been upgraded! You can run the upgrader at any time if needed!", MessageType.Info, true);

                GUILayout.Space(15);

                GUILayout.BeginHorizontal();
                GUILayout.Label("Show Additional Settings", GUILayout.Width(GUI_HALF_EDITOR_WIDTH - 100));
                showAdditionalSettings = EditorGUILayout.Toggle(showAdditionalSettings);
                GUILayout.EndHorizontal();

                GUILayout.Space(15);

                if (showAdditionalSettings)
                {
                    if (GUILayout.Button("Restart Material Upgrading", GUILayout.Height(24)))
                    {
                        validMaterialsUpgraded = false;
                    }
                }
            }
            else
            {
                EditorGUILayout.HelpBox("Material upgrading is required when switching render pipelines or when upgrading to a newer version. Run the Material Upgrader after all modules are installed!", MessageType.Info, true);

                if (!checkAllProjectMaterials)
                {
                    EditorGUILayout.HelpBox("When Check All Project Materials is disabled, the upgrader will only check materials containing TVE Material in their name!",MessageType.Warning, true);
                }

                GUILayout.Space(15);

                GUILayout.BeginHorizontal();
                GUILayout.Label(new GUIContent("Check All Project Materials", "When enabled, the upgrader checks all project materials regardless of naming convention."), GUILayout.Width(GUI_HALF_EDITOR_WIDTH - 100));
                checkAllProjectMaterials = EditorGUILayout.Toggle(checkAllProjectMaterials);
                GUILayout.EndHorizontal();

                GUILayout.Space(15);

                if (GUILayout.Button("Upgrade Project Materials", GUILayout.Height(24)))
                {
                    GetCurrentScenesSaving();
                    UpgradeMaterials();

                    //if (upgradeAllProjectAssets)
                    //{
                    //    UpgradeModels();
                    //}

                    RestartActiveScenes();

                    validMaterialsUpgraded = true;

                    GUIUtility.ExitGUI();
                }
            }

            GUILayout.EndVertical();
            GUILayout.Space(10);
            GUILayout.EndHorizontal();
            GUILayout.Space(15);
        }

        void UpgradeMaterials()
        {
            var allMaterialGUIDs = AssetDatabase.FindAssets("t:material", null);

            int count = 0;

            if (checkAllProjectMaterials)
            {
                var materialCount = allMaterialGUIDs.Length;

                foreach (var guids in allMaterialGUIDs)
                {
                    var path = AssetDatabase.GUIDToAssetPath(guids);

                    if (EditorUtility.DisplayCancelableProgressBar("The Visual Engine", "Processing " + Path.GetFileNameWithoutExtension(path), (float)count * (1.0f / (float)materialCount)))
                    {
                        break;
                    }

                    TVEUtils.ValidateMaterial(path, true);

                    count++;
                }
            }
            else
            {
                var tveMaterialPaths = new List<string>();

                foreach (var asset in allMaterialGUIDs)
                {
                    var path = AssetDatabase.GUIDToAssetPath(asset);
                    var name = Path.GetFileName(path);

                    if ((name.Contains("TVE") && name.Contains("Material")) || (name.Contains("TVE") && name.Contains("Element")) || name.Contains("_Impostor"))
                    {
                        tveMaterialPaths.Add(path);
                    }
                }

                var materialCount = tveMaterialPaths.Count;

                foreach (var path in tveMaterialPaths)
                {
                    if (EditorUtility.DisplayCancelableProgressBar("The Visual Engine", "Processing " + Path.GetFileNameWithoutExtension(path), (float)count * (1.0f / (float)materialCount)))
                    {
                        break;
                    }

                    TVEUtils.ValidateMaterial(path, true);

                    count++;
                }
            }

            EditorUtility.ClearProgressBar();
            Debug.Log("<b>[The Visual Engine]</b> " + count + " project materials have been checked and upgraded!");
        }

        //void UpgradeModels()
        //{
        //    var allPrefabGUIDs = AssetDatabase.FindAssets("t:prefab", null);

        //    int count = 0;
        //    int valid = 0;

        //    foreach (var guids in allPrefabGUIDs)
        //    {
        //        bool isValidUpgrade = false;

        //        var prefabPath = AssetDatabase.GUIDToAssetPath(guids);

        //        if (TVEUtils.HasLabel(prefabPath))
        //        {
        //            continue;
        //        }

        //        var prefabObject = AssetDatabase.LoadAssetAtPath<GameObject>(prefabPath);

        //        List<GameObject> gameObjectsInPrefab = new();
        //        TVEUtils.GetChildRecursive(prefabObject, gameObjectsInPrefab);

        //        foreach (var gameObject in gameObjectsInPrefab)
        //        {
        //            if (gameObject.GetComponent<MeshFilter>() == null)
        //            {
        //                continue;
        //            }

        //            var mesh = gameObject.GetComponent<MeshFilter>().sharedMesh;

        //            if (mesh == null)
        //            {
        //                continue;
        //            }

        //            if (mesh.name.Contains("Impostor"))
        //            {
        //                continue;
        //            }

        //            if (!mesh.name.Contains("TVE") && !mesh.name.Contains("Model"))
        //            {
        //                continue;
        //            }

        //            var meshPath = AssetDatabase.GetAssetPath(mesh);

        //            if (TVEUtils.HasLabel(meshPath))
        //            {
        //                continue;
        //            }

        //            if (Path.GetFullPath(meshPath).Length > 256)
        //            {
        //                Debug.Log("<b>[The Visual Engine]</b> " + meshPath + " could not be upgraded because the file path is too long! To fix the issue, rename the folders and file names, and press the Upgrade Assets button again!");
        //                continue;
        //            }

        //            List<Vector2> subMeshMotion = new();
        //            Material[] materials = null;

        //            if (gameObject.GetComponent<MeshRenderer>() != null)
        //            {
        //                materials = gameObject.GetComponent<MeshRenderer>().sharedMaterials;
        //            }

        //            if (materials != null)
        //            {
        //                for (int i = 0; i < materials.Length; i++)
        //                {
        //                    var material = materials[i];

        //                    subMeshMotion.Add(Vector2.zero);

        //                    if (material != null && material.HasProperty("_IsTVEShader") && !material.HasProperty("_IsLiteShader"))
        //                    {
        //                        isValidUpgrade = true;

        //                        var hasBranch = 0;
        //                        var hasLeaves = 0;

        //                        if (material.HasProperty("_MotionSmallIntensityValue") && material.GetFloat("_MotionSmallIntensityValue") > 0)
        //                        {
        //                            hasBranch = 1;
        //                        }

        //                        if (material.HasProperty("_MotionTinyIntensityValue") && material.GetFloat("_MotionTinyIntensityValue") > 0)
        //                        {
        //                            hasLeaves = 1;
        //                        }

        //                        subMeshMotion[i] = new Vector2(hasBranch, hasLeaves);

        //                        TVEUtils.UnloadMaterialFromMemory(material);
        //                    }
        //                }
        //            }

        //            if (isValidUpgrade)
        //            {
        //                valid++;
        //                TVEUtils.ValidateModel(meshPath, subMeshMotion, true);
        //            }

        //        }

        //        if (isValidUpgrade)
        //        {
        //            TVEUtils.SetLabel(prefabPath);
        //        }

        //        count++;

        //        EditorUtility.DisplayProgressBar("The Visual Engine", "Processing " + Path.GetFileName(prefabPath), (float)count * (1.0f / (float)allPrefabGUIDs.Length));
        //    }

        //    EditorUtility.ClearProgressBar();
        //    Debug.Log("<b>[The Visual Engine]</b> " + valid + " project models has been upgraded!");
        //}

        void GetCurrentScenesSaving()
        {
            activeScenePaths = new List<string>();

            for (int i = 0; i < SceneManager.sceneCount; i++)
            {
                var scene = SceneManager.GetSceneAt(i);
                activeScenePaths.Add(scene.path);
            }

            for (int i = 0; i < activeScenePaths.Count; i++)
            {
                var activeScenePath = activeScenePaths[i];

                if (activeScenePath == "")
                {
                    var saveScene = EditorUtility.DisplayDialog("Save Untitled Scene?", "The current scene is not saved to disk! Would you like to save it?", "Save New Scene", "No");

                    if (saveScene)
                    {
                        var currentScene = SceneManager.GetSceneByPath(activeScenePath);

                        var savePath = EditorUtility.SaveFilePanelInProject("Save Scene", "New Scene", "unity", "Save scene to disk!", "Assets");

                        if (savePath != "")
                        {
                            EditorSceneManager.SaveScene(currentScene, savePath);
                            AssetDatabase.SaveAssets();
                            AssetDatabase.Refresh();

                            activeScenePaths[i] = savePath;
                        }
                    }
                }
                else
                {
                    var currentScene = SceneManager.GetSceneByPath(activeScenePath);

                    if (currentScene.isDirty)
                    {
                        var saveScene = EditorUtility.DisplayDialog("Save Scene " + currentScene.name + "?", "The current scene is modified! Would you like to save it?", "Save Scene", "No");

                        if (saveScene)
                        {
                            EditorSceneManager.SaveScene(currentScene);
                            AssetDatabase.SaveAssets();
                            AssetDatabase.Refresh();
                        }
                    }
                }
            }

            EditorSceneManager.NewScene(NewSceneSetup.EmptyScene);
            requiresSceneRestart = true;
        }

        void RestartActiveScenes()
        {
            if (requiresSceneRestart)
            {
                for (int i = 0; i < activeScenePaths.Count; i++)
                {
                    var activeScenePath = activeScenePaths[i];

                    if (File.Exists(activeScenePath))
                    {
                        var scene = SceneManager.GetSceneByPath(activeScenePath);

                        if (i == 0)
                        {
                            EditorSceneManager.OpenScene(activeScenePath);
                        }
                        else
                        {
                            EditorSceneManager.OpenScene(activeScenePath, OpenSceneMode.Additive);
                        }

                        EditorSceneManager.SaveScene(scene);

                        AssetDatabase.SaveAssets();
                        AssetDatabase.Refresh();
                    }

                    requiresSceneRestart = false;
                }
            }
        }
    }
}
