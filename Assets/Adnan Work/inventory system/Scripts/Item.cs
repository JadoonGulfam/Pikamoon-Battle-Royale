using UnityEngine;

public interface IUsableItem
{
    void Use();
}
public enum ItemCategory
{
    Health,
    Fighting,
    Defense
}

[System.Serializable]
public abstract class Item : ScriptableObject, IUsableItem
{
    public string itemName;
    public Sprite icon;
    public ItemCategory category;
    public string description;

    public abstract void Use();  // Enforce the implementation of Use in derived classes
}
[CreateAssetMenu(fileName = "New Health Item", menuName = "Inventory/Items/Health")]
public class HealthItem : Item
{
    public int restoreAmount;

    public override void Use()
    {
        Debug.Log("Using Health Item: " + itemName);
        // Logic to restore player health
    }
}

[CreateAssetMenu(fileName = "New Fighting Item", menuName = "Inventory/Items/Fighting")]
public class FightingItem : Item
{
    public int attackPower;

    public override void Use()
    {
        Debug.Log("Using Fighting Item: " + itemName);
        // Logic to increase player attack power
    }
}

[CreateAssetMenu(fileName = "New Defense Item", menuName = "Inventory/Items/Defense")]
public class DefenseItem : Item
{
    public int defensePoints;

    public override void Use()
    {
        Debug.Log("Using Defense Item: " + itemName);
        // Logic to increase player defense
    }
}
