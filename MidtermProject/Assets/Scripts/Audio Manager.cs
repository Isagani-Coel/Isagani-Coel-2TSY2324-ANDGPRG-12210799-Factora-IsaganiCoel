using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using System;
using UnityEditor.Experimental.GraphView;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour {
    public Sounds[] bgm, sfx;
    public AudioSource bgmSRC, sfxSRC;
    
    public static AudioManager instance;

    private void Awake() {
        if (instance == null) {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
            Destroy(gameObject);
    }
    void Start() {
        PlayMusic("1-1");   
    }

    void Update() { 
    }

    public void PlayMusic(string name) {
        Sounds s = Array.Find(bgm, x => x.name == name);

        if (s == null)
            Debug.Log("Sound Not Found");

        else {
            bgmSRC.volume = 0.7f;
            bgmSRC.loop = true;
            bgmSRC.clip = s.clip;
            bgmSRC.Play();
        }
    }

    public void PlaySound(string name) {
        Sounds s = Array.Find(sfx, x => x.name == name);

        if (s == null)
            Debug.Log("Sound Not Found");

        else {
            sfxSRC.volume = 0.5f;
            sfxSRC.PlayOneShot(s.clip);
        }
    }
}
