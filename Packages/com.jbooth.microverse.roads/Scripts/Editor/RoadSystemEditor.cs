using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace JBooth.MicroVerseCore
{

    class RoadSystemPostProcessor : AssetPostprocessor
    {
        static void OnPostprocessAllAssets(string[] importedAssets, string[] deletedAssets, string[] movedAssets, string[] movedFromAssetPaths)
        {
            Road.ClearCache();
        }
    }

    [CustomEditor(typeof(RoadSystem))]
    public class RoadSystemEditor : Editor
    {
        /// <summary>
        /// Preview thumbnail size. Width and height.
        /// </summary>
        private static int cellSize = 96;

        private List<GUIContent> templateMatContent = new List<GUIContent>();
        private List<Material> templateMatMaterials = new List<Material>();
        private List<RoadMaterialConfig> roadMaterialConfigs = new List<RoadMaterialConfig>();

        private int customIndex = 0;

        public static List<T> LoadAllInstances<T>() where T : ScriptableObject
        {
            var ret = AssetDatabase.FindAssets($"t: {typeof(T).Name}").ToList()
                        .Select(AssetDatabase.GUIDToAssetPath)
                        .Select(AssetDatabase.LoadAssetAtPath<T>)
                        .ToList();

            return ret;


        }

        private void LoadAllPreviews()
        {
            RoadSystem rs = (target as RoadSystem);
            
            templateMatContent.Clear();
            templateMatMaterials.Clear();

            roadMaterialConfigs = LoadAllInstances<RoadMaterialConfig>();
            foreach (var i in roadMaterialConfigs)
            {
                if (i.contentID == rs.contentID)
                {
                    foreach (var m in i.templateMaterials)
                    {
                        if (m != null && m.material != null)
                        {
                            templateMatContent.Add(new GUIContent(m.material.name, m.preview != null ? m.preview : AssetPreview.GetAssetPreview(m.material)));
                            templateMatMaterials.Add(m.material);
                        }
                    }
                }
            }
        }


        Vector2 matScroll;
        int matSelection = 0;
        public override void OnInspectorGUI()
        {
            serializedObject.Update();
            var rs = ((RoadSystem)target);

            // load all available template materials
            // they need to be available when we find the material in the content collection
            LoadAllPreviews();

            EditorGUI.BeginChangeCheck();

            EditorGUILayout.PropertyField(serializedObject.FindProperty("hideGameObjects"));
            EditorGUILayout.PropertyField(serializedObject.FindProperty("generateAtLoad"));
            EditorGUILayout.PropertyField(serializedObject.FindProperty("systemConfig"));

            if (rs.templateMaterial != null)
            {
                matSelection = templateMatMaterials.IndexOf(rs.templateMaterial);
            }

            // config selector
            // legacy: EditorGUILayout.PropertyField(serializedObject.FindProperty("contentID"));
            List<string> configSelection = roadMaterialConfigs.Select(x => x.contentID).ToList();
            configSelection.Insert(customIndex, "Custom");

            string[] names = configSelection.ToArray();

            int selectionIndex = names.ToList().IndexOf(rs.contentID);

            if (rs.contentID == null || selectionIndex < 0)
                selectionIndex = customIndex;

            using (var check = new EditorGUI.ChangeCheckScope())
            {
                selectionIndex = EditorGUILayout.Popup("Content ID", selectionIndex, names);

                if (check.changed)
                {
                    // none
                    if(selectionIndex == customIndex)
                    {
                        rs.contentID = null;
                    }
                    // road configs
                    else
                    {
                        int configIndex = selectionIndex - 1; // consider <none> as first list item
                        rs.contentID = roadMaterialConfigs[configIndex].contentID;
                    }

                    // rs.templateMaterial = null;
                    matSelection = -1; // implicitly select first; see also clamp in selectiongrid
                }
            }

            // enable material change option only if custom is selected
            GUI.enabled = selectionIndex == customIndex;
            EditorGUILayout.PropertyField(serializedObject.FindProperty("templateMaterial"));
            GUI.enabled = true;

            // calculate editor preview dimensions
            float safetyMargin = cellSize / 2f; // just some margin to keep the preview almost fully visible
            float gridWidth = EditorGUIUtility.currentViewWidth - safetyMargin;
            int columnCount = Mathf.FloorToInt(gridWidth / cellSize);

            // prevent div by zero
            if (columnCount == 0)
                columnCount = 1;

            // show selection grid
            var nms = GUIUtil.SelectionGrid(matSelection, ref matScroll, templateMatContent.ToArray(), cellSize, columnCount, true);
            if (nms != -1 && nms != matSelection && templateMatMaterials.Count > nms && templateMatMaterials[nms] != null)
            {
                matSelection = nms;
                rs.templateMaterial = templateMatMaterials[nms];
                EditorUtility.SetDirty(rs);
            }

            if (EditorGUI.EndChangeCheck())
            {
                rs.UpdateAll();
                serializedObject.ApplyModifiedProperties();
            }

            GUILayout.BeginHorizontal();
            {
                EditorGUILayout.PrefixLabel(" ");
                if (GUILayout.Button("Update System"))
                {
                    Road[] roads = FindObjectsOfType<Road>();
                    foreach (var r in roads)
                    {
                        r.UpdateConnections(r.GetComponentInParent<RoadSystem>(), true);
                    }
                    rs.ReGenerateRoads();
                    rs.UpdateAll();
                }
            }
            GUILayout.EndHorizontal();
            
        }

        

    }
}
