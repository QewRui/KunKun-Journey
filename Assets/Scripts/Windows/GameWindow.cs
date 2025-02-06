using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameWindow : WindowRoot
{
    public GameObject player;
    public GameObject[] levels;
    public GameObject deadWindow; 

    private int levelCount;

    private void Start()
    {
        levelCount = 0;
        gameStart();
        loadLevel();
    }

    private void gameStart()
    {
        player.SetActive(true);
        deadWindow.SetActive(false); // Hide dead window when game start
    }

    private void delLevel()
    {
        Transform currentLevel = transform.Find("Level" + levelCount);
        if (currentLevel != null)
        {
            Destroy(currentLevel.gameObject.gameObject); // <------ .gameObject.gameObject correct ma??
        }
    }

    public void nextLevel()
    {
        delLevel();
        levelCount++;
        loadLevel();
    }

    public void gameOver()
    {
        player.SetActive(false);
        //deadWindow.SetActive(true);  // Show the dead window 
    }

    private void loadLevel()
    {
        GameObject level = Instantiate(levels[levelCount]);
        level.name = "Level" + levelCount;
        level.transform.SetParent(transform, false);

        Transform start = level.transform.Find("StartPoint");
        if (start != null)
        {
            player.transform.localPosition = start.localPosition;
        }
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
        delLevel();
        loadLevel();
    }
}
