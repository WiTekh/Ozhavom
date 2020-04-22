using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class ennemyBehaviour : MonoBehaviour
{
    public float speed;
    public float stopDist;
    public float retreatDist;

    public float detection;

    public bool isRat;
    private float fireRate;
    public float nxtFire;

    public GameObject bullet;
        
    private Transform player;

    private void Start()
    {
        Look4Target();
        fireRate = nxtFire;
    }

    private void Update()
    {
        if (Vector2.Distance(transform.position, player.position) < detection)
        {
            //Follow
            if (Vector2.Distance(transform.position, player.position) > stopDist)
            {
                transform.position = Vector2.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
            }

            //Stand
            if (isRat == false)
            {
                if (Vector2.Distance(transform.position, player.position) < stopDist &&
                    Vector2.Distance(transform.position, player.position) > retreatDist)
                {
                    transform.position = transform.position;
                }
                //Retreat
                else if (Vector2.Distance(transform.position, player.position) < retreatDist)
                {
                    transform.position =
                        Vector2.MoveTowards(transform.position, player.position, -speed * Time.deltaTime);
                }

                if (fireRate <= 0)
                {
                    GameObject b = PhotonNetwork.Instantiate("bullet", transform.position, Quaternion.identity);
                    b.transform.parent = transform;
                    b.GetComponent<AIBullet>().Target = new Vector2(player.position.x, player.position.y);
                    fireRate = nxtFire;
                }
                else
                {
                    fireRate -= Time.deltaTime;
                }
            }
        }
        else
        {
            Look4Target();
        }
    }

    void Look4Target()
    {
        //Getting the Player thru all GO tagged w/ "Player"
        player = GameObject.FindGameObjectsWithTag("Player")[0].transform;
        
        //Getting the nearest Player
        foreach (var gO in GameObject.FindGameObjectsWithTag("Player"))
        {
            if (Vector2.Distance(transform.position, gO.transform.position) < Vector2.Distance(gameObject.transform.position, player.position))
                player = gO.transform;
        }
    }
}
