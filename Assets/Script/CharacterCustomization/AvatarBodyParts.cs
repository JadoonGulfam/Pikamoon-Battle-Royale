using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace CharacterCustomization
{
    public class AvatarBodyParts : MonoBehaviour
    {
        AvatarController avatarController;
        public CharacterData currentCharacterData;
        private GameObject presetObject;
        private void Awake()
        {
            avatarController = GetComponent<AvatarController>();
        }
        public void ApplyHairPreset(GameObject _preset, string _key, BodyPartsType _type, bool _applyColor)
        {
            avatarController.StichItem(-1, _preset, _type, this.gameObject, _applyColor);
            currentCharacterData.hairPreset = _key;
        }
        public void ApplyClothPreset(GameObject _preset, string _key, BodyPartsType _type)
        {
            avatarController.StichItem(-1, _preset, _type, this.gameObject);
            currentCharacterData.clothPreset = _key;
        }
        //public void ApplyTrouserPreset(GameObject _preset, string _key, BodyPartsType _type)
        //{
        //    avatarController.StichItem(-1, _preset, _type, this.gameObject);
        //    currentCharacterData.trouserPreset = _key;
        //}
        //public void ApplyArmsPreset(GameObject _preset, string _key, BodyPartsType _type)
        //{
        //    avatarController.StichItem(-1, _preset, _type, this.gameObject);
        //    currentCharacterData.armPreset = _key;
        //}
        //public void ApplyLegsPreset(GameObject _preset, string _key, BodyPartsType _type)
        //{
        //    avatarController.StichItem(-1, _preset, _type, this.gameObject);
        //    currentCharacterData.legPreset = _key;
        //}
        //public void ApplyShoesPreset(GameObject _preset, string _key, BodyPartsType _type)
        //{
        //    avatarController.StichItem(-1, _preset, _type, this.gameObject);
        //    currentCharacterData.shoesPreset = _key;
        //}
        public void ApplyOnPreset(GameObject _preset, string _key, BodyType _type)
        {
            if (presetObject != null)
                Destroy(presetObject);
            presetObject = Instantiate(_preset);
            currentCharacterData.characterPreset = _key;
        }
        public void ApplyEyeTexture(Texture2D _texture, string _key)
        {
            avatarController.eye.material.SetTexture("_BaseMap", _texture);
            currentCharacterData.eyeColor = _key;
        }
        public void ApplyEyebrowTexture(Texture2D _texture, string _key)
        {
            avatarController.body.materials[2].SetTexture("_BaseMap", _texture);
            currentCharacterData.eyeBrowShape = _key;
        }
        public void ApplyColor(string _color, BodyType _type)
        {
            Color newColor;
            switch (_type)
            {
                case BodyType.SkinColor:
                    if (ColorUtility.TryParseHtmlString("#" + _color, out newColor))
                    {
                        avatarController.body.materials[3].SetColor("_BaseColor", newColor);
                        avatarController.body.materials[5].SetColor("_BaseColor", newColor);
                       // currentCharacterData.skinColor = newColor;
                    }
                    break;
                case BodyType.Lips:
                    if (ColorUtility.TryParseHtmlString("#" + _color, out newColor))
                    {
                        avatarController.body.materials[4].SetColor("_BaseColor", newColor);
                        //currentCharacterData.lipsColor = newColor;
                    }
                    break;
                case BodyType.Hair:
                    if (ColorUtility.TryParseHtmlString("#" + _color, out newColor))
                    {
                        avatarController.wornHair.GetComponent<SkinnedMeshRenderer>().materials[0].SetColor("_Root_Color", newColor);
                        avatarController.wornHair.GetComponent<SkinnedMeshRenderer>().materials[0].SetColor("_Tip_Color", newColor);
                        if (avatarController.wornHair.GetComponent<SkinnedMeshRenderer>().materials[1] != null)
                            avatarController.wornHair.GetComponent<SkinnedMeshRenderer>().materials[1].SetColor("_BaseColor", newColor);
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
}