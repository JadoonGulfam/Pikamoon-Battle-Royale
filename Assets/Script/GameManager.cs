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

    private void Awake()
    {
        if (instance == null) { instance = this; }
        DontDestroyOnLoad(gameObject);

        filePath = Path.Combine(Application.persistentDataPath, "gamesettings.json");
        LoadSettings();
    }
    void Start()
    {
        LoadGame();
        SpawnPrefab(currentSettings.playerIndex);
        NetworkManager.Instance.ChrarcterIndex = currentSettings.playerIndex;
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
        currentSettings.playerIndex = playerIndex;
        SaveSettings();
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
        }
    }
    public void LoadSettings()
    {
        if (File.Exists(filePath))
        {
            string json = File.ReadAllText(filePath);
            currentSettings = JsonUtility.FromJson<SettingsData>(json);
            Debug.Log("Settings loaded");
        }
        else
        {
            Debug.Log("No settings file found, using defaults.");
            RestoreDefaults();
            SaveSettings();
        }
    }
    public void RestoreDefaults()
    {
        // Copy defaults into current settings
        currentSettings = new SettingsData
        {
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
    };

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
    MotionBlur
}