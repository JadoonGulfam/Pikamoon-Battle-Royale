using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
namespace CharacterCustomization
{
    public class AvatarController : MonoBehaviour
    {
        private DefaultClothDatabase defaultClothDatabase;
        public Stitcher stitcher;
        public SkinnedMeshRenderer body, eye;
       // public Material eye;
        public GameObject wornHair, wornPant, wornShirt, wornShoes, wornArms, wornLegs;
        AvatarBodyParts avatarBodyParts;
        private void Awake()
        {
            stitcher = new Stitcher();
            avatarBodyParts = GetComponent<AvatarBodyParts>();
            defaultClothDatabase = GetComponent<DefaultClothDatabase>();
        }
        void Start()
        {
            SetAvatarClothDefault(this.gameObject, GenderType.male);
        }

        public void SetAvatarClothDefault(GameObject applyOn, GenderType _gender)
        {
            WearDefaultItem(BodyPartsType.Hips, applyOn.gameObject, _gender);
            WearDefaultItem(BodyPartsType.Legs, applyOn.gameObject, _gender);
            WearDefaultItem(BodyPartsType.Chest, applyOn.gameObject, _gender);
            WearDefaultItem(BodyPartsType.Feet, applyOn.gameObject, _gender);
            WearDefaultItem(BodyPartsType.Hair, applyOn.gameObject, _gender);
            WearDefaultItem(BodyPartsType.Arms, applyOn.gameObject, _gender);
            SetDefaultTexture();
        }
        public void WearDefaultItem(BodyPartsType _type, GameObject _applyOn, GenderType _gender)
        {
            if (_gender == GenderType.male) // if avatar is Male
            {
                switch (_type)
                {
                    //case BodyPartsType.Hips:
                    //    if (defaultClothDatabase.maleAvatarDefaultCostume.DefaultPent != null)
                    //        StichItem(-1, defaultClothDatabase.maleAvatarDefaultCostume.DefaultPent, _type, _applyOn);
                    //    break;
                    //case BodyPartsType.Chest:
                    //    if (defaultClothDatabase.maleAvatarDefaultCostume.DefaultShirt != null)
                    //        StichItem(-1, defaultClothDatabase.maleAvatarDefaultCostume.DefaultShirt, _type, _applyOn);
                    //    break;
                    //case BodyPartsType.Feet:
                    //    if (defaultClothDatabase.maleAvatarDefaultCostume.DefaultShoes != null)
                    //        StichItem(-1, defaultClothDatabase.maleAvatarDefaultCostume.DefaultShoes, _type, _applyOn);
                    //    break;
                    case BodyPartsType.Hair:
                        if (defaultClothDatabase.maleAvatarDefaultCostume.DefaultHair != null)
                            StichItem(-1, defaultClothDatabase.maleAvatarDefaultCostume.DefaultHair, _type, _applyOn);
                        break;
                    case BodyPartsType.Eyes:
                        if (defaultClothDatabase.maleAvatarDefaultCostume.DefaultEyes != null)
                            avatarBodyParts.ApplyEyeTexture(defaultClothDatabase.maleAvatarDefaultCostume.DefaultEyes, string.Empty);
                        break;
                    case BodyPartsType.Eyebrow:
                        if (defaultClothDatabase.maleAvatarDefaultCostume.DefaultEyebrow != null)
                            avatarBodyParts.ApplyEyebrowTexture(defaultClothDatabase.maleAvatarDefaultCostume.DefaultEyebrow, string.Empty);
                        break;
                    //case BodyPartsType.Arms:
                    //    if (defaultClothDatabase.maleAvatarDefaultCostume.DefaultArms != null)
                    //        StichItem(-1, defaultClothDatabase.maleAvatarDefaultCostume.DefaultArms, _type, _applyOn);
                    //    else if (wornArms != null)
                    //    {
                    //        UnStichItem(_type);
                    //    }
                    //    break;
                    //case BodyPartsType.Legs:
                    //    if (defaultClothDatabase.maleAvatarDefaultCostume.DefaultLegs != null)
                    //        StichItem(-1, defaultClothDatabase.maleAvatarDefaultCostume.DefaultLegs, _type,_applyOn);
                    //    else if (wornLegs != null)
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
                case BodyPartsType.Chest:
                    wornShirt = item;
                    // wornShirt.GetComponent<SkinnedMeshRenderer>().updateWhenOffscreen = true;
                    break;
                case BodyPartsType.Hips:
                    wornPant = item;
                    // wornPant.GetComponent<SkinnedMeshRenderer>().updateWhenOffscreen = true;
                    break;
                case BodyPartsType.Hair:
                    wornHair = item;
                    if (avatarBodyParts.currentCharacterData.hairColor != Color.black && applyHairColor)
                        avatarBodyParts.ApplyColor(ColorUtility.ToHtmlStringRGB(avatarBodyParts.currentCharacterData.hairColor), BodyType.Hair);
                    if (Constants.getColorObject != null)
                        Constants.getColorObject.Invoke();
                    break;
                case BodyPartsType.Feet:
                    wornShoes = item;
                    // wornShoes.GetComponent<SkinnedMeshRenderer>().updateWhenOffscreen = true;
                    break;
                case BodyPartsType.Arms:
                    wornArms = item;
                    // wornShoes.GetComponent<SkinnedMeshRenderer>().updateWhenOffscreen = true;
                    break;
                case BodyPartsType.Legs:
                    wornLegs = item;
                    // wornShoes.GetComponent<SkinnedMeshRenderer>().updateWhenOffscreen = true;
                    break;
            }

        }

        public void UnStichItem(BodyPartsType _type)
        {
            switch (_type)
            {
                case BodyPartsType.Chest:
                    Destroy(wornShirt);
                    break;
                case BodyPartsType.Hips:
                    Destroy(wornPant);
                    break;
                case BodyPartsType.Hair:
                    Destroy(wornHair);
                    break;
                case BodyPartsType.Feet:
                    Destroy(wornShoes);
                    break;
                case BodyPartsType.Arms:
                    Destroy(wornArms);
                    break;
                case BodyPartsType.Legs:
                    Destroy(wornLegs);
                    break;
            }
        }

        public void SetDefaultTexture()
        {
           // eye.SetTexture("_BaseMap", defaultClothDatabase.maleAvatarDefaultCostume.DefaultEyes);

            body.materials[3].SetColor("_BaseColor", defaultClothDatabase.maleAvatarDefaultCostume.DefaultSkinColor);
            body.materials[5].SetColor("_BaseColor", defaultClothDatabase.maleAvatarDefaultCostume.DefaultSkinColor);
          //  body.materials[2].SetColor("_BaseColor", defaultClothDatabase.maleAvatarDefaultCostume.DefaultEyebrowColor);
            body.materials[4].SetColor("_BaseColor", defaultClothDatabase.maleAvatarDefaultCostume.DefaultLipsColor);
        }
    }
}