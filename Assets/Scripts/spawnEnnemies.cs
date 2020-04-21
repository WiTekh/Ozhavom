using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using Random = System.Random;

public class spawnEnnemies : MonoBehaviour
{
    public GameObject[] ennemyPrefab;
    [SerializeField] private Transform[] Spawners;
    private int nbEnnemies;
    Random rd = new Random();
    private bool hasSpawned = true;
    bool done = false;


    [SerializeField] private List<GameObject> ennemies;
    
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
    }

    private void Update()
    {
        if (!hasSpawned && !done)
        {
            if (ennemies.Count == 0)
            {
                Debug.Log("Cleared Room");
                transform.parent.GetComponent<IsOpen>().IsRoomOpen = true;
                done = true;
            }
        }
        
        bool isOccupied = !transform.parent.GetComponent<IsOpen>().IsRoomOpen;

        if (isOccupied && !transform.parent.parent.GetComponent<cleanscript>().spawn && hasSpawned && transform.parent.parent.GetComponent<playerEnter>().hasEntered)
        { 
            Spawn();
        }

        if (ennemies.Count > 0)
        {
            foreach (GameObject e in ennemies)
            {
                if (!e)
                    ennemies.Remove(e);
            }
        }
    }

    void Spawn()
    {
        int nASE = GameObject.FindGameObjectsWithTag("Ennemy").Length;
        foreach (Transform spawner in Spawners)
        {
            if (spawner.gameObject.activeSelf)
            {
                PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "ennemy"), spawner.position, spawner.rotation);
            }
        }

        for (int i = nASE; i < GameObject.FindGameObjectsWithTag("Ennemy").Length; i++)
        {
            ennemies.Add(GameObject.FindGameObjectsWithTag("Ennemy")[i]);
        }
        hasSpawned = false;
    }
}
