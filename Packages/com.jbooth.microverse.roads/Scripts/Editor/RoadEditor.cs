using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.Splines;
using UnityEditor.Splines;

using UnityEditor.Build;
using UnityEditor.Build.Reporting;
using Unity.Mathematics;
using UnityEditor.EditorTools;

namespace JBooth.MicroVerseCore
{
    class RoadAssetProcessor : AssetModificationProcessor
    {
        public static string[] OnWillSaveAssets(string[] paths)
        {
            RoadSystem[] systems = Resources.FindObjectsOfTypeAll<RoadSystem>();
            foreach (var s in systems)
            {
                if (s.generateAtLoad == false)
                {
                    var roads = s.GetComponentsInChildren<Road>();
                    foreach (var r in roads)
                    {
                        MeshCollider[] colliders = r.GetComponentsInChildren<MeshCollider>();
                        foreach (var c in colliders)
                        {
                            c.enabled = true;
                        }
                    }
                }
            }
            return paths;
        }
    }

    class RoadBuildProcessing : IProcessSceneWithReport
    {
        public int callbackOrder { get { return 0; } }
        public void OnProcessScene(UnityEngine.SceneManagement.Scene scene, BuildReport report)
        {
            RoadSystem[] systems = Resources.FindObjectsOfTypeAll<RoadSystem>();
            foreach (var s in systems)
            {
                if (s.generateAtLoad == false)
                {
                    var roads = s.GetComponentsInChildren<Road>();
                    foreach (var r in roads)
                    {
                        r.Generate(s, false);
                        MeshCollider[] colliders = r.GetComponentsInChildren<MeshCollider>();
                        foreach (var c in colliders)
                        {
                            c.enabled = true;
                        }
                    }
                }
            }

        }
    }



    [CustomEditor(typeof(Road))]
    public class RoadEditor : Editor
    {
        static GUIStyle _boxStyle;

        public static GUIStyle boxStyle
        {
            get
            {
                if (_boxStyle == null)
                {
                    _boxStyle = new GUIStyle(EditorStyles.helpBox);
                    _boxStyle.normal.textColor = GUI.skin.label.normal.textColor;
                    _boxStyle.fontStyle = FontStyle.Bold;
                    _boxStyle.fontSize = 11;
                    _boxStyle.alignment = TextAnchor.UpperLeft;
                }
                return _boxStyle;
            }
        }

        static void SelectSpline(SplineContainer spline)
        {
            if (spline != null)
            {
                Selection.activeGameObject = spline.gameObject;
                ActiveEditorTracker.sharedTracker.RebuildIfNecessary();
                ToolManager.SetActiveContext<SplineToolContext>();
                ToolManager.SetActiveTool<SplineMoveTool>();
                //EditorSplineUtility.SetKnotPlacementTool();
            }
        }

        /*
        static GizmoType splineEditMode = GizmoType.Pickable | GizmoType.Selected | GizmoType.Active | GizmoType.InSelectionHierarchy;

        [DrawGizmo(GizmoType.Active | GizmoType.Pickable | GizmoType.NonSelected | GizmoType.Selected)]
        static void RenderExtraGizmo(Road editorTarget, GizmoType gizmoType)
        {
            if ((gizmoType & GizmoType.Pickable) == GizmoType.Pickable || gizmoType == splineEditMode)
            {
                SelectSpline(editorTarget.splineContainer);
            }

            SplineContainer splineContainer = editorTarget.splineContainer;

            if (splineContainer == null)
                return;

            Color prevColor = Gizmos.color;
            Gizmos.color = new Color(0, 0, 1, 0.5f);
            foreach (Spline spline in splineContainer.Splines)
            {
                if (spline.Count > 2)
                {
                    for (int i = 1; i < spline.Count - 1; ++i)
                    {
                        Gizmos.DrawSphere(splineContainer.transform.TransformPoint(spline[i].Position), 1.0f);
                    }

                }
                else if (spline.Count > 1)
                {
                    var position = spline.EvaluatePosition(0.5f);
                    Gizmos.DrawSphere(splineContainer.transform.TransformPoint(position), 1.0f);
                }
            }
            Gizmos.color = prevColor;
        }
        */

        private bool HasFrameBounds()
        {
            return target != null && ((Road)target).splineContainer != null;
        }

        private Bounds OnGetFrameBounds()
        {
            Road r = (Road)target;

            Bounds localSpaceBounds = r.splineContainer.Spline.GetBounds();

            Bounds worldSpaceBounds = new Bounds
            {
                center = r.transform.TransformPoint(localSpaceBounds.center),
                size = r.transform.TransformVector(localSpaceBounds.size)
            };

            return worldSpaceBounds;
        }

        public static void DrawChoiceGUI(Object owner, Road.SplineChoiceData curChoiceData, RoadConfig config, bool isPnt)
        {
            if (curChoiceData == null || config == null)
            {
                return;
            }

            
            EditorGUILayout.Space();
            using (new GUILayout.VerticalScope(GUI.skin.box))
            {
                if (isPnt)
                {
                    GUILayout.Box("Node : " + SplineRoadDataHandles.curChoiceIndex, boxStyle);
                }
                else
                {
                    EditorGUILayout.BeginHorizontal(boxStyle);
                    {
                        GUILayout.Label("Default", EditorStyles.miniBoldLabel);
                        GUILayout.FlexibleSpace();
                        if (GUILayout.Button(new GUIContent($"{config.name} >", "The used road config. Click to ping the config"), EditorStyles.miniBoldLabel))
                        {
                            EditorGUIUtility.PingObject(config);
                        }
                    }
                    EditorGUILayout.EndHorizontal();
                }

                EditorGUI.indentLevel++;
  
                List<string> prefabs = new List<string>(8);
                prefabs.Add("<Default>");

                bool foundCurrent = false;
                int currentIndex = 0;
                for (int i = 0; i < config.entries.Length; ++i)
                {
                    var entry = config.entries[i];
                    if (entry.prefab != null)
                    {
                        prefabs.Add(entry.prefab.name);
                        if (entry.prefab == curChoiceData.roadPrefab)
                        {
                            foundCurrent = true;
                            currentIndex = i + 1;
                        }
                    }
                    else
                    {
                        prefabs.Add("<Missing>");
                    }
                }
                if (!foundCurrent)
                {
                    curChoiceData.roadPrefab = null;
                }

                int newSelected = EditorGUILayout.Popup("Main", currentIndex, prefabs.ToArray());
                if (newSelected != currentIndex)
                {
                    currentIndex = newSelected;
                    if (newSelected == 0)
                    {
                        curChoiceData.roadPrefab = null;
                    }
                    else
                    {
                        curChoiceData.roadPrefab = config.entries[newSelected - 1].prefab;
                    }
                    EditorUtility.SetDirty(owner);
                }
                // shift out of 1 based array, since default is 0
                currentIndex -= 1;
                if (currentIndex < 0)
                    currentIndex = 0;

                var cfgEntry = config.entries[currentIndex];

                // clear layers that no longer exist in the config
                for (int i = 0; i < curChoiceData.overlayEntries.Count; ++i)
                {
                    var o = curChoiceData.overlayEntries[i];
                    if (config.FindOverlay(cfgEntry, o.label) == null)
                    {
                        curChoiceData.overlayEntries.RemoveAt(i);
                        i--;
                    }
                }
                // clear entries which don't have data in the config
                for (int i = 0; i < curChoiceData.overlayEntries.Count; ++i)
                {
                    var o = curChoiceData.overlayEntries[i];
                    if (config.FindOverlay(cfgEntry, o.label, o.prefab) == null)
                    {
                        curChoiceData.overlayEntries.RemoveAt(i);
                        i--;
                    }
                }

                var overlayEntries = config.GetAllOverlays(cfgEntry);
                foreach (var overlays in overlayEntries)
                {
                    // make sure we have an entry for this layer
                    var curOverlay = curChoiceData.FindOverlayEntry(overlays.label);
                    if (curOverlay == null)
                    {
                        curOverlay = new Road.OverlayEntry() { label = overlays.label, prefab = null };
                        curChoiceData.overlayEntries.Add(curOverlay);
                    }


                    int overlayIndex = 0;

                    // create option list
                    List<string> overlayNames = new List<string>();
                    overlayNames.Add("<Default>");
                    overlayNames.Add("<None>");

                    for (int i = 0; i < overlays.prefabs.Length; ++i)
                    {
                        if (overlays.prefabs[i] != null)
                        {
                            overlayNames.Add(overlays.prefabs[i].name);
                            if (overlays.prefabs[i] == curOverlay.prefab)
                            {
                                overlayIndex = i + 2;
                            }
                        }
                        else
                        {
                            overlayNames.Add("<Missing>");
                        }
                    }
                    if (overlayIndex == 0 && curOverlay.none == true)
                    {
                        overlayIndex = 1;
                    }

                    int newOverlay = EditorGUILayout.Popup(overlays.label, overlayIndex, overlayNames.ToArray());
                    if (newOverlay != overlayIndex)
                    {
                        if (newOverlay == 0)
                        {
                            curOverlay.prefab = null;
                            curOverlay.none = false;
                        }
                        else if (newOverlay == 1)
                        {
                            curOverlay.prefab = null;
                            curOverlay.none = true;
                        }
                        else
                        {
                            curOverlay.prefab = overlays.prefabs[newOverlay - 2];
                            curOverlay.label = overlays.label;
                        }
                        EditorUtility.SetDirty(owner);
                    }
                }
                EditorGUI.indentLevel--;
            }
        }

        void RemoveRoll(Road r)
        {
            for (int i = 1; i < r.splineContainer[0].Count-1; ++i)
            {
                var knot = r.splineContainer[0][i];
                r.splineContainer[0].SetTangentModeNoNotify(i, TangentMode.Continuous);
                Quaternion rotation = knot.Rotation;
                Vector3 eulerAngles = rotation.eulerAngles;
                rotation = Quaternion.Euler(eulerAngles.x, eulerAngles.y, 0f);
                knot.Rotation = rotation;
                r.splineContainer[0].SetKnotNoNotify(i, knot);
            }
            //r.UpdateConnections(r.GetComponentInParent<RoadSystem>(), false);
        }

        public override void OnInspectorGUI()
        {
            EditorGUI.BeginChangeCheck();
            serializedObject.Update();
            Road r = (Road)target;
            bool inSetup = (r != null && r.GetComponentInParent<RoadSystem>() == null);
            if (inSetup)
            {
                EditorGUILayout.PropertyField(serializedObject.FindProperty("splineContainer"));
                EditorGUILayout.PropertyField(serializedObject.FindProperty("config"));
            }
            else
            {
                EditorGUI.BeginChangeCheck();
                var rs = r.GetComponentInParent<RoadSystem>();
                EditorGUILayout.PropertyField(serializedObject.FindProperty("modifiesTerrain"));
                if (EditorGUI.EndChangeCheck())
                {
                    serializedObject.ApplyModifiedProperties();
                    if (rs != null) rs.UpdateAll();
                }
                EditorGUILayout.PropertyField(serializedObject.FindProperty("allowRoll"));
                
                if (rs != null && rs.systemConfig != null && rs.systemConfig.allowShaping)
                {
                    EditorGUILayout.PropertyField(serializedObject.FindProperty("splineShapes"));
                }
            }
            serializedObject.ApplyModifiedProperties();
          
            if (!inSetup && r.splineContainer != null && r.splineContainer.Splines.Count > 0)
            {
                EditorGUILayout.BeginVertical(boxStyle);
                if (GUILayout.Button("Remove Roll from Spline"))
                {
                    Undo.RecordObject(r.splineContainer, "Remove Roll");
                    RemoveRoll(r);
                }
                if (GUILayout.Button("Average Heights"))
                {
                    Undo.RecordObject(r.splineContainer, "Average Heights");
                    var spline = r.splineContainer[0];
                    float height0 = spline[0].Position.y;
                    float height1 = spline[1].Position.y;

                    for (int i = 1; i < spline.Count-1; ++i)
                    {
                        float t = SplineUtility.GetNormalizedInterpolation(spline, i, PathIndexUnit.Knot);
                        var knot = spline[i];
                        var pos = knot.Position;
                        pos.y = math.lerp(height0, height1, t);
                        knot.Position = pos;
                        spline.SetKnot(i, knot);
                    }
                }
                

                EditorGUILayout.EndVertical();
            }
            
            
            var curChoiceData = SplineRoadDataHandles.curChoiceData;
            bool isPnt = curChoiceData != null;
            if (curChoiceData == null)
            {
                curChoiceData = r.defaultChoiceData;
            }

            DrawChoiceGUI(r, curChoiceData, r.config, isPnt);
            if (EditorGUI.EndChangeCheck() || SplineRoadDataHandles.changed)
            {
                SplineRoadDataHandles.changed = false;
                r.Generate();
            }
            
        }

        private void OnEnable()
        {
            Spline.Changed += OnAfterSplineWasModified;
            EditorSplineUtility.RegisterSplineDataChanged<float2>(OnAfterSplineDataWasModified);
        }

        private void OnDisable()
        {
            Spline.Changed -= OnAfterSplineWasModified;
            EditorSplineUtility.UnregisterSplineDataChanged<float2>(OnAfterSplineDataWasModified);
        }

        void OnAfterSplineDataWasModified(SplineData<float2> splineData)
        {
            var road = target as Road;

            if (road == null) return;
            if (MicroVerse.instance == null) return;
            if (!MicroVerse.instance.enabled) return;
            if (!road.enabled) return;

            foreach (var sw in road.splineShapes)
            {
                if (splineData == sw.shapeData)
                {
                    EditorUtility.SetDirty(road);
                    road.Generate();
                }
            }
        }

        void RefreshRoad()
        {
            var road = (Road)target;
            Spline.Changed -= OnAfterSplineWasModified;
            if (road.allowRoll == false)
            {
                RemoveRoll(road);
            }
            road.UpdateConnections(road.GetComponentInParent<RoadSystem>(), true);
            road.Generate();
            Spline.Changed += OnAfterSplineWasModified;
        }

        private void OnAfterSplineWasModified(Spline spline, int arg2, SplineModification arg3)
        {
            var road = target as Road;
            if (road != null && road.splineContainer != null && road.splineContainer.Splines != null)
            {
                foreach (var s in road.splineContainer.Splines)
                {
                    if (ReferenceEquals(spline, s))
                    {
                        RefreshRoad();
                    }
                }
            }
        }

    }
}
