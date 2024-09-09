using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterCustomization : MonoBehaviour
{
    public List<int> blendShapes;
    SkinnedMeshRenderer characterMesh;
    public CharacterCustomizationManager characterCustomizationManager;
    void Start()
    {
        blendShapes = new List<int>();
    }

    public void ChangeFaceBlendShapes(string index) 
    {
        ResetBlendShapes();
        // characterMesh.SetBlendShapeWeight(index, 100);
        characterCustomizationManager.avatarBodyParts.currentCharacterData.faceShape = int.Parse(index);

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
        // characterCustomizationManager.DownloadPresetAddressableObject(key, type);      
        if (Constants.downloadAddressableObject != null)
        {
            StartCoroutine(Constants.downloadAddressableObject(key, type, characterCustomizationManager.avatarBodyParts.gameObject));
        }
    }
    public void ChangeSkinColor(string color, bodyType type)
    {
        // characterMesh.SetBlendShapeWeight(index, 100);
        characterCustomizationManager.avatarBodyParts.ApplyColor(color, type);

    }
    public void ChangeLipsColor(string color, bodyType type)
    {
        // characterMesh.SetBlendShapeWeight(index, 100);
        characterCustomizationManager.avatarBodyParts.ApplyColor(color, type);

    }
    public void downloadPresetTexture(string key, bodyType type)
    {
       // characterCustomizationManager.DownloadPresetAddressableTexture(key, type);
        if (Constants.downloadAddressableTexture != null)
        {
            StartCoroutine(Constants.downloadAddressableTexture(key, type, characterCustomizationManager.avatarBodyParts.gameObject));
        }

    }
    private void ResetBlendShapes()
    {
        for (int i = 0; i < blendShapes.Count; i++) 
        {
            characterMesh.SetBlendShapeWeight(i, 0);
        }
    }
}
