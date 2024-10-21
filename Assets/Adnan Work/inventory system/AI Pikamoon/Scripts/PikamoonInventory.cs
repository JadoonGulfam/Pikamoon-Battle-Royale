using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerInputHandler))]  // Assuming PlayerInputHandler exists
public class PikamoonInventory : MonoBehaviour
{
    private List<GameObject> capturedPikamoons = new List<GameObject>();  // Holds multiple Pikamoons

    // Adds Pikamoon to the player's inventory and deactivates it in the world
    public void AddPikamoon(GameObject pikamoon)
    {
        if (!capturedPikamoons.Contains(pikamoon))
        {
            capturedPikamoons.Add(pikamoon);
            pikamoon.SetActive(false);
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
            pikamoonToSpawn.SetActive(true);
            PikamoonFollow followScript = pikamoonToSpawn.GetComponent<PikamoonFollow>();
            if (followScript != null) followScript.EnableFollowing();

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
