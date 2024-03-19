using TMPro;
using UnityEngine;

/// -MILESTONE 3 DUE DATE- (03.15.24)
/// -FINAL BUILD DUE DATE- (03.22.24)
/// -END DATE FOR EVERYTHING- (03.27.24)

/* -IMPORTANT- (03.18.24)
    1. add UI for the different upgrade paths
    2. add a confirmation button when upgrade/selling a tower

    -TOWER UPGRADE FEATURES-
        1. Increased Range
        2. Increaed Damage
*/

public class GameManager : MonoBehaviour {
    public static GameManager instance;

    [Header("Setup Fields")]
    [SerializeField] GameObject goal;
    [SerializeField] TextMeshProUGUI lifeText, goldText, waveText;
    int gold = 100, HP = 20;
    bool isAlive = true, isPaused = false;

    [HideInInspector]
    public static int waveCount = 0;
    
    public int GetHP() { return HP; }
    public int GetGold() { return gold; }
    public GameObject GetGoal() { return goal; }

    void Awake() {
        if (instance == null) {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else Destroy(gameObject);
    }
    void Start() {
        lifeText.text = "Life: " + HP;
        goldText.text = "Gold: " + gold;
        waveText.text = "Wave " + waveCount + "/6";
    }
    void Update() {
        testing();
        if (!isAlive) return;

        lifeText.text = "Life: " + HP;
        goldText.text = "Gold: " + gold;
        waveText.text = "Wave " + (waveCount + 1).ToString() + "/6";

        // if (WaveManager.instance.GetState() == SpawnState.FINISHED) Debug.Log("ALL WAVES HAVE BEEN CLEARED");
    }

    public void EarnGold(int amt) { 
        gold += amt;
        if (gold < 0) gold = 0;
        goldText.text = "Gold: " + gold;
    }
    public void LooseHP() { 
        HP--;
        lifeText.text = "Life: " + HP;
        if (HP <= 0) 
            isAlive = false;
    }

    void TogglePause() {
        if (Input.GetKeyDown(KeyCode.Space)) {
            if (isPaused) {
                Time.timeScale = 1f;
                isPaused = false;
            }
            else if (!isPaused) {
                Time.timeScale = 0f;
                isPaused = true;
            }
        }

        if (Input.GetKeyDown(KeyCode.L)) {
            isPaused = false;
            Time.timeScale = 2f;
        }
    }
    void testing() {
        if (Input.GetKeyDown(KeyCode.UpArrow)) EarnGold(10);
        if (Input.GetKeyDown(KeyCode.DownArrow)) EarnGold(-10);

        if (Input.GetKeyDown(KeyCode.Return)) LooseHP();
        // Debug.Log("Wave " + (waveCount + 1).ToString());

        if (Input.GetKeyDown(KeyCode.L)) {
            isPaused = false;
            Time.timeScale = 2f;
        }
        TogglePause();
    }
}
