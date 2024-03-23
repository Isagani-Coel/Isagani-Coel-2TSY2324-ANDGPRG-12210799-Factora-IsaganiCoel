using UnityEngine;

public class Raven : Enemy {
    protected override void Awake() { base.Awake(); }
    protected override void Start() {
        HP = 70;
        monster = Monster.RAVEN;
        monsterTier = MonsterTier.NORMAL;
        monsterType = MonsterType.AIR;

        base.Start();
        SoundManager.instance.Play("Raven etb", 1);
    }
    protected override void Update() { base.Update(); }

    protected override void OnDestroy() { base.OnDestroy(); }
    protected override void OnTriggerEnter(Collider other) { base.OnTriggerEnter(other); }
}
