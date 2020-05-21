using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Photon.Pun;
using UnityEngine;

public class WeaponShoot : MonoBehaviour
{
    private AvatarSetup AS;
    private PhotonView PV;
    
    public int dmg;

    [SerializeField] GameObject bulletprefab;

    private PlayerMovement move;
    private LineRenderer laser;
    public Transform LaserHit;

    [SerializeField] private int firerate;

    private float fire;

    // Update is called once per frame
    private void Start()
    {
        Transform avatar = transform.parent.parent.parent;
        AS = avatar.GetComponent<AvatarSetup>();
        PV = avatar.GetComponent<PhotonView>();

        move = transform.parent.parent.parent.GetComponent<PlayerMovement>();
        fire = firerate;
        laser = GetComponent<LineRenderer>();
        laser.enabled = false;
        laser.useWorldSpace = true;
    }

    void Update()
    { 
        if (PV.IsMine)
        {
            if (fire == 20)
            {
                move.enabled = true;
                laser.enabled = false;
            }
        
            if (Input.GetMouseButton(0) && firerate <= fire)
            {
                Debug.Log("LASER");
                RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.right);
                LaserHit.position = hit.point;
                if (hit.collider.gameObject.tag == "Ennemy")
                {
                    Debug.Log($"Dealt {dmg}");
                    hit.collider.gameObject.GetComponent<ennemyStats>().health -= dmg;
                }
                laser.sortingLayerName = "Player";
                laser.SetPosition(0,transform.position);
                laser.SetPosition(1,LaserHit.position);
                laser.enabled = true;
                move.enabled = false;
                fire = 0;
            }

            else if (fire<firerate)
            {
                fire+=0.25f;
            }
        }
    }
}