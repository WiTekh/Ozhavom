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
                //CRAB
                case 0:
                    maxH = 300;
                    speed = 6f;
                    break;
                //GOBELIN
                case 1:
                    maxH = 200;
                    speed = 6.5f;
                    break;
                //GNOLL
                case 2:
                    maxH = 250;
                    speed = 7f;
                    break;
                //GOLEM
                case 3:
                    maxH = 400;
                    speed = 5.5f;
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
