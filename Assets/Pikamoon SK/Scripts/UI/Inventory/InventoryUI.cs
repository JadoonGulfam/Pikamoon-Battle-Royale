using Pikamoon.Controller;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Pikamoon.UI
{
    [System.Serializable]
    public struct UI_ItemCategory
    {
        public string CategoryName;
        public UI_ItemSlot[] Items;
        public int MaxInCategory;
        public int AvailedInCategory;
    }

    public class InventoryUI : MonoBehaviour
    {
        public Canvas _canvas;

        public InventoryController _inventory;

        [SerializeField] private Image CharacterImg;


        public DragManager _dragManager;

        [Space]
        public UI_ItemCategory Weapons;

        [Space]
        public UI_ItemCategory Shields;

        [Space]
        public UI_ItemCategory QuickItems;

        [Space]
        public UI_ItemCategory AllItems;

        private void Start()
        {
            AssignDragManagerToAllItemsSlots();
        }

        void AssignDragManagerToAllItemsSlots()
        {
            foreach (var item in Weapons.Items)
            {
                item.AssignDragManager(_dragManager);
            }

            foreach (var item in Shields.Items)
            {
                item.AssignDragManager(_dragManager);
            }

            foreach (var item in QuickItems.Items)
            {
                item.AssignDragManager(_dragManager);
            }

            foreach (var item in AllItems.Items)
            {
                item.AssignDragManager(_dragManager);
            }
        }

        // Update is called once per frame
        void Update()
        {

        }

        public void PopulateList()
        {

        }

        public void ShowUI()
        {
            _canvas.enabled = true;
        }

        public void HideUI()
        {
            _canvas.enabled = false;
        }

        public void AssignToWeapons(Controller.Item item, int index)
        {
            Weapons.Items[index].AssignItem(item);
        }
        public void AssignToShields(Controller.Item item, int index)
        {
            Shields.Items[index].AssignItem(item);
        }
        public void AssignToQuickItems(Controller.Item item, int index)
        {

            QuickItems.Items[index].AssignItem(item);
        }
        public void AssignToAllItems(Controller.Item item, int index)
        {

            AllItems.Items[index].AssignItem(item);
        }
    }
}