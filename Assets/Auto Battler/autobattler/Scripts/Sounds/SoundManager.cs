using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Central manager responsible for coordinating sound components and managing sound playback.
/// </summary>
public class SoundManager : MonoBehaviour
{
    [Header("Sound Settings")]
    [SerializeField] private SoundDatabase soundDatabase;
    [SerializeField] private SoundPlayer soundPlayer;

    private readonly Dictionary<string, Sound> soundDictionaryByName = new Dictionary<string, Sound>();
    private readonly Dictionary<int, Sound> soundDictionaryByID = new Dictionary<int, Sound>();

    private void Awake()
    {
        InitializeSoundDictionaries();
    }

    /// <summary>
    /// Initializes dictionaries for quick sound lookup by name or ID.
    /// </summary>

    private void Start()
    {
        PlaySoundByID(1);
    }
    private void InitializeSoundDictionaries()
    {
        foreach (var sound in soundDatabase.Sounds)
        {
            soundDictionaryByName[sound.soundName] = sound;
            soundDictionaryByID[sound.soundID] = sound;
        }
    }

    public void PlaySoundByName(string soundName)
    {
        if (soundDictionaryByName.TryGetValue(soundName, out Sound sound))
        {
            soundPlayer.PlaySound(sound);
        }
        else
        {
            Debug.LogWarning($"Sound with name '{soundName}' not found!");
        }
    }

    public void PlaySoundByID(int soundID)
    {
        if (soundDictionaryByID.TryGetValue(soundID, out Sound sound))
        {
            soundPlayer.PlaySound(sound);
        }
        else
        {
            Debug.LogWarning($"Sound with ID '{soundID}' not found!");
        }
    }

    public void PauseSoundByName(string soundName)
    {
        if (soundDictionaryByName.TryGetValue(soundName, out Sound sound))
        {
            soundPlayer.PauseSound(sound);
        }
        else
        {
            Debug.LogWarning($"Sound with name '{soundName}' not found!");
        }
    }

    public void PauseSoundByID(int soundID)
    {
        if (soundDictionaryByID.TryGetValue(soundID, out Sound sound))
        {
            soundPlayer.PauseSound(sound);
        }
        else
        {
            Debug.LogWarning($"Sound with ID '{soundID}' not found!");
        }
    }

    public void StopSoundByName(string soundName)
    {
        if (soundDictionaryByName.TryGetValue(soundName, out Sound sound))
        {
            soundPlayer.StopSound(sound);
        }
        else
        {
            Debug.LogWarning($"Sound with name '{soundName}' not found!");
        }
    }

    public void StopSoundByID(int soundID)
    {
        if (soundDictionaryByID.TryGetValue(soundID, out Sound sound))
        {
            soundPlayer.StopSound(sound);
        }
        else
        {
            Debug.LogWarning($"Sound with ID '{soundID}' not found!");
        }
    }

    public void StopAllSounds()
    {
        soundPlayer.StopAllSounds();
    }

    public void FadeInSound(string soundName, float duration)
    {
        if (soundDictionaryByName.TryGetValue(soundName, out Sound sound))
        {
            soundPlayer.FadeInSound(sound, duration);
        }
        else
        {
            Debug.LogWarning($"Sound with name '{soundName}' not found!");
        }
    }

    public void FadeOutSound(string soundName, float duration)
    {
        if (soundDictionaryByName.TryGetValue(soundName, out Sound sound))
        {
            soundPlayer.FadeOutSound(sound, duration);
        }
        else
        {
            Debug.LogWarning($"Sound with name '{soundName}' not found!");
        }
    }
}
