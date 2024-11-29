using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.EditorTools;
using UnityEditor.Splines;
using System;
using System.Reflection;
using System.Linq;
using UnityEngine.Splines;
using Unity.Mathematics;

namespace JBooth.MicroVerseCore
{
    public class DragHandles
    {
        // internal state for DragHandle()
        static int s_DragHandleHash = "DragHandleHash".GetHashCode();
        static Vector2 s_DragHandleMouseStart;
        static Vector2 s_DragHandleMouseCurrent;
        static Vector3 s_DragHandleWorldStart;
        static float s_DragHandleClickTime = 0;
        static int s_DragHandleClickID;
        static float s_DragHandleDoubleClickInterval = 0.5f;
        static bool s_DragHandleHasMoved;

        // externally accessible to get the ID of the most resently processed DragHandle
        public static int lastDragHandleID;

        public enum DragHandleResult
        {
            none = 0,

            LMBPress,
            LMBClick,
            LMBDoubleClick,
            LMBDrag,
            LMBRelease,

            RMBPress,
            RMBClick,
            RMBDoubleClick,
            RMBDrag,
            RMBRelease,
        };

        public static Vector3 DragHandle(Vector3 position, float handleSize, Handles.CapFunction capFunc, Color colorSelected, out DragHandleResult result)
        {
            int id = GUIUtility.GetControlID(s_DragHandleHash, FocusType.Passive);
            lastDragHandleID = id;

            Vector3 screenPosition = Handles.matrix.MultiplyPoint(position);
            Matrix4x4 cachedMatrix = Handles.matrix;

            result = DragHandleResult.none;

            switch (Event.current.GetTypeForControl(id))
            {
                case EventType.MouseDown:
                    if (HandleUtility.nearestControl == id && (Event.current.button == 0 || Event.current.button == 1))
                    {
                        GUIUtility.hotControl = id;
                        s_DragHandleMouseCurrent = s_DragHandleMouseStart = Event.current.mousePosition;
                        s_DragHandleWorldStart = position;
                        s_DragHandleHasMoved = false;

                        Event.current.Use();
                        EditorGUIUtility.SetWantsMouseJumping(1);

                        if (Event.current.button == 0)
                            result = DragHandleResult.LMBPress;
                        else if (Event.current.button == 1)
                            result = DragHandleResult.RMBPress;
                    }
                    break;

                case EventType.MouseUp:
                    if (GUIUtility.hotControl == id && (Event.current.button == 0 || Event.current.button == 1))
                    {
                        GUIUtility.hotControl = 0;
                        Event.current.Use();
                        EditorGUIUtility.SetWantsMouseJumping(0);

                        if (Event.current.button == 0)
                            result = DragHandleResult.LMBRelease;
                        else if (Event.current.button == 1)
                            result = DragHandleResult.RMBRelease;

                        if (Event.current.mousePosition == s_DragHandleMouseStart)
                        {
                            bool doubleClick = (s_DragHandleClickID == id) &&
                                (Time.realtimeSinceStartup - s_DragHandleClickTime < s_DragHandleDoubleClickInterval);

                            s_DragHandleClickID = id;
                            s_DragHandleClickTime = Time.realtimeSinceStartup;

                            if (Event.current.button == 0)
                                result = doubleClick ? DragHandleResult.LMBDoubleClick : DragHandleResult.LMBClick;
                            else if (Event.current.button == 1)
                                result = doubleClick ? DragHandleResult.RMBDoubleClick : DragHandleResult.RMBClick;

                        }
                    }
                    break;

                case EventType.MouseDrag:
                    if (GUIUtility.hotControl == id)
                    {
                        s_DragHandleMouseCurrent += new Vector2(Event.current.delta.x, -Event.current.delta.y);
                        Vector3 position2 = Camera.current.WorldToScreenPoint(Handles.matrix.MultiplyPoint(s_DragHandleWorldStart))
                            + (Vector3)(s_DragHandleMouseCurrent - s_DragHandleMouseStart);
                        position = Handles.matrix.inverse.MultiplyPoint(Camera.current.ScreenToWorldPoint(position2));

                        if (Camera.current.transform.forward == Vector3.forward || Camera.current.transform.forward == -Vector3.forward)
                            position.z = s_DragHandleWorldStart.z;
                        if (Camera.current.transform.forward == Vector3.up || Camera.current.transform.forward == -Vector3.up)
                            position.y = s_DragHandleWorldStart.y;
                        if (Camera.current.transform.forward == Vector3.right || Camera.current.transform.forward == -Vector3.right)
                            position.x = s_DragHandleWorldStart.x;

                        if (Event.current.button == 0)
                            result = DragHandleResult.LMBDrag;
                        else if (Event.current.button == 1)
                            result = DragHandleResult.RMBDrag;

                        s_DragHandleHasMoved = true;

                        GUI.changed = true;
                        Event.current.Use();
                    }
                    break;

                case EventType.Repaint:
                    Color currentColour = Handles.color;
                    if (id == GUIUtility.hotControl && s_DragHandleHasMoved)
                        Handles.color = colorSelected;

                    Handles.matrix = Matrix4x4.identity;
                
                    capFunc(id, screenPosition, Quaternion.identity, handleSize, EventType.Repaint);
                    Handles.matrix = cachedMatrix;

                    Handles.color = currentColour;
                    break;

                case EventType.Layout:
                    Handles.matrix = Matrix4x4.identity;
                    HandleUtility.AddControl(id, HandleUtility.DistanceToCircle(screenPosition, handleSize));
                    Handles.matrix = cachedMatrix;
                    break;
            }

            return position;
        }
    }

    [CustomEditor(typeof(Intersection))]
    public class IntersectionEditor : Editor
    {
        private void OnEnable()
        {
            EditorApplication.update += Update;
        }

        private void OnDisable()
        {
            EditorApplication.update -= Update;
        }

        private void Update()
        {
            if (Application.isPlaying)
                return;
            Intersection i = (Intersection)target;
            if (i == null) return;
            if (i != null && i.transform.hasChanged)
            {
                i.transform.hasChanged = false;
                i.UpdateConnections();
                var rs = i.GetComponentInParent<RoadSystem>();
                if (rs != null)
                {
                    if (i.splineForAreaEffects)
                    {
                        rs.UpdateSystem(SplineUtility.GetBounds(i.splineForAreaEffects.Spline, i.splineForAreaEffects.transform.localToWorldMatrix));
                    }
                }
            }
            {
                var hideFlags = HideFlags.None;
                RoadSystem rs = i.GetComponentInParent<RoadSystem>();
                if (rs != null && rs.hideGameObjects)
                {
                    hideFlags = HideFlags.HideInHierarchy;
                }
                for (int x = 0; x < i.transform.childCount; ++x)
                {
                    i.transform.GetChild(x).hideFlags = hideFlags;
                }
            }
        }

        public override void OnInspectorGUI()
        {
            Intersection inter = (target as Intersection);
            if (inter != null && inter.GetComponentInParent<RoadSystem>() == null)
            {
                // only draw setup data when not under a road system.
                base.OnInspectorGUI();
            }
            EditorGUI.BeginChangeCheck();

            
            if (inter != null)
            {
                serializedObject.Update();
                EditorGUI.BeginChangeCheck();
                EditorGUILayout.PropertyField(serializedObject.FindProperty("modifiesTerrain"));
                if (EditorGUI.EndChangeCheck())
                {
                    serializedObject.ApplyModifiedProperties();
                    var rs = inter.GetComponentInParent<RoadSystem>();
                    if (rs != null) rs.UpdateAll();
                }
                foreach (var connection in inter.connectionPoints)
                {
                    if (connection.connector != null)
                    {
                        EditorGUILayout.BeginHorizontal();
                        EditorGUILayout.PrefixLabel(connection.connector.name);
                        var old = GUI.enabled;
                        GUI.enabled = old && (connection.road != null);
                        if (GUILayout.Button("Select"))
                        {
                            Selection.activeGameObject = connection.road.gameObject;
                        }
                        GUI.enabled = old;
                        EditorGUILayout.EndHorizontal();
                    }
                    
                }
                RoadEditor.DrawChoiceGUI(inter, inter.defaultChoiceData, inter.config, false);
            }
            if (EditorGUI.EndChangeCheck())
            {
                inter.Generate();
            }
        }

        static int nameNum = 0;
        public void OnSceneGUI()
        {
            var intersection = target as Intersection;
            var roadSystem = intersection.transform.GetComponentInParent<RoadSystem>();
            if (roadSystem != null)
            {
                if (intersection.connectionPoints != null)
                {
                    foreach (var pnt in intersection.connectionPoints)
                    {
                        if (pnt.container == null && pnt.connector != null)
                        {
                            DragHandles.DragHandleResult dhResult;
                            Handles.color = pnt.connector.color;
                            Vector3 newPosition = DragHandles.DragHandle(pnt.connector.transform.position, 1.0f, Handles.SphereHandleCap, Color.red, out dhResult);
                            //Handles.CircleHandleCap(0, pnt.transform.position, pnt.transform.rotation, 0.5f, Event.current.type);
                            if (dhResult == DragHandles.DragHandleResult.LMBDoubleClick &&
                                pnt.connector != null &&
                                pnt.connector.config != null &&
                                pnt.connector.config.entries.Length > 0 &&
                                pnt.connector.config.entries[0].prefab != null)
                            {
                                CreateRoad(roadSystem, intersection, pnt);

                            }
                        }
                    }
                }
            }
        }

        public Road CreateRoad(RoadSystem roadSystem, Intersection intersection, Intersection.ConnectionPoint pnt, bool selectSpline = true)
        {
            string name = "Road";
            if (roadSystem != null && roadSystem.systemConfig != null && !string.IsNullOrEmpty(roadSystem.systemConfig.namePrefix))
            {
                name = roadSystem.systemConfig.namePrefix;
            }
            GameObject go = new GameObject(name + nameNum++);
            go.transform.SetParent(roadSystem.transform);
            go.transform.position = pnt.connector.transform.position;
            SplineContainer splineContainer = go.AddComponent<SplineContainer>();
            var road = go.AddComponent<Road>();
            road.config = pnt.connector.config;
            road.modifiesTerrain = intersection.modifiesTerrain;
            Spline spline = new Spline();
            float3 forward = pnt.connector.transform.forward;
            float size = pnt.connector.config.entries[0].size;
            spline.Add(new BezierKnot(new Vector3(0, 0, 0), -forward * size * 0.2f, forward * size * 0.2f, Quaternion.identity));
            spline.Add(new BezierKnot(forward * size, -forward * size * 0.2f, forward * size * 0.2f, Quaternion.identity));
            spline.SetTangentMode(1, TangentMode.AutoSmooth);
            splineContainer.RemoveSplineAt(0);
            splineContainer.AddSpline(spline);
            pnt.container = splineContainer;
            pnt.road = road;
            pnt.front = true;
            road.splineContainer = splineContainer;
            RoadSystem rs = intersection.GetComponentInParent<RoadSystem>();
            intersection.UpdateConnections(rs);
            EditorUtility.SetDirty(intersection);
            if (rs != null)
            {
                if (intersection.splineForAreaEffects)
                {
                    rs.UpdateSystem(SplineUtility.GetBounds(intersection.splineForAreaEffects.Spline, intersection.splineForAreaEffects.transform.localToWorldMatrix));
                }
            }
            road.Generate(rs);

            if (selectSpline)
            {
                SelectSpline(splineContainer);
            }

            return road;
        }

        public void SelectSpline(UnityEngine.Splines.SplineContainer spline)
        {
            Selection.activeGameObject = spline.gameObject;
            ActiveEditorTracker.sharedTracker.RebuildIfNecessary();

            EditorApplication.delayCall += SetKnotPlacementTool;
        }

        void SetKnotPlacementTool()
        {
            string namespaceName = "UnityEditor.Splines";
            string typeName = "KnotPlacementTool";

            Assembly assembly = AppDomain.CurrentDomain.GetAssemblies()
                .SingleOrDefault(a => a.GetTypes().Any(t => t.Namespace == namespaceName));

            if (assembly == null)
            {
                Console.WriteLine("Assembly not found for namespace: " + namespaceName);
                return;
            }

            Type privateType = assembly.GetType(namespaceName + "." + typeName, true);

            
            ToolManager.SetActiveContext<SplineToolContext>();
            //ToolManager.SetActiveTool(privateType);
        }
    }
}
