using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using Random = System.Random;

public class spawnEnnemies : MonoBehaviour
{
    public GameObject ennemyPrefab;
    [SerializeField] private Transform[] Spawners;
    private int nbEnnemies;
    Random rd = new Random();
    private bool hasSpawned = true;

    private void Awake()
    {
        Spawners = new Transform[transform.childCount];
        for (int i = 0; i < transform.childCount; i++)
        {
            Spawners[i] = transform.GetChild(i);
        }
    }

    private void Start()
    {
        //Setting the nb of ennemies w/ more chances of having between 4 and 6 ennemies than more or less
        switch (rd.Next(10))
        {
            case 0:
            case 1:
                nbEnnemies = rd.Next(2,3);
                break;
            case 2:
            case 3:
                nbEnnemies = rd.Next(6, 7);
                break;
            default:
                nbEnnemies = rd.Next(4, 6);
                break;
        }

        for(int i = 0; i < nbEnnemies; i++)
        {
            Spawners[i].gameObject.SetActive(false);
        }
    }

    private void Update()
    {
        bool isOccupied = !transform.parent.GetComponent<IsOpen>().IsRoomOpen;

        if (isOccupied && !transform.parent.parent.GetComponent<cleanscript>().spawn && hasSpawned)
        { 
            Spawn();
        }
    }

    void Spawn()
    {
        foreach (var spawner in Spawners)
        {
            transform.GetComponent<PhotonView>().RPC("SpawnEnnemy", RpcTarget.AllBuffered, spawner);
        }
        hasSpawned = false;
    }
    
    [PunRPC]
    void SpawnEnnemy(Transform parent)
    {
        Instantiate(ennemyPrefab, parent);
    }
}
