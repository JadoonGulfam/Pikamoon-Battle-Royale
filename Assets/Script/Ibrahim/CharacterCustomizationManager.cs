using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class CharacterCustomizationManager : MonoBehaviour
{
    public CharacterCustom characterCustom;
    public SkinnedMeshRenderer characterMesh; // Assumes a SkinnedMeshRenderer for blendshapes
    public Material skinMaterial;
    public Material eyeMaterial;
    public Material eyebrowMaterial;
    public Material lipMaterial;
    public Material hairMaterial;
    public AddressableDownloader addressableDownloader;
    public GameObject loader;
    public GameObject hairObject, shirtObject, trouserObject, shoesObject;
    void Start()
    {
        LoadCharacterCustomization();
    }
    public void SaveCharacterCustomization()
    {
        string json = JsonUtility.ToJson(characterCustom, true);
        File.WriteAllText(Application.persistentDataPath + "/characterCustom.json", json);
        Debug.Log("Character customization saved to " + Application.persistentDataPath + "/characterCustom.json");
    }

    public void LoadCharacterCustomization()
    {
        if (File.Exists(Application.persistentDataPath + "/characterCustom.json"))
        {
            string json = File.ReadAllText(Application.persistentDataPath + "/characterCustom.json");
            characterCustom = JsonUtility.FromJson<CharacterCustom>(json);
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

        Debug.Log("faceshape    " + characterCustom.faceShape);
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
            skinMaterial.color = newColor;
            characterCustom.skinColor = newColor;
        }
    }
    public void ApplyLipsColor(string color, bodyType type)
    {
        Color newColor;
        if (ColorUtility.TryParseHtmlString(color, out newColor))
        {
            lipMaterial.color = newColor;
            characterCustom.lipsColor = newColor;
        }
    }
    public void ApplyHairPreset(GameObject _preset, string _key, bodyType _type)
    {
        if (hairObject != null)
            Destroy(hairObject);
        hairObject = Instantiate(_preset);
        characterCustom.hairPreset = _key;
        loader.SetActive(false);
    }
    public void ApplyShirtPreset(GameObject _preset, string _key, bodyType _type)
    {
        if (shirtObject != null)
            Destroy(shirtObject);
        shirtObject = Instantiate(_preset);
        characterCustom.shirtPreset = _key;
        loader.SetActive(false);
    }
    public void ApplyTrouserPreset(GameObject _preset, string _key, bodyType _type)
    {
        if (trouserObject != null)
            Destroy(trouserObject);
        trouserObject = Instantiate(_preset);
        characterCustom.trouserPreset = _key;
        loader.SetActive(false);
    }
    public void ApplyShoesPreset(GameObject _preset, string _key, bodyType _type)
    {
        if (shoesObject != null)
            Destroy(shoesObject);
        shoesObject = Instantiate(_preset);
        characterCustom.shoespreset = _key;
        loader.SetActive(false);
    }
    public void ApplyHairColor(string color, bodyType type)
    {
        Color newColor;
        if (ColorUtility.TryParseHtmlString(color, out newColor))
        {
            hairMaterial.color = newColor;
            characterCustom.hairColor = newColor;
        }
    }
    public void ApplyEyeTexture(Texture2D _texture, string _key)
    {
        eyeMaterial.mainTexture = _texture;
        characterCustom.eyeColor = _key;
        loader.SetActive(false);
    }
    public void ApplyChanges()
    {
        SaveCharacterCustomization();
    }
}
[Serializable]
public class CharacterCustom
{
    public string gender;
    public int faceShape;
    public int eyeShape;
    public int lipsShape;
    public int noseShape;
    public string hairPreset;
    public string shirtPreset;
    public string trouserPreset;
    public string shoespreset;
    public string eyeColor;
    public Color eyeBrowColor;
    public Color skinColor;
    public Color hairColor;
    public Color lipsColor;
}
public enum bodyType { Face, Hair, Lips, Eyes, Nose, SkinColor, EyeColor, Shirt, Trouser, Shoes }
public enum genderType { male, female }