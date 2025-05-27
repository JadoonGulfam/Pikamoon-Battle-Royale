using UnityEngine;

namespace Pikamoon.Controller
{
    [CreateAssetMenu(fileName = "New Ranged Weapon", menuName = "Pikamoon/Item/Weapon/Create new Ranged Weapon")]
    public class RangedWeaponDataSO : WeaponDataSO
    {
        [Space]
        public Bullet Bullet;
        
        [Space]
        public float BulletSpeed;
        public float BulletDamage;
        
        [Space]
        public float DelayInNextFire;
    }
}

