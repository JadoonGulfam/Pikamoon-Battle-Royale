using Unity.Mathematics;
using UnityEditor;
using UnityEditor.EditorTools;
using UnityEditor.Splines;
using UnityEngine;
using UnityEngine.Splines;

namespace JBooth.MicroVerseCore
{

    [EditorTool("Road Data Tool", typeof(Road))]
    public class RoadOverlayTool : EditorTool, IDrawSelectedHandles
    {
        GUIContent m_IconContent;
        public override GUIContent toolbarIcon => m_IconContent;

        void OnEnable()
        {
            m_IconContent = new GUIContent()
            {
                image = Resources.Load<Texture2D>("Icons/RoadOverlayTool"),
                text = "Spawn Data Tool",
                tooltip = "Adjust which prefabs get spawned along the spline"
            };
        }

        void OnDisable()
        {
            SplineRoadDataHandles.curChoiceData = null;
        }

        public override void OnWillBeDeactivated()
        {
            base.OnWillBeDeactivated();
            SplineRoadDataHandles.curChoiceData = null;
        }
        protected const float k_HandleSize = 0.15f;


        public override void OnToolGUI(EditorWindow window)
        {
            var splineDataTarget = target as Road;
            if (splineDataTarget == null || splineDataTarget.splineContainer == null)
                return;
            
            Undo.RecordObject(splineDataTarget, "Modifying Overlay Choices");
            if (splineDataTarget.splineOverlayChoices.Count == 0)
            {
                splineDataTarget.splineOverlayChoices.Add(new Road.SplineChoices());
            }

            for (int i = 0; i < splineDataTarget.splineOverlayChoices.Count; ++i)
            {
                if (i < splineDataTarget.splineContainer.Splines.Count)
                {
                    var spline = splineDataTarget.splineContainer.Splines[i];

                    var nativeSpline = new NativeSpline(spline, splineDataTarget.splineContainer.transform.localToWorldMatrix);

                    Handles.color = Color.blue;
                    //Using the out-of the box behaviour to manipulate indexes
                    nativeSpline.DataPointHandlesWithRoad(splineDataTarget.splineOverlayChoices[i].choices);
                }
            }

        }

        public void CustomHandleCap(int controlID, Vector3 position, Quaternion rotation, float size, EventType eventType)
        {
            Handles.CubeHandleCap(controlID, position, rotation, size, eventType);
        }


        public void OnDrawHandles()
        {

            var splineDataTarget = target as Road;
            if (ToolManager.IsActiveTool(this) || splineDataTarget == null || splineDataTarget.splineContainer == null)
                return;

            while (splineDataTarget.splineOverlayChoices.Count > splineDataTarget.splineContainer.Splines.Count)
            {
                splineDataTarget.splineOverlayChoices.RemoveAt(splineDataTarget.splineOverlayChoices.Count - 1);
            }
            for (int i = 0; i < splineDataTarget.splineContainer.Splines.Count; ++i)
            {
                if (i >= splineDataTarget.splineOverlayChoices.Count)
                {
                    var sw = new Road.SplineChoices();
                    sw.choices = new SplineData<Road.SplineChoiceData>();
                    sw.choices.PathIndexUnit = PathIndexUnit.Normalized;
                    splineDataTarget.splineOverlayChoices.Add(sw);
                }


            }



        }

    }

}
