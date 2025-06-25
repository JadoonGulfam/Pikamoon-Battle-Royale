using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Pikamoon.UI
{

    public class UI_QuickItemSlot : UI_ItemSlot
    {
        [Serializable]
        public struct SlotAppearenceSettings
        {
            public Color BgIconColor;
            [Space]
            public Color HighlighterColor;
            [Space]
            public Color IconColor;
            public bool IconActiveFlag;
            //[Space]
            //public bool FullHealthTextActiveFlag;
            //public bool HealthTextActiveFlag;
            //[Space]
            //public bool healthFillerBGActiveFlag;
            //public bool healthFillerActiveFlag;
        }


        [Space]
        //public TextMeshProUGUI FullHealthVal;
        //public TextMeshProUGUI HealthVal;
        //[SerializeField]
        //public Image healthFillerBG;
        //public Image healthFiller;

        Controller.Item CurrentItem;


        [Header("Settings")]
        public SlotAppearenceSettings EmptySlotSettings;
        public SlotAppearenceSettings ActiveSlotSettings;
        public SlotAppearenceSettings InActiveSlotSettings;

        private void Start()
        {
            ChangeButtonAppearence(EmptySlotSettings);
        }

        public override void AssignItem()
        {
        }

        public override void AssignItem(Controller.Item item)
        {
            Icon.sprite = item.Data.icon;
            ItemName.text = item.Data.ItemName;
            hasItem = true;

            CurrentItem = item;

            ChangeButtonAppearence(ActiveSlotSettings);
        }



        public override void AssignItem(Sprite _icon,bool isActive, int _fullHealth = 100, int _health = 100)
        {
            Icon.sprite = _icon;

            //FullHealthVal.text = "/ " + _fullHealth + string.Empty;
            //HealthVal.text = _health + string.Empty;

            //healthFiller.fillAmount = ((float)_health / (float)_fullHealth);

            ChangeButtonAppearence(isActive ? ActiveSlotSettings : InActiveSlotSettings);
        }

        public override void UnAssignItem()
        {
            CurrentItem = null;
            ChangeButtonAppearence(InActiveSlotSettings);
        }


        public override void Change()
        {

        }

        public override void RemoveItem()
        {
            Icon.sprite = null;

            CurrentItem = null;
            ChangeButtonAppearence(EmptySlotSettings);
        }


        public void ChangeButtonAppearence(SlotAppearenceSettings settings)
        {
            BtnBg.color = settings.BgIconColor;

            //HighlighterImg.color = settings.HighlighterColor;

            Icon.enabled = settings.IconActiveFlag;
            Icon.color = settings.IconColor;

            //FullHealthVal.enabled = settings.FullHealthTextActiveFlag;
            //HealthVal.enabled = settings.FullHealthTextActiveFlag;

            //healthFillerBG.enabled = settings.healthFillerBGActiveFlag;
            //healthFiller.enabled = settings.healthFillerActiveFlag;

        }

        public override void Select()
        {

        }

        public override void UnSelect()
        {
        }
        public override void OnPointerDown(PointerEventData eventData)
        {
            if (hasItem)
            {
                DragManager.Instance.StartDrag(this, GetItem());
            }
        }

        public override void OnPointerUp(PointerEventData eventData)
        {
            if (DragManager.Instance.IsDragging)
            {
                SwapItems(this);
            }
        }

        public override void OnPointerEnter(PointerEventData eventData)
        {
            if (DragManager.Instance.IsDragging)
            {
                HighlighterImg.enabled = true;

                if (CanAcceptItem(DragManager.Instance.draggedItem))
                {
                    HighlighterImg.color = Color.green;
                }
                else
                {
                    HighlighterImg.color = Color.red;
                }
            }
        }


        public override void OnPointerExit(PointerEventData eventData)
        {
            HighlighterImg.enabled = false;
        }

        public override Controller.Item GetItem()
        {
            return hasItem ? CurrentItem : null; 
        }

        public override bool CanAcceptItem(Controller.Item item)
        {
            return item != null;
        }
    }

}