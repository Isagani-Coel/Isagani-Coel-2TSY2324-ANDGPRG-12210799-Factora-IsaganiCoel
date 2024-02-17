using UnityEngine.Audio;
using UnityEngine;
[System.Serializable] // makes this class visible on the inspector

public class Sound {
    public string name;
    public bool loop;
    public AudioClip clip;

    [Range(0f, 1f)]
    public float volume;

    [Range(0.1f, 3f)]
    public float pitch; // 1f is normal pitch

    [HideInInspector] // makes it public but not present in the inspector
    public AudioSource source;
}

/*
using UnityEngine.Audio;
using UnityEngine.UIElements;
using UnityEngine;
*/