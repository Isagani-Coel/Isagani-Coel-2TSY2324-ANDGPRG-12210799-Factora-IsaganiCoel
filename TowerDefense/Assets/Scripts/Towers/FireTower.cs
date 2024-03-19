public class FireTower : Tower {
    protected override void Start() {
        name = "Fire Tower";
        upgradeCosts = new int[] { 50, 30, 35, 40 };
        dmg = 5;
        range = 8f;
        attackRate = 1.1f;
        effectCountdown = 5f;
        // tempBullet.SetTower(this);

        base.Start();
    }
    protected override void Update() { base.Update(); }
    protected override void Attack() {
        CreateBullet();

        if (tempBullet != null) {
            tempBullet.SetTower(this);
            tempBullet.SetTarget(target);
            tempBullet.SetType(BulletType.FIRE);
        }
    }

    protected override void OnDrawGizmosSelected() { base.OnDrawGizmosSelected(); }
    protected override void OnDestroy() { base.OnDestroy(); }
    protected override void OnMouseEnter() { base.OnMouseEnter(); }
    protected override void OnMouseExit() { base.OnMouseExit(); }
}
