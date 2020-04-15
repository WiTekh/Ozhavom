using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Random = System.Random;

public class ListRooms : MonoBehaviourPunCallbacks
{
    public GameObject roomTemplate;
    
    public GameObject[] SPs;
    public void Start()
    {
        PhotonNetwork.JoinLobby();
    }

    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        int cnt = 0;
        foreach (var room in roomList)
        {
            //Getting all the usefull values
            int currentPlayers = room.PlayerCount;
            int maxPlayers = room.MaxPlayers;
            string roomName = room.Name;

            //Getting the access to all the UI stuff ;)
            GameObject Room = Instantiate(roomTemplate, SPs[cnt].transform) as GameObject;

            TMP_Text roomTitle = Room.GetComponentInChildren<TMP_Text>();
            Text roomNbPlayers = Room.GetComponentInChildren<Text>();

            //Displaying all values on UI
            roomTitle.text = roomName;
            roomNbPlayers.text = $"{currentPlayers.ToString()}/{maxPlayers.ToString()}";

            Debug.Log(currentPlayers.ToString() + "/" + maxPlayers.ToString() + " : " + roomName);
            cnt++;
        }
    }
}
