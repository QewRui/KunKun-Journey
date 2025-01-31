using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameWindow : WindowRoot
{
    public GameObject player;
    public GameObject[] levels;

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
    }

    private void delLevel()
    {
        Transform currentLevel = transform.Find("Level" + levelCount);
        if (currentLevel != null)
        {
            Destroy(currentLevel.gameObject.gameObject);
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
    }

    private void loadLevel()
    {
        // Load the current level and set it as a child of GameWindow
        GameObject level = Instantiate(levels[levelCount]);
        level.name = "Level" + levelCount;
        level.transform.SetParent(transform, false);

        // Position the player at the StartPoint of the new level
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
