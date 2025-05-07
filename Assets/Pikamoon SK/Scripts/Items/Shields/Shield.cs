using Fusion;
using JetBrains.Annotations;
using System;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;


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
        public ShieldType shieldType;

        public float Strength;
        
        public Collider _collider;

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
        public void OnPicked(InventoryController Picker)
        {
            Picker.PickShield();
        }

    }
}