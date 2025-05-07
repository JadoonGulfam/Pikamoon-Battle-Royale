using UnityEngine;
using Pikamoon.UI;
using System.Collections.Generic;

namespace Pikamoon.Controller
{

    [System.Serializable]
    public struct ItemCategory
    {
        public string CategoryName;
        public List<Item> items;
        public int MaxInCategory;
        public int AvailedInCategory;
    }

    public class InventoryController : MonoBehaviour
    {
        public UIManagerSK UI;

        //[SerializeField] Bag
        [Header("Weapons")]

        public WeaponInfo DefaultFistNoWeapon;
        [Space]
        [Space]
        public Weapon[] EquipedWeapons;

        //[Space]
        //public ItemCategory Weapons; 

        [Space]
        public ItemCategory Shields;

        [Space]
        public ItemCategory QuickItems;

        [Space]
        public ItemCategory AllItems;

        [Space]
        public bool AutoEquipWeapon;
        [Space]
        public bool isUsingWeapon;
        public int UsingWeaponIndex;


        [Header("Pickup Setting")]
        public float rangeForItemPickup;
        public LayerMask pickupLayerMask;
        public LayerMask LootBoxLayerMask;


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
            UsingWeaponIndex = 0;
            isUsingWeapon = false;

        }

        public void Initialize(UIManagerSK _uiManager, PlayerController _controller)
        {
            UI = _uiManager;
            Controller = _controller;
            playerInput = Controller.input;

            playerInput.onPrimaryWeaponSelect_Down += ChangeToPrimaryWeapon;
            playerInput.onSecondaryWeaponSelect_Down += ChangeToSecondaryWeapon;

            playerInput.onWeaponDrop_Down += DropWeapon;

            playerInput.onPick_Down += Pick;

            Controller.ActivateWeapon(DefaultFistNoWeapon);

            UI.inventoryUI._inventory = this;
            UI.lootBoxUI._inventory = this;

            allowPickUp = true;
        }
        public PlayerSetupForMultiplayer MP_Setup;
        private void Update()
        {
            if (MP_Setup != null && !MP_Setup.isMinePlayer)
                return;

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
                    Equipping(0, EquipedWeapons[0]);
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


        void UnEquipping(int index, Weapon weapon, bool ActivateNoWeapon)
        {
            WeaponInfo weaponInfo = weapon.GetWeaponInfo();

            Transform restingPoint = Controller.GetRestingPoint(weaponInfo.Data.restingPointType);

            weapon.transform.parent = restingPoint.transform;
            weapon.transform.localPosition = Vector3.zero;
            weapon.transform.localRotation = Quaternion.identity;

            weapon.OnUnEquip();

            UI.hudcontroller.UnEquipWeapon(index);

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
            UI.hudcontroller.EquipWeapon(index, weaponInfo.Data.icon, true, weapon.Health, weaponInfo.Data.InitialHealth);

            isUsingWeapon = true;
            UsingWeaponIndex = index;
        }


        RaycastHit hitItem;
        Transform hitTransform;
        bool isPickableAnItem;

        public void ContinuousCheckForItemsForPickup()
        {
            if (!allowPickUp || !UI)
                return;


            if (Physics.CheckSphere(this.transform.position, rangeForItemPickup, pickupLayerMask + LootBoxLayerMask))
            {
                Vector2 screenCenterPoint = new Vector2(Screen.width / 2, Screen.height / 2);

                Ray ray = Controller._cameraController._camera.ScreenPointToRay(screenCenterPoint);

                if (Physics.Raycast(ray, out hitItem, 999f, pickupLayerMask))
                {
                    hitTransform = hitItem.transform;
                    isPickableAnItem = true;

                    UI.hudcontroller.ShowPickUp();
                }
                else if (Physics.Raycast(ray, out hitItem, 999f, LootBoxLayerMask))
                {
                    hitTransform = hitItem.transform;
                    isPickableAnItem = false;

                    UI.hudcontroller.ShowPickUp();
                }
                else
                {
                    hitTransform = null;
                    UI.hudcontroller.HidePickUp();
                    isPickableAnItem = false;
                }
            }
            else
            {
                hitTransform = null;
                UI.hudcontroller.HidePickUp();
                isPickableAnItem = false;
            }
        }


        public void Pick()
        {
            if (hitTransform)
            {
                if (Controller.IsInAttack || Controller.InAir)
                    return;

                pickableItem = hitTransform.GetComponent<IPickable>();
                if (pickableItem != null)
                {
                    if (isPickableAnItem)
                    {
                        if (pickableItem is Weapon)
                        {
                            PickWeapon(pickableItem as Weapon);
                        }
                        else
                        {
                            pickableItem.OnPicked(this);
                        }
                    }
                    else
                    {
                        PickupLootBox(pickableItem);
                    }
                }
            }
        }

        #region Loot Behaviour

        void PickupLootBox(IPickable pickable)
        {
            pickable.OnPicked(this);
        }

        public void AssignLootBox(LootBox lootbox)
        {
            UI.lootBoxUI.PopulateList(lootbox);
        }

        #endregion
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
                            Transform restingPoint = Controller.GetRestingPoint(weaponInfo.Data.restingPointType);

                            weapon.transform.parent = restingPoint.transform;
                            weapon.transform.localPosition = Vector3.zero;
                            weapon.transform.localRotation = Quaternion.identity;


                            UI.hudcontroller.EquipWeapon(i, weaponInfo.Data.icon, false, weapon.Health, weaponInfo.Data.InitialHealth);
                        }


                        if (weapon.HasScabbard)
                        {
                            weapon.PlaceScabbard(Controller.GetRestingPoint(weaponInfo.Data.restingPointType));
                        }

                        return;
                    }
                }


                //////////////if (weaponsInBag < WeaponBagCapacity)
                //////////////{
                //////////////    weapon.OnPicked();
                //////////////    weapon.gameObject.SetActive(false);
                //////////////    WeaponsBag.items.Add(weapon);
                //////////////    weaponsInBag++;
                //////////////}

                //}


            }
        }


        public void AssignItemToInventory(Item item)
        {
            if(QuickItems.items.Count <= QuickItems.MaxInCategory)
            {

            }


            if (item.Data.itemType == ItemType.Arrow)
            {

            }
            else if (item.Data.itemType == ItemType.Health)
            {

            }
            else if (item.Data.itemType == ItemType.Shield_Head)
            {

            }
            else if (item.Data.itemType == ItemType.Shield_UpperBody)
            {

            }
            else if (item.Data.itemType == ItemType.Shield_LowerBody)
            {

            }
        }

        public void PickArrow()
        {

        }
        public void PickHealth()
        {

        }

        public void PickShield()
        {

        }

        void DropWeapon()
        {
            if (EquipedWeapons[UsingWeaponIndex] == null)
                return;


            if (Controller.IsInAttack || Controller.InAir || Controller.IsSwimming)
                return;

            isUsingWeapon = false;

            UI.hudcontroller.DropWeapon(UsingWeaponIndex);

            EquipedWeapons[UsingWeaponIndex].OnDrop(this.transform, Controller.groundLayer);

            EquipedWeapons[UsingWeaponIndex] = null;

            Controller.ActivateWeapon(DefaultFistNoWeapon);
        }
    }
}