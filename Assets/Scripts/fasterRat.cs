using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fasterRat : MonoBehaviour
{
    private Vector3 playerPos;
    [SerializeField] private float detectionDistance;
    [SerializeField] private float speed;
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Wow damn");
            Vector2.MoveTowards(transform.position, other.transform.position, speed);
        }
    }
}
