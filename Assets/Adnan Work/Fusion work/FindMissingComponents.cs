using UnityEngine;
using UnityEditor;

public class FindMissingComponents
{
    //[MenuItem("Tools/Find Missing Components In Scene")]
    [System.Obsolete]
    public static void Find()
    {
        int missingCount = 0;
        GameObject[] allObjects = Object.FindObjectsOfType<GameObject>();

        foreach (GameObject go in allObjects)
        {
            Component[] components = go.GetComponents<Component>();

            for (int i = 0; i < components.Length; i++)
            {
                if (components[i] == null)
                {
                    Debug.LogWarning($"Missing Component on GameObject: {GetGameObjectPath(go)}", go);
                    missingCount++;
                }
            }
        }

        Debug.Log($"Search complete. Found {missingCount} GameObject(s) with missing components.");
    }

    private static string GetGameObjectPath(GameObject obj)
    {
        string path = obj.name;
        Transform current = obj.transform;
        while (current.parent != null)
        {
            current = current.parent;
            path = current.name + "/" + path;
        }
        return path;
    }
}
