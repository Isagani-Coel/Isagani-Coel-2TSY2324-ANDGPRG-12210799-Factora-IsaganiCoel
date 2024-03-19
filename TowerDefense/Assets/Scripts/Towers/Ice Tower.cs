public class IceTower : Tower {
    protected override void Start() {
        name = "Ice Tower";
        upgradeCosts = new int[] { 50, 30, 35, 40 };
        dmg = 3;
        range = 8f;
        attackRate = 0.9f; 
        effectCountdown = 8f;

        base.Start();
    }
    protected override void Update() { base.Update(); }
    protected override void Attack() {
        CreateBullet();

        if (tempBullet != null) {
            tempBullet.SetTower(this);
            tempBullet.SetTarget(target);
            tempBullet.SetType(BulletType.ICE);
        }
    }

    protected override void OnDrawGizmosSelected() { base.OnDrawGizmosSelected(); }
    protected override void OnDestroy() { base.OnDestroy(); }
    protected override void OnMouseEnter() { base.OnMouseEnter(); }
    protected override void OnMouseExit() { base.OnMouseExit(); }
}
