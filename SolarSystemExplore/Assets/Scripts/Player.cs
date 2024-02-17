using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour {
    [Header("Player Settings")]
    public CharacterController controller;
    public Transform cam;
    public float speed = 6f;
    public float turnSmoothTime = 0.1f;
    float turnSmoothVelocity;

    void Update() {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

        Vector3 direction = new Vector3(h, 0f, v).normalized;

        if (direction.magnitude >= 0.1f) {
            // PLAYER ROTATES WITHT THE GIVEN DIRECTION
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;

            // ADDING SMOOTHNESS TO THE DIRECTION CHANGE
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0, angle, 0);

            // MAKES THE PLAYER MOVE IN THE GIVEN DIRECTION 
            Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward; 

            controller.Move(moveDir.normalized * speed * Time.deltaTime);
        }
    }
}
