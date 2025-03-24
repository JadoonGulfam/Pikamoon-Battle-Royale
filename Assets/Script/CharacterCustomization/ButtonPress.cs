
using UnityEngine;
using UnityEngine.UI;
public class ButtonPress : MonoBehaviour
{
    public string currentIndex;
    public BodyType bodyType;
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
        Constants.Instance.bodyType = bodyType;
        switch (bodyType)
        {
            case BodyType.Face:
                CharacterCustomization.ChangeFaceBlendShapes(currentIndex);
                break;
            case BodyType.Lips:
                CharacterCustomization.ChangeLipsBlendShapes(currentIndex);
                break;
            case BodyType.Eyes:
                CharacterCustomization.ChangeEyeBlendShapes(currentIndex);
                break;
            case BodyType.Hair:
                CharacterCustomization.downloadPresetObject(currentIndex, BodyType.Hair);
                break;
            case BodyType.Nose:
                CharacterCustomization.ChangeNoseBlendShapes(currentIndex);
                break;
            case BodyType.SkinColor:
                CharacterCustomization.downloadPresetTexture(currentIndex, BodyType.SkinColor);
                break;
            case BodyType.EyeColor:
                CharacterCustomization.downloadPresetTexture(currentIndex, BodyType.EyeColor);
                break;
            case BodyType.Eyebrow:
                CharacterCustomization.downloadPresetTexture(currentIndex, BodyType.Eyebrow);
                break;
            case BodyType.Preset:
                CharacterCustomization.downloadPresetObject(currentIndex, BodyType.Preset);
                break;
            case BodyType.Body:
                CharacterCustomization.ChangeBodyBlendShapes(int.Parse(currentIndex));
                break;
            case BodyType.Ears:
                CharacterCustomization.ChangeEarsBlendShapes(currentIndex);
                break;
            //case BodyType.Shirt:
            //    CharacterCustomization.downloadPresetObject(currentIndex, BodyType.Shirt);
            //    break;
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