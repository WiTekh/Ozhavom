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
    
    public float MovementSpeed;
    public float RotationSpeed;

    private Vector2 desiredPosition;

    void Start()
    {
        PV = GetComponent<PhotonView>();
        RB = GetComponent<Rigidbody2D>();
    }
    
    //FixedUpdate has to be called for Rigidbodies
    void FixedUpdate()
    {
        if (PV.IsMine)
        {
            Move();
        }
    }
    
    void Update()
    {
        if (PV.IsMine)
        {
            //Rotate();
            CheckRot();
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
        
        RB.MovePosition(desiredPosition);
    }

    private void Rotate()
    {
        //Will have to change that bc all players have different cameras
        Camera myCam = Camera.main;
        
        Vector3 dir = Input.mousePosition - myCam.WorldToScreenPoint(transform.position);
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }
    
    private void CheckRot()
    {
        /*Sprite frontL = sprites[0];
        Sprite frontR = sprites[1];
        Sprite back = sprites[2];
        */
        if (transform.rotation.z > Mathf.Rad2Deg*((Mathf.PI)/3) && transform.rotation.z < Mathf.Rad2Deg*(2*Mathf.PI/3))
        {
            //Back
            Debug.Log("Looking Back");
        }
        
        if (transform.rotation.z < Mathf.Rad2Deg*((2*Mathf.PI)/3) && transform.rotation.z < Mathf.Rad2Deg*(-Mathf.PI/2))
        {
            //Left
            Debug.Log("Looking Left");
        }
        
        if (transform.rotation.z < Mathf.Rad2Deg*(-Mathf.PI/2) && transform.rotation.z > Mathf.Rad2Deg*((Mathf.PI)/3))
        {
            //Right
            Debug.Log("Looking Right");
        }
    }
}
