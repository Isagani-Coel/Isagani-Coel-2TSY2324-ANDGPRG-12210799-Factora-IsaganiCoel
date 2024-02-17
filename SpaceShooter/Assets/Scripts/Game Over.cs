using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour {
    public static bool isGameOver = false;
    public GameObject GameOverUI;
    public Player player;

    void Awake() {
        player = GetComponent<Player>();
    }

    void Update() {
        if (!player.isAlive) {
            doGameOver();

            if (Input.GetKeyDown(KeyCode.Return))
                Restart();
        }
    }
    void doGameOver() {
        GameOverUI.SetActive(true);
        PauseMenu.canPause = false;
        Time.timeScale = 0f;
    }
    void Restart() {
        GameOverUI.SetActive(false);
        PauseMenu.canPause = true;
        Time.timeScale = 1f;
        WaveSpawner.enemiesEscaped = 0;
        AudioManager.instance.PlayMusic("Theme");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}


/*
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
*/