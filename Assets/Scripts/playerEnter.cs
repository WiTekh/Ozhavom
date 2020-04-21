using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerEnter : MonoBehaviour
{
    public bool hasEntered;
    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
            hasEntered = true;
    }
}
