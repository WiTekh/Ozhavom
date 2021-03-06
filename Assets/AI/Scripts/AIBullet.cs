﻿using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class AIBullet : MonoBehaviour
{
    public float speed;
    public Vector3 Target;

    public float dmg;

    private void Start()
    {
        dmg = transform.parent.GetComponent<ennemyStats>().dmg;
    }

    void FixedUpdate()
    {
        transform.position = Vector2.MoveTowards(transform.position, Target, speed * Time.deltaTime);
        StartCoroutine("AutoDestroy");
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log(gameObject.name + " hit " + other.name + " dealing him " + dmg + " damages");
            other.gameObject.GetComponent<playerStats>().currentH -= dmg;
            Destroy(gameObject);
        }
    }

    IEnumerator AutoDestroy()
    {
        yield return new WaitForSeconds(1);
        Destroy(gameObject);
        //Loot money
    }
}
