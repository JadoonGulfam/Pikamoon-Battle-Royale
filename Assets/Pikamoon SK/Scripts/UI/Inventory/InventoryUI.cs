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

        public InventoryController Player;

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
                item.AssignDragManager(this);
            }

            foreach (var item in Shields.Items)
            {
                item.AssignDragManager(this);
            }

            foreach (var item in QuickItems.Items)
            {
                item.AssignDragManager(this);
            }

            foreach (var item in AllItems.Items)
            {
                item.AssignDragManager(this);
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

        public void AssignToWeapons(Item item, int index)
        {
            Weapons.Items[index].AssignItem(item, false);
        }

        public void removeFromWeapons()
        {

        }


        public void AssignToShields(Item item, int index)
        {
            Shields.Items[index].AssignItem(item,false);
        }
        public void AssignToQuickItems(Item item, int index)
        {

            QuickItems.Items[index].AssignItem(item, true);
        }
        public void AssignToAllItems(Item item, int index)
        {

            AllItems.Items[index].AssignItem(item, true);
        }


        //[ContextMenu("Assign Indexes To List")]
        //void AssignIndexes()
        //{
        //    for (int i = 0; i < Weapons.Items.Length; i++)
        //    {
        //        Weapons.Items[i].indexInList = i;
        //    }

        //    for (int i = 0; i < Shields.Items.Length; i++)
        //    {
        //        Shields.Items[i].indexInList = i;
        //    }


        //    for (int i = 0; i < QuickItems.Items.Length; i++)
        //    {
        //        QuickItems.Items[i].indexInList = i;
        //    }

        //    for (int i = 0; i < AllItems.Items.Length; i++)
        //    {
        //        AllItems.Items[i].indexInList = i;
        //    }

        //}
    }
}