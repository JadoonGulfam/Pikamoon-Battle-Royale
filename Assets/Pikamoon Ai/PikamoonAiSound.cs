using UnityEngine;

public class PikamoonAiSound : MonoBehaviour
{
    [Header("Audio Clips")]
    public AudioClip idleClip;
    public AudioClip walkClip;
    public AudioClip runClip;
    public AudioClip alertClip;
    public AudioClip attackClip;
    public AudioClip stunClip;
    public AudioClip deathClip;

    [Header("Audio Source")]
    public AudioSource audioSource;
    public float pitchRandomization = 0.1f; // Add variation to avoid repetitive sounds

    private void Awake()
    {
        audioSource.playOnAwake = false;
    }

    public void PlaySound(AudioClip clip, bool loop = false)
    {
        if (clip == null) return;

        audioSource.loop = loop;
        audioSource.pitch = 1f + Random.Range(-pitchRandomization, pitchRandomization);
        audioSource.clip = clip;
        audioSource.Play();
    }
    void Start()
    {
        
    }
    public void PlayWalkSound() 
    {
        PlaySound(walkClip,false);
    }
    public void PlayAttackSound()
    {
        PlaySound(attackClip, false);
    }
}
