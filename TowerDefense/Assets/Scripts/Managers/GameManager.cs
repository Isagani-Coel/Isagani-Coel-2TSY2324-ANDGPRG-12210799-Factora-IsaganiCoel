using TMPro;
using UnityEngine;

/// -FINAL BUILD DUE DATE- (03.22.24)
/// -END DATE FOR EVERYTHING- (03.27.24)

/* -IMPORTANT- (03.20.24) 23:57
    - Implement the sounds to the game
    - Get sounds for Crystal Core
    - Add a main menu & game over UI
    - Fix restart button
    - set additional colliders so the tower can't be stacked on top of one another

    -ADDITIONAL FEATURES LATER ON-
    -  Add decorations to the map
    - Make the towers have small movement if they don't have a target (random rotation)
    - restart the game (make it harder) once all waves have been cleared
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
        if (Input.GetKeyDown(KeyCode.UpArrow)) EarnGold(50);
        if (Input.GetKeyDown(KeyCode.DownArrow)) EarnGold(-10);
    }
}
