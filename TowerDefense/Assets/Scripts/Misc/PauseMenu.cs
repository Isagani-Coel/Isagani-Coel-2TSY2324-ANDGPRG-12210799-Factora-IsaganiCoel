using UnityEngine;

public class PauseMenu : MonoBehaviour {
    public static bool isPaused = false;
    public static bool canPause = true;
    public GameObject PauseMenuUI;

    /*
    void Update() {
        if (Input.GetKeyDown(KeyCode.Escape) && canPause)
            if (isPaused) Resume();
            else Pause();
    } // */
    
    public void TogglePause() {
        PauseMenuUI.SetActive(!PauseMenuUI.activeSelf);

        if (PauseMenuUI.activeSelf) Time.timeScale = 0f;
        else Time.timeScale = 1f;
    }

    void Resume() {
        // AudioManager.instance.PlaySound("Pause");
        PauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
    }

    void Pause() {
        // AudioManager.instance.PlaySound("Pause");
        PauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;
    }
}
