using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
public class AvatarController : MonoBehaviour
{
    public AvatarDefaultClothes AvatarDefaultCostume;
    //public AvatarDefaultClothes femaleAvatarDefaultCostume;

    public Stitcher stitcher;
    public SkinnedMeshRenderer body, eye;
    public GameObject wornHair, wornCloth;
    public GenderType genderType;

    public CharacterData currentCharacterData;
    private GameObject presetObject;
    private void Awake()
    {
        stitcher = new Stitcher();
    }
    void Start()
    {

        SetAvatarClothDefault(gameObject, genderType);
    }

    public void SetAvatarClothDefault(GameObject applyOn, GenderType _gender)
    {
        WearDefaultItem(BodyPartsType.Body, applyOn.gameObject, _gender);
        WearDefaultItem(BodyPartsType.Hair, applyOn.gameObject, _gender);
        //SetDefaultTexture();
    }
    //private AvatarDefaultClothes GetDefaultCostume(GenderType gender)
    //{
    //    return gender == GenderType.male ? maleAvatarDefaultCostume : femaleAvatarDefaultCostume;
    //}
    public void WearDefaultItem(BodyPartsType _type, GameObject _applyOn, GenderType _gender)
    {
        //var defaultCostume = GetDefaultCostume(_gender);
        if (AvatarDefaultCostume == null) return;
        switch (_type)
        {
            case BodyPartsType.Body:
                StichItem(AvatarDefaultCostume.DefaultBody, _type, _applyOn);
                break;
            case BodyPartsType.Hair:
                StichItem(AvatarDefaultCostume.DefaultHair, _type, _applyOn);
                break;
            case BodyPartsType.Eyes:
                ApplyEyeTexture(AvatarDefaultCostume.DefaultEyes, string.Empty);
                break;
            case BodyPartsType.Eyebrow:
                ApplyEyebrowTexture(AvatarDefaultCostume.DefaultEyebrow, string.Empty);
                break;
            case BodyPartsType.Skin:
                if (AvatarDefaultCostume.DefaultSkin != null && AvatarDefaultCostume.DefaultFace != null)
                {
                    ApplyFaceTexture(AvatarDefaultCostume.DefaultFace, string.Empty);
                    ApplySkinTexture(AvatarDefaultCostume.DefaultSkin, string.Empty);
                }
                break;
            //case BodyPartsType.Arms:
            //    if (defaultClothDatabase.maleAvatarDefaultCostume.DefaultArms != null)
            //        StichItem(-1, defaultClothDatabase.maleAvatarDefaultCostume.DefaultArms, _type, _applyOn);
            //    else if (wornArms != null)
            //    {
            //        UnStichItem(_type);
            //    }
            //    break;
            default:
                break;
        }     
    }
    public void StichItem(GameObject item, BodyPartsType _type, GameObject applyOn, bool applyHairColor = true)
    {
        if (item == null) return;

        UnStichItem(_type);

        GameObject stitchedItem = stitcher.Stitch(item, applyOn);
        if (stitchedItem == null) return;

        switch (_type)
        {
            case BodyPartsType.Body:
                wornCloth = stitchedItem;
                SetMeshRenderer(stitchedItem);
                break;
            case BodyPartsType.Hair:
                wornHair = stitchedItem;
                //if (currentCharacterData.hairColor != Color.black && applyHairColor)
                //    ApplyColor(ColorUtility.ToHtmlStringRGB(currentCharacterData.hairColor), BodyType.Hair);
                //if (Constants.getColorObject != null)
                //    Constants.getColorObject.Invoke();
                break;
        }

    }

    public void UnStichItem(BodyPartsType _type)
    {
        if (_type == BodyPartsType.Body && wornCloth != null)
        {
            Destroy(wornCloth);
            wornCloth = null;
        }
        else if (_type == BodyPartsType.Hair && wornHair != null)
        {
            Destroy(wornHair);
            wornHair = null;
        }
    }
    private void SetMeshRenderer(GameObject obj)
    {
        SkinnedMeshRenderer renderer = obj.GetComponent<SkinnedMeshRenderer>();
        if (renderer != null)
        {
            renderer.updateWhenOffscreen = true;
        }
    }
    public void SetDefaultTexture()
    {
        body.materials[4].SetColor("_BaseColor", AvatarDefaultCostume.DefaultLipsColor);
    }

    public void ApplyHairPreset(GameObject _preset, string _key, BodyPartsType _type, bool _applyColor)
    {
        StichItem( _preset, _type, this.gameObject, _applyColor);
        currentCharacterData.hairPreset = _key;
    }
    public void ApplyClothPreset(GameObject _preset, string _key, BodyPartsType _type)
    {
        StichItem( _preset, _type, this.gameObject);
        currentCharacterData.clothPreset = _key;
    }
    public void ApplyOnPreset(GameObject _preset, string _key, BodyType _type)
    {
        if (presetObject != null)
            Destroy(presetObject);
        presetObject = Instantiate(_preset);
        currentCharacterData.characterPreset = _key;
    }
    public void ApplyEyeTexture(Texture2D _texture, string _key)
    {
        eye.material.SetTexture("_BaseMap", _texture);
        currentCharacterData.eyeColor = _key;
    }
    public void ApplySkinTexture(Texture2D _texture, string _key)
    {
        body.materials[5].SetTexture("_BaseMap", _texture);
        currentCharacterData.skinColor = _key;
    }
    public void ApplyFaceTexture(Texture2D _texture, string _key)
    {
        body.materials[3].SetTexture("_BaseMap", _texture);
        currentCharacterData.skinColor = _key;
    }
    public void ApplyEyebrowTexture(Texture2D _texture, string _key)
    {
        body.materials[2].SetTexture("_BaseMap", _texture);
        currentCharacterData.eyeBrowShape = _key;
    }
    public void ApplyColor(string _color, BodyType _type)
    {
        Color newColor;
        switch (_type)
        {
            case BodyType.Lips:
                if (ColorUtility.TryParseHtmlString("#" + _color, out newColor))
                {
                    body.materials[4].SetColor("_BaseColor", newColor);
                    //currentCharacterData.lipsColor = newColor;
                }
                break;
            case BodyType.Hair:
                if (ColorUtility.TryParseHtmlString("#" + _color, out newColor))
                {
                    wornHair.GetComponent<SkinnedMeshRenderer>().materials[0].SetColor("_Root_Color", newColor);
                    wornHair.GetComponent<SkinnedMeshRenderer>().materials[0].SetColor("_Tip_Color", newColor);
                    if (wornHair.GetComponent<SkinnedMeshRenderer>().materials[1] != null)
                        wornHair.GetComponent<SkinnedMeshRenderer>().materials[1].SetColor("_BaseColor", newColor);
                }
                break;
                //case BodyType.Eyebrow:
                //    if (ColorUtility.TryParseHtmlString("#" + _color, out newColor))
                //    {
                //        avatarController.body.GetComponent<SkinnedMeshRenderer>().materials[2].SetColor("_BaseColor", newColor);
                //        Debug.Log("111111");
                //    }
                //    break;
        }
    }
}