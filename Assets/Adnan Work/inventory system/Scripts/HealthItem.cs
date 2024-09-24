using UnityEngine;

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
