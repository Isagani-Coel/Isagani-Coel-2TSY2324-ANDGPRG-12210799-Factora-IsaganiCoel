using System;
using UnityEngine;
            
public enum BulletType { ARROW, CANNONBALL, FIRE, ICE };

public class Bullet : MonoBehaviour {
    [SerializeField] Transform target;
    BulletType type;
    Tower tower;
    float speed = 20f;
    // bullet sphere cast for area spash dmg

    public void SetTower(Tower t) { tower = t; }
    public void SetTarget(Transform target) { this.target = target; }
    public void SetType(BulletType type) { this.type = type; }

    void Update() {
        if (target == null) {
            Destroy(gameObject);
            return;
        }

        Vector3 direction = target.position - transform.position;
        float distanceThisFrame = speed * Time.deltaTime;

        if (direction.magnitude <= distanceThisFrame) {
            HitEnemy();
            return;
        }
        transform.Translate(direction.normalized * distanceThisFrame, Space.World);
    }

    void HitEnemy() {
        Enemy e = target.GetComponent<Enemy>();

        if (!e.GetIsBurning() && !e.GetIsFrozen()) {
            if (type == BulletType.FIRE) target.GetComponent<Enemy>().DoFireEffect(tower.GetEffectCountdown());
            if (type == BulletType.ICE) target.GetComponent<Enemy>().DoFrostEffect(tower.GetEffectCountdown());
        }
        
        e.TakeDMG(tower.GetDMG());
        
        tower = null;
        Destroy(gameObject);
    }

}