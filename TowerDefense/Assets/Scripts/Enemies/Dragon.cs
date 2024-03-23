using UnityEngine;

public class Dragon : Enemy {
    protected override void Awake() { base.Awake(); }
    protected override void Start() {
        HP = 200;
        monster = Monster.DRAGON;
        monsterTier = MonsterTier.BOSS;
        monsterType = MonsterType.AIR;

        base.Start();
        SoundManager.instance.Play("Dragon etb", 1);
    }
    protected override void Update() {  base.Update(); }

    protected override void OnDestroy() { base.OnDestroy(); }
    protected override void OnTriggerEnter(Collider other) { base.OnTriggerEnter(other); }
}
