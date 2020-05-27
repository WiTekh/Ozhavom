using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class variablesStock : MonoBehaviour
{
    private Sprite _activeWeapon;

    public bool canGive = false;
    public int weapon;
    
    public Vector2 spawnOffset;
    public int[] slots = new int[3];
    public GameObject bossRoom;
    public Sprite[] availableSprites = new Sprite[3];
    public Sprite blank;

    private void Start()
    {
        //Init of slots
        for (int i = 0; i < slots.Length; i++) {
            slots[i] = -1;
        }
        
        Transform canvas = GameObject.Find("Canvas").transform;
        
        // -- Weapon Init -- 
        //Active Weapon
        canvas.GetChild(2).GetChild(1).GetComponent<Image>().sprite = blank;
        //Slot 1 Weapon
        canvas.GetChild(3).GetChild(1).GetComponent<Image>().sprite = blank;
        //Slot 2 Weapon
        canvas.GetChild(4).GetChild(1).GetComponent<Image>().sprite = blank;
        // -----------------------
    }
    
    public void UpdateIcons(int i)
    {
        Debug.Log("Updating Icons ... ");
        Transform canvas = GameObject.Find("Canvas").transform;

        switch (i)
        {
            // -- Weapon Management -- 
            //Slot 1 Weapon
            case 0:
                canvas.GetChild(2).GetChild(1).GetComponent<Image>().sprite = availableSprites[slots[0]];
                break;
            //Slot 2 Weapon
            case 1:
                canvas.GetChild(3).GetChild(1).GetComponent<Image>().sprite = availableSprites[slots[1]];
                break;
            //Slot 3 Weapon
            default:
                canvas.GetChild(4).GetChild(1).GetComponent<Image>().sprite = availableSprites[slots[2]];
                break;
            // -----------------------
        }
    }

    public void DeleteLastIcon()
    {
        Transform canvas = GameObject.Find("Canvas").transform;
        canvas.GetChild(3).GetChild(3).GetComponent<Image>().sprite = blank;
    }
}
