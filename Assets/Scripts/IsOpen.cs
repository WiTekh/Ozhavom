using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsOpen : MonoBehaviour
{
    public bool IsRoomOpen;

    private void Update()
    {
        for (int i = 1; i < transform.childCount; i++)
        {
            Transform gO = transform.GetChild(i);
            
            if (gO.gameObject.activeSelf)
            {
                switch (gO.tag)
                {
                    case "rTop":
                        IsRoomOpen |= gO.GetChild(0).GetComponent<OpenGateTop>().Isopen;
                        break;
                    case "rBot":
                        IsRoomOpen |= gO.GetChild(0).GetComponent<OpenGateot>().Isopen;
                        break;
                    case "rLeft":
                        IsRoomOpen |= gO.GetChild(0).GetComponent<OpenGateLeft>().Isopen;
                        break;
                    case "rRight":
                        IsRoomOpen |= gO.GetChild(0).GetComponent<OpenGateRight>().Isopen;
                        break;
                }
            }
        }
    }
}
