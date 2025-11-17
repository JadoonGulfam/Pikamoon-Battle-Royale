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
        Shield
    }


    //[Serializable]
    //public struct ItemInfo
    //{
    //    public GameObject GO;
    //    public ItemDataSO Data;
    //    public int Quantity;
    //}

    public class Item : MonoBehaviour, IPickable, IDroppable
    {
        public ItemDataSO Data;
        public int SubType;
        public EquipType isEquiped = 0;
        public bool hasSpecializeSlot;
        public float Quantity;


        public T GetItemDataAs<T>() where T : ItemDataSO
        {
            return Data as T; // Tries to cast the currentWeapon to the specified type
        }

        public T GetItemAs<T>() where T : Item
        {
            return this as T; // Tries to cast the currentWeapon to the specified type
        }

        public virtual void OnPicked()
        {
        }

        public virtual void OnPicked(Transform Picker)
        {
        }

        public virtual void TryToPick(InventoryController Picker)
        {
        }

        public virtual void OnDrop()
        {
        }

        public virtual void OnDrop(Transform Dropper, LayerMask DropLayer)
        {
        }
    }

}