using System.Collections;
using System.Collections.Generic;
using System.IO;
using Photon.Pun;
using UnityEngine;

public class PhotonPlayer : MonoBehaviour
{
    private PhotonView PV;
    public GameObject myAvatar;
    
    
    private int _nbPl;

    void Start()
    {
        PV = GetComponent<PhotonView>();
        _nbPl = PhotonNetwork.CurrentRoom.PlayerCount - 1;
        if (PV.IsMine)
        {
            myAvatar = PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "Avatar"),
                GameSetup.GS.SpawnPoints[_nbPl].position, Quaternion.identity, 0);
        }
    }
}
