using UnityEngine;

public class Cannon : Tower {
    protected override void Start() {
        name = "Cannon";
        upgradeCosts = new int[] { 80, 50, 60, 70 };
        dmg = 20;
        range = 9f;
        attackRate = 0.8f;
        type = TowerType.CANNON;

        base.Start();
    }
    protected override void Update() {
        if (target == null) return;

        // THE CANNON CAN ONLY TARGET GROUND ENEMIES
        if (target.GetComponent<Enemy>().GetMonsterType() != MonsterType.AIR) LockInAtTarget();

        if (attackCountdown <= 0f) {
            if (state == TowerState.PLACED) Attack();
            attackCountdown = 1f / attackRate;
        }
        else attackCountdown -= Time.deltaTime;
    }

    protected override void Attack() {
        CreateBullet();

        // THE CANNON CAN ONLY TARGET GROUND ENEMIES
        if (target.GetComponent<Enemy>().GetMonsterType() == MonsterType.AIR) return;
        SoundManager.instance.Play("CT Attack", 2);

        if (tempBullet != null) {
            tempBullet.SetTower(this);
            tempBullet.SetTarget(target);
            tempBullet.SetType(BulletType.CANNONBALL);
        }
    }


    protected override void OnDrawGizmosSelected() { base.OnDrawGizmosSelected(); }
    protected override void OnDestroy() { base.OnDestroy(); }
    protected override void OnMouseEnter() { base.OnMouseEnter(); }
    protected override void OnMouseExit() { base.OnMouseExit(); }
}
