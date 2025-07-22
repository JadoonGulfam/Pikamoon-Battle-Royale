using Pikamoon.Controller;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Pikamoon.UI
{

    public class UI_LootSlot : UI_ItemSlot
    {
        [Space(20)]
        [SerializeField] LootBox_UI lootBox_UI;
        public TextMeshProUGUI Quantity;


        Item CurrentItem;




        public override void AssignItem(Item item, bool alsoExecuteDependency)
        {
        }
        public override void AssignItemByDependentSlot(Item item)
        {
        }


        public override void AssignItem(Sprite icon, bool isActive, int health = 100, int _fullHealth = 100)
        {
        }
        public override void UnAssignItem(bool alsoExecuteDependency)
        {
        }
        public override void UnAssignItemByDependentSlot()
        {
        }

        public override void RemoveItem(bool alsoExecuteDependency)
        {
        }
        public override void RemoveItemByDependentSlot()
        {
        }
        public override void Change()
        {
        }

        public override void Select()
        {
            lootBox_UI.ClickOnItem(indexInList);
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
        public override void OnPointerClick(PointerEventData eventData)
        {

        }

        public override Item GetItem()
        {
            return CurrentItem;
        }

        public override bool CanAcceptItem(Controller.Item draggedItem, Controller.Item targetItem, UI_ItemSlot sourceSlot)
        {
            return false;
        }


    }
}
