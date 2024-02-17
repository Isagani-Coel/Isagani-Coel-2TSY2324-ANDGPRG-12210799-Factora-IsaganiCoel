using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIHandler : MonoBehaviour {
    public Slider bgmSlider, sfxSlider;

    public void ToggleMusic() {
        AudioManager.instance.ToggleMusic();
    }
    public void ToggleSound(){
        AudioManager.instance.ToggleSound();
    }

    public void BGMVolume() {
        AudioManager.instance.BGMvolume(bgmSlider.value);
    }
    public void SFXVolume() {
        AudioManager.instance.SFXvolume(sfxSlider.value);
    }

}
