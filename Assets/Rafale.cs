using System.Collections;
using System.Collections.Generic;
using System.IO;
using Photon.Pun;
using UnityEngine;

public class Rafale : MonoBehaviour
{
   [SerializeField] GameObject bulletprefab;
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
           if (firerate == 20)
           {
               GameObject bullet = PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "Bullet"), transform.position, transform.rotation);
               Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
               rb.AddForce(transform.right*20f,ForceMode2D.Impulse);
           }
   
           if (firerate == 40)
           {
               GameObject bullet = PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "Bullet"), transform.position, transform.rotation);
               Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
               rb.AddForce(transform.right*20f,ForceMode2D.Impulse);
           }
   
           if (firerate >= fire)
           {
               switch (slot)
               {
                   case 0:
                       if (Input.GetKey(KeyCode.Z))
                       {
                           Fire();
                       }
                       break;
                   case 1:
                       if (Input.GetKey(KeyCode.E))
                       { 
                           Fire();
                       }
                       break;
                   case 2:
                       if (Input.GetKey(KeyCode.R))
                       {
                           Fire();
                       }
                       break;
                   case 3:
                       if (Input.GetKey(KeyCode.T))
                       {
                           Fire();
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
           fire = 0;
           
       }
}
