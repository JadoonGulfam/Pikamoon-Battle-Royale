using UnityEngine;


namespace Pikamoon.Controller
{
    public enum WeaponHoldingPointType
    {
        left,
        right
    }
    public enum WeaponRestingPointType
    {
        leftShoulder,
        rightShoulder,
        leftPelvis,
        rightPelvis,
        leftKnee,
        rightKnee,
        leftBack,
        RightBack

    }

    public abstract class Weapon : Item, IPickable
    {
        public T GetWeaponDataAs<T>() where T : WeaponDataSO
        {
            return weaponData as T; // Tries to cast the currentWeapon to the specified type
        }

        protected WeaponInfo weaponInfo;

        public WeaponType Type;

        [SerializeField] protected WeaponDataSO weaponData;

        [SerializeField] protected Collider _collider;

        public int Health;

        public abstract WeaponInfo GetWeaponInfo();

        public abstract void OnPicked();

        public abstract void OnPicked(Transform Picker);

        public abstract void Drop();

        public abstract void Equip();

        public abstract void UnEquip();
    }
}