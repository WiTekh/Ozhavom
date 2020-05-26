using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class miniMap : MonoBehaviour
{
    public List<Vector3> rooms = new List<Vector3>();

    private void Start()
    {
        for (int i = 0; i < 9; i++)
        {
            for (int j = 0; j < 9; j++)
            {
                GameObject oo = Instantiate(Resources.Load("mmRoom"), Vector3.zero, Quaternion.identity) as GameObject;
                oo.name = oo.transform.position.ToString();
                oo.transform.SetParent(transform);
                oo.transform.localPosition = new Vector3((i) * 15, (j) * 15);
                oo.transform.localScale = Vector3.one;
                oo.SetActive(false);
            }
        }
    }

    public void rStart()
    {
        Transform map = GameObject.Find("DUNGEON").transform;
        
        for (int i = 0; i < map.childCount; i++)
        {
            rooms.Add(map.GetChild(i).position);
        }
    }
}
