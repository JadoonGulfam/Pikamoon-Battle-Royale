using UnityEngine;

[RequireComponent(typeof(PikamoonInventory))]
public class PlayerPikamoonManager : MonoBehaviour
{
    private PikamoonInventory pikamoonInventory;

    private void Awake()
    {
        // Ensure the reference is assigned correctly
        pikamoonInventory = GetComponent<PikamoonInventory>();

        if (pikamoonInventory == null)
        {
            Debug.LogError("PikamoonInventory component is missing on " + gameObject.name);
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.C)) // Capture Pikamoon
        {
            GameObject newPikamoon = GetRandomPikamoon();
            if (newPikamoon != null)
            {
                pikamoonInventory.AddPikamoon(newPikamoon);
            }
            else
            {
                Debug.LogWarning("No available Pikamoon found to capture.");
            }
        }

        if (Input.GetKeyDown(KeyCode.P)) // Spawn Pikamoon
        {
            Vector3 spawnPosition = transform.position + new Vector3(2, 0, 2);
            pikamoonInventory.SpawnPikamoon(0, spawnPosition);
        }

        if (Input.GetKeyDown(KeyCode.R)) // Release Pikamoon
        {
            pikamoonInventory.ReleasePikamoon(0);
        }
    }

    private void Start()
    {
        // Assign the PikamoonInventory from a tagged object
        GameObject inventoryObject = GameObject.FindWithTag("PikaMoonInventory");
        if (inventoryObject != null)
        {
            pikamoonInventory = inventoryObject.GetComponent<PikamoonInventory>();
        }

        if (pikamoonInventory == null)
        {
            Debug.LogError("PikamoonInventory not found! Make sure the object is tagged correctly.");
        }
    }

    /// <summary>
    /// Finds a random available Pikamoon in the scene.
    /// </summary>
    private GameObject GetRandomPikamoon()
    {
        PikamoonRoaming[] availablePikamoons = FindObjectsOfType<PikamoonRoaming>();

        if (availablePikamoons.Length == 0)
        {
            return null; // No Pikamoons available
        }

        int randomIndex = Random.Range(0, availablePikamoons.Length);
        return availablePikamoons[randomIndex].gameObject;
    }
}
