using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;
using UnityEngine.Events;

[AddComponentMenu("Orbio Games — Ultra Pro Max VFX Frame Trigger")]
public class OrbioUltraProMaxVFXFrameTrigger : MonoBehaviour
{
    [Header("🎬 Animation Settings")]
    public Animator animator;
    public int triggerFrame = 27;
    public bool isLooping = false;
    public int frameOffset = 0;
    public string clipNameFilter = "";

    [Header("✨ VFX Settings")]
    public List<ParticleSystem> particleSystems = new List<ParticleSystem>();
    public List<VisualEffect> vfxGraphs = new List<VisualEffect>();
    public bool stayAttached = true;
    public bool autoStart = true;
    public float destroyAfterSeconds = 3f;

    [Header("🔊 Audio FX Settings")]
    public List<AudioSource> audioSources = new List<AudioSource>();
    [Range(0f, 1f)] public float audioVolume = 1f;
    public bool playAudioOnTrigger = true;

    [Header("💡 Light FX Settings")]
    public List<Light> lightsToEnable = new List<Light>();
    public float disableLightsAfter = 2f;

    [Header("⚡ Custom Events")]
    public UnityEvent onTriggerEvent;

    [Header("🐞 Debug Settings")]
    public bool showDebugLogs = false;

    private bool hasTriggered = false;
    private float lastLoopTime = -1f;

    void Start()
    {
        if (animator == null)
        {
            animator = GetComponent<Animator>();
            if (animator == null && showDebugLogs)
                Debug.LogWarning("No Animator assigned on " + gameObject.name);
        }
    }

    void Update()
    {
        if (animator == null || animator.runtimeAnimatorController == null) return;

        AnimatorStateInfo state = animator.GetCurrentAnimatorStateInfo(0);
        if (!state.loop && isLooping) return;

        AnimationClip clip = animator.runtimeAnimatorController.animationClips.Length > 0
            ? animator.runtimeAnimatorController.animationClips[0]
            : null;
        if (clip == null) return;
        if (!string.IsNullOrEmpty(clipNameFilter) && !clip.name.Contains(clipNameFilter)) return;

        float normalizedTime = state.normalizedTime % 1f;
        int currentFrame = Mathf.FloorToInt((normalizedTime * clip.length * clip.frameRate)) + frameOffset;

        if (showDebugLogs)
            Debug.Log($"[Orbio Ultra Pro Max] Frame: {currentFrame}, Trigger: {triggerFrame}");

        // Check loop reset
        if (normalizedTime < lastLoopTime)
        {
            hasTriggered = false;
        }
        lastLoopTime = normalizedTime;

        if (!hasTriggered && currentFrame >= triggerFrame)
        {
            PlayAllEffects();
            hasTriggered = true;
        }
    }

    private void PlayAllEffects()
    {
        if (showDebugLogs)
            Debug.Log($"🎯 Triggering FX at frame {triggerFrame}");

        foreach (var ps in particleSystems)
        {
            if (ps == null) continue;
            ps.Play(true);
            if (!stayAttached)
            {
                ps.transform.SetParent(null, true);
                if (destroyAfterSeconds > 0)
                    Destroy(ps.gameObject, destroyAfterSeconds);
            }
        }

        foreach (var vfx in vfxGraphs)
        {
            if (vfx == null) continue;
            vfx.Play();
            if (!stayAttached)
            {
                vfx.transform.SetParent(null, true);
                if (destroyAfterSeconds > 0)
                    Destroy(vfx.gameObject, destroyAfterSeconds);
            }
        }

        if (playAudioOnTrigger)
        {
            foreach (var audio in audioSources)
            {
                if (audio == null) continue;
                audio.volume = audioVolume;
                audio.Play();
            }
        }

        foreach (var light in lightsToEnable)
        {
            if (light == null) continue;
            light.enabled = true;
            if (disableLightsAfter > 0)
                StartCoroutine(DisableLightAfterTime(light, disableLightsAfter));
        }

        onTriggerEvent?.Invoke();
    }

    private IEnumerator DisableLightAfterTime(Light light, float time)
    {
        yield return new WaitForSeconds(time);
        if (light != null) light.enabled = false;
    }
}
