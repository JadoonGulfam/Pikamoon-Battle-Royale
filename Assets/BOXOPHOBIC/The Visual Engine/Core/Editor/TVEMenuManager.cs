using UnityEditor;
using UnityEngine;
using UnityEditor.SceneManagement;

namespace TheVisualEngine
{
    public static class TVEWindowMenus
    {
        [MenuItem("Window/BOXOPHOBIC/The Visual Engine/Discord Server", false, 8000)]
        public static void Discord()
        {
            Application.OpenURL("https://discord.com/invite/znxuXET");
        }

        [MenuItem("Window/BOXOPHOBIC/The Visual Engine/Publisher Page", false, 8001)]
        public static void AssetStore()
        {
            Application.OpenURL("https://assetstore.unity.com/publishers/20529");
        }

        [MenuItem("Window/BOXOPHOBIC/The Visual Engine/Documentation", false, 8002)]
        public static void Documentation()
        {
            Application.OpenURL("https://docs.google.com/document/d/1ofHGsicGeyvCQTCky4ec5q96Ttaxub_PuuJ0YEoFpWk");
        }

        [MenuItem("Window/BOXOPHOBIC/The Visual Engine/Changelog", false, 8005)]
        public static void Chnagelog()
        {
            Application.OpenURL("https://docs.google.com/document/d/1ofHGsicGeyvCQTCky4ec5q96Ttaxub_PuuJ0YEoFpWk/edit?pli=1#heading=h.1rbujejuzjce");
        }

        [MenuItem("Window/BOXOPHOBIC/The Visual Engine/Write A Review", false, 9999)]
        public static void WriteAReview()
        {
            Application.OpenURL("https://assetstore.unity.com/packages/tools/utilities/the-visual-engine-286827#reviews");
        }
    }

    public static class TVEHierarchyMenus
    {
        private static float lastMenuCallTimestamp = 0f;

        [MenuItem("GameObject/BOXOPHOBIC/The Visual Engine/Manager", false, 6)]
        static void CreateManager()
        {
            GameObject manager = new GameObject();
            manager.AddComponent<TVEManager>();

            EditorSceneManager.MarkSceneDirty(EditorSceneManager.GetActiveScene());
        }

        [MenuItem("GameObject/BOXOPHOBIC/The Visual Engine/Element", false, 7)]
        static void CreateElement()
        {
            if (Time.unscaledTime.Equals(lastMenuCallTimestamp)) 
                return;

            if (Selection.gameObjects != null && Selection.gameObjects.Length > 0)
            {
                for (int i = 0; i < Selection.gameObjects.Length; i++)
                {
                    var selection = Selection.gameObjects[i];

                    if (selection.activeInHierarchy)
                    {
                        GameObject element = MonoBehaviour.Instantiate(Resources.Load<GameObject>("Internal Element"));

                        if (EditorSceneManager.IsPreviewSceneObject(element))
                        {
                            Debug.Log("<b>[The Visual Engine]</b> " + "Elements cannot be created inside prefabs");
                            return;
                        }

                        element.AddComponent<TVEElement>();
                        element.name = "Element";

                        if (selection.GetComponent<Terrain>() != null)
                        {
                            var terrain = selection.GetComponent<Terrain>();

                            var position = terrain.transform.position;
                            var bounds = terrain.terrainData.bounds;
                            element.transform.localPosition = new Vector3(bounds.center.x + position.x, bounds.min.y + position.y, bounds.center.z + position.z);
                            element.transform.localScale = new Vector3(terrain.terrainData.size.x, 1, terrain.terrainData.size.z);
                            element.transform.parent = selection.transform;

                            element.GetComponent<TVEElement>().terrainData = terrain;

                            element.name = "Element (Terrain)";

                            var blanketShader = Shader.Find("BOXOPHOBIC/The Visual Engine/Elements/Form Surface (Terrain)");

                            if (blanketShader != null)
                            {
                                element.GetComponent<Renderer>().sharedMaterial.shader = blanketShader;
                                element.name = "Element (Surface)";
                            }
                        }
                        else
                        {
                            element.transform.parent = selection.transform;
                        }
                    }
                }
            }
            else
            {
                GameObject element = MonoBehaviour.Instantiate(Resources.Load<GameObject>("Internal Element"));

                if (EditorSceneManager.IsPreviewSceneObject(element))
                {
                    Debug.Log("<b>[The Visual Engine]</b> " + "Elements cannot be created inside prefabs");
                    return;
                }

                element.AddComponent<TVEElement>();
                element.name = "Element";

                var sceneCamera = SceneView.lastActiveSceneView.camera;

                if (sceneCamera != null)
                {
                    element.transform.position = sceneCamera.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 10f));
                }
                else
                {
                    element.transform.localPosition = Vector3.zero;
                    element.transform.localEulerAngles = Vector3.zero;
                    element.transform.localScale = Vector3.one;
                }

                Selection.activeGameObject = element;
            }

            EditorSceneManager.MarkSceneDirty(EditorSceneManager.GetActiveScene());

            lastMenuCallTimestamp = Time.unscaledTime;
        }

        [MenuItem("GameObject/BOXOPHOBIC/The Visual Engine/Element (Trail)", false, 8)]
        static void CreateElementTrail()
        {
            if (Time.unscaledTime.Equals(lastMenuCallTimestamp))
                return;

            if (Selection.gameObjects != null && Selection.gameObjects.Length > 0)
            {
                for (int i = 0; i < Selection.gameObjects.Length; i++)
                {
                    var selection = Selection.gameObjects[i];

                    if (selection.activeInHierarchy)
                    {
                        GameObject element = MonoBehaviour.Instantiate(Resources.Load<GameObject>("Internal Element Trail"));

                        if (EditorSceneManager.IsPreviewSceneObject(element))
                        {
                            Debug.Log("<b>[The Visual Engine]</b> " + "Elements cannot be created inside prefabs");
                            return;
                        }

                        element.AddComponent<TVEElement>();
                        element.name = "Element (Trail)";

                        element.transform.parent = selection.transform;
                    }
                }
            }
            else
            {
                GameObject element = MonoBehaviour.Instantiate(Resources.Load<GameObject>("Internal Element Trail"));

                if (EditorSceneManager.IsPreviewSceneObject(element))
                {
                    Debug.Log("<b>[The Visual Engine]</b> " + "Elements cannot be created inside prefabs");
                    return;
                }

                element.AddComponent<TVEElement>();
                element.name = "Element (Trail)";

                var sceneCamera = SceneView.lastActiveSceneView.camera;

                if (sceneCamera != null)
                {
                    element.transform.position = sceneCamera.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 10f));
                }
                else
                {
                    element.transform.localPosition = Vector3.zero;
                    element.transform.localEulerAngles = Vector3.zero;
                    element.transform.localScale = Vector3.one;
                }

                Selection.activeGameObject = element;
            }

            EditorSceneManager.MarkSceneDirty(EditorSceneManager.GetActiveScene());

            lastMenuCallTimestamp = Time.unscaledTime;
        }

        [MenuItem("GameObject/BOXOPHOBIC/The Visual Engine/Element (Particle)", false, 9)]
        static void CreateElementParticle()
        {
            if (Time.unscaledTime.Equals(lastMenuCallTimestamp))
                return;

            if (Selection.gameObjects != null && Selection.gameObjects.Length > 0)
            {
                for (int i = 0; i < Selection.gameObjects.Length; i++)
                {
                    var selection = Selection.gameObjects[i];

                    if (selection.activeInHierarchy)
                    {
                        GameObject element = MonoBehaviour.Instantiate(Resources.Load<GameObject>("Internal Element Particle"));

                        if (EditorSceneManager.IsPreviewSceneObject(element))
                        {
                            Debug.Log("<b>[The Visual Engine]</b> " + "Elements cannot be created inside prefabs");
                            return;
                        }

                        element.AddComponent<TVEElement>();
                        element.name = "Element (Particle)";

                        element.transform.parent = selection.transform;
                    }
                }
            }
            else
            {
                GameObject element = MonoBehaviour.Instantiate(Resources.Load<GameObject>("Internal Element Particle"));

                if (EditorSceneManager.IsPreviewSceneObject(element))
                {
                    Debug.Log("<b>[The Visual Engine]</b> " + "Elements cannot be created inside prefabs");
                    return;
                }

                element.AddComponent<TVEElement>();
                element.name = "Element (Particle)";

                var sceneCamera = SceneView.lastActiveSceneView.camera;

                if (sceneCamera != null)
                {
                    element.transform.position = sceneCamera.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 10f));
                }
                else
                {
                    element.transform.localPosition = Vector3.zero;
                    element.transform.localEulerAngles = Vector3.zero;
                    element.transform.localScale = Vector3.one;
                }

                Selection.activeGameObject = element;
            }

            EditorSceneManager.MarkSceneDirty(EditorSceneManager.GetActiveScene());

            lastMenuCallTimestamp = Time.unscaledTime;
        }

        //[MenuItem("GameObject/BOXOPHOBIC/The Visual Engine/Volume", false, 8)]
        //static void CreateVolume()
        //{
        //    GameObject volume = new GameObject();
        //    volume.AddComponent<OOVolume>();
        //    volume.name = "Volume";

        //    var sceneCamera = SceneView.lastActiveSceneView.camera;

        //    if (sceneCamera != null)
        //    {
        //        volume.transform.position = sceneCamera.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 10f));
        //    }
        //    else
        //    {
        //        volume.transform.localPosition = Vector3.zero;
        //        volume.transform.localEulerAngles = Vector3.zero;
        //    }

        //    volume.transform.localScale = new Vector3(40, 40, 40);

        //    Selection.activeGameObject = volume;

        //    EditorSceneManager.MarkSceneDirty(EditorSceneManager.GetActiveScene());
        //}
    }

    [InitializeOnLoad]
    class TVEHierarchyIcons
    {
        static TVEHierarchyIcons()
        {
            EditorApplication.hierarchyWindowItemOnGUI += HierarchyItemCB;
            EditorApplication.projectWindowItemOnGUI += ProjectItemCB;
        }

        static void HierarchyItemCB(int id, Rect rect)
        {
            int showIcons = Shader.GetGlobalInt("TVE_ShowIcons");

            if (showIcons == 0)
            {
                return;
            }

            Rect iconRect = new Rect(32, rect.y + 1, 2, 14);

            GameObject go = EditorUtility.InstanceIDToObject(id) as GameObject;

            if (go != null && go.GetComponent<TVEPrefab>() != null)
            {
                EditorGUI.DrawRect(iconRect, new Color(1f, 0.8f, 0.4f));
            }
        }

        static void ProjectItemCB(string guid, Rect selectionRect)
        {
            int showIcons = Shader.GetGlobalInt("TVE_ShowIcons");

            if (showIcons == 0)
            {
                return;
            }

            Rect iconRect = new Rect(8, selectionRect.y + 1, 2, 14);

            var assetPath = AssetDatabase.GUIDToAssetPath(guid);

            GameObject go = AssetDatabase.LoadAssetAtPath<GameObject>(assetPath);

            if (go != null && go.GetComponent<TVEPrefab>() != null)
            {
                EditorGUI.DrawRect(iconRect, new Color(1f, 0.8f, 0.4f));
            }
        }
    }
}

