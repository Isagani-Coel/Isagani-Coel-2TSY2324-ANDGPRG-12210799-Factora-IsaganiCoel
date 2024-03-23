using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour {
    public static SpawnManager instance;

    [Header("Spawner Settings")]
    [SerializeField] Transform spawnPoint;
    [SerializeField] GameObject[] enemyPrefabs;
    List<GameObject> enemies = new List<GameObject>();

    public List<GameObject> GetEnemies() { return enemies; }
    
    void Awake() {
        if (instance == null) {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else Destroy(gameObject);
    }

    public void Spawn(int i) {
        if (!GameManager.instance.GetIsAlive() || WaveManager.instance.GetState() == SpawnState.FINISHED) return;

        GameObject enemy = Instantiate(enemyPrefabs[i], spawnPoint.position, Quaternion.identity);
        enemy.GetComponent<Enemy>().SetTarget(GameManager.instance.GetGoal().transform);
        enemies.Add(enemy);
    }
    public void Despawn(GameObject enemy) { enemies.Remove(enemy); }
}