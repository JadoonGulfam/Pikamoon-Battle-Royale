/// ---------------------------------------------
/// Ultimate Character Controller
/// Copyright (c) Opsive. All Rights Reserved.
/// https://www.opsive.com
/// ---------------------------------------------

namespace Opsive.UltimateCharacterController.Demo.Objects
{
    using Opsive.UltimateCharacterController.Demo.UI;
    using Opsive.UltimateCharacterController.Game;
    using Opsive.UltimateCharacterController.Utility;
    using UnityEngine;

    /// <summary>
    /// Scene trigger which loads the menu when the character steps in.
    /// </summary>
    public class MenuTrigger : MonoBehaviour
    {
        [Tooltip("The LayerMask of the character.")]
        [SerializeField] protected LayerMask m_LayerMask = 1 << LayerManager.Character;

        private ZoneSelection m_ZoneSelection;

        /// <summary>
        /// Initialize the default values.
        /// </summary>
        private void Awake()
        {
#if UNITY_2023_1_OR_NEWER
            m_ZoneSelection = FindFirstObjectByType<ZoneSelection>();
#else
            m_ZoneSelection = FindObjectOfType<ZoneSelection>();
#endif
        }

        /// <summary>
        /// An object has entered the trigger.
        /// </summary>
        /// <param name="other">The object that entered the trigger.</param>
        private void OnTriggerEnter(Collider other)
        {
            if (!MathUtility.InLayerMask(other.gameObject.layer, m_LayerMask)) {
                return;
            }

            m_ZoneSelection.ShowMenu(true);
        }
    }
}