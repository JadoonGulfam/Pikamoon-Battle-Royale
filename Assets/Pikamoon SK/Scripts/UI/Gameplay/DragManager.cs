using Pikamoon.Controller;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Pikamoon.UI
{
    public class DragManager : MonoBehaviour
    {
        public Item draggedItem;
        public UI_ItemSlot draggedSlot;
        public UI_ItemSlot hoveredSlot;
        public EventSystem _eventSystem;


        private Canvas canvas;
        [Header("UI")]
        public Image dragIcon;

        private void Awake()
        {
            canvas = GetComponentInParent<Canvas>();
        }

        public bool IsDragging => draggedItem != null;

        public void StartDrag(UI_ItemSlot slot, Item item)
        {
            draggedSlot = slot;
            draggedItem = item;
            hoveredSlot = slot;

            if (dragIcon != null && item != null)
            {
                dragIcon.sprite = item.Data.icon;
            }
        }

        public void EnableDraggedIcon()
        {
            dragIcon.enabled = true;
            dragIcon.gameObject.SetActive(true);
        }

        public void EndDrag()
        {
            draggedSlot = null;
            draggedItem = null;
            hoveredSlot = null;

            if (dragIcon != null)
            {
                dragIcon.gameObject.SetActive(false);
            }
        }
        public void SetHoveredSlot(UI_ItemSlot slot)
        {
            hoveredSlot = slot;
        }

        public void ClearHoveredSlot(UI_ItemSlot slot)
        {
            if (hoveredSlot == slot)
                hoveredSlot = null;
        }

        public void SwapItems()
        {
            if (!IsDragging) return;

            var targetSlot = hoveredSlot;
            var targetItem = targetSlot?.GetItem();

            if (draggedSlot == targetSlot)
            {
                EndDrag();
                return;
            }

            if (targetSlot == null)// && draggedSlot != targetSlot)
            {
                draggedSlot.DropItem();
                draggedSlot.RemoveItem(true);
                EndDrag();
                return; 
            }

            if (targetSlot.CanAcceptItem(hoveredSlot.GetItem(), draggedItem, draggedSlot))
            {
                targetSlot.AssignItem(draggedItem, true);

                if (targetItem == null)
                {
                    draggedSlot.RemoveItem(true);
                }
                else
                {
                    draggedSlot.AssignItem(targetItem, true);
                }
            }

            EndDrag();
        }


        void Update()
        {
            if (IsDragging)
            {
                Vector2 pos;
                RectTransformUtility.ScreenPointToLocalPointInRectangle(
                    canvas.transform as RectTransform,
                    Input.mousePosition, canvas.worldCamera, out pos
                );
                dragIcon.rectTransform.anchoredPosition = pos;
            }
        }
    }
}
