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

        public virtual void AssignItem()
        {

        }
        public abstract void AssignItem(Controller.Item item);
        public abstract void AssignItem(Sprite icon,bool isActive, int health = 100, int _fullHealth = 100);

        public abstract void UnAssignItem();


        public abstract void RemoveItem();


        public abstract void Change();
        public abstract void Select();
        public abstract void UnSelect();


        public virtual bool CanAcceptItem(Controller.Item item)
        {
            // Default to true — override in subclasses
            return true;
        }

        protected void SwapItems(UI_ItemSlot targetSlot)
        {
            if (!DragManager.Instance.IsDragging) return;

            Debug.Log("Swapped Item 1");

            var sourceSlot = DragManager.Instance.draggedSlot;
            var draggedItem = DragManager.Instance.draggedItem;

            if (!targetSlot.CanAcceptItem(draggedItem) || !sourceSlot.CanAcceptItem(targetSlot.GetItem()))
                return;


            Debug.Log("Swapped Item 2");

            // Cache items
            var tempItem = targetSlot.GetItem();

            // Swap
            targetSlot.AssignItem(draggedItem);
            sourceSlot.AssignItem(tempItem);

            DragManager.Instance.EndDrag();
        }

        public abstract Controller.Item GetItem();


        public abstract void OnPointerDown(PointerEventData eventData);

        public abstract void OnPointerUp(PointerEventData eventData);

        public abstract void OnPointerEnter(PointerEventData eventData);

        public abstract void OnPointerExit(PointerEventData eventData);
    }
}