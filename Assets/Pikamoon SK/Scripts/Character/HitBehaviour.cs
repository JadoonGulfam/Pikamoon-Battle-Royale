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

    public class HitBehaviour : MonoBehaviour
    {
        [SerializeField] HitPointHolder[] hitPointHolders;
        [SerializeField] Collider[] WeaponColliders;  
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



        public void EnableWeaponHitPoint()
        {
            for (int i = 0; i < WeaponColliders.Length; i++)
            {
                WeaponColliders[i].enabled = true;
            }
        }

        public void DisableWeaponHitPoint()
        {
            for (int i = 0; i < WeaponColliders.Length; i++)
            {
                WeaponColliders[i].enabled = false;
            }
        }



    }
}
