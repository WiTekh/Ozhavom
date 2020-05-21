using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Photon.Pun;
using UnityEngine;

public class RotationFP : MonoBehaviour
{
    private PhotonView PV;
    [SerializeField] private int firerate;
    [SerializeField] int fire;
    [SerializeField] private Camera myCam;



    // Start is called before the first frame update
    void Start()
    {
        PV = transform.parent.parent.GetComponent<PhotonView>();
        fire = firerate;
    }

    // Update is called once per frame
    void Update()
    {
        if (PV.IsMine)
        {
            if (Input.GetMouseButton(0) && firerate <= fire)
            {
                GameObject bullet = PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "Bullet"), transform.position, transform.rotation);
                bullet.GetComponent<BulletColision>().dmg = 50;
                fire = 0;
            }
            else if (fire<firerate)
            {
                fire++;
            }
            

        }

    }

    private void FixedUpdate()
    {
        Vector3 dir = Input.mousePosition - myCam.WorldToScreenPoint(transform.position);
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }
}
