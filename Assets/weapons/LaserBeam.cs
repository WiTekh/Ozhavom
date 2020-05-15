using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class LaserBeam : MonoBehaviour
{
    private PhotonView PV;
    public int slot;
    public bool active;
    private PlayerMovement move;
    private LineRenderer laser;
    public Transform LaserHit;

    [SerializeField] private int firerate;

    private float fire;
    // Update is called once per frame
    private void Start()
    {
        PV = transform.parent.parent.parent.GetComponent<PhotonView>();

        move = transform.parent.parent.parent.GetComponent<PlayerMovement>();
        fire = firerate;
        laser = GetComponent<LineRenderer>();
    }

    void Update()
    {
        if (PV.IsMine)
        {


            if (fire == 80)
            {
                move.enabled = true;
                laser.enabled = false;
            }

            if (fire >= firerate)
            {
                switch (slot)
                {
                    case 0:
                        if (Input.GetKey(KeyCode.Z))
                        {
                            Fire();
                            fire = 0;
                        }

                        break;
                    case 1:
                        if (Input.GetKey(KeyCode.E))
                        {
                            Fire();
                            fire = 0;

                        }

                        break;
                    case 2:
                        if (Input.GetKey(KeyCode.R))
                        {
                            Fire();
                            fire = 0;
                        }

                        break;
                }
            }

            else if (fire < firerate)
            {
                fire++;
            }
        }
    }

    void Fire()
    {
        laser.enabled = true;
        RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.right);
        LaserHit.position = hit.point;
        laser.SetPosition(0,transform.position);
        laser.SetPosition(1,LaserHit.position);
        move.enabled = false;
    }
}
