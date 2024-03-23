using UnityEngine;

public class Hog : Enemy {
    protected override void Awake() { base.Awake(); }
    protected override void Start() {
        HP = 150;
        monster = Monster.HOG;
        monsterTier = MonsterTier.BOSS;
        monsterType = MonsterType.GROUND;

        base.Start();
        SoundManager.instance.Play("Hog etb", 1);
    }
    protected override void Update() { base.Update(); }

    protected override void OnDestroy() { base.OnDestroy(); }
    protected override void OnTriggerEnter(Collider other) { base.OnTriggerEnter(other); }
}
