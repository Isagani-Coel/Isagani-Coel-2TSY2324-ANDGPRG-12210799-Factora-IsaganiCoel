using TMPro;
using UnityEngine;
using UnityEngine.AI;

public enum MonsterType { GROUND, AIR };
public enum MonsterTier { NORMAL, BOSS };
public enum Monster { GOBLIN, RAVEN, HOG, DRAGON };

public abstract class Enemy : MonoBehaviour {

    [Header("Setup Fields")]
    [SerializeField] NavMeshAgent agent;
    [SerializeField] TextMeshProUGUI healthText;
    [SerializeField] protected Monster monster;
    protected MonsterType monsterType;
    protected MonsterTier monsterTier;

    protected int HP, maxHP;
    protected bool isAlive, isFrozen, isBurning;
    
    public void SetTarget(Transform target) { agent.SetDestination(target.position); }
    public void SetMonster(Monster m) { monster = m; }
    
    public bool GetIsFrozen() { return isFrozen; }
    public bool GetIsBurning() { return isBurning; }
    public MonsterType GetMonsterType() { return monsterType; }

    protected virtual void Awake () { agent = GetComponent<NavMeshAgent>(); }
    protected virtual void Start() {
        if (monsterTier == MonsterTier.BOSS) HP += 100;
        isAlive = true;
        maxHP = HP;
        healthText.text = HP + "/" + maxHP;
    }
    protected virtual void Update() { 
        if (!isAlive) {
            Die();
            return;
        }
    }

    public void TakeDMG(int dmg) {
        HP -= dmg;
        healthText.text = HP + "/" + maxHP;
        if (HP <= 0) {
            HP = 0;
            isAlive = false;
        }
    }
    public void DoFrostEffect(float countdown) {
        if (isFrozen || isBurning) return;
        isFrozen = true; // anti effect-stacking measure 
        SoundManager.instance.Play("IT Effect", 2);

        float normalSpeed = agent.GetComponent<NavMeshAgent>().speed;

        if (countdown > 0f) {
            agent.GetComponent<NavMeshAgent>().speed *= 0.5f;
            countdown -= Time.deltaTime;
        }

        if (countdown <= 0f) {
            agent.GetComponent<NavMeshAgent>().speed = normalSpeed;
            isFrozen = false;
        }
    }
    public void DoFireEffect(float countdown) {
        if (isFrozen || isBurning) return; 
        isBurning = true; // anti effect-stacking measure
        SoundManager.instance.Play("FT Effect", 2);

        if (countdown > 0f) { 
            HP--;
            countdown -= Time.deltaTime;
        }

        if (countdown <= 0f) isBurning = false;
    }

    protected void Die() {
        SpawnManager.instance.Despawn(gameObject);
        Destroy(gameObject);
    }

    protected virtual void OnDestroy() {
        if (isAlive) {
            SoundManager.instance.Play("Damage", 0);
            GameManager.instance.LooseHP();
            return;
        }

        switch (monsterTier) {
            case MonsterTier.NORMAL: GameManager.instance.EarnGold(10); break;
            case MonsterTier.BOSS: GameManager.instance.EarnGold(50); break;
            default: break;
        }

        switch (monsterType) {
            case MonsterType.GROUND: GameManager.instance.EarnGold(10); break;
            case MonsterType.AIR: GameManager.instance.EarnGold(20); break;
            default: break;
        }

        switch (monster) {
            case Monster.GOBLIN: SoundManager.instance.Play("Goblin ltb", 1); break;
            case Monster.RAVEN: SoundManager.instance.Play("Raven ltb", 1); break;
            case Monster.HOG: SoundManager.instance.Play("Hog ltb", 1); break;
            case Monster.DRAGON: SoundManager.instance.Play("Dragon ltb", 1); break;
            default: break;
        }

    }
    protected virtual void OnTriggerEnter(Collider other) {
        if (other.gameObject.CompareTag("Finish"))
            Die();
    }
}
