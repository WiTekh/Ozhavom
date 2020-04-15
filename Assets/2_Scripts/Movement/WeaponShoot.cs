using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Photon.Pun;
using UnityEngine;

public class WeaponShoot : MonoBehaviour
{
    [SerializeField] GameObject bulletprefab;

    [SerializeField] private int firerate;

    private int fire;
    // Update is called once per frame
    private void Start()
    {
        fire = firerate;
    }

    void Update()
    {
        if (Input.GetMouseButton(0)&& firerate <= fire)
        {
            GameObject bullet = PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "Bullet"), transform.position, transform.rotation);
            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
            rb.AddForce(transform.right*20f,ForceMode2D.Impulse);
            fire = 0;
        }
        else if (fire<firerate)
        {
            fire++;
        }
    }
}
