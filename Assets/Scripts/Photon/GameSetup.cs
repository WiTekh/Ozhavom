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

    private (bool, bool, bool, bool, bool, bool, bool, bool, bool, bool, bool)[,] matrix;
    public void OnEnable()
    {
        if (GameSetup.GS == null)
            GameSetup.GS = this;
    }

    public void Start()
    {
        matrix = GameObject.Find("starter").GetComponent<matrixe>().matrix;
        GameObject.Find("Canvas").transform.GetChild(0).GetChild(5).GetComponent<TMP_Text>().text = PhotonNetwork.LocalPlayer.NickName;
        
        //Set the spawnpoints in the right room
        for (int i = 0; i < matrix.GetLength(0); i++)
        {
            for (int j = 0; j < matrix.GetLength(1); j++)
            {
                if (matrix[i,j].Item5)
                    foreach (var sp in SpawnPoints)
                    {
                        sp.position += new Vector3(i*19, j*12, 0);
                    }
            }
        }
    }
}
