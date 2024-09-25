using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public ItemDatabase itemDatabase;
    private List<Item> collectedItems = new List<Item>();
    private void Start()
    {
        CollectItem("1");
        UseItem("1");
    }
    public void CollectItem(string itemName)
    {
        Item item = itemDatabase.GetItem(itemName);
        if (item != null)
        {
            collectedItems.Add(item);
            Debug.Log("Collected: " + item.itemName);
        }
        else
        {
            Debug.Log("Item not found in database.");
        }
    }

    public void UseItem(string itemName)
    {
        Item item = collectedItems.Find(i => i.itemName == itemName);
        if (item != null)
        {
            item.Use();
        }
        else
        {
            Debug.Log("Item not found in inventory.");
        }
    }

    public void SaveInventory()
    {
        InventoryData data = new InventoryData(collectedItems);
        string json = JsonUtility.ToJson(data, true);
        System.IO.File.WriteAllText(GetSavePath(), json);
        Debug.Log("Inventory saved.");
    }

    public void LoadInventory()
    {
        string path = GetSavePath();
        if (System.IO.File.Exists(path))
        {
            string json = System.IO.File.ReadAllText(path);
            InventoryData data = JsonUtility.FromJson<InventoryData>(json);

            // Clear the current inventory before loading
            collectedItems.Clear();

            // Load the items based on their names and retrieve them from the ItemDatabase
            foreach (var itemName in data.items)
            {
                Item item = itemDatabase.GetItem(itemName);
                if (item != null)
                {
                    collectedItems.Add(item);
                }
                else
                {
                    Debug.LogWarning("Item not found in database: " + itemName);
                }
            }
            Debug.Log("Inventory loaded.");
        }
    }

    private string GetSavePath()
    {
        return Application.persistentDataPath + "/inventory.json";
    }
}
