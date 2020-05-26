using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Photon.Pun;
using UnityEngine;
using Random = System.Random;

public class ennemyBehaviour : MonoBehaviour
{
    public float speed;
    public float stopDist;
    public float retreatDist;
    private PhotonView Pv;

    public int badGuyId;
    
    public int cooldown;
    private int cooled;
    public float detection;
    private float fireRate;
    public float nxtFire;
    private Transform player;
    private Random rd = new Random();
    public bool collided;

    private void Start()
    {
        Pv = GetComponent<PhotonView>();
        Look4Target();
        fireRate = nxtFire;
        cooled = cooldown;
    }

    private void Update()
    {
        if (Pv.IsMine)
        {
            if (badGuyId == 0)
            {
                Thrower();
            }
            else if (badGuyId == 1)
            {
                Rat();
            }
            else if (badGuyId == 2)
            {
                Tom();
            }

            if (cooled <= cooldown)
            {
                cooled++;
            }
        }
    }

    void Tom()
    {
        if (Vector2.Distance(transform.position, player.position) < detection)
        {
            if (fireRate <= 0)
            {
                GameObject b = PhotonNetwork.Instantiate("bullet", transform.position, Quaternion.identity);
                b.GetComponent<AIBullet>().Target = new Vector2(player.position.x, player.position.y);
                fireRate = nxtFire;
                b.transform.parent = transform;
            }
            else
            {
                fireRate -= Time.deltaTime;
            }
        }
        else
        {
            Look4Target();
        }
    }
    void Rat()
    {
        //Gonna move towards the player dealing melee damage
        //Choose a target
        player = GameObject.FindGameObjectsWithTag("Player")[
                rd.Next(GameObject.FindGameObjectsWithTag("Player").Length)]
            .transform;
        //Follow it until it's dead
//        transform.position += dir.normalized * speed;
        if (!collided)
            transform.position = Vector2.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
        //If hit, stand for 1 sec, and then push to the player
        else
        {
            transform.position = transform.position;
            collided = false;
        }
    }

    void Thrower()
    {
        if (Vector2.Distance(transform.position, player.position) < detection)
        {
            //Follow
            if (Vector2.Distance(transform.position, player.position) > stopDist)
            {
                transform.position = Vector2.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
            }

            //Stand
            if (Vector2.Distance(transform.position, player.position) < stopDist &&
                Vector2.Distance(transform.position, player.position) > retreatDist)
            {
                transform.position = transform.position;
            }
            //Retreat
            else if (Vector2.Distance(transform.position, player.position) < retreatDist)
            {
                transform.position = Vector2.MoveTowards(transform.position, player.position, -speed * Time.deltaTime);
            }

            if (fireRate <= 0)
            {
                GameObject b = PhotonNetwork.Instantiate("bullet", transform.position, Quaternion.identity);
                b.GetComponent<AIBullet>().Target = new Vector2(player.position.x, player.position.y);
                fireRate = nxtFire;
                b.transform.parent = transform;
            }
            else
            {
                fireRate -= Time.deltaTime;
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
            if (Vector2.Distance(transform.position, gO.transform.position) <
                Vector2.Distance(gameObject.transform.position, player.position))
                player = gO.transform;
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.gameObject.GetComponent<playerStats>().currentH -= GetComponent<ennemyStats>().dmg;
            collided = true;
        }
    }
}