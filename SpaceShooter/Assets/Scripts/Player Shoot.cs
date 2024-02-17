using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PlayerShoot: MonoBehaviour {
    [SerializeField] Transform nozzle;
    [SerializeField] GameObject[] ammo;
    [SerializeField] Player player;
    int input = 0;

    void Start() {
        player = GetComponent<Player>();
    }

    void Update() {
        selectBulletType();
        attack();      
    }

    void selectBulletType() { // CHANGE BOOLET FIRING PATTERN BASED ON A KEYPRESS
        if (Input.GetKey(KeyCode.Alpha1)) {
            input = 0;
            player.getBulletType("Normal");
        }
        if (Input.GetKey(KeyCode.Alpha2)) {
            input = 1;
            player.getBulletType("Enchanced");
        }
        if (Input.GetKey(KeyCode.Alpha3)) {
            input = 2;
            player.getBulletType("Hasty");
        }
        if (Input.GetKey(KeyCode.Alpha4)) {
            input = 3;
            player.getBulletType("Giant");
        }
    }

    void attack() { // BOOLET FIRING LOGIC
        if (Input.GetKeyDown(KeyCode.Space)) {
            switch (input) {
                case 0: basicAttack(); break;
                case 1: doubleBullets(); break;
                case 2: hastyBullets(); break;
                case 3: giantBullet(); break;
                default:
                    Debug.Log("THE GIVEN INPUT OUTSIDE THE RANGE");
                    break;
            }
        }
    }
    
    void basicAttack() {
        AudioManager.instance.PlaySound("Normal Bullet");
        GameObject boolet = Instantiate(ammo[input], nozzle.position, nozzle.rotation) as GameObject;
        boolet.transform.position = nozzle.transform.position;

        Destroy(boolet, 2.5f);
    }
    void doubleBullets() {
        AudioManager.instance.PlaySound("Normal Bullet");
        GameObject boolet1 = Instantiate(ammo[input]) as GameObject;
        boolet1.transform.position = new Vector3(nozzle.position.x - 1, nozzle.position.y, nozzle.position.z);

        AudioManager.instance.PlaySound("Normal Bullet");
        GameObject boolet2 = Instantiate(ammo[input]) as GameObject;
        boolet2.transform.position = new Vector3(nozzle.position.x + 1, nozzle.position.y, nozzle.position.z);

        Destroy(boolet1, 3.0f);
        Destroy(boolet2, 3.0f); 
    }
    void hastyBullets() {
        AudioManager.instance.PlaySound("Hasty Bullet");
        GameObject boolet = Instantiate(ammo[input], nozzle.position, nozzle.rotation) as GameObject;
        boolet.transform.position = nozzle.transform.position;

        Destroy(boolet, 2.0f);
    }
    void giantBullet() {
        AudioManager.instance.PlaySound("Giant Bullet");
        GameObject boolet = Instantiate(ammo[input], nozzle.position, nozzle.rotation) as GameObject;
        boolet.transform.position = nozzle.transform.position;

        Destroy(boolet, 10.0f);
    }
}
