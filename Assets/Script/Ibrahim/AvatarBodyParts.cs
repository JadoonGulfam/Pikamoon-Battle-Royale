using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class AvatarBodyParts : MonoBehaviour
{
    AvatarController avatarController;
    public CharacterData currentCharacterData;
    private GameObject presetObject;
    private void Awake()
    {
        avatarController = GetComponent<AvatarController>();
    }
    public void ApplyHairPreset(GameObject _preset, string _key, string _type, bool _applyColor)
    {
        avatarController.StichItem(-1, _preset, _type, this.gameObject, _applyColor);
        currentCharacterData.hairPreset = _key;
    }
    public void ApplyShirtPreset(GameObject _preset, string _key, string _type)
    {
        avatarController.StichItem(-1, _preset, "Chest", this.gameObject);
        currentCharacterData.shirtPreset = _key;
    }
    public void ApplyTrouserPreset(GameObject _preset, string _key, string _type)
    {
        avatarController.StichItem(-1, _preset, "Legs", this.gameObject);
        currentCharacterData.trouserPreset = _key;
    }
    public void ApplyShoesPreset(GameObject _preset, string _key, string _type)
    {
        avatarController.StichItem(-1, _preset, "Feet", this.gameObject);
        currentCharacterData.shoespreset = _key;
    }
    public void ApplyOnPreset(GameObject _preset, string _key, string _type)
    {
        if (presetObject != null)
            Destroy(presetObject);
        presetObject = Instantiate(_preset);
        currentCharacterData.characterPreset = _key;
    }
    public void ApplyEyeTexture(Texture2D _texture, string _key)
    {
        avatarController.eye.SetTexture("_BaseMap", _texture);
        currentCharacterData.eyeColor = _key;
    }
    public void ApplyColor(string color, bodyType _type)
    {
        Color newColor;
        switch (_type)
        {
            case bodyType.SkinColor:
                if (ColorUtility.TryParseHtmlString("#" + color, out newColor))
                {
                    avatarController.body.materials[0].SetColor("_BaseColor", newColor);
                    avatarController.body.materials[2].SetColor("_BaseColor", newColor);
                    currentCharacterData.skinColor = newColor;
                }
                break;
            case bodyType.Lips:
                if (ColorUtility.TryParseHtmlString("#" + color, out newColor))
                {
                    avatarController.body.materials[1].SetColor("_BaseColor", newColor);
                    currentCharacterData.lipsColor = newColor;
                }
                break;
            case bodyType.Hair:
                if (ColorUtility.TryParseHtmlString("#" + color, out newColor))
                {
                    avatarController.wornHair.GetComponent<SkinnedMeshRenderer>().materials[0].SetColor("_Root_Color", newColor);
                    avatarController.wornHair.GetComponent<SkinnedMeshRenderer>().materials[0].SetColor("_Tip_Color", newColor);
                    avatarController.wornHair.GetComponent<SkinnedMeshRenderer>().materials[1].SetColor("_BaseColor", newColor);
                }
                break;
        }
    }
    public void ColorForEyeBrow(Color _color)
    {
        //body.materials[1].SetColor("_BaseColor", _color);
    }
    //public void ApplyLipsColor(string color, bodyType type)
    //{
    //    Color newColor;
    //    if (ColorUtility.TryParseHtmlString(color, out newColor))
    //    {
    //        avatarController.body.materials[1].color = newColor;
    //        currentCharacterData.lipsColor = newColor;
    //    }
    //}
}
