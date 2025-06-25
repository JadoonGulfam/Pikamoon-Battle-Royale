using UnityEngine;
using UnityEngine.UI;

namespace Pikamoon.UI
{
    public class DragManager : MonoBehaviour
    {
        public static DragManager Instance;

        public UI_ItemSlot draggedSlot;
        public Controller.Item draggedItem;

        [Space]
        public Image icon;

        private void Awake()
        {
            if (Instance == null)
                Instance = this;
            else
                Destroy(gameObject);
        }

        public void StartDrag(UI_ItemSlot slot, Controller.Item item)
        {
            draggedSlot = slot;
            draggedItem = item;
            icon.sprite = item.Data.icon;
        }

        public void EndDrag()
        {
            draggedSlot = null;
            draggedItem = null;
            icon.sprite = null;
        }

        public bool IsDragging => draggedItem != null;
    }
}
