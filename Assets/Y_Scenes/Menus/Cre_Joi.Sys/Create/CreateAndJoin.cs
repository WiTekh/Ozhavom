using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

public class CreateAndJoin : MonoBehaviourPunCallbacks
{
    private SaveVal SV;

    private float _maxPl;
    private string _roomName;
    private bool _isRoomVis;

    void Start()
    {
        SV = GameObject.Find("UIManager").GetComponent<SaveVal>();
    }
    
    void Update()
    {
        _maxPl = SV.MaxPlayerNb;
        _roomName = SV.RoomNameStr;
        _isRoomVis = SV.IsRoomVisible;
    }
    /*
    public void CreateAndJoinRoom()
    {
        //Customizing the options
        RoomOptions RO = new RoomOptions();
        RO.IsVisible = _isRoomVis;
        RO.MaxPlayers = (byte) _maxPl;
        
        //Create the room using the options
        PhotonNetwork.CreateRoom(_roomName, RO);
    }*/

    public void CreateRoom()//Join room GetField
    {
        RoomOptions RO = new RoomOptions();
        RO.IsVisible = _isRoomVis;
        RO.MaxPlayers = (byte) _maxPl;
        
        PhotonNetwork.JoinOrCreateRoom(_roomName, RO, TypedLobby.Default); //dev mode
    }
    
    public override void OnCreatedRoom()
    {
        PhotonNetwork.JoinRoom(_roomName);
        Debug.Log($"Room has been created with the following settings :\nName : {_roomName} \nMax Players : {_maxPl.ToString()}");
    }

    public override void OnJoinedRoom()
    {
        Debug.Log($"The Room {_roomName} has been joined, loading level ...");
        PhotonNetwork.LoadLevel(5);
    }

    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        Debug.Log("Room failed to create, please try again later");
    }

    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        Debug.Log("Failed to create the room");
    }
}
