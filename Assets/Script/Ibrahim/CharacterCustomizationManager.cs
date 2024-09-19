using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class CharacterCustomizationManager : MonoBehaviour
{
    public CharacterData defaultCharacterdata;
    public GameObject loader;
    public AvatarController avatarController;
    public AvatarBodyParts avatarBodyParts;
    public int isGuest = 0;
    public Button save, reset;
    private void OnEnable()
    {
        save.onClick.AddListener(ApplyChanges);
        reset.onClick.AddListener(ResetChanges);
    }
    private void OnDisable()
    {
        save.onClick.RemoveListener(ApplyChanges);
        reset.onClick.RemoveListener(ResetChanges);
    }
    void Start()
    {
        LoadCharacterCustomization();
    }
    public void SaveCharacterCustomization()
    {
        string json = JsonUtility.ToJson(defaultCharacterdata, true);
        File.WriteAllText(Application.persistentDataPath + "/characterCustom.json", json);
        Debug.Log("Character customization saved to " + Application.persistentDataPath + "/characterCustom.json");
        save.interactable = false;
    }

    public void LoadCharacterCustomization()
    {
        if (File.Exists(Application.persistentDataPath + "/characterCustom.json"))
        {
            string json = File.ReadAllText(Application.persistentDataPath + "/characterCustom.json");
            defaultCharacterdata = JsonUtility.FromJson<CharacterData>(json);
            avatarBodyParts.currentCharacterData = defaultCharacterdata.Clone();
            Debug.Log("Character customization loaded from " + Application.persistentDataPath + "/characterCustom.json");
            //int isGuest = PlayerPrefs.GetInt("Guest", 0) == 1 ? 1 : 0;
            if (isGuest == 1)
                ApplyCharacterCustomization();
            else
                avatarController.SetAvatarClothDefault(avatarController.gameObject, "Male");
        }
        else
        {
            Debug.Log("No character customization file found at " + Application.persistentDataPath + "/characterCustom.json");
            avatarController.SetAvatarClothDefault(avatarController.gameObject, "Male");
        }
    }
    private async void ApplyCharacterCustomization()
    {

        avatarBodyParts.currentCharacterData = defaultCharacterdata.Clone();
        if (Constants.downloadAddressableObject != null)
        {
            if (defaultCharacterdata.hairPreset != null && defaultCharacterdata.hairPreset != "")
            {
                _ = StartCoroutine(Constants.downloadAddressableObject(defaultCharacterdata.hairPreset, bodyType.Hair, avatarBodyParts.gameObject, true));
            }
            else
            {
                avatarController.WearDefaultItem("Hair", avatarController.gameObject, "Male");
            }
            if (defaultCharacterdata.trouserPreset != null && defaultCharacterdata.trouserPreset != "")
            {
                _ = StartCoroutine(Constants.downloadAddressableObject(defaultCharacterdata.trouserPreset, bodyType.Trouser, avatarBodyParts.gameObject, false));
            }
            else
            {
                avatarController.WearDefaultItem("Trouser", avatarController.gameObject, "Male");
            }
            if (defaultCharacterdata.shirtPreset != null && defaultCharacterdata.shirtPreset != "")
            {
                _ = StartCoroutine(Constants.downloadAddressableObject(defaultCharacterdata.shirtPreset, bodyType.Shirt, avatarBodyParts.gameObject, false));
            }
            else
            {
                avatarController.WearDefaultItem("Shirt", avatarController.gameObject, "Male");
            }
            if (defaultCharacterdata.shoespreset != null && defaultCharacterdata.shoespreset != "")
            {
                _ = StartCoroutine(Constants.downloadAddressableObject(defaultCharacterdata.shoespreset, bodyType.Shoes, avatarBodyParts.gameObject, false));
            }
            else
            {
                avatarController.WearDefaultItem("Shoes", avatarController.gameObject, "Male");
            }
            if (defaultCharacterdata.eyeColor != null && defaultCharacterdata.eyeColor != "")
            {
                await Constants.downloadAddressableTexture(defaultCharacterdata.eyeColor, bodyType.EyeColor, avatarBodyParts.gameObject);
            }
            else
            {
                avatarController.WearDefaultItem("Eyes", avatarController.gameObject, "Male");
            }
            if (defaultCharacterdata.skinColor != null)
            {
                avatarBodyParts.ApplyColor(ColorUtility.ToHtmlStringRGB(defaultCharacterdata.skinColor), bodyType.SkinColor);
                //  avatarBodyParts.ApplyColor(defaultCharacterdata.skinColor.ToString(), bodyType.SkinColor);

            }
            else
            {
                //avatarController.WearDefaultItem("Eyes", avatarController.gameObject, "Male");
            }
            if (defaultCharacterdata.lipsColor != null)
            {
                avatarBodyParts.ApplyColor(ColorUtility.ToHtmlStringRGB(defaultCharacterdata.lipsColor), bodyType.Lips);
            }
            else
            {
                //avatarController.WearDefaultItem("Eyes", avatarController.gameObject, "Male");
            }
            if (defaultCharacterdata.eyeBrowColor != null)
            {
                avatarBodyParts.ApplyColor(ColorUtility.ToHtmlStringRGB(defaultCharacterdata.eyeBrowColor), bodyType.EyebrowColor);
            }
            else
            {
                //    //avatarController.WearDefaultItem("Eyes", avatarController.gameObject, "Male");
            }
        }
    }
    public void ApplyChanges()
    {
        defaultCharacterdata = avatarBodyParts.currentCharacterData.Clone();
        SaveCharacterCustomization();
    }
    public async void ResetChanges()
    {
        switch (Constants.Instance.bodyType)
        {
            case bodyType.Face:
                //Constants.resetBlendShapes.Invoke();
                //avatarController.body.SetBlendShapeWeight(defaultCharacterdata.faceShape, 100);
                //avatarBodyParts.currentCharacterData.faceShape = defaultCharacterdata.faceShape;
                break;
            case bodyType.Hair:
                _ = StartCoroutine(Constants.downloadAddressableObject(defaultCharacterdata.hairPreset, bodyType.Hair, avatarBodyParts.gameObject, false));
                avatarBodyParts.currentCharacterData.hairPreset = defaultCharacterdata.hairPreset;
                break;
            case bodyType.Lips:
                //Constants.resetBlendShapes.Invoke();
                //avatarController.body.SetBlendShapeWeight(defaultCharacterdata.lipsShape, 100);
                //avatarBodyParts.currentCharacterData.lipsShape = defaultCharacterdata.lipsShape;
                break;
            case bodyType.Eyes:
                //Constants.resetBlendShapes.Invoke();
                //avatarController.body.SetBlendShapeWeight(defaultCharacterdata.eyeShape, 100);
                //avatarBodyParts.currentCharacterData.eyeShape = defaultCharacterdata.eyeShape;
                break;
            case bodyType.Nose:
                //Constants.resetBlendShapes.Invoke();
                //avatarController.body.SetBlendShapeWeight(defaultCharacterdata.noseShape, 100);
                //avatarBodyParts.currentCharacterData.noseShape = defaultCharacterdata.noseShape;
                break;
            case bodyType.SkinColor:
                avatarBodyParts.ApplyColor(ColorUtility.ToHtmlStringRGB(defaultCharacterdata.skinColor), bodyType.SkinColor);
                break;
            case bodyType.EyebrowColor:
                avatarBodyParts.ApplyColor(ColorUtility.ToHtmlStringRGB(defaultCharacterdata.eyeBrowColor), bodyType.EyebrowColor);
                break;
            case bodyType.EyeColor:
                if (Constants.downloadAddressableTexture != null)
                {
                    await Constants.downloadAddressableTexture(defaultCharacterdata.eyeColor, bodyType.EyeColor, avatarBodyParts.gameObject);
                }
                break;
            case bodyType.Shirt:
                _ = StartCoroutine(Constants.downloadAddressableObject(defaultCharacterdata.shirtPreset, bodyType.Shirt, avatarBodyParts.gameObject, false));
                avatarBodyParts.currentCharacterData.shirtPreset = defaultCharacterdata.shirtPreset;
                break;
            case bodyType.Trouser:
                _ = StartCoroutine(Constants.downloadAddressableObject(defaultCharacterdata.trouserPreset, bodyType.Trouser, avatarBodyParts.gameObject, false));
                avatarBodyParts.currentCharacterData.trouserPreset = defaultCharacterdata.trouserPreset;
                break;
            case bodyType.Shoes:
                _ = StartCoroutine(Constants.downloadAddressableObject(defaultCharacterdata.shoespreset, bodyType.Shoes, avatarBodyParts.gameObject, false));
                avatarBodyParts.currentCharacterData.shoespreset = defaultCharacterdata.shoespreset;
                break;
            case bodyType.Preset:
                break;
            case bodyType.Arms:
                //Constants.resetBlendShapes.Invoke();
                //avatarController.body.SetBlendShapeWeight((int)defaultCharacterdata.armShape, 100);
                //avatarBodyParts.currentCharacterData.armShape = defaultCharacterdata.armShape;
                break;
            case bodyType.Legs:
                //Constants.resetBlendShapes.Invoke();
                //avatarController.body.SetBlendShapeWeight((int)defaultCharacterdata.legShape, 100);
                //avatarBodyParts.currentCharacterData.legShape = defaultCharacterdata.legShape;
                break;
            case bodyType.Torso:
                //Constants.resetBlendShapes.Invoke();
                //avatarController.body.SetBlendShapeWeight((int)defaultCharacterdata.torsoShape, 100);
                //avatarBodyParts.currentCharacterData.torsoShape = defaultCharacterdata.torsoShape;
                break;
        }
        reset.interactable = false;
    }
}
