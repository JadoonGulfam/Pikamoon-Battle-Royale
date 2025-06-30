using Pikamoon.Controller;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Pikamoon.UI
{
    public class UI_ShieldSlot : UI_ItemSlot
    {
        private Item CurrentItem;

        private void Start()
        {
            RemoveItem();
        }

        public override void AssignItem(Item item)
        {
            if (item == null) { RemoveItem(); return; }
            CurrentItem = item;
            hasItem = true;
            Icon.sprite = item.Data.icon;
            if (ItemName != null)
                ItemName.text = item.Data.ItemName;
            if (LevelNo != null)
                LevelNo.text = item.Data.LevelNo.ToString();
        }

        public override void AssignItem(Sprite icon, bool isActive, int health = 100, int fullHealth = 100)
        {
            Icon.sprite = icon;
        }

        public override void RemoveItem()
        {
            CurrentItem = null;
            hasItem = false;
            Icon.sprite = null;
            if (ItemName != null)
                ItemName.text = "";
            if (LevelNo != null)
                LevelNo.text = "";
        }

        public override void UnAssignItem() => RemoveItem();
        public override void Change() { }
        public override void Select() { }
        public override void UnSelect() { }
        public override Item GetItem() => hasItem ? CurrentItem : null;

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

            if (targetSlot != null && targetSlot != this &&
                CanAcceptItem(CurrentItem, targetItem, _dragManager.draggedSlot))
            {
                if (targetItem == null)
                {
                    targetSlot.AssignItem(CurrentItem);
                    RemoveItem();
                }
                else
                {
                    targetSlot.AssignItem(CurrentItem);
                    AssignItem(targetItem);
                }
            }
            _dragManager.EndDrag();
        }

        public override bool CanAcceptItem(Item draggedItem, Item targetItem, UI_ItemSlot sourceSlot)
        {
            if (draggedItem == null) return false;
            if (draggedItem.Data.itemType != ItemType.Shield) return false;

            // the SubType of shield (0=head, 1=upper, 2=lower)
            int draggedShieldType = draggedItem.SubType;

            // determine what this slot accepts
            int requiredShieldType = slotType switch
            {
                SlotType.Shield_Head => 0,
                SlotType.Shield_UpperBody => 1,
                SlotType.Shield_LowerBody => 2,
                _ => -1
            };

            if (requiredShieldType == -1)
                return false; // not a shield slot

            // if dragging from another shield slot
            if (sourceSlot is UI_ShieldSlot)
            {
                if (targetItem == null)
                {
                    // empty target, check if dragged shield type matches
                    return draggedShieldType == requiredShieldType;
                }
                else
                {
                    // check target and dragged are both of correct type
                    return targetItem.Data.itemType == ItemType.Shield
                        && targetItem.SubType == requiredShieldType
                        && draggedShieldType == requiredShieldType;
                }
            }
            else
            {
                // dragged from quick/all items
                if (draggedShieldType != requiredShieldType)
                    return false;

                if (targetItem == null) return true;

                return targetItem.Data.itemType == ItemType.Shield && targetItem.SubType == requiredShieldType;
            }
        }


    }
}
