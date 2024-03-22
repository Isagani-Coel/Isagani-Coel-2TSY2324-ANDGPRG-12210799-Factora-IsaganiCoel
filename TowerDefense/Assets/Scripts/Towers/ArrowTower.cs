using UnityEngine;

public class ArrowTower : Tower {
    protected override void Start() {
        name = "Arrow Tower";
        upgradeCosts = new int[] { 30, 15, 20, 25 };
        dmg = 5;
        range = 10f;
        attackRate = 1f;

        base.Start();
    }
    protected override void Update() { base.Update(); }

    protected override void Attack() {
        CreateBullet();

        if (tempBullet != null) {
            tempBullet.SetType(BulletType.ARROW);
            tempBullet.SetTarget(target);
            tempBullet.SetTower(this);
        }
    }

    protected override void OnDrawGizmosSelected() { base.OnDrawGizmosSelected(); }
    protected override void OnDestroy() { base.OnDestroy(); }
    protected override void OnMouseEnter() { base.OnMouseEnter(); }
    protected override void OnMouseExit() { base.OnMouseExit(); }
    protected override void OnTriggerEnter(Collider other) { base.OnTriggerEnter(other); }
    protected override void OnTriggerExit(Collider other) { base.OnTriggerExit(other); }
}
