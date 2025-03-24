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

    public abstract class Weapon : Item, IPickable,IDroppable
    {
        public T GetWeaponDataAs<T>() where T : WeaponDataSO
        {
            return weaponData as T; // Tries to cast the currentWeapon to the specified type
        }

        protected WeaponInfo weaponInfo;

        public WeaponType Type;

        [SerializeField] protected WeaponDataSO weaponData;

        [SerializeField] protected Collider[] _colliders;

        public int Health;

        [Header("Scabbard")]
        public bool HasScabbard;
        public Transform Scabbard;


        [Header("VFX")]
        public Transform SwingParticle;

        [Header("SFX")]
        public AudioClip SwingSound;
        public abstract WeaponInfo GetWeaponInfo();

        
        public abstract Transform GetScabbard();
        public abstract void PlaceScabbard(Transform parent);


        public abstract void OnPicked();
        public abstract void OnPicked(Transform Picker);



        public abstract void OnDrop();
        public abstract void OnDrop(Transform Picker, LayerMask DropLayer);



        public abstract void OnEquip();
        public abstract void OnUnEquip();

    }
}