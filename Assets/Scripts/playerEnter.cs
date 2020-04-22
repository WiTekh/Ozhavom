using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Photon.Pun;
using UnityEngine;


public class playerEnter : MonoBehaviour
{
    public bool hasEntered ;
    [SerializeField] private cleanscript clean;

    private void Start()
    {
        hasEntered = false;
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (!hasEntered && clean.item)
            {
                for (int i = 0; i < 4; i++)
                {
                    Vector3 vect = new Vector3(-3 +2 *i,0,0);
                    
                    PhotonNetwork.Instantiate( Path.Combine("PhotonPrefabs","Item"), transform.position-vect, transform.rotation);
                    transform.GetChild(4).GetComponent<IsOpen>().IsRoomOpen = true;
                }

                hasEntered = true;
            } 

            if (clean.cook || clean.forge || clean.instructor || clean.shop)
            {
                transform.GetChild(4).GetComponent<IsOpen>().IsRoomOpen = true;
            }
            
            hasEntered = true;
        }
    }
}
