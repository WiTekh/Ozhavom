using System.Collections;
using System.Collections.Generic;
using DG.Tweening.Plugins.Core.PathCore;
using Photon.Pun;
using UnityEngine;
using Path = System.IO.Path;

public class PoisonDart : MonoBehaviour
{
    private PhotonView PV;
    [SerializeField] private int firerate;
    [SerializeField] public bool active;
    [SerializeField] public int slot;
   
    private int fire;
    // Update is called once per frame
    private void Start()
    {
        fire = firerate;
        PV = transform.parent.parent.parent.GetComponent<PhotonView>();

    }
   
    private void Update()
    {
        if (PV.IsMine)
        {


            if (fire < firerate)
            {
                fire++;
            }

            if (fire >= firerate)
            {
                switch (slot)
                {
                    case 0:
                        if (Input.GetKey(KeyCode.Z))
                        {
                            fire = 0;
                            Fire();
                            
                        }

                        break;
                    case 1:
                        if (Input.GetKey(KeyCode.E))
                        {
                            fire = 0;

                            Fire();

                        }

                        break;
                    case 2:
                        if (Input.GetKey(KeyCode.R))
                        {
                            fire = 0;
                            Fire();
                        }

                        break;
                }
            }
        }
    }
   
    void Fire()
    {
        GameObject bullet = PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "PoisonDart"), transform.position, transform.rotation);
          
    }
}
