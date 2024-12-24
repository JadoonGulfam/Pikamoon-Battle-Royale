// Cristian Pop - https://boxophobic.com/

using UnityEngine;
using Boxophobic.StyledGUI;

namespace TheVisualEngine
{
#if UNITY_EDITOR
    [HelpURL("https://docs.google.com/document/d/145JOVlJ1tE-WODW45YoJ6Ixg23mFc56EnB_8Tbwloz8/edit#heading=h.q4fstlrr3cw4")]
    [ExecuteInEditMode]
    [AddComponentMenu("BOXOPHOBIC/The Visual Engine/TVE Prefab")]
#endif
    public class TVEPrefab : StyledMonoBehaviour
    {
#if UNITY_EDITOR
        [StyledBanner(0.890f, 0.745f, 0.309f, "Prefab")]
        public bool styledBanner;

#if !THE_VISUAL_ENGINE_DEBUG
        [HideInInspector]
#endif
        public string storedPrefabBackupGUID = "";
#if !THE_VISUAL_ENGINE_DEBUG
        [HideInInspector]
#endif
        public string storedPreset = "";
#if !THE_VISUAL_ENGINE_DEBUG
        [HideInInspector]
#endif
        public string storedOverrides = "";
#endif
    }
}




