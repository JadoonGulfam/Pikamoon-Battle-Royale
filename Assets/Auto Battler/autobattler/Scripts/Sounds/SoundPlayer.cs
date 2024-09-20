using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Responsible for playing, pausing, stopping, and fading sounds.
/// </summary>
public class SoundPlayer : MonoBehaviour
{
    [Header("Audio Sources")]
    [SerializeField] private AudioSource musicSource; // Dedicated for background music
    [SerializeField] private AudioSource sfxSource;   // General-purpose SFX source
    [SerializeField] private int additionalSFXSources = 3; // Number of extra sources for simultaneous SFX

    private Queue<AudioSource> sfxPool;

    private void Awake()
    {
        InitializeSFXPool();
    }

    private void InitializeSFXPool()
    {
        sfxPool = new Queue<AudioSource>();

        for (int i = 0; i < additionalSFXSources; i++)
        {
            AudioSource newSource = gameObject.AddComponent<AudioSource>();
            sfxPool.Enqueue(newSource);
        }
    }

    public void PlaySound(Sound sound)
    {
        if (sound.loop)
        {
            musicSource.clip = sound.clip;
            musicSource.volume = sound.volume;
            musicSource.pitch = sound.pitch;
            musicSource.loop = sound.loop;
            musicSource.Play();
        }
        else
        {
            AudioSource source = GetAvailableSFXSource();
            source.clip = sound.clip;
            source.volume = sound.volume;
            source.pitch = sound.pitch;
            source.loop = sound.loop;
            source.Play();
            StartCoroutine(ReturnSourceToPoolAfterPlayback(source));
        }
    }

    public void PauseSound(Sound sound)
    {
        sound.source.Pause();
    }

    public void StopSound(Sound sound)
    {
        sound.source.Stop();
    }

    public void StopAllSounds()
    {
        musicSource.Stop();
        foreach (var source in sfxPool)
        {
            source.Stop();
        }
    }

    public void FadeInSound(Sound sound, float duration)
    {
        StartCoroutine(FadeInCoroutine(sound, duration));
    }

    public void FadeOutSound(Sound sound, float duration)
    {
        StartCoroutine(FadeOutCoroutine(sound, duration));
    }

    private AudioSource GetAvailableSFXSource()
    {
        if (sfxPool.Count > 0)
        {
            return sfxPool.Dequeue();
        }
        else
        {
            AudioSource newSource = gameObject.AddComponent<AudioSource>();
            return newSource;
        }
    }

    private System.Collections.IEnumerator ReturnSourceToPoolAfterPlayback(AudioSource source)
    {
        yield return new WaitUntil(() => !source.isPlaying);
        sfxPool.Enqueue(source);
    }

    private System.Collections.IEnumerator FadeInCoroutine(Sound sound, float duration)
    {
        float startVolume = 0f;
        sound.source.volume = startVolume;
        sound.source.Play();

        while (sound.source.volume < sound.volume)
        {
            sound.source.volume += sound.volume * Time.deltaTime / duration;
            yield return null;
        }

        sound.source.volume = sound.volume;
    }

    private System.Collections.IEnumerator FadeOutCoroutine(Sound sound, float duration)
    {
        float startVolume = sound.source.volume;

        while (sound.source.volume > 0)
        {
            sound.source.volume -= startVolume * Time.deltaTime / duration;
            yield return null;
        }

        sound.source.Stop();
        sound.source.volume = sound.volume;
    }
}
