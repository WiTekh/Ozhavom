using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeismeDmg : MonoBehaviour
{
    [SerializeField] private float dmg;
    [SerializeField] private SpriteRenderer sr;

    private int time = 0;
    // Start is called before the first frame update
    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Ennemy")&& time == 10)
        {
            other.gameObject.GetComponent<ennemyStats>().health -= dmg;
        }

       
    }

    private void Update()
    {
        
        if (time == 31)
        {
            Destroy(gameObject);
        }

        if (time %10 >= 5)
        {
            sr.enabled = true;
        }
        else
        {
            sr.enabled = false;
        }
        time++;
    }
}
