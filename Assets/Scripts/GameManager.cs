using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject pauseMenuScreen;
    public GameObject BGMusic;
    public void pauseGame(){
        Time.timeScale = 0;
        pauseMenuScreen.SetActive(true);
        BGMusic.SetActive(false);
    }
    public void resumeGame(){
        Time.timeScale = 1;
        pauseMenuScreen.SetActive(false);
        BGMusic.SetActive(true);
    }
    public void homeMenu(){
        Time.timeScale = 1;
        SceneManager.LoadScene("StartScene");
    }
}
