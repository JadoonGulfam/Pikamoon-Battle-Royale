using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using UnityEngine.Analytics;

public class CharacterCustomizationManager : MonoBehaviour
{
    public CharacterData defaultCharacterdata;
    public Material eyebrowMaterial;
    public AddressableDownloader addressableDownloader;
    public GameObject loader;
    private GameObject presetObject;
    [HideInInspector]
    public GameObject _curretClickedBtn;
    [HideInInspector]
    public GameObject _lastAvatarClickedBtn;

    public CharacterData currentCharacterData;
    public AvatarController avatarController;  
    void Start()
    {
        LoadCharacterCustomization();
    }
    public void SaveCharacterCustomization()
    {
        string json = JsonUtility.ToJson(defaultCharacterdata, true);
        File.WriteAllText(Application.persistentDataPath + "/characterCustom.json", json);
        Debug.Log("Character customization saved to " + Application.persistentDataPath + "/characterCustom.json");
    }

    public void LoadCharacterCustomization()
    {
        if (File.Exists(Application.persistentDataPath + "/characterCustom.json"))
        {
            string json = File.ReadAllText(Application.persistentDataPath + "/characterCustom.json");
            defaultCharacterdata = JsonUtility.FromJson<CharacterData>(json);
            Debug.Log("Character customization loaded from " + Application.persistentDataPath + "/characterCustom.json");

            ApplyCharacterCustomization();
        }
        else
        {
            Debug.Log("No character customization file found at " + Application.persistentDataPath + "/characterCustom.json");
        }
    }
    private void ApplyCharacterCustomization()
    {
        // Apply the loaded customization settings to your character
        // Example:
        // characterMesh.SetBlendShapeWeight(0, characterCustom.faceShape);
        // skinMaterial.color = characterCustom.skinColor;
        // Implement other settings as needed
        currentCharacterData =  defaultCharacterdata.Clone();
        Debug.Log("faceshape    " + defaultCharacterdata.faceShape);
    }
    public void DownloadPresetAddressableObject(string key, bodyType type)
    {
        loader.SetActive(true);
        addressableDownloader.StartCoroutine(addressableDownloader.DownloadAddressableObject(key, type));
    }
    public void DownloadPresetAddressableTexture(string key, bodyType type)
    {
        loader.SetActive(true);
        addressableDownloader.StartCoroutine(addressableDownloader.DownloadAddressableTexture(key, type));
    }
    public void ApplySkinColor(string color, bodyType type)
    {
        Color newColor;
        if (ColorUtility.TryParseHtmlString(color, out newColor))
        {
            avatarController.body.materials[0].color = newColor;
            avatarController.body.materials[2].color = newColor;
            currentCharacterData.skinColor = newColor;
        }
    }
    public void ApplyLipsColor(string color, bodyType type)
    {
        Color newColor;
        if (ColorUtility.TryParseHtmlString(color, out newColor))
        {
            avatarController.body.materials[1].color = newColor;
            currentCharacterData.lipsColor = newColor;
        }
    }
    public void ApplyHairPreset(GameObject _preset, string _key, string _type)
    {
        avatarController.StichItem(-1, _preset, "Hair", avatarController.gameObject);
        currentCharacterData.hairPreset = _key;
        loader.SetActive(false);
    }
    public void ApplyShirtPreset(GameObject _preset, string _key, string _type)
    {
        avatarController.StichItem(-1, _preset, "Shirt", avatarController.gameObject);
        currentCharacterData.shirtPreset = _key;
        loader.SetActive(false);
    }
    public void ApplyTrouserPreset(GameObject _preset, string _key, string _type)
    {
        avatarController.StichItem(-1, _preset, "Trouser", avatarController.gameObject);
        currentCharacterData.trouserPreset = _key;
        loader.SetActive(false);
    }
    public void ApplyShoesPreset(GameObject _preset, string _key, string _type)
    {
        avatarController.StichItem(-1, _preset, "Shoes", avatarController.gameObject);
        currentCharacterData.shoespreset = _key;
        loader.SetActive(false);
    }
    public void ApplyOnPreset(GameObject _preset, string _key, string _type) 
    {
        if (presetObject != null)
            Destroy(presetObject);
        presetObject = Instantiate(_preset);
        currentCharacterData.characterPreset = _key;
        loader.SetActive(false);
    }
    //public void ApplyHairColor(string color, bodyType type)
    //{
    //    Color newColor;
    //    if (ColorUtility.TryParseHtmlString(color, out newColor))
    //    {
    //        hairMaterial.color = newColor;
    //        characterCustom.hairColor = newColor;
    //    }
    //}
    public void ApplyEyeTexture(Texture2D _texture, string _key)
    {
        avatarController.eye.mainTexture = _texture;
        currentCharacterData.eyeColor = _key;
        loader.SetActive(false);
    }
    public void ApplyChanges()
    {
        defaultCharacterdata = currentCharacterData.Clone();
        SaveCharacterCustomization();
    }
    public void ResetChanges()
    {
        // currentCharacterData =  defaultCharacterdata;
        ApplyEyeTexture(avatarController.defaultClothDatabase.maleAvatarDefaultCostume.DefaultEyes, "");
        ApplyLipsColor(avatarController.defaultClothDatabase.maleAvatarDefaultCostume.DefaultLipsColor.ToString(), bodyType.Lips);
        ApplySkinColor(avatarController.defaultClothDatabase.maleAvatarDefaultCostume.DefaultSkinColor.ToString(), bodyType.SkinColor);
        ApplyHairPreset(avatarController.defaultClothDatabase.maleAvatarDefaultCostume.DefaultHair, "", bodyType.Hair.ToString());
        ApplyShirtPreset(avatarController.defaultClothDatabase.maleAvatarDefaultCostume.DefaultShirt, "", bodyType.Shirt.ToString());
        ApplyTrouserPreset(avatarController.defaultClothDatabase.maleAvatarDefaultCostume.DefaultPent, "", bodyType.Trouser.ToString());
        ApplyShoesPreset(avatarController.defaultClothDatabase.maleAvatarDefaultCostume.DefaultShoes, "", bodyType.Shoes.ToString());
    }
}
