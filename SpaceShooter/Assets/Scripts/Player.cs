using TMPro;
using UnityEngine;

public class Player : MonoBehaviour {
    [Header("Player Settings")]
    [SerializeField] float speed;
    public TextMeshProUGUI bulletType, points;
    string booletType = "Normal";
    Vector3 spawnPoint = new Vector3(0f, -50f, 0f);

    [HideInInspector] // makes the variables public but not seen in the Inspector
    public bool isAlive = true;
    public static int score;

    void Start() {
        // speed = 20.0f;
        bulletType.text = "Bullet Type: " + booletType;
        points.text = "Score: " + score;
        transform.position = spawnPoint;
    }

    void Update() {
        if (!isAlive) return;

        movement();
        points.text = "Score: " + score;

        // INCREASED SPEED AFTER POINT THRESHOLD IS REACHED
        if (score % 500 == 0 && score != 0) {
            AudioManager.instance.PlaySound("Level up");

            if (speed < 60f) speed++;
        }

        // PLAYER DIES ONLY WHEN TOO MANY ENEMIES HAVE ESCAPED
        if (WaveSpawner.enemiesEscaped == WaveSpawner.maxEscapes) {
            AudioManager.instance.PlaySound("Game Over");
            AudioManager.instance.bgmSRC.Stop();
            isAlive = false;
        }
    }

    void movement() {
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow)){
            if (transform.position.y < 64f)
                transform.Translate(Vector3.down * speed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow)){
            if (transform.position.x > -36f)
                transform.Translate(Vector3.left * speed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow)){
            if (transform.position.y > -52f)
                transform.Translate(Vector3.up * speed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow)){
            if (transform.position.x < 36f)
                transform.Translate(Vector3.right * speed * Time.deltaTime);
        }
    }

    public void getBulletType(string boolet) {
        booletType = boolet;
        bulletType.text = "Bullet Type: " + booletType;
    }

    void OnTriggerEnter(Collider other) {
        if (other.gameObject.name.Contains("Enemy")) {
            AudioManager.instance.PlaySound("Death");
            AudioManager.instance.bgmSRC.Stop();
            isAlive = false;
            transform.position = new Vector3(100, 100, 0);
        }
    }
}

/*
using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using TMPro;
using Unity.VisualScripting;
using UnityEditor.Build.Content;
using UnityEditor.U2D;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements; 
*/