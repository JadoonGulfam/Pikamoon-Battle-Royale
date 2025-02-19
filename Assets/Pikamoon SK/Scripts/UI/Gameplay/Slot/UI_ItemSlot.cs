using TMPro;
using UnityEngine;
using UnityEngine.UI;


namespace Pikamoon.UI
{

    public abstract class UI_ItemSlot : MonoBehaviour
    {
        public Image BtnBg;
        public Image Icon;
        public TextMeshProUGUI KeyCode;
        public Image HighlighterImg;
        public bool hasItem;

        public virtual void AssignItem()
        {

        }
        public abstract void AssignItem(Sprite icon);
        public abstract void AssignItem(Sprite icon,bool isActive, int health = 100, int _fullHealth = 100);

        public abstract void UnAssignItem();


        public abstract void RemoveItem();


        public abstract void Change();
        public abstract void Select();
        public abstract void UnSelect();
        
    }

}