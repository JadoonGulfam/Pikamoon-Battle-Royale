using UnityEngine;
using UnityEngine.EventSystems;

namespace Pikamoon.UI
{
    public class UI_AllItemsSlot : UI_ItemSlot
    {
        [System.Serializable]
        public struct SlotAppearenceSettings
        {
            public Color BgIconColor;
            public Color IconColor;
            public bool IconActiveFlag;
        }

        [Header("Settings")]
        public SlotAppearenceSettings EmptySlotSettings;
        public SlotAppearenceSettings ActiveSlotSettings;
        public SlotAppearenceSettings InActiveSlotSettings;

        private Controller.Item CurrentItem;

        protected override void Awake()
        {
            base.Awake();
            ChangeButtonAppearance(EmptySlotSettings);
        }

        public override void AssignItem(Controller.Item item)
        {
            if (item == null) { RemoveItem(); return; }
            CurrentItem = item;
            hasItem = true;
            Icon.sprite = item.Data.icon;
            ItemName.text = item.Data.ItemName;
            ChangeButtonAppearance(ActiveSlotSettings);
        }

        public override void AssignItem(Sprite icon, bool isActive, int _fullHealth = 100, int _health = 100)
        {
            Icon.sprite = icon;
            ChangeButtonAppearance(isActive ? ActiveSlotSettings : InActiveSlotSettings);
        }

        public override void UnAssignItem()
        {
            CurrentItem = null;
            hasItem = false;
            ChangeButtonAppearance(InActiveSlotSettings);
        }

        public override void RemoveItem()
        {
            Icon.sprite = null;
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

        public override Controller.Item GetItem() => hasItem ? CurrentItem : null;
        public override void Change() { }
        public override void Select() { }
        public override void UnSelect() { }

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

            if (targetSlot != null && targetSlot != this)
            {
                if (CanAcceptItem(CurrentItem, targetItem, _dragManager.draggedSlot))
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
            }
            _dragManager.EndDrag();
        }
    }
}
