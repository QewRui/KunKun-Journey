using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class triggerTrapController : MonoBehaviour
{
    public Rigidbody2D trap;
    public float speed;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player") 
        {
            Vector2 v = new Vector2(speed, 0);
            trap.velocity = v;
        }
    }
}
