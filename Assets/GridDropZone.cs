using UnityEngine;
using UnityEngine.EventSystems;

public class GridDropZone : MonoBehaviour, IDropHandler
{

    public float gridSize = 1.0f; // Set the size of each grid cell

    // This method is called when a draggable object is dropped into the zone
    public void OnDrop(PointerEventData eventData)
    {
        Draggable draggable = eventData.pointerDrag.GetComponent<Draggable>();

        if (draggable != null)
        {
            // Snap the object to the closest grid point
            Vector3 dropPosition = GetNearestGridPosition(draggable.transform.position);
            draggable.transform.position = dropPosition;

            // Set the parent of the dragged object to the drop zone
            draggable.parentToReturnTo = this.transform;
        }
    }

    // Method to calculate the nearest grid position
    private Vector3 GetNearestGridPosition(Vector3 originalPosition)
    {
        float x = Mathf.Round(originalPosition.x / gridSize) * gridSize;
        float y = Mathf.Round(originalPosition.y / gridSize) * gridSize;
        float z = Mathf.Round(originalPosition.z / gridSize) * gridSize;

        return new Vector3(x, y, z);
    }
}
