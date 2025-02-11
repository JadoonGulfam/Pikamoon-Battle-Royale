using DG.Tweening;
using System.Collections.Generic;
using UnityEngine;

namespace Pikamoon.Controller
{
    public class RangedWeapon : Weapon
    {

        [Space]
        [Header("Core Class Value")]

        public Transform FirePoint;

        [Space]
        [Header("Bullet")]
        [SerializeField] Transform BulletsParent;
        public int MaxPoolSize;
        List<Bullet> bulletsPool;


        [Space]
        [SerializeField] Transform DebugTransform;
        [SerializeField] float Value;

        bool isReturning;
        bool activated;

        Vector3 _distantPoint;
        Transform _curvePoint;
        Transform _throwingOrigin;

        float returnTime;
        int bulletIndex;

        IDamageable damageable;

        RangedWeaponDataSO rWeaponData;

        private void Awake()
        {
            rWeaponData = GetWeaponDataAs<RangedWeaponDataSO>();

            returnTime = 0;
            activated = isReturning = false;
            MakePool(rWeaponData.Bullet);
        }

        public void ShootBullet(Vector3 hit)
        {
            GetBulletIndex();

            bulletsPool[bulletIndex].Shoot(FirePoint.position, hit, rWeaponData.BulletSpeed, rWeaponData.BulletDamage);
            DebugTransform.transform.position = hit;
        }

        void GetBulletIndex()
        {
            bulletIndex++;
            if (bulletIndex == bulletsPool.Count)
            {
                bulletIndex = 0;
            }
        }


        void MakePool(Bullet bullet)
        {
            bulletsPool = new List<Bullet>();
            bulletsPool.Clear();
            bulletIndex = 0;

            for (int i = 0; i < MaxPoolSize; ++i)
            {
                var _bllt = Instantiate(bullet, null);

                _bllt.transform.parent = null;

                _bllt.Initialize(this, i);

                bulletsPool.Add(_bllt);
            }
        }


        public float GetFireRate()
        {
            return rWeaponData.DelayInNextFire;
        }

        public override void OnPicked()
        {
        }

        public override void OnPicked(Transform Picker)
        {
        }

        public override void Drop()
        {
        }

        public override void Equip()
        {
            foreach (var collider in _colliders)
            {
                collider.enabled = false;

            }
        }
        public override void UnEquip()
        {
            foreach (var collider in _colliders)
            {
                collider.enabled = true;
            }
        }

        public override WeaponInfo GetWeaponInfo()
        {
            WeaponInfo info = new WeaponInfo();

            info.Prefab = this;
            info.Data = rWeaponData;

            return info;
        }
    }
}