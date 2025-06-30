using Pikamoon.Controller;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Pikamoon.UI
{

    public class UI_LootSlot : UI_ItemSlot
    {
        [SerializeField] int IndexInLootBox;
        [Space(20)]
        [SerializeField] LootBox_UI lootBox_UI;
        public TextMeshProUGUI Quantity;


        Controller.Item CurrentItem;

        public override void AssignItem(Controller.Item item)
        {
        }


        public override void AssignItem(Sprite icon, bool isActive, int health = 100, int _fullHealth = 100)
        {
        }
        public override void UnAssignItem()
        {
        }
        public override void Change()
        {
        }

        public override void RemoveItem()
        {
        }

        public override void Select()
        {
            lootBox_UI.ClickOnItem(IndexInLootBox);
        }

        public override void UnSelect()
        {
        }

        public override void OnPointerDown(PointerEventData eventData)
        {
        }

        public override void OnPointerUp(PointerEventData eventData)
        {
        }

        public override void OnPointerEnter(PointerEventData eventData)
        {
        }

        public override void OnPointerExit(PointerEventData eventData)
        {
        }

        public override Controller.Item GetItem()
        {
            return CurrentItem;
        }

        public override bool CanAcceptItem(Controller.Item draggedItem, Controller.Item targetItem, UI_ItemSlot sourceSlot)
        {
            return false;
        }
    }
}
