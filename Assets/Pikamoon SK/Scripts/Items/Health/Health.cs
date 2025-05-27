using UnityEngine;

namespace Pikamoon.Controller
{
    public class Health : Item, IPickable
    {


        public Collider _collider;
        public void OnPicked()
        {
            _collider.enabled = false;
        }

        public void OnPicked(Transform Picker)
        {
        }

        public void TryToPick(InventoryController Picker)
        {
            Picker.PickHealth(this);
        }
    }
}