using System.Collections.Generic;
using UnityEngine;

namespace Pikamoon.Controller
{
    public class LootBox : MonoBehaviour, IPickable
    {
        public Transform ItemParent;

        public List<Item> items;

        public List<Item> GetItems() => items;

        public void AddItem(Item item)
        {
            if (!items.Contains(item))
                items.Add(item);
        }

        public void RemoveItem(Item item)
        {
            if (items.Contains(item))
            {
                items.Remove(item);
            }
        }
        public void RemoveItem(int index)
        {
            if (items.Count >= index + 1)
            {
                items.RemoveAt(index);
            }
        }

        public void AssignItemToInventory(int index)
        {

        }



        public void OnPicked()
        {
        }

        public void OnPicked(Transform Picker)
        {
        }

        public void TryToPick(InventoryController Picker)
        {
            Picker.AssignLootBox(this);
        }



    }

}