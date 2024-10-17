using UnityEngine;

public interface IUsableItem
{
    void Use();
}

[System.Serializable]
public abstract class Item : ScriptableObject, IUsableItem
{
    public string itemName;
    public Sprite icon;
    public ItemCategory category;
    public string description;
    public int quantity;
    public abstract void Use();
}
