using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    public Inventory inventory;
    public Transform itemsParent;
    public InventorySlot[] slots;

    private void Start()
    {
        inventory.InventoryChanged += UpdateUI;
        inventory.LoadInventory();  // Load saved inventory on start
    }

    private void OnDestroy()
    {
        inventory.InventoryChanged -= UpdateUI;
    }

    // Update the inventory UI
    private void UpdateUI()
    {
        for (int i = 0; i < slots.Length; i++)
        {
            if (i < inventory.items.Count)
            {
                slots[i].AddItem(inventory.items[i]);
            }
            else
            {
                slots[i].ClearSlot();
            }
        }
    }
}
