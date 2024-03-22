using System.Collections.Generic;
using System.Net;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class BuildManager : MonoBehaviour {
    public static BuildManager instance;

    [Header("Build Settings")]
    [SerializeField] GameObject[] towerPrefabs;
    [SerializeField] Material selectionMat;

    [Header("Tower UI")]
    [SerializeField] GameObject towerInfoUI;
    [SerializeField] TextMeshProUGUI towerInfo;

    [Header("Towers On Field")]
    [SerializeField] List<GameObject> placedTowers = new List<GameObject>();
   
    // TOWER SELECTION & DRAGGING
    GameObject draggableTower, selectedTower;

    Ray ray; // shoots a line from your origin to the end point of your trajectory
    RaycastHit hit; // the GameObject that is being hit

    int towerChoice = 0;
    int[] buildCosts = { 30, 50, 50, 80 };

    public List<GameObject> GetPlacedTowers() { return placedTowers; }
    public GameObject GetSelectedTower() { return selectedTower; }

    void Awake() {
        if (instance == null) {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else Destroy(gameObject);
    }
    void Update() {
        if (Input.GetKeyDown(KeyCode.Space)) SoundManager.instance.PlaySound("Select", 0);

        if (draggableTower == null) {
            SelectTower();
            return;
        }
        MoveTower();
    }

    public void CreateTower(int i) {
        if (buildCosts[i] > GameManager.instance.GetGold()) { 
            Debug.LogError("YOU DON'T HAVE ENOUGH GOLD TO BUY THIS TOWER");
            return;
        }

        GameObject towerClone = (GameObject)Instantiate(towerPrefabs[i]);
        draggableTower = towerClone;
        towerChoice = i; 
    }
    void MoveTower() {
        selectedTower = null;
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (!Physics.Raycast(ray, out hit)) return;

        draggableTower.transform.position = SnapToGrid(hit.point);
        Tower t = draggableTower.GetComponent<Tower>();

        if (hit.point.y > 2.1f && hit.point.y < 2.3f && !t.GetIsColliding()) {
            draggableTower.GetComponent<Tower>().Buildable();

            if (Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject()) {
                GameManager.instance.EarnGold(-buildCosts[towerChoice]);
                t.Build();
                placedTowers.Add(draggableTower);
                draggableTower = null;
            }
        }
        else {
            t.Unbuildable();

            if (Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject())
                Debug.LogError("YOU CAN'T PLACE THAT TOWER THERE");
        }
        
        Vector3 SnapToGrid(Vector3 towerPos) {
            return new Vector3(Mathf.Round(towerPos.x), towerPos.y, Mathf.Round(towerPos.z));
        }
    }
    void SelectTower() {
        draggableTower = null;
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (!Physics.Raycast(ray, out hit)) return;

        if (hit.collider.gameObject.CompareTag("Tower") && Input.GetMouseButtonDown(0)) {
            GameObject tempTow = hit.collider.gameObject;

            selectedTower = tempTow;
            Tower t = selectedTower.GetComponent<Tower>();
            towerInfoUI.SetActive(true);
            towerInfo.text = t.name + " (Level " + t.GetLevel() + ")";
        }
    }
    public void UpgradeTower(GameObject tower, Stat stat) {
        Tower t = tower.GetComponent<Tower>();

        if (GameManager.instance.GetGold() < t.GetUpgradeCosts(t.GetLevel() - 1)) {
            Debug.LogError("YOU DON'T HAVE ENOUGH GOLD TO UPGRADE THIS TOWER!");
            return;
        }
        if (t.GetIsMaxed()) {
            Debug.LogError("THIS TOWER IS ALREADY AT ITS MAXIMUM LEVEL!");
            return;
        }

        t.Upgrade(stat);
        GameManager.instance.EarnGold(-t.GetUpgradeCosts(t.GetLevel() - 1));
        UIHandler.canPause = true;
        // Debug.Log(t.name + " HAS BEEN UPGRADED!");
    }
    public void SellTower(GameObject tower) {
        Tower t = tower.GetComponent<Tower>();

        UIHandler.canPause = true;
        GameManager.instance.EarnGold(t.GetTowerValue());
        placedTowers.Remove(tower);
        Destroy(tower);
    }
}