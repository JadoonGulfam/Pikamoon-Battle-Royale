using TMPro;
using UnityEngine;

namespace Pikamoon.UI
{

    public class UI_LootSlot : UI_ItemSlot
    {
        [Space(20)]
        public TextMeshProUGUI Quantity;

        public override void AssignItem(Sprite icon)
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
            Debug.Log("Name = " + transform.name);
        }

        public override void UnSelect()
        {
        }
    }

}