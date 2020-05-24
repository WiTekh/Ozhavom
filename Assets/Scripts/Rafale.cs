using System.Collections;
using System.Collections.Generic;
using System.IO;
using Photon.Pun;
using UnityEngine;

public class Rafale : MonoBehaviour
{
    [SerializeField] public bool active;
    public Sprite weaponRenderer;
    
    public  variablesStock _dataHandler;

    private PhotonView PV;
    [SerializeField] private int firerate;
    [SerializeField] public int slot;

    private int fire;

    // Update is called once per frame
    private void Awake()
    {
        fire = firerate;
        PV = transform.parent.parent.parent.GetComponent<PhotonView>();

        _dataHandler = GameObject.Find("varHolder").GetComponent<variablesStock>();
    }

    private void Update()
    {
        if (PV.IsMine)
        {
            if (fire < firerate)
            {
                fire++;
            }

            if (fire == 10)
            {
                Fire();
            }

            if (fire == 20)
            {
                Fire();
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
        }
    }

    void Fire()
    {
        GameObject bullet = PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "Bullet"), transform.position,
            transform.rotation);
    }
}