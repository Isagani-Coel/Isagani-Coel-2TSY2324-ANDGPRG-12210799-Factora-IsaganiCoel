using UnityEngine.Audio;
using UnityEngine;
using System;
using Unity.VisualScripting;

public class AudioManager : MonoBehaviour {
    public Sound[] bgm, sfx;
    public AudioSource bgmSRC, sfxSRC;
    public static AudioManager instance;

    private void Awake() {
        if (instance == null) { 
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else Destroy(gameObject); // only 1 instancewibn the program is allowed
    }
    void Start() {
        PlayMusic("Theme");
    }

    void Update() {

    }

    public void PlayMusic(string name) {
        Sound s = Array.Find(bgm, x => x.name == name);

        if (s == null) Debug.Log("SOUND NOT FOUND");
        else { 
            bgmSRC.volume = s.volume;
            bgmSRC.pitch = s.pitch;
            bgmSRC.loop = s.loop;
            bgmSRC.clip = s.clip;
            bgmSRC.Play();
        }
    }   
    public void PlaySound(string name) {
        Sound s = Array.Find(sfx, x => x.name == name);

        if (s == null) Debug.Log("SOUND NOT FOUND");
        else {
            sfxSRC.volume = s.volume;
            sfxSRC.pitch = s.pitch;
            sfxSRC.loop = s.loop;
            sfxSRC.clip = s.clip;
            sfxSRC.PlayOneShot(s.clip);
        }
    } 

    public void ToggleMusic() {
        bgmSRC.mute = !bgmSRC.mute;
    }

    public void ToggleSound() {
        sfxSRC.mute = !sfxSRC.mute;
    }

    public void BGMvolume(float volume) {
        bgmSRC.volume = volume;
    }

    public void SFXvolume(float volume) {
        sfxSRC.volume = volume;
    }
}

/*
using UnityEngine.Audio;
using UnityEngine;
using System;
using Unity.VisualScripting; 
*/