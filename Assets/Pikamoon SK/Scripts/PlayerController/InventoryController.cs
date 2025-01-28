using UnityEngine;
using Pikamoon.UI;
using System.Collections.Generic;
using UnityEngine.InputSystem;

namespace Pikamoon.Controller
{
    [System.Serializable]
    public struct Bag
    {
        public List<Item> items;
    }


    [SerializeField]
    public enum ItemType
    {
        Weapon,
        Ammo,
        Health,
        Shield
    }


    public class InventoryController : MonoBehaviour
    {
        public HUDController HUDcontroller;

        //[SerializeField] Bag
        [Header("Weapons")]
        public Bag WeaponsBag;
        public int WeaponBagCapacity;
        public Weapon[] EquipedWeapons;

        public bool AutoEquipWeapon;
        int weaponsInBag;
        public int UsingWeaponIndex;


        [Header("Pickup Setting")]
        public Collider[] nearbyItems;
        public float rangeForItemPickup;
        public LayerMask pickupLayerMask;

        bool allowPickUp;
        public bool AllowPickUp
        {
            get { return allowPickUp; }
            set { allowPickUp = value; }
        }

        bool allowAutoPickUp;
        public bool AllowAutoPickUp
        {
            get { return allowAutoPickUp; }
            set { allowAutoPickUp = value; }
        }

        private PlayerController Controller;
        private PlayerInput playerInput;

        IPickable pickableItem;
        private void Start()
        {
            EquipedWeapons = new Weapon[2];
            WeaponBagCapacity = 1;
            UsingWeaponIndex = 0;
            allowPickUp = true;
            Controller = GetComponent<PlayerController>();
            playerInput = Controller.input;



            playerInput.onPrimaryWeaponSelect_Down += ChangeWeapon;
            playerInput.onSecondaryWeaponSelect_Down += ChangeWeapon;
        }

        private void Update()
        {
            ContinuousCheckForItemsForPickup();
        }

        void ChangeWeapon()
        {
            int nextWeaponIndex = UsingWeaponIndex == 1 ? 0 : 1;

            if (EquipedWeapons[nextWeaponIndex] != null)
            {
                WeaponInfo weaponInfo = EquipedWeapons[UsingWeaponIndex].GetWeaponInfo();

                Transform restingPoint = Controller.GetRestingPoint(weaponInfo.Data.restingPointType);

                EquipedWeapons[UsingWeaponIndex].transform.parent = restingPoint.transform;
                EquipedWeapons[UsingWeaponIndex].transform.localPosition = Vector3.zero;
                EquipedWeapons[UsingWeaponIndex].transform.localRotation = Quaternion.identity;


                HUDcontroller.EquipWeapon(UsingWeaponIndex, weaponInfo.Data.icon, false, EquipedWeapons[UsingWeaponIndex].Health, weaponInfo.Data.InitialHealth);


                weaponInfo = EquipedWeapons[nextWeaponIndex].GetWeaponInfo();

                Controller.ActivateWeapon(weaponInfo);
                HUDcontroller.EquipWeapon(nextWeaponIndex, weaponInfo.Data.icon, true, EquipedWeapons[nextWeaponIndex].Health, weaponInfo.Data.InitialHealth);


                UsingWeaponIndex = nextWeaponIndex;
            }
        }

        public void ContinuousCheckForItemsForPickup()
        {
            if (!allowPickUp)
                return;

            nearbyItems = Physics.OverlapSphere(this.transform.position, rangeForItemPickup, pickupLayerMask);

            if (nearbyItems.Length != 0)
            {
                HUDcontroller.ShowPickUp();

                if (Input.GetKeyDown(KeyCode.E))
                {
                    pickableItem = nearbyItems[0].GetComponent<IPickable>();
                    if (pickableItem != null)
                    {
                        if (pickableItem is Weapon)
                        {
                            PickWeapon(pickableItem as Weapon);
                        }
                    }
                }
            }
            else
            {
                HUDcontroller.HidePickUp();
            }
        }

        void PickWeapon(Weapon weapon)
        {

            if (weapon != null)
            {
            //if (AllowAutoPickUp)
            //{
                for (int i = 0; i < EquipedWeapons.Length; i++)
                {
                    if (EquipedWeapons[i] == null)
                    {
                        EquipedWeapons[i] = weapon;

                        weapon.Equip();

                        WeaponInfo weaponInfo = weapon.GetWeaponInfo();

                        if (Controller.ActiveWeapon.Prefab == null)
                        {
                            Controller.ActivateWeapon(weaponInfo);
                            HUDcontroller.EquipWeapon(i, weaponInfo.Data.icon,true, weapon.Health, weaponInfo.Data.InitialHealth);
                            UsingWeaponIndex = i;
                        }
                        else
                        {
                           Transform restingPoint =  Controller.GetRestingPoint(weaponInfo.Data.restingPointType);

                            weapon.transform.parent = restingPoint.transform;
                            weapon.transform.localPosition = Vector3.zero;
                            weapon.transform.localRotation = Quaternion.identity;


                            HUDcontroller.EquipWeapon(i, weaponInfo.Data.icon, false, weapon.Health, weaponInfo.Data.InitialHealth);
                        }


                        return;
                    }
                }


                if (weaponsInBag < WeaponBagCapacity)
                {
                    weapon.Equip();
                    weapon.gameObject.SetActive(false);
                    WeaponsBag.items.Add(weapon);
                    weaponsInBag++;
                }

            //}


            }
        }
    }
}