using System;
using System.Collections;
using System.Collections.Generic;
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

    private void Start()
    {
        octoHealth = GameObject.Find("Canvas").transform.GetChild(1).GetComponent<Slider>();
        
        MaxOH = GetComponent<ennemyStats>().health;
        octoHealth.maxValue = MaxOH;
        CurrentOH = MaxOH;
        
    }

    private void Update()
    {
        if (octoHealth.value == 0)
        {
            anim.SetTrigger("death");
        }
        //Synch Health / Slider
        CurrentOH = GetComponent<ennemyStats>().health;
        octoHealth.value = CurrentOH;
        //Moving Mecanism
        
        /*
         * 1 - Find the nearest player position
         * 2 - Choose him as target
         * 3 - Move forward to him (just a little bit (ex : take the normalized vector to the target and multiply it by a Length coef))
         */
        
        
        //Keeping the display of the Boss' healthbar
        CurrentOH = GetComponent<ennemyStats>().health;
        octoHealth.value = CurrentOH;
    }
}
