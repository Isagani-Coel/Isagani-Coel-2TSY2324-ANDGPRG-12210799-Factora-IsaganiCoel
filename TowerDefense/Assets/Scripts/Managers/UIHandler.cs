using TMPro;
using UnityEngine;

public enum Stat { START, DMG, RANGE, DPS };

public class UIHandler : MonoBehaviour {
    public static UIHandler instance;

    [Header("UI Panels")]
    [SerializeField] TextMeshProUGUI confirmationText;
    [SerializeField] GameObject pausePanel, sidebarPanel, upgradePanel, confirmPanel, statPanel;

    public static bool canPause;
    bool isPaused, is2xSpeed, isSelling, isUpgrading;
    Stat chosenStat;

    void Awake() {
        if (instance == null) {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else Destroy(gameObject);
    }
    void Start() {
        isPaused = false;
        canPause = true;
        is2xSpeed = false;
        isSelling = false;
        isUpgrading = false;
        chosenStat = Stat.START;
    }
    void Update() {
        if (!GameManager.instance.GetIsAlive() || WaveManager.instance.GetState() == SpawnState.FINISHED) return;

        // HOTKEYS FOR THE MENU
        if (Input.GetKeyDown(KeyCode.Tab)) {
            SoundManager.instance.Play("Select", 0);
            sidebarPanel.SetActive(!sidebarPanel.activeSelf);
        }
        if (Input.GetKeyDown(KeyCode.Space)) {
            SoundManager.instance.Play("Select", 0);
            if (!isPaused) Pause();
            else Resume();
        }
        if (Input.GetKeyDown(KeyCode.Return)) Toggle2xSpeed();
    }

    public void SetStat(int i) {
        SoundManager.instance.Play("Select", 0);
        
        switch (i) { // idk why you can't use enums for the parameter in the buttons 
            case 0: chosenStat = Stat.DPS; break;
            case 1: chosenStat = Stat.DMG; break;
            case 2: chosenStat = Stat.RANGE; break;
            default: break;
        }
        
        statPanel.SetActive(false);
        confirmPanel.SetActive(true);
    }
    public void Cancel() {
        SoundManager.instance.Play("Select", 0);
        canPause = true;
        isSelling = false;
        isUpgrading = false;
        
        upgradePanel.SetActive(false);
        confirmPanel.SetActive(false);
        statPanel.SetActive(false);
    }
    public void EnableUpgrade() {
        SoundManager.instance.Play("Select", 0);
        Tower t = BuildManager.instance.GetSelectedTower().GetComponent<Tower>();

        if (GameManager.instance.GetGold() < t.GetUpgradeCosts(t.GetLevel())) {
            Debug.LogError("YOU DON'T HAVE ENOUGH GOLD TO UPGRADE THIS TOWER!");
            Cancel();
            return;
        }

        canPause = false;
        isUpgrading = true;
        upgradePanel.SetActive(false);
        statPanel.SetActive(true);
    }
    public void EnableSell() {
        SoundManager.instance.Play("Select", 0);

        canPause = false;
        isSelling = true;
        upgradePanel.SetActive(false);
        confirmPanel.SetActive(true);
    }
    public void ConfirmAction() {
        if (isSelling) {
            BuildManager.instance.SellTower(BuildManager.instance.GetSelectedTower());
            SoundManager.instance.Play("Sell", 0);
        }
        if (isUpgrading) {
            BuildManager.instance.UpgradeTower(BuildManager.instance.GetSelectedTower(), chosenStat);
            SoundManager.instance.Play("Upgrade", 0);
        }

        confirmPanel.SetActive(false);
        isSelling = false;
        isUpgrading = false;
        chosenStat = Stat.START;
    }

    public void Pause() {
        if (!canPause) return;
        SoundManager.instance.Play("Select", 0);
        isPaused = true;
        pausePanel.SetActive(true);
        Time.timeScale = 0f;
    }
    public void Resume() {
        if (!canPause) return;
        SoundManager.instance.Play("Select", 0);
        isPaused = false;
        pausePanel.SetActive(false);

        if (is2xSpeed) {
            Time.timeScale = 2f;
            return;
        }
        Time.timeScale = 1f;
    }
    public void Quit() {
        SoundManager.instance.Play("Select", 0);
        Application.Quit();
    }
    public void Toggle2xSpeed() {
        SoundManager.instance.Play("Select", 0);

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
