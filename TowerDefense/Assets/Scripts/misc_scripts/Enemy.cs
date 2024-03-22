using TMPro;
using UnityEngine;
using UnityEngine.AI;

public enum MonsterType { GROUND, AIR };
public enum MonsterTier { NORMAL, BOSS };

public class Enemy : MonoBehaviour {

    [Header("Setup Fields")]
    [SerializeField] NavMeshAgent agent;
    [SerializeField] TextMeshProUGUI healthText;
    [SerializeField] MonsterType monsterType;
    [SerializeField] MonsterTier monsterTier;

    int HP = 50, maxHP;
    bool isAlive = true;
    bool isFrozen, isBurning;
    
    public void SetTarget(Transform target) { agent.SetDestination(target.position); }
    
    public bool GetIsFrozen() { return isFrozen; }
    public bool GetIsBurning() { return isBurning; }
    public MonsterType GetMonsterType() { return monsterType; }

    void Awake () { agent = GetComponent<NavMeshAgent>(); }
    void Start() {
        if (monsterTier == MonsterTier.BOSS) HP += 100;

        maxHP = HP;
        healthText.text = HP + "/" + maxHP;
    }
    void Update() { 
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

        if (countdown > 0f) {
            // TakeDMG(1);
            HP--;
            countdown -= Time.deltaTime;
        }

        if (countdown <= 0f) isBurning = false;
    }

    void Die() {
        SpawnManager.instance.Despawn(gameObject);
        Destroy(gameObject);
    }    
    
    void OnDestroy() {
        if (isAlive) GameManager.instance.LooseHP();
        else {
            // AudioManager.instance.PlaySound("Goblin Death");
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
        }
    }
    void OnTriggerEnter(Collider other) {
        if (other.gameObject.CompareTag("Finish"))
            Die();
    }
}
