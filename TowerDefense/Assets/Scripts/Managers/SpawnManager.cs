using System.Collections;
using System.Collections.Generic;
using System.Net.Http.Headers;
using Unity.VisualScripting;

using UnityEngine;
using UnityEngine.SceneManagement;

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
        GameObject e = Instantiate(enemyPrefabs[i], spawnPoint.position, Quaternion.identity);
        e.GetComponent<Enemy>().SetTarget(GameManager.instance.GetGoal().transform);
        enemies.Add(e);
    }

    public void Despawn(GameObject clone) {
        enemies.Remove(clone);
    }
}

/*
using UnityEditor.Build.Content; 
*/