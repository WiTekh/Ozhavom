using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class AvatarSetup : MonoBehaviour
{
    public int charVal;
    public GameObject myChar;
    [SerializeField] private PhotonView PV;
    [SerializeField] private Camera cam;

    public float maxH;
    public float speed;

    private void Awake()
    {
        if (PV.IsMine)
        {
            charVal = PlayerInfos.PI.mySelectedChar;
            switch (charVal)
            {
                case 0:
                    maxH = 300;
                    speed = 6;
                    break;
                case 1:
                    maxH = 200;
                    speed = 7;
                    break;
                case 2:
                    maxH = 100;
                    speed = 8;
                    break;
                case 3:
                    maxH = 450;
                    speed = 7.5f;
                    break;
                default:
                    maxH = 100;
                    break;
            }
        }
        else
        {
            cam.enabled = false;
        }
        
    }

   
    
}
