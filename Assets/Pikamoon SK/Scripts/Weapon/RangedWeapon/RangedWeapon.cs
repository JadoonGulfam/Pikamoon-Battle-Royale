using DG.Tweening;
using System.Collections.Generic;
using UnityEngine;

namespace Pikamoon.Controller
{
    public class RangedWeapon : Weapon
    {

        [Header("Core Data")]
        [Space]

        public Transform FirePoint;

        [Space]
        [Header("Bullet")]
        [SerializeField] Transform BulletsParent;
        public int MaxPoolSize;
        List<Bullet> bulletsPool;


        [Space]
        [SerializeField] Transform DebugTransform;
        [SerializeField] float Value;


        int bulletIndex;


        RangedWeaponDataSO rWeaponData;

        private void Awake()
        {
            rWeaponData = GetItemDataAs<RangedWeaponDataSO>();

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




        #region Parent Imnplementation

        public override WeaponInfo GetWeaponInfo()
        {
            WeaponInfo info = new WeaponInfo();

            info.Prefab = this;
            info.Data = rWeaponData;

            return info;
        }



        public override Transform GetScabbard()
        {
            if(HasScabbard)
            {
                return Scabbard;
            }

            return null;
        }
        public override void PlaceScabbard(Transform parent)
        {
            Scabbard.parent = parent;

            Scabbard.transform.localPosition = Vector3.zero;
            Scabbard.transform.localRotation = Quaternion.identity;
        }


        public override void OnPicked()
        {
            foreach (var collider in _colliders)
            {
                collider.enabled = false;
            }
        }
        public override void OnPicked(Transform Picker)
        {

        }

        public override void TryToPick(InventoryController Picker)
        {
            Picker.PickWeapon(this);
        }

        public override void OnDrop()
        {
        }
        public override void OnDrop(Transform Dropper, LayerMask DropLayer)
        {
            RaycastHit hit;

            if (Physics.Raycast(Dropper.position + (Dropper.forward * 2) + (Vector3.up * 2), Vector3.down, out hit, 5, DropLayer))
            {

                Vector3 pos = hit.point + Vector3.up * 1;


                if (HasScabbard)
                {
                    Scabbard.transform.parent = null;
                    Scabbard.transform.position = pos;
                    Scabbard.transform.rotation = Quaternion.identity;
                    
                    transform.parent = Scabbard;
                    transform.localPosition = Vector3.zero;
                    transform.localRotation = Quaternion.identity;

                }
                else
                {
                    transform.parent = null;
                    transform.position = pos;
                    transform.rotation = Quaternion.identity;
                }

                foreach (var collider in _colliders)
                {
                    collider.enabled = true;
                }
            }
        }



        public override void OnEquip()
        {

        }
        public override void OnUnEquip()
        {

        }

        public override void OnHit(Vector3 point)
        {
        }



        #endregion






    }
}