using UnityEngine;
using System.Collections.Generic;
[System.Serializable]
public class Inventory : MonoBehaviour
{
    private const string saveFileName = "player_inventory.json";

    public List<Item> items = new List<Item>();
    public int maxInventorySize = 20;

    // Delegate to notify UI and other systems when inventory changes
    public delegate void OnInventoryChanged();
    public event OnInventoryChanged InventoryChanged;

    // Add item to the inventory
    public bool AddItem(Item item)
    {
        if (items.Count >= maxInventorySize)
        {
            Debug.Log("Inventory is full!");
            return false;
        }

        items.Add(item);
        InventoryChanged?.Invoke();
        return true;
    }

    // Remove item from inventory
    public void RemoveItem(Item item)
    {
        if (items.Contains(item))
        {
            items.Remove(item);
            InventoryChanged?.Invoke();
        }
    }

    // Use an item from inventory
    public void UseItem(Item item)
    {
        item.Use();
        RemoveItem(item);
    }

    // Save inventory to a JSON file
    public void SaveInventory()
    {
        InventoryData data = new InventoryData(items);
        string json = JsonUtility.ToJson(data, true);
        System.IO.File.WriteAllText(GetSavePath(), json);
        Debug.Log("Inventory saved.");
    }

    // Load inventory from a JSON file
    public void LoadInventory()
    {
        if (System.IO.File.Exists(GetSavePath()))
        {
            string json = System.IO.File.ReadAllText(GetSavePath());
            InventoryData data = JsonUtility.FromJson<InventoryData>(json);
            items = data.ToItemList();
            InventoryChanged?.Invoke();
            Debug.Log("Inventory loaded.");
        }
        else
        {
            Debug.Log("No save file found.");
        }
    }

    private string GetSavePath()
    {
        return System.IO.Path.Combine(Application.persistentDataPath, saveFileName);
    }
}
