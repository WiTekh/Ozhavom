using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dmg_Melee : MonoBehaviour
{
    public bool inFront;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Ennemy"))
            inFront = true;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Ennemy"))
            inFront = false;
    }
}
