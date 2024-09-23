using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class InventoryData
{
    public List<string> itemIDs = new List<string>();

    // Constructor to convert the inventory items to item IDs
    public InventoryData(List<Item> items)
    {
        foreach (Item item in items)
        {
            itemIDs.Add(item.name);  // Use the name as the unique identifier
        }
    }

    // Convert the item IDs back to actual items
    public List<Item> ToItemList()
    {
        List<Item> loadedItems = new List<Item>();

        foreach (string id in itemIDs)
        {
            Item item = Resources.Load<Item>("Items/" + id);  // Load item from Resources folder
            if (item != null)
            {
                loadedItems.Add(item);
            }
        }

        return loadedItems;
    }
}
