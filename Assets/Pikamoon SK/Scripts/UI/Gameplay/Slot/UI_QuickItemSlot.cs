using UnityEngine;
using UnityEngine.EventSystems;
using Pikamoon.Controller;

namespace Pikamoon.UI
{
    public class UI_QuickItemSlot : UI_ItemSlot
    {

        [Header("Settings")]
        public SlotAppearenceSettings EmptySlotSettings;
        public SlotAppearenceSettings ActiveSlotSettings;
        public SlotAppearenceSettings InActiveSlotSettings;

        private Item CurrentItem;

        protected override void Awake()
        {
            base.Awake();
            ChangeButtonAppearance(EmptySlotSettings);
        }

        public override void AssignItem(Item item, bool alsoExecuteDependency)
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
            
            inventoryUI._inventory.AddQuickItemsToList(item, indexInList);

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


        public override void AssignItem(Sprite icon, bool isActive, int _fullHealth = 100, int _health = 100)
        {
            if (Icon)
                Icon.sprite = icon;

            ChangeButtonAppearance(isActive ? ActiveSlotSettings : InActiveSlotSettings);
        }

        public override void UnAssignItem(bool alsoExecuteDependency)
        {
            CurrentItem = null;
            hasItem = false;
            ChangeButtonAppearance(InActiveSlotSettings);


            inventoryUI._inventory.RemoveQuickItemsFromList(indexInList);

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

            inventoryUI._inventory.RemoveQuickItemsFromList(indexInList);

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
            if (Icon)
            {
                Icon.enabled = settings.IconActiveFlag;
                Icon.color = settings.IconColor;
            }
        }

        public override Item GetItem() => hasItem ? CurrentItem : null;
        public override void Change() { }
        public override void Select() { }
        public override void UnSelect() { }

        public override void OnPointerExit(PointerEventData eventData)
        {
            HighlighterImg.enabled = false;
            HighlighterImg.color = Color.red;

            if (_dragManager?.hoveredSlot == this)
                _dragManager.hoveredSlot = null;
        }

        public override void OnPointerDown(PointerEventData eventData)
        {
            if (hasItem)
                _dragManager.StartDrag(this, CurrentItem);
        }

        public override void OnPointerUp(PointerEventData eventData)
        {
            _dragManager.SwapItems();
        }

        public override bool CanAcceptItem(Item destinationItem, Item sourceItem, UI_ItemSlot sourceSlot)
        {
            if (destinationItem == null)
            {
                return true;
            }
            else
            {
                if (sourceSlot.slotType == SlotType.Weapon)
                {
                    return destinationItem.Data.itemType == ItemType.Weapon;
                }
                else if (sourceSlot.slotType == SlotType.Shield_Head)
                {
                    return destinationItem.Data.itemType == ItemType.Shield && destinationItem.SubType == 0;
                }
                else if (sourceSlot.slotType == SlotType.Shield_UpperBody)
                {
                    return destinationItem.Data.itemType == ItemType.Shield && destinationItem.SubType == 1;
                }
                else if (sourceSlot.slotType == SlotType.Shield_LowerBody)
                {
                    return destinationItem.Data.itemType == ItemType.Shield && destinationItem.SubType == 2;
                }
            }

            // other slots: allow anything
            return true;
        }
    }
}
