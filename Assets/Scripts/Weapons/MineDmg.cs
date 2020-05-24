using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MineDmg : MonoBehaviour
{
    [SerializeField] private float dmg;

    private int time = 0;
    // Start is called before the first frame update
    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Ennemy"))
        {
            other.gameObject.GetComponent<ennemyStats>().health -= dmg;
            Destroy(this.gameObject);
        }
    }

    private void Update()
    {
        time++;
        if (time == 300)
        {
            Destroy(gameObject);
        }
    }
}
