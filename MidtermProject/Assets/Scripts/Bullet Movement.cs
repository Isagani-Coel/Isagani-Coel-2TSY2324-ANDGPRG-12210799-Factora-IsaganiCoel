using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class BulletMovement : MonoBehaviour {
    [SerializeField] float speed;
    void Update() {
        this.transform.Translate(Vector2.left * speed * Time.deltaTime);
    }
}
