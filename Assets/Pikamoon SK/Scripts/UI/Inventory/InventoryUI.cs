using Pikamoon.Controller;
using UnityEngine;
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

        [Space]
        public UI_ItemCategory Weapons;

        [Space]
        public UI_ItemCategory Shields;

        [Space]
        public UI_ItemCategory QuickItems;

        [Space]
        public UI_ItemCategory AllItems;


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