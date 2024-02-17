using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine.UIElements;
using UnityEngine;

public class WaveSpawner : MonoBehaviour {
    [Header("Enemy Spawn Settings")]
    [SerializeField] GameObject enemyPrefab;
    [SerializeField] float spawnRate, spawnInterval;
    public int enemyCount;

    [Header("Enemy Wave UI")]
    public TextMeshProUGUI waveCountText;
    public TextMeshProUGUI enemiesEscapedText;
    bool waveDone = true;
    int waveCount = 0;

    [HideInInspector]
    public static int enemiesEscaped = 0;
    public const int maxEscapes = 10;
    public static int killCount = 0;

    void Start() {
        /* DEFAULT VALUES IN CASE I FORGET TO ADD THEM IN THE INSPECTOR
        spawnRate = 1.0f;
        spawnInterval = 3.0f;
        */
    }
    void Update() {
        waveCountText.text = "Wave " + waveCount;
        enemiesEscapedText.text = "Escaped: " + enemiesEscaped + " / " + maxEscapes;
        
        if (waveDone)
            StartCoroutine(waveSpawner());
    }

    IEnumerator waveSpawner() {
        waveDone = false;

        for (int i = 0; i < enemyCount; i++) {
            GameObject clone = Instantiate(enemyPrefab) as GameObject;
            clone.transform.position = new Vector3(Random.Range(-36f, 36f), 70f, 0f);

            // ADDS A DELAY IN BETWEEN SPAWNING THE ENEMIES
            yield return new WaitForSeconds(spawnRate);
        }

        // MAKES WAVES HARDER THE MORE IT CONTINUES
        spawnRate -= 0.1f;
        enemyCount++;
        waveCount++;

        // ADDS A DELAY IN BETWEEN WAVES
        yield return new WaitForSeconds(spawnInterval);

        // BREAKS OUT OF THE FUNCTION SO IT DOESN'T SPAWN TOO MANY ENEMIES AT ONCE
        waveDone = true;
    }
}

/*
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.PackageManager;
using UnityEngine;
using UnityEngine.UIElements;
*/

/*
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEditor.PackageManager;
using UnityEngine.UIElements;
using UnityEngine;
*/