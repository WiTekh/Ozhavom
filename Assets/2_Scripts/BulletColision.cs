using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using Photon.Pun.Demo.Asteroids;
using UnityEngine;

public class BulletColision : MonoBehaviour
{
   private void OnCollisionEnter2D(Collision2D collision)
   {
      PhotonNetwork.Destroy(gameObject);
   }
}
