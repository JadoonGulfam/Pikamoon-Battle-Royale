/// ---------------------------------------------
/// Ultimate Character Controller
/// Copyright (c) Opsive. All Rights Reserved.
/// https://www.opsive.com
/// ---------------------------------------------

namespace Opsive.UltimateCharacterController.Items.AnimatorAudioStates
{
    using Opsive.UltimateCharacterController.Character;
    using UnityEngine;

    /// <summary>
    /// The Random state will move from one state to another in a random order.
    /// </summary>
    public class Random : AnimatorAudioStateSelector
    {
        private int m_CurrentIndex = -1;

        /// <summary>
        /// Initializes the selector.
        /// </summary>
        /// <param name="gameObject">The GameObject that the state belongs to.</param>
        /// <param name="characterLocomotion">The character that the state belongs to.</param>
        /// <param name="characterItem">The item that the state belongs to.</param>
        /// <param name="states">The states which are being selected.</param>
        /// <param name="count">The count of states to expect.</param>
        public override void Initialize(GameObject gameObject, UltimateCharacterLocomotion characterLocomotion, CharacterItem characterItem, AnimatorAudioStateSet.AnimatorAudioState[] states, int count)
        {
            base.Initialize(gameObject, characterLocomotion, characterItem, states, count);

            // Call next state so the index will be initialized to a random value.
            NextState();
        }

        /// <summary>
        /// Returns the current state index. -1 indicates this index is not set by the class.
        /// </summary>
        /// <returns>The current state index.</returns>
        public override int GetStateIndex()
        {
            return m_CurrentIndex;
        }

        /// <summary>
        /// Set the new state index.
        /// </summary>
        /// <param name="stateIndex">The new state index.</param>
        public override void SetStateIndex(int stateIndex)
        {
            var size = m_States.Length;
            m_CurrentIndex = stateIndex % size;

            if (m_CurrentIndex < 0) {
                m_CurrentIndex += size;
            }
        }

        /// <summary>
        /// Moves to the next state.
        /// </summary>
        /// <returns>Was the state changed successfully?</returns>
        public override bool NextState()
        {
            var lastIndex = m_CurrentIndex;
            var count = 0;
            var size = m_States.Length;
            if (size == 0) {
                return false;
            }
            do {
                m_CurrentIndex = UnityEngine.Random.Range(0, size);
                ++count;
            } while ((!IsStateValid(m_CurrentIndex) || !m_States[m_CurrentIndex].Enabled) && count <= size);
            var stateChange = count <= size;
            if (stateChange) {
                ChangeStates(lastIndex, m_CurrentIndex);
            }
            return stateChange;
        }
    }
}