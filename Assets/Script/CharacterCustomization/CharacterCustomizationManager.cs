using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
public class CharacterCustomizationManager : MonoBehaviour
{
    public CharacterData defaultCharacterdata;
    public AvatarController avatarController;
    //public AvatarBodyParts avatarBodyParts;
    public int isGuest = 0;
    public Button save, reset;

    public Toggle maleToggle;
    public Toggle femaleToggle;
    public Button doneButton;

    private string selectedGender = "Male"; // Default gender selection

    private void OnEnable()
    {
       // save.onClick.AddListener(ApplyChanges);
       // reset.onClick.AddListener(ResetChanges);
    }
    private void OnDisable()
    {
       // save.onClick.RemoveListener(ApplyChanges);
       // reset.onClick.RemoveListener(ResetChanges);
    }
    private void Awake()
    {
       // GameManager.instance.InitPlayer();
       GameManager.instance.SpawnPrefab(0);
    }
    IEnumerator Start()
    {
        maleToggle.onValueChanged.AddListener(delegate { OnToggleChanged("Male", maleToggle.isOn); });
        femaleToggle.onValueChanged.AddListener(delegate { OnToggleChanged("Female", femaleToggle.isOn); });

        // Add listener to Done button
        doneButton.onClick.AddListener(OnDoneButtonPressed);
        yield return new WaitForSeconds(1);
        // LoadCharacterCustomization();
    }

    public void LoadCharacterCustomization()
    {
        if (File.Exists(Application.persistentDataPath + "/characterCustom.json"))
        {
            string json = File.ReadAllText(Application.persistentDataPath + "/characterCustom.json");
            defaultCharacterdata = JsonUtility.FromJson<CharacterData>(json);
            avatarController.currentCharacterData = defaultCharacterdata.Clone();
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

        avatarController.currentCharacterData = defaultCharacterdata.Clone();
        if (Constants.downloadAddressableObject != null)
        {
            if (defaultCharacterdata.hairPreset != null && defaultCharacterdata.hairPreset != string.Empty)
            {
                _ = StartCoroutine(Constants.downloadAddressableObject(defaultCharacterdata.hairPreset, BodyType.Hair, avatarController.gameObject, true));
            }
            else
            {
                avatarController.WearDefaultItem(BodyPartsType.Hair, avatarController.gameObject, GenderType.male);
            }
            if (defaultCharacterdata.eyeColor != null && defaultCharacterdata.eyeColor != string.Empty)
            {
                await Constants.downloadAddressableTexture(defaultCharacterdata.eyeColor, BodyType.EyeColor, avatarController.gameObject);
            }
            else
            {
                avatarController.WearDefaultItem(BodyPartsType.Eyes, avatarController.gameObject, GenderType.male);
            }
            if (defaultCharacterdata.skinColor != null)
            {
                //  avatarBodyParts.ApplyColor(ColorUtility.ToHtmlStringRGB(defaultCharacterdata.skinColor), BodyType.SkinColor);
                //  avatarBodyParts.ApplyColor(defaultCharacterdata.skinColor.ToString(), bodyType.SkinColor);

            }
            else
            {
                //avatarController.WearDefaultItem("Eyes", avatarController.gameObject, "Male");
            }
            //if (defaultCharacterdata.lipsColor != null)
            //{
            //    avatarBodyParts.ApplyColor(ColorUtility.ToHtmlStringRGB(defaultCharacterdata.lipsColor), BodyType.Lips);
            //}
            //else
            //{
            //    //avatarController.WearDefaultItem("Eyes", avatarController.gameObject, "Male");
            //}
            if (defaultCharacterdata.eyeBrowShape != null && defaultCharacterdata.eyeBrowShape != string.Empty)
            {
                await Constants.downloadAddressableTexture(defaultCharacterdata.eyeBrowShape, BodyType.Eyebrow, avatarController.gameObject);
            }
            else
            {
                //    //avatarController.WearDefaultItem("Eyes", avatarController.gameObject, "Male");
            }
        }
    }
    public void ApplyChanges()
    {
        defaultCharacterdata = avatarController.currentCharacterData.Clone();
        // SaveCharacterCustomization();
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
                _ = StartCoroutine(Constants.downloadAddressableObject(defaultCharacterdata.hairPreset, BodyType.Hair, avatarController.gameObject, false));
                avatarController.currentCharacterData.hairPreset = defaultCharacterdata.hairPreset;
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
                //  avatarBodyParts.ApplyColor(ColorUtility.ToHtmlStringRGB(defaultCharacterdata.skinColor), BodyType.SkinColor);
                break;
            case BodyType.Eyebrow:
                if (Constants.downloadAddressableTexture != null)
                {
                    await Constants.downloadAddressableTexture(defaultCharacterdata.eyeBrowShape, BodyType.Eyebrow, avatarController.gameObject);
                }
                break;
            case BodyType.EyeColor:
                if (Constants.downloadAddressableTexture != null)
                {
                    await Constants.downloadAddressableTexture(defaultCharacterdata.eyeColor, BodyType.EyeColor, avatarController.gameObject);
                }
                break;
            case BodyType.Body:
                _ = StartCoroutine(Constants.downloadAddressableObject(defaultCharacterdata.torsoShape, BodyType.Body, avatarController.gameObject, false));
                avatarController.currentCharacterData.clothPreset = defaultCharacterdata.clothPreset;
                break;
                //case BodyType.Arms:
                //    //Constants.resetBlendShapes.Invoke();
                //    //avatarController.body.SetBlendShapeWeight((int)defaultCharacterdata.armShape, 100);
                //    //avatarBodyParts.currentCharacterData.armShape = defaultCharacterdata.armShape;
                //    break;
        }
        reset.interactable = false;
    }
    void OnToggleChanged(string gender, bool isOn)
    {
        if (isOn)
        {
            selectedGender = gender;
        }
    }

    void OnDoneButtonPressed()
    {
        // Destroy old character if exists and create a new one
        if (GameManager.instance._player != null)
        {
            Destroy(GameManager.instance._player);
        }

        InstantiateCharacter(selectedGender);
        Debug.Log("Confirmed Gender: " + selectedGender);
    }

    void InstantiateCharacter(string gender)
    {
        GameObject selectedPrefab = (gender == "Male") ? GameManager.instance.allPlayer[0] : GameManager.instance.allPlayer[1];
        GameManager.instance._player = Instantiate(selectedPrefab, Vector3.zero, Quaternion.identity);
        defaultCharacterdata.gender = gender;
    }
}