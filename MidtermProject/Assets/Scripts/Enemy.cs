using UnityEngine;

public class Enemy : MonoBehaviour {
    [Header("Enemy Settings")]
    [SerializeField] GameObject bullet, target, nozzle;
    [SerializeField] float attackRate = 2f;
    [SerializeField] float bulletDespawnTime = 2f;

    void Start() {
        InvokeRepeating(nameof(Attack), this.attackRate, this.attackRate);
    }

    void Attack() {
        GameObject b = Instantiate(bullet, nozzle.transform.position, nozzle.transform.rotation) as GameObject;
        AudioManager.instance.PlaySound("Bullet");

        Destroy(b, bulletDespawnTime);
    }
}


/*
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine; 
*/
