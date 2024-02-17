using UnityEngine;

public class PauseMenu : MonoBehaviour {
    public static bool isPaused = false;
    public static bool canPause = true;
    public GameObject PauseMenuUI;

    void Update() {
        if (Input.GetKeyDown(KeyCode.Escape) && canPause)
            if (isPaused) Resume();
            else          Pause();
    }

    void Resume() {
        AudioManager.instance.PlaySound("Pause");
        PauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
    }

    void Pause() {
        AudioManager.instance.PlaySound("Pause");
        PauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;
    }
}

/*
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
*/