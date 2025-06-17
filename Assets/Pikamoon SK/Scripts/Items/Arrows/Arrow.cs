using UnityEngine;

namespace Pikamoon.Controller
{
    public class Arrow : Item, IPickable, IDroppable
    {
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

        public  void OnPicked(Transform Picker)
        {
        }

        public void TryToPick(InventoryController Picker)
        {
            Picker.PickArrow(this);
        }
    }
}