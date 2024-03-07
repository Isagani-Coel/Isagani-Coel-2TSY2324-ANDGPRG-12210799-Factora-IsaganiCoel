using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public enum TowerState { START, PLACED };

public abstract class Tower : MonoBehaviour {

    [Header("Tower Settings")]
    [SerializeField] protected float range = 7f, turnSpeed = 10f;
    [SerializeField] protected float attackRate = 1f, attackCountdown = 0f;
    protected float searchCountdown = 0f, searchInterval = 2f;

    [Header("Setup Fields")]
    [SerializeField] protected Material towerMat;
    [SerializeField] protected GameObject bulletPrefab;
    [SerializeField] protected Transform pivot, nozzle, target;
    [SerializeField] protected TowerState state = TowerState.START;

    protected virtual void Start() {
        /* DEFAULT VALUES
            range = 7
            turnSpd = 10
            attack Rate & Countdown = 0
            search Interval = 2 & Countdown = 0
        */

        InvokeRepeating("Retarget", searchCountdown, searchInterval);
    }

    protected virtual void Update() {
        if (target == null) return;

        if (state == TowerState.PLACED) {
            towerMat.color = Color.white;
            LockInAtTarget();
        }

        if (attackCountdown <= 0f) {
            if (state == TowerState.PLACED) Attack();
            attackCountdown = 1f / attackRate;
        }
        else attackCountdown -= Time.deltaTime;
    }

    protected void Retarget() {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        GameObject nearestEnemy = null;
        float shortestDistance = Mathf.Infinity;

        foreach (GameObject enemy in enemies) {
            float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);

            // FINDS AN ENEMY CLOSEST TO THE TURRET
            if (distanceToEnemy < shortestDistance) {
                shortestDistance = distanceToEnemy;
                nearestEnemy = enemy;
            }
        }

        if (nearestEnemy != null && shortestDistance <= range) 
            target = nearestEnemy.transform;
        else target = null;
    }

    protected void LockInAtTarget() {
        Vector3 direction = target.position - transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(direction);

        // ADD SMOOTHNESS WHEN LOOKING AT THE ENEMY
        Vector3 rotation = Quaternion.Lerp(pivot.rotation, lookRotation, turnSpeed * Time.deltaTime).eulerAngles;
        pivot.rotation = Quaternion.Euler(0f, rotation.y, 0f);
    }

    protected abstract void Attack();

    public void Buildable() {
        if (state == TowerState.PLACED) return;

        towerMat.color = Color.green; 
    }
    public void Unbuildable() {
        if (state == TowerState.PLACED) return;
        
        towerMat.color = Color.red;
    }
    public void Build() { 
        state = TowerState.PLACED;
        towerMat.color = Color.white;
    }

    void OnDrawGizmosSelected() {
        Gizmos.color = Color.white;
        Gizmos.DrawWireSphere(transform.position, range);
    }
}

/*
using UnityEditor.Experimental.GraphView;
 */