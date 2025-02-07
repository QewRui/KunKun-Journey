using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class knockBackController : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player") 
        {
            collision.gameObject.GetComponent<playerController>().knockBackCounter = collision.gameObject.GetComponent<playerController>().knockBackDuration;
            if (collision.transform.position.x <= transform.position.x) 
            {
                collision.gameObject.GetComponent<playerController>().knockBackFromRight = true;
            }
            if (collision.transform.position.x > transform.position.x) 
            {
                collision.gameObject.GetComponent<playerController>().knockBackFromRight = false;
            }
        }
    }
}
