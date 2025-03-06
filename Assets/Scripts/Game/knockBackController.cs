using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class knockBackController : MonoBehaviour
{

    //AudioSource 
    AudioSource[] audioSources;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        var player = collision.collider.GetComponent<playerController>();

        if (player != null) 
        {
            player.playerKnockBack(transform);
            audioSources = GetComponents<AudioSource>();
            audioSources[0].Play();

        }
    }
}
