using TMPro;
using UnityEngine;

[System.Serializable]
public enum Stat { START, DMG, RANGE, DPS };

public class UIHandler : MonoBehaviour {
    public static bool isPaused, canPause, is2xSpeed, isSelling, isUpgrading;

    [Header("UI Panels")]
    [SerializeField] TextMeshProUGUI confirmationText;
    [SerializeField] GameObject pausePanel, sidebarPanel, upgradePanel, confirmPanel, statPanel;

    Stat chosenStat;

    void Start() {
        isPaused = false;
        canPause = true;
        is2xSpeed = false;
        isSelling = false;
        isUpgrading = false;
        chosenStat = Stat.START;
    }
    void Update() {
        if (Input.GetKeyDown(KeyCode.Tab)) sidebarPanel.SetActive(!sidebarPanel.activeSelf);
        
        if (Input.GetKeyDown(KeyCode.Escape)) {
            if (!isPaused) Pause();
            else Resume();
        }
    }

    public void SetStat(int i) {

        // idk why you can't use enums for the parameter in the buttons
        switch (i) { 
            case 0: chosenStat = Stat.DPS; break;
            case 1: chosenStat = Stat.DMG; break;
            case 2: chosenStat = Stat.RANGE; break;
            default: break;
        }
        
        statPanel.SetActive(false);
        confirmPanel.SetActive(true);
    }
    public void Cancel() {
        canPause = true;
        upgradePanel.SetActive(false);
        confirmPanel.SetActive(false);
        statPanel.SetActive(false);
    }
    public void EnableUpgrade() {
        canPause = false;
        isUpgrading = true;
        upgradePanel.SetActive(false);
        statPanel.SetActive(true);
    }
    public void EnableSell() {
        canPause = false;
        isSelling = true;
        upgradePanel.SetActive(false);
        confirmPanel.SetActive(true);
    }

    public void ConfirmAction() {
        if (isSelling) {
            confirmationText.text = "Confirm sell?";
            BuildManager.instance.SellTower(BuildManager.instance.GetSelectedTower());
            confirmPanel.SetActive(false);
        }
        
        if (isUpgrading) {
            confirmationText.text = "Confirm upgrade?";
            BuildManager.instance.UpgradeTower(BuildManager.instance.GetSelectedTower(), chosenStat);
            confirmPanel.SetActive(false);
        }

        isSelling = false;
        isUpgrading = false;
        chosenStat = Stat.START;
    }

    // NOT YET WORKING
    public void Restart() {
        Debug.LogError("this feature isn't working properly yet");
        return;

        // SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); 
    }
    public void Pause() {
        if (!canPause) return;

        isPaused = true;
        pausePanel.SetActive(true);
        Time.timeScale = 0f;
    }
    public void Resume() {
        if (!canPause) return;
        
        isPaused = false;
        pausePanel.SetActive(false);

        if (is2xSpeed) {
            Time.timeScale = 2f;
            return;
        }
        Time.timeScale = 1f;
    }
    public void Quit() { Application.Quit(); }
    public void Toggle2xSpeed() {
        if (!is2xSpeed) {
            Time.timeScale = 2f;
            is2xSpeed = true;
        }
        else {
            Time.timeScale = 1f;
            is2xSpeed = false;
        }
    }
}
