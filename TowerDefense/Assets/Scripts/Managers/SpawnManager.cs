using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class SpawnManager : MonoBehaviour {
    public static SpawnManager instance;

    [Header("Enemy Spawn Settings")]
    [SerializeField] GameObject[] enemyPrefab;
    [SerializeField] Transform spawnPoint;
    [SerializeField] List<GameObject> enemies = new List<GameObject>();
    [SerializeField] float spawnRate, spawnInterval;
    int enemyCount;

    /*
    [Header("Enemy Wave UI")]
    public TextMeshProUGUI waveCountText;
    public TextMeshProUGUI enemiesEscapedText; // */
    // bool waveDone = false;
    // int waveCount = 0;

    [HideInInspector]
    public static int enemiesEscaped = 0;
    public const int maxEscapes = 10;
    public static int killCount = 0; 

    void Awake() {
        instance = this;
        /*
        if (instance == null) {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else Destroy(gameObject); // */
    }

    void Start() {
        // SPAWNS 3 GOBLINS AT THE START OF THE GAME
        // for (int i = 0; i < 3; i++) SpawnEnemy(0);
    }

    void Update() {
        testing();
        // if (waveDone) StartCoroutine(waveSpawner());


        
    }

    void SpawnEnemy(int i) {
        GameObject enemyObj = (GameObject)Instantiate(enemyPrefab[i], spawnPoint.position, Quaternion.identity);
        enemyObj.tag = tag;
        enemyObj.transform.position = spawnPoint.position;
        enemyObj.GetComponent<Enemy>().SetTarget(GameManager.instance.CrystalCore.transform);
        enemies.Add(enemyObj);
    }

    public void RemoveEnemy(GameObject obj) {
		enemies.Remove(obj);
	}
    /*
    IEnumerator waveSpawner() {
        waveDone = false;

        for (int i = 0; i < waveCount * 2; i++) {
            // CAN SPAWN EITEHR A GOBLIN OR A RAVEN
            SpawnEnemy(Random.Range(0, 1));

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
    } // */

    void testing() {
        if (Input.GetKeyDown(KeyCode.Alpha1)) SpawnEnemy(0); // goblin
        if (Input.GetKeyDown(KeyCode.Alpha2)) SpawnEnemy(1); // raven
        if (Input.GetKeyDown(KeyCode.Alpha3)) SpawnEnemy(2); // hog
        if (Input.GetKeyDown(KeyCode.Alpha4)) SpawnEnemy(3); // dragon

        // if (Input.GetKeyDown(KeyCode.R)) SpawnEnemy(1);
    }
}
