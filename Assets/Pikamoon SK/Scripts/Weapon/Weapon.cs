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
        RightBack,

        leftBut,
        RightBut,

        LeftMidBack,
        RightMidBack
    }

    public abstract class Weapon : Item
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
        public Transform AlwaysOnParticle;
        public Transform TrailOnAttackParticle;
        public Transform HitImpactParticle;

        [Header("SFX")]
        public AudioClip SwingSound;

        public PlayerController Holder;

        public abstract WeaponInfo GetWeaponInfo();

        public virtual void AssignHolder(PlayerController playerController)
        {
            Holder = playerController;
        }


        public abstract Transform GetScabbard();
        public abstract void PlaceScabbard(Transform parent);


        public abstract override void OnPicked();
        public abstract override void OnPicked(Transform Picker);
        public abstract override void TryToPick(InventoryController Picker);       



        public abstract override void OnDrop();
        public abstract override void OnDrop(Transform Picker, LayerMask DropLayer);



        public abstract void OnEquip();
        public abstract void OnUnEquip();



        public abstract void OnHit(Vector3 point);

    }
}