using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameWindow : MonoBehaviour
{
    public GameObject player;
    public Transform startPoint;

    private void gameStart() 
    {
        player.SetActive(true);
    }

    private void gameOver()
    {
        player.SetActive(false);
    }

    private void loadLevel() 
    {
        
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R)) 
        {
            respawn();
        }
    }

    private void respawn()
    {
        gameStart();
        player.transform.localPosition = startPoint.localPosition;
    }
}
