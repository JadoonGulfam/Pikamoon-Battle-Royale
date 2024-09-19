using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterCustomization : MonoBehaviour
{
    public List<int> blendShapes;
    public CharacterCustomizationManager characterCustomizationManager;
    private void OnEnable()
    {
        Constants.resetBlendShapes += ResetBlendShapes;
    }
    private void OnDisable()
    {
        Constants.resetBlendShapes -= ResetBlendShapes;
    }
    void Start()
    {
        blendShapes = new List<int>();
    }

    public void ChangeFaceBlendShapes(string _index)
    {
        ResetBlendShapes();
        // characterMesh.SetBlendShapeWeight(index, 100);
        //characterCustomizationManager.avatarController.body.SetBlendShapeWeight(int.Parse(_index), 100);
        characterCustomizationManager.avatarBodyParts.currentCharacterData.faceShape = int.Parse(_index);
        characterCustomizationManager.save.interactable = true;
        characterCustomizationManager.reset.interactable = true;

    }
    public void ChangeLipsBlendShapes(string index)
    {
        ResetBlendShapes();
        // characterMesh.SetBlendShapeWeight(index, 100);
        characterCustomizationManager.avatarBodyParts.currentCharacterData.lipsShape = int.Parse(index);
        characterCustomizationManager.save.interactable = true;
        characterCustomizationManager.reset.interactable = true;

    }
    public void ChangeEyeBlendShapes(string index)
    {
        ResetBlendShapes();
        // characterMesh.SetBlendShapeWeight(index, 100);
        characterCustomizationManager.avatarBodyParts.currentCharacterData.eyeShape = int.Parse(index);
        characterCustomizationManager.save.interactable = true;
        characterCustomizationManager.reset.interactable = true;

    }
    public void ChangeNoseBlendShapes(string index)
    {
        ResetBlendShapes();
        // characterMesh.SetBlendShapeWeight(index, 100);
        characterCustomizationManager.avatarBodyParts.currentCharacterData.noseShape = int.Parse(index);
        characterCustomizationManager.save.interactable = true;
        characterCustomizationManager.reset.interactable = true;

    }
    public void ChangeArmsBlendShapes(float index)
    {
        //ResetBlendShapes();
        // characterMesh.SetBlendShapeWeight(index, 100);
        characterCustomizationManager.avatarBodyParts.currentCharacterData.armShape = index;
        characterCustomizationManager.save.interactable = true;
        characterCustomizationManager.reset.interactable = true;

    }
    public void ChangeLegsBlendShapes(float index)
    {
        //ResetBlendShapes();
        // characterMesh.SetBlendShapeWeight(index, 100);
        characterCustomizationManager.avatarBodyParts.currentCharacterData.legShape = index;
        characterCustomizationManager.save.interactable = true;
        characterCustomizationManager.reset.interactable = true;

    }
    public void ChangeTorsoBlendShapes(float index)
    {
        //ResetBlendShapes();
        // characterMesh.SetBlendShapeWeight(index, 100);
        characterCustomizationManager.avatarBodyParts.currentCharacterData.torsoShape = index;
        characterCustomizationManager.save.interactable = true;
        characterCustomizationManager.reset.interactable = true;

    }
    public void downloadPresetObject(string key, bodyType type)
    {
        if (Constants.downloadAddressableObject != null)
        {
            _ = StartCoroutine(Constants.downloadAddressableObject(key, type, characterCustomizationManager.avatarBodyParts.gameObject, false));
        }
        characterCustomizationManager.save.interactable = true;
        characterCustomizationManager.reset.interactable = true;
    }
    public void ChangeSkinColor(string color, bodyType type)
    {
        characterCustomizationManager.avatarBodyParts.ApplyColor(color, type);
        characterCustomizationManager.save.interactable = true;
        characterCustomizationManager.reset.interactable = true;
    }
    public void ChangeLipsColor(string color, bodyType type)
    {
        characterCustomizationManager.avatarBodyParts.ApplyColor(color, type);
        characterCustomizationManager.save.interactable = true;
        characterCustomizationManager.reset.interactable = true;
    }
    public async void downloadPresetTexture(string key, bodyType type)
    {
        if (Constants.downloadAddressableTexture != null)
        {
            await Constants.downloadAddressableTexture(key, type, characterCustomizationManager.avatarBodyParts.gameObject);
        }
        characterCustomizationManager.save.interactable = true;
        characterCustomizationManager.reset.interactable = true;

    }
    private void ResetBlendShapes()
    {
        for (int i = 0; i < blendShapes.Count; i++)
        {
            characterCustomizationManager.avatarController.body.SetBlendShapeWeight(i, 0);
        }
    }
}
