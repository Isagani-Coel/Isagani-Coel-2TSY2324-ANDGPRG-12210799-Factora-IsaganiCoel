using System.Collections;
using TMPro;
using UnityEngine;

public enum SpawnState { SPAWNING, WAITING, COUNTING, FINISHED };

public class WaveManager : MonoBehaviour {
    public static WaveManager instance;

    [Header("Wave Settings")]
    [SerializeField] SpawnState state = SpawnState.COUNTING;
    [SerializeField] Wave[] waves;
    [SerializeField] TextMeshProUGUI waveCountdownText;
    [SerializeField] float waveInterval = 5f, waveCountdown;
    float searchInterval = 1f, searchCountdown;

    public SpawnState GetState() { return state; }

    void Awake() {
        if (instance == null) {
            instance = this;
            DontDestroyOnLoad(this);
        }
        else Destroy(gameObject);
    }
    void Start() {
        waveCountdown = waveInterval;
        searchCountdown = searchInterval;
    }
    void Update() {
        if (state == SpawnState.FINISHED || BuildManager.instance.GetPlacedTowers().Count == 0 || !GameManager.instance.GetIsAlive()) return;

        // CHECKS IF THE ENEMIES FROM THE CURRENT ARE STILL ALIVE
        if (state == SpawnState.WAITING) {
            if (!WaveHasEnemies()) {
                WaveCompleted();

                if (state != SpawnState.FINISHED) state = SpawnState.COUNTING;
                return;
            }
            else return;
        }

        // MAIN WAVE LOGIC
        if (waveCountdown <= 0f) {
            if (state != SpawnState.SPAWNING) {
                StartCoroutine(StartWave(waves[GameManager.waveCount]));
                return;
            }
        }

        waveCountdown -= Time.deltaTime;
        waveCountdown = Mathf.Clamp(waveCountdown, 0f, Mathf.Infinity);
    }

    IEnumerator StartWave(Wave wave) {
        state = SpawnState.SPAWNING;

        for (int i = 0; i < wave.enemyCount; i++) {
            // BOSS
            if (i == wave.enemyCount - 1) {
                if (GameManager.waveCount == 2) SpawnManager.instance.Spawn(0); // hog
                if (GameManager.waveCount == 5) SpawnManager.instance.Spawn(3); // dragon
            }

            // NORMAL ENEMY
            if (GameManager.waveCount < 3) SpawnManager.instance.Spawn(1);
            else {
                if (Random.Range(0, 2) < 0.3) SpawnManager.instance.Spawn(2);
                else SpawnManager.instance.Spawn(1);
            }

            yield return new WaitForSeconds(1f / wave.spawnRate);
        }

        state = SpawnState.WAITING;
        yield break;
    }
    bool WaveHasEnemies() {
        searchCountdown -= Time.deltaTime;

        if (searchCountdown <= 0f) {
            searchCountdown = searchInterval;
            if (SpawnManager.instance.GetEnemies().Count == 0)
                return false;
        }
        return true;
    }
    void WaveCompleted() {
        state = SpawnState.COUNTING;
        waveCountdown = waveInterval;

        if (GameManager.waveCount == waves.Length - 1) {
            SoundManager.instance.Play("Win", 0);
            state = SpawnState.FINISHED;
            return;
        }

        SoundManager.instance.Play("Wave Done", 0);
        GameManager.waveCount++;
    }
}

[System.Serializable]
public class Wave {
    public string name;
    public int enemyCount;
    public float spawnRate;
}