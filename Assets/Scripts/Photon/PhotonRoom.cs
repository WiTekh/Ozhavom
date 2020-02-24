using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;
using System.IO;
using System.Xml.Schema;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PhotonRoom : MonoBehaviourPunCallbacks, IInRoomCallbacks
{
    public static PhotonRoom Room;
    private PhotonView PV;

    public int currentScene;
    public int multiplayerScene;

    private void Awake()
    {
        if (PhotonRoom.Room == null)
        {
            PhotonRoom.Room = this;
        }
        else
        {
            if (PhotonRoom.Room != this)
            {
                Destroy(PhotonRoom.Room.gameObject);
                PhotonRoom.Room = this;
            }
        }
        DontDestroyOnLoad(this);
    }

    private void Start()
    {
        //Ensuring that we are connected to master server
        if (!PhotonNetwork.IsConnected)
        {
            Debug.Log("Wasn't connected, trying again ...");
            PhotonNetwork.ConnectUsingSettings();
        }
        PV = GetComponent<PhotonView>();
    }

    public override void OnEnable()
    {
        base.OnEnable();
        PhotonNetwork.AddCallbackTarget(this);
        SceneManager.sceneLoaded += OnSceneFinishedLoading;
    }

    public override void OnDisable()
    {
        base.OnDisable();
        PhotonNetwork.RemoveCallbackTarget(this);
        SceneManager.sceneLoaded -= OnSceneFinishedLoading;
    }

    public override void OnJoinedRoom()
    {
        base.OnJoinedRoom();
        Debug.Log("You are now connected to a room");
        StartGame();
    }

    void StartGame()
    {
        if (!PhotonNetwork.IsMasterClient)
            return;
        PhotonNetwork.LoadLevel(multiplayerScene);
    }

    void OnSceneFinishedLoading(Scene scene, LoadSceneMode mode)
    {
        currentScene = scene.buildIndex;
        
        if (currentScene == multiplayerScene)
            CreatePlayer();
    }

    private void CreatePlayer()
    {
        PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "NetworkPlayer"), transform.position, Quaternion.identity, 0 );
    }
}
