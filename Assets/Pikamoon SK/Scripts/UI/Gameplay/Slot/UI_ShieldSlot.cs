using Pikamoon.Controller;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Pikamoon.UI
{
    public class UI_ShieldSlot : UI_ItemSlot
    {
        private Item CurrentItem;

        [Header("Settings")]
        public SlotAppearenceSettings EmptySlotSettings;
        public SlotAppearenceSettings ActiveSlotSettings;
        public SlotAppearenceSettings InActiveSlotSettings;
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

            inventoryUI._inventory.AddShieldsToList(item, indexInList);

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

            inventoryUI._inventory.RemoveShieldsFromList(indexInList);

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

            inventoryUI._inventory.RemoveShieldsFromList(indexInList);


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



        public override void OnPointerEnter(PointerEventData eventData)
        {
            if (_dragManager == null || !_dragManager.IsDragging)
            {
                isSwappingAllowed = false;
                return;
            }

            _dragManager.hoveredSlot = this;


            if (_dragManager.draggedSlot == _dragManager.hoveredSlot)
                return;


            HighlighterImg.enabled = true;


            isSwappingAllowed = _dragManager.hoveredSlot.CanAcceptItem(_dragManager.hoveredSlot.GetItem(), _dragManager.draggedItem, _dragManager.draggedSlot);

            HighlighterImg.color = isSwappingAllowed

                ? Color.green
                : Color.red;

        }

        public override void OnPointerDown(PointerEventData eventData)
        {
            if (hasItem)
                _dragManager.StartDrag(this, CurrentItem);
        }

        public override void OnPointerUp(PointerEventData eventData)
        {
            if (!_dragManager.IsDragging) return;

            var targetSlot = _dragManager.hoveredSlot;
            var targetItem = targetSlot?.GetItem();

            if (targetSlot == null || _dragManager.draggedSlot == targetSlot)
            {
                _dragManager.EndDrag();
                return;
            }


            if (targetSlot.CanAcceptItem(_dragManager.hoveredSlot.GetItem(), _dragManager.draggedItem, _dragManager.draggedSlot))
            {
                targetSlot.AssignItem(_dragManager.draggedItem, true);

                if (targetItem == null)
                {
                    _dragManager.draggedSlot.RemoveItem(true);
                }
                else
                {
                    _dragManager.draggedSlot.AssignItem(targetItem, true);
                }

                HighlighterImg.enabled = false;
            }

            _dragManager.EndDrag();
        }


        public override bool CanAcceptItem(Item destinationItem, Item sourceItem, UI_ItemSlot sourceSlot)
        {
            if (destinationItem.Data.itemType != ItemType.Shield) 
                return false;

            // determine what this slot accepts
            int requiredShieldType = slotType switch
            {
                SlotType.Shield_Head => 0,
                SlotType.Shield_UpperBody => 1,
                SlotType.Shield_LowerBody => 2,
                _ => -1
            };

            if(requiredShieldType != -1 && sourceItem.SubType == requiredShieldType)
            {
                return true;
            }
            else
                return false;
        }


    }
}
