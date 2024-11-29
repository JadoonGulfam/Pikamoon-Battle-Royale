using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Splines;
using Unity.Mathematics;
using Unity.Jobs;
using Unity.Collections;
using UnityEngine.Profiling;

namespace JBooth.MicroVerseCore
{
    [ExecuteAlways]
    [SelectionBase]
    public class Road : MonoBehaviour
    {
        public SplineContainer splineContainer;
        [Tooltip("When false, any roll on the spline is automatically prevented")]
        public bool allowRoll = false;
        [Tooltip("Allows you to disable/enable terrain modification on a road piece")]
        public bool modifiesTerrain = true;

        [HideInInspector] public Intersection.ConnectionPoint beginConnector;
        [HideInInspector] public Intersection.ConnectionPoint endConnector;

        [System.Serializable]
        public class OverlayEntry
        {
            public string label;
            public GameObject prefab;
            public bool none;
        }
        [System.Serializable]
        public class SplineChoiceData
        {
            public GameObject roadPrefab;
            public List<OverlayEntry> overlayEntries = new List<OverlayEntry>();
            public OverlayEntry FindOverlayEntry(GameObject prefab)
            {
                foreach (var p in overlayEntries)
                {
                    if (p.prefab == prefab)
                        return p;
                }
                return null;
            }

            public OverlayEntry FindOverlayEntry(string label)
            {
                foreach (var p in overlayEntries)
                {
                    if (p.label == label)
                        return p;
                }
                return null;
            }
        }

        public SplineChoiceData defaultChoiceData = new SplineChoiceData();

        [System.Serializable]
        public class SplineChoices
        {
            public SplineData<SplineChoiceData> choices = new SplineData<SplineChoiceData>();
        }

        public List<SplineChoices> splineOverlayChoices = new List<SplineChoices>();

        [System.Serializable]
        public class SplineShapeData
        {
            public SplineData<float2> shapeData = new SplineData<float2>();
        }

        public List<SplineShapeData> splineShapes = new List<SplineShapeData>();


        SplineChoiceData FindChoiceData(float normalized_t)
        {
            SplineChoiceData last = null;
            foreach (var k in splineOverlayChoices[0].choices)
            {
                var kn = k.Index;// spline.ConvertIndexUnit(k.Index, PathIndexUnit.Knot, PathIndexUnit.Normalized);
                if (normalized_t >= kn)
                {
                    last = k.Value;
                }
                else if (normalized_t < kn)
                {
                    return last;
                }
            }
            return last;
        }


        public enum Orientation
        {
            X = 0, Z = 2
        }

        public int seed = -1;

        [Tooltip("Config for this road")]
        public RoadConfig config;


        string instanceName = "Generated Road Object";
        public List<GameObject> children = new List<GameObject>();
        public List<Mesh> meshes = new List<Mesh>();

        static Dictionary<Mesh, MeshCacheData> meshCache = new Dictionary<Mesh, MeshCacheData>();
        ObjJobHolder objJobHolder = null;
        
        List<VertexJobHolder> bendJobs = new List<VertexJobHolder>();
        NativeSpline nspline;
        NativeArray<CacheSplineJob.PosQuat> cachePosQuats;
        NativeArray<float3> splineWidthArray;

        List<ObjectSpawnJobLinearHolder> spawnLinearJobs = new List<ObjectSpawnJobLinearHolder>();

#if UNITY_EDITOR

        // hide flags don't save correctly, so we have to reset them.
        public void OnEnable()
        {
#if __MICROVERSE_SPLINES__
            // don't allow spline paths on roads, this cost me like 2 hours of debugging
            SplinePath sp = GetComponent<SplinePath>();
            if (sp)
            {
                DestroyImmediate(sp);
            }
#endif
            RoadSystem rs = GetComponentInParent<RoadSystem>();
            if (rs != null)
            {
                if (rs.hideGameObjects)
                {
                    for (int i = 0; i < this.transform.childCount; ++i)
                    {
                        SetHideFlags(this.transform.GetChild(i).gameObject, rs);
                    }
                }
            }
                
        }
#endif
        static List<Intersection> itersectionCache = new List<Intersection>(100);
        public void UpdateConnections(RoadSystem systemRoot, bool allowDisconnnect)
        {
            if (Application.isPlaying)
                return;
            
            if (systemRoot == null)
                systemRoot = GetComponentInParent<RoadSystem>();

            if (systemRoot == null)
                return;


            Profiler.BeginSample("Update Connections");
            Profiler.BeginSample("Find Intersections");
            systemRoot.GetComponentsInChildren<Intersection>(itersectionCache);
            Profiler.EndSample();

            float grabDistance = 4;
            if (config != null)
                grabDistance = config.modelWidth * 0.75f;

            // TODO: Rewrite to find closest points, not first qualifying. 
            foreach (var intersection in itersectionCache)
            {
                if (splineContainer != null)
                {
                    var spline = splineContainer.Spline;
#if UNITY_EDITOR
                    bool dirty = false;
#endif

                    bool needsUpdate = false;
                    if (spline.Count > 0)
                    {
                        var wsp = splineContainer.transform.localToWorldMatrix.MultiplyPoint(spline[0].Position);
                        foreach (var anchor in intersection.connectionPoints)
                        {
                            if (Vector3.Distance(anchor.connector.transform.position, wsp) < grabDistance)
                            {
                                if (anchor.container == null)
                                {
                                    anchor.front = true;
                                    anchor.container = splineContainer;
                                    anchor.road = this;
                                    beginConnector = anchor;
                                    anchor.owner = intersection;
#if UNITY_EDITOR
                                    dirty = true;
#endif
                                }
                                needsUpdate = true;

                            }
                            else
                            {
                                if (allowDisconnnect && anchor.container == splineContainer && anchor.front == true)
                                {
                                    anchor.container = null;
                                    anchor.owner = null;
                                    beginConnector = null;
                                    anchor.road = null;
#if UNITY_EDITOR
                                    dirty = true;
#endif
                                }
                            }
                        }
                    }
                    if (spline.Count > 1)
                    {
                        var wsp = splineContainer.transform.localToWorldMatrix.MultiplyPoint(spline[spline.Count - 1].Position);
                        foreach (var anchor in intersection.connectionPoints)
                        {
                            if (Vector3.Distance(anchor.connector.transform.position, wsp) < grabDistance)
                            {
                                if (anchor.container == null)
                                {
                                    anchor.front = false;
                                    anchor.container = splineContainer;
                                    anchor.road = this;
                                    endConnector = anchor;
                                    anchor.owner = intersection;

#if UNITY_EDITOR
                                    dirty = true;
#endif
                                }
                                needsUpdate = true;
                            }
                            else if (allowDisconnnect && anchor.container == splineContainer && anchor.front == false)
                            {
                                anchor.container = null;
                                anchor.road = null;
                                endConnector = null;
                                anchor.road = null;
#if UNITY_EDITOR
                                dirty = true;
#endif
                            }
                        }
                    }
                    if (needsUpdate)
                    {
                        intersection.UpdateConnections(systemRoot);
                    }

#if UNITY_EDITOR
                    if (dirty)
                    {
                        UnityEditor.EditorUtility.SetDirty(intersection);
                    }
#endif
                }
            }
            Profiler.EndSample();

            
        }



#if UNITY_EDITOR
        private void OnDestroy()
        {
            CleanupMeshes();
            if (MicroVerse.instance != null && Application.isPlaying == false)
            {
                var rs = GetComponentInParent<RoadSystem>();
                if (rs != null)
                {
                    rs.UpdateSystem(null);
                }
            }
        }
#endif

        void CleanupMeshes()
        {
            Profiler.BeginSample("Cleanup Spline Road");

            int meshNulls = 0;
            int childNulls = 0;
            foreach (var m in meshes)
            {
                if (m != null)
                    DestroyImmediate(m);
                else
                    meshNulls++;
            }

            foreach (var c in children)
            {
                if (c != null)
                    DestroyImmediate(c);
                else
                    childNulls++;
            }

            // We lost references, need to check children
            // This happens when a RoadSystem - Road is part of a Prefab
            // Pressing Undo clears these references
            if ((meshNulls == meshes.Count || childNulls == children.Count) && transform.childCount > 0)
            {
                for(int i = 0; i < transform.childCount; i++)
                {
                    Transform child = transform.GetChild(i);
                    // Not our object
                    if(child.name != instanceName)
                    {
                        continue;
                    }

                    var allTrans = child.GetComponentsInChildren<Transform>();
                    foreach (Transform t in allTrans)
                    {
                        MeshFilter mf = t.GetComponent<MeshFilter>();

                        if (mf != null && mf.sharedMesh.name == instanceName)
                            meshes.Add(mf.sharedMesh);
                    }

                    children.Add(child.gameObject);
                }

                CleanupMeshes();
            }

            children.Clear();
            meshes.Clear();
            Profiler.EndSample();
        }


        public static void SetHideFlags(Object o, RoadSystem rs)
        {
            if (o == null) return;
            if (rs == null)
            {
                o.hideFlags = HideFlags.None;
            }
            else if (rs.generateAtLoad)
            {
                if (rs.hideGameObjects)
                    o.hideFlags = HideFlags.HideAndDontSave;
                else
                    o.hideFlags = HideFlags.DontSave;
            }
            else
            {
                if (rs.hideGameObjects)
                    o.hideFlags = HideFlags.HideInHierarchy;
                else
                    o.hideFlags = HideFlags.None;
            }
        }

        struct BendMeshData
        {
            public GameObject owner;
            public MeshFilter mf;
            public MeshCollider mc;
            public Mesh mesh;
            public float start;
            public float range;
            public float meshLength;
            public float scale;
            public int orient;
            public NativeArray<CacheSplineJob.PosQuat> posQuats;
            public JobHandle cacheSplineJob;
            public BendRules.CullMode cullMode;
            public Vector2 globalScaleBegin;
            public Vector2 globalScaleEnd;
        }

        void BendMesh(BendMeshData bd)
        {
            if (bd.mesh.isReadable)
            {
                Profiler.BeginSample("Mesh Cache Management");
                NativeArray<Vector3> vertices;
                NativeArray<Vector3> normals;
                NativeArray<Vector4> tangents;

                MeshCacheData cacheData;
                if (meshCache.ContainsKey(bd.mesh))
                {
                    cacheData = meshCache[bd.mesh];
                    vertices = cacheData.vertices;
                    normals = cacheData.normals;
                    tangents = cacheData.tangents;
                }
                else
                {
                    int count = bd.mesh.vertexCount;
                    vertices = new NativeArray<Vector3>(count, Allocator.Persistent, NativeArrayOptions.UninitializedMemory);
                    vertices.CopyFrom(bd.mesh.vertices);
                    var meshNormals = bd.mesh.normals;
                    if (meshNormals != null && meshNormals.Length == count)
                    {
                        normals = new NativeArray<Vector3>(count, Allocator.Persistent, NativeArrayOptions.UninitializedMemory);
                        normals.CopyFrom(meshNormals);
                    }
                    else
                    {
                        normals = new NativeArray<Vector3>(1, Allocator.Persistent, NativeArrayOptions.UninitializedMemory);
                    }
                    var meshTangent = bd.mesh.tangents;
                    if (meshTangent != null && meshTangent.Length == count)
                    {
                        tangents = new NativeArray<Vector4>(count, Allocator.Persistent, NativeArrayOptions.UninitializedMemory);
                        tangents.CopyFrom(meshTangent);
                    }
                    else
                    {
                        tangents = new NativeArray<Vector4>(1, Allocator.Persistent);
                    }

                    cacheData = new MeshCacheData() { vertices = vertices, normals = normals, tangents = tangents };
                    meshCache.Add(bd.mesh, cacheData);
                }
                Profiler.EndSample();
                Profiler.BeginSample("Alloc Native Arrays");
                NativeArray<Bounds> bounds = new NativeArray<Bounds>(1, Allocator.TempJob, NativeArrayOptions.UninitializedMemory);
                NativeArray<Vector4> tangs = new NativeArray<Vector4>(tangents.Length, Allocator.TempJob, NativeArrayOptions.UninitializedMemory);
                NativeArray<Vector3> norms = new NativeArray<Vector3>(normals.Length, Allocator.TempJob, NativeArrayOptions.UninitializedMemory);
                NativeArray<Vector3> verts = new NativeArray<Vector3>(vertices.Length, Allocator.TempJob, NativeArrayOptions.UninitializedMemory);
                Profiler.EndSample();
                Profiler.BeginSample("CopyFrom");
                verts.CopyFrom(vertices);
                norms.CopyFrom(normals);
                tangs.CopyFrom(tangents);
                Profiler.EndSample();

                Profiler.BeginSample("Schedule Bends");
                var old = splineContainer.transform.position;
                splineContainer.transform.position = Vector3.zero;

                BendVertexJob bj = new BendVertexJob()
                {
                    localToWorld = bd.owner.transform.localToWorldMatrix,
                    worldToLocal = bd.owner.transform.worldToLocalMatrix,
                    start = bd.start,
                    range = bd.range,
                    meshLength = bd.meshLength,
                    meshScale = bd.scale,
                    orientation = bd.orient,
                    positions = verts,
                    normals = norms,
                    tangents = tangs,
                    posQuats = bd.posQuats,
                    bounds = bounds,
                    allowRoll = allowRoll,
                    cullingMode = bd.cullMode,
                    globalScaleBegin = bd.globalScaleBegin,
                    globalScaleEnd = bd.globalScaleEnd,
                    localPos = bd.owner.transform.localPosition
                };

                splineContainer.transform.position = old;
                VertexJobHolder jh = new VertexJobHolder();
                bendJobs.Add(jh);
                jh.meshCollider = bd.mc;
                jh.cacheData = cacheData;
                jh.bendJob = bj;
                jh.meshFilter = bd.mf;
                jh.mesh = bd.mesh;
                jh.bendHandle = bj.Schedule(bd.cacheSplineJob);
                Profiler.EndSample();
                
            }
            else
            {
                Debug.LogError("Mesh : " + bd.mesh.name + " is not set to read-write, cannot bend");
            }
        }

        void Bend(GameObject prefab, float start, float range,
            float meshLength, float scale, int orient, float curLength, float totalLength,
            ObjJobHolder objJobHolder,
            NativeArray<CacheSplineJob.PosQuat> posQuats,
            JobHandle cacheSplineJob, ref Unity.Mathematics.Random random, RoadSystem roadSystem)
        {
            LinearObjectRules[] lors = prefab.GetComponentsInChildren<LinearObjectRules>();
            if (lors != null && lors.Length > 0)
            {
                if (start <= 0)
                {
                    foreach (var lor in lors)
                    {
                        Spline spline = splineContainer[0];
                        Vector3 offset = lor.transform.position;
                        offset[orient] = 0;
                        ObjSpawnJobLinear job = new ObjSpawnJobLinear()
                        {
                            beginOffset = lor.beginOffset,
                            linearDistance = lor.linearDistance,
                            offset = offset,
                            spline = nspline,
                            positions = new NativeList<float3>(32, Allocator.TempJob),
                            quaternions = new NativeList<quaternion>(32, Allocator.TempJob),

                        };


                        var handle = job.Schedule();
                        ObjectSpawnJobLinearHolder holder = new ObjectSpawnJobLinearHolder()
                        {
                            job = job,
                            handle = handle,
                            prefab = lor.gameObject,
                        };
                        spawnLinearJobs.Add(holder);
                    }
                }
                // don't do any further processing as this is a null op on any other start value
                return;
            }


            Profiler.BeginSample("Set Physics");
            MeshCollider[] mcs = prefab.GetComponentsInChildren<MeshCollider>();
            foreach (var mc in mcs)
            {
                mc.enabled = false; // disable physics until save
            }
            Profiler.EndSample();

            Profiler.BeginSample("Instantiate prefabs");

#if false // this breaks multi material instantiation for some reason..
            GameObject prefabInstance = (GameObject)UnityEditor.PrefabUtility.InstantiatePrefab(prefab);
            prefabInstance.transform.localPosition = Vector3.zero;
            prefabInstance.transform.localRotation = Quaternion.identity;
#else

            GameObject prefabInstance = Instantiate(prefab, Vector3.zero, Quaternion.identity);
#endif
            prefabInstance.tag = gameObject.tag;
            prefabInstance.layer = gameObject.layer;

            SetHideFlags(prefabInstance, roadSystem);
            // Can't use tags ^
            // Using this to mark objects spawned by the Road
            // In case of losing references to child objects, this can identify objects spawned by the Road
            // Need to mark them, cause the User might have objects under their Road object
            prefabInstance.name = instanceName;
            children.Add(prefabInstance);
            Profiler.EndSample();

            Profiler.BeginSample("Gather Sub-Objects");
            var allTrans = prefabInstance.transform.GetComponentsInChildren<Transform>();
            Profiler.EndSample();

            
            foreach (var trans in allTrans)
            {
                GameObject go = trans.gameObject;
                MeshFilter mf = go.GetComponent<MeshFilter>();
                MeshCollider mc = go.GetComponent<MeshCollider>();
                BendRules rules = go.GetComponent<BendRules>();
                if (rules == null)
                    rules = go.GetComponentInParent<BendRules>();

                if (rules == null || rules.mode == BendRules.Mode.Bend)
                {
                    Profiler.BeginSample("Create Bend Job");
                    if (!BendRules.ShouldSpawn(rules, curLength, meshLength, totalLength, random))
                    {
                        DestroyImmediate(go);
                        Profiler.EndSample();
                        continue;
                    }
                    if (rules != null && rules.spawnRules.cullingMode == BendRules.CullMode.Cull && curLength < meshLength * rules.spawnRules.requiredLeft)
                    {
                        DestroyImmediate(go);
                        Profiler.EndSample();
                        continue;
                    }
                    var cullMode = BendRules.CullMode.Clamp;
                    if (rules != null)
                    {
                        cullMode = rules.spawnRules.cullingMode;
                    }


                    float2 gss = 1;
                    float2 gse = 1;
                    if (beginConnector != null && beginConnector.owner != null)
                    {
                        gss = new float2(beginConnector.owner.transform.lossyScale.x, beginConnector.owner.transform.lossyScale.y);
                    }
                    if (endConnector != null && endConnector.owner != null)
                    {
                        gse = new float2(endConnector.owner.transform.lossyScale.x, endConnector.owner.transform.lossyScale.y);
                    }
                    BendMeshData bmd = new BendMeshData()
                    {
                        owner = go,
                        mf = mf,
                        mc = mc,
                        start = start,
                        range = range,
                        meshLength = meshLength,
                        scale = scale,
                        orient = orient,
                        posQuats = posQuats,
                        cacheSplineJob = cacheSplineJob,
                        cullMode = cullMode,
                        globalScaleBegin = gss,
                        globalScaleEnd = gse
                    };

                    

                    if (mf != null && mc != null && mf.sharedMesh != null && mc.sharedMesh != null)
                    {
                        // share one mesh
                        if (mf.sharedMesh == mc.sharedMesh)
                        {
                            bmd.mesh = mf.sharedMesh;
                            BendMesh(bmd);
                        }
                        else
                        {
                            bmd.mesh = mf.sharedMesh;
                            bmd.mc = null;
                            BendMesh(bmd);
                            bmd.mc = mc;
                            bmd.mf = null;
                            bmd.mesh = mc.sharedMesh;
                            BendMesh(bmd);
                        }
                        
                    }
                    else if (mf != null && mf.sharedMesh != null) // just a mesh filter
                    {
                        bmd.mc = null;
                        bmd.mesh = mf.sharedMesh;
                        BendMesh(bmd);
                    }
                    else if (mc != null && mc.sharedMesh != null) // just a collider
                    {
                        bmd.mf = null;
                        bmd.mesh = mc.sharedMesh;
                        BendMesh(bmd);
                    }
                    
                    Profiler.EndSample();
                }
                else if (rules.mode != BendRules.Mode.None)
                {
                    if (trans.parent != prefabInstance.transform)
                        continue;

                    Profiler.BeginSample("Append Object To Job");
                    if (!BendRules.ShouldSpawn(rules, curLength, meshLength, totalLength, random))
                    {
                        go.SetActive(false);
                        Profiler.EndSample();
                        continue;
                    }
                    
                    /// this should likely be done in world space instead? Wouldn't have to stop one level deep.
                    var entry = new ObjectSpawnJob.ObjEntry()
                    {
                        position = go.transform.localPosition,
                        start = start,
                        range = range,
                        meshLength = meshLength,
                        bendRule = rules.mode,
                        scaleUniform = rules.placeRules.scaleUniform,
                        positionVariance = rules.placeRules.positionVariance,
                        rotationVariance = rules.placeRules.rotationVariance,
                        scaleVariance = rules.placeRules.scaleVariant,
                        cullingMode = rules.spawnRules.cullingMode,
                    };

                    objJobHolder.entries.Add(entry);
                    objJobHolder.transforms.Add(go.transform);
                    Profiler.EndSample();
                }
            }
            prefabInstance.transform.SetParent(splineContainer.transform);
            prefabInstance.transform.localPosition = Vector3.zero;
            prefabInstance.transform.localRotation = Quaternion.identity;
        }


        public static void ClearCache()
        {
            foreach (var mc in meshCache.Values)
            {
                mc.vertices.Dispose();
                mc.normals.Dispose();
                mc.tangents.Dispose();
            }
            meshCache.Clear();
        }

        


        public void LaunchJobs(RoadSystem rs)
        {
            bendJobs.Clear();
            var spline = splineContainer.Spline;
            if (spline == null || spline.Count < 1)
            {
                return;
            }
            Profiler.BeginSample("Generate Spline Road");
            Profiler.BeginSample("Pre Launch Jobs");

            //UpdateConnections(rs, false);
            CleanupMeshes();

            if (seed < 0)
            {
                seed = UnityEngine.Random.Range(1, 1024);
            }
            var random = new Unity.Mathematics.Random((uint)seed);

            objJobHolder = new ObjJobHolder();

            int orient = (int)config.orientation;

            float4x4 xfm = splineContainer.transform.localToWorldMatrix;
            float length = spline.CalculateLength(xfm);
            float totalLength = length;
            nspline = new NativeSpline(spline, Allocator.TempJob);
            float start = 0;
            Profiler.EndSample();
            // We cache the spline at a 1 meter resolution. Takes 0.25 ms to update 1200 points.
            // Values are interpolated between that resolution.
            Profiler.BeginSample("Cache Spline Job");
            int sampleCount = (int)(totalLength) + 1;
            
            cachePosQuats = new NativeArray<CacheSplineJob.PosQuat>((int)sampleCount, Allocator.TempJob, NativeArrayOptions.UninitializedMemory);
            if (rs != null && rs.systemConfig != null && rs.systemConfig.allowShaping && splineShapes.Count > 0 && splineShapes[0].shapeData.Count > 0)
            {
                var shapes = splineShapes[0].shapeData;

                splineWidthArray = new NativeArray<float3>(shapes.Count, Allocator.TempJob, NativeArrayOptions.UninitializedMemory);
                for (int i = 0; i < shapes.Count; ++i)
                {
                    var v = shapes[i].Value;
                    splineWidthArray[i] = new float3(shapes[i].Index, v.x, v.y);
                }
            }
            else
            {
                splineWidthArray = new NativeArray<float3>(1, Allocator.TempJob, NativeArrayOptions.UninitializedMemory);
                splineWidthArray[0] = 0;
            }

            CacheSplineJob cacheJob = new CacheSplineJob()
            {
                spline = nspline,
                shapeData = splineWidthArray,
                data = cachePosQuats,
                sampleCount = (int)sampleCount
            };
            var cacheSplineHandle = cacheJob.Schedule((int)sampleCount, 64);

            Profiler.EndSample();

            Profiler.BeginSample("Generate Jobs");

            var entry = config.entries[0];
            float scaledEntrySize = entry.size;
            float scale = 1;

            if (config.stretchToFit)
            {
                // we add a tiny bit of length, otherwise we get sliver pieces from floating point rounding errors
                float count = (totalLength + 0.001f) / entry.size + config.stretchToFitBoost;
                scale =  (count / Mathf.Round(count));
                scaledEntrySize *= scale;
            }

            while (length > 0)
            {
                GameObject prefabToSpawn = entry.prefab;
                SplineChoiceData choice = null;
                if (splineOverlayChoices.Count > 0)
                {
                    choice = FindChoiceData(start / totalLength);
                }
                if (choice == null)
                    choice = defaultChoiceData;
                if (choice != null && choice.roadPrefab != null)
                    prefabToSpawn = choice.roadPrefab;

                float start01 = start / totalLength;
                float range = scaledEntrySize / totalLength;

                // do the main road, essentially
                Bend(prefabToSpawn, start01, range, scaledEntrySize, scale,
                    orient, length, totalLength, objJobHolder, 
                    cachePosQuats, cacheSplineHandle, ref random, rs);

                var overlays = config.GetAllOverlays(entry);
                if (choice == null && overlays != null && overlays.Count > 0)
                {
                    for (int i = 0; i < overlays.Count; ++i)
                    {
                        var overlay = overlays[i];
                        if (overlay != null && overlay.prefabs.Length > 0 && overlay.spawnFirstAsDefault)
                        {
                            if (random.NextFloat(1.0f) < overlay.overlayChance)
                            {
                                Bend(overlay.prefabs[0], start01, range, scaledEntrySize, scale,
                                    orient, length, totalLength, objJobHolder,
                                    cachePosQuats, cacheSplineHandle, ref random, rs);
                            }
                        }
                    }
                }
                else if (choice != null)
                {
                    for (int i = 0; i < choice.overlayEntries.Count; ++i)
                    {
                        var e = choice.overlayEntries[i];
                        if (e.prefab == null && e.none == false) // spawn default
                        {
                            var overlay = config.FindOverlay(entry, e.label);
                            if (overlay != null && overlay.prefabs.Length > 0 && overlay.spawnFirstAsDefault)
                            {
                                if (random.NextFloat(1.0f) < overlay.overlayChance)
                                {
                                    Bend(overlay.prefabs[0], start01, range, scaledEntrySize, scale,
                                        orient, length, totalLength, objJobHolder,
                                        cachePosQuats, cacheSplineHandle, ref random, rs);
                                }
                            }
                        }
                        else if (e.prefab != null)
                        {
                            Bend(e.prefab, start01, range, scaledEntrySize, scale,
                                orient, length, totalLength, objJobHolder,
                                cachePosQuats, cacheSplineHandle, ref random, rs);
                        }
                    }
                }
                float size = scaledEntrySize;
                if (size <= 0) size = 1;
                length -= size;
                start += size;

            }

            if (objJobHolder.entries.Count > 0)
            {
                float2 gss = 1;
                float2 gse = 1;
                if (beginConnector != null && beginConnector.owner != null)
                {
                    gss = new float2(beginConnector.owner.transform.lossyScale.x, beginConnector.owner.transform.lossyScale.y);
                }
                if (endConnector != null && endConnector.owner != null)
                {
                    gse = new float2(endConnector.owner.transform.lossyScale.x, endConnector.owner.transform.lossyScale.y);
                }
                objJobHolder.objBendJob = new ObjectSpawnJob()
                {
                    orientation = orient,
                    allowRoll = allowRoll,
                    entries = new NativeArray<ObjectSpawnJob.ObjEntry>(objJobHolder.entries.ToArray(), Allocator.TempJob),
                    posQuats = cachePosQuats,
                    meshLength = length,
                    globalScaleBegin = gss,
                    globalScaleEnd = gse,
                };
                objJobHolder.handle = objJobHolder.objBendJob.Schedule(cacheSplineHandle);
            }

            
            Profiler.EndSample();
            Profiler.EndSample();

        }

        public void CancelJobs()
        {
            if (bendJobs.Count == 0)
                return;
            Profiler.BeginSample("Cancel Bend Jobs");
            for (int i = 0; i < bendJobs.Count; ++i)
            {
                var job = bendJobs[i];
                job.bendHandle.Complete();

                job.bendJob.positions.Dispose();
                job.bendJob.normals.Dispose();
                job.bendJob.tangents.Dispose();
                job.bendJob.bounds.Dispose();
            }

            for (int i = 0; i < spawnLinearJobs.Count; ++i)
            {
                var job = spawnLinearJobs[i];
                job.handle.Complete();

                job.job.positions.Dispose();
                job.job.quaternions.Dispose();
            }
            spawnLinearJobs.Clear();
            bendJobs.Clear();
            nspline.Dispose();
            
            cachePosQuats.Dispose();
            splineWidthArray.Dispose();
            objJobHolder.objBendJob.entries.Dispose();

            Profiler.EndSample();
        }

        public void ProcessJobs(RoadSystem rs)
        {
            Profiler.BeginSample("Process Jobs");

            if (objJobHolder != null && objJobHolder.entries.Count > 0)
            {
                Profiler.BeginSample("Object Job");
                objJobHolder.handle.Complete();

                for (int i = 0; i < objJobHolder.entries.Count; ++i)
                {
                    var entry = objJobHolder.objBendJob.entries[i];
                    var trans = objJobHolder.transforms[i];
                    trans.localPosition = entry.position;
                    trans.localScale *= entry.scale + 1.0f;

                    var quat = entry.quaternion;
                    if (quat.value.Equals(float4.zero))
                    {
                        trans.gameObject.SetActive(false);
                    }
                    else
                    {
                        trans.transform.localRotation *= quat;
                    }
                }
                objJobHolder.objBendJob.entries.Dispose();
                Profiler.EndSample();
            }

            Profiler.BeginSample("Processing Linear Spawn Jobs");
            for (int i = 0; i < spawnLinearJobs.Count; ++i)
            {
                Profiler.BeginSample("Wait for jobs");
                var job = spawnLinearJobs[i];
                job.handle.Complete();
                Profiler.EndSample();
                Profiler.BeginSample("Instance Linear Objects");
                for (int x = 0; x < job.job.positions.Length; ++x)
                {
                    var quat = job.job.quaternions[x];
                    if (quat.value.Equals(float4.zero))
                    {
                        continue;
                    }
                    var pos = job.job.positions[x];
                    var inst = Object.Instantiate(job.prefab, this.transform);
                    children.Add(inst);
                    inst.name = instanceName;
                    inst.transform.localPosition = pos;
                    inst.transform.localRotation *= quat;
                    SetHideFlags(inst, rs);
                }

                Profiler.EndSample();
                job.job.positions.Dispose();
                job.job.quaternions.Dispose();
            }
            spawnLinearJobs.Clear();
            Profiler.EndSample();

            Profiler.BeginSample("Mesh Bend Jobs");
            for (int i = 0; i < bendJobs.Count; ++i)
            {
                Profiler.BeginSample("Wait for jobs");
                var job = bendJobs[i];
                job.bendHandle.Complete();
                Profiler.EndSample();

                Profiler.BeginSample("Instance Mesh");
                // about 10x the speed of calling Instantiate, and less memory alloc
                Mesh m = new Mesh();
                var meshData = Mesh.AcquireReadOnlyMeshData(job.mesh);
                Mesh.ApplyAndDisposeWritableMeshData(meshData, m);
                SetHideFlags(m, rs);
                Profiler.EndSample();
                Profiler.BeginSample("Set Mesh Data");
                
                m.name = instanceName;
                meshes.Add(m);
                m.SetVertices(job.bendJob.positions);
                if (job.bendJob.positions.Length == job.bendJob.normals.Length)
                {
                    m.SetNormals(job.bendJob.normals);
                }
                if (job.bendJob.positions.Length == job.bendJob.tangents.Length)
                {
                    m.SetTangents(job.bendJob.tangents);
                }
                m.bounds = job.bendJob.bounds[0];
                if (job.meshFilter != null)
                    job.meshFilter.sharedMesh = m;
                if (job.meshCollider != null)
                    job.meshCollider.sharedMesh = m;

                job.bendJob.positions.Dispose();
                job.bendJob.normals.Dispose();
                job.bendJob.tangents.Dispose();
                job.bendJob.bounds.Dispose();
                Profiler.EndSample();


            }
            bendJobs.Clear();
            Profiler.EndSample();

            nspline.Dispose();
            cachePosQuats.Dispose();
            splineWidthArray.Dispose();

            Profiler.EndSample();

            Profiler.BeginSample("Material Override");
            if (rs != null && rs.templateMaterial != null)
            {
                RoadMaterialOverride[] overs = GetComponentsInChildren<RoadMaterialOverride>();
                if (overs != null)
                {
                    foreach (var o in overs)
                    {
                        o.Override(rs.templateMaterial);
                    }
                }
            }
            Profiler.EndSample();
        }

        public void Generate(RoadSystem rs = null, bool updateMS = true)
        {
            if (rs == null)
                rs = GetComponentInParent<RoadSystem>();
            
#if __MICROVERSE__ && __MICROVERSE_SPLINES__
            if (GetComponentInParent<MicroVerse>() != null && updateMS && !Application.isPlaying)
            {
                if (rs != null)
                {
                    SplinePath sp = rs.GetComponent<SplinePath>();
                    if (sp != null && MicroVerse.instance != null)
                    {
                        Bounds b = SplineUtility.GetBounds(splineContainer.Spline, splineContainer.transform.localToWorldMatrix);
                        MicroVerse.instance.AddRoadJob(this, rs, b);
                        MicroVerse.instance.Invalidate(sp.GetBounds());
                        return;
                    }
                }
                
            }
#endif

            LaunchJobs(rs);
            ProcessJobs(rs);
        }
    }
}
