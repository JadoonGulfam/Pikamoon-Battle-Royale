using System.Collections.Generic;

[System.Serializable]
public class InventoryData
{
    public string[] items;

    public InventoryData(List<Item> collectedItems)
    {
        items = new string[collectedItems.Count];
        for (int i = 0; i < collectedItems.Count; i++)
        {
            items[i] = collectedItems[i].itemName;
        }
    }
}

public enum ItemCategory
{
    Health,
    Fighting,
    Defense,
    Miscellaneous
}
