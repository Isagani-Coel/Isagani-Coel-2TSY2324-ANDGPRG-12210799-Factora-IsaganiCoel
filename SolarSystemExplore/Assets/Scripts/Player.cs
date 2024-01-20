using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour {
	[SerializeField] float moveSpeed;

	// START IS CALLED BEFORE THE FIRST FRAME UPDATE
	void Start() {
		moveSpeed = 50f;
	}

	// UPDATE IS CALLED ONCE PER FRAME
	void Update() {
		// BASIC MOVEMENT
        if (Input.GetKey(KeyCode.W)) this.transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);
        if (Input.GetKey(KeyCode.A)) this.transform.Translate(Vector3.left * moveSpeed * Time.deltaTime);
        if (Input.GetKey(KeyCode.S)) this.transform.Translate(Vector3.back * moveSpeed * Time.deltaTime);
        if (Input.GetKey(KeyCode.D)) this.transform.Translate(Vector3.right * moveSpeed * Time.deltaTime);
    }
}
