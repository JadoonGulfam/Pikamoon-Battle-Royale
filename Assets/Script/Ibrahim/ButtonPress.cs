
using UnityEngine;
using UnityEngine.UI;

public class ButtonPress : MonoBehaviour
{
    public string currentIndex;
    public bodyType bodyType;
    public CharacterCustomization CharacterCustomization;
    void Start()
    {
       
    }
    private void OnEnable()
    {
        GetComponent<Button>().onClick.AddListener(ChangeValue);
    }
    public void ChangeValue() 
    {
        string type = bodyType.ToString();
        switch (type) 
        {
            case "Face":
                CharacterCustomization.ChangeFaceBlendShapes(currentIndex);
                break;
            case "Lips":
                CharacterCustomization.ChangeLipsBlendShapes(currentIndex);
                break;
            case "Eyes":
                CharacterCustomization.ChangeEyeBlendShapes(currentIndex);
                break;
            case "Hair":
                CharacterCustomization.downloadPresetObject(currentIndex, bodyType.Hair);
                break;
            case "Nose":
                CharacterCustomization.ChangeNoseBlendShapes(currentIndex);
                break;
            case "SkinColor":
                CharacterCustomization.ChangeSkinColor(currentIndex, bodyType.SkinColor);
                break;
            case "LipsColor":
                CharacterCustomization.ChangeLipsColor(currentIndex, bodyType.Lips);
                break;
            case "HairColor":
                CharacterCustomization.ChangeHairColor(currentIndex, bodyType.SkinColor);
                break;
            case "EyeColor":
                CharacterCustomization.downloadPresetTexture(currentIndex, bodyType.EyeColor);
                break;
            default:
                break;
        }
    }
    private void OnDisable()
    {
        GetComponent<Button>().onClick.RemoveListener(ChangeValue);
    }
}
