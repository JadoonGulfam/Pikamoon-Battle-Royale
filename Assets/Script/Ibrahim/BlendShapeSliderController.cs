using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BlendShapeSliderController : MonoBehaviour
{
    public Slider blendShapeSlider;
    public SliderType bodyType;
    public CharacterCustomization characterCustomization;
    private void OnEnable()
    {
        SetRelatedData();
        blendShapeSlider.onValueChanged.AddListener(UpdateBlendShape);
    }
    private void OnDisable()
    {
        blendShapeSlider.onValueChanged.RemoveListener(UpdateBlendShape);
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
              blendShapeSlider.value = characterCustomization.characterCustomizationManager.characterCustom.armShape;
                break;

            case SliderType.Legs:
                blendShapeSlider.value = characterCustomization.characterCustomizationManager.characterCustom.legShape;
                break;
            case SliderType.Torso:
                blendShapeSlider.value = characterCustomization.characterCustomizationManager.characterCustom.torsoShape;
                break;
            default:
                break;
        }
    }

}
public enum SliderType
{
    Arms, Legs, Torso
}