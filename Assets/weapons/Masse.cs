using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class Masse : MonoBehaviourPunCallbacks, IPunObservable
{
    [SerializeField] private int firerate;
    private int fire;
    private SpriteRenderer sr;
    private BoxCollider2D bc;
    private Transform rb;
    private PhotonView PV;
    [SerializeField] public bool active;
    [SerializeField] public int slot;
    [SerializeField] private MasseDmg MasseDmg;
    private bool yes = false;
    Vector3 vect = new Vector3(0,0,1);
    private void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        bc = GetComponent<BoxCollider2D>();
        fire = firerate;
        rb = transform.parent;
        PV = GetComponent<PhotonView>();
    }

    private void Update()
    {
        if (PV.IsMine)
        {


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

            if (fire < firerate)
            {
                fire++;
            }

            if (fire == 1080)
            {
                sr.enabled = false;
                bc.enabled = false;
                MasseDmg.enabled = false;
                yes = false;
            }

            if (fire < 1080)
            {
                rb.rotation *= Quaternion.Euler(vect);
            }
        }
    }
    void Fire()
    {
        sr.enabled = true;
        bc.enabled = true;
        MasseDmg.enabled = true;
        yes = true;
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(yes);
        }
        else if (stream.IsReading)
        {
            yes = (bool) stream.ReceiveNext();
            sr.enabled = yes;
            bc.enabled = yes;
            MasseDmg.enabled = yes;
        }
    }
}


