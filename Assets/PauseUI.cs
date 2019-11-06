using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseUI : MonoBehaviour
{
    public GameObject gameUI;
    public GameObject pauseUI;
    // Start is called before the first frame update
    public void Pause()
    {
        gameUI.SetActive(false);
        pauseUI.SetActive(true);   
    }
    public void Unpause()
    {
        gameUI.SetActive(true);
        pauseUI.SetActive(false);   
    }
}
