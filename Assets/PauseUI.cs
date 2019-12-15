using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseUI : MonoBehaviour
{
    public bool Paused;
    public GameObject gameUI;
    public GameObject pauseUI;
    public GameObject optionsUI;
    // Start is called before the first frame update
    public void Pause()
    {
        gameUI.SetActive(false);
        pauseUI.SetActive(true);
        Paused = true;   
    }
    public void Unpause()
    {
        gameUI.SetActive(true);
        pauseUI.SetActive(false);
        Paused = false;   
    }
    public void ShowOptions()
    {
        optionsUI.SetActive(true);
    }
    public void HideOptions()
    {
        optionsUI.SetActive(false);
    }
    public void ReturntoMenu()
    {
        GlobalProfile.getInstance().SaveGame();
        SceneManager.LoadScene(1);
    }
}
