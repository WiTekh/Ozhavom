using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;

public class MenuController : MonoBehaviour
{ 
    public void OnClickSelection(int whichChar)
    {
        GameObject.Find("AMBIANCE").transform.GetChild(1).GetChild(0).GetComponent<AudioSource>().Play();
        if (PlayerInfos.PI != null)
        {
            PlayerInfos.PI.mySelectedChar = whichChar;
            PlayerPrefs.SetInt("MyChar", whichChar);
        }
    }
}
