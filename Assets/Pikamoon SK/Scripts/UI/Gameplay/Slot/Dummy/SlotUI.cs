using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class SlotUI : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
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

    public void OnPointerDown(PointerEventData eventData)
    {
        if (itemSprite != null)
        {
            Debug.Log("Clicked On " + this.name);
            dragHandler.GrabItem(this, itemSprite);
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        SlotUI targetSlot = dragHandler.GetSlotUnderPointer(eventData);
        if (targetSlot != null && targetSlot != this)
        {
            Debug.Log("Released On " + this.name);
            SwapItems(targetSlot);
        }
        dragHandler.ReleaseItem();
    }

    private void SwapItems(SlotUI other)
    {
        Sprite temp = other.itemSprite;
        other.itemSprite = this.itemSprite;
        this.itemSprite = temp;

        other.UpdateIcon();
        this.UpdateIcon();
    }

    public void UpdateIcon()
    {
        if (itemIcon != null)
        {
            itemIcon.sprite = itemSprite;
            itemIcon.enabled = (itemSprite != null);
        }
    }
}
