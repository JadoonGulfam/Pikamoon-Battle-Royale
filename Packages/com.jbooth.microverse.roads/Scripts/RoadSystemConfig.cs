using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JBooth.MicroVerseCore
{
    [CreateAssetMenu(fileName = "road", menuName = "MicroVerse/RoadSystemConfig")]
    public class RoadSystemConfig : ScriptableObject
    {
        [Tooltip("Naming Prefix for any road segment")]
        public string namePrefix = "Road";

        [Tooltip("When true, the user can change the width and height of the spline data")]
        public bool allowShaping = false;
        [Tooltip("Sets a minimum size for the distance of handles. Default is 1, but on large objects you may want it higher")]
        public float shapingSizeHandleStart = 5;

        [Tooltip("If set, this will be the config we use when spline painting from the content browser. This should be your default straight segment's config")]
        public RoadConfig splinePaintDefault;

        [Tooltip("When creating via spline tools, do we modify the terrain by defualt?")]
        public bool modifyTerrainByDefault = true;

    }
}
