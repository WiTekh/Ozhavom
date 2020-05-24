using System.Collections;
using System.Collections.Generic;
using System.IO;
using Photon.Pun;
using UnityEngine;

public class Mine : MonoBehaviour
{
    [SerializeField] public bool active;
    public Sprite weaponRenderer;
    private variablesStock _dataHandler;

    private PhotonView PV;
    [SerializeField] private int firerate;
    [SerializeField] public int slot;
   
    private int fire;

    private void Start()
    {
        fire = firerate;
        PV = transform.parent.GetComponent<PhotonView>();

        _dataHandler = GameObject.Find("varHolder").GetComponent<variablesStock>();
    }
   
    private void Update()
    {
        _dataHandler.GetComponent<variablesStock>().activeWeapon = weaponRenderer;

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
            else
            {
                fire++;
            }
        }
    }
   
    void Fire()
    {
        GameObject yes = PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "Mine"), transform.position, transform.rotation);
    }
}
