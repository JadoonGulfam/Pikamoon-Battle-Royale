using System;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{
    [Serializable]
    public class DesignerPresetInfo 
    {
        public string name;
        public string description;
        public GameObject prefab;
    }
    public static GameManager instance;
    public GameObject _player;//, _weapon;
    public List<GameObject> emojiList = new List<GameObject>();
    //public List<GameObject> allWeapons;
   // public List<GameObject> designerPreset;
    public DesignerPresetInfo[] designerPresetList;
    public UserDataBase userDataBase;
    private int playerIndex = 0;
   // private int weaponIndex = 2;
   // private Transform weaponMountPoint;
    public Transform playerPosition;
    public List<GameObject> uiPanels;

    public TextMeshProUGUI characterNameText;
    public TextMeshProUGUI characterDescriptionText;
    private void Awake()
    {
        if (instance == null) { instance = this; }
        DontDestroyOnLoad(gameObject);
    }
    void Start()
    {
        LoadGame();
        SpawnPrefab(0);
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
    //public void SpawnPlayer()
    //{
    //    if (_player != null)
    //    {
    //        _player.SetActive(true);
    //    }
    //    else
    //    {
    //        _player = Instantiate(designerPreset[playerIndex], playerPosition);
    //        weaponMountPoint = _player.GetComponent<Animator>().GetBoneTransform(HumanBodyBones.RightHand);
    //    }
    //    if (_weapon != null)
    //    {
    //        _weapon.SetActive(false);
    //    }
    //}
    public void SpawnPrefab(int index)
    {
        if (index < 0 || index >= designerPresetList.Length) return; // Safety check

        // Destroy existing prefab before spawning a new one
        if (_player != null)
        {
            Destroy(_player);
        }

        // Instantiate the selected prefab at the spawn position
        _player = Instantiate(designerPresetList[index].prefab, playerPosition);
        //weaponMountPoint = _player.GetComponent<Animator>().GetBoneTransform(HumanBodyBones.RightHand);
        characterNameText.text = designerPresetList[index].name;
        characterDescriptionText.text = designerPresetList[index].description;
        playerIndex = index;
    }
    //public void SpawnWeapons()
    //{
    //    if (_player != null)
    //    {
    //        _player.SetActive(true);
    //    }
    //    else
    //    {
    //        _player = Instantiate(designerPreset[playerIndex], Vector3.zero, Quaternion.identity);
    //        weaponMountPoint = _player.GetComponent<Animator>().GetBoneTransform(HumanBodyBones.RightHand);
    //    }
    //    if (_weapon != null)
    //    {
    //        _weapon.SetActive(true);
    //    }
    //    else
    //    {
    //        _weapon = Instantiate(allWeapons[weaponIndex], Vector3.zero, Quaternion.identity);

    //        if (weaponMountPoint != null)
    //        {
    //            _weapon.transform.SetParent(weaponMountPoint);
    //            _weapon.transform.localPosition = new Vector3(-0.09f, 0f, -0.05f);      // Or use a specific offset if needed
    //            _weapon.transform.localRotation = Quaternion.Euler(new Vector3(-15f, -140f, -25f));
    //        }
    //    }
    //}
    //public void SpawnWeaponPrefab(int index)
    //{
    //    if (index < 0 || index >= allWeapons.Count) return; // Safety check

    //    // Destroy existing prefab before spawning a new one
    //    if (_weapon != null)
    //    {
    //        Destroy(_weapon);
    //    }
    //    if (_player != null)
    //    {
    //        _player.SetActive(true);
    //    }
    //    else
    //        _player = Instantiate(designerPreset[playerIndex], Vector3.zero, Quaternion.identity);
    //    weaponMountPoint = _player.GetComponent<Animator>().GetBoneTransform(HumanBodyBones.RightHand);
    //    _weapon = Instantiate(allWeapons[index], Vector3.zero, Quaternion.identity);
    //    weaponIndex = index;

    //    if (weaponMountPoint != null)
    //    {
    //        _weapon.transform.SetParent(weaponMountPoint);
    //        _weapon.transform.localPosition = new Vector3(-0.09f, 0f, -0.05f);      // Or use a specific offset if needed
    //        _weapon.transform.localRotation = Quaternion.Euler(new Vector3(-15f, -140f, -25f));
    //    }
    //}
    //public void PlayerActiveDeactive(bool value)
    //{
    //   if(_player != null)    _player.SetActive(value);
    //}
    public void SwitchUIPanels(int index) 
    {
        foreach (var panel in uiPanels) 
        {
            panel.SetActive(false);
        }
        uiPanels[index].SetActive(true);
    }
    public void SelectPlayer() 
    {
        NetworkManager.Instance.ChrarcterIndex = playerIndex;
    }
    //public void SelectWeapon()
    //{
    //    NetworkManager.Instance.selectedWearablesIndex = weaponIndex;
    //}
}
