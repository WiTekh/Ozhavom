using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MasseDmg : MonoBehaviour
{
    [SerializeField] private int dmg;
    // Start is called before the first frame update
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Ennemy") && other.gameObject.CompareTag("Boss"))
        {
            other.gameObject.GetComponent<ennemyStats>().health -= dmg;
        }
    }
}
