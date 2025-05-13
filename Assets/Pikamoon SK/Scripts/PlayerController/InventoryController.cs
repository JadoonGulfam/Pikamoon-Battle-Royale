using UnityEngine;
using Pikamoon.UI;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using System;

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
        public HUDController UI;

        //[SerializeField] Bag
        [Header("Weapons")]

        public WeaponInfo DefaultFistNoWeapon;
        [Space]
        [Space]
        public Bag WeaponsBag;
        public int WeaponBagCapacity;
        [Space]
        [Space]
        public Weapon[] EquipedWeapons;
        [Space]
        [Space]
        public bool AutoEquipWeapon;
        int weaponsInBag;
        [Space]
        public bool isUsingWeapon;
        public int UsingWeaponIndex;


        [Header("Pickup Setting")]
        public Collider[] nearbyItems;
        public float rangeForItemPickup;
        public LayerMask pickupLayerMask;


        [SerializeField] Transform Dummy;


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
            isUsingWeapon = false;

        }

        public void Initialize(HUDController _ui, PlayerController _controller)
        {
            UI = _ui;
            Controller = _controller;
            playerInput = Controller.input;

            playerInput.onPrimaryWeaponSelect_Down += ChangeToPrimaryWeapon;
            playerInput.onSecondaryWeaponSelect_Down += ChangeToSecondaryWeapon;

            playerInput.onWeaponDrop_Down += DropWeapon;

            Controller.ActivateWeapon(DefaultFistNoWeapon);

            allowPickUp = true;
        }
        public PlayerSetupForMultiplayer MP_Setup;
        private void Update()
        {
            //if (Controller.MP_Setup != null && !Controller.MP_Setup.isMinePlayer)
            //    return;

            if (MP_Setup != null && !MP_Setup.isMinePlayer)
                return;


            //Dummy.position = this.transform.position + (this.transform.forward * 2) + (Vector3.up * 2);


            ContinuousCheckForItemsForPickup();

        }



        void ChangeToPrimaryWeapon()
        {
            if (EquipedWeapons[0] == null)
                return;

            if (Controller.IsInAttack || Controller.InAir || Controller.IsSwimming)
                return;



            if (UsingWeaponIndex == 0)
            {
                if (isUsingWeapon)
                {
                    UnEquipping(0, EquipedWeapons[0], true);
                }
                else
                {
                    Equipping(0,EquipedWeapons[0]);
                }
            }
            else
            {
                if (isUsingWeapon)
                {
                    UnEquipping(UsingWeaponIndex, EquipedWeapons[UsingWeaponIndex], false);

                    Equipping(0, EquipedWeapons[0]);
                }
                else
                {
                    Equipping(0, EquipedWeapons[0]);
                }
            }
        }
        void ChangeToSecondaryWeapon()
        {
            if (EquipedWeapons[1] == null)
                return;

            if (Controller.IsInAttack || Controller.InAir || Controller.IsSwimming)
                return;


            if (UsingWeaponIndex == 1)
            {
                if (isUsingWeapon)
                {
                    UnEquipping(1, EquipedWeapons[1], true);
                }
                else
                {
                    Equipping(1, EquipedWeapons[1]);
                }
            }
            else
            {
                if (isUsingWeapon)
                {
                    UnEquipping(UsingWeaponIndex, EquipedWeapons[UsingWeaponIndex], false);

                    Equipping(1, EquipedWeapons[1]);
                }
                else
                {
                    Equipping(1, EquipedWeapons[1]);
                }
            }
        }




        void UnEquipping(int index, Weapon weapon,bool ActivateNoWeapon)
        {
            WeaponInfo weaponInfo = weapon.GetWeaponInfo();

            Transform restingPoint = Controller.GetRestingPoint(weaponInfo.Data.restingPointType);

            weapon.transform.parent = restingPoint.transform;
            weapon.transform.localPosition = Vector3.zero;
            weapon.transform.localRotation = Quaternion.identity;

            weapon.OnUnEquip();

            UI.UnEquipWeapon(index);

            isUsingWeapon = false;

            if (ActivateNoWeapon)
            {
                Controller.ActivateWeapon(DefaultFistNoWeapon);
            }
        }

        void Equipping(int index, Weapon weapon)
        {
            WeaponInfo weaponInfo = weapon.GetWeaponInfo();

            weapon.OnEquip();

            Controller.ActivateWeapon(weaponInfo);
            UI.EquipWeapon(index, weaponInfo.Data.icon, true, weapon.Health, weaponInfo.Data.InitialHealth);

            isUsingWeapon = true;
            UsingWeaponIndex = index;
        }

        

        public void ContinuousCheckForItemsForPickup()
        {
            if (!allowPickUp || !UI)
                return;

            nearbyItems = Physics.OverlapSphere(this.transform.position, rangeForItemPickup, pickupLayerMask);

            if (nearbyItems.Length != 0)
            {
                UI.ShowPickUp();

                if (Input.GetKeyDown(KeyCode.E))
                {
                    if(Controller.IsInAttack || Controller.InAir)
                        return;


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
                UI.HidePickUp();
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

                        weapon.OnPicked();

                        

                        WeaponInfo weaponInfo = weapon.GetWeaponInfo();

                        if (Controller.ActiveWeapon.Prefab == null)
                        {
                            Equipping(i, weapon);
                        }
                        else
                        {
                           Transform restingPoint =  Controller.GetRestingPoint(weaponInfo.Data.restingPointType);

                            weapon.transform.parent = restingPoint.transform;
                            weapon.transform.localPosition = Vector3.zero;
                            weapon.transform.localRotation = Quaternion.identity;


                            UI.EquipWeapon(i, weaponInfo.Data.icon, false, weapon.Health, weaponInfo.Data.InitialHealth);
                        }


                        if (weapon.HasScabbard)
                        {
                            weapon.PlaceScabbard(Controller.GetRestingPoint(weaponInfo.Data.restingPointType));
                        }

                        return;
                    }
                }


                if (weaponsInBag < WeaponBagCapacity)
                {
                    weapon.OnPicked();
                    weapon.gameObject.SetActive(false);
                    WeaponsBag.items.Add(weapon);
                    weaponsInBag++;
                }

            //}
            }

        }

        public void ManualAssignAtStart(Transform StartItem)
        {              
            print("ManualAssignAtStart");
            pickableItem = StartItem.GetComponent<IPickable>();
            if (pickableItem != null)
            {
                if (pickableItem is Weapon)
                {
                    //pickableItem.OnPicked();
                   // UI.HidePickUp();
                    PickWeapon(pickableItem as Weapon);
                }
            }
        }
        void DropWeapon()
        {
            if (EquipedWeapons[UsingWeaponIndex] == null)
                return;


            if (Controller.IsInAttack || Controller.InAir || Controller.IsSwimming)
                return;

            isUsingWeapon = false;

            UI.DropWeapon(UsingWeaponIndex);

            EquipedWeapons[UsingWeaponIndex].OnDrop(this.transform, Controller.groundLayer);

            EquipedWeapons[UsingWeaponIndex] = null;

            Controller.ActivateWeapon(DefaultFistNoWeapon);
        }
    }
}