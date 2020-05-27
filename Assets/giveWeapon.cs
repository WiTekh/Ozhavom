using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class giveWeapon : MonoBehaviour
{
    public variablesStock dataHandler;

    private void Start()
    {
        dataHandler = GameObject.Find("varHolder").GetComponent<variablesStock>();
    }

    public void Give(int weapon)
    {
        variablesStock oo = GameObject.Find("varHolder").GetComponent<variablesStock>();
        oo.canGive = true;
        oo.weapon = weapon;
    }
}
