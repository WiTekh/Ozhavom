using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class adjustment : MonoBehaviour
{
    private void Start()
    {
        int c = 0;
        foreach (var p in GameObject.FindGameObjectsWithTag("Player"))
        {
            Debug.Log(c+ "_Found " + p.name);
            c++;
        }
    }
}
