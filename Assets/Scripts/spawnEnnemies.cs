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
    [SerializeField] private bool isOccupied;
    private cleanscript CS;

    public GameObject bossRoom;
    
    [SerializeField] private List<GameObject> ennemies;
    
//    private void Awake()
//    { 
//        Spawners = new Transform[transform.childCount];
//        for (int i = 0; i < transform.childCount; i++)
//        {
//            Spawners[i] = transform.GetChild(i);
//        }
//    }

    private void Start()
    {
        CS = gameObject.GetComponent<cleanscript>();
        nbEnnemies = 3; 
        isOccupied = true;
        
        //Determining the pattern
        transform.GetChild(6+rd.Next(1, 8)).gameObject.SetActive(true);
    }

    private void Update()
    {
        if (!hasSpawned && !done)
        {
            if (ennemies.Count == 0)
            {
                Debug.Log("Cleared Room");
                transform.GetChild(4).GetComponent<IsOpen>().IsRoomOpen = true;
                done = true;
            }
        }
        
       

        //Spawning Ennemies only if not a shop, a spawn, a boss room, etc..
        if (isOccupied && !CS.shop && !CS.item && !CS.instructor 
            && !CS.forge && !CS.cook && !CS.boss 
            && !CS.spawn && hasSpawned && gameObject.GetComponent<playerEnter>().hasEntered)
        { 
            Debug.Log("Spawning ... ");
            Spawn();
            
        }
        //Spawning the Boss
        if (isOccupied && CS.boss &&
            gameObject.GetComponent<playerEnter>().hasEntered && go4boss)
        {
            SpawnBoss();
            go4boss = false;
           
        }

        if (ennemies.Count > 0)
        {
            for (int i = 0; i < ennemies.Count; i++)
            {
                if (ennemies[i] == null)
                {
                    ennemies.Remove(ennemies[i]);
                }
            }
        }
    }

    void Spawn()
    {
        if (RoomID == 80001)
        {
            for (int i = 6; i < transform.childCount; i++)
            {
                if (transform.GetChild(i).gameObject.active)
                {
                    for (int j = 0; j < transform.GetChild(i).childCount; j++)
                    {
                        int rd = new Random().Next(0, 3);
                        string ennemy = "";

                        if (rd == 0)
                            ennemy = "rat";
                        else if (rd == 1)
                            ennemy = "ennemy";

                        else
                            ennemy = "tom";
                        
                        GameObject ooo = PhotonNetwork.InstantiateSceneObject(Path.Combine("PhotonPrefabs", ennemy), transform.GetChild(i).GetChild(j).position,
                            Quaternion.identity);
                        ennemies.Add(ooo);
                    }
                }
            }
        }

        hasSpawned = false;
    }

    void SpawnBoss()
    {
        Debug.Log($"Boss : ({transform.position.x},{transform.position.y}) Spawned");
        GameObject bite = PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "Octothorp"), transform.position, Quaternion.identity);
        bite.transform.parent = transform;
        bossRoom = gameObject;
        if (CS.boss)
        {
            Debug.Log("Found Boss");
            GameObject.Find("varHolder").GetComponent<variablesStock>().bossRoom = bossRoom;
        }

        GameObject.Find("Canvas").transform.GetChild(1).gameObject.SetActive(true);
    }
}
