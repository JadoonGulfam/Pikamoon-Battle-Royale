using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;

public class AvatarController : MonoBehaviour
{
    public DefaultClothDatabase defaultClothDatabase;
    public Stitcher stitcher;
    void Start()
    {
        stitcher = new Stitcher();
        SetAvatarClothDefault(this.gameObject, "Male");
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
                wornShirt.GetComponent<SkinnedMeshRenderer>().updateWhenOffscreen = true;
                break;
            case "Legs":
                wornPant = item;
                wornPant.GetComponent<SkinnedMeshRenderer>().updateWhenOffscreen = true;
                break;
            case "Hair":
                wornHair = item;
                break;
            case "Feet":
                wornShoes = item;              
                wornShoes.GetComponent<SkinnedMeshRenderer>().updateWhenOffscreen = true;
                break;
        }
       
    }

    public GameObject wornHair, wornPant, wornShirt, wornShoes;
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
    public SkinnedMeshRenderer body;
    string Skin_TextureName = "_BaseMap";
    string Lips_TextureName = "_BaseMap";
    string Eyes_TextureName = "_BaseMap";
    string Hair_ColorName = "_BaseColor";
    public void TextureForSkin(Texture texture)
    {
        body.materials[0].SetTexture(Skin_TextureName, texture);
        body.materials[2].SetTexture(Skin_TextureName, texture);
    }   
    public void TextureForLips(Texture texture)
    {
        body.materials[1].SetTexture(Lips_TextureName, texture);
    }
    public void TextureForEyes(Texture texture)
    {
        body.materials[0].SetTexture(Eyes_TextureName, texture);
    }
    public void ColorForHairs(Color _color)
    {
        wornHair.GetComponent<SkinnedMeshRenderer>().materials[0].SetColor(Hair_ColorName, _color);
    }
}
