using System;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using UnityEngine.UI;

public class NetworkSetter : MonoBehaviourPunCallbacks
{
    #region UI Setting

#pragma warning disable 0649
    [SerializeField] GameObject onlineText;
#pragma warning disable 0649
    [SerializeField] GameObject offlineText;
    
    #endregion

    public Button multiButton;
    void Awake()
    {
        UpdateText(true);
        PhotonNetwork.ConnectUsingSettings();
    }

    public override void OnConnectedToMaster()
    {
        //Updating UI
        UpdateText(false);
        multiButton.interactable = true;
        
        Debug.Log($"You are now connected to Photon's {PhotonNetwork.CloudRegion}Master server!");
        PhotonNetwork.AutomaticallySyncScene = true;
    }

    public override void OnDisconnected(DisconnectCause cause)
    {
        //Updating UI
        UpdateText(true);
        multiButton.interactable = false;
        
        PhotonNetwork.ConnectUsingSettings();
    }

    private void UpdateText(bool isOffline)
    {
        offlineText.SetActive(isOffline);
        onlineText.SetActive(!isOffline);
    }

    private void OnFailedToConnectToMasterServer()
    {
        Debug.Log("Failed to connect to Photon Master Server, retrying ...");
        try
        {
            PhotonNetwork.ConnectUsingSettings();
        }
#pragma warning disable 0168
        catch (Exception e)
        {
            Console.WriteLine("Connexion Failed, try again later :(");
        }
    }
}
