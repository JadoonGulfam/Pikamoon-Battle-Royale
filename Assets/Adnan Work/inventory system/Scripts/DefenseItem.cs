using UnityEngine;

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
