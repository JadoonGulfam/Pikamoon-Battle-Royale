using UnityEngine;

namespace Pikamoon.Controller
{
    public enum EquipType
    {
        none,
        InBag,
        Equiped,
    }

    public class Item : MonoBehaviour
    {
        public ItemType itemType;
        public EquipType isEquiped = 0;
    }

}