using UnityEngine;

[CreateAssetMenu(fileName = "ItemDatabase", menuName = "Inventory/ItemDatabase")]
public class ItemDatabase : ScriptableObject
{
    public Item[] allItems;

    public Item GetItem(string itemName)
    {
        foreach (var item in allItems)
        {
            if (item.itemName == itemName)
            {
                return item;
            }
        }
        return null;
    }
}
