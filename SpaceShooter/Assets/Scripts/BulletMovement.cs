using UnityEngine;

public class BulletMovement : MonoBehaviour {
    [SerializeField] float speed;
    void Update() {
        this.transform.Translate(Vector3.up * speed * Time.deltaTime);
    }
}

/*
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
*/
