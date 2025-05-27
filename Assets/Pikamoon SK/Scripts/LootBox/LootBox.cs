using System.Collections.Generic;
using UnityEngine;

namespace Pikamoon.Controller
{
    public class LootBox : MonoBehaviour, IPickable
    {
        public Transform ItemParent;

        public List<Item> items;

        public List<Item> GetItems() => items;

        public int TotalItems;
        public int NoOfItemsPicked;

        void Start()
        {
            TotalItems = items.Count;
        }

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

        public Item GetItem(int index)
        {
            return items[index];
        }

        public void SelectItem(int index)
        {
            items[index] = null;
            NoOfItemsPicked++;

            if (NoOfItemsPicked >= TotalItems)
            {
                items.Clear();
                this.gameObject.SetActive(false);
            }

        }

        public bool HasItems()
        {
            return NoOfItemsPicked < TotalItems;
        }

        public void OnPicked()
        {
        }

        public void OnPicked(Transform Picker)
        {
        }

        public void TryToPick(InventoryController _picker)
        {
            _picker.AssignLootBox(this);
        }
    }
}