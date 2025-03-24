using UnityEngine;
namespace Pikamoon.Controller
{

    public class SFXController : MonoBehaviour
    {
        public AudioSource WeaponAttackSource;
        public AudioSource DamageSource;

        public void PlaySound(AudioClip clip)
        {
        }

        public void PlayShootSound(AudioClip clip, float volume = 1)
        {
            WeaponAttackSource.volume = volume;
            WeaponAttackSource.PlayOneShot(clip);
        }

        public void PlayDamageSound(AudioClip clip, float volume = 1)
        {
            DamageSource.volume = volume;
            DamageSource.PlayOneShot(clip);
        }
    }
}