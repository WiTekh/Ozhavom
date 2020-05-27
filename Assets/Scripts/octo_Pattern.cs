using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Photon.Pun;
using UnityEngine;
using UnityEngine.UI;
using Random = System.Random;

public class octo_Pattern : MonoBehaviour
{
    public Slider octoHealth;
    
    private float MaxOH;
    private float CurrentOH;
    private Animator anim;
    private Transform Stone;

    private void Start()
    {
        octoHealth = GameObject.Find("Canvas").transform.GetChild(1).GetComponent<Slider>();
        Stone = gameObject.transform.GetChild(0);
        
        MaxOH = Stone.GetComponent<ennemyStats>().health;
        octoHealth.maxValue = MaxOH;
        CurrentOH = MaxOH;
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        if (octoHealth.value <= 0)
        {
            anim.SetTrigger("death");
            gameObject.GetComponent<SpriteRenderer>().enabled = false;
            gameObject.GetComponent<ennemyBehaviour>().enabled = false;
            gameObject.GetComponent<BoxCollider2D>().enabled = false;
            gameObject.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
            PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "bagofcoin"), transform.position, Quaternion.identity);
            PhotonNetwork.Destroy(gameObject);
        }
        //Synch Health / Slider
        CurrentOH = Stone.GetComponent<ennemyStats>().health;
        octoHealth.value = CurrentOH;
        //Moving Mecanism
        
        /*
         * 1 - Find the nearest player position
         * 2 - Choose him as target
         * 3 - Move forward to him (just a little bit (ex : take the normalized vector to the target and multiply it by a Length coef))
         */
        
        
        //Keeping the display of the Boss' healthbar
    }
}
