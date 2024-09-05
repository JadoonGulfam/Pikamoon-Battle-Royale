
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
        GetComponent<Button>().onClick.AddListener(OnButtonClick);
    }
    private void OnDisable()
    {
        GetComponent<Button>().onClick.RemoveListener(OnButtonClick);
    }
    public void OnButtonClick() 
    {
        if (GetComponent<Image>().color.a is 1f) return;
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
          //  case "HairColor":
          //     CharacterCustomization.ChangeHairColor(currentIndex, bodyType.SkinColor);
                break;
            case "EyeColor":
                CharacterCustomization.downloadPresetTexture(currentIndex, bodyType.EyeColor);
                break;
            case "Preset":
                CharacterCustomization.downloadPresetObject(currentIndex, bodyType.Preset);
                break;
            default:
                break;
        }

        Constants.Instance._curretClickedBtn = this.gameObject;
        if (Constants.Instance._lastAvatarClickedBtn && Constants.Instance._curretClickedBtn == Constants.Instance._lastAvatarClickedBtn)
            return;

        Constants.Instance._curretClickedBtn.GetComponent<Image>().color = new Color(0f, 0f, 1f, 1f);

        if (Constants.Instance._lastAvatarClickedBtn)
        {
            if (Constants.Instance._lastAvatarClickedBtn.GetComponent<ButtonPress>())
                Constants.Instance._lastAvatarClickedBtn.GetComponent<Image>().color = new Color(1f, 1f, 1f, 0f);
        }

        Constants.Instance._lastAvatarClickedBtn = this.gameObject;
    }
}
