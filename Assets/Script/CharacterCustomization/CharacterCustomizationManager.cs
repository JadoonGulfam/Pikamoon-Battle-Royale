using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
namespace CharacterCustomization
{
    public class CharacterCustomizationManager : MonoBehaviour
    {
        public CharacterData defaultCharacterdata;      
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
                else { }
                  //  avatarController.SetAvatarClothDefault(avatarController.gameObject, GenderType.male);
            }
            else
            {
                Debug.Log("No character customization file found at " + Application.persistentDataPath + "/characterCustom.json");
                //avatarController.SetAvatarClothDefault(avatarController.gameObject, GenderType.male);
            }
        }
        private async void ApplyCharacterCustomization()
        {

            avatarBodyParts.currentCharacterData = defaultCharacterdata.Clone();
            if (Constants.downloadAddressableObject != null)
            {
                if (defaultCharacterdata.hairPreset != null && defaultCharacterdata.hairPreset != string.Empty)
                {
                    _ = StartCoroutine(Constants.downloadAddressableObject(defaultCharacterdata.hairPreset, BodyType.Hair, avatarBodyParts.gameObject, true));
                }
                else
                {
                    avatarController.WearDefaultItem(BodyPartsType.Hair, avatarController.gameObject, GenderType.male);
                }
                if (defaultCharacterdata.trouserPreset != null && defaultCharacterdata.trouserPreset != string.Empty)
                {
                    _ = StartCoroutine(Constants.downloadAddressableObject(defaultCharacterdata.trouserPreset, BodyType.Trouser, avatarBodyParts.gameObject, false));
                }
                else
                {
                    avatarController.WearDefaultItem(BodyPartsType.Hips, avatarController.gameObject, GenderType.male);
                }
                if (defaultCharacterdata.shirtPreset != null && defaultCharacterdata.shirtPreset != string.Empty)
                {
                    _ = StartCoroutine(Constants.downloadAddressableObject(defaultCharacterdata.shirtPreset, BodyType.Shirt, avatarBodyParts.gameObject, false));
                }
                else
                {
                    avatarController.WearDefaultItem(BodyPartsType.Chest, avatarController.gameObject, GenderType.male);
                }
                if (defaultCharacterdata.shoesPreset != null && defaultCharacterdata.shoesPreset != string.Empty)
                {
                    _ = StartCoroutine(Constants.downloadAddressableObject(defaultCharacterdata.shoesPreset, BodyType.Shoes, avatarBodyParts.gameObject, false));
                }
                else
                {
                    avatarController.WearDefaultItem(BodyPartsType.Feet, avatarController.gameObject, GenderType.male);
                }
                if (defaultCharacterdata.eyeColor != null && defaultCharacterdata.eyeColor != string.Empty)
                {
                    await Constants.downloadAddressableTexture(defaultCharacterdata.eyeColor, BodyType.EyeColor, avatarBodyParts.gameObject);
                }
                else
                {
                    avatarController.WearDefaultItem(BodyPartsType.Eyes, avatarController.gameObject, GenderType.male);
                }
                if (defaultCharacterdata.skinColor != null)
                {
                    avatarBodyParts.ApplyColor(ColorUtility.ToHtmlStringRGB(defaultCharacterdata.skinColor), BodyType.SkinColor);
                    //  avatarBodyParts.ApplyColor(defaultCharacterdata.skinColor.ToString(), bodyType.SkinColor);

                }
                else
                {
                    //avatarController.WearDefaultItem("Eyes", avatarController.gameObject, "Male");
                }
                if (defaultCharacterdata.lipsColor != null)
                {
                    avatarBodyParts.ApplyColor(ColorUtility.ToHtmlStringRGB(defaultCharacterdata.lipsColor), BodyType.Lips);
                }
                else
                {
                    //avatarController.WearDefaultItem("Eyes", avatarController.gameObject, "Male");
                }
                if (defaultCharacterdata.eyeBrowShape != null && defaultCharacterdata.eyeBrowShape != string.Empty)
                {
                    await Constants.downloadAddressableTexture(defaultCharacterdata.eyeBrowShape, BodyType.Eyebrow, avatarBodyParts.gameObject);
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
                case BodyType.Face:
                    //Constants.resetBlendShapes.Invoke();
                    //avatarController.body.SetBlendShapeWeight(defaultCharacterdata.faceShape, 100);
                    //avatarBodyParts.currentCharacterData.faceShape = defaultCharacterdata.faceShape;
                    break;
                case BodyType.Hair:
                    _ = StartCoroutine(Constants.downloadAddressableObject(defaultCharacterdata.hairPreset, BodyType.Hair, avatarBodyParts.gameObject, false));
                    avatarBodyParts.currentCharacterData.hairPreset = defaultCharacterdata.hairPreset;
                    break;
                case BodyType.Lips:
                    //Constants.resetBlendShapes.Invoke();
                    //avatarController.body.SetBlendShapeWeight(defaultCharacterdata.lipsShape, 100);
                    //avatarBodyParts.currentCharacterData.lipsShape = defaultCharacterdata.lipsShape;
                    break;
                case BodyType.Eyes:
                    //Constants.resetBlendShapes.Invoke();
                    //avatarController.body.SetBlendShapeWeight(defaultCharacterdata.eyeShape, 100);
                    //avatarBodyParts.currentCharacterData.eyeShape = defaultCharacterdata.eyeShape;
                    break;
                case BodyType.Nose:
                    //Constants.resetBlendShapes.Invoke();
                    //avatarController.body.SetBlendShapeWeight(defaultCharacterdata.noseShape, 100);
                    //avatarBodyParts.currentCharacterData.noseShape = defaultCharacterdata.noseShape;
                    break;
                case BodyType.SkinColor:
                    avatarBodyParts.ApplyColor(ColorUtility.ToHtmlStringRGB(defaultCharacterdata.skinColor), BodyType.SkinColor);
                    break;
                case BodyType.Eyebrow:
                    if (Constants.downloadAddressableTexture != null)
                    {
                        await Constants.downloadAddressableTexture(defaultCharacterdata.eyeBrowShape, BodyType.Eyebrow, avatarBodyParts.gameObject);
                    }
                    break;
                case BodyType.EyeColor:
                    if (Constants.downloadAddressableTexture != null)
                    {
                        await Constants.downloadAddressableTexture(defaultCharacterdata.eyeColor, BodyType.EyeColor, avatarBodyParts.gameObject);
                    }
                    break;
                case BodyType.Shirt:
                    _ = StartCoroutine(Constants.downloadAddressableObject(defaultCharacterdata.shirtPreset, BodyType.Shirt, avatarBodyParts.gameObject, false));
                    avatarBodyParts.currentCharacterData.shirtPreset = defaultCharacterdata.shirtPreset;
                    break;
                case BodyType.Trouser:
                    _ = StartCoroutine(Constants.downloadAddressableObject(defaultCharacterdata.trouserPreset, BodyType.Trouser, avatarBodyParts.gameObject, false));
                    avatarBodyParts.currentCharacterData.trouserPreset = defaultCharacterdata.trouserPreset;
                    break;
                case BodyType.Shoes:
                    _ = StartCoroutine(Constants.downloadAddressableObject(defaultCharacterdata.shoesPreset, BodyType.Shoes, avatarBodyParts.gameObject, false));
                    avatarBodyParts.currentCharacterData.shoesPreset = defaultCharacterdata.shoesPreset;
                    break;
                case BodyType.Preset:
                    break;
                case BodyType.Arms:
                    //Constants.resetBlendShapes.Invoke();
                    //avatarController.body.SetBlendShapeWeight((int)defaultCharacterdata.armShape, 100);
                    //avatarBodyParts.currentCharacterData.armShape = defaultCharacterdata.armShape;
                    break;
                case BodyType.Legs:
                    //Constants.resetBlendShapes.Invoke();
                    //avatarController.body.SetBlendShapeWeight((int)defaultCharacterdata.legShape, 100);
                    //avatarBodyParts.currentCharacterData.legShape = defaultCharacterdata.legShape;
                    break;
                case BodyType.Torso:
                    //Constants.resetBlendShapes.Invoke();
                    //avatarController.body.SetBlendShapeWeight((int)defaultCharacterdata.torsoShape, 100);
                    //avatarBodyParts.currentCharacterData.torsoShape = defaultCharacterdata.torsoShape;
                    break;
            }
            reset.interactable = false;
        }
    }
}