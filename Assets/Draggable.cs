using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Draggable : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{

    [HideInInspector]
    public Transform parentToReturnTo = null;
    [HideInInspector]
    public Transform placeHolderParent = null;

    private GameObject placeHolder = null;
    private GameObject draggingImage = null;


    public GameObject lastHitObject = null; // Store the last hit object
    public Material originalMaterial = null; // Store the original material to reset it later

    public void OnBeginDrag(PointerEventData eventData)
    {
        // Instantiate a new image that will act as the dragged item
        draggingImage = new GameObject("DraggedImage");
        draggingImage.transform.SetParent(this.transform.parent.parent, false); // Set it in the same hierarchy level as the original item

        // Copy the dragged item's image
        Image originalImage = GetComponent<Image>();
        Image newImage = draggingImage.AddComponent<Image>();
        newImage.sprite = originalImage.sprite;
        newImage.rectTransform.sizeDelta = originalImage.rectTransform.sizeDelta;
        newImage.color = new Color(originalImage.color.r, originalImage.color.g, originalImage.color.b, 0.6f); // Optional: Set transparency
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (draggingImage != null)
        {
            draggingImage.transform.position = eventData.position;

        }



        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition); // Create a ray from the camera through the mouse position
        RaycastHit hit; // Variable to store the raycast hit information

        if (Physics.Raycast(ray, out hit))
        {
            GameObject hitObject = hit.collider.gameObject;
            print("Here");
            // If the ray hits a new object (not the same as the last hit object)
            if (hitObject != lastHitObject)
            {
                // Reset the transparency of the last hit object if there was one
                if (lastHitObject != null)
                {
                    SetObjectTransparency(lastHitObject, 0.2f); // Trigger exit, set transparency back to 20%
                }

                // Store the new object and its original material
                lastHitObject = hitObject;

                // Make the hit object transparent
                SetObjectTransparency(hitObject, 0.9f); // Trigger enter, set transparency to 90%
            }
            else
            {
                // Trigger stay: keep the object's transparency at 90%
                SetObjectTransparency(hitObject, 0.9f);
            }


        }
    }
    private void SetObjectTransparency(GameObject obj, float transparency)
    {
        Renderer renderer = obj.GetComponent<Renderer>();
        if (renderer != null)
        {
            foreach (Material mat in renderer.materials)
            {
                Color color = mat.color;
                color.a = transparency;
                mat.color = color;

                // Enable transparency in the material shader
                mat.SetFloat("_Mode", 3); // 3 for Transparent mode
                mat.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
                mat.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
                mat.SetInt("_ZWrite", 0);
                mat.DisableKeyword("_ALPHATEST_ON");
                mat.EnableKeyword("_ALPHABLEND_ON");
                mat.DisableKeyword("_ALPHAPREMULTIPLY_ON");
                mat.renderQueue = 3000;
            }
        }
    }
    public void OnEndDrag(PointerEventData eventData)
    {
        // Destroy the dragging image when done dragging
        if (draggingImage != null)
        {
            Destroy(draggingImage);
        }

        // Reposition the original item to the new sibling index
      //  this.transform.SetParent(parentToReturnTo);
     //   this.transform.SetSiblingIndex(placeHolder.transform.GetSiblingIndex());
      //  this.GetComponent<CanvasGroup>().blocksRaycasts = true;
      //  Destroy(placeHolder); // Clean up the placeholder
    }
}
