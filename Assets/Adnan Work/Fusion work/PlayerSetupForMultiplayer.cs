using UnityEngine;
using Fusion;
using NUnit.Framework;
using System.Collections.Generic;

public class PlayerSetupForMultiplayer : NetworkBehaviour
{
    
    [Header("List of Points (Transforms)")]
    public List<Transform> holdingpoints = new List<Transform>();

    public bool isMinePlayer;
    private void Start() // Change from Awake() to Start()
    {
        if (Object != null) // Ensure Object is initialized
        {
            isMinePlayer = Object.HasStateAuthority;
        }
        else
        {
            //Debug.LogError("NetworkObject is null!");
        }
    }
}
