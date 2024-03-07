using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public enum BulletType { ARROW, CANNONBALL, FIRE, ICE };

public class Bullet : MonoBehaviour {
    [SerializeField] Transform target;
    LineRenderer lineRenderer;
    BulletType type;
    float speed = 20f;

    bool isFireType;
    // bullet sphere cast for area spash dmg

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
        switch (type) { 
            case BulletType.ARROW: 
                target.GetComponent<Enemy>().TakeDMG(5);
                break;
            case BulletType.FIRE:
                target.GetComponent<Enemy>().DoFireEffect();
                break;
            case BulletType.ICE:
                target.GetComponent<Enemy>().TakeDMG(5);
                target.GetComponent<Enemy>().DoFrostEffect();
                break;
            case BulletType.CANNONBALL:
                target.GetComponent<Enemy>().TakeDMG(20);
                break;
            default: 
                break;
        }

        Destroy(gameObject);
    }

}