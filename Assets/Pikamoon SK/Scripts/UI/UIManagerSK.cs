using Pikamoon.Controller;
using UnityEngine;
namespace Pikamoon.UI
{
    public class UIManagerSK : MonoBehaviour
    {
        public HUDController hudcontroller;
        public LootBox_UI lootBoxUI;
        public InventoryUI inventoryUI;
        public GamePlayPopupUI exitController;

        void Awake()
        {
            hudcontroller.InitializeWeaponUI(inventoryUI);
        }

        public void OpenLootBox(LootBox lootbox)
        {

        }
        public void CloseLootBox()
        {

        }

    }

}