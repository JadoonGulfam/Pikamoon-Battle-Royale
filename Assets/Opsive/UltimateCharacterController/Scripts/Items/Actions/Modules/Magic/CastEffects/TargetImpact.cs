/// ---------------------------------------------
/// Ultimate Character Controller
/// Copyright (c) Opsive. All Rights Reserved.
/// https://www.opsive.com
/// ---------------------------------------------

namespace Opsive.UltimateCharacterController.Items.Actions.Modules.Magic.CastEffects
{
    using Opsive.Shared.Utility;
    using UnityEngine;

    /// <summary>
    /// Immediately calls impact on the object at the target position.
    /// </summary>
    [System.Serializable]
    public class TargetImpact : MagicMultiTargetCastEffectModule
    {
        [Tooltip("The gizmo settings are used to draw gizmos.")]
        [SerializeField] protected GizmoSettings m_GizmoSettings = new GizmoSettings(false,true, Color.red);

        protected bool m_DrawGizmos;
        protected Vector3 m_Origin;
        protected Vector3 m_Direction;
        
        /// <summary>
        /// Initialize the module.
        /// </summary>
        protected override void InitializeInternal()
        {
            base.InitializeInternal();
            MagicAction.OnDrawGizmosHybridEvent += OnDrawGizmosHybrid;
        }

        /// <summary>
        /// Draw the gizmos to show the detection area.
        /// </summary>
        /// <param name="onSelected">Show gizmos on select?</param>
        private void OnDrawGizmosHybrid(bool onSelected)
        {
            if (m_GizmoSettings.SetGizmoSettings(onSelected) || !m_DrawGizmos) {
                return;
            }
            
            m_DrawGizmos = false;

            Gizmos.DrawRay(m_Origin, m_Direction * float.MaxValue);
        }
        
        /// <summary>
        /// Performs the cast.
        /// </summary>
        /// <param name="useDataStream">The use data stream, contains the cast data.</param>
        protected override void DoCastInternal(MagicUseDataStream useDataStream)
        {
            m_DrawGizmos = true;

            m_CastID = (uint)useDataStream.CastData.CastID;
            m_Direction = useDataStream.CastData.Direction.normalized;
            m_Origin = useDataStream.CastData.CastPosition - m_Direction.normalized * 0.1f;

            if (Physics.Raycast(m_Origin, m_Direction, out var hit, float.MaxValue, useDataStream.CastData.DetectLayers)) {
                MagicAction.PerformImpact(m_CastID, GameObject, hit.transform.gameObject, hit);
            } else {
                MagicAction.ResetImpactModules(m_CastID);
            }
        }

        /// <summary>
        /// Stops the cast.
        /// </summary>
        public override void StopCast()
        {
            for (int i = 0; i < MagicAction.ImpactModuleGroup.EnabledModules.Count; i++) {
                var module = MagicAction.ImpactModuleGroup.EnabledModules[i];
                if(module == null){ continue; }
                module.Reset(m_CastID);
            }

            base.StopCast();
        }
    }
}