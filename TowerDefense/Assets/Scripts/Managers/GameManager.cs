using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

/* -NOTES- (03.07.24)
    -FEATURES-
    1. Shop System so you can upgrade/sell towers
    2. Upgrade paths
    3. Shop UI
    4. Anything Upgrades-related

    -TOWER FEATUERS-
    Arrow Tower: 1) Short range high dps 2) long range low dps
    Cannon: Can upgrade to a double cannon  

   -BUGS- (03.03.24)
    1. When you create a new arrow tower, all previous towers share the same color
    2. The towre can already attack before buying it.

    -MILESTONE 2 DUE DATE- (03.08.24)
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
        waveText.text = "Wave " + waveCount;
    }

    void Update() {
        // testing();
        if (!isAlive) return;

        lifeText.text = "Life: " + HP;
        goldText.text = "Gold: " + gold;
        waveText.text = "Wave " + (waveCount + 1).ToString();

        TogglePause();

        if (WaveManager.instance.GetState() == SpawnState.FINISHED) Debug.Log("ALL WAVES HAVE BEEN CLEARED");
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

        /*
        if (Input.GetKeyDown(KeyCode.Space)) {
            if (Time.timeScale != 0f) Time.timeScale = 1f;
            if (Time.timeScale != 1f) Time.timeScale = 0f;
        } // */

        TogglePause();

        // RESTART
        if (Input.GetKeyDown(KeyCode.Escape))
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

        if (Input.GetKeyDown(KeyCode.Return)) SpawnManager.instance.Spawn(3);
        if (Input.GetKeyDown(KeyCode.Backspace)) SpawnManager.instance.Spawn(0);
    }
}
