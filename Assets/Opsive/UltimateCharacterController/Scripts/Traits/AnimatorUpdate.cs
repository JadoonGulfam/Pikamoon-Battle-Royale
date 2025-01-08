/// ---------------------------------------------
/// Ultimate Character Controller
/// Copyright (c) Opsive. All Rights Reserved.
/// https://www.opsive.com
/// ---------------------------------------------

namespace Opsive.UltimateCharacterController.Traits
{
    using Opsive.UltimateCharacterController.Game;
    using UnityEngine;

    /// <summary>
    /// Updates the Animator component at a fixed delta time.
    /// </summary>
    [RequireComponent(typeof(Animator))]
    public class AnimatorUpdate : MonoBehaviour, ISmoothedObject, ISmoothMover
    {
        [Tooltip("The children that move as a result of the Animator Update. These children only need to be assigned if the root object does not move.")]
        [SerializeField] protected Transform[] m_MovingChildren;

        private Animator m_Animator;

        private int m_SimulationIndex = -1;

        public int SimulationIndex { set { m_SimulationIndex = value; } }
        public Transform Transform { get { return transform; } }

        private Vector3[] m_SmoothedPositions;
        private Quaternion[] m_SmoothedRotations;
        private Vector3[] m_FixedPositions;
        private Quaternion[] m_FixedRotations;

        /// <summary>
        /// Cache the component references.
        /// </summary>
        private void Awake()
        {
            m_Animator = GetComponent<Animator>();
            m_Animator.enabled = false;

            if (m_MovingChildren != null && m_MovingChildren.Length > 0) {
                m_SmoothedPositions = new Vector3[m_MovingChildren.Length];
                m_SmoothedRotations = new Quaternion[m_MovingChildren.Length];
                m_FixedPositions = new Vector3[m_MovingChildren.Length];
                m_FixedRotations = new Quaternion[m_MovingChildren.Length];

                for (int i = 0; i < m_MovingChildren.Length; ++i) {
                    m_SmoothedPositions[i] = m_FixedPositions[i] = m_MovingChildren[i].position;
                    m_SmoothedRotations[i] = m_FixedRotations[i] = m_MovingChildren[i].rotation;
                }
            }
        }

        /// <summary>
        /// Registers the object with the Simulation Manager.
        /// </summary>
        private void OnEnable()
        {
            m_SimulationIndex = SimulationManager.RegisterSmoothedObject(this);
        }

        /// <summary>
        /// Updates the Animator at a fixed delta time.
        /// </summary>
        public void Move()
        {
            for (int i = 0; i < m_MovingChildren.Length; ++i) {
                m_SmoothedPositions[i] = m_FixedPositions[i];
                m_SmoothedRotations[i] = m_FixedRotations[i];
                m_MovingChildren[i].SetPositionAndRotation(m_SmoothedPositions[i], m_SmoothedRotations[i]);
            }

            m_Animator.Update(Time.deltaTime);

            for (int i = 0; i < m_MovingChildren.Length; ++i) {
                m_FixedPositions[i] = m_MovingChildren[i].position;
                m_FixedRotations[i] = m_MovingChildren[i].rotation;
            }
        }

        /// <summary>
        /// Smoothly interpolates the object during the Update loop.
        /// </summary>
        /// <param name="interpAmount">The amount to interpolate between the smooth and fixed position.</param>
        public void SmoothMove(float interpAmount)
        {
            for (int i = 0; i < m_MovingChildren.Length; ++i) {
                m_MovingChildren[i].SetPositionAndRotation(Vector3.Lerp(m_SmoothedPositions[i], m_FixedPositions[i], interpAmount),
                                                           Quaternion.Slerp(m_SmoothedRotations[i], m_FixedRotations[i], interpAmount));
            }
        }

        /// <summary>
        /// Unregisters the object with the Simulation Manager.
        /// </summary>
        private void OnDisable()
        {
            SimulationManager.UnregisterSmoothedObject(m_SimulationIndex);
        }
    }
}