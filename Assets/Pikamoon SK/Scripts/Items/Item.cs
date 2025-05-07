using System;
using UnityEngine;


public enum InventoryPlacementType
{
    None,
    LootBox,
    AllItems,
    QuickSlot,
    DedicatedSlot
}


namespace Pikamoon.Controller
{

    public enum EquipType
    {
        none,
        InBag,
        Equiped,
    }

    [SerializeField]
    public enum ItemType
    {
        Weapon,
        Arrow,
        Health,
        Shield_Head,
        Shield_UpperBody,
        Shield_LowerBody
    }
    //[Serializable]
    //public struct ItemInfo
    //{
    //    public GameObject GO;
    //    public ItemDataSO Data;
    //    public int Quantity;
    //}

    public class Item : MonoBehaviour
    {
        public ItemDataSO Data;
        public EquipType isEquiped = 0;
        public int Quantity;

        public T GetItemDataAs<T>() where T : ItemDataSO
        {
            return Data as T; // Tries to cast the currentWeapon to the specified type
        }

    }

}