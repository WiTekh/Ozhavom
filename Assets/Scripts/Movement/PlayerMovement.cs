using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private PhotonView PV;
    private Rigidbody2D RB;       //hit box of the sprite
   

    //[SerializeField] private Sprite[] sprites;

    private Camera _cam;

    [SerializeField] private float MovementSpeed = 0;

    void Start()
    {
        PV = GetComponent<PhotonView>();
        RB = GetComponentInParent<Rigidbody2D>();
        if (PV.IsMine)
        {
            MovementSpeed = gameObject.GetComponent<AvatarSetup>().speed;

        }
    }
    
    //FixedUpdate has to be called for Rigidbodies
    void FixedUpdate()
    {
        if (PV.IsMine)
        {
            Move();
        }
       
    }
    private void Move()
    {
        
        if (Input.GetKey(KeyCode.S)&&Input.GetKey(KeyCode.D)&&!Input.GetKey(KeyCode.W)&&!Input.GetKey(KeyCode.A))   //detect diagonals 
        {
            RB.transform.Translate(new Vector2(Mathf.Sqrt(2)*MovementSpeed/2 ,-Mathf.Sqrt(2)*MovementSpeed/2 ) * Time.deltaTime);
        }
        else if (Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.D) && !Input.GetKey(KeyCode.S) &&
                 !Input.GetKey(KeyCode.A))
        {
            RB.transform.Translate(new Vector2(Mathf.Sqrt(2)*MovementSpeed/2 ,Mathf.Sqrt(2)*MovementSpeed/2 ) * Time.deltaTime);
        }
        else if(Input.GetKey(KeyCode.S) && Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.W) &&
                !Input.GetKey(KeyCode.D))
        {
            RB.transform.Translate(new Vector2(-Mathf.Sqrt(2)*MovementSpeed/2 ,-Mathf.Sqrt(2)*MovementSpeed/2 ) * Time.deltaTime);
        }
        else if (Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.S) &&
                 !Input.GetKey(KeyCode.D))
        {
            RB.transform.Translate(new Vector2(-Mathf.Sqrt(2)*MovementSpeed/2 ,Mathf.Sqrt(2)*MovementSpeed/2 ) * Time.deltaTime);
        }
        else
        {
            if (Input.GetKey(KeyCode.D))
                RB.transform.Translate(new Vector2(MovementSpeed, 0) * Time.deltaTime);
            if (Input.GetKey(KeyCode.A))
                RB.transform.Translate(new Vector2(-MovementSpeed, 0) * Time.deltaTime);
            if (Input.GetKey(KeyCode.S))
                RB.transform.Translate(new Vector2(0, -MovementSpeed) * Time.deltaTime);
            if (Input.GetKey(KeyCode.W))
                RB.transform.Translate(new Vector2(0, MovementSpeed) * Time.deltaTime);
        }
    }
}
