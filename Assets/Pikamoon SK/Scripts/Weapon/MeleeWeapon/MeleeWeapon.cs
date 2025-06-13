using UnityEngine;


namespace Pikamoon.Controller
{
    public class MeleeWeapon : Weapon
    {
        [Header("Hit Collider")]
        public WeaponHitBox HitBox;
        MeleeWeaponDataSO mWeaponData;

        Vector3 defaultHitParticlePos;

        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Awake()
        {
            mWeaponData = GetItemDataAs<MeleeWeaponDataSO>();

            if (TrailParticle)
                TrailParticle.gameObject.SetActive(false);

            defaultHitParticlePos = HitImpactParticle.localPosition;
        }


        #region Parent Imnplementation

        public override WeaponInfo GetWeaponInfo()
        {
            WeaponInfo info = new WeaponInfo();

            info.Prefab = this;

            if(!mWeaponData)
            {
                mWeaponData = GetItemDataAs<MeleeWeaponDataSO>();
            }

            info.Data = mWeaponData;

            return info;
        }


        public override Transform GetScabbard()
        {
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

            if (HitBox != null)
                HitBox._collider.enabled = false;
        }
        public override void OnPicked(Transform Picker)
        {
            if (HitBox != null)
                HitBox.enabled = false;
        }
        public override void TryToPick(InventoryController Picker)
        {
            Picker.PickWeapon(this);
        }


        public override void OnDrop()
        {
            if (HitBox != null)
                HitBox._collider.enabled = false;
        }
        public override void OnDrop(Transform Dropper, LayerMask DropLayer)
        {
            RaycastHit hit;
            
            if(Physics.Raycast(Dropper.position + (Dropper.forward*2) + (Vector3.up*2), Vector3.down, out hit ,5, DropLayer))
            {

                Vector3 pos = hit.point+Vector3.up*1;

                transform.parent = null;
                transform.position = pos;
                transform.rotation = Quaternion.identity;

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

            if (HitBox != null)
                HitBox.GetComponent<Collider>().enabled = false;
        }



        public override void OnEquip()
        {
            if(TrailParticle)
                TrailParticle.gameObject.SetActive(true);
        }
        public override void OnUnEquip()
        {
            if (TrailParticle)
                TrailParticle.gameObject.SetActive(false);
        }

        public override void OnHit(Vector3 point)
        {
            HitImpactParticle.gameObject.SetActive(false);

            HitImpactParticle.transform.parent = this.transform;
            HitImpactParticle.transform.localPosition = defaultHitParticlePos;
            HitImpactParticle.transform.parent = null;

            HitImpactParticle.gameObject.SetActive(true);
        }

        public override void AssignHolder(PlayerController Controller)
        {

        }

        #endregion

    }
}