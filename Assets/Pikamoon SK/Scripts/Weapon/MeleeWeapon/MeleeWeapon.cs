using UnityEngine;
namespace Pikamoon.Controller
{
    public class MeleeWeapon : Weapon
    {

        MeleeWeaponDataSO mWeaponData;


        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            mWeaponData = GetWeaponDataAs<MeleeWeaponDataSO>();
        }

        // Update is called once per frame
        void Update()
        {

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
        }

        public override void UnEquip()
        {
        }
    }
}