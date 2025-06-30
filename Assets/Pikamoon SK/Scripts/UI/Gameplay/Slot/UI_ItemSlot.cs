using Pikamoon.Controller;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Pikamoon.UI
{
    [System.Serializable]
    public enum SlotType
    {
        Loot,
        Weapon,
        Shield_Head,
        Shield_UpperBody,
        Shield_LowerBody,
        QuickItem,
        AllItems
    }

    public abstract class UI_ItemSlot : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerEnterHandler, IPointerExitHandler
    {
        public SlotType slotType;
        public Image BtnBg;
        public Image Icon;
        public TextMeshProUGUI ItemName;
        public TextMeshProUGUI LevelNo;
        [Space]
        public Image HighlighterImg;
        [Space]
        public bool HasHotKey;
        public TextMeshProUGUI HotKey;
        [Space]
        public bool hasItem;

        protected DragManager _dragManager;
        protected ShieldType _slotShieldType;

        protected virtual void Awake()
        {
            if (slotType == SlotType.Shield_Head)
                _slotShieldType = ShieldType.Head;
            else if (slotType == SlotType.Shield_UpperBody)
                _slotShieldType = ShieldType.UpperBody;
            else if (slotType == SlotType.Shield_LowerBody)
                _slotShieldType = ShieldType.LowerBody;
        }

        public void AssignDragManager(DragManager dm)
        {
            _dragManager = dm;
        }

        public virtual void AssignItem() { }

        public abstract void AssignItem(Controller.Item item);
        public abstract void AssignItem(Sprite icon, bool isActive, int health = 100, int _fullHealth = 100);
        public abstract void UnAssignItem();
        public abstract void RemoveItem();
        public abstract void Change();
        public abstract void Select();
        public abstract void UnSelect();
        public abstract Controller.Item GetItem();



        public virtual void OnPointerEnter(PointerEventData eventData)
        {
            if (_dragManager == null || !_dragManager.IsDragging) return;

            HighlighterImg.enabled = true;
            _dragManager.hoveredSlot = this;

            var draggedItem = _dragManager.draggedItem;
            var sourceSlot = _dragManager.draggedSlot;
            var targetItem = GetItem();

            HighlighterImg.color =
                CanAcceptItem(draggedItem, targetItem, sourceSlot) &&
                sourceSlot.CanAcceptItem(draggedItem, targetItem, sourceSlot)
                ? Color.green
                : Color.red;
        }

        public virtual void OnPointerExit(PointerEventData eventData)
        {
            HighlighterImg.enabled = false;
            if (_dragManager?.hoveredSlot == this)
                _dragManager.hoveredSlot = null;
        }

        public abstract void OnPointerDown(PointerEventData eventData);
        public abstract void OnPointerUp(PointerEventData eventData);


        // highly optimized consistent logic
        public virtual bool CanAcceptItem(Controller.Item draggedItem, Controller.Item targetItem, UI_ItemSlot sourceSlot)
        {
            if (draggedItem == null) return false;

            // WEAPON rules
            if (sourceSlot is UI_WeaponSlot)
            {
                if (targetItem == null) return true;
                return targetItem.Data.itemType == ItemType.Weapon;
            }

            // other slots: allow anything
            return true;
        }

    }
}
