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

    [System.Serializable]
    public struct WeaponExtensionHitPoint
    {
        public WeaponHitboxExtensionType effectPoint;
        public WeaponHitBoxExtension weaponHitPoint;

    }

    [System.Serializable]
    public enum WeaponHitboxExtensionType
    {
        WeaponExtension_LeftShoulder,
        WeaponExtension_RightShoulder,

        WeaponExtension_LeftElbow,
        WeaponExtension_RightElbow,
    }

    public class HitBehaviour : MonoBehaviour
    {
        [SerializeField] HitPointHolder[] hitPointHolders;
        [SerializeField] WeaponExtensionHitPoint[] WeaponColliders;  
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
            foreach (WeaponExtensionHitPoint holder in WeaponColliders)
            {
                if (holder.weaponHitPoint)
                    holder.weaponHitPoint._collider.enabled = false;
            }
        }

        public void EnableWeaponHitPoint(int index)
        {
            WeaponColliders[index].weaponHitPoint._collider.enabled = true;

        }
        public void EnableWeaponHitPoint(WeaponHitboxExtensionType extension)
        {
            WeaponColliders[(int)extension].weaponHitPoint._collider.enabled = true;

        }

        public void DisableWeaponHitPoint(int index)
        {
            WeaponColliders[index].weaponHitPoint._collider.enabled = false;
        }
        public void DisableWeaponHitPoint(WeaponHitboxExtensionType extension)
        {
            WeaponColliders[(int)extension].weaponHitPoint._collider.enabled = false;

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
