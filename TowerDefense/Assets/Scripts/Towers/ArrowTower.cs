public class ArrowTower : Tower {
    protected override void Start() {
        name = "Arrow Tower";
        upgradeCosts = new int[] { 30, 15, 20, 25 };
        dmg = 5;
        range = 10f;
        attackRate = 1f;
        type = TowerType.ARROW;

        base.Start();
    }
    protected override void Update() { base.Update(); }

    protected override void Attack() {
        CreateBullet();
        SoundManager.instance.Play("AT Attack", 2);

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
}
