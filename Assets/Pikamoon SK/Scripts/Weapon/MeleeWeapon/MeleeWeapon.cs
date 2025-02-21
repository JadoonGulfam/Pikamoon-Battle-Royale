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


        #region Parent Imnplementation

        public override WeaponInfo GetWeaponInfo()
        {
            WeaponInfo info = new WeaponInfo();

            info.Prefab = this;
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
        }
        public override void OnPicked(Transform Picker)
        {

        }



        public override void OnDrop()
        {
        }
        public override void OnDrop(Transform Dropper, LayerMask DropLayer)
        {
            RaycastHit hit;
            
            if(Physics.Raycast(Dropper.position + (Dropper.forward*2) + (Vector3.up*2), Vector3.down, out hit ,5, DropLayer))
            {

                Vector3 pos = hit.point+Vector3.up*1;

                this.transform.parent = null;
                this.transform.position = pos;
                this.transform.rotation = Quaternion.identity;

                if(HasScabbard)
                {
                    Scabbard.transform.parent = null;
                    Scabbard.transform.position = pos;
                    Scabbard.transform.rotation = Quaternion.identity;
                }

                foreach (var collider in _colliders)
                {
                    collider.enabled = true;
                }
            }
        }



        public override void Equip()
        {

        }
        public override void UnEquip()
        {

        }

        #endregion

    }
}