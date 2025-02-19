using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
    public class AvatarController : MonoBehaviour
    {
        public AvatarDefaultClothes maleAvatarDefaultCostume;
        public AvatarDefaultClothes femaleAvatarDefaultCostume;

        //private DefaultClothDatabase defaultClothDatabase;
        public Stitcher stitcher;
        public SkinnedMeshRenderer body, eye;
        public GameObject wornHair, wornCloth;
        //AvatarBodyParts avatarBodyParts;
        public GenderType genderType;

        public CharacterData currentCharacterData;
        private GameObject presetObject;
        private void Awake()
        {
            stitcher = new Stitcher();
           //avatarBodyParts = GetComponent<AvatarBodyParts>();
           // defaultClothDatabase = GetComponent<DefaultClothDatabase>();
        }
        void Start()
        {

            SetAvatarClothDefault(this.gameObject, genderType);
        }

        public void SetAvatarClothDefault(GameObject applyOn, GenderType _gender)
        {
            WearDefaultItem(BodyPartsType.Body, applyOn.gameObject, _gender);
            WearDefaultItem(BodyPartsType.Hair, applyOn.gameObject, _gender);
            SetDefaultTexture();
        }
        public void WearDefaultItem(BodyPartsType _type, GameObject _applyOn, GenderType _gender)
        {
            if (_gender == GenderType.male) // if avatar is Male
            {
                switch (_type)
                {
                    case BodyPartsType.Body:
                        if (maleAvatarDefaultCostume.DefaultBody != null)
                            StichItem(-1, maleAvatarDefaultCostume.DefaultBody, _type, _applyOn);
                        break;
                    case BodyPartsType.Hair:
                        if (maleAvatarDefaultCostume.DefaultHair != null)
                            StichItem(-1, maleAvatarDefaultCostume.DefaultHair, _type, _applyOn);
                        break;
                    case BodyPartsType.Eyes:
                        if (maleAvatarDefaultCostume.DefaultEyes != null)
                            ApplyEyeTexture(maleAvatarDefaultCostume.DefaultEyes, string.Empty);
                        break;
                    case BodyPartsType.Eyebrow:
                        if (maleAvatarDefaultCostume.DefaultEyebrow != null)
                            ApplyEyebrowTexture(maleAvatarDefaultCostume.DefaultEyebrow, string.Empty);
                        break;
                    case BodyPartsType.Skin:
                        if (maleAvatarDefaultCostume.DefaultSkin != null && maleAvatarDefaultCostume.DefaultFace != null)
                        {
                            ApplyFaceTexture(maleAvatarDefaultCostume.DefaultFace, string.Empty);
                            ApplySkinTexture(maleAvatarDefaultCostume.DefaultSkin, string.Empty);
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
            if (_gender == GenderType.female) // if avatar is FeMale
            {
                switch (_type)
                {
                    case BodyPartsType.Body:
                        if (femaleAvatarDefaultCostume.DefaultBody != null)
                            StichItem(-1, femaleAvatarDefaultCostume.DefaultBody, _type, _applyOn);
                        break;
                    case BodyPartsType.Hair:
                        if (femaleAvatarDefaultCostume.DefaultHair != null)
                            StichItem(-1, femaleAvatarDefaultCostume.DefaultHair, _type, _applyOn);
                        break;
                    case BodyPartsType.Eyes:
                        if (femaleAvatarDefaultCostume.DefaultEyes != null)
                            ApplyEyeTexture(femaleAvatarDefaultCostume.DefaultEyes, string.Empty);
                        break;
                    case BodyPartsType.Eyebrow:
                        if (femaleAvatarDefaultCostume.DefaultEyebrow != null)
                            ApplyEyebrowTexture(femaleAvatarDefaultCostume.DefaultEyebrow, string.Empty);
                        break;
                    case BodyPartsType.Skin:
                        if (femaleAvatarDefaultCostume.DefaultSkin != null && femaleAvatarDefaultCostume.DefaultFace != null)

                        {
                            ApplyFaceTexture(femaleAvatarDefaultCostume.DefaultFace, string.Empty);
                            ApplySkinTexture(femaleAvatarDefaultCostume.DefaultSkin, string.Empty);
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
        }
        public void StichItem(int itemId, GameObject item, BodyPartsType _type, GameObject applyOn, bool applyHairColor = true)
        {

            UnStichItem(_type);

            item = this.stitcher.Stitch(item, applyOn);
            switch (_type)
            {
                case BodyPartsType.Body:
                    wornCloth = item;
                    wornCloth.GetComponent<SkinnedMeshRenderer>().updateWhenOffscreen = true;
                    break;
                case BodyPartsType.Hair:
                    wornHair = item;
                    if (currentCharacterData.hairColor != Color.black && applyHairColor)
                        ApplyColor(ColorUtility.ToHtmlStringRGB(currentCharacterData.hairColor), BodyType.Hair);
                    if (Constants.getColorObject != null)
                        Constants.getColorObject.Invoke();
                    break;
            }

        }

        public void UnStichItem(BodyPartsType _type)
        {
            switch (_type)
            {
                case BodyPartsType.Body:
                    Destroy(wornCloth);
                    break;
                case BodyPartsType.Hair:
                    Destroy(wornHair);
                    break;
            }
        }
        public void SetDefaultTexture()
        {
            body.materials[4].SetColor("_BaseColor", maleAvatarDefaultCostume.DefaultLipsColor);
        }

        public void ApplyHairPreset(GameObject _preset, string _key, BodyPartsType _type, bool _applyColor)
        {
            StichItem(-1, _preset, _type, this.gameObject, _applyColor);
            currentCharacterData.hairPreset = _key;
        }
        public void ApplyClothPreset(GameObject _preset, string _key, BodyPartsType _type)
        {
            StichItem(-1, _preset, _type, this.gameObject);
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