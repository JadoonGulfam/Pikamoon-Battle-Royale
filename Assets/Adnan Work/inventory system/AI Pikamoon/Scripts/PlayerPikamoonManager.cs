using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(PikamoonInventory))]
public class PlayerPikamoonManager : MonoBehaviour
{
    public PikamoonInventory pikamoonInventory;

    void Awake()
    {
       // pikamoonInventory = GetComponent<PikamoonInventory>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))  // Add Pikamoon to inventory
        {
            GameObject newPikamoon = GetRandomPikamoon();
            pikamoonInventory.AddPikamoon(newPikamoon);
        }

        if (Input.GetKeyDown(KeyCode.P))  // Spawn first Pikamoon in inventory
        {
            
            Vector3 spawnPosition = transform.position + new Vector3(2, 0, 2);
            pikamoonInventory.SpawnPikamoon(0, spawnPosition);  // Spawns first Pikamoon
        }

        if (Input.GetKeyDown(KeyCode.R))  // Release first Pikamoon in inventory
        {
            pikamoonInventory.ReleasePikamoon(0);  // Releases first Pikamoon
        }
    }

    private void Start()
    {
        pikamoonInventory = GameObject.FindWithTag("PikaMoonInventory").GetComponent<PikamoonInventory>();
    }

    [System.Obsolete]
    private GameObject GetRandomPikamoon()
    {
        // Logic to get a Pikamoon from the world
        PikamoonRoaming[] availablePikamoons = FindObjectsOfType<PikamoonRoaming>();
        return availablePikamoons.Length > 0 ? availablePikamoons[0].gameObject : null;
    }
}
