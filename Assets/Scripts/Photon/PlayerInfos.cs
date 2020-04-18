using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;
using TMPro;
using UnityEngine.UI;
using UnityEngine;

public class PlayerInfos : MonoBehaviour
{
    public static PlayerInfos PI;

    public int mySelectedChar;

    public GameObject[] allCharacters;

    public string name;
    public TMP_InputField inputName;

    public void OnEnable()
    {
        if (PlayerInfos.PI == null)
        {
            PlayerInfos.PI = this;
        }
        else if (PlayerInfos.PI != this)
        {
            Destroy(PlayerInfos.PI.gameObject);
            PlayerInfos.PI = this;
        }
        DontDestroyOnLoad(this.gameObject);
    }

    void Start()
    {
        inputName = GameObject.Find("Canvas").transform.GetChild(5).GetComponent<TMP_InputField>();
        if (PlayerPrefs.HasKey("MyChar"))
        {
            mySelectedChar = PlayerPrefs.GetInt("MyChar");
        }
        else
        {
            mySelectedChar = 0;
            PlayerPrefs.SetInt("MyChar", mySelectedChar);
        }
    }

    public void Update()
    {
        name = inputName.text;
        PhotonNetwork.LocalPlayer.NickName = name;
    }
}
