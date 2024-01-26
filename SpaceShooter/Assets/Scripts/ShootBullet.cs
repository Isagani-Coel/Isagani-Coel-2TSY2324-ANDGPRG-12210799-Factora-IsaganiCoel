using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class ShootBullet : MonoBehaviour {
    [SerializeField] Transform nozzle;
    [SerializeField] GameObject b1, b2, b3, b4;

    private GameObject[] ammo;
    private int input;

    void Start() {
        input = 0;
    }

    void Update() {
        // CHANGE BOOLET FIRING PATTERN BASED ON A KEYPRESS
        if (Input.GetKey(KeyCode.Alpha1)) input = 0; 
        if (Input.GetKey(KeyCode.Alpha2)) input = 1; 
        if (Input.GetKey(KeyCode.Alpha3)) input = 2; 
        if (Input.GetKey(KeyCode.Alpha4)) input = 3;

        // ACTUAL BOOLET FIRING
        if (Input.GetKeyDown(KeyCode.Space)) { 
            switch (input) {
                case 0: basicAttack();   break;
                case 1: doubleBullets(); break;
                case 2: hastyBullets();  break;
                case 3: giantBullet();   break;
                default:                 break;
            }
        }
    }

    void basicAttack() {
        GameObject boolet = Instantiate(b1, nozzle.position, nozzle.rotation) as GameObject;
        boolet.transform.position = nozzle.transform.position;

        Destroy(boolet, 2.5f);
    }
    void doubleBullets() {
        GameObject boolet1 = Instantiate(b2) as GameObject;
        boolet1.transform.position = new Vector3(nozzle.position.x - 1, nozzle.position.y, nozzle.position.z);

        GameObject boolet2 = Instantiate(b2) as GameObject;
        boolet2.transform.position = new Vector3(nozzle.position.x + 1, nozzle.position.y, nozzle.position.z);

        Destroy(boolet1, 3.0f);
        Destroy(boolet2, 3.0f);
    }
    void hastyBullets() {
        GameObject boolet = Instantiate(b3, nozzle.position, nozzle.rotation) as GameObject;
        boolet.transform.position = nozzle.transform.position;

        Destroy(boolet, 2.0f);
    }
    void giantBullet() {
        GameObject boolet = Instantiate(b4, nozzle.position, nozzle.rotation) as GameObject;
        boolet.transform.position = nozzle.transform.position;

        Destroy(boolet, 10.0f);
    }
}
