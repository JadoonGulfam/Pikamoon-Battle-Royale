using UnityEngine;

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
