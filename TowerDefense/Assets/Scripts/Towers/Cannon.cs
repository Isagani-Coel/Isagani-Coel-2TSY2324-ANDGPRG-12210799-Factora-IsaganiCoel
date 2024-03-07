using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cannon : Tower {
    protected override void Start() { base.Start(); }
    protected override void Update() {
        if (target == null) return;

        if (state == TowerState.PLACED) {
            towerMat.color = Color.white;
            LockInAtTarget();
        }

        // THE CANNON WILL ONLY TARGET GROUND ENEMIES
        if (target.GetComponent<Enemy>().GetMonsterType() != MonsterType.AIR) LockInAtTarget();

        if (attackCountdown <= 0f) {
            if (state == TowerState.PLACED) Attack();
            attackCountdown = 1f / attackRate;
        }
        else attackCountdown -= Time.deltaTime;

    }
    protected override void Attack() {
        GameObject bulletClone = Instantiate(bulletPrefab, nozzle.transform.position, nozzle.transform.rotation);
        Bullet b = bulletClone.GetComponent<Bullet>();

        // THE CANNON WILL ONLY TARGET GROUND ENEMIES
        if (target.GetComponent<Enemy>().GetMonsterType() == MonsterType.AIR) return;

        if (b != null) {
            b.SetTarget(target);
            b.SetType(BulletType.CANNONBALL);
        }
    }
}
