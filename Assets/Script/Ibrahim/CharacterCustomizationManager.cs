using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterCustomizationManager : MonoBehaviour
{
    public CharacterData defaultCharacterdata;
    public GameObject loader;
    public AvatarController avatarController;
    public AvatarBodyParts avatarBodyParts;
    void Start()
    {
        PlayerPrefs.SetInt("Guest", 1);
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
            int isGuest = PlayerPrefs.GetInt("Guest", 0) == 1 ? 1 : 0;
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
                StartCoroutine(Constants.downloadAddressableObject(defaultCharacterdata.hairPreset, bodyType.Hair, avatarBodyParts.gameObject));
            }
            else
            {
                avatarController.WearDefaultItem("Hair", avatarController.gameObject, "Male");
            }
            if (defaultCharacterdata.trouserPreset != null && defaultCharacterdata.trouserPreset != "")
            {
                StartCoroutine(Constants.downloadAddressableObject(defaultCharacterdata.trouserPreset, bodyType.Trouser, avatarBodyParts.gameObject));
            }
            else
            {
                avatarController.WearDefaultItem("Trouser", avatarController.gameObject, "Male");
            }
            if (defaultCharacterdata.shirtPreset != null && defaultCharacterdata.shirtPreset != "")
            {
                StartCoroutine(Constants.downloadAddressableObject(defaultCharacterdata.shirtPreset, bodyType.Shirt, avatarBodyParts.gameObject));
            }
            else
            {
                avatarController.WearDefaultItem("Shirt", avatarController.gameObject, "Male");
            }
            if (defaultCharacterdata.shoespreset != null && defaultCharacterdata.shoespreset != "")
            {
                StartCoroutine(Constants.downloadAddressableObject(defaultCharacterdata.shoespreset, bodyType.Shoes, avatarBodyParts.gameObject));
            }
            else
            {
                avatarController.WearDefaultItem("Shoes", avatarController.gameObject, "Male");
            }
        }
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
    public void ApplyChanges()
    {
        defaultCharacterdata = avatarBodyParts.currentCharacterData.Clone();
        SaveCharacterCustomization();
    }
    public void ResetChanges()
    {
        // currentCharacterData =  defaultCharacterdata;
        avatarBodyParts.ApplyEyeTexture(avatarController.defaultClothDatabase.maleAvatarDefaultCostume.DefaultEyes, "");
        avatarBodyParts.ApplyColor(avatarController.defaultClothDatabase.maleAvatarDefaultCostume.DefaultLipsColor.ToString(), bodyType.Lips);
        avatarBodyParts.ApplyColor(avatarController.defaultClothDatabase.maleAvatarDefaultCostume.DefaultSkinColor.ToString(), bodyType.SkinColor);
        avatarBodyParts.ApplyHairPreset(avatarController.defaultClothDatabase.maleAvatarDefaultCostume.DefaultHair, "", bodyType.Hair.ToString());
        avatarBodyParts.ApplyShirtPreset(avatarController.defaultClothDatabase.maleAvatarDefaultCostume.DefaultShirt, "", bodyType.Shirt.ToString());
        avatarBodyParts.ApplyTrouserPreset(avatarController.defaultClothDatabase.maleAvatarDefaultCostume.DefaultPent, "", bodyType.Trouser.ToString());
        avatarBodyParts.ApplyShoesPreset(avatarController.defaultClothDatabase.maleAvatarDefaultCostume.DefaultShoes, "", bodyType.Shoes.ToString());
    }
}
