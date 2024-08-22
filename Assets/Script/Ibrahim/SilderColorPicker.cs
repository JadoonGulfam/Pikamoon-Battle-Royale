using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class SilderColorPicker : MonoBehaviour
{
    [SerializeField] Slider slider;
    [SerializeField] Image output;
    [SerializeField] Text outputTxt;
    bool itemAlreadySaved = false;
    public CharacterCustomizationManager characterCustomizationManager;

    private Color currColor;
    private float hue;
    private float saturation;
    private float brightness;

    public SliderType sliderCategory;

    private void OnEnable()
    {
        SetRelatedData();
        slider.onValueChanged.AddListener(delegate { ValueChangeCheck(); });
        SaveCurrentColor();
    }

    private void OnDisable()
    {
        slider.onValueChanged.RemoveAllListeners();

    }
    public void ValueChangeCheck()
    {
        {
            print("Horayyy you have Access");

            itemAlreadySaved = false;
            currColor = GetCurrentColor();

            Color tempColor;
            Color.RGBToHSV(currColor, out hue, out saturation, out brightness);
            tempColor = Color.HSVToRGB(slider.value, saturation , brightness);
            output.color = Color.HSVToRGB(slider.value, saturation + .2f, brightness);
            ChangeColor(tempColor);
            outputTxt.text = ConvertColorToHex(tempColor);
        }
    }

    void ChangeSliderColor(Color m_color)
    {
        Color.RGBToHSV(m_color, out hue, out saturation, out brightness);
        slider.value = hue;
        output.color = Color.HSVToRGB(slider.value, saturation + .2f, brightness);
    }


    string ConvertColorToHex(Color color)
    {
        return ColorUtility.ToHtmlStringRGBA(color);
    }
    public Color GetLipColor()
    {
        return characterCustomizationManager.lipMaterial.color;
    }
    public Color GetEyebrowColor()
    {
        return characterCustomizationManager.eyebrowMaterial.color;
    }
    public Color GetHairColor()
    {
        return characterCustomizationManager.hairMaterial.color;
    }
    void SetRelatedData()
    {
        switch (sliderCategory)
        {
            case SliderType.HairColor:
                ChangeSliderColor(GetHairColor());
                outputTxt.text = ConvertColorToHex(GetHairColor());
                break;

            case SliderType.LipsColor:
                ChangeSliderColor(GetLipColor());
                outputTxt.text = ConvertColorToHex(GetLipColor());
                break;
            case SliderType.EyebrowColor:
                ChangeSliderColor(GetEyebrowColor());
                outputTxt.text = ConvertColorToHex(GetEyebrowColor());
                break;
            default:
                break;
        }
    }

    private Color currentColor;
    public bool addToList = true;
    void ChangeColor(Color _color)
    {
        switch (sliderCategory)
        {
            case SliderType.HairColor:
                ChangeHairColor(_color);
                break;
            case SliderType.LipsColor:
                ChangeLipColor(_color);
                break;
            case SliderType.EyebrowColor:
                ChangeEyebrowColor(_color);
                break;
            default:
                break;

        }
    }
    public void ChangeHairColor(Color color)
    {      
        characterCustomizationManager.hairMaterial.color = color;
        characterCustomizationManager.characterCustom.hairColor = color;
    }
    public void ChangeLipColor(Color color)
    {
        characterCustomizationManager.lipMaterial.color = color;
        characterCustomizationManager.characterCustom.lipsColor = color;
    }
    public void ChangeEyebrowColor(Color color)
    {
        characterCustomizationManager.eyebrowMaterial.color = color;
        characterCustomizationManager.characterCustom.eyeBrowColor = color;
    }
    Color GetCurrentColor()
    {
        Color tempColor = Color.white;
        switch (sliderCategory)
        {
            case SliderType.HairColor:
                tempColor = GetHairColor();
                break;
            case SliderType.LipsColor:
                tempColor = GetLipColor();
                break;
            case SliderType.EyebrowColor:
                tempColor = GetEyebrowColor();
                break;
            default:
                return Color.white;
                break;
        }
        return tempColor;
    }
    private void SaveCurrentColor()
    {
        switch (sliderCategory)
        {
            case SliderType.HairColor:
                currentColor = GetHairColor();
                break;
            case SliderType.LipsColor:
                currentColor = GetLipColor();
                break;
            case SliderType.EyebrowColor:
                currentColor = GetEyebrowColor();
                break;
            default:
                break;
        }
    }
}
public enum SliderType
{
    HairColor, LipsColor, EyebrowColor
}
