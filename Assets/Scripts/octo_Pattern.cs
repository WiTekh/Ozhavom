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
    
    [SerializeField] private float MaxOH;
    [SerializeField] private float CurrentOH;
    
    Random rd = new Random();

    private float CD1;
    private float act1 = 0;
    private float CD2;
    private float act2 = 0;
    private float CD3;
    private float act3 = 0;

    private float CDA1;
    private float actA1;
    
    private float CDA2;
    private float actA2;
    
    private void Start()
    {
        CurrentOH = MaxOH;
        octoHealth = GameObject.Find("Canvas").transform.GetChild(1).GetComponent<Slider>();
        
        CD1 = rd.Next(5, 10);
        CD2 = rd.Next(5, 10);
        CD3 = rd.Next(5, 10);
        
    }

    private void Update()
    {
        //Keeping the display of the Boss' healthbar
        octoHealth.value = CurrentOH / MaxOH;
        
        if (CurrentOH <= 2*MaxOH / 3)
        {
            if (CurrentOH <= MaxOH / 3)
            {
                if (CurrentOH <= 0)
                {
                    //Ending Screen
                }
                //Phase 3
                //Abilities
                if (act3 >= CD3)
                {
                    //---------------------------------------------------
                    // Columns coming out of sky and dealing zone damage
                    //---------------------------------------------------
                    (act3, CD3) = (0, rd.Next(5, 10));
                }
                else
                    act3 += Time.deltaTime;
                
                //Auto-Attack
                if (actA2 >= CDA2)
                {
                    //Shoot
                    actA2 = 0;
                }
                else
                    actA2 += Time.deltaTime;

            }
            //Phase 2
            //Abilities
            if (act2 >= CD2)
            {
                //-------------------------------------------------------
                // Big Laser around him every Random.Next(5, 10) seconds
                //-------------------------------------------------------
                (act2, CD2) = (0, rd.Next(5, 10));
            }
            else
                act2 += Time.deltaTime;
            
            //Auto-Attack
            if (actA2 >= CDA2)
            {
                //Shoot
                actA2 = 0;
            }
            else
                actA2 += Time.deltaTime;
            //Bigger Projectiles, 0.75 times speed
        }
        else
        {
            //Phase 1
            //Abilities
            if (act1 >= CD1)
            {
                //-------------------------------------------------------------------
                // Damages around him in a big zone every Random.Next(5, 10) seconds
                //-------------------------------------------------------------------
                (act1, CD1) = (0, rd.Next(5, 10));
            }
            else
                act1 += Time.deltaTime;
                
            //Auto-Attack
            if (actA1 >= CDA1)
            {
                //Shoot
                actA1 = 0;
            }
            else
                actA1 += Time.deltaTime;

            //Shoot little fast projectiles every 1.5 seconds
        }
        
    }
}
