using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Splines;
using Unity.Mathematics;

namespace JBooth.MicroVerseCore
{

    [ExecuteAlways]
    [SelectionBase]
    public class Intersection : MonoBehaviour
    {
        [Tooltip("Allows you to disable/enable terrain modification on a road piece")]
        public bool modifiesTerrain = true;

        [System.Serializable]
        public class ConnectionPoint
        {
            public Connector connector;
            [HideInInspector] public Intersection owner;
            [HideInInspector] public SplineContainer container;
            [HideInInspector] public Road road;
            [HideInInspector] public bool front;
        }
        public ConnectionPoint[] connectionPoints;

        [HideInInspector] public Road.SplineChoiceData defaultChoiceData = new Road.SplineChoiceData();
        public RoadConfig config;

        public SplineContainer splineForAreaEffects;

        public void OnDrawGizmos()
        {
            if (connectionPoints != null)
            {
                foreach (var pnt in connectionPoints)
                {
                    if (pnt != null && pnt.connector != null)
                    {
                        Gizmos.color = pnt.connector.color;
                        Gizmos.DrawCube(pnt.connector.transform.position, new Vector3(0.3f, 0.3f, 0.3f));
                        Gizmos.DrawRay(new Ray(pnt.connector.transform.position, pnt.connector.transform.forward * 2));
                    }
                }
            }
        }

        void ClearSpawns()
        {
            foreach (var s in spawns)
            {
                if (s != null)
                {
                    DestroyImmediate(s);
                }
            }
            spawns.Clear();
        }

        private void OnDisable()
        {
#if __MICROVERSE__
            if (modifiesTerrain) MicroVerse.instance?.Invalidate();
#endif
        }

        [HideInInspector] public List<GameObject> spawns = new List<GameObject>();
        public void Generate(RoadSystem rs = null)
        {
            if (rs == null)
                rs = GetComponentInParent<RoadSystem>();

            ClearSpawns();

            foreach (var oe in defaultChoiceData.overlayEntries)
            {
                if (oe.prefab != null)
                {
                    GameObject go = Instantiate(oe.prefab, transform);
                    go.transform.localPosition = Vector3.zero;
                    go.transform.localRotation = Quaternion.identity;
                    Road.SetHideFlags(go, rs);
                    spawns.Add(go);
                }
            }
        }


        BezierKnot MatchConnection(ConnectionPoint pnt, BezierKnot knot)
        {
            float scale = 6;
            if (pnt.road.config != null && pnt.road.config.entries.Length > 0)
                scale = pnt.road.config.entries[0].size;

            knot.Position = pnt.container.transform.worldToLocalMatrix.MultiplyPoint(pnt.connector.transform.position);
            knot.TangentIn = Vector3.forward * Mathf.Max(scale, math.length(knot.TangentIn));
            knot.TangentOut = Vector3.forward * Mathf.Max(scale, math.length(knot.TangentOut));
            knot.Rotation = Quaternion.Inverse(pnt.container.transform.rotation) * pnt.connector.transform.rotation;
            return knot;
        }

        public void UpdateConnections(RoadSystem rs = null)
        {
            if (rs == null)
            {
                rs = GetComponentInParent<RoadSystem>();
            }
            if (rs != null && rs.templateMaterial != null)
            {
                var over = GetComponent<RoadMaterialOverride>();
                if (over != null)
                {
                    over.Override(rs.templateMaterial);
                }
            }

            // make sure xz rotations match..
            if (transform.localScale.x != transform.localScale.z)
            {
                transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y, transform.localScale.x);
            }
            if (connectionPoints != null)
            {
                foreach (var pnt in connectionPoints)
                {
                    pnt.owner = this;
                    if (pnt.container != null)
                    {
                        var spline = pnt.container.Spline;

                        if (spline.Count > 0 && pnt.front)
                        {
                            if (pnt.road != null)
                            {
                                pnt.road.beginConnector = pnt;
                            }
                            else
                            {
                                Debug.LogError("Intersection " + this.name + " connector is missing it's road");
                            }
                            var knot = spline[0];
                            var knot2 = knot;
                            knot = MatchConnection(pnt, knot);

                            if (!knot.Equals(knot2))
                            {
                                spline.SetTangentModeNoNotify(spline.Count - 1, TangentMode.Broken);
                                spline.SetKnot(0, knot);
                                pnt.road?.Generate(rs);
                            }
                        }
                        if (spline.Count > 1 && pnt.front == false)
                        {
                            var knot = spline[spline.Count - 1];
                            var knot2 = knot;
                            knot = MatchConnection(pnt, knot);
                            if (pnt.road != null)
                            {
                                pnt.road.endConnector = pnt;
                            }
                            else
                            {
                                Debug.LogError("Intersection " + this.name + " connector is missing it's road");
                            }
                            if (!knot.Equals(knot2))
                            {
                                spline.SetTangentModeNoNotify(spline.Count - 1, TangentMode.Broken);
                                spline.SetKnot(spline.Count - 1, knot);
                                pnt.road?.Generate(rs);
                            }
                        }
                    }
                }
            }
        }
    }
}
