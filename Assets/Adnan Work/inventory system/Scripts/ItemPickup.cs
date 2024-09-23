using UnityEngine;

public class ItemPickup : MonoBehaviour
{
    public Item item;

    public void Interact()
    {
        Inventory inventory = FindObjectOfType<Inventory>();
        if (inventory != null && inventory.AddItem(item))
        {
            inventory.SaveInventory();  // Save after item is picked up
            Destroy(gameObject);  // Remove item from world
        }
        else
        {
            Debug.LogError("Inventory not found!");
        }
    }
}
