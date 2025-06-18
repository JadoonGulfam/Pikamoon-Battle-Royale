using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class SlotUI : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerEnterHandler, IPointerExitHandler
{
    public Image itemIcon;
    public Sprite itemSprite;

    public DragHandler dragHandler;

    private void Start()
    {
        itemIcon = GetComponent<Image>();
        itemSprite = itemIcon.sprite;
        dragHandler = FindFirstObjectByType<DragHandler>();
        UpdateIcon();
    }

    private void SwapItems(SlotUI other)
    {
        Sprite temp = other.itemSprite;
        other.itemSprite = itemSprite;
        itemSprite = temp;

        other.UpdateIcon();
        UpdateIcon();
    }

    public void UpdateIcon()
    {
        if (itemIcon != null)
        {
            itemIcon.sprite = itemSprite;
            itemIcon.enabled = (itemSprite != null);
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (itemSprite != null)
        {
            dragHandler.GrabItem(this, itemSprite);
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        SlotUI targetSlot = dragHandler.GetSlotUnderPointer(eventData);
        if (targetSlot != null && targetSlot != this)
        {
            SwapItems(targetSlot);
        }
        dragHandler.ReleaseItem();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
    }

    public void OnPointerExit(PointerEventData eventData)
    {
    }
}
