using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class CharacterCustomizationManager : MonoBehaviour
{
    public CharacterData defaultCharacterdata;
    public GameObject loader;
    public AvatarController avatarController;
    public AvatarBodyParts avatarBodyParts;
    public int isGuest = 0;
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
    private void ApplyCharacterCustomization()
    {

        avatarBodyParts.currentCharacterData = defaultCharacterdata.Clone();
        if (Constants.downloadAddressableObject != null)
        {
            if (defaultCharacterdata.hairPreset != null && defaultCharacterdata.hairPreset != "")
            {
                StartCoroutine(Constants.downloadAddressableObject(defaultCharacterdata.hairPreset, bodyType.Hair, avatarBodyParts.gameObject, true));
            }
            else
            {
                avatarController.WearDefaultItem("Hair", avatarController.gameObject, "Male");
            }
            if (defaultCharacterdata.trouserPreset != null && defaultCharacterdata.trouserPreset != "")
            {
                StartCoroutine(Constants.downloadAddressableObject(defaultCharacterdata.trouserPreset, bodyType.Trouser, avatarBodyParts.gameObject, false));
            }
            else
            {
                avatarController.WearDefaultItem("Trouser", avatarController.gameObject, "Male");
            }
            if (defaultCharacterdata.shirtPreset != null && defaultCharacterdata.shirtPreset != "")
            {
                StartCoroutine(Constants.downloadAddressableObject(defaultCharacterdata.shirtPreset, bodyType.Shirt, avatarBodyParts.gameObject, false));
            }
            else
            {
                avatarController.WearDefaultItem("Shirt", avatarController.gameObject, "Male");
            }
            if (defaultCharacterdata.shoespreset != null && defaultCharacterdata.shoespreset != "")
            {
                StartCoroutine(Constants.downloadAddressableObject(defaultCharacterdata.shoespreset, bodyType.Shoes, avatarBodyParts.gameObject, false));
            }
            else
            {
                avatarController.WearDefaultItem("Shoes", avatarController.gameObject, "Male");
            }
            if (defaultCharacterdata.eyeColor != null && defaultCharacterdata.eyeColor != "")
            {
                StartCoroutine(Constants.downloadAddressableTexture(defaultCharacterdata.eyeColor, bodyType.EyeColor, avatarBodyParts.gameObject));
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
            //if (defaultCharacterdata.hairColor != null)
            //{
            //    avatarBodyParts.ApplyColor(ColorUtility.ToHtmlStringRGB(defaultCharacterdata.hairColor), bodyType.Hair);
            //}
            //else
            //{
            //    //avatarController.WearDefaultItem("Eyes", avatarController.gameObject, "Male");
            //}
        }
    }
    public void ApplyChanges()
    {
        defaultCharacterdata = avatarBodyParts.currentCharacterData.Clone();
        SaveCharacterCustomization();
    }
    public void ResetChanges()
    {
        switch (Constants.Instance.bodyType)
        {
            case bodyType.Face:
                break;
            case bodyType.Hair:
                StartCoroutine(Constants.downloadAddressableObject(defaultCharacterdata.hairPreset, bodyType.Hair, avatarBodyParts.gameObject, false));
                avatarBodyParts.currentCharacterData.hairPreset = defaultCharacterdata.hairPreset;
                break;
            case bodyType.Lips:
                break;
            case bodyType.Eyes:
                break;
            case bodyType.Nose:
                break;
            case bodyType.SkinColor:
                break;
            case bodyType.EyeColor:
                break;
            case bodyType.Shirt:
                StartCoroutine(Constants.downloadAddressableObject(defaultCharacterdata.shirtPreset, bodyType.Shirt, avatarBodyParts.gameObject, false));
                avatarBodyParts.currentCharacterData.shirtPreset = defaultCharacterdata.shirtPreset;
                break;
            case bodyType.Trouser:
                StartCoroutine(Constants.downloadAddressableObject(defaultCharacterdata.trouserPreset, bodyType.Trouser, avatarBodyParts.gameObject, false));
                avatarBodyParts.currentCharacterData.trouserPreset = defaultCharacterdata.trouserPreset;
                break;
            case bodyType.Shoes:
                StartCoroutine(Constants.downloadAddressableObject(defaultCharacterdata.shoespreset, bodyType.Shoes, avatarBodyParts.gameObject, false));
                avatarBodyParts.currentCharacterData.shoespreset = defaultCharacterdata.shoespreset;
                break;
            case bodyType.Preset:
                break;
            case bodyType.Arms:
                break;
            case bodyType.Legs:
                break;
            case bodyType.Torso:
                break;
        }
        //avatarBodyParts.ApplyEyeTexture(avatarController.defaultClothDatabase.maleAvatarDefaultCostume.DefaultEyes, "");
        //avatarBodyParts.ApplyColor(avatarController.defaultClothDatabase.maleAvatarDefaultCostume.DefaultLipsColor.ToString(), bodyType.Lips);
        //avatarBodyParts.ApplyColor(avatarController.defaultClothDatabase.maleAvatarDefaultCostume.DefaultSkinColor.ToString(), bodyType.SkinColor);
        //avatarBodyParts.ApplyHairPreset(avatarController.defaultClothDatabase.maleAvatarDefaultCostume.DefaultHair, "", bodyType.Hair.ToString(), false);
        //avatarBodyParts.ApplyShirtPreset(avatarController.defaultClothDatabase.maleAvatarDefaultCostume.DefaultShirt, "", bodyType.Shirt.ToString());
        //avatarBodyParts.ApplyTrouserPreset(avatarController.defaultClothDatabase.maleAvatarDefaultCostume.DefaultPent, "", bodyType.Trouser.ToString());
        //avatarBodyParts.ApplyShoesPreset(avatarController.defaultClothDatabase.maleAvatarDefaultCostume.DefaultShoes, "", bodyType.Shoes.ToString());
    }
}
