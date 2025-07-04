using Pikamoon.Controller;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Pikamoon.UI
{
    public class DragManager : MonoBehaviour
    {
        public UI_ItemSlot draggedSlot;
        public Item draggedItem;
        public EventSystem _eventSystem;
        public UI_ItemSlot hoveredSlot;


        private Canvas canvas;
        [Header("UI")]
        public Image dragIcon;  // Assign in inspector

        private void Awake()
        {
            canvas = GetComponentInParent<Canvas>();
        }

        public bool IsDragging => draggedItem != null;

        public void StartDrag(UI_ItemSlot slot, Controller.Item item)
        {
            draggedSlot = slot;
            draggedItem = item;

            if (dragIcon != null && item != null)
            {
                dragIcon.sprite = item.Data.icon;
                dragIcon.enabled = true;
                dragIcon.gameObject.SetActive(true);
            }
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
