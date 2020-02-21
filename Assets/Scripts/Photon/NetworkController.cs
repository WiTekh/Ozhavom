using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class NetworkController : MonoBehaviourPunCallbacks
{
    void Start()
    {
        PhotonNetwork.ConnectUsingSettings(); //Connects to Photon master servers
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("You are now connected to Photon's '" + PhotonNetwork.CloudRegion + "Master server!");
    }
}
