using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using TMPro;
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
    public Inventory inventory;
    private Dictionary<string, GameObject> uiSlots = new Dictionary<string, GameObject>();

    public void AddItemToUI(Item item)
    {
        // Check if the item already exists in the UI

        if (uiSlots.ContainsKey(item.itemName))
        {
            IncrementItemUI(item);
        }

        // Create a new slot if the item doesn't already exist in the UI
        GameObject slot = Instantiate(inventorySlotPrefab, inventoryPanel);

        slot.GetComponent<Image>().sprite = item.icon;

        slot.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = item.itemName;

        TextMeshProUGUI quantityTextComponent = slot.transform.GetChild(1).GetComponent<TextMeshProUGUI>();
        if(item.quantity <= 1)
             item.quantity = 1;
        quantityTextComponent.text = item.quantity.ToString();

        // Add the new slot to the uiSlots dictionary
        uiSlots[item.itemName] = slot;

        // Add button functionality for item usage
        Button button = slot.GetComponent<Button>();
        button.onClick.AddListener(() => UseItem(item));

        Debug.Log($"{item.itemName} added to the UI with quantity: {item.quantity}.");
    }
    public void IncrementItemUI(Item item)
    {
        // Update the quantity for the existing item
        GameObject existingSlot = uiSlots[item.itemName];

        // Assuming there's a TextMeshProUGUI component in the slot's child that displays the quantity
        TextMeshProUGUI quantityText = existingSlot.transform.GetChild(1).GetComponent<TextMeshProUGUI>();

        // Update the item quantity

        item.quantity++;
        quantityText.text = item.quantity.ToString();

        Debug.Log($"{item.itemName} quantity updated to {item.quantity} in the UI.");
        return;

    }
    public void DecrementItemUI(Item item)
    {
        // Update the quantity for the existing item
        GameObject existingSlot = uiSlots[item.itemName];

        // Assuming there's a TextMeshProUGUI component in the slot's child that displays the quantity
        TextMeshProUGUI quantityText = existingSlot.transform.GetChild(1).GetComponent<TextMeshProUGUI>();

        // Update the item quantity

        
        quantityText.text = item.quantity.ToString();

        Debug.Log($"{item.itemName} quantity updated to {item.quantity} in the UI.");
        return;

    }



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

    private void UseItem(Item item)
    {
        
        inventory.UseItem(item.itemName);  // Calls the UseItem method in Inventory
        item.quantity--;
        print("remaining items "+item.quantity);
        if (item.quantity > 0)
            DecrementItemUI(item);
        else
            inventory.RemoveItem(item.itemName);
        
    }
    public void ClearInventoryUI()
    {
        foreach (var slot in uiSlots.Values)
        {
            Destroy(slot); // Destroy all UI slots
        }
        uiSlots.Clear();   // Clear the dictionary
    }
}
