using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Handles the player's inventory system, including collecting, removing, and saving items.
/// </summary>
public class Inventory : MonoBehaviour
{
    [Header("References")]
    [Tooltip("The centralized item database containing all possible items.")]
    public ItemDatabase itemDatabase;

    [Tooltip("Reference to the UI script to update the inventory panel.")]
    public InventoryUI inventoryUI;

    // List to store all collected items
    private List<Item> collectedItems = new List<Item>();

    private void Start()
    {
        LoadInventory();
       // StartCoroutine(addItem());
    }
    int count = 5;
    IEnumerator addItem()
    {
        count--;
        yield return new WaitForSeconds(1);
        CollectItem("1");
        SaveInventory();
        if (count != 0)
            StartCoroutine(addItem());
    }
    /// <summary>
    /// Collects an item and adds it to the inventory.
    /// Also updates the inventory UI panel to display the collected item.
    /// </summary>
    /// <param name="itemName">The name of the item to collect.</param>
    public void CollectItem(string itemName)
    {
        // Retrieve item from the database
        Item item = itemDatabase.GetItem(itemName);
        if (item != null)
        {
            collectedItems.Add(item);
            Debug.Log($"Collected: {item.itemName}");

            // Update UI
            inventoryUI.AddItemToUI(item);
        }
        else
        {
            Debug.LogWarning($"Item not found in database: {itemName}");
        }
    }

    /// <summary>
    /// Removes an item from the inventory and the UI.
    /// </summary>
    /// <param name="itemName">The name of the item to remove.</param>
    public void RemoveItem(string itemName)
    {
        Item item = collectedItems.Find(i => i.itemName == itemName);
        if (item != null)
        {
            collectedItems.Remove(item);
            Debug.Log($"Removed: {item.itemName}");

            // Update UI
            inventoryUI.RemoveItemFromUI(itemName);
        }
        else
        {
            Debug.LogWarning($"Item not found in inventory: {itemName}");
        }
    }

    /// <summary>
    /// Saves the player's inventory to a JSON file.
    /// </summary>
    public void SaveInventory()
    {
        InventoryData data = new InventoryData(collectedItems);
        string json = JsonUtility.ToJson(data, true);
        System.IO.File.WriteAllText(GetSavePath(), json);
        Debug.Log("Inventory saved.");
    }

    /// <summary>
    /// Loads the player's inventory from a saved JSON file.
    /// Also updates the UI to reflect the loaded items.
    /// </summary>
    public void LoadInventory()
    {
        string path = GetSavePath();
        if (System.IO.File.Exists(path))
        {
            string json = System.IO.File.ReadAllText(path);
            InventoryData data = JsonUtility.FromJson<InventoryData>(json);

            // Clear current inventory and UI
            collectedItems.Clear();
            inventoryUI.ClearInventoryUI();

            // Load items from save
            foreach (var itemName in data.items)
            {
                Item item = itemDatabase.GetItem(itemName);
                if (item != null)
                {
                    collectedItems.Add(item);
                    inventoryUI.AddItemToUI(item); // Update UI for loaded items
                }
                else
                {
                    Debug.LogWarning($"Item not found in database: {itemName}");
                }
            }
            Debug.Log("Inventory loaded.");
        }
    }

    /// <summary>
    /// Retrieves the path to the save file where the inventory data is stored.
    /// </summary>
    /// <returns>A string representing the save file path.</returns>
    private string GetSavePath()
    {
        return Application.persistentDataPath + "/inventory.json";
    }
}
