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

    private void Awake()
    {
        PV = GetComponent<PhotonView>();
        if (PV.IsMine)
            PV.RPC("RPC_AddChar", RpcTarget.AllBuffered, PlayerInfos.PI.mySelectedChar);
    }

    [PunRPC]
    void RPC_AddChar(int whichChar)
    {
        charVal = whichChar;
        myChar = Instantiate(PlayerInfos.PI.allCharacters[whichChar], transform.position, transform.rotation,
            transform);
    }
}
