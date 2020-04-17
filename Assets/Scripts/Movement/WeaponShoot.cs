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
    private playerStats PS;
    
    [SerializeField] GameObject bulletprefab;

    [SerializeField] private int firerate;

    public TMP_Text coinsAmount;
    public Slider healthBar;

    private int fire;
    
    private void Start()
    {
        Transform avatar = transform.parent.parent.parent.parent;
        AS = avatar.GetComponent<AvatarSetup>();
        PV = avatar.GetComponent<PhotonView>();

        healthBar = GameSetup.GS.healthBar;
        coinsAmount = GameSetup.GS.coinsAmount;
        
        fire = firerate;
    }

    void Update()
    {
        if (Input.GetMouseButton(0) && firerate <= fire)
        {
            PV.RPC("RPC_Shooting", RpcTarget.All);
        }
        else if (fire<firerate)
        {
            fire++;
        }

        healthBar.value = PS.currentH / PS.maxH;
        coinsAmount.text = PS.coinAmount.ToString();
    }

    [PunRPC]
    void RPC_Shooting()
    {
        GameObject bullet = PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "Bullet"), transform.position, transform.rotation);
        bullet.GetComponent<BulletColision>().dmg = 50;
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.AddForce(transform.right*20f,ForceMode2D.Impulse);
        fire = 0;
    }
}
