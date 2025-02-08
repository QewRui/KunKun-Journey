using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pauseMenuController : MonoBehaviour
{
    public GameObject pauseMenu;
    public GameWindow gameWindow;

    public void Pause() 
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0;
    }
    
    public void Home() 
    {
        pauseMenu.SetActive(false);
        gameWindow.delLevel();
        gameWindow.Start();
        Time.timeScale = 1;
    }
    
    public void Resume() 
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1;
    }

    public void Restart()
    {
        pauseMenu.SetActive(false);
        gameWindow.respawn();
        Time.timeScale = 1;
    }
}
