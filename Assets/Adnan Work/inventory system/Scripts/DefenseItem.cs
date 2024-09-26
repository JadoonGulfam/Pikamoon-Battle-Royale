using UnityEngine;

[CreateAssetMenu(fileName = "New Defense Item", menuName = "Inventory/Items/Defense")]
public class DefenseItem : Item
{
    public int defensePoints;

    public override void Use()
    {
        // Perform item-specific logic
        if (quantity > 0)
        {
            Debug.Log($"{itemName} used. Remaining quantity: {quantity}");
        }
        else
        {
            Debug.LogWarning($"{itemName} has no quantity left to use.");
        }
    }
}
