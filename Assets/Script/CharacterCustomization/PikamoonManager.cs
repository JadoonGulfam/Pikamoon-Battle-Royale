using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using CharacterCustomization;
public class PikamoonManager : MonoBehaviour
{
    public Pikamoons[] pikaTypes;  // Array to store different Pika types
    private Dictionary<string, GameObject> pikaPool = new Dictionary<string, GameObject>();  // Object pool to avoid re-instantiating
    GameObject activePikaObject;
    public Transform pikaTransform;
    private void OnEnable()
    {
        //GetComponent<Button>().onClick.AddListener(OnButtonClick);
    }
    private void OnDisable()
    {
        // GetComponent<Button>().onClick.RemoveListener(OnButtonClick);
    }
    void Start()
    {

    }
    public void OnButtonClick(int _pikaTypeIndex, int _level)
    {
        InitPika(_pikaTypeIndex, _level);
    }
    void Update()
    {

    }

    public void InitPika(int _pikaTypeIndex, int _level)
    {
        if (_pikaTypeIndex >= pikaTypes.Length)
        {
            Debug.LogError("Invalid Pika type index.");
            return;
        }
        Pikamoons selectedPika = pikaTypes[_pikaTypeIndex]; // Get the Pika from the array
        GameObject pikaPrefab = selectedPika.GetLevel(_level); // Get the specific level
        if (pikaPrefab == null)
        {
            Debug.LogError("Pika prefab not found for the given level.");
            return;
        }
        // Hide the current active Pika if any
        if (activePikaObject != null)
        {
            activePikaObject.SetActive(false);
        }
        string poolKey = selectedPika.pika + "_" + _level;  // Create a unique key for the pool (Pika name + level)
                                                            // Check if the requested Pika is already instantiated
        if (pikaPool.ContainsKey(poolKey))
        {
            activePikaObject = pikaPool[poolKey];
        }
        else
        {
            // Instantiate a new Pika and add it to the pool
            activePikaObject = Instantiate(pikaPrefab, pikaTransform);
            pikaPool.Add(poolKey, activePikaObject);
        }

        activePikaObject.SetActive(true);  // Activate the newly selected Pika
    }


}
public enum Pika { barken, salvet, lava }
[Serializable]
public class Pikamoons
{
    public Pika pika;
    public GameObject[] levels;  // Array to store the Pika's different evolution levels

   // Return the GameObject of the Pika for a specific level
    public GameObject GetLevel(int level)
    {
        if (level >= 0 && level < levels.Length)
        {
            return levels[level];
        }
        else
        {
            Debug.LogError($"Level {level} for Pika {pika} is not valid.");
            return null;
        }
    }
}