using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace CharacterCustomization
{
    public class CharacterCustomization : MonoBehaviour
    {
        public List<int> blendShapes;
        public CharacterCustomizationManager characterCustomizationManager;
        private void OnEnable()
        {
            Constants.resetBlendShapes += ResetBlendShapes;
        }
        private void OnDisable()
        {
            Constants.resetBlendShapes -= ResetBlendShapes;
        }
        void Start()
        {
           // blendShapes = new List<int>();
        }

        public void ChangeFaceBlendShapes(string _index)
        {
            ResetBlendShapes();
            if (_index != "default")
            {
                characterCustomizationManager.avatarController.body.SetBlendShapeWeight(int.Parse(_index), 100);
                characterCustomizationManager.avatarBodyParts.currentCharacterData.faceShape = _index;
                characterCustomizationManager.save.interactable = true;
                characterCustomizationManager.reset.interactable = true;
            }
            else
                characterCustomizationManager.avatarBodyParts.currentCharacterData.faceShape = _index;

        }
        public void ChangeLipsBlendShapes(string _index)
        {
            ResetBlendShapes();
            if (_index != "default")
            {
                // characterMesh.SetBlendShapeWeight(index, 100);
                characterCustomizationManager.avatarBodyParts.currentCharacterData.lipsShape = _index;
                characterCustomizationManager.save.interactable = true;
                characterCustomizationManager.reset.interactable = true;
            }
            else
                characterCustomizationManager.avatarBodyParts.currentCharacterData.lipsShape = _index;

        }
        public void ChangeEyeBlendShapes(string _index)
        {
            ResetBlendShapes();
            if (_index != "default")
            {
                // characterMesh.SetBlendShapeWeight(index, 100);
                characterCustomizationManager.avatarBodyParts.currentCharacterData.eyeShape = _index;
                characterCustomizationManager.save.interactable = true;
                characterCustomizationManager.reset.interactable = true;
            }
            else
                characterCustomizationManager.avatarBodyParts.currentCharacterData.eyeShape = _index;
        }
        public void ChangeNoseBlendShapes(string _index)
        {
            ResetBlendShapes();
            if (_index != "default")
            {
                // characterMesh.SetBlendShapeWeight(index, 100);
                characterCustomizationManager.avatarBodyParts.currentCharacterData.noseShape = _index;
                characterCustomizationManager.save.interactable = true;
                characterCustomizationManager.reset.interactable = true;
            }
            else
                characterCustomizationManager.avatarBodyParts.currentCharacterData.noseShape = _index;

        }
        public void ChangeArmsBlendShapes(float _index)
        {
            //ResetBlendShapes();
            // characterMesh.SetBlendShapeWeight(index, 100);
            characterCustomizationManager.avatarBodyParts.currentCharacterData.armShape = _index;
            characterCustomizationManager.save.interactable = true;
            characterCustomizationManager.reset.interactable = true;

        }
        public void ChangeLegsBlendShapes(float _index)
        {
            //ResetBlendShapes();
            // characterMesh.SetBlendShapeWeight(index, 100);
            characterCustomizationManager.avatarBodyParts.currentCharacterData.legShape = _index;
            characterCustomizationManager.save.interactable = true;
            characterCustomizationManager.reset.interactable = true;

        }
        public void ChangeTorsoBlendShapes(float _index)
        {
            //ResetBlendShapes();
            // characterMesh.SetBlendShapeWeight(index, 100);
            characterCustomizationManager.avatarBodyParts.currentCharacterData.torsoShape = _index;
            characterCustomizationManager.save.interactable = true;
            characterCustomizationManager.reset.interactable = true;

        }
        public void downloadPresetObject(string _key, BodyType _type)
        {
            if (Constants.downloadAddressableObject != null)
            {
                if (_key != "default")
                    _ = StartCoroutine(Constants.downloadAddressableObject(_key, _type, characterCustomizationManager.avatarBodyParts.gameObject, false));
                else
                {
                    switch (_type)
                    {
                        case BodyType.Trouser:
                            characterCustomizationManager.avatarController.WearDefaultItem(BodyPartsType.Hips, characterCustomizationManager.avatarController.gameObject, GenderType.male);
                            characterCustomizationManager.avatarBodyParts.currentCharacterData.trouserPreset = _key;
                            break;
                        case BodyType.Shirt:
                            characterCustomizationManager.avatarController.WearDefaultItem(BodyPartsType.Chest, characterCustomizationManager.avatarController.gameObject, GenderType.male);
                            characterCustomizationManager.avatarBodyParts.currentCharacterData.shirtPreset = _key;
                            break;
                        case BodyType.Shoes:
                            characterCustomizationManager.avatarController.WearDefaultItem(BodyPartsType.Feet, characterCustomizationManager.avatarController.gameObject, GenderType.male);
                            characterCustomizationManager.avatarBodyParts.currentCharacterData.shoesPreset = _key;
                            break;
                        case BodyType.Hair:
                            characterCustomizationManager.avatarController.WearDefaultItem(BodyPartsType.Hair, characterCustomizationManager.avatarController.gameObject, GenderType.male);
                            characterCustomizationManager.avatarBodyParts.currentCharacterData.hairPreset = _key;
                            break;
                        case BodyType.Arms:
                            characterCustomizationManager.avatarController.WearDefaultItem(BodyPartsType.Arms, characterCustomizationManager.avatarController.gameObject, GenderType.male);
                            characterCustomizationManager.avatarBodyParts.currentCharacterData.armPreset = _key;
                            break;
                        case BodyType.Legs:
                            characterCustomizationManager.avatarController.WearDefaultItem(BodyPartsType.Legs, characterCustomizationManager.avatarController.gameObject, GenderType.male);
                            characterCustomizationManager.avatarBodyParts.currentCharacterData.legPreset = _key;
                            break;
                    }
                }
            }
            characterCustomizationManager.save.interactable = true;
            characterCustomizationManager.reset.interactable = true;
        }
        public void ChangeSkinColor(string _color, BodyType _type)
        {
            if (_color != "default")
            {
                characterCustomizationManager.avatarBodyParts.ApplyColor(_color, _type);
                characterCustomizationManager.save.interactable = true;
                characterCustomizationManager.reset.interactable = true;
            }
            else
            {
                characterCustomizationManager.avatarBodyParts.ApplyColor("FFFFFF", _type);
            }
        }
        //public void ChangeLipsColor(string _color, bodyType _type)
        //{
        //        characterCustomizationManager.avatarBodyParts.ApplyColor(_color, _type);
        //        characterCustomizationManager.save.interactable = true;
        //        characterCustomizationManager.reset.interactable = true;
        //}
        public async void downloadPresetTexture(string _key, BodyType _type)
        {
            if (Constants.downloadAddressableTexture != null)
            {
                if (_key != "default")
                    await Constants.downloadAddressableTexture(_key, _type, characterCustomizationManager.avatarBodyParts.gameObject);
                else
                {
                    switch (_type)
                    {
                        case BodyType.EyeColor:
                            characterCustomizationManager.avatarController.WearDefaultItem(BodyPartsType.Eyes, characterCustomizationManager.avatarController.gameObject, GenderType.male);
                            characterCustomizationManager.avatarBodyParts.currentCharacterData.eyeColor = _key;
                            break;
                    }
                }
            }
            characterCustomizationManager.save.interactable = true;
            characterCustomizationManager.reset.interactable = true;

        }
        private void ResetBlendShapes()
        {
            for (int i = 0; i < blendShapes.Count; i++)
            {                
                characterCustomizationManager.avatarController.body.SetBlendShapeWeight(blendShapes[i], 0);
            }
        }
    }
}