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
        public PlayerSetupForMultiplayer MP_Setup;
        private void Update()
        {
            if (MP_Setup != null && !MP_Setup.isMinePlayer)
                return;

            ContinuousCheckForItemsForPickup();
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
                        pickableItem.TryToPick(this);
                    }
                    else
                    {
                        PickupLootBox(pickableItem);
                    }
                }
            }
        }

        public void PickItemFromLootBox(Transform itemTransform)
        {
            if (Controller.IsInAttack || Controller.InAir)
                return;

            pickableItem = itemTransform.GetComponent<IPickable>();
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
                        pickableItem.TryToPick(this);
                    }
                }
                else
                {
                    PickupLootBox(pickableItem);
                }
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


        //bool canBePickedUp()
        //{

        //}


        void PickupLootBox(IPickable pickable)
        {
            pickable.TryToPick(this);
        }

        public void AssignLootBox(LootBox lootbox)
        {
            UI.lootBoxUI.PopulateList(lootbox);
        }

        #endregion

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
            //for (int i = 0; i < Weapons.items.Count; i++)
            //{
            //    if (Weapons.items[i] == null)
            //    {
            //        Weapons.items[i] = weapon;

            //        weapon.OnPicked();


            //        WeaponInfo weaponInfo = weapon.GetWeaponInfo();

            //        if (Controller.ActiveWeapon.Prefab == null)
            //        {
            //            Equipping(i, weapon);
            //        }
            //        else
            //        {
            //            Transform restingPoint = Controller.GetRestingPoint(weaponInfo.Data.restingPointType);

            //            weapon.transform.parent = restingPoint.transform;
            //            weapon.transform.localPosition = Vector3.zero;
            //            weapon.transform.localRotation = Quaternion.identity;


            //            UI.hudcontroller.EquipWeapon(i, weaponInfo.Data.icon, false, weapon.Health, weaponInfo.Data.InitialHealth);
            //        }


            //        if (weapon.HasScabbard)
            //        {
            //            weapon.PlaceScabbard(Controller.GetRestingPoint(weaponInfo.Data.restingPointType));
            //        }

            //        return;
            //    }
            //}

            int index = GetMeAvailableSlotForNewWeapon();
            if (index != -1)
            {
                Weapons.items[index] = weapon;

                weapon.OnPicked();


                WeaponInfo weaponInfo = weapon.GetWeaponInfo();

                if (Controller.ActiveWeapon.Prefab == null)
                {
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

                return;
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
            Debug.Log("Quick Item Index = " + index);
            if (index != -1)
            {
                AddItemToQuickItemsByPickup(arrow, index);
            }
            else
            {
                index = GetMeSlotForAllItem();

                Debug.Log("All Item Index = " + index);
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
                Shields.items[index] = shield;
                Shields.AvailedInCategory++;

                shield.gameObject.SetActive(false);
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

            if (isPickableAnItem)
            {
                pickableItem.OnPicked();
            }
            else
            {

            }

            item.transform.parent = ItemsParent;
            item.transform.localPosition = Vector3.zero;
            item.transform.localRotation = Quaternion.identity;

            item.gameObject.SetActive(false);
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

            if (isPickableAnItem)
            {
                pickableItem.OnPicked();
            }
            else
            {

            }

            item.transform.parent = ItemsParent;
            item.transform.localPosition = Vector3.zero;
            item.transform.localRotation = Quaternion.identity;

            item.gameObject.SetActive(false);
        }
        #endregion



    }
}