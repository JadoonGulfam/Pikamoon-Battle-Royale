using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Mathematics;
using Unity.Jobs;
using Unity.Burst;
using Unity.Collections;
using UnityEngine.Splines;
using System.Runtime.CompilerServices;

namespace JBooth.MicroVerseCore
{

    [BurstCompile]
    public struct ObjSpawnJobLinear : IJob
    {
        [ReadOnly] public float linearDistance;
        [ReadOnly] public float beginOffset;
        [ReadOnly] public NativeSpline spline;
        [ReadOnly] public float3 offset;

        [WriteOnly] public NativeList<float3> positions;
        [WriteOnly] public NativeList<quaternion> quaternions;

        void Evaluate(float t, out float3 pos, out quaternion quat)
        {
            // slow as shit, but super accurate path
            spline.Evaluate(t, out pos, out var tangent, out var up);
            tangent = math.normalizesafe(tangent);
            up = math.normalizesafe(up);
            quat = quaternion.LookRotation(tangent, up);

        }

        void GetPointAtLinearDistance(
        float fromT,
        float relativeDistance,
        out float resultPointT,
        out float3 position,
        out quaternion quat)
        {
            const float epsilon = 0.001f;
            if (fromT < 0)
            {
                resultPointT = 0f;
                spline.Evaluate(0, out position, out var tangent, out var up);
                tangent = math.normalizesafe(tangent);
                up = math.normalizesafe(up);
                quat = quaternion.LookRotation(tangent, up);
                Matrix4x4 mtx = Matrix4x4.TRS(position, quat, Vector3.one);
                position = mtx.MultiplyPoint(offset);
                return;
            }

            var length = spline.GetLength();
            var lengthAtT = fromT * length;
            float currentLength = lengthAtT;
            if (currentLength + relativeDistance >= length) //relativeDistance >= 0 -> Forward search
            {
                resultPointT = 1f;
                spline.Evaluate(1.0f, out position, out var tangent, out var up);
                tangent = math.normalizesafe(tangent);
                up = math.normalizesafe(up);
                quat = quaternion.LookRotation(tangent, up);
                Matrix4x4 mtx = Matrix4x4.TRS(position, quat, Vector3.one);
                position = mtx.MultiplyPoint(offset);
                return;
            }
            else if (currentLength + relativeDistance <= 0) //relativeDistance < 0 -> Forward search
            {
                resultPointT = 0f;
                spline.Evaluate(0, out position, out var tangent, out var up);
                tangent = math.normalizesafe(tangent);
                up = math.normalizesafe(up);
                quat = quaternion.LookRotation(tangent, up);
                Matrix4x4 mtx = Matrix4x4.TRS(position, quat, Vector3.one);
                position = mtx.MultiplyPoint(offset);
                return;
            }

            float3 curPos;
            Evaluate(fromT, out curPos, out quat);
            {
                Matrix4x4 mtx = Matrix4x4.TRS(curPos, quat, Vector3.one);
                curPos = mtx.MultiplyPoint(offset);
            }
            var point = curPos;
            resultPointT = fromT;

            var forwardSearch = relativeDistance >= 0;
            var residual = math.abs(relativeDistance);
            float linearDistance;
            while (residual > epsilon && (forwardSearch ? resultPointT < 1f : resultPointT > 0))
            {
                currentLength += forwardSearch ? residual : -residual;
                resultPointT = currentLength / length;

                if (resultPointT > 1f) //forward search
                {
                    resultPointT = 1f;
                }
                else if (resultPointT < 0f) //backward search
                {
                    resultPointT = 0f;
                }
                Evaluate(resultPointT, out point, out quat);
                Matrix4x4 mtx = Matrix4x4.TRS(point, quat, Vector3.one);
                point = mtx.MultiplyPoint(offset);
                linearDistance = math.distance(curPos, point);
                residual = math.abs(relativeDistance) - linearDistance;
            }
            position = point;
        }


        public void Execute()
        {
            GetPointAtLinearDistance(0.0f, beginOffset, out var curPosT, out var position, out var quat);

            while (curPosT < 1)
            {
                positions.Add(position);
                quaternions.Add(quat);

                // move forward
                GetPointAtLinearDistance(curPosT, linearDistance, out curPosT, out position, out quat);
            }

        }
    }

    [BurstCompile]
    public struct CacheSplineJob : IJobParallelFor
    {
        public struct PosQuat
        {
            public float3 pos;
            public quaternion quat;
            public float2 scale;
        }

        [ReadOnly] public NativeSpline spline;
        [ReadOnly] public NativeArray<float3> shapeData;
        [ReadOnly] public int sampleCount;
        [WriteOnly] public NativeArray<PosQuat> data;

        
        float2 FindShapeValue(float normalized_t)
        {
            int count = shapeData.Length;
            float3 last = 0;
            int lastIdx = -1;
            for (int idx = 0; idx < count; ++idx)
            {
                var k = shapeData[idx];
                
                if (normalized_t >= k.x) // past current frame
                {
                    lastIdx = idx;
                    last = k;
                }
                else
                {
                    break;
                }
            }

            if (lastIdx >= 0)
            { 
                float r;
                float2 b = 0;
                float2 a = shapeData[lastIdx].yz;
                if (lastIdx < count - 1) // do we have a next frame?
                {
                    b = shapeData[lastIdx + 1].yz;
                    r = Mathf.InverseLerp(last.x, shapeData[lastIdx + 1].x, normalized_t);
                }
                else
                {
                    r = Mathf.InverseLerp(last.x, 1, normalized_t);
                }
                return math.lerp(a, b, r);
            }
            
            float t = Mathf.InverseLerp(0, shapeData[0].x, normalized_t);
            return math.lerp(new float2(0, 0), shapeData[0].yz, t);
            
        }
        
        
        public void Execute(int i)
        {
            
            float sVal = ((float)i) / (sampleCount - 1);
            spline.Evaluate(sVal, out var pos, out var dir, out var up);
            up = math.normalizesafe(up);
            dir = math.normalizesafe(dir);
            PosQuat pq = new PosQuat();
            pq.pos = pos;
            pq.quat = quaternion.LookRotation(dir, up);
            pq.scale = FindShapeValue(sVal);
            data[i] = pq;
        }
    }

    class ObjectSpawnJobLinearHolder
    {
        public ObjSpawnJobLinear job;
        public JobHandle handle;
        public GameObject prefab;
    }

    class QuatUtil
    {
        public static float3 ExtractForward(quaternion q)
        {
            float4 v = q.value;
            float x = 2 * (v.x * v.z + v.w * v.y);
            float y = 2 * (v.y * v.z - v.w * v.x);
            float z = 1 - 2 * (v.x * v.x + v.y * v.y);
            return new float3(x, y, z);
        }

        public static float3 ExtractUp(quaternion q)
        {
            float4 v = q.value;
            float x = 2 * (v.x * v.y - v.w * v.z);
            float y = 1 - 2 * (v.x * v.x + v.z * v.z);
            float z = 2 * (v.y * v.z + v.w * v.x);
            return new float3(x, y, z);
        }

        public static float3 ExtractLeft(quaternion q)
        {
            float4 v = q.value;
            float x = 1 - 2 * (v.y * v.y + v.z * v.z);
            float y = 2 * (v.x * v.y + v.w * v.z);
            float z = 2 * (v.x * v.z - v.w * v.y);
            return new float3(x, y, z);
        }
    }

    [BurstCompile]
    public struct BendVertexJob : IJob
    {
        [ReadOnly] public Matrix4x4 localToWorld;
        [ReadOnly] public Matrix4x4 worldToLocal;
        [ReadOnly] public NativeArray<CacheSplineJob.PosQuat> posQuats;
        [ReadOnly] public float start;
        [ReadOnly] public float range;
        [ReadOnly] public float meshLength;
        [ReadOnly] public float meshScale;
        [ReadOnly] public int orientation;
        [ReadOnly] public bool allowRoll;
        [ReadOnly] public BendRules.CullMode cullingMode;
        [ReadOnly] public float2 globalScaleBegin;
        [ReadOnly] public float2 globalScaleEnd;
        [ReadOnly] public float3 localPos;
        
        public NativeArray<Vector3> positions;
        public NativeArray<Vector3> normals;
        public NativeArray<Vector4> tangents;
        public NativeArray<Bounds> bounds;


        Matrix4x4 ExtractRotationFromMatrix(Matrix4x4 matrix)
        {
            Matrix4x4 rotationMatrix = matrix;
            rotationMatrix.m03 = 0; // Clear the translation in the last column
            rotationMatrix.m13 = 0;
            rotationMatrix.m23 = 0;
            return rotationMatrix;
        }

        public void Execute()
        {
            float halfMeshLength = meshLength * 0.5f;
            int sampleCount = posQuats.Length;
            float3 min = float.MaxValue;
            float3 max = float.MinValue;

            int antiOrient = 0;
            if (orientation == 0)
                antiOrient = 2;
            
            for (int i = 0; i < positions.Length; ++i)
            {
                float3 vert = positions[i];
                vert[orientation] *= meshScale;

                // we have to convert to world to get our sVal
                float3 worldVert = localToWorld.MultiplyPoint(vert);
                float sVal = ((worldVert[orientation] + halfMeshLength) / meshLength);
                
                float2 gscale = math.lerp(globalScaleBegin, globalScaleEnd, math.saturate(sVal * range + start));
                var lp = ExtractRotationFromMatrix(localToWorld).MultiplyPoint(localPos);
                vert[antiOrient] += lp[antiOrient];
                vert[antiOrient] *= gscale.x;
                vert[antiOrient] -= lp[antiOrient];
                
                vert.y *= gscale.y;
                

                // reconvert to world space after scale modification is applied
                worldVert = localToWorld.MultiplyPoint(vert);

                if (cullingMode == BendRules.CullMode.Clamp)// && meshScale == 1)
                    sVal = math.saturate(sVal);
                sVal *= range;
                sVal += start;
                float r = sVal * (sampleCount - 1);
                float fr = math.frac(r);
                int fl = math.clamp((int)math.floor(r), 0, sampleCount - 1);
                int fl1 = math.clamp(fl + 1, 0, sampleCount - 1);
                var pq0 = posQuats[fl];
                var pq1 = posQuats[fl1];
                float3 pos = math.lerp(pq0.pos, pq1.pos, fr);
                
                quaternion quat = math.slerp(pq0.quat, pq1.quat, fr);
                float2 scale = math.lerp(pq0.scale, pq1.scale, fr);

                if (!allowRoll)
                {
                    float3 forward = QuatUtil.ExtractForward(quat);
                    float3 up = new float3(0, 1, 0);
                    quat = quaternion.LookRotation(forward, up);
                }
                if (cullingMode == BendRules.CullMode.Overflow)
                {
                    // don't clip, project forward..
                    if (sVal < 0)
                    {
                        pos -= QuatUtil.ExtractForward(quat) * (math.abs(sVal) * meshLength);
                    }
                    if (sVal > 1)
                    {
                        pos += QuatUtil.ExtractForward(quat) * ((sVal - 1) * meshLength);
                    }
                }

                worldVert[orientation] = 0;

                if (orientation == 0)
                    worldVert.z *= scale.x;
                else
                    worldVert.x *= 1 + (scale.x / meshLength);
                worldVert.y *= 1 + (scale.y / meshLength);

                worldVert = math.mul(quat, worldVert);
                worldVert += pos;

                
                if (normals.Length == positions.Length)
                {
                    normals[i] = localToWorld.MultiplyVector(normals[i]);
                    normals[i] = math.mul(quat, normals[i]);
                    normals[i] = worldToLocal.MultiplyVector(normals[i]);
                }
                if (tangents.Length == positions.Length)
                {
                    var tang = tangents[i];
                    float3 ntang = new float3(tang.x, tang.y, tang.z);
                    ntang = localToWorld.MultiplyVector(ntang);
                    ntang = math.mul(quat, ntang);
                    ntang = worldToLocal.MultiplyVector(ntang);
                    tangents[i] = new Vector4(ntang.x, ntang.y, ntang.z, tang.w);
                }
                vert = worldToLocal.MultiplyPoint(worldVert);
                min = math.min(min, vert);
                max = math.max(max, vert);
                positions[i] = vert;

            }
            float3 size = max - min;
            bounds[0] = new Bounds(min + size * 0.5f, size);
        }
    }

    [BurstCompile]
    public struct ObjectSpawnJob : IJob
    {
        public struct ObjEntry
        {
            [ReadOnly] public float start;
            [ReadOnly] public float range;
            [ReadOnly] public float meshLength;
            [ReadOnly] public BendRules.Mode bendRule;
            [ReadOnly] public float3 positionVariance;
            [ReadOnly] public float3 rotationVariance;
            [ReadOnly] public float3 scaleVariance;
            [ReadOnly] public bool scaleUniform;
            [ReadOnly] public float chance;
            [ReadOnly] public BendRules.CullMode cullingMode;
            

            public Vector3 position;
            [WriteOnly] public quaternion quaternion;
            [WriteOnly] public float3 scale;
        }

        [ReadOnly] public NativeArray<CacheSplineJob.PosQuat> posQuats;
        [ReadOnly] public float meshLength;
        [ReadOnly] public int orientation;
        [ReadOnly] public bool allowRoll;
        [ReadOnly] public float2 globalScaleBegin;
        [ReadOnly] public float2 globalScaleEnd;
        


        public NativeArray<ObjEntry> entries;


        public void Execute()
        {
            int antiOrient = 0;
            if (orientation == 0)
                antiOrient = 2;
            var rand = new Unity.Mathematics.Random(45);
            for (int i = 0; i < entries.Length; ++i)
            {
                var entry = entries[i];
                float3 vert = entry.position;
                float sVal = (vert[orientation] + entry.meshLength * 0.5f) / entry.meshLength;
                float2 gscale = math.lerp(globalScaleBegin, globalScaleEnd, math.saturate(sVal * entry.range + entry.start));
                vert[antiOrient] *= gscale.x;
                vert.y *= gscale.y;

                vert[orientation] += entry.meshLength * 0.5f;
                sVal *= entry.range;
                sVal += entry.start;

                float r = sVal * (posQuats.Length - 1);
                float fr = math.frac(r);
                int fl = math.clamp((int)math.floor(r), 0, posQuats.Length - 1);
                int fl1 = math.clamp(fl + 1, 0, posQuats.Length - 1);
                var pq0 = posQuats[fl];
                var pq1 = posQuats[fl1];
                float3 pos = math.lerp(pq0.pos, pq1.pos, fr);
                quaternion quat = math.slerp(pq0.quat, pq1.quat, fr);
                float2 scale = math.lerp(pq0.scale, pq1.scale, fr);

                if (entry.cullingMode == BendRules.CullMode.Overflow)
                {
                    if (sVal < 0)
                    {
                        pos -= QuatUtil.ExtractForward(quat) * (math.abs(sVal) * meshLength);
                    }
                    if (sVal > 1)
                    {
                        pos += QuatUtil.ExtractForward(quat) * ((sVal - 1) * meshLength);
                    }
                }

                //spline.Evaluate(sVal, out var pos, out var dir, out var up);
                var up = QuatUtil.ExtractUp(quat);
                var dir = QuatUtil.ExtractForward(quat);

                up = math.normalizesafe(up);
                dir = math.normalizesafe(dir);

                vert[orientation] = 0;
                if (orientation == 0)
                    vert.z *= scale.x;
                else
                    vert.x *= 1 + (scale.x / meshLength);
                vert.y *= 1 + (scale.y / meshLength);

                vert = math.mul(quat, vert);
                vert += pos;
                entry.position = vert;

                switch (entry.bendRule)
                {
                    case BendRules.Mode.Place:
                        quat = quaternion.identity;
                        break;
                    case BendRules.Mode.PlaceRotate:
                        var euler = EulerZXY(quat);
                        if(!allowRoll) 
                            euler.z = 0;
                        quat = quaternion.EulerZXY(euler);
                        break;
                    case BendRules.Mode.PlaceRotateNoSlope:
                        // Use World Up direction for "no slope" objects
                        dir.y = 0;
                        if(!allowRoll)
                            quat = quaternion.LookRotation(dir, new float3(0, 1, 0));
                        else
                            quat = quaternion.LookRotation(dir, up);
                        break;
                }

                if ((entry.positionVariance.x > 0) || (entry.positionVariance.y > 0) || (entry.positionVariance.z > 0))
                {
                    entry.position += (Vector3)rand.NextFloat3(-entry.positionVariance, entry.positionVariance);
                }
                float3 rdir = rand.NextFloat3(-entry.rotationVariance, entry.rotationVariance) * 0.01745329251f;
                if (entry.rotationVariance.x > 0)
                {
                    quat = math.mul(quat, quaternion.RotateX(rdir.x));
                }
                if (entry.rotationVariance.y > 0)
                {
                    quat = math.mul(quat, quaternion.RotateY(rdir.y));
                }
                if (entry.rotationVariance.z > 0)
                {
                    quat = math.mul(quat, quaternion.RotateZ(rdir.z));
                }
                if (entry.scaleUniform)
                {
                    entry.scale = rand.NextFloat(-entry.scaleVariance.x, entry.scaleVariance.x);
                }
                else if ((entry.scaleVariance.x > 0) || (entry.scaleVariance.y > 0) || (entry.scaleVariance.z > 0))
                {
                    entry.scale = (Vector3)rand.NextFloat3(-entry.scaleVariance, entry.scaleVariance);
                }
                else
                {
                    entry.scale = 0;
                }
                if (entry.cullingMode == BendRules.CullMode.Cull && sVal >= 1.0f)
                {
                    quat = new quaternion(0, 0, 0, 0); // do not show
                }
                entry.quaternion = quat;

                entries[i] = entry;
            }
        }

        /// <summary>
        /// Taken from Mathematics 1.3.2
        /// Returns the Euler angle representation of the quaternion following the ZXY rotation order.
        /// All rotation angles are in radians and clockwise when looking along the rotation axis towards the origin.
        /// </summary>
        /// <param name="q">The quaternion to convert to Euler angles.</param>
        /// <returns>The Euler angle representation of the quaternion in ZXY order.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float3 EulerZXY(quaternion q)
        {
            const float epsilon = 1e-6f;
            const float cutoff = (1f - 2f * epsilon) * (1f - 2f * epsilon);

            // prepare the data
            var qv = q.value;
            var d1 = qv * qv.wwww * math.float4(2f); //xw, yw, zw, ww
            var d2 = qv * qv.yzxw * math.float4(2f); //xy, yz, zx, ww
            var d3 = qv * qv;
            var euler = Unity.Mathematics.float3.zero;

            var y1 = d2.y - d1.x;
            if (y1 * y1 < cutoff)
            {
                var x1 = d2.x + d1.z;
                var x2 = d3.y + d3.w - d3.x - d3.z;
                var z1 = d2.z + d1.y;
                var z2 = d3.z + d3.w - d3.x - d3.y;
                euler = math.float3(math.atan2(x1, x2), -math.asin(y1), math.atan2(z1, z2));
            }
            else //zxz
            {
                y1 = math.clamp(y1, -1f, 1f);
                var abcd = math.float4(d2.z, d1.y, d2.y, d1.x);
                var x1 = 2f * (abcd.x * abcd.w + abcd.y * abcd.z); //2(ad+bc)
                var x2 = math.csum(abcd * abcd * math.float4(-1f, 1f, -1f, 1f));
                euler = math.float3(math.atan2(x1, x2), -math.asin(y1), 0f);
            }

            return euler.yzx;
        }
    }

    class VertexJobHolder
    {
        public BendVertexJob bendJob;
        public JobHandle bendHandle;
        public MeshCacheData cacheData;

        public MeshFilter meshFilter;
        public MeshCollider meshCollider;
        public Mesh mesh;

    }


    class MeshCacheData
    {
        public NativeArray<Vector3> vertices;
        public NativeArray<Vector3> normals;
        public NativeArray<Vector4> tangents;
    }


    class ObjJobHolder
    {
        public ObjectSpawnJob objBendJob;
        public JobHandle handle;
        public List<Transform> transforms = new List<Transform>(500);
        public List<ObjectSpawnJob.ObjEntry> entries = new List<ObjectSpawnJob.ObjEntry>();
    }
}
