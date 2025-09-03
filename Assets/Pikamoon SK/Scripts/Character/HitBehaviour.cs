using System;
using UnityEngine;


namespace Pikamoon.Controller
{
    [System.Serializable]
    public struct HitPointHolder
    {
        public CombatMoveEffectPoint effectPoint;
        public HitPoint hitPoint;

    }

    //[System.Serializable]
    //public struct WeaponExtensionHitPoint
    //{
    //    public WeaponHitBoxExtension weaponHitBoxExtensions;

    //}

    [System.Serializable]
    public struct ExtensionForWeaponHoldingPoint
    {
        public WeaponHoldingPointType HoldingPoint;
        public WeaponHitBoxExtension[] weaponHitBoxExtensions;
    }


    public class HitBehaviour : MonoBehaviour
    {
        [SerializeField] HitPointHolder[] hitPointHolders;
        [SerializeField] ExtensionForWeaponHoldingPoint[] extensionForWeaponHoldingPoint;


        public void AssignWeaponForExtensionsHitBox(Weapon weapon)
        {
            foreach (var holder in extensionForWeaponHoldingPoint)
            {
                foreach (var item in holder.weaponHitBoxExtensions)
                {
                    item.AssignWeapon(weapon);
                }
            }
        }

        public void UnAssignWeaponForExtensionsHitBox()
        {
            foreach (var holder in extensionForWeaponHoldingPoint)
            {
                foreach (var item in holder.weaponHitBoxExtensions)
                {
                    item.UnAssignWeapon();
                }
            }
        }

        public void DisableAllHitPoints()
        {
            foreach (HitPointHolder holder in hitPointHolders)
            {
                if(holder.hitPoint)
                    holder.hitPoint.collider.enabled = false;
            }
        }

        public void DisableHitPoint(int index)
        {
            hitPointHolders[index].hitPoint.collider.enabled = false;
        }

        public void DisableHitPoint(CombatMoveEffectPoint effectPoint)
        {

            hitPointHolders[(int)effectPoint].hitPoint.collider.enabled = false;
        }


        public void EnableHitPoint(int index)
        {
            hitPointHolders[index].hitPoint.collider.enabled = true;
        }

        public void EnableHitPoint(CombatMoveEffectPoint effectPoint)
        {
            hitPointHolders[(int)effectPoint].hitPoint.collider.enabled = true;
        }


        public void DisableAllWeaponHitPoint()
        {
            foreach (var holder in extensionForWeaponHoldingPoint)
            {
                foreach (var item in holder.weaponHitBoxExtensions)
                {
                    item._collider.enabled = false;
                }
            }
        }

        public void EnableWeaponHitPoint(int index)
        {
            foreach (var item in extensionForWeaponHoldingPoint[index].weaponHitBoxExtensions)
            {
                item._collider.enabled = true;
            }
        }

        public void EnableWeaponHitPoint(WeaponHoldingPointType holdingPoint)
        {
            foreach (var item in extensionForWeaponHoldingPoint[(int)holdingPoint].weaponHitBoxExtensions)
            {
                item._collider.enabled = true;
            }

        }

        public void DisableWeaponHitPoint(int index)
        {
            foreach (var item in extensionForWeaponHoldingPoint[index].weaponHitBoxExtensions)
            {
                item._collider.enabled = false;
            }
        }

        public void DisableWeaponHitPoint(WeaponHoldingPointType holdingPoint)
        {
            foreach (var item in extensionForWeaponHoldingPoint[(int)holdingPoint].weaponHitBoxExtensions)
            {
                item._collider.enabled = false;
            }

        }
        #region Editor Methods
        [ContextMenu("Assign Players To Hit Boxes")]
        void AssignPlayersToHitBoxes()
        {
            for(int i = 0;i < hitPointHolders.Length; i++)
            {
                if(hitPointHolders[i].hitPoint != null)
                    hitPointHolders[i].hitPoint.Attacker = this.transform;
            }
        }

        #endregion

    }
}
