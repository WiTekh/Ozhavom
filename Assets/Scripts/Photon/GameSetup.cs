using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameSetup : MonoBehaviour
{
    public static GameSetup GS;

    public Transform[] SpawnPoints;

    public TMP_Text coinsAmount;
    public Slider healthBar;

    public Slider octoBar;

    private (bool, bool, bool, bool, bool, bool, bool, bool, bool, bool, bool)[,] matrix;
    public void OnEnable()
    {
        if (GameSetup.GS == null)
            GameSetup.GS = this;
    }

    public void Start()
    {
        GameObject.Find("Canvas").transform.GetChild(0).GetChild(5).GetComponent<TMP_Text>().text =
            PhotonNetwork.LocalPlayer.NickName;
        octoBar = GameObject.Find("Canvas").transform.GetChild(1).GetComponent<Slider>();
    }
}
