using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireTower : Tower {
    protected override void Start() { base.Start(); }
    protected override void Update() { base.Update(); }
    protected override void Attack() {
        GameObject bulletClone = Instantiate(bulletPrefab, nozzle.transform.position, nozzle.transform.rotation);
        Bullet b = bulletClone.GetComponent<Bullet>();

        if (b != null) {
            b.SetTarget(target);
            b.SetType(BulletType.FIRE);
        }
    }
}
