﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSetup : MonoBehaviour
{
    public static GameSetup GS;

    public Transform SpawnPoints;

    public void OnEnable()
    {
        if (GameSetup.GS == null)
            GameSetup.GS = this;
    }
}
