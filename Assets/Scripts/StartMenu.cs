using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartMenu : MonoBehaviour
{
    int savedScene;
    int sceneIndex;
    public void StartGame(){
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    public void QuitGame(){
        Application.Quit();
    }
    public void LoadGame(){
        savedScene = PlayerPrefs.GetInt("Saved");
        if(savedScene != 0){
            SceneManager.LoadSceneAsync(savedScene);
        }else
            return;
    }
    public void SaveGame(){
        sceneIndex = SceneManager.GetActiveScene().buildIndex;
        PlayerPrefs.SetInt("Saved", sceneIndex);
        PlayerPrefs.Save();
        // SceneManager.LoadSceneAsync(0);
    }
}
