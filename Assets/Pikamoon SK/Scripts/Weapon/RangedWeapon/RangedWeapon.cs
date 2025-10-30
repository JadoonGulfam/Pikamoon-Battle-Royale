using DG.Tweening;
using System.Collections.Generic;
using UnityEngine;

namespace Pikamoon.Controller
{
    public class RangedWeapon : Weapon
    {

        [Header("Core Data")]
        [Space]
        public Animator _animator;
        int PullHash;
        int ShootHash;


        [Space]
        public Transform FirePoint;

        [Space]
        [Header("Bullet")]
        [SerializeField] Transform BulletsParent;
        public int MaxPoolSize;
        List<Bullet> bulletsPool;
        Bullet ActiveBullet;

        [Space]
        [SerializeField] Transform DebugTransform;
        [SerializeField] float Value;


        int bulletIndex;


        RangedWeaponDataSO rWeaponData;



        private void Awake()
        {
            rWeaponData = GetItemDataAs<RangedWeaponDataSO>();

            MakePool(rWeaponData.Bullet);

            PullHash = Animator.StringToHash("Pull");
            ShootHash = Animator.StringToHash("Shoot");
        }

        public void ShootBullet(Vector3 hit, float perfectShotDamageMultiplier)
        {
            ActiveBullet.Shoot(FirePoint.position, hit, rWeaponData.BulletSpeed, rWeaponData.BulletDamage * perfectShotDamageMultiplier);
            DebugTransform.transform.position = hit;

            UpdateNextBulletIndex();

            _animator.SetTrigger(ShootHash);
        }

        public void EnableActiveArrow(Transform parent)
        {
            Transform ArrowVisual = ActiveBullet.GetVisualTransform();

            ArrowVisual.parent = parent;
            ArrowVisual.localPosition = Vector3.zero;
            ArrowVisual.localRotation = Quaternion.identity;

        }

        public void DisableActiveArrow()
        {
            ActiveBullet.GiveBackVisual();
        }

        void UpdateNextBulletIndex()
        {
            bulletIndex++;
            if (bulletIndex == bulletsPool.Count)
            {
                bulletIndex = 0;
            }

            ActiveBullet = bulletsPool[bulletIndex];
        }

        public int GetBulletIndex()
        {
            return bulletIndex;
        }

        public void Pull()
        {
            _animator.SetBool(PullHash, true);
        }


        public void Release()
        {
            _animator.SetBool(PullHash, false);
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

            ActiveBullet = bulletsPool[0];
        }

        public Transform GetActionCamParent()
        {
            return bulletsPool[bulletIndex].ActionCamParent;
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