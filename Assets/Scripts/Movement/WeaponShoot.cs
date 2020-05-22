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

    [SerializeField] private int firerate;

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
            if (Input.GetMouseButton(0) && firerate <= fire)
            {
                GameObject bullet = PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "Bullet"),
                    transform.position, transform.rotation);
                bullet.GetComponent<BulletColision>().dmg = 50;
                fire = 0;
            }
            else if (fire < firerate)
            {
                fire++;
            }
        }
    }
}