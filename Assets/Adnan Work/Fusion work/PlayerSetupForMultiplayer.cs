using UnityEngine;
using Fusion;

public class PlayerSetupForMultiplayer : NetworkBehaviour
{
    public bool isMinePlayer;

    private void Start() // Change from Awake() to Start()
    {
        if (Object != null) // Ensure Object is initialized
        {
            isMinePlayer = Object.HasStateAuthority;
        }
        else
        {
            Debug.LogError("NetworkObject is null!");
        }
    }
}
