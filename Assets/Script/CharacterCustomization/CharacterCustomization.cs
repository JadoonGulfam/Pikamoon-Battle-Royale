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
        public void ChangeBodyBlendShapes(int _index)
        {
            characterCustomizationManager.avatarController.wornCloth.GetComponent<SkinnedMeshRenderer>().SetBlendShapeWeight(0, _index);
            characterCustomizationManager.avatarController.wornCloth.GetComponent<SkinnedMeshRenderer>().SetBlendShapeWeight(1, _index);
            characterCustomizationManager.avatarController.wornCloth.GetComponent<SkinnedMeshRenderer>().SetBlendShapeWeight(2, _index);

            _ = StartCoroutine(LerpBlendShapeWeight(characterCustomizationManager.avatarController.body, 100, _index, 1f));
            _ = StartCoroutine(LerpBlendShapeWeight(characterCustomizationManager.avatarController.body, 101, _index, 1f));
            _ = StartCoroutine(LerpBlendShapeWeight(characterCustomizationManager.avatarController.body, 102, _index, 1f));
        }
        public void ChangeFaceBlendShapes(string _index)
        {
            ResetBlendShapes();
            if (_index != "0")
            {
                int blendShapeIndex = int.Parse(_index);
                _ = StartCoroutine(LerpBlendShapeWeight(characterCustomizationManager.avatarController.body, blendShapeIndex, 100f, 1f));
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
            if (_index != "0")
            {
                int blendShapeIndex = int.Parse(_index);
                //characterCustomizationManager.avatarController.body.SetBlendShapeWeight(int.Parse(_index), 100);
                // Start a coroutine to smoothly change the blend shape weight
                _ = StartCoroutine(LerpBlendShapeWeight(characterCustomizationManager.avatarController.body, blendShapeIndex, 100f, 1f));

                characterCustomizationManager.avatarBodyParts.currentCharacterData.lipsShape = _index;
                characterCustomizationManager.save.interactable = true;
                characterCustomizationManager.reset.interactable = true;
            }
            else
                characterCustomizationManager.avatarBodyParts.currentCharacterData.lipsShape = _index;

        }
        public void ChangeEarsBlendShapes(string _index)
        {
            ResetBlendShapes();
            if (_index != "0")
            {
                int blendShapeIndex = int.Parse(_index);
                //characterCustomizationManager.avatarController.body.SetBlendShapeWeight(int.Parse(_index), 100);
                // Start a coroutine to smoothly change the blend shape weight
                _ = StartCoroutine(LerpBlendShapeWeight(characterCustomizationManager.avatarController.body, blendShapeIndex, 100f, 1f));

                characterCustomizationManager.avatarBodyParts.currentCharacterData.earsShape = _index;
                characterCustomizationManager.save.interactable = true;
                characterCustomizationManager.reset.interactable = true;
            }
            else
                characterCustomizationManager.avatarBodyParts.currentCharacterData.earsShape = _index;

        }        
        public void ChangeEyeBlendShapes(string _index)
        {
            ResetBlendShapes();
            if (_index != "0")
            {
                int blendShapeIndex = int.Parse(_index);
                _ = StartCoroutine(LerpBlendShapeWeight(characterCustomizationManager.avatarController.body, blendShapeIndex, 100f, 1f));
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
            if (_index != "0")
            {
                int blendShapeIndex = int.Parse(_index);
                _ = StartCoroutine(LerpBlendShapeWeight(characterCustomizationManager.avatarController.body, blendShapeIndex, 100f, 1f));
                characterCustomizationManager.avatarBodyParts.currentCharacterData.noseShape = _index;
                characterCustomizationManager.save.interactable = true;
                characterCustomizationManager.reset.interactable = true;
            }
            else
                characterCustomizationManager.avatarBodyParts.currentCharacterData.noseShape = _index;

        }      
        //public void ChangeTorsoBlendShapes(string _index)
        //{
        //    //ResetBlendShapes();
        //    // characterMesh.SetBlendShapeWeight(index, 100);
        //    characterCustomizationManager.avatarController.body.SetBlendShapeWeight(blendShapes[0], int.Parse(_index));
        //    characterCustomizationManager.avatarBodyParts.currentCharacterData.torsoShape = _index;
        //    characterCustomizationManager.save.interactable = true;
        //    characterCustomizationManager.reset.interactable = true;

        //}
        // Coroutine to smoothly change the blend shape weight
        private IEnumerator LerpBlendShapeWeight(SkinnedMeshRenderer _bodyRenderer, int _blendShapeIndex, float _targetWeight, float _duration)
        {
            float startWeight = _bodyRenderer.GetBlendShapeWeight(_blendShapeIndex);
            float elapsedTime = 0f;

            while (elapsedTime < _duration)
            {
                elapsedTime += Time.deltaTime;
                float newWeight = Mathf.Lerp(startWeight, _targetWeight, elapsedTime / _duration);
                _bodyRenderer.SetBlendShapeWeight(_blendShapeIndex, newWeight);
                yield return null;
            }

            // Ensure the final value is set precisely to the target weight
            _bodyRenderer.SetBlendShapeWeight(_blendShapeIndex, _targetWeight);
        }
        public void downloadPresetObject(string _key, BodyType _type)
        {
            if (Constants.downloadAddressableObject != null)
            {
                if (_key != "0")
                    _ = StartCoroutine(Constants.downloadAddressableObject(_key, _type, characterCustomizationManager.avatarBodyParts.gameObject, false));
                else
                {
                    switch (_type)
                    {
                        case BodyType.Body:
                            characterCustomizationManager.avatarController.WearDefaultItem(BodyPartsType.Body, characterCustomizationManager.avatarController.gameObject, GenderType.male);
                            characterCustomizationManager.avatarBodyParts.currentCharacterData.clothPreset = _key;
                            break;
                        case BodyType.Hair:
                            characterCustomizationManager.avatarController.WearDefaultItem(BodyPartsType.Hair, characterCustomizationManager.avatarController.gameObject, GenderType.male);
                            characterCustomizationManager.avatarBodyParts.currentCharacterData.hairPreset = _key;
                            break;
                            //case BodyType.Arms:
                            //    characterCustomizationManager.avatarController.WearDefaultItem(BodyPartsType.Arms, characterCustomizationManager.avatarController.gameObject, GenderType.male);
                            //    characterCustomizationManager.avatarBodyParts.currentCharacterData.armPreset = _key;
                            //    break;
                    }
                }
            }
            characterCustomizationManager.save.interactable = true;
            characterCustomizationManager.reset.interactable = true;
        }
        //public void ChangeSkinColor(string _color, BodyType _type)
        //{
        //    if (_color != "0")
        //    {
        //        characterCustomizationManager.avatarBodyParts.ApplyColor(_color, _type);
        //        characterCustomizationManager.save.interactable = true;
        //        characterCustomizationManager.reset.interactable = true;
        //    }
        //    else
        //    {
        //        characterCustomizationManager.avatarBodyParts.ApplyColor("FFFFFF", _type);
        //    }
        //}
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
                if (_key != "0")
                    await Constants.downloadAddressableTexture(_key, _type, characterCustomizationManager.avatarBodyParts.gameObject);
                else
                {
                    switch (_type)
                    {
                        case BodyType.EyeColor:
                            characterCustomizationManager.avatarController.WearDefaultItem(BodyPartsType.Eyes, characterCustomizationManager.avatarController.gameObject, GenderType.male);
                            characterCustomizationManager.avatarBodyParts.currentCharacterData.eyeColor = _key;
                            break;
                        case BodyType.Eyebrow:
                            characterCustomizationManager.avatarController.WearDefaultItem(BodyPartsType.Eyebrow, characterCustomizationManager.avatarController.gameObject, GenderType.male);
                            characterCustomizationManager.avatarBodyParts.currentCharacterData.eyeBrowShape = _key;
                            break;
                        case BodyType.SkinColor:
                            characterCustomizationManager.avatarController.WearDefaultItem(BodyPartsType.Skin, characterCustomizationManager.avatarController.gameObject, GenderType.male);
                            characterCustomizationManager.avatarBodyParts.currentCharacterData.skinColor = _key;
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