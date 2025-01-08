/// ---------------------------------------------
/// Ultimate Character Controller
/// Copyright (c) Opsive. All Rights Reserved.
/// https://www.opsive.com
/// ---------------------------------------------

namespace Opsive.UltimateCharacterController.Editor.Utility
{
    using System.Collections.Generic;
    using UnityEngine;

    /// <summary>
    /// Small ScriptableObject which maps the AnimationClips that can be replaced.
    /// </summary>
    [CreateAssetMenu(fileName = "Animation Replacements", menuName = "Opsive/Ultimate Character Controller/Utility/Animation Replacements", order = 0)]
    public class AnimationReplacements : ScriptableObject
    {
        [Tooltip("The original Animation Clips.")]
        [SerializeField] protected AnimationClip[] m_Originals;
        [Tooltip("The replacement Aniamtion Clips.")]
        [SerializeField] protected AnimationClip[] m_Replacements;

        private Dictionary<AnimationClip, AnimationClip> m_ReplacementMap = new Dictionary<AnimationClip, AnimationClip>();

        public Dictionary<AnimationClip, AnimationClip> ReplacementMap { get { return m_ReplacementMap; } }

        /// <summary>
        /// Initialize the replacements.
        /// </summary>
        public void Initialize()
        {
            if (m_Originals.Length != m_Replacements.Length) {
                Debug.LogError("Error: The originals array length must match the replacements array length.");
                return;
            }

            m_ReplacementMap.Clear();
            for (int i = 0; i < m_Originals.Length; ++i) { 
                if (m_Originals[i] == null || m_Replacements[i] == null) {
                    continue;
                }

                m_ReplacementMap.Add(m_Originals[i], m_Replacements[i]);
            }
        }
    }
}