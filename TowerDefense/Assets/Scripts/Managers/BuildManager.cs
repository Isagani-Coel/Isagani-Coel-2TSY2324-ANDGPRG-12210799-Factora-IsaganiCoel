using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Runtime.CompilerServices;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;

public class BuildManager : MonoBehaviour {
    public static BuildManager instance;

    [Header("Build Settings")]
    [SerializeField] Towers[] towers; // will be used later on once there are different ugrades for the towers
    [SerializeField] GameObject[] towerPrefabs;
    GameObject draggableTower; // temporary until the player has selected a spot for the tower
    Tower tempTower;
    float buildableOffsetY = 2.2f;
    int choice = 0;

    // MOVING THE TOWER ON THE MAP
    Ray ray; // shoots a line from your origin to the end point of your trajectory
    RaycastHit hit; // the object that is being hit

    void Awake() {
        if (instance == null) {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else Destroy(gameObject);
    }
    
    void Update() {
        if (draggableTower == null) return;

        MoveTower();
    }

    public void CreateTower(int i) {
        if (towers[i].buildCost > GameManager.instance.GetGold()) { 
            Debug.LogWarning("YOU DON'T HAVE ENOUGH GOLD");
            return;
        }

        GameObject t = (GameObject)Instantiate(towerPrefabs[i]);;
        draggableTower = t;
        tempTower = t.GetComponent<Tower>();
        choice = i; 
    }

    void MoveTower() {
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit)) {
            draggableTower.transform.position = SnapToGrid(hit.point);

            if (hit.point.y > buildableOffsetY) {
                tempTower.Buildable();

                if (Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject()) {
                    GameManager.instance.EarnGold(-towers[choice].buildCost);
                    tempTower.Build();
                    draggableTower = null;
                    tempTower = null;
                }
            }
            else {
                tempTower.Unbuildable();

                if (Input.GetMouseButtonDown(0))
                    Debug.LogError("YOU CAN'T PLACE THAT TOWER THERE");
            }
        }
        Vector3 SnapToGrid(Vector3 towerPos) { 
            return new Vector3(Mathf.Round(towerPos.x), towerPos.y, Mathf.Round(towerPos.z));
        }
    }
}

[System.Serializable]
public class Towers {
    public string towerName;
    public Material towerMat;
    public GameObject[] towerLevelPrefabs;
    public int buildCost, upgradeCost;
}

/*
using UnityEditor.Experimental.GraphView;
 */