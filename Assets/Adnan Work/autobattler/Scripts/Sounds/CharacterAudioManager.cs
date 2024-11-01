using UnityEngine;

/// <summary>
/// Manages character-specific sounds such as spawn, death, and victory.
/// This script should be attached to each character, and each character should have its own AudioSource.
/// </summary>
[RequireComponent(typeof(AudioSource))]
public class CharacterAudioManager : MonoBehaviour
{
    [Header("Character Sounds")]
    [SerializeField] private AudioClip spawnClip;
    [SerializeField] private AudioClip deathClip;
    [SerializeField] private AudioClip victoryClip;

    private AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    /// <summary>
    /// Plays the spawn sound.
    /// </summary>
    public void PlaySpawnSound()
    {
        PlaySound(spawnClip);
    }

    /// <summary>
    /// Plays the death sound.
    /// </summary>
    public void PlayDeathSound()
    {
        PlaySound(deathClip);
    }

    /// <summary>
    /// Plays the victory sound.
    /// </summary>
    public void PlayVictorySound()
    {
        PlaySound(victoryClip);
    }

    /// <summary>
    /// Plays a given sound clip.
    /// </summary>
    /// <param name="clip">The AudioClip to play.</param>
    private void PlaySound(AudioClip clip)
    {
        if (clip == null)
        {
            Debug.LogWarning("Attempted to play a null sound clip.");
            return;
        }

        audioSource.clip = clip;
        audioSource.Play();
    }
}
