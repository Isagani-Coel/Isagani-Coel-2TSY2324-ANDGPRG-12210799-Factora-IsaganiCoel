using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.Net.Http.Headers;
using System.Runtime.InteropServices;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;

public enum MonsterType { GROUND, AIR };
public enum MonsterTier { NORMAL, BOSS };

public class Enemy : MonoBehaviour {

    [Header("Setup Fields")]
    [SerializeField] NavMeshAgent agent;
    [SerializeField] TextMeshProUGUI healthText;
    [SerializeField] MonsterType monsterType;
    [SerializeField] MonsterTier monsterTier;

    int HP = 50, maxHP;
    float effectCountdown;
    bool isAlive = true;
    bool isFrozen, isBurning;
    public void SetTarget(Transform target) { agent.SetDestination(target.position); }
    public MonsterType GetMonsterType() { return monsterType; }

    void Awake () {
        agent = GetComponent<NavMeshAgent>();
    }

    void Start() {
        if (monsterTier == MonsterTier.BOSS)
            HP += 50;

        maxHP = HP;
        healthText.text = HP + "/" + maxHP;

        effectCountdown = Mathf.Clamp(effectCountdown, 0, Mathf.Infinity);
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

    public void DoFrostEffect() {
        if (isBurning || isFrozen) return;
        isFrozen = true; // anti effect-stacking measure
        effectCountdown = 8f;

        if (effectCountdown > 0f) {
            agent.GetComponent<NavMeshAgent>().speed =
            agent.GetComponent<NavMeshAgent>().speed * 0.8f;
            effectCountdown -= Time.deltaTime;
        }

        isFrozen = false;
        return;
    }

    public void DoFireEffect() {
        if (isFrozen || isBurning) return;
        isBurning = true; // anti effect-stacking measure
        effectCountdown = 5f;

        if (effectCountdown > 0f) {
            // SPEEDS-UP BECAUSE IT'S ASS IS ON FIRE
            agent.GetComponent<NavMeshAgent>().speed =
            agent.GetComponent<NavMeshAgent>().speed * 1.2f;
            TakeDMG(1);
            effectCountdown -= Time.deltaTime;
        }

        isBurning = false;
        return;
    }

    void Die() {
        SpawnManager.instance.Despawn(gameObject);
        Destroy(gameObject);
    }

    
    
    void OnDestroy() {
        if (isAlive) {
            // Debug.Log("AN ENEMY HAS BREACHED THE CORE"); 
            GameManager.instance.LooseHP();
        }
        else {
            // Debug.Log("YOU HAVE KILLED AN ENEMY");
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
