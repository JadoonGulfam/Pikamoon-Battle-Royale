using UnityEngine;
using UnityEngine.UI;
namespace Pikamoon.UI
{
    public class GamePlayPopupUI : MonoBehaviour
    {
        [Header("Exit Menu References")]
        [SerializeField] private GameObject exitMenu;
        [SerializeField] private Button yesBtn;
        [SerializeField] private Button noBtn;

        private void Awake()
        {
            yesBtn.onClick.AddListener(ToggleOnYes);
            noBtn.onClick.AddListener(ToggleOnNo);
        }
        public void ToggleExitMenu() 
        {
            if (exitMenu.activeInHierarchy)
            {
                ToggleOnNo();
            }
            else
            {
                exitMenu.SetActive(true);
                ToggleCursor(true);
            }
        }
        void ToggleOnYes() 
        {
           // LoadingManager.Instance.StartCoroutine("Circle_Loading");
            NetworkManager.Instance.OnGameLeave();

        }
        void ToggleOnNo() 
        { 
            exitMenu.SetActive(false);
            ToggleCursor(false);
        }
        void ToggleCursor(bool flag)
        {
            Cursor.visible = flag;
            Cursor.lockState = !flag ? CursorLockMode.None : CursorLockMode.Confined;
        }
    }
}
