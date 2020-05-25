using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class MoreShoot : MonoBehaviourPunCallbacks, IPunObservable
{
    [SerializeField] public bool active;
    public Sprite weaponRenderer;
    private variablesStock _dataHandler;
    private bool fornetwork = true;

    private PhotonView PV;
    [SerializeField] private int firerate;
    [SerializeField] public int slot;
   
    private int fire;
    // Update is called once per frame
    private void Start()
    {
        fire = firerate;
        PV = transform.parent.GetComponent<PhotonView>();

        _dataHandler = GameObject.Find("varHolder").GetComponent<variablesStock>();
    }
   
    private void Update()
    {
        if (PV.IsMine)
        {
            if (fire == 125)
            {
                transform.parent.parent.parent.GetChild(5).gameObject.SetActive(false);
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
        transform.parent.parent.parent.GetChild(5).gameObject.SetActive(true);
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(fornetwork);
        }

        if (stream.IsReading)
        {
            transform.parent.parent.parent.GetChild(5).gameObject.SetActive(fornetwork);
        }
    }
}
