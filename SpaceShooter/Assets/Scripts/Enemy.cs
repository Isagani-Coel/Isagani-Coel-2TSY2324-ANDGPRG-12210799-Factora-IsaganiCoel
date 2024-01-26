using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Enemy : MonoBehaviour {
    [SerializeField] TextMeshProUGUI health;
    private Vector3 pos;
    private float HP, maxHP;

    void Start() {
        maxHP = 100.0f;
        HP = maxHP;

        pos = this.transform.position;
        UpdateText();
    }

    void TakeDMG(int dmg) {
        HP -= dmg;
        if (HP <= 0)
            doDeath();

        UpdateText();
    }

    void doDeath() {
        Destroy(this.gameObject);
    }

    void UpdateText() {
        health.text = HP + "/" + maxHP;
    }

    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.name.Contains("basic_bullet"))    TakeDMG(5);
        if (other.gameObject.name.Contains("enhanced_bullet")) TakeDMG(10);
        if (other.gameObject.name.Contains("hasty_bullet"))    TakeDMG(10);
        if (other.gameObject.name.Contains("giant_bullet"))    TakeDMG(50);

        Destroy(other.gameObject);
    }
}
