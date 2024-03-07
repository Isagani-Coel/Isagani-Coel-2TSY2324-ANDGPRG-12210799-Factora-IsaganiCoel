using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public class ArrowTower : Tower {
    protected override void Start() { base.Start(); }
    protected override void Update() { base.Update(); }
    protected override void Attack() {
        GameObject bulletClone = Instantiate(bulletPrefab, nozzle.transform.position, nozzle.transform.rotation);
        Bullet b = bulletClone.GetComponent<Bullet>();

        if (b != null) {
            b.SetType(BulletType.ARROW);
            b.SetTarget(target);
        }
    }
}

/*
using Cinemachine.Editor;
*/