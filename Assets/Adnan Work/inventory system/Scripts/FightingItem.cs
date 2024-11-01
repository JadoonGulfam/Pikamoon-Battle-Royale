using UnityEngine;

[CreateAssetMenu(fileName = "New Fighting Item", menuName = "Inventory/Items/Fighting")]
public class FightingItem : Item
{
    public int attackPower;

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
