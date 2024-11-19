using UnityEngine;

namespace Pikamoon.Controller
{
    [CreateAssetMenu(fileName = "New Throwable Weapon", menuName = "Pikamoon/Weapon/Create new Throwable Weapon")]
    public class ThrowableWeaponSO : WeaponSO
    {
        [Space]
        public GameObject weapon;
        [Space]
        public float Power;
        public float Damage;
        [Space]
        public float DelayInNextFire;
    }
}
