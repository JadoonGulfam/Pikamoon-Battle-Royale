using UnityEngine;

namespace Pikamoon.Controller
{
    public class Arrow : Item
    {
        public Collider _collider;
        public void OnDrop()
        {
        }

        public void OnDrop(Transform Dropper, LayerMask DropLayer)
        {
            RaycastHit hit;

            Debug.Log("Dropped Object is Arrow item");
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
            Picker.PickArrow(this);
        }
    }
}