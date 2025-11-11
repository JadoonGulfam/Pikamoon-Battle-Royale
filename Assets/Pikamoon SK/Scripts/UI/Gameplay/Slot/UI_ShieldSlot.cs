using Pikamoon.Controller;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Pikamoon.UI
{
    public class UI_ShieldSlot : UI_ItemSlot
    {

        [Header("Settings")]
        public SlotAppearenceSettings EmptySlotSettings;
        public SlotAppearenceSettings ActiveSlotSettings;
        public SlotAppearenceSettings InActiveSlotSettings;
        protected override void Awake()
        {
            base.Awake();
            ChangeButtonAppearance(EmptySlotSettings);
        }

        public override void AssignItem(Item item, bool alsoExecuteDependency, bool isEquipped = false)
        {
            if (item == null)
            {
                RemoveItem(true);
                return;
            }

            CurrentItem = item;
            hasItem = true;

            if (Icon)
                Icon.sprite = item.Data.icon;

            if (ItemName)
                ItemName.text = item.Data.ItemName;

            ChangeButtonAppearance(ActiveSlotSettings);

            inventoryUI.Player.AddShieldsToList(item, indexInList);

            if (alsoExecuteDependency && hasDependantSlot)
                DependantSlot.AssignItemByDependentSlot(item);
        }

        public override void AssignItemByDependentSlot(Item item)
        {
            if (item == null) { RemoveItem(true); return; }

            CurrentItem = item;
            hasItem = true;

            if (Icon)
                Icon.sprite = item.Data.icon;

            if (ItemName)
                ItemName.text = item.Data.ItemName;

            ChangeButtonAppearance(ActiveSlotSettings);
        }


        //public override void AssignItem(Sprite icon, bool isActive, int _fullHealth = 100, int _health = 100)
        //{
        //    if (Icon)
        //        Icon.sprite = icon;

        //    ChangeButtonAppearance(isActive ? ActiveSlotSettings : InActiveSlotSettings);
        //}

        public override void UnAssignItem(bool alsoExecuteDependency)
        {
            if (Icon)
                Icon.sprite = null;
            if (ItemName)
                ItemName.text = "";
            CurrentItem = null;
            hasItem = false;

            ChangeButtonAppearance(InActiveSlotSettings);

            inventoryUI.Player.RemoveShieldsFromList(indexInList);

            if (alsoExecuteDependency && hasDependantSlot)
                DependantSlot.UnAssignItemByDependentSlot();
        }

        public override void UnAssignItemByDependentSlot()
        {
            CurrentItem = null;
            hasItem = false;

            ChangeButtonAppearance(InActiveSlotSettings);
        }

        public override void RemoveItem(bool alsoExecuteDependency)
        {
            if (Icon)
                Icon.sprite = null;
            if (ItemName)
                ItemName.text = "";
            CurrentItem = null;
            hasItem = false;

            ChangeButtonAppearance(EmptySlotSettings);

            inventoryUI.Player.RemoveShieldsFromList(indexInList);

            if (alsoExecuteDependency && hasDependantSlot)
                DependantSlot.RemoveItemByDependentSlot();
        }

        public override void RemoveItemByDependentSlot()
        {
            if (Icon)
                Icon.sprite = null;

            if (ItemName)
                ItemName.text = "";

            CurrentItem = null;
            hasItem = false;

            ChangeButtonAppearance(EmptySlotSettings);
        }

        private void ChangeButtonAppearance(SlotAppearenceSettings settings)
        {
            BtnBg.color = settings.BgIconColor;
            Icon.enabled = settings.IconActiveFlag;
            Icon.color = settings.IconColor;
        }

        public override void Change() { }
        public override void Select() { }
        public override void UnSelect() { }
        public override Item GetItem() => hasItem ? CurrentItem : null;



        public override void OnPointerClick(PointerEventData eventData)
        {

        }

        public override bool CanAcceptItem(Item destinationItem, Item sourceItem, UI_ItemSlot sourceSlot)
        {
            if (sourceItem.Data.itemType != ItemType.Shield)
                return false;

            if (destinationItem != null && destinationItem.Data.itemType != ItemType.Shield) 
                return false;

            int requiredShieldType = -1;

            switch(slotType)
            {
                case SlotType.Shield_Head:
                    requiredShieldType = 0;
                    break;
                case SlotType.Shield_UpperBody:
                    requiredShieldType = 1;
                    break;
                case SlotType.Shield_LowerBody:
                    requiredShieldType = 2;
                    break;
            }


            if(requiredShieldType != -1 && sourceItem.SubType == requiredShieldType)
            {
                return true;
            }
            else
                return false;
        }

        public override void DropItem()
        {
            throw new System.NotImplementedException();
        }
    }
}
