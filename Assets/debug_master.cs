using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class debug_master : MonoBehaviour
{
    void Start()
    {
        foreach (var p in PhotonNetwork.PlayerList)
        {
            Debug.Log(p.IsMasterClient);
        }
    }
}
