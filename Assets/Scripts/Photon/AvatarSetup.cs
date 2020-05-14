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
                maxH = 300;
                speed = 5;
                break;
            case 1:
                maxH = 200;
                speed = 7;
                break;
            case 2:
                maxH = 100;
                speed = 10;
                break;
            default:
                maxH = 100;
                break;
        }
    }

    private void Start()
    { 
        //Getting the PV of the weapon and setting its ownership to the Local Player
        if (PV.IsMine)
        {
            PhotonView PV2 = transform.GetChild(1).GetComponent<PhotonView>();
            PhotonView PV3 = transform.GetChild(1).GetChild(4).GetChild(0).GetComponent<PhotonView>();
            PhotonView PV4 = transform.GetChild(1).GetChild(5).GetChild(0).GetComponent<PhotonView>();
            PV2.TransferOwnership(PhotonNetwork.LocalPlayer);
            PV3.TransferOwnership(PhotonNetwork.LocalPlayer);
            PV4.TransferOwnership(PhotonNetwork.LocalPlayer);
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
