using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class goContainer : MonoBehaviour
{
    public GameObject button;

    public float HH;
    public float SS;

    private void Update()
    {
        HH = button.GetComponent<displayCharacter>().hVal;
        SS = button.GetComponent<displayCharacter>().sVal;
    }
}
