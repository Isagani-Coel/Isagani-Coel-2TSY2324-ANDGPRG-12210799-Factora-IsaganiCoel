using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour {
    [Header("Camera Settings")]
    [SerializeField] CinemachineVirtualCamera cam;
    float moveSpeed = 10f;
    float rotateSpeed = 100f;
    Vector3 origin = new Vector3(28f, 5f, 35f);

    // DRAG PAN SETTINGS
    Vector2 lastMousePos;
    float dragPanSpeed = 0.1f;
    bool dragPanMoveActive;

    /* VIRTUAL CAMERA OFFSET SETTINGS
        xOff = 0
        yOff = 20
        zOff = -10
        FOV = 50

        * REMOVE ALL DAMPING SO IT HAS A SNAPPY FEEL
    */

    void Start() {
        // SETS THE SPAWN POINT AT THE BOTTOM CENTER OF THE MAP
        transform.position = origin;
    }

    void Update() {
        Rotate();
        Movement();

        // RESET CAMERA POSITION & ROTATION
        if (Input.GetKeyDown(KeyCode.Backspace)) {
            transform.position = origin;
            transform.rotation = Quaternion.Euler(0, 180, 0);
        }
    }

    void Movement() {
        Vector3 inputDir = new Vector3(0, 0, 0);

        if (Input.GetMouseButtonDown(1)) {
            dragPanMoveActive = true;
            lastMousePos = Input.mousePosition;
        }
        if (Input.GetMouseButtonUp(1)) dragPanMoveActive = false;

        // PANNING THE CAMERA USING THE MOUSE
        if (dragPanMoveActive) {
            Vector2 mouseMovementDelta = (Vector2)Input.mousePosition - lastMousePos;

            inputDir.x = mouseMovementDelta.x * dragPanSpeed * -1;
            inputDir.z = mouseMovementDelta.y * dragPanSpeed * -1;

            /* CLAMP THE POSITION SO IT DOESN'T GO OUT OF BOUNDS 
            inputDir.x = Mathf.Clamp(inputDir.x, 22f, 40f);
            inputDir.z = Mathf.Clamp(inputDir.z, 18f, 44f); // */

            lastMousePos = Input.mousePosition;

            // clamp x form 22 - 40
            // clamp z from 18 - 44
        }

        Vector3 moveDir = transform.forward * inputDir.z + transform.right * inputDir.x;
        transform.position += moveDir * moveSpeed * Time.deltaTime;
    }

    void Rotate() {
        float rotateDir = Input.mouseScrollDelta.y * 5;

        transform.eulerAngles += new Vector3(0f, rotateDir * rotateSpeed * Time.deltaTime, 0f);
    }
}