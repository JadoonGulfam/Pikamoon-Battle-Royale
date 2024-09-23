using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    public Image icon;
    private Item currentItem;

    public void AddItem(Item newItem)
    {
        currentItem = newItem;
        icon.sprite = newItem.icon;
        icon.enabled = true;
    }

    public void ClearSlot()
    {
        currentItem = null;
        icon.sprite = null;
        icon.enabled = false;
    }

    public void UseItem()
    {
        if (currentItem != null)
        {
            currentItem.Use();
        }
    }
}
