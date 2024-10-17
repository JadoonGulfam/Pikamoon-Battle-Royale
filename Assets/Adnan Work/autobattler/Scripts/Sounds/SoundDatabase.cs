using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Holds a database of all sounds used in the game.
/// </summary>
[CreateAssetMenu(fileName = "SoundDatabase", menuName = "Sound/SoundDatabase")]
public class SoundDatabase : ScriptableObject
{
    [SerializeField] private List<Sound> sounds;

    public List<Sound> Sounds => sounds;
}

/// <summary>
/// Represents an individual sound with its properties.
/// </summary>
[Serializable]
public class Sound
{
    public string soundName;
    public int soundID;
    public AudioClip clip;
    [Range(0f, 1f)] public float volume = 1f;
    [Range(0.1f, 3f)] public float pitch = 1f;
    public bool loop;

    [HideInInspector] public AudioSource source;
}
