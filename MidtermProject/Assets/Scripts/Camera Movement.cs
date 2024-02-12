using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour {
    public GameObject target;

    void Update() {
        this.transform.position = new Vector3(target.transform.position.x + 4.5f, target.transform.position.y + 3f, target.transform.position.z - 10);
    }
}
