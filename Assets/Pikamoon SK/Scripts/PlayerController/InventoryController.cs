using UnityEngine;
using Pikamoon.UI;
using System.Collections.Generic;
using System;

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
        public ItemCategory Weapons;

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

        [Header("Inventory Core")]
        [SerializeField] Transform ItemsParent;

        public PlayerSetupForMultiplayer MP_Setup;

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

        LootBox CurrentLootbox;
        int IndexInLootBox;

        RaycastHit hitItem;
        Transform hitTransform;
        bool isPickableAnItem;
        bool isPointerOnLootBox;

        IPickable pickableItem;

        private void Start()
        {

            UsingWeaponIndex = 0;
            isUsingWeapon = false;

        }

        public void Initialize(UIManagerSK _uiManager, PlayerController _controller)
        {
            UI = _uiManager;
            Controller = _controller;
            playerInput = Controller.input;

            //Shields.InitializeCategory();
            //QuickItems.InitializeCategory();
            //AllItems.InitializeCategory();


            playerInput.onPrimaryWeaponSelect_Down += ChangeToPrimaryWeapon;
            playerInput.onSecondaryWeaponSelect_Down += ChangeToSecondaryWeapon;

            playerInput.onWeaponDrop_Down += DropWeapon;

            playerInput.onPick_Down += Pick;

            Controller.ActivateWeapon(DefaultFistNoWeapon);

            UI.inventoryUI._inventory = this;
            UI.lootBoxUI._inventory = this;

            allowPickUp = true;
        }
        private void Update()
        {
            if (MP_Setup != null && !MP_Setup.isMinePlayer)
                return;

            ContinuousCheckForItemsForPickup();
        }

        public void ContinuousCheckForItemsForPickup()
        {
            if (!allowPickUp || UI.lootBoxUI.LootCanvas.enabled)
                return;


            if (Physics.CheckSphere(this.transform.position, rangeForItemPickup, pickupLayerMask + LootBoxLayerMask))
            {
                Vector2 screenCenterPoint = new Vector2(Screen.width / 2, Screen.height / 2);

                Ray ray = Controller._cameraController._camera.ScreenPointToRay(screenCenterPoint);

                if (Physics.Raycast(ray, out hitItem, 999f, pickupLayerMask))
                {
                    hitTransform = hitItem.transform;
                    isPointerOnLootBox = false;

                    UI.hudcontroller.ShowPickUp();
                }
                else if (Physics.Raycast(ray, out hitItem, 999f, LootBoxLayerMask))
                {
                    hitTransform = hitItem.transform;
                    isPointerOnLootBox = true;

                    UI.hudcontroller.ShowPickUp();
                }
                else
                {
                    hitTransform = null;
                    UI.hudcontroller.HidePickUp();
                }
            }
            else
            {
                hitTransform = null;
                UI.hudcontroller.HidePickUp();
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
                    if (!isPointerOnLootBox)
                    {
                        isPickableAnItem = true;
                        pickableItem.TryToPick(this);
                    }
                    else
                    {

                        isPickableAnItem = false;
                        PickupLootBox(pickableItem);
                    }
                }
            }
        }

        public void PickItemFromLootBox(int index)
        {
            IndexInLootBox = index;

            pickableItem = CurrentLootbox.GetItem(index).GetComponent<IPickable>();
            if (pickableItem != null)
            {
                pickableItem.TryToPick(this);
            }
        }

        public void ManualAssignAtStart(Transform StartItem)
        {
            pickableItem = StartItem.GetComponent<IPickable>();
            if (pickableItem != null)
            {
                if (pickableItem is Weapon)
                {
                    PickWeapon(pickableItem as Weapon);
                }
            }
        }


        #region Loot Behaviour

        void ShowLootBoxUI()
        {
            UI.hudcontroller.HidePickUp();
            UI.inventoryUI.ShowUI();
            Controller.CameraOrbitStatus = false;
            Controller.ToggleCursor(true);
            AllowPickUp = false;
        }

        public void HideLootBoxUI()
        {
            Controller.CameraOrbitStatus = true;
            UI.inventoryUI.HideUI();
            Controller.ToggleCursor(false);
            AllowPickUp = true;
        }

        void PickupLootBox(IPickable pickable)
        {
            pickable.TryToPick(this);
        }

        public void AssignLootBox(LootBox _lootbox)
        {
            CurrentLootbox = _lootbox;
            UI.lootBoxUI.PopulateList(CurrentLootbox);
            ShowLootBoxUI();
        }

        #endregion

        void SuccessfullyItemPickedFromLoot()
        {
        }

        void SuccessfullyItemPickedFromEnvironment()
        {
        }

        void NoSlotAvaialbleForItem()
        {
        }


        #region Weapon Portion

        void ChangeToPrimaryWeapon()
        {
            if (Weapons.items[0] == null)
                return;

            if (Controller.IsInAttack || Controller.InAir || Controller.IsSwimming)
                return;



            if (UsingWeaponIndex == 0)
            {
                if (isUsingWeapon)
                {
                    UnEquipping(0, Weapons.items[0], true);
                }
                else
                {
                    Equipping(0, Weapons.items[0]);
                }
            }
            else
            {
                if (isUsingWeapon)
                {
                    UnEquipping(UsingWeaponIndex, Weapons.items[UsingWeaponIndex], false);

                    Equipping(0, Weapons.items[0]);
                }
                else
                {
                    Equipping(0, Weapons.items[0]);
                }
            }
        }
        void ChangeToSecondaryWeapon()
        {
            if (Weapons.items[1] == null)
                return;

            if (Controller.IsInAttack || Controller.InAir || Controller.IsSwimming)
                return;


            if (UsingWeaponIndex == 1)
            {
                if (isUsingWeapon)
                {
                    UnEquipping(1, Weapons.items[1], true);
                }
                else
                {
                    Equipping(1, Weapons.items[1]);
                }
            }
            else
            {
                if (isUsingWeapon)
                {
                    UnEquipping(UsingWeaponIndex, Weapons.items[UsingWeaponIndex], false);

                    Equipping(1, Weapons.items[1]);
                }
                else
                {
                    Equipping(1, Weapons.items[1]);
                }
            }
        }

        void UnEquipping(int index, Item item, bool ActivateNoWeapon)
        {
            Weapon weapon = item.GetItemAs<Weapon>();

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
        void Equipping(int index, Item item)
        {
            Weapon weapon = item.GetItemAs<Weapon>();

            WeaponInfo weaponInfo = weapon.GetWeaponInfo();

            weapon.OnEquip();

            Controller.ActivateWeapon(weaponInfo);
            UI.hudcontroller.EquipWeapon(index, weaponInfo.Data.icon, true, weapon.Health, weaponInfo.Data.InitialHealth);

            isUsingWeapon = true;
            UsingWeaponIndex = index;
        }

        public void PickWeapon(Weapon weapon)
        {
            int index = GetMeAvailableSlotForNewWeapon();
            if (index != -1)
            {
                AddItemToWeaponsByPickup(weapon, index);
            }
            else
            {

                index = GetMeSlotForQuickItem();
                if (index != -1)
                {
                    AddItemToQuickItemsByPickup(weapon, index);
                }
                else
                {
                    index = GetMeSlotForAllItem();

                    if (index != -1)
                    {
                        AddItemToAllItemsByPickup(weapon, index);
                    }
                    else
                    {
                        NoSlotAvaialbleForItem();
                    }
                }
            }

        }
        void DropWeapon()
        {
            if (Weapons.items[UsingWeaponIndex] == null)
                return;


            if (Controller.IsInAttack || Controller.InAir || Controller.IsSwimming)
                return;

            isUsingWeapon = false;

            UI.hudcontroller.DropWeapon(UsingWeaponIndex);

            Weapons.items[UsingWeaponIndex].GetItemAs<Weapon>().OnDrop(this.transform, Controller.groundLayer);

            Weapons.items[UsingWeaponIndex] = null;

            Controller.ActivateWeapon(DefaultFistNoWeapon);
        }

        void AddItemToWeaponsByPickup( Weapon weapon, int index)
        {
            Weapons.items[index] = weapon;
            Weapons.AvailedInCategory++;
            UI.inventoryUI.AssignToWeapons(weapon,index);

            weapon.OnPicked();

            weapon.AssignHolder(Controller);

            bool isEquipeWeapon = false;

            WeaponInfo weaponInfo = weapon.GetWeaponInfo();

            if (Controller.ActiveWeapon.Prefab == null)
            {
                isEquipeWeapon = true;
                Equipping(index, weapon);
            }
            else
            {
                Transform restingPoint = Controller.GetRestingPoint(weaponInfo.Data.restingPointType);

                weapon.transform.parent = restingPoint.transform;
                weapon.transform.localPosition = Vector3.zero;
                weapon.transform.localRotation = Quaternion.identity;


                UI.hudcontroller.EquipWeapon(index, weaponInfo.Data.icon, false, weapon.Health, weaponInfo.Data.InitialHealth);
            }


            if (weapon.HasScabbard)
            {
                weapon.PlaceScabbard(Controller.GetRestingPoint(weaponInfo.Data.restingPointType));
            }

            // if this weapon is collected from Loot Box
            if (!isPickableAnItem)
            {
                if(isEquipeWeapon)
                    weapon.OnEquip();

                weapon.transform.gameObject.SetActive(true);
                CurrentLootbox.SelectItem(IndexInLootBox);
                UI.lootBoxUI.DisableItemInUI(IndexInLootBox);

                SuccessfullyItemPickedFromLoot();
            }
            else
            {
                SuccessfullyItemPickedFromEnvironment();
            }
        }
        int GetMeAvailableSlotForNewWeapon()
        {
            int index = -1;
            for (int i = 0; i < Weapons.items.Count; i++)
            {
                if (Weapons.items[i] == null)
                {
                    index = i;
                    break;
                }
            }
            return index;
        }


        #endregion


        #region Arrow Portion
        public void PickArrow(Item arrow)
        {
            int index = GetMeSlotForQuickItem();
            if (index != -1)
            {
                AddItemToQuickItemsByPickup(arrow, index);
            }
            else
            {
                index = GetMeSlotForAllItem();

                if (index != -1)
                {
                    AddItemToAllItemsByPickup(arrow, index);
                }
                else
                {
                    NoSlotAvaialbleForItem();
                }
            }
        }

        #endregion


        #region Shield Portion

        public void PickShield(Item shield, ShieldType type)
        {
            int index = GetMeAvailableSlotForShield(type);
            if (index != -1)
            {
                AddItemToShieldsByPickup(shield, index);
            }
            else
            {
                index = GetMeSlotForQuickItem();
                if (index != -1)
                {
                    AddItemToQuickItemsByPickup(shield, index);
                }
                else
                {
                    index = GetMeSlotForAllItem();
                    if (index != -1)
                    {
                        AddItemToAllItemsByPickup(shield, index);
                    }
                    else
                    {
                        NoSlotAvaialbleForItem();
                    }
                }
            }
        }
        
        void AddItemToShieldsByPickup(Item item, int index)
        {
            Shields.items[index] = item;
            Shields.AvailedInCategory++;

            UI.inventoryUI.AssignToShields(item, index);

            if (isPickableAnItem)
            {
                pickableItem.OnPicked();

                item.transform.parent = ItemsParent;
                item.transform.localPosition = Vector3.zero;
                item.transform.localRotation = Quaternion.identity;

                item.gameObject.SetActive(false);
                SuccessfullyItemPickedFromEnvironment();
            }
            else
            {

                CurrentLootbox.SelectItem(IndexInLootBox);
                UI.lootBoxUI.DisableItemInUI(IndexInLootBox);
                SuccessfullyItemPickedFromLoot();
            }

        }

        int GetMeAvailableSlotForShield(ShieldType type)
        {
            int hasItem = -1;


            if (Shields.items[(int)type] == null)
            {
                hasItem = (int)type;
            }


            return hasItem;
        }

        #endregion


        #region Health Portion

        public void PickHealth(Item health)
        {
            int index = GetMeSlotForQuickItem();
            if (index != -1)
            {
                AddItemToQuickItemsByPickup(health, index);
            }
            else
            {
                index = GetMeSlotForAllItem();
                if (index != -1)
                {
                    AddItemToAllItemsByPickup(health, index);
                }
                else
                {
                    NoSlotAvaialbleForItem();
                }
            }
        }

        #endregion


        #region Quick Items Portion
        int GetMeSlotForQuickItem()
        {
            int index = -1;

            if (QuickItems.AvailedInCategory >= QuickItems.MaxInCategory)
            {
                return index;
            }


            for (int i = 0; i < QuickItems.items.Count; i++)
            {
                if (QuickItems.items[i] == null)
                {
                    index = i;
                    break;
                }
            }

            return index;
        }

        void AddItemToQuickItemsByPickup(Item item, int index)
        {
            QuickItems.items[index] = item;
            QuickItems.AvailedInCategory++;
            UI.inventoryUI.AssignToQuickItems(item, index);

            if (isPickableAnItem)
            {
                pickableItem.OnPicked(); 
                
                item.transform.parent = ItemsParent;
                item.transform.localPosition = Vector3.zero;
                item.transform.localRotation = Quaternion.identity;

                item.gameObject.SetActive(false);
                SuccessfullyItemPickedFromEnvironment();
            }
            else
            {
                CurrentLootbox.SelectItem(IndexInLootBox);
                UI.lootBoxUI.DisableItemInUI(IndexInLootBox);
                SuccessfullyItemPickedFromLoot();
            }


        }

        #endregion


        #region All Items Portion
        int GetMeSlotForAllItem()
        {
            int index = -1;

            if (AllItems.AvailedInCategory >= AllItems.MaxInCategory)
            {
                return index;
            }

            for (int i = 0; i < AllItems.items.Count; i++)
            {
                if (AllItems.items[i] == null)
                {
                    index = i;
                    break;
                }
            }

            return index;
        }
        void AddItemToAllItemsByPickup(Item item, int index)
        {
            AllItems.items[index] = item;
            AllItems.AvailedInCategory++;
            UI.inventoryUI.AssignToAllItems(item, index);

            if (isPickableAnItem)
            {
                pickableItem.OnPicked();
                
                item.transform.parent = ItemsParent;
                item.transform.localPosition = Vector3.zero;
                item.transform.localRotation = Quaternion.identity;

                item.gameObject.SetActive(false); 
                SuccessfullyItemPickedFromEnvironment();
            }
            else
            {
                CurrentLootbox.SelectItem(IndexInLootBox);
                UI.lootBoxUI.DisableItemInUI(IndexInLootBox);
                SuccessfullyItemPickedFromLoot();
            }
        }
        #endregion

    }
}