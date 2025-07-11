using TMPro;
using UnityEngine;
using UnityEngine.UI;
namespace Pikamoon.UI
{

    public class HUDController : MonoBehaviour
    {
        #region Health System
        [Header("Health System")]
        public Image HeadShieldFiller;
        public Image UpperShieldFiller;
        public Image LowerShieldFiller;
        [Space]
        public Image HealthFiller;
        public TextMeshProUGUI HealthText;


        public void UpdateHeadShield(float val, float maxVal)
        {
            HeadShieldFiller.fillAmount = val / maxVal;
        }

        public void UpdateUpperShield(float val, float maxVal)
        {
            UpperShieldFiller.fillAmount = val / maxVal;
        }
        public void UpdateLowerShield(float val, float maxVal)
        {
            LowerShieldFiller.fillAmount = val / maxVal;
        }

        public void UpdateHealth(float val, float maxVal)
        {
            HealthFiller.fillAmount = val / maxVal;
            HealthText.text = val + string.Empty;   
        }


        #endregion

        #region Weapons
        [Space]
        [Header("Weapons")]
        [Space]
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
            Weapons[index].UnAssignItem(true);
        }

        public void DropWeapon(int index)
        {
            Weapons[index].RemoveItem(true);
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

        [Space]
        [Header("Pick")]
        [Space]
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