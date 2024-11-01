using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Manages the player's inventory, handles item collection and removal,
/// and stores session data for better performance.
/// </summary>
public class Inventory : MonoBehaviour
{
    [Header("References")]
    [Tooltip("The centralized item database containing all possible items.")]
    public ItemDatabase itemDatabase;

    [Tooltip("Reference to the UI script to update the inventory panel.")]
    public InventoryUI inventoryUI;

    // List to store all collected items in the current session.
    private List<Item> collectedItems = new List<Item>();

    // In-memory storage to improve performance for frequent accesses during gameplay.
    private Dictionary<string, Item> itemCache = new Dictionary<string, Item>();


    private void Start()
    {
        // ClearAllItems();
         LoadInventory();
         StartCoroutine(addItem());
        // CollectItem("1");
       // ClearSavedInventoryData();


    }
    int i = 5;
    IEnumerator addItem()
    {
        yield return new WaitForSeconds(1);
        CollectItem("1");
        i--;
        if (i > 0)
            StartCoroutine(addItem());
        
    }

    public void CollectItem(string itemName)
    {
        Item item = itemDatabase.GetItem(itemName);
        if (itemCache.ContainsKey(itemName))
        {
            inventoryUI.IncrementItemUI(item);
            //Debug.Log($"Item already collected: {itemName}");
            return;
        }

        
        if (item != null)
        {
            collectedItems.Add(item);
            itemCache[itemName] = item;
            Debug.Log($"Collected: {item.itemName}");

            // Update UI
            inventoryUI.AddItemToUI(item);
        }
        else
        {
            Debug.LogWarning($"Item not found in database: {itemName}");
        }
    }


    public void RemoveItem(string itemName)
    {
        if (itemCache.ContainsKey(itemName))
        {
            Item item = itemCache[itemName];
            collectedItems.Remove(item);
            itemCache.Remove(itemName);
            Debug.Log($"Removed: {item.itemName}");

            // Update UI
            inventoryUI.RemoveItemFromUI(itemName);
        }
        else
        {
            Debug.LogWarning($"Item not found in inventory: {itemName}");
        }
    }

    public void UseItem(string itemName)
    {
        Item item = collectedItems.Find(i => i.itemName == itemName);
        if (item != null)
        {
            item.Use(); // Call the Use method of the item
          //  RemoveItem(itemName);
            Debug.Log($"Used: {item.itemName}");
        }
        else
        {
            Debug.LogWarning($"Item not found in inventory: {itemName}");
        }
    }
    public void ClearAllItems()
    {
        // Clear the session inventory
        collectedItems.Clear();
        itemCache.Clear();

        // Update the UI to reflect the cleared inventory
        inventoryUI.ClearInventoryUI();

        Debug.Log("All items removed from the inventory and UI.");
    }
    public void SaveInventory()
    {
        InventoryData data = new InventoryData(collectedItems);
        string json = JsonUtility.ToJson(data, true);
        System.IO.File.WriteAllText(GetSavePath(), json);
        Debug.Log("Inventory saved with quantities and categories.");
    }

    public void LoadInventory()
    {
        string path = GetSavePath();
        if (System.IO.File.Exists(path))
        {
            string json = System.IO.File.ReadAllText(path);
            InventoryData data = JsonUtility.FromJson<InventoryData>(json);

            // Clear current session data and UI
            collectedItems.Clear();
            itemCache.Clear();
            inventoryUI.ClearInventoryUI();

            // Load items with quantities and categories
            foreach (var itemData in data.items)
            {
                Item item = itemDatabase.GetItem(itemData.itemName);
                if (item != null)
                {
                    item.quantity = itemData.quantity;
                    item.category = itemData.category;

                    collectedItems.Add(item);
                    itemCache[item.itemName] = item;
                    inventoryUI.AddItemToUI(item);
                }
                else
                {
                    Debug.LogWarning($"Item not found in database: {itemData.itemName}");
                }
            }
            Debug.Log("Inventory loaded with quantities and categories.");
        }
    }

    public void ClearSavedInventoryData()
    {
        string path = GetSavePath();
        if (System.IO.File.Exists(path))
        {
            System.IO.File.Delete(path);
            Debug.Log("Inventory data cleared.");
        }
        else
        {
            Debug.LogWarning("No saved inventory data found to clear.");
        }
    }
   
    private string GetSavePath()
    {
        return Application.persistentDataPath + "/inventory1.json";
    }

    private void OnApplicationQuit()
    {
        SaveInventory();
    }
}
