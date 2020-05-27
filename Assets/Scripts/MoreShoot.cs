using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class MoreShoot : MonoBehaviour
{
    [SerializeField] public bool active;
    public Sprite weaponRenderer;
    [SerializeField] private Sprite _sprite;
    private variablesStock _dataHandler;
    public int upgrade;
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

            if (fire >= firerate - 5*upgrade)
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
        transform.parent.parent.parent.GetChild(5).gameObject.SetActive(true);
    }
}
