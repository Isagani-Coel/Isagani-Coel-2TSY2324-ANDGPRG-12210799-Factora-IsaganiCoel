using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro;
using Unity.VisualScripting;
using UnityEditor;
using UnityEditor.Rendering;
using UnityEditor.U2D;
using UnityEditor.UI;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class Player : MonoBehaviour { 
    Vector2 vecGravity;
    Vector3 spawnPoint = new Vector3(-1f, 2f, 0f);    

    [Header("Player Settings")]
    Rigidbody2D rb;
    SpriteRenderer sr;
    Color originalColor;
    public float speed;
    private float h; // GETS INPUT FROM THE HORIZONTAL AXIS

    [Header("Jump Settings")]
    public Transform groundCheck;
    public LayerMask groundLayer;
    public float jumpHeight;
    [SerializeField] float fallSpeed, jumpMultiplier, jumpTime;
    private bool isGrounded, isJumping, doubleJump, doubleJumpSkill;
    private float jumpCounter;

    void Start() {
        vecGravity = new Vector2(0, -Physics2D.gravity.y);
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();

        originalColor = Color.white;
        doubleJumpSkill = false;
    }

    void Update() {
        // MOVEMENT
        h = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(h * speed, rb.velocity.y);

        jump();
        outOfBounds();

        // FOR TESTING PURPOSES
        test();
    }
    void jump() {
        isGrounded = Physics2D.OverlapCapsule(groundCheck.position, new Vector2(0f, 0.3f), CapsuleDirection2D.Horizontal, 0, groundLayer);

        // INPUT LOGIC
        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow)) {
            if (isGrounded) {
                AudioManager.instance.PlaySound("Jump");
                jumpCounter = 0;
                rb.velocity = new Vector2(rb.velocity.x, jumpHeight);
                doubleJump = true;
                isJumping = true;
            }
            else if (doubleJump && doubleJumpSkill) {
                AudioManager.instance.PlaySound("Jump");
                rb.velocity = new Vector2(rb.velocity.x, jumpHeight);
                jumpTime = 0.1f;
                doubleJump = false;
            }
        } 
        if (Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.UpArrow)) {
            isJumping = false;
            jumpCounter = 0;

            if (rb.velocity.y > 0)
                rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.6f);
        }

        // MAKES YOU FALL FASTER AFTER YOU JUMP
        if (rb.velocity.y < 0)
            rb.velocity -= vecGravity * fallSpeed * Time.deltaTime;

        // JUMP HEIGHT VARIES ON HOW LONG THE BUTTON IS PRESSED
        if (rb.velocity.y > 0 && isJumping) {
            float t = jumpCounter / jumpTime;
            float currentJumpM = jumpMultiplier;

            jumpCounter += Time.deltaTime;
            if (jumpCounter > jumpTime)
                isJumping = false;

            // SMOOTHER JUMP MOVEMENT
            if (t > 0.5) currentJumpM = jumpMultiplier * (1 - t);
            rb.velocity += vecGravity * currentJumpM * Time.deltaTime;
        }
    }
    void outOfBounds() { 
        // PLAYER WILL GO BACK TO THE ORIGINAL SPAWNPOINT WHEN THEY GO OUT OF BOUNDS
        if (this.transform.position.y > 5f) respawn();
        if (this.transform.position.y < -5f) respawn();
        if (this.transform.position.x < -5f) respawn();

        // FOR TESTING PURPOSES
        if (Input.GetKeyDown(KeyCode.Backspace)) respawn();
    }
    void respawn(){
        this.transform.position = spawnPoint;
    }
    void test() {
        if (Input.GetKeyDown(KeyCode.Escape))
            SceneController.Restart();

        // 1-1
        if (Input.GetKeyDown(KeyCode.Alpha1))
            SceneController.LoadScene(2);

        // 1-2
        if (Input.GetKeyDown(KeyCode.Alpha2))
            SceneController.LoadScene(3);

        // 1-3
        if (Input.GetKeyDown(KeyCode.Alpha3))
            SceneController.LoadScene(4);
    }
    void OnTriggerEnter2D (Collider2D other) {
        if (other.gameObject.name.Contains("Double Jump Boost")) {
            AudioManager.instance.PlaySound("Boost");
            doubleJumpSkill = true;
            sr.color = Color.cyan;
            other.gameObject.SetActive(false);
        }
        if (other.gameObject.name.Contains("Projectile Boost")) {
            AudioManager.instance.PlaySound("Boost");
            doubleJumpSkill = false;
            sr.color = Color.red;
            other.gameObject.SetActive(false);
        }
        if (other.gameObject.name.Contains("Spike"))
            respawn();

        if (other.gameObject.name.Contains("Bullet")){
            Destroy(other.gameObject);
            respawn();
        }

        if (other.gameObject.CompareTag("Finish")) {    
            AudioManager.instance.PlaySound("Level Complete");
            SceneController.NextLevel();
        }        
    }
}