using UnityEngine;

public class UIPanelSound : MonoBehaviour
{
    public UISoundProfile soundProfile;
    private bool hasPlayedOpen = false;

    private void OnEnable()
    {
        // Prevent double-play if panel is enabled twice in same frame
        if (!hasPlayedOpen)
        {
            OpenPanel();
            hasPlayedOpen = true;
        }
    }

    private void OnDisable()
    {
        hasPlayedOpen = false; // reset for next activation
        ClosePanel();
    }
    public void OpenPanel()
    {
        UISoundEvents.OnPanelOpen?.Invoke(soundProfile.panelOpen, soundProfile.volume);
    }

    public void ClosePanel()
    {
        UISoundEvents.OnPanelClose?.Invoke(soundProfile.panelClose, soundProfile.volume);
        gameObject.SetActive(false);
    }
}
