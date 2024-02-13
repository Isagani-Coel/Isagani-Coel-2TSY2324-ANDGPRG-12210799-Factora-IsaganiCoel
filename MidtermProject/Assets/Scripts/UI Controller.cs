using UnityEngine;

public class UIController : MonoBehaviour {
    public void Play() {
        SceneController.LoadScene(1);
    }
    public void Restart() {
        SceneController.Restart();
    }
    public void NextLevel() {
        SceneController.NextLevel();
    }
    public void SceneLoad(int sceneIndex){
        SceneController.LoadScene(sceneIndex);
    }
}

/*
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
*/