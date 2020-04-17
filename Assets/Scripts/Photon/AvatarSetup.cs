using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class AvatarSetup : MonoBehaviour
{
    private PhotonView PV;
    public int charVal;
    public GameObject myChar;

    public float maxH;
    public float speed;

    private void Awake()
    {
        PV = GetComponent<PhotonView>();
        if (PV.IsMine)
            PV.RPC("RPC_AddChar", RpcTarget.AllBuffered, PlayerInfos.PI.mySelectedChar);
        
        switch (charVal)
        {
            case 0:
                maxH = 100;
                speed = 30;
                break;
            case 1:
                maxH = 200;
                speed = 30;
                break;
            case 2:
                maxH = 300;
                speed = 10;
                break;
            default:
                maxH = 100;
                break;
        }
    }

    [PunRPC]
    void RPC_AddChar(int whichChar)
    {
        charVal = whichChar;
        myChar = Instantiate(PlayerInfos.PI.allCharacters[whichChar], transform.position, transform.rotation,
            transform);
    }
}
