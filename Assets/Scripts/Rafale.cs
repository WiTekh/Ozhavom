﻿using System.Collections;
using System.Collections.Generic;
using System.IO;
using Photon.Pun;
using UnityEngine;

public class Rafale : MonoBehaviour
{
       [SerializeField] private int firerate;
       [SerializeField] public bool active;
       [SerializeField] public int slot;
   
       private int fire;
       // Update is called once per frame
       private void Start()
       {
           fire = firerate;
       }
   
       private void Update()
       {
           if (fire<firerate)
           {
               fire++;
           }
           if (fire == 20)
           {
               Fire();
           }
   
           if (fire == 40)
           {
               Fire();
           }
   
           if (fire >= firerate)
           {
               switch (slot)
               {
                   case 0:
                       if (Input.GetKey(KeyCode.Z))
                       {
                           Fire();
                           fire = 0;
                       }
                       break;
                   case 1:
                       if (Input.GetKey(KeyCode.E))
                       { 
                           Fire();
                           fire = 0;
                           
                       }
                       break;
                   case 2:
                       if (Input.GetKey(KeyCode.R))
                       {
                           Fire();
                           fire = 0;
                       }
                       break;
               }
           }
       }
   
       void Fire()
       {
           GameObject bullet = PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "Bullet"), transform.position, transform.rotation);
           Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
           rb.AddForce(transform.right*20f,ForceMode2D.Impulse);
       }
}
