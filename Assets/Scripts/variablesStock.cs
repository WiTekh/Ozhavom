using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class variablesStock : MonoBehaviour
{
    private Sprite _activeWeapon;

    public Vector2 spawnOffset;

    public Sprite activeWeapon;
    public Sprite slot1Wp;
    public Sprite slot2Wp;

    private void Update()
    {
        Transform canvas = GameObject.Find("Canvas").transform;

        // -- Weapon Management -- 
        //Active Weapon
        canvas.GetChild(2).GetChild(2).GetComponent<Image>().sprite = activeWeapon;
        //Slot 1 Weapon
        canvas.GetChild(3).GetChild(2).GetComponent<Image>().sprite = slot1Wp;
        //Slot 2 Weapon
        canvas.GetChild(4).GetChild(2).GetComponent<Image>().sprite = slot2Wp;
        // -----------------------
        
        // -- Spell Management --
        //Spell 1
        
        //Spell 2
        
        //Spell 3
        
        // ----------------------
    }
}
