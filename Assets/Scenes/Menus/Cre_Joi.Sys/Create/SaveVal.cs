using System;
using Photon.Pun.Demo.Cockpit;
using TMPro;
using UnityEngine.UI;
using UnityEngine;

public class SaveVal : MonoBehaviour
{
    public Slider maxPlayer;
    public TMP_InputField roomName;
    public Toggle isVisible;

    public Text maxPlInput;

    public float MaxPlayerNb => maxPlayer.value;
    public string RoomNameStr => roomName.text;
    public bool IsRoomVisible => isVisible.isOn;

    private void Update()
    {
        maxPlInput.text = MaxPlayerNb.ToString();
    }
}
