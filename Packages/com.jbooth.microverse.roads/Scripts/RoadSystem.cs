using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Splines;

namespace JBooth.MicroVerseCore
{
    [ExecuteAlways]
    public class RoadSystem : MonoBehaviour
    {
        [Tooltip("Show generated objects in editor?")]
        public bool hideGameObjects = true;
        [Tooltip("When false, meshes are saved with the scene file. This can make the scene file much larger. When true, meshes are generated at load time, which can make scene startup slower")]
        public bool generateAtLoad = false;
        public string contentID;
        [Tooltip("Road System Config for this road system")]
        public RoadSystemConfig systemConfig;


#if __MICROVERSE__ && __MICROVERSE_SPLINES__
        [HideInInspector] public SplinePath splinePath;
#endif

        [Tooltip("If set, this will copy all properties from the template material except the mask to all pieces")]
        public Material templateMaterial;

        [HideInInspector]
        public Dictionary<Texture2D, Material> materialInstances = new Dictionary<Texture2D, Material>();

        public void OnEnable()
        {
            if (generateAtLoad)
            {
                ReGenerateRoads();
                Road[] roads = GetComponentsInChildren<Road>();
                foreach (var r in roads)
                {
                    MeshCollider[] colliders = r.GetComponentsInChildren<MeshCollider>();
                    foreach (var c in colliders)
                    {
                        c.enabled = true;
                    }
                }
            }
#if __MICROVERSE__ && __MICROVERSE_SPLINES__

            UpdateSystem(null);
            
#endif
        }

        public void ReGenerateRoads()
        {
            Road[] roads = GetComponentsInChildren<Road>();
            foreach (var r in roads)
            {
                r.Generate(this);
            }

            Intersection[] inters = GetComponentsInChildren<Intersection>();
            foreach (var i in inters)
            {
                i.Generate(this);
            }
        }

        private void OnDisable()
        {
#if __MICROVERSE__ && __MICROVERSE_SPLINES__
            MicroVerse.instance?.Invalidate();
#endif
            Road.ClearCache();
        }


        private void OnDestroy()
        {
            Road.ClearCache();
        }

        public void UpdateAll()
        {
            UpdateMaterialOverrides();
            UpdateSystem(null);
        }

        public void UpdateMaterialOverrides()
        {
            if (templateMaterial != null)
            {
                RoadMaterialOverride[] overs = GetComponentsInChildren<RoadMaterialOverride>();
                foreach (var o in overs)
                {
                    o.Override(templateMaterial);
                }
            }
        }

        public void UpdateSystem(Bounds? bounds)
        {
#if __MICROVERSE__ && __MICROVERSE_SPLINES__ && UNITY_EDITOR

            if (splinePath == null)
                splinePath = GetComponent<SplinePath>();
            if (splinePath == null && GetComponentInParent<MicroVerse>() != null)
            {
                splinePath = gameObject.AddComponent<SplinePath>();
                splinePath.sdfRes = SplinePath.SDFRes.k512;
                splinePath.occludeTextureMod = true;
                splinePath.occludeHeightMod = true;
                splinePath.modifySplatMap = false;
                splinePath.clearTrees = true;
                splinePath.clearDetails = true;
                splinePath.clearObjects = true;
                splinePath.trench = 0.1f;
            }
            if (splinePath == null)
                return;

            UnityEngine.Profiling.Profiler.BeginSample("RoadSystem::Update");
            splinePath.spline = null;
            splinePath.treatAsSplineArea = true;

            List<SplineRenderer.RenderDesc> descs = new List<SplineRenderer.RenderDesc>();

            // do intersections before roads because they are areas, and otherwise the SDFs
            // do not combine correctly
            Intersection[] inters = GetComponentsInChildren<Intersection>();
            foreach (var r in inters)
            {
                if (r.splineForAreaEffects != null && r.modifiesTerrain)
                {
                    descs.Add(new SplineRenderer.RenderDesc()
                    {
                        splineContainer = r.splineForAreaEffects,
                        widthBoost = 0,
                        sdfMult = r.transform.lossyScale.x,
                        mode = SplineRenderer.RenderDesc.Mode.Intersection
                    });
                }
            }

            Road[] roads = GetComponentsInChildren<Road>();

            foreach (var r in roads)
            {
                if (r.splineContainer != null && r.config != null && r.modifiesTerrain)
                {
                    Vector2 gss = Vector2.zero;
                    Vector2 gse = Vector2.zero;

                    if (r.beginConnector != null && r.beginConnector.owner != null)
                    {
                        gss = new Vector2(r.beginConnector.owner.transform.lossyScale.x, r.beginConnector.owner.transform.lossyScale.y);
                    }
                    if (r.endConnector != null && r.endConnector.owner != null)
                    {
                        gse = new Vector2(r.endConnector.owner.transform.lossyScale.x, r.endConnector.owner.transform.lossyScale.y);
                    }
                    var widthData = new List<SplinePath.SplineWidthData>(1)
                    {
                        new SplinePath.SplineWidthData()
                    };
                    var wd = widthData[0].widthData;
                    
                    wd.Add(0, Mathf.Max(gss.x - 1, 0) * r.config.modelWidth);
                    wd.Add(1, Mathf.Max(gse.x - 1, 0) * r.config.modelWidth);

                    descs.Add(new SplineRenderer.RenderDesc()
                    {
                        splineContainer = r.splineContainer,
                        widthBoost = r.config.modelWidth,
                        widths = widthData,
                        mode = SplineRenderer.RenderDesc.Mode.Road
                    });
                }
            }

            
            splinePath.multiSpline = descs.ToArray();
            splinePath.ClearSplineRenders(bounds);
            MicroVerse.instance?.Invalidate(bounds);
            UnityEngine.Profiling.Profiler.EndSample();
#endif
        }
    }
}
