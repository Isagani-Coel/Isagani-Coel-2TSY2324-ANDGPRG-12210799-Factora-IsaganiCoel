using System.Runtime.InteropServices.WindowsRuntime;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

/// -FINAL BUILD DUE DATE- (03.22.24)
/// -END DATE FOR EVERYTHING- (03.27.24)

/* -IMPORTANT- (03.20.24) 23:57
    - Add a main menu & game over UI
    - Fix the restart button

    -ADDITIONAL FEATURES LATER ON-
    - Make the towers have small movement if they don't have a target (random rotation)
    - restart the game (make it harder) once all waves have been cleared
*/

public class GameManager : MonoBehaviour {
    public static GameManager instance;

    [Header("Setup Fields")]
    [SerializeField] GameObject goal;
    [SerializeField] GameObject winPanel, loosePanel;
    [SerializeField] TextMeshProUGUI lifeText, goldText, waveText;
    int gold = 100, HP = 20;
    bool isAlive;

    [HideInInspector]
    public static int waveCount = 0;
    
    public int GetHP() { return HP; }
    public int GetGold() { return gold; }
    public bool GetIsAlive() { return isAlive; }
    public GameObject GetGoal() { return goal; }

    void Awake() {
        if (instance == null) {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else Destroy(gameObject);
    }
    void Start() {
        HP = 20;
        gold = 100;
        waveCount = 0;
        isAlive = true;

        lifeText.text = "Life: " + HP;
        goldText.text = "Gold: " + gold;
        waveText.text = "Wave " + waveCount + "/6";

        winPanel.SetActive(false);
        loosePanel.SetActive(false);
    }
    void Update() {
        if (!isAlive) {
            loosePanel.SetActive(true);
            Time.timeScale = 0f;

            if (Input.GetKeyDown(KeyCode.Escape)) Restart();

            return;
        }

        lifeText.text = "Life: " + HP;
        goldText.text = "Gold: " + gold;
        waveText.text = "Wave " + (waveCount + 1).ToString() + "/6";

        if (WaveManager.instance.GetState() == SpawnState.FINISHED) {
            Debug.Log("ALL WAVES HAVE BEEN CLEARED");
            Time.timeScale = 0f;
            winPanel.SetActive(true);
        }
    }

    public void EarnGold(int amt) { 
        gold += amt;
        if (gold < 0) gold = 0;
        goldText.text = "Gold: " + gold;
    }
    public void LooseHP() {
        SoundManager.instance.Play("Damage", 0);
        HP--;
        lifeText.text = "Life: " + HP;

        if (HP <= 0) {
            HP = 0;
            isAlive = false;
        }
    }
    void Restart() {
        Start();

        Time.timeScale = 1f;
        SpawnManager.instance.GetEnemies().Clear();
        BuildManager.instance.GetPlacedTowers().Clear();

        SceneManager.LoadScene(0);
    }
}
