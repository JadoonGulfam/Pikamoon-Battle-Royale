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
        characterCustomizationManager.characterCustom.faceShape = int.Parse(index);

    }
    public void ChangeLipsBlendShapes(string index)
    {
        ResetBlendShapes();
        // characterMesh.SetBlendShapeWeight(index, 100);
        characterCustomizationManager.characterCustom.lipsShape = int.Parse(index);

    }
    public void ChangeEyeBlendShapes(string index)
    {
        ResetBlendShapes();
        // characterMesh.SetBlendShapeWeight(index, 100);
        characterCustomizationManager.characterCustom.eyeShape = int.Parse(index);

    }
    public void ChangeNoseBlendShapes(string index)
    {
        ResetBlendShapes();
        // characterMesh.SetBlendShapeWeight(index, 100);
        characterCustomizationManager.characterCustom.noseShape = int.Parse(index);

    }
    public void ChangeArmsBlendShapes(float index)
    {
        //ResetBlendShapes();
        // characterMesh.SetBlendShapeWeight(index, 100);
        characterCustomizationManager.characterCustom.armShape = index;

    }
    public void ChangeLegsBlendShapes(float index)
    {
        //ResetBlendShapes();
        // characterMesh.SetBlendShapeWeight(index, 100);
        characterCustomizationManager.characterCustom.legShape = index;

    }
    public void ChangeTorsoBlendShapes(float index)
    {
        //ResetBlendShapes();
        // characterMesh.SetBlendShapeWeight(index, 100);
        characterCustomizationManager.characterCustom.torsoShape = index;

    }
    public void downloadPresetObject(string key, bodyType type)
    {
        // characterMesh.SetBlendShapeWeight(index, 100);
        characterCustomizationManager.DownloadPresetAddressableObject(key, type);      

    }
    public void ChangeSkinColor(string color, bodyType type)
    {
        // characterMesh.SetBlendShapeWeight(index, 100);
        characterCustomizationManager.ApplySkinColor(color, type);

    }
    //public void ChangeHairColor(string color, bodyType type)
    //{
    //    // characterMesh.SetBlendShapeWeight(index, 100);
    //    characterCustomizationManager.ApplyHairColor(color, type);

    //}
    public void ChangeLipsColor(string color, bodyType type)
    {
        // characterMesh.SetBlendShapeWeight(index, 100);
        characterCustomizationManager.ApplyLipsColor(color, type);

    }
    public void downloadPresetTexture(string key, bodyType type)
    {
        characterCustomizationManager.DownloadPresetAddressableTexture(key, type);       

    }
    private void ResetBlendShapes()
    {
        for (int i = 0; i < blendShapes.Count; i++) 
        {
            characterMesh.SetBlendShapeWeight(i, 0);
        }
    }
}
