using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Splines;
using Unity.Mathematics;

namespace JBooth.MicroVerseCore
{
    [ExecuteAlways]
    public class SplineRelativeTransform : MonoBehaviour
    {
        public SplineContainer splineContainer;
        public float3 offset;
        [HideInInspector] public Quaternion rotOffset;
        [HideInInspector] public float T;


        public bool keepUpright = false;

#if UNITY_EDITOR

        private void OnSplineModified(Spline spline, int knot, SplineModification mod)
        {
            if (Application.isPlaying)
                return;
            if (splineContainer != null)
            {
                foreach (var s in splineContainer.Splines)
                {
                    if (ReferenceEquals(spline, s))
                    {
                        if (mod == SplineModification.KnotInserted || mod == SplineModification.KnotRemoved)
                            CaptureOffset();
                        Refresh();
                    }
                }
            }
        }

        private void Update()
        {
            if (splineContainer != null)
            {
                if (transform.hasChanged)
                {
                    CaptureOffset();

                    transform.hasChanged = false;
                }
            }
        }

        private void OnEnable()
        {
            Spline.Changed += OnSplineModified;
        }

        private void OnDisable()
        {
            Spline.Changed -= OnSplineModified;
        }
#endif

        public void CaptureOffset()
        {
            if (splineContainer != null)
            {
                float3 localPos = splineContainer.transform.worldToLocalMatrix.MultiplyPoint(transform.position);
                SplineUtility.GetNearestPoint(splineContainer[0], localPos, out var nearest, out T, 128, 128);
                SplineUtility.Evaluate(splineContainer[0], T, out var pos, out var tang, out var up);
                tang = math.normalizesafe(tang);
                up = math.normalizesafe(up);
                Quaternion quat = Quaternion.LookRotation(math.cross(tang, up), up);
                Matrix4x4 mtx = Matrix4x4.TRS(pos, quat, Vector3.one);
                offset = mtx.inverse.MultiplyPoint(localPos);
                
                // rotate offset by spline direction
                rotOffset = transform.rotation * Quaternion.Inverse(quat);
            }
        }

        public void Refresh()
        {
            splineContainer[0].Evaluate(T, out var position, out var tangent, out var up);
            up = math.normalizesafe(up);
            tangent = math.normalizesafe(tangent);
            Quaternion quat = Quaternion.LookRotation(Vector3.Cross(tangent, up), up);
            Matrix4x4 mtx = Matrix4x4.TRS(position, quat, Vector3.one);
            var localPos = mtx.MultiplyPoint(offset);
            transform.position = splineContainer.transform.localToWorldMatrix.MultiplyPoint(localPos);
            transform.rotation = rotOffset * quat;

            if (keepUpright)
            {
                var euler = transform.rotation.eulerAngles;
                euler.x = 0;
                euler.z = 0;
                transform.rotation = Quaternion.Euler(euler);
            }
        }
    }
}
