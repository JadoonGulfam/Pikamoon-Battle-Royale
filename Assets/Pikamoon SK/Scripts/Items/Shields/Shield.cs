using System;
using UnityEngine;


namespace Pikamoon.Controller
{

    [Serializable]
    public enum ShieldType
    {
        Head,
        UpperBody,
        LowerBody
    }



    public class Shield : Item
    {
        [SerializeField] ShieldType shieldType;

        [SerializeField] float Strength;

        [SerializeField] Collider _collider;

        public override void OnDrop()
        {
        }

        public override void OnDrop(Transform Dropper, LayerMask DropLayer)
        {
            RaycastHit hit;

            Debug.Log("Dropped Object is Shield item");
            if (Physics.Raycast(Dropper.position + (Dropper.forward * 2) + (Vector3.up * 2), Vector3.down, out hit, 50, DropLayer))
            {

                Vector3 pos = hit.point + Vector3.up * 1;

                transform.parent = null;
                transform.position = pos;
                transform.rotation = Quaternion.identity;


                transform.parent = null;
                transform.position = pos;
                transform.rotation = Quaternion.identity;

                _collider.enabled = true;
            }
        }

        public override void OnPicked()
        {
            _collider.enabled = false;

        }

        public override void OnPicked(Transform Picker)
        {
        }

        public override void TryToPick(InventoryController Picker)
        {
            Picker.PickShield(this, shieldType);
        }

    }
}