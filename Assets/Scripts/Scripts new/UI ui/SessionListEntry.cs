using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Fusion;
using UnityEngine.UI;

public class SessionListEntry : MonoBehaviour
{
    public TextMeshProUGUI roomName, PlayerCount;
    public Button joinButton;

    private NetworkManager networkManager;

    private void Start()
    {
        // Find the NetworkManager in the scene
        networkManager = FindObjectOfType<NetworkManager>();

        if (networkManager == null)
        {
            Debug.LogError("NetworkManager not found in the scene.");
        }
    }

    public void JoinRoom()
    {
        if (networkManager != null)
        {
            networkManager.JoinSession(roomName.text);
            print("Joining session: " + roomName.text);
        }
        else
        {
            Debug.LogError("NetworkManager reference is null.");
        }
    }
}
