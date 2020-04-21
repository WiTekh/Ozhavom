using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moving : MonoBehaviour
{
    [SerializeField] private float speed;
    private Vector2 moveVel;
    
    private void Update()
    {
        Vector2 move = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        moveVel = move.normalized * speed;
    }

    private void FixedUpdate()
    {
        gameObject.GetComponent<Rigidbody2D>().MovePosition(gameObject.GetComponent<Rigidbody2D>().position + moveVel * Time.deltaTime);
    }
}
