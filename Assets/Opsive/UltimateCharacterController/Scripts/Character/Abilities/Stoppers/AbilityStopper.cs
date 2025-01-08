/// ---------------------------------------------
/// Ultimate Character Controller
/// Copyright (c) Opsive. All Rights Reserved.
/// https://www.opsive.com
/// ---------------------------------------------

namespace Opsive.UltimateCharacterController.Character.Abilities.Stoppers
{
    using Opsive.Shared.Input;

    /// <summary>
    /// The AbilityStopper allows a custom object to decide when the ability should stop.
    /// </summary>
    [System.Serializable]
    [UnityEngine.Scripting.Preserve]
    public abstract class AbilityStopper
    {
        protected Ability m_Ability;

        /// <summary>
        /// Initializes the stopper to the specified ability.
        /// </summary>
        /// <param name="ability">The ability that owns the stopper.</param>
        public virtual void Initialize(Ability ability) { m_Ability = ability; }

        /// <summary>
        /// Can the stopper stop the ability?
        /// </summary>
        /// <param name="playerInput">A reference to the input component.</param>
        /// <returns>True if the stopper can stop the ability.</returns>
        public abstract bool CanInputStopAbility(IPlayerInput playerInput);

        /// <summary>
        /// The ability has started.
        /// </summary>
        public virtual void AbilityStarted() { }

        /// <summary>
        /// The ability has stopped running.
        /// </summary>
        public virtual void AbilityStopped() { }

        /// <summary>
        /// The object has been destroyed.
        /// </summary>
        public virtual void OnDestroy() { }
    }
}