using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
    [SerializeField] float speed;

    void Start() {
        speed = 20.0f;
    }

    void Update() {
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
            this.transform.Translate(Vector3.up * speed * Time.deltaTime);

        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
            this.transform.Translate(Vector3.left * speed * Time.deltaTime);

        if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
            this.transform.Translate(Vector3.down * speed * Time.deltaTime);

        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
            this.transform.Translate(Vector3.right * speed * Time.deltaTime);
    }

    // Adding this here just in case I reuse this block of code later on
    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.name.Contains("Enemy")) {
            // Debug.Log("AN ENEMY HAS HIT YOU!");
            // Destroy(this.gameObject);
        }
    }
}
