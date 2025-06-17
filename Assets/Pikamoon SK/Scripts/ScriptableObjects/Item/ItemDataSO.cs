using Pikamoon.Controller;
using UnityEngine;

namespace Pikamoon.Controller
{

    public class ItemDataSO : ScriptableObject
    {
        public string ItemName;
        public string LevelNo;
        public ItemType itemType;
        public Sprite icon;
        [Space]
        public int perSlotLimit;
    }

}