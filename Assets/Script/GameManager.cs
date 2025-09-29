using Pikamoon.Controller;
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
    public void StartAutoBattler()
    {
        SceneManager.LoadScene("AutoBattler");
        Destroy(gameObject);
    }
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
}
