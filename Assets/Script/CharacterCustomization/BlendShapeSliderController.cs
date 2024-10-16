using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
namespace CharacterCustomization
{
    public class BlendShapeSliderController : MonoBehaviour
    {
        public Slider blendShapeSlider;
        public Button reset;
        public SliderType bodyType;
        public CharacterCustomization characterCustomization;
        private float defaultValueArms, defaultValueLegs, defaultValueTorso;
        private void OnEnable()
        {
            SetRelatedData();
            blendShapeSlider.onValueChanged.AddListener(UpdateBlendShape);
            reset.onClick.AddListener(ResetData);
        }
        private void OnDisable()
        {
            blendShapeSlider.onValueChanged.RemoveListener(UpdateBlendShape);
            reset.onClick.RemoveListener(ResetData);
        }
        void UpdateBlendShape(float value)
        {
            switch (bodyType)
            {
                case SliderType.Arms:
                    characterCustomization.ChangeArmsBlendShapes(value);
                    break;
                case SliderType.Legs:
                    characterCustomization.ChangeLegsBlendShapes(value);
                    break;
                case SliderType.Torso:
                    characterCustomization.ChangeTorsoBlendShapes(value);
                    break;
            }
        }
        void SetRelatedData()
        {
            switch (bodyType)
            {
                case SliderType.Arms:
                    blendShapeSlider.value = characterCustomization.characterCustomizationManager.avatarBodyParts.currentCharacterData.armShape;
                    defaultValueArms = characterCustomization.characterCustomizationManager.avatarBodyParts.currentCharacterData.armShape;
                    break;

                case SliderType.Legs:
                    blendShapeSlider.value = characterCustomization.characterCustomizationManager.avatarBodyParts.currentCharacterData.legShape;
                    defaultValueLegs = characterCustomization.characterCustomizationManager.avatarBodyParts.currentCharacterData.legShape;
                    break;
                case SliderType.Torso:
                    blendShapeSlider.value = characterCustomization.characterCustomizationManager.avatarBodyParts.currentCharacterData.torsoShape;
                    defaultValueTorso = characterCustomization.characterCustomizationManager.avatarBodyParts.currentCharacterData.torsoShape;
                    break;
                default:
                    break;
            }
        }
        void ResetData()
        {
            switch (bodyType)
            {
                case SliderType.Arms:
                    blendShapeSlider.value = defaultValueArms;
                    characterCustomization.ChangeArmsBlendShapes(defaultValueArms);
                    break;

                case SliderType.Legs:
                    blendShapeSlider.value = defaultValueLegs;
                    characterCustomization.ChangeLegsBlendShapes(defaultValueLegs);
                    break;
                case SliderType.Torso:
                    blendShapeSlider.value = defaultValueTorso;
                    characterCustomization.ChangeTorsoBlendShapes(defaultValueTorso);
                    break;
                default:
                    break;
            }
        }

    }
}