using MeshEffects2;
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

    public abstract class Weapon : Item, IPickable ,IDroppable
    {
        protected WeaponInfo weaponInfo;

        [Space(20)]
        public WeaponType weaponType;

        public Collider[] _colliders;

        public int Health;

        [Header("Scabbard")]
        public bool HasScabbard;
        public Transform Scabbard;


        [Header("VFX")]
        public ME2_Velocity TrailParticle;
        public Transform HitImpactParticle;

        [Header("SFX")]
        public AudioClip SwingSound;

        public PlayerController Holder;

        public abstract WeaponInfo GetWeaponInfo();

        public abstract void AssignHolder(PlayerController playerController);


        public abstract Transform GetScabbard();
        public abstract void PlaceScabbard(Transform parent);


        public abstract void OnPicked();
        public abstract void OnPicked(Transform Picker);
        public abstract void TryToPick(InventoryController Picker);       



        public abstract void OnDrop();
        public abstract void OnDrop(Transform Picker, LayerMask DropLayer);



        public abstract void OnEquip();
        public abstract void OnUnEquip();



        public abstract void OnHit(Vector3 point);

    }
}