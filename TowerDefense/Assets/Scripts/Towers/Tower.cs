using System.Transactions;
using UnityEngine;

public enum TowerState { START, PLACED, SELECTED };

public abstract class Tower : MonoBehaviour {

    [Header("Setup Fields")]
    [SerializeField] protected TowerState state = TowerState.START;
    [SerializeField] protected Transform target;
    [SerializeField] protected GameObject bulletPrefab;

    [Header("Levels")]
    [SerializeField] protected GameObject[] turret;
    [SerializeField] protected Transform[] pivots, nozzles;

    [Header("Plane Settings")]
    [SerializeField] protected GameObject plane;
    [SerializeField] protected Material planeMat;

    // TOWER VARIBLES 
    protected Bullet tempBullet;
    protected const int MAX_LEVEL = 4;
    protected int[] upgradeCosts;
    protected int sellValue = 0;
    protected int level = 1; // counter for each tower level
    protected int towerValue = 0; // used when you're planning to sell the tower
    protected int towerIndex = 0; // the different looks for each tower
    protected bool isMaxed, isColliding;

    // BASE TOWER STATS
    protected const float turnSpeed = 20f;
    protected float attackCountdown = 0f;

    // UPGRADABLE TOWER STATS
    protected int dmg = 0;
    protected float range, attackRate, effectCountdown;


    public int GetTowerValue() { return sellValue; }
    public int GetDMG() { return dmg; }
    public int GetUpgradeCosts(int i) { return upgradeCosts[i]; }
    public int GetLevel() { return level; }
    public int GetMaxLevel() { return MAX_LEVEL; }
    public bool GetIsMaxed() { return isMaxed; }
    public bool GetIsColliding() { return isColliding; }
    public float GetEffectCountdown() { return effectCountdown; }
    public GameObject GetPlane() { return plane; }


    protected virtual void Start() {
        isMaxed = false;
        isColliding = false;
        sellValue += upgradeCosts[0] / 2;
        InvokeRepeating("Retarget", 0f, 2f);
    }
    protected virtual void Update()  {
        testing();

        if (target == null) return;
        LockInAtTarget();

        /*
        if (state == TowerState.PLACED) {
            plane.SetActive(false);
            LockInAtTarget();
        } */

        if (attackCountdown <= 0f) {
            if (state != TowerState.START) Attack();
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

            // THE TURRET WILL ALWAYS LOOK FOR THE CLOSET ENEMY AFTER THE PREVIOUS ENEMY GOES OUT OF RANGE
            if (distanceToEnemy < shortestDistance) {
                shortestDistance = distanceToEnemy;
                nearestEnemy = enemy;
            }
        }

        // FINDS THE NEAREST ENEMY AND FIRES AT THAT
        if (nearestEnemy != null && shortestDistance <= range) 
            target = nearestEnemy.transform;
        else target = null;
    }
    protected void LockInAtTarget() {
        Vector3 direction = target.position - transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(direction);

        // ADD SMOOTHNESS WHEN LOOKING AT THE ENEMY
        Vector3 rotation = Quaternion.Lerp(pivots[towerIndex].rotation, lookRotation, turnSpeed * Time.deltaTime).eulerAngles;
        pivots[towerIndex].rotation = Quaternion.Euler(0f, rotation.y, 0f);
    }

    protected void CreateBullet() {
        GameObject bulletClone = (GameObject)Instantiate(bulletPrefab, nozzles[towerIndex].position, nozzles[towerIndex].rotation);
        tempBullet = bulletClone.GetComponent<Bullet>();
    }
    protected abstract void Attack();

    public void Buildable() { planeMat.color = Color.green; }
    public void Unbuildable() { planeMat.color = Color.red; }
    public void Build() { 
        state = TowerState.PLACED;
        plane.SetActive(false);
        gameObject.layer = LayerMask.NameToLayer("Selectable");
        gameObject.tag = "Tower";
    }

    public void Selected() {
        if (state == TowerState.START) return;

        state = TowerState.SELECTED;
        plane.SetActive(true);
        planeMat.color = Color.yellow;
    }
    public void Deselected() {
        if (state == TowerState.START) return;
    
        state = TowerState.PLACED;
        plane.SetActive(false);
        planeMat.color = Color.green;
    }

    public void Upgrade(Stat stat) {
        level++;
        
        if (level == MAX_LEVEL) { // LEVEL 4 DESIGN (MAX LEVEL)
            isMaxed = true;
            towerIndex = 2;
            turret[0].SetActive(false);
            turret[1].SetActive(false);
            turret[2].SetActive(true);
        }
        else if (level == 1) { // LEVEL 1 DESIGN
            towerIndex = 0;
            turret[0].SetActive(true);
            turret[1].SetActive(false);
            turret[2].SetActive(false);
        }
        else { // LEVEL 2 & 3 DESIGN
            towerIndex = 1;
            turret[0].SetActive(false);
            turret[1].SetActive(true);
            turret[2].SetActive(false);
        }

        switch (stat) {
            case Stat.DMG:
                Debug.Log("DAMAGE HAS BEEN INCREASED!");
                dmg += 5;
                break;
            case Stat.RANGE:
                Debug.Log("RANGE HAS BEEN INCREASED!");
                range += 0.1f;
                break;
            case Stat.DPS:
                Debug.Log("ATTACK TIME HAS BEEN DECREASED!");
                attackRate += 0.5f;
                break;
            default: break;
        }

        sellValue += upgradeCosts[towerIndex];
    }

    protected virtual void OnDrawGizmosSelected() {
        Gizmos.color = Color.white;
        Gizmos.DrawWireSphere(transform.position, range);
    }
    protected virtual void OnDestroy () {
        // SELLING AN UPGRADED TOWER GIVES A HIGHER RETURN
        GameManager.instance.EarnGold(sellValue / 2);
    }
    protected virtual void OnMouseEnter() { Selected(); }
    protected virtual void OnMouseExit() { Deselected(); }
    protected virtual void OnTriggerEnter(Collider other) { if (other.CompareTag("Environment")) isColliding = true; }
    protected virtual void OnTriggerExit(Collider other) { if (other.CompareTag("Environment")) isColliding = false; }

    protected virtual void testing() {
        /*
        if (Input.GetKeyDown(KeyCode.I)) {
            Debug.Log("Build Level: " + buildLevel);
            Debug.Log("Tower Level: " + towerLevel);
        } */
    }
}
