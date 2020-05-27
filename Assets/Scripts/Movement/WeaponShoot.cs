using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Photon.Pun;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WeaponShoot : MonoBehaviour
{
    private AvatarSetup AS;
    private PhotonView PV;

    [SerializeField] GameObject bulletprefab;
    [SerializeField] public int dmg = 50;
    [SerializeField] private int firerate;
    public int upgrade = 0;

    public TMP_Text coinsAmount;
    public Slider healthBar;

    [SerializeField] int fire;

    private void Start()
    {
        Transform avatar = transform.parent.parent.parent;
        AS = avatar.GetComponent<AvatarSetup>();
        PV = avatar.GetComponent<PhotonView>();

        healthBar = GameSetup.GS.healthBar;
        coinsAmount = GameSetup.GS.coinsAmount;

        fire = firerate;
    }

    void Update()
    {
        if (PV.IsMine)
        {
            if (Input.GetMouseButton(0) && firerate-5*upgrade <= fire)
            {
//                GameObject bullet = PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "Bullet"),
//                    transform.position, transform.rotation);
//                bullet.GetComponent<BulletColision>().dmg = dmg+5*upgrade;
//                
//                
                //Change shooting depending on character
                switch (GameObject.Find("PlayerInfos").GetComponent<PlayerInfos>().mySelectedChar)
                {
                    case 0:
                        Debug.Log("Crab");
                        
                        break; 
                    case 1:
                        Debug.Log("Gobelin");
                        break;
                    case 2:
                        Debug.Log("Gnoll");
                        break;
                    case 3:
                        Debug.Log("Golem");
                        break;
                }

                fire = 0;
            }
            else if (fire < firerate)
            {
                fire++;
            }
        }
    }
}