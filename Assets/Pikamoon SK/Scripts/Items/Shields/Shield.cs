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



    public class Shield : Item, IPickable, IDroppable
    {
        [SerializeField] ShieldType shieldType;

        [SerializeField] float Strength;

        [SerializeField] Collider _collider;

        public void OnDrop()
        {
        }

        public void OnDrop(Transform Dropper, LayerMask DropLayer)
        {
        }

        public void OnPicked()
        {
            _collider.enabled = false;

        }

        public void OnPicked(Transform Picker)
        {
        }

        public void TryToPick(InventoryController Picker)
        {
            Picker.PickShield(this, shieldType);
        }

    }
}