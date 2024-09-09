using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterCustomization : MonoBehaviour
{
    public List<int> blendShapes;
    public CharacterCustomizationManager characterCustomizationManager;
    void Start()
    {
        blendShapes = new List<int>();
    }

    public void ChangeFaceBlendShapes(string _index) 
    {
        ResetBlendShapes();
        // characterMesh.SetBlendShapeWeight(index, 100);
       // characterCustomizationManager.avatarController.body.SetBlendShapeWeight(int.Parse(_index), 100);
        characterCustomizationManager.avatarBodyParts.currentCharacterData.faceShape = int.Parse(_index);

    }
    public void ChangeLipsBlendShapes(string index)
    {
        ResetBlendShapes();
        // characterMesh.SetBlendShapeWeight(index, 100);
        characterCustomizationManager.avatarBodyParts.currentCharacterData.lipsShape = int.Parse(index);

    }
    public void ChangeEyeBlendShapes(string index)
    {
        ResetBlendShapes();
        // characterMesh.SetBlendShapeWeight(index, 100);
        characterCustomizationManager.avatarBodyParts.currentCharacterData.eyeShape = int.Parse(index);

    }
    public void ChangeNoseBlendShapes(string index)
    {
        ResetBlendShapes();
        // characterMesh.SetBlendShapeWeight(index, 100);
        characterCustomizationManager.avatarBodyParts.currentCharacterData.noseShape = int.Parse(index);

    }
    public void ChangeArmsBlendShapes(float index)
    {
        //ResetBlendShapes();
        // characterMesh.SetBlendShapeWeight(index, 100);
        characterCustomizationManager.avatarBodyParts.currentCharacterData.armShape = index;

    }
    public void ChangeLegsBlendShapes(float index)
    {
        //ResetBlendShapes();
        // characterMesh.SetBlendShapeWeight(index, 100);
        characterCustomizationManager.avatarBodyParts.currentCharacterData.legShape = index;

    }
    public void ChangeTorsoBlendShapes(float index)
    {
        //ResetBlendShapes();
        // characterMesh.SetBlendShapeWeight(index, 100);
        characterCustomizationManager.avatarBodyParts.currentCharacterData.torsoShape = index;

    }
    public void downloadPresetObject(string key, bodyType type)
    {   
        if (Constants.downloadAddressableObject != null)
        {
            StartCoroutine(Constants.downloadAddressableObject(key, type, characterCustomizationManager.avatarBodyParts.gameObject, false));
        }
    }
    public void ChangeSkinColor(string color, bodyType type)
    {
        characterCustomizationManager.avatarBodyParts.ApplyColor(color, type);
    }
    public void ChangeLipsColor(string color, bodyType type)
    {
        characterCustomizationManager.avatarBodyParts.ApplyColor(color, type);
    }
    public void downloadPresetTexture(string key, bodyType type)
    {
        if (Constants.downloadAddressableTexture != null)
        {
            StartCoroutine(Constants.downloadAddressableTexture(key, type, characterCustomizationManager.avatarBodyParts.gameObject));
        }

    }
    private void ResetBlendShapes()
    {
        for (int i = 0; i < blendShapes.Count; i++) 
        {
            characterCustomizationManager.avatarController.body.SetBlendShapeWeight(i, 0);
        }
    }
}
