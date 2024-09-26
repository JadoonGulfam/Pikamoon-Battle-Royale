using UnityEngine;

[CreateAssetMenu(fileName = "New Health Item", menuName = "Inventory/Items/Health")]
public class HealthItem : Item
{
    public int restoreAmount;

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
