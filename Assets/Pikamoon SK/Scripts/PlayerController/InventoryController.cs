using UnityEngine;
using Pikamoon.UI;
using System.Collections.Generic;
using System;
using Fusion;

namespace Pikamoon.Controller
{

    [Serializable]
    public struct ItemCategory
    {
        public string CategoryName;
        public List<Item> items;
        public int MaxInCategory;
        public int AvailedInCategory;
    }

    public class InventoryController : MonoBehaviour
    {
        public GameObject NetworkManagerob;
        public AnimationController animationController;

        [Header("Weapons")]

        public WeaponInfo DefaultFistNoWeapon;
        [Space]
        [Space]
        public ItemCategory Weapons;

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
        [SerializeField] LootBox lootBox;
        public float yOffsetFromGroundAfterDeath;
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

        [SerializeField] bool isBusyInSwitchingWeapon;

        [SerializeField] private PlayerController Controller;
        private PlayerInput playerInput;

        LootBox CurrentLootbox;
        int IndexInLootBox;

        RaycastHit hitItem;
        Transform hitTransform;
        bool isPickableAnItem;
        bool isPointerOnLootBox;
        bool IsInventoryOpen;
        IPickable pickableItem;
        UIManagerSK UI;

        private void Start()
        {
            isBusyInSwitchingWeapon = false;
            UsingWeaponIndex = 0;
            isUsingWeapon = false;
            IsInventoryOpen = false;
            NetworkManagerob = GameObject.FindGameObjectWithTag("NetworkManager");
        }



        public void Initialize(UIManagerSK _uiManager, PlayerController _controller)
        {
            UI = _uiManager;
            Controller = _controller;
            playerInput = Controller.input;

            //Shields.InitializeCategory();
            //QuickItems.InitializeCategory();
            //AllItems.InitializeCategory();

            UI.inventoryUI.Player = this;
            UI.lootBoxUI._inventory = this;

            Controller.ActivateWeapon(DefaultFistNoWeapon);

            playerInput.onPrimaryWeaponSelect_Down += ChangeToPrimaryWeapon;
            playerInput.onSecondaryWeaponSelect_Down += ChangeToSecondaryWeapon;
            playerInput.onTertiaryWeaponSelect_Down += ChangeToTertiaryWeapon;

            playerInput.onWeaponDrop_Down += DropWeapon;
            playerInput.onPick_Down += Pick;

            playerInput.onInventoryShow_Down += ToggleInventoryUI;



            allowPickUp = true;
        }
        private void Update()
        {
            if (MP_Setup != null && !MP_Setup.isMinePlayer)
                return;

            ContinuousCheckForItemsForPickup();


        }


        void ToggleInventoryUI()
        {
            if (!IsInventoryOpen)
            {
                IsInventoryOpen = true;

                if (isUsingWeapon)
                {
                    UnEquipping(UsingWeaponIndex, Weapons.items[UsingWeaponIndex], true);
                }


                ShowInventoryUI();
                Controller.ToggleCursor(true);
                AllowPickUp = false;
                UI.hudcontroller.canvas.enabled = false;
            }
            else
            {
                IsInventoryOpen = false;
                HideInventoryUI();
                Controller.ToggleCursor(false);
                AllowPickUp = true;
                UI.hudcontroller.canvas.enabled = true;
            }
        }

        public void ContinuousCheckForItemsForPickup()
        {
            if (!allowPickUp || UI.lootBoxUI.LootCanvas.enabled)
                return;


            if (Physics.CheckSphere(this.transform.position, rangeForItemPickup, pickupLayerMask + LootBoxLayerMask))
            {
                Vector2 screenCenterPoint = new Vector2(Screen.width / 2, Screen.height / 2);

                Ray ray = Controller.cameraController._camera.ScreenPointToRay(screenCenterPoint);

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
            // print("item picked");
            if (hitTransform)
            {
                if (Controller.IsInAttack || Controller.InAir)
                    return;
                pickableItem = hitTransform.GetComponent<IPickable>();
                if (pickableItem != null)
                {
                    if (!isPointerOnLootBox)
                    {

                        if(MP_Setup != null)
                        {
                            isPickableAnItem = true;
                            //NetworkManagerob.GetComponent<NetworkManager>().SpawnWeaponAndReturn(0);
                            int weaponId = hitTransform.GetComponent<WeaponData>().id;
                            pickableItem = NetworkManagerob.GetComponent<NetworkManager>().SpawnWeaponAndReturn(weaponId).GetComponent<IPickable>();
                            pickableItem.TryToPick(this);
                            hitTransform.gameObject.SetActive(false);
                            animationController.RequestToDespawn(hitTransform.GetComponent<NetworkObject>().Id);
                            //RequestDespawn(hitTransform.GetComponent<NetworkObject>().Id);
                        }
                        else
                        {
                            isPickableAnItem = true;
                            pickableItem.TryToPick(this);
                        }

                    }
                    else
                    {
                        isPickableAnItem = false;
                        PickupLootBox(pickableItem);
                    }
                }
            }
        }
        public void Pick(Item item)
        {
            if (Controller.IsInAttack || Controller.InAir)
                return;

            pickableItem = item.GetComponent<IPickable>();
            if (pickableItem != null)
            {
                isPickableAnItem = false;
                PickupLootBox(pickableItem);
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


        void ShowInventoryUI()
        {
            UI.inventoryUI._canvas.enabled = true;
            Controller.CameraOrbitStatus = false;
        }
        void HideInventoryUI()
        {
            UI.inventoryUI._canvas.enabled = false;
            Controller.CameraOrbitStatus = true;
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
            UI.inventoryUI.HideUI();
            Controller.CameraOrbitStatus = true;
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

        public void PlaceLootBoxAfterDeath()
        {
            Vector3 LootPosition = this.transform.position + Vector3.up * yOffsetFromGroundAfterDeath;
            if (!Controller.IsGrounded)
            {
                RaycastHit hit;
                Vector3 origin = this.transform.position + Vector3.one * 3;
                if (Physics.Raycast(origin, Vector3.down, out hit, 100, Controller.groundLayer))
                {
                    LootPosition = hit.point + Vector3.up * yOffsetFromGroundAfterDeath;
                }
            }

            lootBox.transform.position = LootPosition;
            lootBox.transform.rotation = Quaternion.identity;

            lootBox.RemoveAllItems();

            for (int i = 0; i < Weapons.AvailedInCategory; i++)
            {
                if (Weapons.items[i] != null)
                {
                    lootBox.AddItem(Weapons.items[i]);
                    Weapons.items[i] = null;
                    Weapons.AvailedInCategory--;
                }
            }

            for (int i = 0; i < Shields.AvailedInCategory; i++)
            {
                if (Shields.items[i] != null)
                {
                    lootBox.AddItem(Shields.items[i]);
                    Shields.items[i] = null;
                    Shields.AvailedInCategory--;
                }
            }

            for (int i = 0; i < AllItems.AvailedInCategory; i++)
            {
                if (AllItems.items[i] != null)
                {
                    lootBox.AddItem(AllItems.items[i]);
                    AllItems.items[i] = null;
                    AllItems.AvailedInCategory--;
                }
            }

            for (int i = 0; i < QuickItems.AvailedInCategory; i++)
            {
                if (QuickItems.items[i] != null)
                {
                    lootBox.AddItem(QuickItems.items[i]);
                    QuickItems.items[i] = null;
                    QuickItems.AvailedInCategory--;
                }
            }

            lootBox.gameObject.SetActive(true);
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
            if (Weapons.items[0] == null || isBusyInSwitchingWeapon)
                return;

            if (Controller.IsInAttack || Controller.InAir || Controller.IsSwimming)
                return;

            isBusyInSwitchingWeapon = true;
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
            if (Weapons.items[1] == null || isBusyInSwitchingWeapon)
                return;

            if (Controller.IsInAttack || Controller.InAir || Controller.IsSwimming)
                return;


            isBusyInSwitchingWeapon = true;
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
        void ChangeToTertiaryWeapon()
        {
            if (Weapons.items[2] == null || isBusyInSwitchingWeapon)
                return;


            if (Controller.IsInAttack || Controller.InAir || Controller.IsSwimming)
                return;


            isBusyInSwitchingWeapon = true;
            if (UsingWeaponIndex == 2)
            {
                if (isUsingWeapon)
                {
                    UnEquipping(2, Weapons.items[2], true);
                }
                else
                {
                    Equipping(2, Weapons.items[2]);
                }
            }
            else
            {
                if (isUsingWeapon)
                {
                    UnEquipping(UsingWeaponIndex, Weapons.items[UsingWeaponIndex], false);

                    Equipping(2, Weapons.items[2]);
                }
                else
                {
                    Equipping(2, Weapons.items[2]);
                }
            }
        }

        public void WeaponSwitchingComplete()
        {
            isBusyInSwitchingWeapon = false;
        }
        void UnEquipping(int index, Item item, bool ActivateNoWeapon)
        {
            Weapon weapon = item.GetItemAs<Weapon>();

            WeaponInfo weaponInfo = weapon.GetWeaponInfo();

            Controller.AC.PAnimator.SetInteger(Controller.AC.Parameters.SecondaryState.Hash, 400 + (int)weaponInfo.Data.restingPointType);
            Controller.AC.PAnimator.SetTrigger(Controller.AC.Parameters.UnEquip.Hash);
            Controller.AC.SetAnimatorLayer(3, 1);


            weapon.OnUnEquip();

            UI.hudcontroller.UnEquipWeapon(index, DefaultFistNoWeapon.Data.AimIcon, DefaultFistNoWeapon.Data.icon);


            isUsingWeapon = false;

            if (ActivateNoWeapon)
            {
                Controller.ActivateWeapon(DefaultFistNoWeapon);
            }
            else
            {
                Transform restingPoint = Controller.GetRestingPoint(weaponInfo.Data.restingPointType);

                weapon.transform.parent = restingPoint.transform;
                weapon.transform.localPosition = Vector3.zero;
                weapon.transform.localRotation = Quaternion.identity;

            }
        }

        void Equipping(int index, Item item)
        {
            Weapon weapon = item.GetItemAs<Weapon>();

            WeaponInfo weaponInfo = weapon.GetWeaponInfo();

            //Weapon should be on rest point before handed over to Holding Point
            Transform restingPoint = Controller.GetRestingPoint(weaponInfo.Data.restingPointType);
            weapon.transform.parent = restingPoint.transform;
            weapon.transform.localPosition = Vector3.zero;
            weapon.transform.localRotation = Quaternion.identity;

            weapon.OnEquip();

            Controller.ActivateWeapon(weaponInfo);

            UI.hudcontroller.EquipWeapon(index, item, true, weaponInfo.Data.AimIcon);

            Controller.AC.PAnimator.SetInteger(Controller.AC.Parameters.SecondaryState.Hash, 300 + (int)weaponInfo.Data.restingPointType);
            Controller.AC.PAnimator.SetTrigger(Controller.AC.Parameters.Equip.Hash);
            Controller.AC.SetAnimatorLayer(3, 1);

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

            RemoveWeaponsFromList(UsingWeaponIndex);

            Controller.ActivateWeapon(DefaultFistNoWeapon);
        }
        void DropWeapon(int index)
        {
            if (Weapons.items[index] == null)
                return;

            if (Controller.IsInAttack || Controller.InAir || Controller.IsSwimming)
                return;

            isUsingWeapon = false;

            //UI.hudcontroller.DropWeapon(index);

            Weapons.items[index].GetItemAs<Weapon>().OnDrop(this.transform, Controller.groundLayer);

            RemoveWeaponsFromList(index);

            Controller.ActivateWeapon(DefaultFistNoWeapon);
        }


        void AddItemToWeaponsByPickup(Weapon weapon, int index)
        {
            AddWeaponsToList(weapon, index);

            UI.inventoryUI.AssignToWeapons(weapon, index, UsingWeaponIndex == index);

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
                if (isEquipeWeapon)
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
        public void AddWeaponsToList(Item item, int index)
        {
            if (Weapons.items[index] == null)
            {
                Weapons.items[index] = item;
                Weapons.AvailedInCategory++;
            }
        }


        public void RemoveWeaponsFromList(int index)
        {
            if (Weapons.items[index] != null)
            {
                Weapons.items[index] = null;
                Weapons.AvailedInCategory--;
            }
        }
        public void DropWeaponsFromList(int index)
        {
            DropWeapon(index);
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
            AddShieldsToList(item, index);

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

        public void AddShieldsToList(Item item, int index)
        {
            if (Shields.items[index] == null)
            {
                Shields.items[index] = item;
                Shields.AvailedInCategory++;
            }
        }

        public void RemoveShieldsFromList(int index)
        {
            if (Shields.items[index] != null)
            {
                Shields.items[index] = null;
                Shields.AvailedInCategory--;
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
            AddQuickItemsToList(item, index);
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

        public void AddQuickItemsToList(Item item, int index)
        {
            if (QuickItems.items[index] == null)
            {
                QuickItems.items[index] = item;
                QuickItems.AvailedInCategory++;
            }

        }

        public void RemoveQuickItemsFromList(int index)
        {
            if (QuickItems.items[index] != null)
            {
                QuickItems.items[index] = null;
                QuickItems.AvailedInCategory--;
            }
        }


        public void DropFromQuickItem(int index)
        {
            if (QuickItems.items[index] == null)
                return;

            if (Controller.IsInAttack || Controller.InAir || Controller.IsSwimming)
                return;
            
            
            

            QuickItems.items[index].OnDrop(this.transform, Controller.groundLayer);
            



            RemoveQuickItemsFromList(index);

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
            AddAllItemsToList(item, index);
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

        public void AddAllItemsToList(Item item, int index)
        {
            if (AllItems.items[index] == null)
            {
                AllItems.items[index] = item;
                AllItems.AvailedInCategory++;
            }
        }

        public void RemoveAllItemsFromList(int index)
        {
            if (AllItems.items[index] != null)
            {
                AllItems.items[index] = null;
                AllItems.AvailedInCategory--;
            }
        }

        #endregion

    }
}