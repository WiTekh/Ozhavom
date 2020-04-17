using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameSetup : MonoBehaviour
{
    public static GameSetup GS;

    public Transform SpawnPoints;

    public TMP_Text coinsAmount;
    public Slider healthBar;

    public void OnEnable()
    {
        if (GameSetup.GS == null)
            GameSetup.GS = this;
    }
}
