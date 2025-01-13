using UnityEngine;


namespace Pikamoon.Controller
{
    public enum WeaponHoldingPointType
    {
        left,
        right
    }

    public abstract class Weapon : MonoBehaviour, IPickable
    {
        public T GetWeaponDataAs<T>() where T : WeaponDataSO
        {
            return weaponData as T; // Tries to cast the currentWeapon to the specified type
        }

        protected WeaponInfo weaponInfo;

        public WeaponType Type;

        [SerializeField] protected WeaponDataSO weaponData;

        [SerializeField] protected Collider _collider;

        public float Health;

        public abstract WeaponInfo GetWeaponInfo();

        public abstract void OnPicked();

        public abstract void OnPicked(Transform Picker);

        public abstract void Drop();

    }
}