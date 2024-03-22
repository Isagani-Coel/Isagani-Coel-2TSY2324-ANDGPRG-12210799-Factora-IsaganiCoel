using UnityEngine.Audio;
using UnityEngine;
using System;

public class SoundManager : MonoBehaviour {
    public static SoundManager instance;

    [SerializeField] Sound[] misc, enemies, towers;
    [SerializeField] AudioSource soundSource;

    void Awake() {
        if (instance == null) {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else Destroy(gameObject);
    }

    public void PlaySound(string name, int mode) {
        Sound s = null;

        switch (mode) {
            case 0: s = Array.Find(misc, i => i.name == name); break;
            case 1: s = Array.Find(enemies, i => i.name == name); break;
            case 2: s = Array.Find(towers, i => i.name == name); break;
        }

        if (s != null) {
            soundSource.volume = s.volume;
            soundSource.loop = s.loop;
            soundSource.clip = s.clip;
            soundSource.PlayOneShot(s.clip);
        }
        else Debug.LogError("SOUND NOT FOUND");
    }
    public void ToggleMute() { soundSource.mute = !soundSource.mute; }
    public void ToggleVolume(float v) { soundSource.volume = v; }
}

[System.Serializable]
public class Sound {
    public string name;
    public AudioClip clip;
    public bool loop;

    [Range(0f, 1f)]
    public float volume = 0.8f;

    [HideInInspector]
    public AudioSource source;
}
