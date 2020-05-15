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
        PV.RPC("Ownership", RpcTarget.AllBuffered);
        
        GameObject oo = GameObject.FindWithTag("spawner");
    }

    [PunRPC]
    void RPC_AddChar(int whichChar)
    {
        charVal = whichChar;
        myChar = Instantiate(PlayerInfos.PI.allCharacters[whichChar], transform.position, transform.rotation,
            transform);
    }

    [PunRPC]
    void Ownership()
    {
        if (PV.IsMine)
        {
            PhotonView PVG = transform.GetChild(1).GetComponent<PhotonView>();
            PhotonView PVM = transform.GetChild(1).GetChild(4).GetChild(0).GetComponent<PhotonView>();
            PhotonView PVW = transform.GetChild(1).GetChild(5).GetChild(0).GetComponent<PhotonView>();
            PVG.TransferOwnership(PV.Owner);
            PVM.TransferOwnership(PV.Owner);
            PVW.TransferOwnership(PV.Owner);
            
        }
    }
}
