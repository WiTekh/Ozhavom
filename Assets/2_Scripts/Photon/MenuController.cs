using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuController : MonoBehaviour
{
    public void OnClickSelection(int whichChar)
    {
        if (PlayerInfos.PI != null)
        {
            PlayerInfos.PI.mySelectedChar = whichChar;
            PlayerPrefs.SetInt("MyChar", whichChar);
        }
    }
    
}
