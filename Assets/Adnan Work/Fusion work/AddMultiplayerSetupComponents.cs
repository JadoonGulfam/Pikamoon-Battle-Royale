using UnityEditor;
using UnityEngine;
using Fusion;


public class AddMultiplayerSetupComponents : MonoBehaviour
{
    //[MenuItem("Tools/Multiplayer/Add Fusion Multiplayer Setup")]
    //private static void AddFusionMultiplayerSetup()
    //{
    //    if (Selection.activeGameObject == null)
    //    {
    //        Debug.LogWarning("No GameObject selected. Please select a GameObject in the Hierarchy.");
    //        return;
    //    }

    //    GameObject selectedObject = Selection.activeGameObject;

    //    Undo.RegisterFullObjectHierarchyUndo(selectedObject, "Add Fusion Multiplayer Components");

    //    // Add PlayerSetupForMultiplayer
        

    //    // Add NetworkObject
    //    if (selectedObject.GetComponent<NetworkObject>() == null)
    //    {
    //        selectedObject.AddComponent<NetworkObject>();
    //        Debug.Log("Added NetworkObject.");
    //    }

    //    // Add NetworkTransform
    //    if (selectedObject.GetComponent<NetworkTransform>() == null)
    //    {
    //        selectedObject.AddComponent<NetworkTransform>();
    //        Debug.Log("Added NetworkTransform.");
    //    }

    //    // Add NetworkMecanimAnimator
    //    Animator animator = selectedObject.GetComponent<Animator>();
    //    if (animator != null)
    //    {
    //        if (selectedObject.GetComponent<NetworkMecanimAnimator>() == null)
    //        {
    //            selectedObject.AddComponent<NetworkMecanimAnimator>();
    //            Debug.Log("Added NetworkMecanimAnimator.");
    //        }
    //    }
    //    else
    //    {
    //        Debug.LogWarning("No Animator component found. NetworkMecanimAnimator was not added.");
    //    }
    //    if (selectedObject.GetComponent<PlayerSetupForMultiplayer>() == null)
    //    {
    //        selectedObject.AddComponent<PlayerSetupForMultiplayer>();
    //        Debug.Log("Added PlayerSetupForMultiplayer.");
    //    }

    //    Debug.Log("Fusion multiplayer components added successfully.");
    //}
}
