using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using DG.Tweening;
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
                //Change shooting depending on character
                switch (GameObject.Find("PlayerInfos").GetComponent<PlayerInfos>().mySelectedChar)
                {
                    case 0:
                        //Melee Attack
                        Debug.Log("Crab");
                        if (transform.parent.GetChild(3).GetComponent<dmg_Melee>().inFront)
                        {
                            Debug.Log("In front");
                            foreach (GameObject mec in transform.parent.GetChild(3).GetComponent<dmg_Melee>().gOs)
                            {
                                mec.GetComponent<ennemyStats>().health -= dmg + 5 * upgrade;
                            }
                        }
                        break; 
                    case 1:
                        Debug.Log("Gobelin");
                        GameObject FB = PhotonNetwork.InstantiateSceneObject(Path.Combine("PhotonPrefabs", "fireball"), transform.position, transform.rotation);
                        FB.GetComponent<BulletColision>().dmg = dmg + 5 * upgrade;
                        break;
                    case 2:
                        Debug.Log("Gnoll"); 
                        GameObject bullet = PhotonNetwork.InstantiateSceneObject(Path.Combine("PhotonPrefabs", "Bullet"), transform.position, transform.rotation);
                        bullet.GetComponent<BulletColision>().dmg = dmg+5*upgrade;
                        break;
                    case 3:
                        Debug.Log("Golem");
                        GameObject gogo = PhotonNetwork.InstantiateSceneObject(Path.Combine("PhotonPrefabs", "golemBullet"), transform.position, transform.rotation);
                        gogo.GetComponent<BulletColision>().dmg = dmg+5*upgrade;
                        break;
                }
                fire = 0;
            }
            else if (Input.GetMouseButton(1) && firerate - 5 * upgrade <= fire)
            {
                Debug.Log("shot heal");
                GameObject gogo = PhotonNetwork.InstantiateSceneObject(Path.Combine("PhotonPrefabs", "golemBullet"), transform.position, transform.rotation);
                fire = 0;
            }
            else if (fire < firerate)
            {
                fire++;
            }
            

        }
    }
}