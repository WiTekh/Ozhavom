using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using Photon.Pun.Demo.Asteroids;
using Photon.Realtime;
using UnityEngine;

public class BulletColision : MonoBehaviour
{
   public float dmg = 50;

   private void Update()
   {
      transform.Translate(0.4f,0,0);
   }

   private void OnTriggerEnter2D(Collider2D collision)
   {
      
      if (collision.CompareTag("Ennemy"))
      {
         Debug.Log($"Dealt {dmg}");
         collision.gameObject.GetComponent<ennemyStats>().health -= dmg;
      }
     

      if (!collision.CompareTag("Player")&& !collision.CompareTag("Bullet"))
      {
         Destroy(gameObject);
      }
      
   }

   
}
