using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;

public enum SpawnState { SPAWNING, WAITING, COUNTING, FINISHED };

public class WaveManager : MonoBehaviour {
    public static WaveManager instance;

    [Header("Wave Settings")]
    [SerializeField] Wave[] waves;
    [SerializeField] TextMeshProUGUI waveCountdownText;
    [SerializeField] float waveInterval = 5f, waveCountdown;
    [SerializeField] SpawnState state = SpawnState.COUNTING;
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
        if (state == SpawnState.FINISHED) return;

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
        // waveCountdownText.text = string.Format("{0:00.00}", waveCountdown);
    }

    IEnumerator StartWave(Wave wave) {
        state = SpawnState.SPAWNING;

        for (int i = 0; i < wave.enemyCount; i++) {
            // SPAWNS THE DIFERENT BOSSES
            if (i == wave.enemyCount - 1) {
                if (GameManager.waveCount == 2) SpawnManager.instance.Spawn(0);
                if (GameManager.waveCount == 5) SpawnManager.instance.Spawn(3);
            }

            // SPAWNS NORMAL ENEMIES
            if (GameManager.waveCount < 3) SpawnManager.instance.Spawn(1);
            else SpawnManager.instance.Spawn(Random.Range(1, 3));

            // TAKES A SHORT BREAK AFTER EACH ITERATION
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
            state = SpawnState.FINISHED;
            return;
        }

        GameManager.waveCount++;
    }
}

[System.Serializable]
public class Wave {
    public string name;
    public int enemyCount;
    public float spawnRate;
}