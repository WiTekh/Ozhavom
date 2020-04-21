using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class ennemyStats : MonoBehaviour
{
    public float health = 100;

    public float dmg = 50;

    private void Update()
    {
        if (health <= 0)
            PhotonNetwork.Destroy(gameObject);
    }
}
