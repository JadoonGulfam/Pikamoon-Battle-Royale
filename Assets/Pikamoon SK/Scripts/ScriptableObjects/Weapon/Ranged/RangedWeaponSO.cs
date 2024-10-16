using UnityEngine;

namespace Pikamoon.Controller
{
    [CreateAssetMenu(fileName = "New Ranged Weapon", menuName = "Pikamoon/Weapon/Create new Ranged Weapon")]
    public class RangedWeaponSO : WeaponSO
    {
        public Bullet Bullet;
        [Space]
        public float BulletSpeed;
        public float BulletDamage;
        [Space]
        public float DelayInNextFire;
    }
}

