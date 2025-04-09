using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class PikamoonUI : MonoBehaviour
{
    public PikamoonInventory playerInventory;
    public Dropdown pikamoonDropdown;

    void Start()
    {
        UpdatePikamoonDropdown();
    }

    public void SpawnSelectedPikamoon()
    {
        int selectedIndex = pikamoonDropdown.value;
        Vector3 spawnPosition = transform.position + new Vector3(2, 0, 2);
        playerInventory.SpawnPikamoon(selectedIndex, spawnPosition);
    }

    public void ReleaseSelectedPikamoon()
    {
        int selectedIndex = pikamoonDropdown.value;
        playerInventory.ReleasePikamoon(selectedIndex);
    }

    public void UpdatePikamoonDropdown()
    {
        pikamoonDropdown.ClearOptions();
        List<string> options = new List<string>();
        //List<GameObject> capturedPikamoons = playerInventory.GetCapturedPikamoons();
        //foreach (var pikamoon in capturedPikamoons)
       // {
       //     options.Add(pikamoon.name);
        //}
        //pikamoonDropdown.AddOptions(options);
    }
}
