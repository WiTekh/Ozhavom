using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Realtime;
using UnityEngine;

public class PlayerInfos : MonoBehaviour
{
    public static PlayerInfos PI;

    public int mySelectedChar;

    public GameObject[] allCharacters;

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
}
