using DG.Tweening;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;
using Fusion;
using static PikamoonPopulationManager;
using System.Collections;
using Photon.Realtime;
[RequireComponent(typeof(PlayerInputHandler))]  // Assuming PlayerInputHandler exists
public class PikamoonInventory : MonoBehaviour
{
    public List<GameObject> capturedPikamoons = new List<GameObject>();  // Holds multiple Pikamoons

    public GameObject player;
    public GameObject[] players;
    // Adds Pikamoon to the player's inventory and deactivates it in the world
    IEnumerator Start()
    {
        yield return new WaitForSeconds(0.5f);
       // player = GameObject.FindWithTag("Player").transform;

        players = GameObject.FindGameObjectsWithTag("Player");

        foreach (var playerObject in players)
        {
            // Check if the player object has authority (is the master player)
            if (playerObject.GetComponent<NetworkObject>().HasStateAuthority)
            {
                print("player set");
                player = playerObject;
                break; // Exit the loop once we find the master player
            }
            print("player not found");
        }
    }
    public void AddPikamoon(GameObject pikamoon)
    {
        print("0000");

        if (!capturedPikamoons.Contains(pikamoon))
        {
            capturedPikamoons.Add(pikamoon);
            pikamoon.SetActive(false);
            pikamoon.GetComponent<PikamoonAI>().setPlayer(player.transform);
            pikamoon.GetComponent<PikamoonAI>().Call_RPC_AddPikamoon(pikamoon);



            Debug.Log($"Pikamoon {pikamoon.name} added to inventory.");
        }
    }


    // Spawns a Pikamoon from the inventory
    public void SpawnPikamoon(int index, Vector3 spawnPosition)
    {
        if (index >= 0 && index < capturedPikamoons.Count)
        {
            GameObject pikamoonToSpawn = capturedPikamoons[index];
            pikamoonToSpawn.transform.position = spawnPosition;
           // pikamoonToSpawn.transform.localScale = Vector3.one * 2;
            
            PikamoonFollow followScript = pikamoonToSpawn.GetComponent<PikamoonFollow>();
            followScript.SetMasterCharacter(player.transform);
            if (followScript != null) followScript.EnableFollowing();
            else print("follow script is null");

            pikamoonToSpawn.GetComponent<PikamoonAI>().Call_RPC_SpawnPikamoon(pikamoonToSpawn, spawnPosition);
            pikamoonToSpawn.SetActive(true);

            Debug.Log($"Pikamoon {pikamoonToSpawn.name} spawned.");
        }
    }

    // Releases a Pikamoon back to the world, enabling its roaming behavior
    public void ReleasePikamoon(int index)
    {
        if (index >= 0 && index < capturedPikamoons.Count)
        {
            GameObject pikamoonToRelease = capturedPikamoons[index];
            capturedPikamoons.RemoveAt(index);

            PikamoonRoaming roamingScript = pikamoonToRelease.GetComponent<PikamoonRoaming>();
            if (roamingScript != null)
            {
                roamingScript.EnableRoaming(); // Start roaming immediately
            }

            // Call DisableFollowing when releasing
            PikamoonFollow followScript = pikamoonToRelease.GetComponent<PikamoonFollow>();
            if (followScript != null)
            {
                followScript.DisableFollowing();
            }

            pikamoonToRelease.SetActive(true);

            Debug.Log($"Pikamoon {pikamoonToRelease.name} released.");
        }
    }


    // Gets the list of captured Pikamoons
    public List<GameObject> GetCapturedPikamoons()
    {
        return capturedPikamoons;
    }

    // Updates the inventory UI if applicable (optional)
    public void UpdateInventoryUI()
    {
        // Logic to update the UI with the inventory
    }



}
