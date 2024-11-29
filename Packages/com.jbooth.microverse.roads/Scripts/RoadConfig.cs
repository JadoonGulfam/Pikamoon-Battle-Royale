using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace JBooth.MicroVerseCore
{
    [CreateAssetMenu(fileName = "road", menuName = "MicroVerse/RoadConfig")]
    public class RoadConfig : ScriptableObject
    {

        [System.Serializable]
        public class Overlay
        {
            [Tooltip("Category for the overlay. Each category can instantiate one prefab from the list")]
            public string label;
            [Tooltip("List of prefabs to choose from")]
            public GameObject[] prefabs;
            [Tooltip("If true, when none is specified the first prefab will be spawned as default. Nothing will spawn if false")]
            public bool spawnFirstAsDefault = true;
            [Tooltip("You can have it only spawn occationally by default")]
            [Range(0, 1)] public float overlayChance = 1;
        }

        [System.Serializable]
        public class Entry
        {
            [Tooltip("Size of piece along primary axis")]
            public float size = 12;
            [Tooltip("prefab to instantiate along the spline")]
            public GameObject prefab;
            [Tooltip("addional prefabs to spawn along the spline, like little scenes")]
            public Overlay[] overlays;

            public Overlay FindOverlay(string name)
            {
                foreach (var oe in overlays)
                {
                    if (oe.label == name)
                        return oe;
                }
                return null;
            }

            public Overlay FindOverlay(string name, GameObject prefab)
            {
                foreach (var oe in overlays)
                {
                    if (oe.label == name)
                    {
                        foreach (var p in oe.prefabs)
                        {
                            if (p == prefab)
                                return oe;
                        }
                    }
                }
                return null;
            }
        }
        public Entry[] entries;
        [Tooltip("Overlays for all Entrys")]
        public Overlay[] sharedOverlays;
        [Tooltip("Orientation of the main axis along the spline.")]
        public Road.Orientation orientation = Road.Orientation.Z;
        [Tooltip("How wide is the model, on average.")]
        [FormerlySerializedAs("roadWidth")]
        public float modelWidth = 8;
        [Tooltip("If true, bendable geometry will be strecthed to fix the average piece size over the spline")]
        public bool stretchToFit;
        [Tooltip("If we're overlapping things on the edges of pieces, you can increase this to allow stuff to overlap more")]
        [Range(0, 5)]
        public float stretchToFitBoost = 0.0f;

        public Overlay FindOverlay(Entry e, string name)
        {
            foreach (var oe in sharedOverlays)
            {
                if (oe.label == name)
                    return oe;
            }
            return e.FindOverlay(name);
        }

        public Overlay FindOverlay(Entry e, string name, GameObject prefab)
        {
            foreach (var oe in sharedOverlays)
            {
                if (oe.label == name)
                {
                    foreach (var p in oe.prefabs)
                    {
                        if (p == prefab)
                            return oe;
                    }
                }
            }
            return e.FindOverlay(name, prefab);
        }

        public List<Overlay> GetAllOverlays(Entry e)
        {
            List<Overlay> overlays = new List<Overlay>();
            if (sharedOverlays != null && sharedOverlays.Length > 0)
                overlays.AddRange(sharedOverlays);
            if (e.overlays != null && e.overlays.Length > 0)
            {
                overlays.AddRange(e.overlays);
            }
            return overlays;
        }

    }
}
