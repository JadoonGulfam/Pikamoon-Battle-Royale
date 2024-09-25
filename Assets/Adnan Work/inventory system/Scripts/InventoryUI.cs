using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

/// <summary>
/// Manages the UI for the inventory system, allowing for dynamic display of collected items.
/// </summary>
public class InventoryUI : MonoBehaviour
{
    [Header("UI References")]
    [Tooltip("Prefab representing an inventory slot with icon and item name.")]
    public GameObject inventorySlotPrefab;

    [Tooltip("Panel where all the inventory items will be displayed.")]
    public Transform inventoryPanel;

    private Dictionary<string, GameObject> uiSlots = new Dictionary<string, GameObject>();

    /// <summary>
    /// Adds an item to the UI inventory panel.
    /// </summary>
    /// <param name="item">The item to add to the UI.</param>
    public void AddItemToUI(Item item)
    {
        // Check if the item already exists in the UI
        if (uiSlots.ContainsKey(item.itemName))
        {
            Debug.LogWarning($"{item.itemName} is already present in the UI.");
            return;
        }

        // Instantiate a new inventory slot UI
        GameObject newSlot = Instantiate(inventorySlotPrefab, inventoryPanel);

        // Set item icon and name
        Image icon = newSlot.transform.Find("Icon").GetComponent<Image>();
        Text itemName = newSlot.transform.Find("ItemName").GetComponent<Text>();

        icon.sprite = item.icon;
        itemName.text = item.itemName;

        // Store reference to the UI slot for later management
        uiSlots[item.itemName] = newSlot;
    }

    /// <summary>
    /// Removes an item from the UI inventory panel.
    /// </summary>
    /// <param name="itemName">Name of the item to remove from the UI.</param>
    public void RemoveItemFromUI(string itemName)
    {
        if (uiSlots.ContainsKey(itemName))
        {
            Destroy(uiSlots[itemName]);  // Remove the UI element
            uiSlots.Remove(itemName);    // Remove from dictionary
        }
        else
        {
            Debug.LogWarning($"Item not found in UI: {itemName}");
        }
    }

    /// <summary>
    /// Clears the entire inventory UI panel.
    /// Useful when loading a new game or resetting the inventory.
    /// </summary>
    public void ClearInventoryUI()
    {
        foreach (var slot in uiSlots.Values)
        {
            Destroy(slot); // Destroy all UI slots
        }
        uiSlots.Clear();   // Clear the dictionary
    }
}
