using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PikamoonUIManager : MonoBehaviour
{
    public PikamoonManager pikaManager;  // Reference to the PikaManager
    private int selectedLevel = 0;  // Default level
    void Start()
    {
        
    }

    // Function to select the Pika type and instantiate it
    public void SelectPikaType(int pikaTypeIndex)
    {
        pikaManager.InitPika(pikaTypeIndex, selectedLevel);
    }

    // Function to select the level
    public void SelectLevel(int levelIndex)
    {
        selectedLevel = levelIndex;  // Update the selected level
    }
}
