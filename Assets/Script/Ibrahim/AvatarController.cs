using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Analytics;
using UnityEngine.Rendering;

public class AvatarController : MonoBehaviour
{
    public DefaultClothDatabase defaultClothDatabase;
    public Stitcher stitcher;
    public SkinnedMeshRenderer body;
    public Material eye, eyeBrow;
    public GameObject wornHair, wornPant, wornShirt, wornShoes;
    void Start()
    {
        stitcher = new Stitcher();      
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void SetAvatarClothDefault(GameObject applyOn, string _gender)
    {
        WearDefaultItem("Legs", applyOn.gameObject, _gender);
        WearDefaultItem("Chest", applyOn.gameObject,_gender);
        WearDefaultItem("Feet", applyOn.gameObject, _gender);
        WearDefaultItem("Hair", applyOn.gameObject, _gender);
        SetDefaultTexture();
    }
    public void WearDefaultItem(string type, GameObject applyOn, string gender)
    {      
        if (gender == "Male") // if avatar is Male
        {
            switch (type)
            {
                case "Legs":
                    if (defaultClothDatabase.maleAvatarDefaultCostume.DefaultPent != null)
                        StichItem(-1, defaultClothDatabase.maleAvatarDefaultCostume.DefaultPent, type, applyOn);
                    break;
                case "Chest":
                    if (defaultClothDatabase.maleAvatarDefaultCostume.DefaultShirt != null)
                        StichItem(-1, defaultClothDatabase.maleAvatarDefaultCostume.DefaultShirt, type, applyOn);
                    break;
                case "Feet":
                    if (defaultClothDatabase.maleAvatarDefaultCostume.DefaultShoes != null)
                        StichItem(-1, defaultClothDatabase.maleAvatarDefaultCostume.DefaultShoes, type, applyOn);
                    break;
                case "Hair":
                    if (defaultClothDatabase.maleAvatarDefaultCostume.DefaultHair != null)
                        StichItem(-1, defaultClothDatabase.maleAvatarDefaultCostume.DefaultHair, type, applyOn);
                    break;
                default:
                    break;
            }
        }
    }
    public void StichItem(int itemId, GameObject item, string type, GameObject applyOn, bool applyHairColor = true)
    {

        UnStichItem(type);
     
        item = this.stitcher.Stitch(item, applyOn);
        switch (type)
        {
            case "Chest":
                wornShirt = item;
               // wornShirt.GetComponent<SkinnedMeshRenderer>().updateWhenOffscreen = true;
                break;
            case "Legs":
                wornPant = item;
               // wornPant.GetComponent<SkinnedMeshRenderer>().updateWhenOffscreen = true;
                break;
            case "Hair":
                wornHair = item;
                break;
            case "Feet":
                wornShoes = item;              
               // wornShoes.GetComponent<SkinnedMeshRenderer>().updateWhenOffscreen = true;
                break;
        }
       
    }

    public void UnStichItem(string type)
    {
        switch (type)
        {
            case "Chest":
                Destroy(wornShirt);
                break;
            case "Legs":
                Destroy(wornPant);
                break;
            case "Hair":
                Destroy(wornHair);
                break;
            case "Feet":
                Destroy(wornShoes);
                break;          
        }
    }

    public void SetDefaultTexture() 
    {
        eye.SetTexture("_BaseMap", defaultClothDatabase.maleAvatarDefaultCostume.DefaultEyes);

        body.materials[0].SetColor("_BaseColor", defaultClothDatabase.maleAvatarDefaultCostume.DefaultSkinColor);
        body.materials[2].SetColor("_BaseColor", defaultClothDatabase.maleAvatarDefaultCostume.DefaultSkinColor);

        body.materials[1].SetColor("_BaseColor", defaultClothDatabase.maleAvatarDefaultCostume.DefaultLipsColor);
    }
    public void TextureForSkin(Texture texture)
    {
        body.materials[0].SetTexture("_BaseMap", texture);
        body.materials[2].SetTexture("_BaseMap", texture);
    }
    public void ColorForSkin(Color _color)
    {
        body.materials[0].SetColor("_BaseColor", _color);
        body.materials[2].SetColor("_BaseColor", _color);
    }
    public void ColorForLips(Color _color)
    {
        body.materials[1].SetColor("_BaseColor", _color);
    }
    public void ColorForEyeBrow(Color _color)
    {
        //body.materials[1].SetColor("_BaseColor", _color);
    }
    public void TextureForEyes(Texture texture)
    {
        eye.SetTexture("_BaseMap", texture);
    }
    public void ColorForHairs(Color _color)
    {
        if (wornHair.GetComponent<SkinnedMeshRenderer>().materials[0].HasProperty("Root Color") && wornHair.GetComponent<SkinnedMeshRenderer>().materials[0].HasProperty("Tip Color"))
        {
            wornHair.GetComponent<SkinnedMeshRenderer>().materials[0].SetColor("Root Color", _color);
            wornHair.GetComponent<SkinnedMeshRenderer>().materials[0].SetColor("Tip Color", _color);
        }
    }
}
