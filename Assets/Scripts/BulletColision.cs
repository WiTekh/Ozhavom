using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using Photon.Pun.Demo.Asteroids;
using Photon.Realtime;
using UnityEngine;
using UnityEngine.UI;

public class BulletColision : MonoBehaviourPunCallbacks, IPunObservable
{
   public float dmg = 50;
   [SerializeField] public Sprite _sprite;

   [SerializeField] private SpriteRenderer sp;

   public bool isH = false;

   private void Update()
   {
      transform.Translate(0.4f, 0, 0);
      _sprite = GetComponent<SpriteRenderer>().sprite;
      sp.sprite = _sprite;
   }

   private void OnTriggerEnter2D(Collider2D other)
   {
      Debug.Log(other.tag);

      if (other.CompareTag("Ennemy") || other.CompareTag("Boss"))
      {
         Debug.Log($"Dealt {dmg}");
         other.gameObject.GetComponent<ennemyStats>().health -= dmg;
      }


      if (!other.CompareTag("Player") && !other.CompareTag("Bullet"))
      {
         if (isH)
         {
            other.gameObject.GetComponent<playerStats>().currentH += 20;
         }
         PhotonNetwork.Destroy(gameObject);
      }

      if (isH && other.CompareTag("Player"))
      {
         other.gameObject.GetComponent<playerStats>().currentH += 20;
      }
      else if (other.CompareTag("Ennemy"))
      {
         other.gameObject.GetComponent<ennemyStats>().health -= dmg;
      }
      
   }
   
   public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
   {
      if (stream.IsWriting)
      {
         stream.SendNext(_sprite);
      }
      else if (stream.IsReading)

      {
         sp.sprite = (Sprite) stream.ReceiveNext();
      }
   }
}
