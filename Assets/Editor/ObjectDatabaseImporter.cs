using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using System.IO;

public class ObjectDatabaseImporter : EditorWindow
{
    private string jsonFilePath = "Assets/PikamoonStats.json";
    private ObjectDatabaseSO objectDatabase;
    private System.DateTime lastImportTime;

    [MenuItem("Tools/Import Object Data")]
    public static void ShowWindow()
    {
        GetWindow<ObjectDatabaseImporter>("Object Data Importer");
    }

    private void OnEnable()
    {
        // Initialize the last import time
        lastImportTime = System.DateTime.MinValue;
        LoadLastImportTime();
    }

    private void OnGUI()
    {
        GUILayout.Label("Import Object Data from JSON File", EditorStyles.boldLabel);

        jsonFilePath = EditorGUILayout.TextField("JSON File Path", jsonFilePath);
        objectDatabase = (ObjectDatabaseSO)EditorGUILayout.ObjectField("Object Database", objectDatabase, typeof(ObjectDatabaseSO), false);

        if (GUILayout.Button("Import Data"))
        {
            ImportData();
        }

        if (GUILayout.Button("Check for Updates"))
        {
            CheckForUpdates();
        }
    }

    private void ImportData()
    {
        if (objectDatabase == null)
        {
            Debug.LogError("Object Database ScriptableObject is not assigned.");
            return;
        }

        if (!File.Exists(jsonFilePath))
        {
            Debug.LogError("File not found: " + jsonFilePath);
            return;
        }

        string jsonData = File.ReadAllText(jsonFilePath);
        Wrapper importedWrapper = JsonUtility.FromJson<Wrapper>(jsonData);

        if (importedWrapper != null)
        {
            objectDatabase.objectData = importedWrapper.items;

            // Save the changes to the ScriptableObject
            EditorUtility.SetDirty(objectDatabase);
            AssetDatabase.SaveAssets();
            Debug.Log("Object data imported successfully.");

            // Update the last import time
            lastImportTime = File.GetLastWriteTime(jsonFilePath);
            SaveLastImportTime();
        }
        else
        {
            Debug.LogError("Failed to parse JSON data.");
        }
    }

    private void CheckForUpdates()
    {
        if (File.Exists(jsonFilePath))
        {
            System.DateTime lastWriteTime = File.GetLastWriteTime(jsonFilePath);

            if (lastWriteTime > lastImportTime)
            {
                Debug.Log("JSON file has been updated. Reimporting data...");
                ImportData();
            }
            else
            {
                Debug.Log("JSON file has not been updated.");
            }
        }
        else
        {
            Debug.LogError("File not found: " + jsonFilePath);
        }
    }

    private void LoadLastImportTime()
    {
        string path = Application.persistentDataPath + "/lastImportTime.dat";
        if (File.Exists(path))
        {
            string timestamp = File.ReadAllText(path);
            if (System.DateTime.TryParse(timestamp, out System.DateTime savedTime))
            {
                lastImportTime = savedTime;
            }
        }
    }

    private void SaveLastImportTime()
    {
        string path = Application.persistentDataPath + "/lastImportTime.dat";
        File.WriteAllText(path, lastImportTime.ToString());
    }

    [System.Serializable]
    private class Wrapper
    {
        public List<ObjectData> items;
    }
}
