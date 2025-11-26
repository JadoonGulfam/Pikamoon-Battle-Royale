using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIButtonSound : MonoBehaviour, IPointerEnterHandler, IPointerClickHandler
{
    public UISoundProfile soundProfile;
    //public Button soundButton;
    public bool isHover = false;
    private void Start()
    {
        //if (soundButton)
        //    soundButton.onClick.AddListener(() =>
        //    {
        //        UISoundEvents.OnClick?.Invoke(soundProfile.clickSound, soundProfile.volume);
        //    });
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if(isHover)
        UISoundEvents.OnHover?.Invoke(soundProfile.hoverSound, soundProfile.volume);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        UISoundEvents.OnClick?.Invoke(soundProfile.clickSound, soundProfile.volume);
    }
}
