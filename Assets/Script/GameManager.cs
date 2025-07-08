using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{

    public static GameManager instance;
   // public GameObject PlayerPrefab;
    public List<GameObject> allPlayer;
    //public CharacterData characterdata;
    public GameObject _player, _weapon;
    //private Dictionary<GameObject, Vector3> originalPositions = new Dictionary<GameObject, Vector3>();

    public List<GameObject> emojiList = new List<GameObject>();
    public UserDataBase userDataBase;
    public int playerIndex = 0;
    public int weaponIndex = 0;
    public List<GameObject> allWeapons;
    public List<GameObject> designerPreset;
    public Transform weaponMountPoint;
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
        LoadingManager.Instance.ActivateLoading("Splash_Loading", true);
        //Invoke(nameof(LoadNextScene), 3f);
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
    //public void SaveCharacterCustomization()
    //{
    //    characterdata = _player.GetComponent<AvatarController>().currentCharacterData.Clone();
    //    string json = JsonUtility.ToJson(characterdata, true);
    //    File.WriteAllText(Application.persistentDataPath + "/characterCustom.json", json);
    //    Debug.Log("Character customization saved to " + Application.persistentDataPath + "/characterCustom.json");
    //    //save.interactable = false;
    //}
    public void SpawnPlayer()
    {
        if (_player != null)
        {
            _player.SetActive(true);
        }
        else 
        {
            _player = Instantiate(designerPreset[playerIndex], Vector3.zero, Quaternion.identity);
            weaponMountPoint = _player.GetComponent<Animator>().GetBoneTransform(HumanBodyBones.RightHand);
        }
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
        weaponMountPoint = _player.GetComponent<Animator>().GetBoneTransform(HumanBodyBones.RightHand);
        NetworkManager.Instance.ChrarcterIndex=playerIndex = index;
    }
    public void SpawnWeapons()
    {
        if (_player != null)
        {
            _player.SetActive(true);
        }
        else
        {
            _player = Instantiate(designerPreset[playerIndex], Vector3.zero, Quaternion.identity);
            weaponMountPoint = _player.GetComponent<Animator>().GetBoneTransform(HumanBodyBones.RightHand);
        }
        if (_weapon != null)
        {
            _weapon.SetActive(true);
        }
        else
        {
            _weapon = Instantiate(allWeapons[weaponIndex], Vector3.zero, Quaternion.identity);

            if (weaponMountPoint != null)
            {
                _weapon.transform.SetParent(weaponMountPoint);
                _weapon.transform.localPosition = new Vector3(-0.09f, 0f, -0.05f);      // Or use a specific offset if needed
                _weapon.transform.localRotation = Quaternion.Euler(new Vector3(-15f, -140f, -25f));
            }
        }
    }
    public void SpawnWeaponPrefab(int index)
    {
        if (index < 0 || index >= allWeapons.Count) return; // Safety check

        // Destroy existing prefab before spawning a new one
        if (_weapon != null)
        {
            Destroy(_weapon);
        }
        if (_player != null) 
        {
            _player.SetActive(true);
        }
        else
        _player = Instantiate(designerPreset[playerIndex], Vector3.zero, Quaternion.identity);
        weaponMountPoint = _player.GetComponent<Animator>().GetBoneTransform(HumanBodyBones.RightHand);
        _weapon = Instantiate(allWeapons[index], Vector3.zero, Quaternion.identity);
        NetworkManager.Instance.selectedWearablesIndex = weaponIndex = index;

        if (weaponMountPoint != null)
        {
            _weapon.transform.SetParent(weaponMountPoint);
            _weapon.transform.localPosition = new Vector3(-0.09f, 0f, -0.05f);      // Or use a specific offset if needed
            _weapon.transform.localRotation = Quaternion.Euler(new Vector3(-15f, -140f, -25f));
        }
    }
    public void PlayerActiveDeactive(bool value) 
    {
            _player?.SetActive(value);
    }
}
