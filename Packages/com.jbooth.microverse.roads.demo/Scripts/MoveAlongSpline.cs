using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Splines;
using static UnityEngine.Splines.SplineComponent;

namespace JBooth.MicroVerseCore.Roads.Demo
{
    public class MoveAlongSpline : MonoBehaviour
    {
        [Header("Components")]

        public GameObject npc;

        public SplineContainer splineContainer;
        public Vector3 pathOffset;

        [Header("Speed Settings")]
        
        public float speed = 1.0f;
        public float minSpeed = 1f;
        public float maxSpeed = 10f;

        [Header("Manual Adjustment")]

        #region mousewheel
        
        [Tooltip("Manual speed adjustment via mouse wheel")]
        public bool mouseWheelSpeedChange = false;

        [Tooltip("Factor that's applied to the mouse wheel's delta increment")]
        public float mouseWheelFactor = 1.0f;

        #endregion mousewheel

        [Header("Alignment")]

        public bool useRoll = true;

        public MoveAlongSpline parent;
        public float parentOffsetT = 0f;

        private SplinePath path;
        private float pathLength;

        private float t = 0f;

        #region axis alignment
        public AlignAxis axisForward = AlignAxis.ZAxis;
        public AlignAxis axisUp = AlignAxis.YAxis;
        readonly float3[] axisToVector = new float3[] { math.right(), math.up(), math.forward(), math.left(), math.down(), math.back() };

        protected float3 GetAxis(AlignAxis axis)
        {
            return axisToVector[(int)axis];
        }
        #endregion axis alignment

        void Start()
        {
            if (npc == null)
            {
                npc = this.gameObject;
            }

            var container1Transform = splineContainer.transform.localToWorldMatrix;

            path = new SplinePath(new[]
            {
                new SplineSlice<Spline>(splineContainer.Splines[0], new SplineRange(/* start (inclusive) */ 0, /* count; +1 for the closed loop segment */ splineContainer.Splines[0].Count + 1), container1Transform),
            });

            pathLength = path.GetLength();

        }

        void FixedUpdate()
        {
            if(parent != null)
            {
                t = parent.GetT() + parentOffsetT / pathLength;
            }

            Vector3 pos = path.EvaluatePosition(t);
            npc.transform.position = pos + pathOffset;

            Vector3 direction = path.EvaluateTangent(t);

            Vector3 forward = Vector3.Normalize(path.EvaluateTangent(t));
            Vector3 up = path.EvaluateUpVector(t);

            var remappedForward = GetAxis(axisForward); // new Vector3(0, 0, 1);
            var remappedUp = GetAxis(axisUp); // new Vector3(0, 1, 0);
            var axisRemapRotation = Quaternion.Inverse(Quaternion.LookRotation(remappedForward, remappedUp));

            npc.transform.rotation = Quaternion.LookRotation(forward, up) * axisRemapRotation;

            float angle = Vector3.Angle(transform.up, Vector3.up);

            float distance = direction.magnitude;
            float forceMagnitude = Physics.gravity.y * angle / Mathf.Pow(distance, 2);

            Vector3 force = direction.normalized * forceMagnitude;

            speed += force.y;

            if (mouseWheelSpeedChange)
            {
                speed += Input.mouseScrollDelta.y * mouseWheelFactor;
            }

            speed = Mathf.Clamp(speed, minSpeed, maxSpeed);

            t = (t + speed * Time.deltaTime / pathLength) % 1f;

        }

        public float GetT()
        {
            return t;
        }
    }
}