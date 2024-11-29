using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "roadmatcfg", menuName = "MicroVerse/RoadMaterialConfig")]
public class RoadMaterialConfig : ScriptableObject
{
    [Tooltip("Must match the content id used for the road from the content browser")]
    public string contentID;

    [System.Serializable]
    public class Entry
    {
        public Material material;
        public Texture2D preview;
    }

    public List<Entry> templateMaterials = new List<Entry>();
}
