using System.Collections;
using UnityEngine;
using Fusion;
using TMPro;

public class PlayerStat : NetworkBehaviour
{
    [Networked] public NetworkString<_32> PlayerName { get; set; } // Networked property for player name

    [SerializeField] private TextMeshPro playerNameLabel; // Reference to the TextMeshPro for displaying the name

    private void Start()
    {
        
        if (HasStateAuthority)
        {
            PlayerName = NetworkManager.Instance.GetPlayerName();
            Debug.Log("This object belongs to the local player.");
        }
        else
        {
            Debug.Log("This object belongs to a remote player.");
        }
        UpdatePlayerName();
    }

    //public override void FixedUpdateNetwork()
    //{
    //    // Update the displayed name in case it changes
    //    print("update plaer name");
    //    UpdatePlayerName();
    //}

    private void UpdatePlayerName()
    {
        // Update the TMP text with the player's name
        if (playerNameLabel != null && !string.IsNullOrEmpty(PlayerName.ToString()))
        {
            playerNameLabel.text = PlayerName.ToString();
        }
    }
}
