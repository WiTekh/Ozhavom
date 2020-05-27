using System.Collections;
using System.Collections.Generic;
using System.IO;
using Photon.Pun;
using UnityEngine;

public class AttackAoe : MonoBehaviour
{
    [SerializeField] public bool active;
    public Sprite weaponRenderer;
    private variablesStock _dataHandler;
    public int upgrade;
    [SerializeField] private Sprite _sprite;

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
            if (fire >= firerate - 10*upgrade)
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
       GameObject yes = PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "AoeDmg"), transform.position, transform.rotation);
       
       yes.GetComponent<BulletColision>()._sprite = _sprite;
    }
}
