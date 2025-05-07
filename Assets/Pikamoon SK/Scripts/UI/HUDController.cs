using TMPro;
using UnityEngine;

namespace Pikamoon.UI
{

    public class HUDController : MonoBehaviour
    {


        #region Weapons
        [Header("Weapons")]
        public UI_ItemSlot[] Weapons;
        int selectedIndex;



        public void EquipWeapon(int index, Sprite icon, bool isActive, int resourceRemaining, int totalResource)
        {
            Weapons[index].AssignItem(icon, isActive, resourceRemaining,totalResource);

            if(isActive)
                selectedIndex = index;
        }

        public void UnEquipWeapon(int index)
        {
            Weapons[index].UnAssignItem();
        }

        public void DropWeapon(int index)
        {
            Weapons[index].RemoveItem();
        }

        void ChangeWeapon(int index)
        {

        }

        public void SwitchToWeapon_1()
        {

        }
        public void SwitchToWeapon_2()
        {

        }

        #endregion

        #region Pick

        [Header("Pick")]
        public GameObject PickNotifier;
        public TextMeshProUGUI PickKeyText;
        bool isShowing;

        public void ShowPickUp()
        {
            if (isShowing)
                return;

            isShowing = true;

            PickNotifier.gameObject.SetActive(true);
        }

        public void HidePickUp()
        {
            if (!isShowing)
                return;

            isShowing = false;

            PickNotifier.gameObject.SetActive(false);

        }

        #endregion

        
    }

}