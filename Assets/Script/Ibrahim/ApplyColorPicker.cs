using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class ApplyColorPicker : MonoBehaviour
{
    //[SerializeField] Slider slider;
    //[SerializeField] Image output;
    //[SerializeField] Text outputTxt;
    //bool itemAlreadySaved = false;
    public CharacterCustomizationManager characterCustomizationManager;

    //private Color currColor;
    //private float hue;
    //private float saturation;
    //private float brightness;

    public ColorType colorCategory;
    public bool getStartingColorFromMaterial;
    public FlexibleColorPicker fcp;
    //public Material material;
    private void OnEnable()
    {
        //slider.onValueChanged.AddListener(delegate { ValueChangeCheck(); });
        //SaveCurrentColor();

        SetRelatedData();
        fcp.onColorChange.AddListener(OnChangeColor);
    }
    private void OnDisable()
    {
        fcp.onColorChange.RemoveAllListeners();

    }
    //public void ValueChangeCheck()
    //{
    //    {
    //        itemAlreadySaved = false;
    //        currColor = GetCurrentColor();
    //        if (currColor == Color.black)
    //        {
    //            // If the current color is black, adjust the color to a default value
    //            currColor = new Color(1f, 1f, 1f);
    //        }
    //        Color tempColor;
    //        Color.RGBToHSV(currColor, out hue, out saturation, out brightness);
    //        tempColor = Color.HSVToRGB(slider.value, saturation+ .2f , brightness);
    //        output.color = Color.HSVToRGB(slider.value, saturation + .2f, brightness);
    //        ChangeColor(tempColor);
    //        outputTxt.text = ConvertColorToHex(tempColor);
    //    }
    //}

    void ChangeColor(Color m_color)
    {
        if (getStartingColorFromMaterial)      
            fcp.color = m_color;
    }


    //string ConvertColorToHex(Color color)
    //{
    //    return ColorUtility.ToHtmlStringRGBA(color);
    //}
    public Color GetLipColor()
    {
        return characterCustomizationManager.avatarBodyParts.currentCharacterData.lipsColor;
    }
    public Color GetEyebrowColor()
    {
        return characterCustomizationManager.avatarBodyParts.currentCharacterData.eyeBrowColor;
    }
    public Color GetHairColor()
    {
        return characterCustomizationManager.avatarBodyParts.currentCharacterData.hairColor;
    }
    void SetRelatedData()
    {
        switch (colorCategory)
        {
            case ColorType.HairColor:
                ChangeColor(GetHairColor());
                //outputTxt.text = ConvertColorToHex(GetHairColor());
                break;

            case ColorType.LipsColor:
                ChangeColor(GetLipColor());
                // outputTxt.text = ConvertColorToHex(GetLipColor());
                break;
            case ColorType.EyebrowColor:
                ChangeColor(GetEyebrowColor());
                //outputTxt.text = ConvertColorToHex(GetEyebrowColor());
                break;
            default:
                break;
        }
    }

    //private Color currentColor;
    //public bool addToList = true;
    void OnChangeColor(Color _color)
    {
        switch (colorCategory)
        {
            case ColorType.HairColor:
                ChangeHairColor(_color);
                break;
            case ColorType.LipsColor:
                ChangeLipColor(_color);
                break;
            case ColorType.EyebrowColor:
                ChangeEyebrowColor(_color);
                break;
            default:
                break;

        }
    }
    public void ChangeHairColor(Color _color)
    {
        characterCustomizationManager.avatarBodyParts.ApplyColor(ColorUtility.ToHtmlStringRGB(_color), bodyType.Hair); //wornHair.GetComponent<SkinnedMeshRenderer>().materials[0].color = color;
        characterCustomizationManager.avatarBodyParts.currentCharacterData.hairColor = _color;
    }
    public void ChangeLipColor(Color _color)
    {
        characterCustomizationManager.avatarBodyParts.ApplyColor(ColorUtility.ToHtmlStringRGB(_color),bodyType.Lips);//   .body.materials[1].color = color;
        characterCustomizationManager.avatarBodyParts.currentCharacterData.lipsColor = _color;
    }
    public void ChangeEyebrowColor(Color _color)
    {
        characterCustomizationManager.avatarController.eyeBrow.color = _color;
        characterCustomizationManager.avatarBodyParts.currentCharacterData.eyeBrowColor = _color;
    }
    //Color GetCurrentColor()
    //{
    //    Color tempColor = Color.white;
    //    switch (sliderCategory)
    //    {
    //        case SliderType.HairColor:
    //            tempColor = GetHairColor();
    //            break;
    //        case SliderType.LipsColor:
    //            tempColor = GetLipColor();
    //            break;
    //        case SliderType.EyebrowColor:
    //            tempColor = GetEyebrowColor();
    //            break;
    //        default:
    //            return Color.white;
    //            break;
    //    }
    //    return tempColor;
    //}
    //private void SaveCurrentColor()
    //{
    //    switch (sliderCategory)
    //    {
    //        case SliderType.HairColor:
    //            currentColor = GetHairColor();
    //            break;
    //        case SliderType.LipsColor:
    //            currentColor = GetLipColor();
    //            break;
    //        case SliderType.EyebrowColor:
    //            currentColor = GetEyebrowColor();
    //            break;
    //        default:
    //            break;
    //    }
    //}
}
public enum ColorType
{
    HairColor, LipsColor, EyebrowColor
}
