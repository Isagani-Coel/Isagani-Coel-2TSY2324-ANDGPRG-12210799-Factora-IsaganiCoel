using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public enum MonsterType { GROUND, AIR }

public class Enemy : MonoBehaviour {
    [SerializeField] Transform target;
    [SerializeField] NavMeshAgent agent;
    [SerializeField] MonsterType Type;
    public MonsterType type { get { return Type; } }

    
    void Awake() {
        agent = GetComponent<NavMeshAgent>();


    }

    void Update() {
        testing();
    }

    public void SetTarget(Transform target) {
        // Debug.Log(agent.pathStatus);
        this.target = target;
		agent.SetDestination(target.position);
	}

	void OnTriggerEnter(Collider other) {
        if (other.gameObject.CompareTag("Finish")) {
            SpawnManager.instance.RemoveEnemy(gameObject);
            Destroy(gameObject);
        }
	}

    void OnDestroy() {
        Debug.Log("ENEMY HAS BEEN DESTROYED");
    }

    void testing() {
        if (Input.GetKeyDown(KeyCode.Delete)) Destroy(this.gameObject);
    }

}
