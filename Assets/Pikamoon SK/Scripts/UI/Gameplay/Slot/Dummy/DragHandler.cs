using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class DragHandler : MonoBehaviour
{
    public Image dragIconPrefab;

    private Image dragIcon;
    private Canvas canvas;
    private SlotUI originSlot;
    private bool isDragging = false;

    void Awake()
    {
        canvas = GetComponentInParent<Canvas>();
        dragIcon = Instantiate(dragIconPrefab, canvas.transform);
        dragIcon.raycastTarget = false;
        dragIcon.gameObject.SetActive(false);
    }

    public void GrabItem(SlotUI origin, Sprite sprite)
    {
        originSlot = origin;
        isDragging = true;
        dragIcon.sprite = sprite;
        dragIcon.gameObject.SetActive(true);
    }

    public void ReleaseItem()
    {
        isDragging = false;
        dragIcon.gameObject.SetActive(false);
        originSlot = null;
    }

    void Update()
    {
        if (isDragging)
        {
            Vector2 pos;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(
                canvas.transform as RectTransform,
                Input.mousePosition, canvas.worldCamera, out pos
            );
            dragIcon.rectTransform.anchoredPosition = pos;
        }
    }

    public SlotUI GetSlotUnderPointer(PointerEventData eventData)
    {
        if (eventData == null) return null;

        var results = new System.Collections.Generic.List<RaycastResult>();
        EventSystem.current.RaycastAll(eventData, results);

        foreach (var result in results)
        {
            SlotUI slot = result.gameObject.GetComponent<SlotUI>();
            if (slot != null)
                return slot;
        }
        return null;
    }
}
