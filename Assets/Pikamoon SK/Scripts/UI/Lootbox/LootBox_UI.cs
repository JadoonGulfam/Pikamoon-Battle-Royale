using Pikamoon.Controller;
using UnityEngine;

namespace Pikamoon.UI
{


    public class LootBox_UI : MonoBehaviour
    {
        public InventoryController _inventory;

        public Canvas LootCanvas;

        public UI_LootSlot[] itemSlots;


        LootBox _lootbox;
        int previousSize;

        private void Start()
        {
            previousSize = 0;
        }

        public void PopulateList(LootBox lootbox)
        {
            LootCanvas.enabled = true;

            _lootbox = lootbox;

            for(int i  = 0; i < _lootbox.items.Count;i++)
            {
                itemSlots[i].ItemName.text = _lootbox.items[i].Data.ItemName;
                itemSlots[i].Icon.sprite = _lootbox.items[i].Data.icon;
                itemSlots[i].LevelNo.text = _lootbox.items[i].Data.LevelNo;

                itemSlots[i].Quantity.text = lootbox.items[i].Quantity + string.Empty;
                itemSlots[i].gameObject.SetActive(true);
            }

            for (int i = _lootbox.items.Count - 1; i < itemSlots.Length; i++)
            {
                itemSlots[i].gameObject.SetActive(false);
            }
        }


        public void ClickOnItem(int index)
        {

        }
    }

}