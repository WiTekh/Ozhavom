using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;
using Random = System.Random;

public class pickUpCoin : MonoBehaviour
{
    private Random rd = new Random();
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.gameObject.GetComponent<playerStats>().coinAmount += rd.Next(1, 6);
            PhotonNetwork.Destroy(gameObject);
        }
    }
}
