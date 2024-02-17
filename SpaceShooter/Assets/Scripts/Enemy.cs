using TMPro;
using UnityEngine;

public class Enemy : MonoBehaviour {
    [Header("Enemy Settings")]
    public TextMeshProUGUI health;
    float speed;
    int maxHP, HP;

    void Awake() {
        maxHP = 50;
        HP = maxHP;
        speed = Random.Range(10f, 30f);

        /* DEFAULT VALUES IN CASE I FORGET
            speed = 20f;
            attackRate = 2f;
            bulletDespawnTime = 2f;
        */
    }

    void Start() {
        health.text = HP + "/" + maxHP;
    }

    void Update() { 
        this.transform.Translate(Vector3.down * speed * Time.deltaTime);

        // SELF-DESTRUCTS IF THE PLAYER DIDN'T KILL IT
        if (this.transform.position.y < -75f) {
            WaveSpawner.enemiesEscaped++;
            Destroy(this.gameObject);
        }
    }

    void TakeDMG(int dmg) {
        HP -= dmg;
        if (HP <= 0) {
            Player.score += 100;
            AudioManager.instance.PlaySound("Death");
            Destroy(this.gameObject);
        }
        health.text = HP + "/" + maxHP;
    }

    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.name.Contains("Basic Bullet")) TakeDMG(5);
        if (other.gameObject.name.Contains("Enchanced Bullet")) TakeDMG(10);
        if (other.gameObject.name.Contains("Hasty Bullet")) TakeDMG(10);
        if (other.gameObject.name.Contains("Giant Bullet")) TakeDMG(20);

        // DELETES ONLY THE BOOLETS THAT COLLIDE WITH THE ENEMY
        if (other.gameObject.name.Contains("Player")) return;

        Destroy(other.gameObject);
    }
}
/*
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
*/