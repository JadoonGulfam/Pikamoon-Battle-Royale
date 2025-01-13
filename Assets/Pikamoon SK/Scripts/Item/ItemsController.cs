using UnityEngine;

namespace Pikamoon.Controller
{
    public class ItemsController : MonoBehaviour
    {
        [Header("Pickup Setting")]
        public Collider[] NearbyItems;
        public float rangeForItemPickup;
        public LayerMask pickupLayerMask;

        bool allowPickUp;
        public bool AllowPickUp
        {
            get { return allowPickUp; }
            set { allowPickUp = value; }
        }

        private PlayerController Controller;

        IPickable pickableItem;
        private void Start()
        {
            allowPickUp = true;
            Controller = GetComponent<PlayerController>();
        }


        private void Update()
        {
            ContinuousCheckForItemsForPickup();
        }


        public void ContinuousCheckForItemsForPickup()
        {
            if (!allowPickUp)
                return;

            NearbyItems = Physics.OverlapSphere(this.transform.position, rangeForItemPickup, pickupLayerMask);
            
            if(NearbyItems.Length != 0)
            {
                Debug.Log("Nearby Item = " + NearbyItems[0]);
                if (Input.GetKeyDown(KeyCode.E))
                {
                    pickableItem = NearbyItems[0].GetComponent<IPickable>();
                    if (pickableItem != null)
                    {
                        Weapon weapon = pickableItem as Weapon;

                        if (weapon != null)
                        {
                            Controller.AssignWeapon(weapon.GetWeaponInfo());
                        }
                    }
                }
            }
        }
    }
}