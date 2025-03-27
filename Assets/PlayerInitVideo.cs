using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInitVideo : MonoBehaviour
{
    public GameObject _player;
    public Transform TransferFunction;
    public List<GameObject> designerPreset;
    public Text mainText;

    void Start()
    {
        SpawnPrefab(6);

    }
    public void SpawnPrefab(int index)
    {
        if (index < 0 || index >= designerPreset.Count) return; // Safety check

        // Destroy existing prefab before spawning a new one
        if (_player != null)
        {
            Destroy(_player);
        }

        // Instantiate the selected prefab at the spawn position
        _player = Instantiate(designerPreset[index], TransferFunction);
        _player.name = designerPreset[index].name;
        mainText.text = _player.name;
    }

}
