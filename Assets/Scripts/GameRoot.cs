using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameRoot : MonoBehaviour
{
    public static GameRoot instance;
    public StartWindow startWindow;

    // Start is called before the first frame update
    private void Start()
    {
        instance = this;
        gameStartWindow();
    }

    private void gameStartWindow() 
    {
        Transform canvas = transform.Find("Canvas");
        for (int i = 0; i < canvas.childCount; i++) 
        {
            canvas.GetChild(i).gameObject.SetActive(false);
        }
        startWindow.setWindowState(true);
    }
}
