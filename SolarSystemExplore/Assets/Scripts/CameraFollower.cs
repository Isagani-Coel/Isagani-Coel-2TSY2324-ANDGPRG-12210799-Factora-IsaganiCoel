using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollower : MonoBehaviour {
    [SerializeField] Transform target;
    [SerializeField] float distance;
    [SerializeField] float speed;

    // START IS CALLED BEFORE THE FIRST FRAME OF UPDATE
    void Start() {
        distance = 10f;
        speed = 50f;
    }

    // UPDATE IS CALLED ONCE PER FRAME
    void LateUpdate() {
        this.transform.LookAt(target);
       
        if(Vector3.Distance(this.transform.position, target.position) > distance)
            this.transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }
}
