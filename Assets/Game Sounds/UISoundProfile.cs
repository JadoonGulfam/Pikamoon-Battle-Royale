using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/UISoundProfile")]
public class UISoundProfile : ScriptableObject
{
    [Header("Game Music")]
    public AudioClip musicClip;

    [Header("Buttons")]
    public AudioClip hoverSound;
    public AudioClip clickSound;

    [Header("Panels")]
    public AudioClip panelOpen;
    public AudioClip panelClose;

    [Header("Extras")]
    public float volume = 1f;
}
