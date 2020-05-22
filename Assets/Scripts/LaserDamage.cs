using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserDamage : MonoBehaviour
{
    public int dmg;

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("YES");
        if (collision.CompareTag("Ennemy"))
        {
            Debug.Log($"Dealt {dmg}");
            collision.gameObject.GetComponent<ennemyStats>().health -= dmg;
        }
    }
}
