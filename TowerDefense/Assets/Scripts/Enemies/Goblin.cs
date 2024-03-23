using UnityEngine;

public class Goblin : Enemy {
    protected override void Awake() { base.Awake(); }
    protected override void Start() {
        HP = 30;
        monster = Monster.GOBLIN;
        monsterTier = MonsterTier.NORMAL;
        monsterType = MonsterType.GROUND;

        base.Start();
        SoundManager.instance.Play("Goblin etb", 1);
    }
    protected override void Update() { base.Update(); }

    protected override void OnDestroy() { base.OnDestroy(); }
    protected override void OnTriggerEnter(Collider other) { base.OnTriggerEnter(other); }
}
