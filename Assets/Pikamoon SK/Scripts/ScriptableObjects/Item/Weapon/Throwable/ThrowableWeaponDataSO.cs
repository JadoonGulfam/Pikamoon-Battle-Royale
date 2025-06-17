using UnityEngine;

namespace Pikamoon.Controller
{
    [CreateAssetMenu(fileName = "New Throwable Weapon", menuName = "Pikamoon/Item/Weapon/Create new Throwable Weapon")]
    public class ThrowableWeaponDataSO : WeaponDataSO
    {
        [Header("Core Data")]
        [Space]

        [Space]
        public float Power;
        public float Damage;
        [Space]
        public float DelayInNextFire;
    }
}
