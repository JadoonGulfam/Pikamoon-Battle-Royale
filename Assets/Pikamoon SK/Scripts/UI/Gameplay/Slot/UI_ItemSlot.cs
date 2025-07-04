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
        [System.Serializable]
        public struct SlotAppearenceSettings
        {
            public Color BgIconColor;
            public Color IconColor;
            public bool IconActiveFlag;
        }

        public SlotType slotType;
        [Space]
        [SerializeField]
        protected bool hasDependantSlot; 
        [SerializeField]
        protected UI_ItemSlot DependantSlot;
        [Space]
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
        [Space]
        public int indexInList;


        protected bool isSwappingAllowed;
        protected InventoryUI inventoryUI;
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

        public void AssignDragManager(InventoryUI ui)
        {
            inventoryUI = ui;
            _dragManager = ui._dragManager;
        }

        public virtual void AssignItem() { }

        public abstract void AssignItem(Item item, bool alsoExecuteDependency);
        public abstract void AssignItemByDependentSlot(Item item);
        public abstract void AssignItem(Sprite icon, bool isActive, int health = 100, int _fullHealth = 100);
        public abstract void UnAssignItem(bool alsoExecuteDependency);
        public abstract void UnAssignItemByDependentSlot();
        public abstract void RemoveItem(bool alsoExecuteDependency);
        public abstract void RemoveItemByDependentSlot();
        public abstract void Change();
        public abstract void Select();
        public abstract void UnSelect();
        public abstract Item GetItem();



        public virtual void OnPointerEnter(PointerEventData eventData)
        {
            if (_dragManager == null || !_dragManager.IsDragging) return;

            HighlighterImg.enabled = true;
            _dragManager.hoveredSlot = this;


            var targetItem = GetItem();

            isSwappingAllowed = CanAcceptItem(_dragManager.draggedItem, targetItem, _dragManager.draggedSlot) &&
                _dragManager.draggedSlot.CanAcceptItem(targetItem, targetItem, this);

            HighlighterImg.color = isSwappingAllowed

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
        public abstract bool CanAcceptItem(Item destinationItem, Item sourceItem, UI_ItemSlot sourceSlot);

        //public abstract bool CanAcceptItem(Item DestinationItem, UI_ItemSlot SourceSlot);

    }
}
