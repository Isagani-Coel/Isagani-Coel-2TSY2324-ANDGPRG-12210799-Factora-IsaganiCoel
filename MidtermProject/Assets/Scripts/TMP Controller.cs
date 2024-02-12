using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class TMPController : MonoBehaviour {
    [SerializeField] TextMeshProUGUI textElement;

    public void PlayGame() {
        SceneManager.LoadSceneAsync("1-1");
    }

    public void MainMenu() {
        SceneManager.LoadSceneAsync("Main Menu");
    }

    public void QuitGame(){
        Application.Quit();
    }    
}
