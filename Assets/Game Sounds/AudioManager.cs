using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

[DisallowMultipleComponent]
public class AudioManager : MonoBehaviour
{

    [Header("Audio Sources")]
    public AudioSource uiSource;      // For hover, click, panel sounds
    public AudioSource musicSource;   // For background music

    [Header("Mixer")]
    public AudioMixer mixer;          // Assign your GameAudio mixer here

    private Coroutine musicFadeRoutine;

    #region --- UI Sound Playback ---
    void Awake()
    {
        DontDestroyOnLoad(gameObject);

    }
    private void OnEnable()
    {
        UISoundEvents.OnHover += PlayHover;
        UISoundEvents.OnClick += PlayClick;
         UISoundEvents.OnMusic += PlayMusic;
        // UISoundEvents.OnUIToggle += ToggleUI;
        UISoundEvents.OnPanelOpen += PlayPanelOpen;
        UISoundEvents.OnPanelClose += PlayPanelClose;
    }

    private void OnDisable()
    {
        UISoundEvents.OnHover -= PlayHover;
        UISoundEvents.OnClick -= PlayClick;
        UISoundEvents.OnMusic -= PlayMusic;
        //  UISoundEvents.OnUIToggle -= ToggleUI;
        UISoundEvents.OnPanelOpen -= PlayPanelOpen;
        UISoundEvents.OnPanelClose -= PlayPanelClose;
    }
    public void PlayHover(AudioClip clip, float volume = 1f)
    {
        if (clip != null)
            uiSource.PlayOneShot(clip, volume);
    }

    public void PlayClick(AudioClip clip, float volume = 1f)
    {
        if (clip != null)
            uiSource.PlayOneShot(clip, volume);
    }
    private void PlayPanelOpen(AudioClip clip, float volume)
    {
        if (clip) uiSource.PlayOneShot(clip, volume);
    }

    private void PlayPanelClose(AudioClip clip, float volume)
    {
        if (clip) uiSource.PlayOneShot(clip, volume);
    }

    #endregion

    #region --- Music Playback ---

    /// <summary>
    /// Play a music clip with smooth fade
    /// </summary>
    public void PlayMusic(AudioClip clip, float volume = 1f, float fadeDuration = 1.5f)
    {
        if (clip == null) return;

        if (musicFadeRoutine != null)
            StopCoroutine(musicFadeRoutine);

        musicFadeRoutine = StartCoroutine(FadeMusic(clip, volume, fadeDuration));
    }

    private IEnumerator FadeMusic(AudioClip newClip, float targetVolume, float duration)
    {
        float t = 0f;
        float startVolume = musicSource.volume;

        // Fade out
        while (t < duration)
        {
            t += Time.unscaledDeltaTime;
            musicSource.volume = Mathf.Lerp(startVolume, 0, t / duration);
            yield return null;
        }

        musicSource.clip = newClip;
        musicSource.Play();

        // Fade in
        t = 0f;
        while (t < duration)
        {
            t += Time.unscaledDeltaTime;
            musicSource.volume = Mathf.Lerp(0, targetVolume, t / duration);
            yield return null;
        }
    }

    #endregion

    #region --- Volume Controls ---

    public void SetMusicVolume(float value)
    {
        // value: 0 to 1
        mixer.SetFloat("MusicVolume", Mathf.Log10(Mathf.Clamp(value, 0.0001f, 1f)) * 20f);
    }

    public void SetUIVolume(float value)
    {
        mixer.SetFloat("UIVolume", Mathf.Log10(Mathf.Clamp(value, 0.0001f, 1f)) * 20f);
    }

    #endregion
}
public static class UISoundEvents
{
    public static System.Action<AudioClip, float> OnHover;
    public static System.Action<AudioClip, float> OnClick;
    public static System.Action<AudioClip, float, float> OnMusic;
   // public static System.Action<bool> OnUIToggle;

    public static System.Action<AudioClip, float> OnPanelOpen;
    public static System.Action<AudioClip, float> OnPanelClose;
}