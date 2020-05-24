using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class ChargedBeam : MonoBehaviour
{
    [SerializeField] public bool active;
    public Sprite weaponRenderer;
    private variablesStock _dataHandler;

    private PhotonView PV;
    public int slot;
    private PlayerMovement move;
    private LineRenderer laser;
    public Transform LaserHit;

    [SerializeField] private int firerate;

    private float fire;
    // Update is called once per frame
    private void Start()
    {
        PV = transform.parent.parent.parent.GetComponent<PhotonView>();

        move = transform.parent.parent.parent.parent.GetComponent<PlayerMovement>();
        laser = GetComponent<LineRenderer>();
        
        _dataHandler = GameObject.Find("varHolder").GetComponent<variablesStock>();
    }

    void Update()
    {
        if (PV.IsMine)
        {
            if (move.enabled == false)
            {
                fire += 0.25f;
            }

            if (fire == 20)
            {
                move.enabled = true;
                laser.enabled = false;
            }


            switch (slot)
            {
                case 0:
                    if (Input.GetKey(KeyCode.Z))
                    {
                        Fire();

                    }

                    break;
                case 1:
                    if (Input.GetKey(KeyCode.E))
                    {
                        Fire();


                    }

                    break;
                case 2:
                    if (Input.GetKey(KeyCode.R))
                    {
                        Fire();

                    }

                    break;
                default:
                    if (firerate <= fire)
                    {
                        laser.enabled = true;
                        RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.right);
                        LaserHit.position = hit.point;
                        laser.SetPosition(0, transform.position);
                        laser.SetPosition(1, LaserHit.position);
                        move.enabled = false;
                        fire = 0;
                    }

                    else if (move.enabled)
                    {
                        fire = 0;
                    }

                    break;
            }


        }
    }

    void Fire()
    {
        if (fire < firerate)
        {
            fire+=0.25f;   
        }
    }
}
