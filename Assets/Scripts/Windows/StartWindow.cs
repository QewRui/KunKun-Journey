using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartWindow : MonoBehaviour
{
    public playerController playerController;
    public GameWindow gameWindow;

    // Update is called once per frame
    private void Update()
    {
        goGameWindow();
    }

    private void goGameWindow() 
    {
        if (Input.GetMouseButtonDown(0)) 
        {
            playerController.initPlayer();
            gameObject.SetActive(false);
            gameWindow.gameObject.SetActive(true);
        }
    }
}
