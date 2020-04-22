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
    private bool done = false;
    public int RoomID = 80001;
    private bool go4boss = true;
    private bool isOccupied;


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
    {/*
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
        } */
        nbEnnemies = 2;
        bool isOccupied = true;
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
        
       

        //Spawning Ennemies only if not a shop, a spawn, a boss room, etc..
        if (isOccupied && !transform.parent.parent.GetComponent<cleanscript>().shop && !transform.parent.parent.GetComponent<cleanscript>().item && !transform.parent.parent.GetComponent<cleanscript>().instructor 
            && !transform.parent.parent.GetComponent<cleanscript>().forge && !transform.parent.parent.GetComponent<cleanscript>().cook && !transform.parent.parent.GetComponent<cleanscript>().boss 
            && !transform.parent.parent.GetComponent<cleanscript>().spawn && hasSpawned && transform.parent.parent.GetComponent<playerEnter>().hasEntered)
        { 
            Spawn();
            isOccupied = false;
        }
        //Spawning the Boss
        if (isOccupied && transform.parent.parent.GetComponent<cleanscript>().boss &&
            transform.parent.parent.GetComponent<playerEnter>().hasEntered && go4boss)
        {
            SpawnBoss();
            go4boss = false;
            isOccupied = false;
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
        if (RoomID == 80001)
        {
            PhotonNetwork.InstantiateSceneObject(Path.Combine("PhotonPrefabs", "ennemy"), transform.position + new Vector3(-2,0,0), transform.rotation);
            PhotonNetwork.InstantiateSceneObject(Path.Combine("PhotonPrefabs", "Rat"), transform.position + new Vector3(2,0,0), transform.rotation);
        }
        hasSpawned = false;
    }

    void SpawnBoss()
    {
        Debug.Log($"Boss : ({transform.position.x},{transform.position.y}) Spawned");
        PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "octo"), transform.position, Quaternion.identity);
        GameObject.Find("Canvas").transform.GetChild(1).gameObject.SetActive(true);
    }
}
