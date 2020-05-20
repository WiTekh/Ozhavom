using System.Collections;
using System.Collections.Generic;
using System.IO;
using Photon.Pun;
using UnityEngine;
using Debug = System.Diagnostics.Debug;

public class PhotonPlayer : MonoBehaviour
{
    private PhotonView PV;
    public GameObject myAvatar;
    public int charVal;
    
    private int _nbPl;

    void Start()
    {
        PV = GetComponent<PhotonView>();
        charVal = PlayerInfos.PI.mySelectedChar;
        if (PV.IsMine)
        {
            if (charVal == 0)
            {
                myAvatar = PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "Crab"),
                    GameSetup.GS.SpawnPoints[PhotonNetwork.PlayerList.Length -1].position, Quaternion.identity, 0);
            }
            else if (charVal == 1)
            {
                myAvatar = PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "Goblin"),
                    GameSetup.GS.SpawnPoints[PhotonNetwork.PlayerList.Length -1].position, Quaternion.identity, 0);
            }
            else if (charVal == 2)
            {
                myAvatar = PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "Gnoll"),
                    GameSetup.GS.SpawnPoints[PhotonNetwork.PlayerList.Length -1].position, Quaternion.identity, 0);
            }
            else
            {
                myAvatar = PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "Golem"),
                    GameSetup.GS.SpawnPoints[PhotonNetwork.PlayerList.Length -1].position, Quaternion.identity, 0);
            }
        }
    }
}
