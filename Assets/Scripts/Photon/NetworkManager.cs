using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;

public class NetworkManager : MonoBehaviourPunCallbacks
{
    public static NetworkManager Lobby;

    private void Awake()
    {
        Lobby = this;
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log($"Connected on {PhotonNetwork.CloudRegion} region");
        PhotonNetwork.AutomaticallySyncScene = true;
    }
    
    public void OnClick()
    {
        if (PhotonNetwork.OfflineMode)
            CreateRoom();
        else
            PhotonNetwork.JoinRandomRoom();
    }

    public override void OnJoinedRoom()
    {
        Debug.Log("You just joined a room");
    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        Debug.Log("Failed to join a random game, creating a room ...");
        CreateRoom();
    }

    void CreateRoom()
    {
        int randomRoomId = UnityEngine.Random.Range(0, 99999);
        RoomOptions defaultROps = new RoomOptions() {IsVisible =  true, IsOpen = true, MaxPlayers = 4};
        PhotonNetwork.CreateRoom($"Room_{randomRoomId.ToString()}", defaultROps);
        
        Debug.Log($"Room created with the id {randomRoomId.ToString()}");
    }

    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        //Recalling Create if OnCreateRoomFailed called bc that RoomName already exists
        CreateRoom();
    }
}
