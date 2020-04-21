using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using Photon.Pun.Demo.Asteroids;
using UnityEngine;

public class BulletColision : MonoBehaviour
{
   public float dmg = 0;
   private void OnCollisionEnter2D(Collision2D collision)
   {
      PhotonNetwork.Destroy(gameObject);
      if (collision.collider.CompareTag("Ennemy"))
      {
         Debug.Log($"Dealt {dmg}");
         collision.gameObject.GetComponent<ennemyStats>().health -= dmg;
      }
   }
}
