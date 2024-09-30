using UnityEngine;
using UnityEngine.UI;
using System.IO;
namespace CharacterCustomization
{
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
            Constants.getColorObject += SetRelatedData;
            fcp.onColorChange.AddListener(OnChangeColor);
            SetRelatedData();
        }
        private void OnDisable()
        {
            Constants.getColorObject -= SetRelatedData;
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
            //float h, s, v;
            //Color.RGBToHSV(m_color, out h, out s, out v);
            //if (IsHV(h, v))
            //{
            //    Debug.Log("The color is HV (Hue-Value)");
            //    fcp.ChangeMode(0);
            //}
            //else if (IsHS(h, s))
            //{
            //    Debug.Log("The color is HS (Hue-Saturation)");
            //    fcp.ChangeMode(1);
            //}
            //else
            //{
            //    fcp.ChangeMode(1);
            //}
            if (getStartingColorFromMaterial)
                fcp.color = m_color;
        }


        //string ConvertColorToHex(Color color)
        //{
        //    return ColorUtility.ToHtmlStringRGBA(color);
        //}
        public Color GetLipColor()
        {
            Renderer lipsRenderer = characterCustomizationManager.avatarController.body.GetComponent<Renderer>();
            return lipsRenderer.materials[2].GetColor("_BaseColor");
        }
        public Color GetEyebrowColor()
        {
            Renderer eyebrowRenderer = characterCustomizationManager.avatarController.body.GetComponent<Renderer>();
            return eyebrowRenderer.materials[0].GetColor("_BaseColor");
        }
        public Color GetHairColor()
        {
            Renderer hairRenderer = characterCustomizationManager.avatarController.wornHair.GetComponent<Renderer>();
            return hairRenderer.materials[0].GetColor("_Root_Color");
            // return characterCustomizationManager.avatarController.wornHair.  .avatarBodyParts.currentCharacterData.hairColor;
        }
        public void SetRelatedData()
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
            characterCustomizationManager.save.interactable = true;
            characterCustomizationManager.reset.interactable = true;
        }
        public void ChangeLipColor(Color _color)
        {
            characterCustomizationManager.avatarBodyParts.ApplyColor(ColorUtility.ToHtmlStringRGB(_color), bodyType.Lips);//   .body.materials[1].color = color;
            characterCustomizationManager.avatarBodyParts.currentCharacterData.lipsColor = _color;
            characterCustomizationManager.save.interactable = true;
            characterCustomizationManager.reset.interactable = true;
        }
        public void ChangeEyebrowColor(Color _color)
        {
            characterCustomizationManager.avatarBodyParts.ApplyColor(ColorUtility.ToHtmlStringRGB(_color), bodyType.EyebrowColor);
            characterCustomizationManager.avatarBodyParts.currentCharacterData.eyeBrowColor = _color;
            characterCustomizationManager.save.interactable = true;
            characterCustomizationManager.reset.interactable = true;
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
}
public enum ColorType
{
    HairColor, LipsColor, EyebrowColor
}
