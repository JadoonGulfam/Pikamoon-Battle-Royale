using System.Collections.Generic;

/// <summary>
/// Stores the data for items in the inventory, including item names, quantities, and categories.
/// </summary>
[System.Serializable]
public class InventoryData
{
    // Array to store item data (name, quantity, category) for each collected item
    public ItemData[] items;

    // Constructor that converts a list of collected items into serializable data
    public InventoryData(List<Item> collectedItems)
    {
        items = new ItemData[collectedItems.Count];
        for (int i = 0; i < collectedItems.Count; i++)
        {
            Item currentItem = collectedItems[i];
            items[i] = new ItemData(currentItem.itemName, currentItem.quantity, currentItem.category);
        }
    }
}

/// <summary>
/// Stores individual item information including name, quantity, and category.
/// </summary>
[System.Serializable]
public class ItemData
{
    public string itemName;         // ItemName of the item
    public int quantity;            // Quantity of the item
    public ItemCategory category;   // Category of the item (e.g., Health, Fighting)

    // Constructor to initialize item data
    public ItemData(string name, int qty, ItemCategory cat)
    {
        itemName = name;
        quantity = qty;
        category = cat;
    }
}

/// <summary>
/// Enum to categorize items into predefined categories.
/// </summary>
public enum ItemCategory
{
    Health,        // Health-related items like potions
    Fighting,      // Offensive items or weapons
    Defense,       // Defensive items like shields
    Miscellaneous  // Other items that don't fit into the main categories
}
