using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class WeaponRotation : MonoBehaviour
{
    private PhotonView PV;
    
    private Transform transform;

    private void Start() // Start function is called before the first frame
    {
        PV = GetComponentInParent<PhotonView>();
        transform = GetComponent<Transform>();
    }

    void Update() // called once per frame
    {
        if (PV.IsMine)
            Rotate();
    }
    private void Rotate()
    {
        //Will have to change that bc all players have different cameras
        Camera myCam = Camera.main;
        
        Vector3 dir = Input.mousePosition - myCam.WorldToScreenPoint(transform.position);
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }
    
}
