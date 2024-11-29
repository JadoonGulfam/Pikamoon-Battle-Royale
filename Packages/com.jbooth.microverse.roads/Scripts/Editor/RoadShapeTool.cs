using Unity.Mathematics;
using UnityEditor;
using UnityEditor.EditorTools;
using UnityEditor.Splines;
using UnityEngine;
using UnityEngine.Splines;

namespace JBooth.MicroVerseCore
{

    [EditorTool("Shape Tool", typeof(Road))]
    public class RoadShapeTool : EditorTool, IDrawSelectedHandles
    {
        public override bool IsAvailable()
        {
            var splineDataTarget = target as Road;
            if (splineDataTarget == null || splineDataTarget.splineContainer == null)
                return false;
            RoadSystem rs = splineDataTarget.GetComponentInParent<RoadSystem>();
            if (rs == null || rs.systemConfig == null || rs.systemConfig.allowShaping == false)
                return false;

            return base.IsAvailable();
        }
        GUIContent m_IconContent;
        public override GUIContent toolbarIcon => m_IconContent;

        bool m_DisableHandles = false;

        void OnEnable()
        {
            m_IconContent = new GUIContent()
            {
                image = Resources.Load<Texture2D>("Icons/WidthTool"),
                text = "Shape Tool",
                tooltip = "Adjust the shape of the created path."
            };
        }

        protected const float k_HandleSize = 0.15f;

        protected bool DrawDataPoints(ISpline spline, SplineData<float2> splineData)
        {
            var inUse = false;
            for (int dataFrameIndex = 0; dataFrameIndex < splineData.Count; dataFrameIndex++)
            {
                var dataPoint = splineData[dataFrameIndex];

                var normalizedT = SplineUtility.GetNormalizedInterpolation(spline, dataPoint.Index, splineData.PathIndexUnit);
                spline.Evaluate(normalizedT, out var position, out var tangent, out var up);
                tangent.y = 0;
                if (DrawDataPoint(position, tangent, Vector3.up, dataPoint.Value, out var result))
                {
                    dataPoint.Value = result;
                    splineData[dataFrameIndex] = dataPoint;
                    inUse = true;
                }
            }
            return inUse;
        }

        public override void OnToolGUI(EditorWindow window)
        {
            var splineDataTarget = target as Road;
            if (splineDataTarget == null || splineDataTarget.splineContainer == null)
                return;

            Undo.RecordObject(splineDataTarget, "Modifying Shape SplineData");

            for (int i = 0; i < splineDataTarget.splineShapes.Count; ++i)
            {
                if (i < splineDataTarget.splineContainer.Splines.Count)
                {
                    var spline = splineDataTarget.splineContainer.Splines[i];

                    var nativeSpline = new NativeSpline(spline, splineDataTarget.splineContainer.transform.localToWorldMatrix);

                    Handles.color = Color.blue;
                    m_DisableHandles = false;

                    //User defined handles to manipulate width
                    DrawDataPoints(nativeSpline, splineDataTarget.splineShapes[i].shapeData);

                    //Using the out-of the box behaviour to manipulate indexes
                    nativeSpline.DataPointHandles(splineDataTarget.splineShapes[i].shapeData);
                }
            }

        }

        public void OnDrawHandles()
        {

            var splineDataTarget = target as Road;
            if (ToolManager.IsActiveTool(this) || splineDataTarget == null || splineDataTarget.splineContainer == null)
                return;

            while (splineDataTarget.splineShapes.Count > splineDataTarget.splineContainer.Splines.Count)
            {
                splineDataTarget.splineShapes.RemoveAt(splineDataTarget.splineShapes.Count - 1);
            }
            for (int i = 0; i < splineDataTarget.splineContainer.Splines.Count; ++i)
            {
                if (i >= splineDataTarget.splineShapes.Count)
                {
                    var sw = new Road.SplineShapeData();
                    sw.shapeData = new SplineData<float2>();
                    sw.shapeData.PathIndexUnit = PathIndexUnit.Normalized;
                    splineDataTarget.splineShapes.Add(sw);
                }

                var nativeSpline = new NativeSpline(splineDataTarget.splineContainer.Splines[i], splineDataTarget.splineContainer.transform.localToWorldMatrix);

                Color color = Color.blue;
                color.a = 0.5f;
                Handles.color = color;
                m_DisableHandles = true;

                DrawDataPoints(nativeSpline, splineDataTarget.splineShapes[i].shapeData);

            }

        }

        protected bool DrawDataPoint(
            Vector3 position,
            Vector3 tangent,
            Vector3 up,
            float2 inValue,
            out float2 outValue)
        {
            // width handles
            int id1 = m_DisableHandles ? -1 : GUIUtility.GetControlID(FocusType.Passive);
            int id2 = m_DisableHandles ? -1 : GUIUtility.GetControlID(FocusType.Passive);
            // height handles
            int id3 = m_DisableHandles ? -1 : GUIUtility.GetControlID(FocusType.Passive);
            int id4 = m_DisableHandles ? -1 : GUIUtility.GetControlID(FocusType.Passive);

            outValue = inValue;
            if (tangent == Vector3.zero)
                return false;

            if (Event.current.type == EventType.MouseUp
                && Event.current.button != 0
                && (GUIUtility.hotControl == id1 || GUIUtility.hotControl == id2 || GUIUtility.hotControl == id3 || GUIUtility.hotControl == id4))
            {
                Event.current.Use();
                return false;
            }

            var handleColor = Color.cyan;
            if (GUIUtility.hotControl == id1 || GUIUtility.hotControl == id2 || GUIUtility.hotControl == id3 || GUIUtility.hotControl == id4)
                handleColor = Handles.selectedColor;

            else if (GUIUtility.hotControl == 0 && (HandleUtility.nearestControl == id1 || HandleUtility.nearestControl == id2 || HandleUtility.nearestControl == id3 || HandleUtility.nearestControl == id4))
                handleColor = Handles.preselectionColor;

            RoadSystem rs = (target as Road).GetComponentInParent<RoadSystem>();
            float handleOffset = 0;
            if (rs != null && rs.systemConfig != null)
            {
                handleOffset = rs.systemConfig.shapingSizeHandleStart;
            }

            var normalDirection = math.normalize(math.cross(tangent, up));
            inValue++;
            var extremity1 = position - inValue.x * (Vector3)normalDirection - ((Vector3)normalDirection * handleOffset);
            var extremity2 = position + inValue.x * (Vector3)normalDirection + ((Vector3)normalDirection * handleOffset);
            var extremity3 = position - inValue.y * (Vector3)up - ((Vector3)up * handleOffset);
            var extremity4 = position + inValue.y * (Vector3)up + ((Vector3)up * handleOffset); 
            Vector3 val1, val2, val3, val4;
            using (new Handles.DrawingScope(handleColor))
            {
                Handles.DrawLine(extremity1, extremity2);
                Handles.DrawLine(extremity3, extremity4);
                val1 = Handles.Slider(id1, extremity1, normalDirection,
                    k_HandleSize * .5f * HandleUtility.GetHandleSize(position), CustomHandleCap, 0);
                val2 = Handles.Slider(id2, extremity2, normalDirection,
                    k_HandleSize * .5f * HandleUtility.GetHandleSize(position), CustomHandleCap, 0);

                val3 = Handles.Slider(id3, extremity3, up,
                    k_HandleSize * .5f * HandleUtility.GetHandleSize(position), CustomHandleCap, 0);
                val4 = Handles.Slider(id4, extremity4, up,
                    k_HandleSize * .5f * HandleUtility.GetHandleSize(position), CustomHandleCap, 0);
            }

            if (GUIUtility.hotControl == id1 && math.abs((val1 - extremity1).magnitude) > 0)
            {
                outValue.x = math.max(0, math.abs((val1 - position).magnitude) - 1 - handleOffset);
                return true;
            }

            if (GUIUtility.hotControl == id2 && math.abs((val2 - extremity2).magnitude) > 0)
            {
                outValue.x = math.max(0, math.abs((val2 - position).magnitude) - 1 - handleOffset);
                return true;
            }

            if (GUIUtility.hotControl == id3 && math.abs((val3 - extremity3).magnitude) > 0)
            {
                outValue.y = math.max(0, math.abs((val3 - position).magnitude) - 1 - handleOffset);
                return true;
            }

            if (GUIUtility.hotControl == id4 && math.abs((val4 - extremity4).magnitude) > 0)
            {
                outValue.y = math.max(0, math.abs((val4 - position).magnitude) - 1 - handleOffset);
                return true;
            }

            return false;
        }

        public void CustomHandleCap(int controlID, Vector3 position, Quaternion rotation, float size, EventType eventType)
        {
            Handles.CubeHandleCap(controlID, position, rotation, size, m_DisableHandles ? EventType.Repaint : eventType);
        }
    }

}
