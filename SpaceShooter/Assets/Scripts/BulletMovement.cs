using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMovement : MonoBehaviour {
    [SerializeField] float speed;

    void Start() {
    
    }

    void Update() {
        this.transform.Translate(Vector3.up * speed * Time.deltaTime);
    }
}
