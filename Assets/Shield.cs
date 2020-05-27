using System.Collections;
using System.Collections.Generic;
using System.IO;
using Photon.Pun;
using UnityEngine;

public class Shield : MonoBehaviourPunCallbacks, IPunObservable
{
    // Start is called before the first frame update

    [SerializeField] private Sprite _sprite;
    // Start is called before the first frame update
    [SerializeField] public bool active;
    public Sprite weaponRenderer;
    private variablesStock _dataHandler;

    private PhotonView PV;
    [SerializeField] private int firerate;
    [SerializeField] public int slot;
    [SerializeField] private GameObject GameObject;
    private bool fornetwork = false;
    public int upgrade;

    private int fire;

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
            if (fire == 50)
            {
                GameObject.SetActive(false);
                fornetwork = false;
            }
            if (fire >= firerate - 10 * upgrade)
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
        fornetwork = true;
        GameObject.SetActive(true);
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(fornetwork);
        }
        else if (stream.IsReading)
        {
            GameObject.SetActive((bool)stream.ReceiveNext());
        }
    }
}
