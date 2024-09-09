using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PreViewSystem : MonoBehaviour
{
    [SerializeField]
    private float previewYOffet = 0;

    [SerializeField]
    private GameObject cellIndicator;
    private GameObject preViewObject;

    [SerializeField]
    private Material preViewMaterialPrefab;
    private Material preViewmaterialInstance;

    private Renderer cellIndicatorRenderer;

    private void Start()
    {
        preViewmaterialInstance = new Material(preViewMaterialPrefab);
        cellIndicator.SetActive(false);
        cellIndicatorRenderer = cellIndicator.GetComponentInChildren<Renderer>();
    }

    public void StartShowingPlacementPreview(GameObject prefab, Vector2Int size)
    {
        preViewObject = Instantiate(prefab);

        // Calculate the offset to center the preview object
        Vector3 offset = new Vector3(size.x / 2f, 0, size.y / 2f);
        preViewObject.transform.position += offset;

        PreparePreView(preViewObject);
        PrepareCurser(size);
        cellIndicator.SetActive(true);
    }


    private void PrepareCurser(Vector2Int size)
    {
        if (size.x > 0 || size.y > 0)
        {
            Vector3 offset = new Vector3(size.x / 2f, 0, size.y / 2f);
            cellIndicator.transform.localPosition = offset;
            cellIndicatorRenderer.material.mainTextureScale = size;
        }
    }


    private void PreparePreView(GameObject preViewObject)
    {
        Renderer[] renderers = preViewObject.GetComponentsInChildren<Renderer>();

        foreach (Renderer renderer in renderers)
        {
            Material[] materials = renderer.materials;
            for (int i = 0; i < materials.Length; i++)
            {
                materials[i] = preViewmaterialInstance;
            }
            renderer.materials = materials;
        }
     }
    public void StopShowingPreView()
    {
        cellIndicator.SetActive(false);
        Destroy(preViewObject);
    }

    public void UpdatePosition(Vector3 position, bool validity)
    {
        if(isActiveAndEnabled)
        MoveCurser(position);
        MovePreView(position);
        ApplyFeedback(validity);
    }

    private void ApplyFeedback(bool validity)
    {
        Color c = validity ? Color.white : Color.red;
        cellIndicatorRenderer.material.color = c;
        c.a = 0.5f;
        preViewmaterialInstance.color = c;
    }

    private void MoveCurser(Vector3 position)
    {
        cellIndicator.transform.position = position;
    }

    private void MovePreView(Vector3 position)
    {
        // Assuming grid cell size is 1x1, adjust accordingly if different
        Vector3 offset = new Vector3(0.5f, 0, 0.5f); // Center offset for a 1x1 cell
        preViewObject.transform.position = position + offset;
    }

}
