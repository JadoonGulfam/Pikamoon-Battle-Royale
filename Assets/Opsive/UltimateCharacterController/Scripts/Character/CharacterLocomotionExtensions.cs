/// ---------------------------------------------
/// Ultimate Character Controller
/// Copyright (c) Opsive. All Rights Reserved.
/// https://www.opsive.com
/// ---------------------------------------------

namespace Opsive.UltimateCharacterController.Character
{
    using UnityEngine;

    /// <summary>
    /// Extension methods for the CharacterLocomotion class.
    /// </summary>
    public static class CharacterLocomotionExtensions
    {
        /// <summary>
        /// Transforms the position from local space to world space. This is similar to Transform.TransformPoint.
        /// </summary>
        /// <param name="characterLocomotion">A reference to the CharacterLocomotion.</param>
        /// <param name="localPosition">The local position of the object.</param>
        /// <returns>The world space position.</returns>
        public static Vector3 TransformPoint(this CharacterLocomotion characterLocomotion, Vector3 localPosition)
        {
            return characterLocomotion.Position + (characterLocomotion.Rotation * localPosition);
        }

        /// <summary>
        /// Transforms the position from local space to world space. This is similar to Transform.TransformPoint.
        /// </summary>
        /// <param name="characterLocomotion">A reference to the CharacterLocomotion.</param>
        /// <param name="xPosition">The local x position of the object.</param>
        /// <param name="yPosition">The local y position of the object.</param>
        /// <param name="zPosition">The local z position of the object.</param>
        /// <returns>The world space position.</returns>
        public static Vector3 TransformPoint(this CharacterLocomotion characterLocomotion, float xPosition, float yPosition, float zPosition)
        {
            return TransformPoint(characterLocomotion, new Vector3(xPosition, yPosition, zPosition));
        }

        /// <summary>
        /// Transforms the position from world space to local space. This is similar to Transform.InverseTransformPoint.
        /// </summary>
        /// <param name="characterLocomotion">A reference to the CharacterLocomotion.</param>
        /// <param name="position">The position of the object.</param>
        /// <returns>The local space position.</returns>
        public static Vector3 InverseTransformPoint(this CharacterLocomotion characterLocomotion, Vector3 position)
        {
            var diff = position - characterLocomotion.Position;
            return Quaternion.Inverse(characterLocomotion.Rotation) * diff;
        }

        /// <summary>
        /// Transforms the position from world space to local space. This is similar to Transform.InverseTransformPoint.
        /// </summary>
        /// <param name="characterLocomotion">A reference to the CharacterLocomotion.</param>
        /// <param name="xPosition">The x position of the object.</param>
        /// <param name="yPosition">The y position of the object.</param>
        /// <param name="zPosition">The z position of the object.</param>
        /// <returns>The local space position.</returns>
        public static Vector3 InverseTransformPoint(this CharacterLocomotion characterLocomotion, float xPosition, float yPosition, float zPosition)
        {
            return InverseTransformPoint(characterLocomotion, new Vector3(xPosition, yPosition, zPosition));
        }

        /// <summary>
        /// Transforms the direction from local space to world space. This is similar to Transform.TransformDirection.
        /// </summary>
        /// <param name="characterLocomotion">A reference to the CharacterLocomotion.</param>
        /// <param name="direction">The direction to transform from local space to world space.</param>
        /// <returns>The world space direction.</returns>
        public static Vector3 TransformDirection(this CharacterLocomotion characterLocomotion, Vector3 direction)
        {
            return characterLocomotion.Rotation * direction;
        }

        /// <summary>
        /// Transforms the direction from local space to world space. This is similar to Transform.TransformDirection.
        /// </summary>
        /// <param name="characterLocomotion">A reference to the CharacterLocomotion.</param>
        /// <param name="xDirection">The x direction to transform from local space to world space.</param>
        /// <param name="yDirection">The y direction to transform from local space to world space.</param>
        /// <param name="zDirection">The z direction to transform from local space to world space.</param>
        /// <returns>The world space direction.</returns>
        public static Vector3 TransformDirection(this CharacterLocomotion characterLocomotion, float xDirection, float yDirection, float zDirection)
        {
            return TransformDirection(characterLocomotion, new Vector3(xDirection, yDirection, zDirection));
        }

        /// <summary>
        /// Transforms the direction from world space to local space. This is similar to Transform.InverseTransformDirection.
        /// </summary>
        /// <param name="characterLocomotion">A reference to the CharacterLocomotion.</param>
        /// <param name="direction">The direction to transform from world space to local space.</param>
        /// <returns>The local space direction.</returns>
        public static Vector3 InverseTransformDirection(this CharacterLocomotion characterLocomotion, Vector3 direction)
        {
            return Quaternion.Inverse(characterLocomotion.Rotation) * direction;
        }

        /// <summary>
        /// Transforms the direction from world space to local space. This is similar to Transform.InverseTransformDirection.
        /// </summary>
        /// <param name="characterLocomotion">A reference to the CharacterLocomotion.</param>
        /// <param name="xDirection">The x direction to transform from world space to local space.</param>
        /// <param name="yDirection">The y direction to transform from world space to local space.</param>
        /// <param name="zDirection">The z direction to transform from world space to local space.</param>
        /// <returns>The local space direction.</returns>
        public static Vector3 InverseTransformDirection(this CharacterLocomotion characterLocomotion, float xDirection, float yDirection, float zDirection)
        {
            return InverseTransformDirection(characterLocomotion, new Vector3(xDirection, yDirection, zDirection));
        }
    }
}