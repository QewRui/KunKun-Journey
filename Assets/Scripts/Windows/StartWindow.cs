using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartWindow : WindowRoot
{
    public playerController playerController;
    public GameWindow gameWindow;

    // Update is called once per frame
    private void Update()
    {
        enterGame();
    }

    private void enterGame() 
    {
        if (Input.GetMouseButtonDown(0)) 
        {
            playerController.initPlayer();
            setWindowState(false);
            gameWindow.setWindowState(true);
        }
    }
}
