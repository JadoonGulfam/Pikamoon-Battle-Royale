using Pikamoon.Controller;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
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
    public GameObject _player;
    public List<GameObject> emojiList = new List<GameObject>();
    public DesignerPresetInfo[] designerPresetList;
    private int playerIndex = 0;
    public Transform playerPosition;
    public List<GameObject> uiPanels;

    public TextMeshProUGUI characterNameText;
    public TextMeshProUGUI characterDescriptionText;

    private string filePath;
    public SettingsData currentSettings;

    public event Action OnSettingsChanged;

    [Header("UI References")]
    public TextMeshProUGUI userName;
    public Button logOutBtn;

    [Header("UI References")]
    [SerializeField] private TMP_InputField searchInput;
    [SerializeField] private TMP_Dropdown regionDropdown;
    [SerializeField] private TMP_Dropdown privacyDropdown;
    [SerializeField] private TMP_Dropdown typeDropdown;
    [SerializeField] private Transform roomListContainer;
    [SerializeField] private GameObject roomItemPrefab;
    [SerializeField] private TMP_Dropdown mapDropdown;
    [SerializeField] private Image mapPreviewImage;

    [Header("Map Data")]
    [SerializeField] private List<MapData> maps = new List<MapData>();

    // Example room data (replace this with your actual list from server)
    private List<RoomData> allRooms = new List<RoomData>();
    private List<GameObject> spawnedRooms = new List<GameObject>();

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        filePath = Path.Combine(Application.persistentDataPath, "gamesettings.json");
        //Ensure directory exists before using it
        string directoryPath = Path.GetDirectoryName(filePath);
        if (!Directory.Exists(directoryPath))
        {
            Directory.CreateDirectory(directoryPath);
        }
        InitializeSettings();
        //LoadGame();
        logOutBtn.onClick.AddListener(LogOut);
    }   
    private void InitializeSettings()
    {
        if (!File.Exists(filePath))
        {
            Debug.Log("No settings file found. Creating defaults...");
            RestoreDefaults();
            SaveSettings();
        }
        else
        {
            Debug.Log("Settings file found. Loading...");
            LoadSettings();
        }
    }
    void Start()
    {
      //  LoadGame();
        SpawnPrefab(currentSettings.playerIndex);
        NetworkManager.Instance.ChrarcterIndex = currentSettings.playerIndex;

        // Populate dropdown with map names
        mapDropdown.ClearOptions();
        List<string> mapNames = new List<string>();
        foreach (var map in maps)
            mapNames.Add(map.mapName);
        mapDropdown.AddOptions(mapNames);
        mapDropdown.onValueChanged.AddListener(OnMapChanged);
        OnMapChanged(currentSettings.mapIndex);
        //// Assign dropdown listeners
        //searchInput.onValueChanged.AddListener(delegate { ApplyFilters(); });
        //regionDropdown.onValueChanged.AddListener(delegate { ApplyFilters(); });
        //privacyDropdown.onValueChanged.AddListener(delegate { ApplyFilters(); });
        //typeDropdown.onValueChanged.AddListener(delegate { ApplyFilters(); });

        //// Display all rooms initially
        //DisplayRooms(allRooms);
    }
    private void OnMapChanged(int _index)
    {
        if (_index >= 0 && _index < maps.Count)
        {
            //mapPreviewImage.sprite = maps[index].mapPreview;
            StartCoroutine(FadePreview(maps[_index].mapPreview));
            currentSettings.mapIndex = _index;
            NetworkManager.Instance.mapIndex = _index;
            // Optional: Play transition animation or fade
        }
    }
    private IEnumerator FadePreview(Sprite newSprite)
    {
        float duration = 0.3f;
        float elapsed = 0f;
        Color c = mapPreviewImage.color;

        // Fade out
        while (elapsed < duration)
        {
            c.a = Mathf.Lerp(1f, 0f, elapsed / duration);
            mapPreviewImage.color = c;
            elapsed += Time.deltaTime;
            yield return null;
        }

        mapPreviewImage.sprite = newSprite;

        // Fade in
        elapsed = 0f;
        while (elapsed < duration)
        {
            c.a = Mathf.Lerp(0f, 1f, elapsed / duration);
            mapPreviewImage.color = c;
            elapsed += Time.deltaTime;
            yield return null;
        }
    }
    void DisplayRooms(List<RoomData> roomsToShow)
    {
        // Clear old
        foreach (var item in spawnedRooms)
            Destroy(item);
        spawnedRooms.Clear();

        // Spawn new
        foreach (var room in roomsToShow)
        {
            GameObject item = Instantiate(roomItemPrefab, roomListContainer);
            item.GetComponentInChildren<TMP_Text>().text =
                $"{room.roomName}  ({room.currentPlayers}/{room.maxPlayers}) {room.region} {room.type} {room.privacy}";
            //roomObj.transform.Find("RoomName").GetComponent<Text>().text = room.roomName;
            //roomObj.transform.Find("RoomType").GetComponent<Text>().text = room.roomType;
            //roomObj.transform.Find("Region").GetComponent<Text>().text = room.region;
            //roomObj.transform.Find("Players").GetComponent<Text>().text = room.players.ToString();

            spawnedRooms.Add(item);
        }
    }
    public void CreateRoom(string roomName, int _player, int _maxPlayer, string roomType, string region, string _privacy)
    {
        // Create new data
        RoomData newRoom = new RoomData(roomName, _player, _maxPlayer, roomType, region, _privacy);
        allRooms.Add(newRoom);

        // Refresh UI
        DisplayRooms(allRooms);
    }
    private void ApplyFilters()
    {
        string searchText = searchInput.text.ToLower();
        string selectedRegion = regionDropdown.options[regionDropdown.value].text;
        string selectedPrivacy = privacyDropdown.options[privacyDropdown.value].text;
        string selectedType = typeDropdown.options[typeDropdown.value].text;

        // Combine all filters
        var filteredRooms = allRooms
             .Where(room =>
                 (string.IsNullOrEmpty(searchText) || room.roomName.ToLower().Contains(searchText)) && // Only name search
                 (selectedRegion == "ALL" || room.region == selectedRegion) &&
                 (selectedPrivacy == "ALL" || room.privacy == selectedPrivacy) &&
                 (selectedType == "ALL" || room.type == selectedType)
             )
             .ToList();

        DisplayRooms(filteredRooms);
    }
    //void LoadGame()
    //{
    //    LoadingManager.Instance.ActivateLoading("Splash_Loading", true);
    //    Invoke("EnvironmentLagCompensationRoutine", 3.5f);
    //    //Invoke(nameof(LoadNextScene), 3f);
    //}
    public void StartAutoBattler()
    {
        SceneManager.LoadScene("AutoBattler");
        Destroy(gameObject);
    }
    public void SpawnPrefab(int index)
    {
        if (index < 0 || index >= designerPresetList.Length) return; // Safety check
        if (_player != null && playerIndex == index) return;
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
        currentSettings.playerIndex = playerIndex;
        SaveSettings();
    }
    public void GameExit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
    public void SaveSettings()
    {
        string json = JsonUtility.ToJson(currentSettings, true);
        File.WriteAllText(filePath, json);
        Debug.Log("Settings saved to " + filePath);
    }
    public string GetSettingValue(SettingType type)
    {
        return type switch
        {
            SettingType.SFXVolume => currentSettings.sfxVolume ? "ON" : "OFF",
            SettingType.MusicVolume => currentSettings.musicVolume ? "ON" : "OFF",
            SettingType.Quality => currentSettings.quality,
            SettingType.TextureQuality => currentSettings.textureQuality,
            SettingType.AntiAliasing => currentSettings.antiAliasing,
            SettingType.PostProcessing => currentSettings.postProcessing ? "ON" : "OFF",
            SettingType.Bloom => currentSettings.Bloom,
            SettingType.Vignette => currentSettings.Vignette,
            SettingType.ColorAdjustments => currentSettings.ColorAdjustments,
            SettingType.AmbientOcclusion => currentSettings.AmbientOcclusion,
            SettingType.MotionBlur => currentSettings.MotionBlur,
            SettingType.Language => currentSettings.language,
            _ => ""
        };
    }

    public void SetSettingValue(SettingType type, string value)
    {
        switch (type)
        {
            case SettingType.SFXVolume:
                //if (float.TryParse(value, out float sfxv)) currentSettings.sfxVolume = sfxv;
                currentSettings.sfxVolume = value == "ON";
                break;
            case SettingType.MusicVolume:
                //if (float.TryParse(value, out float musv)) currentSettings.musicVolume = musv;
                currentSettings.musicVolume = value == "ON";
                break;
            case SettingType.Quality:
                ApplyQualitySetting(value);
                currentSettings.quality = value;
                break;
            case SettingType.TextureQuality:
                currentSettings.textureQuality = value;
                break;
            case SettingType.AntiAliasing:
                currentSettings.antiAliasing = value;
                break;
            case SettingType.PostProcessing:
                currentSettings.postProcessing = value == "ON";
                break;
            case SettingType.Bloom:
                currentSettings.Bloom = value;
                break;
            case SettingType.Vignette:
                currentSettings.Vignette = value;
                break;
            case SettingType.ColorAdjustments:
                currentSettings.ColorAdjustments = value;
                break;
            case SettingType.AmbientOcclusion:
                currentSettings.AmbientOcclusion = value;
                break;
            case SettingType.MotionBlur:
                currentSettings.MotionBlur = value;
                break;
            case SettingType.Language:
                currentSettings.language = value;
                break;
        }
    }
    private void ApplyQualitySetting(string value)
    {
        int index = Array.FindIndex(QualitySettings.names, q =>
            q.Equals(value, StringComparison.OrdinalIgnoreCase));

        if (index >= 0)
        {
            QualitySettings.SetQualityLevel(index, true);
            Debug.Log($"[Settings] Quality set to: {value}");
        }
        else
        {
            Debug.LogWarning($"[Settings] Quality level '{value}' not found in QualitySettings.");
        }
    }
    public void LoadSettings()
    {
        string json = File.ReadAllText(filePath);
        currentSettings = JsonUtility.FromJson<SettingsData>(json);
        Debug.Log("Settings loaded");

        if (currentSettings == null)
        {
            Debug.LogWarning("Settings file corrupted. Restoring defaults...");
            RestoreDefaults();
        }
        else
        {
            Debug.Log("Settings loaded successfully.");
        }
    }
    public void RestoreDefaults()
    {
        // Copy defaults into current settings
        currentSettings = new SettingsData
        {
            playerIndex = defaultSettings.playerIndex,
            musicVolume = defaultSettings.musicVolume,
            sfxVolume = defaultSettings.sfxVolume,
            quality = defaultSettings.quality,
            textureQuality = defaultSettings.textureQuality,
            antiAliasing = defaultSettings.antiAliasing,
            postProcessing = defaultSettings.postProcessing,
            Bloom = defaultSettings.Bloom,
            Vignette = defaultSettings.Vignette,
            ColorAdjustments = defaultSettings.ColorAdjustments,
            AmbientOcclusion = defaultSettings.AmbientOcclusion,
            MotionBlur = defaultSettings.MotionBlur,
            language = defaultSettings.language,
            mapIndex = defaultSettings.mapIndex,
        };

        // Save to JSON so it persists
        SaveSettings();

        // Force UI to update
        OnSettingsChanged?.Invoke();

        Debug.Log("Settings restored to default values!");
    }
    private readonly SettingsData defaultSettings = new SettingsData
    {
        playerIndex = 0,
        musicVolume = true,
        sfxVolume = true,
        quality = "High",
        textureQuality = "Medium",
        antiAliasing = "4x",
        postProcessing = true,
        Bloom = "Medium",
        Vignette = "Medium",
        ColorAdjustments = "Medium",
        AmbientOcclusion = "Medium",
        MotionBlur = "Medium",
        language = "English",
        mapIndex = 0,
    };
    public void LogOut()
    {
        AutoLoginManager.Clear();
        GameExit();
    }
}
[Serializable]
public class SettingsData
{
    public int playerIndex;

    public bool sfxVolume;
    public bool musicVolume;

    public string quality;
    public string textureQuality;
    public string antiAliasing;
    public bool postProcessing;
    public string Bloom;
    public string Vignette;
    public string ColorAdjustments;
    public string AmbientOcclusion;
    public string MotionBlur;

    public int mapIndex;

    public string language = "English";
}
public enum SettingType
{
    None,
    SFXVolume,
    MusicVolume,
    Quality,
    TextureQuality,
    AntiAliasing,
    PostProcessing,
    Bloom,
    Vignette,
    ColorAdjustments,
    AmbientOcclusion,
    MotionBlur,
    Language
}
[System.Serializable]
public class RoomData
{
    public string roomName;
    public int currentPlayers;
    public int maxPlayers;
    public string type;
    public string region;
    public string privacy;

    public RoomData(string name, int players, int max, string type, string region, string privacy)
    {
        roomName = name;
        currentPlayers = players;
        maxPlayers = max;
        this.type = type;
        this.region = region;
        this.privacy = privacy;
    }
}
[System.Serializable]
public class MapData
{
    public string mapName;
    public Sprite mapPreview;
}
[System.Serializable]
public class RegionPikamoons
{
    public string RegionName;
    public GameObject[] Pikamoons;
}