using UnityEngine;
namespace Pikamoon.Controller
{
    public class MeleeWeapon : Weapon
    {

        MeleeWeaponDataSO mWeaponData;


        [Header("Scabbard")]
        public bool HasScabbard;
        public Transform Scabbard;
        public WeaponRestingPointType scabbardRestingPointType;

        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            mWeaponData = GetWeaponDataAs<MeleeWeaponDataSO>();
        }


        public override void Drop()
        {
        }

        public override void OnPicked()
        {
        }

        public override void OnPicked(Transform Picker)
        {

        }

        public override WeaponInfo GetWeaponInfo()
        {
            WeaponInfo info = new WeaponInfo();

            info.Prefab = this;
            info.Data = mWeaponData;

            return info;
        }

        public override void Equip()
        {
            foreach (var collider in _colliders)
            {
                collider.enabled = false;
            }

            //if(HasScabbard)
            //{

            //}

        }
        public override void UnEquip()
        {
            foreach (var collider in _colliders)
            {
                collider.enabled = true;
            }
        }

        //public override void HasScabbard()
        //{
        //}
    }
}