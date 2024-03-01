using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {
    public static GameManager instance;

    [SerializeField] GameObject crystalCore;
    public GameObject CrystalCore { get { return crystalCore; } }

	void Awake() {
        instance = this;
        /*
        if (instance == null) {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else Destroy(gameObject); // */
	}
	
	void Start() {
        
    }
    
    void Update() {
        
    }
}
