using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindowRoot : MonoBehaviour
{
    public void setWindowState(bool isActive) 
    {
        if (gameObject.activeSelf != isActive) 
        {
            gameObject.SetActive(isActive);
        }
    }
}
