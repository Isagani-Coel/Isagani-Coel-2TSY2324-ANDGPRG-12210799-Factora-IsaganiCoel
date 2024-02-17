using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Collision : MonoBehaviour {
    public GameObject UI;

    void OnTriggerEnter (Collider other) {
        if (other.gameObject.name.Contains("Player"))
            UI.SetActive(true);
    }

    void OnTriggerExit(Collider other) {
        if (other.gameObject.name.Contains("Player"))
            UI.SetActive(false);
    }
}