using UnityEngine;

public class PikamoonAiSound : MonoBehaviour
{
    [Header("Audio Clips")]
    public AudioClip walkClipStep_1;
    public AudioClip walkClipStep_2;
    public AudioClip runClipStep_1;
    public AudioClip runClipStep_2;
    public AudioClip alertClip;
    public AudioClip attackClip;
    public AudioClip stunClip;
    public AudioClip deathClip;
    public AudioClip hitClip;

    private AudioSource audioSource;
   // public float pitchRandomization = 0.1f; // Add variation to avoid repetitive sounds

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.playOnAwake = false;
    }

    public void PlaySound(AudioClip clip, bool loop = false)
    {
        if (clip == null) return;

        audioSource.loop = loop;
       // audioSource.pitch = 1f + Random.Range(-pitchRandomization, pitchRandomization);
        audioSource.clip = clip;
        audioSource.PlayOneShot(clip);
    }
    public void PlayWalkSoundStep_1() 
    {
        PlaySound(walkClipStep_1,false);
    }
    public void PlayWalkSoundStep_2()
    {
        PlaySound(walkClipStep_2, false);
    }
    public void PlayRunSoundStep_1()
    {
        PlaySound(runClipStep_1, false);
    }
    public void PlayRunSoundStep_2()
    {
        PlaySound(runClipStep_2, false);
    }
    public void PlayAttackSound()
    {
        PlaySound(attackClip, false);
    }
}
