
using UnityEngine;
/// ---------------------------------------------
/// Ultimate Character Controller
/// Copyright (c) Opsive. All Rights Reserved.
/// https://www.opsive.com
/// ---------------------------------------------

namespace Opsive.UltimateCharacterController.Game
{
    /// <summary>
    /// Interface for any moving object that can be smoothed.
    /// </summary>
    public interface ISmoothedObject
    {
        /// <summary>
        /// The index of the object within the Simulation Manager.
        /// </summary>
        int SimulationIndex { set; }

        /// <summary>
        /// The Transform of the object.
        /// </summary>
        Transform Transform { get; }

        /// <summary>
        /// Moves the object during the FixedUpdate loop.
        /// </summary>
        void Move();
    }

    /// <summary>
    /// Interface for any moving object that should implement its own SmoothMove.
    /// </summary>
    public interface ISmoothMover
    {
        /// <summary>
        /// Smoothly interpolates the object during the Update loop.
        /// </summary>
        /// <param name="interpAmount">The amount to interpolate between the smooth and fixed position.</param>
        void SmoothMove(float interpAmount);
    }
}