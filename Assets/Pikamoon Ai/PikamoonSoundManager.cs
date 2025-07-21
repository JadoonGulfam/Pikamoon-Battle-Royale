using UnityEngine;

public class PikamoonSoundManager : MonoBehaviour
{
    [Header("Audio Source")]
    [SerializeField] private AudioSource audioSource;

    [Header("Pikamoon Sounds")]
    [SerializeField] private AudioClip idleClip;
    [SerializeField] private AudioClip walkClip;
    [SerializeField] private AudioClip runClip;
    [SerializeField] private AudioClip alertClip;
    [SerializeField] private AudioClip attackClip;
    [SerializeField] private AudioClip hitClip;
    [SerializeField] private AudioClip stunClip;
    [SerializeField] private AudioClip fleeClip;

    public void PlayIdle() => PlaySound(idleClip, true);
    public void PlayWalk() => PlaySound(walkClip, false);
    public void PlayRun() => PlaySound(runClip, true);
    public void PlayAlert() => PlaySound(alertClip, false);
    public void PlayAttack() => PlaySound(attackClip, false);
    public void PlayHit() => PlaySound(hitClip, false);
    public void PlayStun() => PlaySound(stunClip, false);
    public void PlayFlee() => PlaySound(fleeClip, true);
    public void StopSound() => audioSource.Stop();

    private void PlaySound(AudioClip clip, bool loop)
    {
        if (clip == null || audioSource == null) return;

        if (audioSource.clip == clip && audioSource.isPlaying && loop)
            return;

        audioSource.loop = loop;
        audioSource.clip = clip;
        audioSource.Play();
    }
}
