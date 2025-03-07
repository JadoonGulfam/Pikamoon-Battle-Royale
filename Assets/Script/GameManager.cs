using Fusion;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion.Sockets;
using System;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using System.IO;
  using WebSocketSharp;
public class GameManager : MonoBehaviour
{

    public static GameManager instance;
    public GameObject PlayerPrefab;
    public GameObject PlayerPrefabForSinglePlayer;
    public List<GameObject> allPlayer;
    public CharacterData characterdata;
    public GameObject _player;
    //private Dictionary<GameObject, Vector3> originalPositions = new Dictionary<GameObject, Vector3>();

    public List<GameObject> emojiList = new List<GameObject>();
    public UserDataBase userDataBase;
    public int playerIndex = 0;

    public List<GameObject> designerPreset;
    public int currentCharacterIndex=0;
    private void Awake()
    {
        if (instance == null) { instance = this; }
        DontDestroyOnLoad(gameObject);
    }
    void Start()
    {
        LoadGame();
    }
    void LoadGame()
    {
        LoadingManager.Instance.ActivateLoading("Splash_Loading");
        Invoke(nameof(LoadNextScene), 3f);
    }
    void LoadNextScene()
    {
        bool isUserLoggedIn = userDataBase.GetLoggedInUser() != null;
        string nextScene = isUserLoggedIn ? "Main Menu" : "Login Scene";

        if (!isUserLoggedIn)
        {
            // Load Login Scene Additively
            LoadingManager.Instance.LoadSceneAdditive(nextScene, () =>
            {
                LoadingManager.Instance.DeactivateAll(); // Hide splash panel after login UI is ready
            });
        }
        else
        {
            LoadingManager.Instance.DeactivateAll(); // Hide splash panel after loading
        }
    }
    //public void InitPlayer()
    //{
    //    // Check if players are already instantiated
    //    if (instantiatedPlayers.Count == allPlayer.Count)
    //    {
    //        // If all players are already instantiated, simply enable them and return
    //        foreach (GameObject player in instantiatedPlayers)
    //        {
    //            player.transform.position = originalPositions[player];
    //            if (!player.activeSelf)
    //            {
    //                player.SetActive(true);
    //            }
    //        }
    //        CharacterHoverEffect.isSelected = false; // Reset selection
    //        return;
    //    }
    //    GameObject playerInstance = Instantiate(allPlayer[playerIndex], allPlayerParentTransform);
    //    playerInstance.SetActive(true);
    //    instantiatedPlayers.Add(playerInstance);
    //    _player = playerInstance;
    //    originalPositions[playerInstance] = playerInstance.transform.position;
    //}
    public void StartAutoBattler()
    {
        SceneManager.LoadScene("AutoBattler");
        Destroy(gameObject);
    }
    public void SaveCharacterCustomization()
    {
        characterdata = _player.GetComponent<AvatarController>().currentCharacterData.Clone();
        string json = JsonUtility.ToJson(characterdata, true);
        File.WriteAllText(Application.persistentDataPath + "/characterCustom.json", json);
        Debug.Log("Character customization saved to " + Application.persistentDataPath + "/characterCustom.json");
        //save.interactable = false;
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
        _player = Instantiate(designerPreset[index], Vector3.zero, Quaternion.identity);
        currentCharacterIndex = index;
    }
}
