using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JBooth.MicroVerseCore
{
    public class BendRules : MonoBehaviour
    {
        public enum Mode
        {
            None = -1,
            Bend = 0,
            Place,
            PlaceRotate,
            PlaceRotateNoSlope,
        }

        [Tooltip("How should objects be affected by the spline\n" +
            "   (Bend) Bend vertices to follow the spline\n" +
            "   (PlaceRotate) Follow spline facing\n" +
            "   (PlaceRotateNoSlope) Do not follow virtical slope\n"
            )]
        public Mode mode = Mode.Bend;

        public enum CapMode
        {
            Always,
            BeginOnly,
            Along,
            EndOnly
        }

        public enum CullMode
        {
            Cull,
            Clamp,
            Overflow
        }

        [System.Serializable]
        public class SpawnRules
        {
            [Range(0, 1)]
            [Tooltip("Chance that this object appears")]
            public float chance = 1.0f;
            [Tooltip("Should this object only appear at the begining or end of the spline, or all along it?")]
            public CapMode capMode = CapMode.Always;
            [Tooltip("How much of the segment needs to be left to place the end object")]
            [Range(0, 1.5f)] public float requiredLeft = 0.25f;
            [Tooltip("Should the object be culled when out of the range of the segment? Or clamped to the edge? Or allowed to flow over the edge?")]
            public CullMode cullingMode = CullMode.Cull;
        }

        [System.Serializable]
        public class PlaceRules
        {
            public Vector3 positionVariance = Vector3.zero;
            [Tooltip("Random Rotation applied to any axis")]
            public Vector3 rotationVariance = Vector3.zero;
            [Tooltip("Random Scale applied to any axis. Beware when using with colliders!")]
            public Vector3 scaleVariant = Vector3.zero;
            [Tooltip("Use X scale as Uniform Scale")]
            public bool scaleUniform = false;
        }

        public SpawnRules spawnRules = new SpawnRules();
        public PlaceRules placeRules = new PlaceRules();


        public static CapMode GetDesiredCapMode(float remainingLength, float meshLength, float totalLength)
        {
            if (remainingLength == totalLength)
            {
                return CapMode.BeginOnly;
            }
            else if (meshLength > remainingLength)
            {
                return CapMode.EndOnly;
            }
            return CapMode.Along;
        }

        public static bool ShouldSpawn(BendRules rules, float curLength, float meshLength, float totalLength, Unity.Mathematics.Random rand)
        {
            if (rules == null) return true;
            if (rand.NextFloat(1.0f) > rules.spawnRules.chance)
                return false;
            if (rules.spawnRules.capMode != CapMode.Always)
            {
                var capMode = BendRules.GetDesiredCapMode(curLength, meshLength, totalLength);
                if (capMode != rules.spawnRules.capMode)
                {
                    return false;
                }
                else if (rules.spawnRules.capMode == CapMode.EndOnly)
                {
                    float left = curLength / meshLength;
                    if (left < rules.spawnRules.requiredLeft)
                    {
                        return false;
                    }
                }
            }
            return true;
        }
    }
}