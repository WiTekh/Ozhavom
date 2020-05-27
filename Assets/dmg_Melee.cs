using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dmg_Melee : MonoBehaviour
{
    public bool inFront;
    public List<GameObject> gOs = new List<GameObject>();
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Ennemy"))
        {
            inFront = true;
            gOs.Add(other.gameObject);
            if (other.gameObject.GetComponent<ennemyStats>().health <= 0)
            {
                gOs.Remove(other.gameObject);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Ennemy"))
        {
            inFront = false;
            gOs.Remove(other.gameObject);
        }
    }
}
