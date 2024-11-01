using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Transform))]
public class PlaceOnTerrainEditor : Editor
{
    void OnSceneGUI()
    {
        // Check if the user is placing the object in the scene
        if (Event.current.type == EventType.MouseUp && Event.current.button == 0)
        {
            Transform objTransform = ((Transform)target);
            PlaceObjectOnTerrain(objTransform);
        }
    }

    private void PlaceObjectOnTerrain(Transform objTransform)
    {
        Ray ray = new Ray(objTransform.position + Vector3.up * 100, Vector3.down);
        RaycastHit hit;

        // Check if we hit the terrain
        if (Physics.Raycast(ray, out hit, Mathf.Infinity))
        {
            Terrain terrain = hit.collider.GetComponent<Terrain>();
            if (terrain != null)
            {
                // Move the object to the hit point on the terrain
                objTransform.position = hit.point;
            }
        }
    }
}
