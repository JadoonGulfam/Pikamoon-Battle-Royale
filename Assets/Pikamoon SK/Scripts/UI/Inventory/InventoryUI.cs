using Pikamoon.Controller;
using UnityEngine;
using UnityEngine.UI;

namespace Pikamoon.UI
{
    [System.Serializable]
    public struct UI_ItemCategory
    {
        public string CategoryName;
        public UI_ItemSlot[] Items;
        public int MaxInCategory;
        public int AvailedInCategory;
    }

    public class InventoryUI : MonoBehaviour
    {
        public Canvas _canvas;

        public InventoryController _inventory;

        [SerializeField] private Image CharacterImg;

        [Space]
        public UI_ItemCategory Weapons;

        [Space]
        public UI_ItemCategory Shields;

        [Space]
        public UI_ItemCategory QuickItems;

        [Space]
        public UI_ItemCategory AllItems;





        //[Header("Weapons")]


        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }
    }

}