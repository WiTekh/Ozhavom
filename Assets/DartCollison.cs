using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DartCollison : MonoBehaviour
{
    public float dmg = 15;
    public float poison = 5;

    private void Update()
    {
        transform.Translate(0.4f,0,0);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
      
        if (collision.CompareTag("Ennemy"))
        {
            Debug.Log($"Dealt {dmg}");
            collision.gameObject.GetComponent<ennemyStats>().health -= dmg;
            collision.gameObject.GetComponent<ennemyStats>().poison += poison;
        
        }
     

        if (!collision.CompareTag("Player")&& !collision.CompareTag("Bullet"))
        {
         
            Destroy(gameObject);
        }
      
    }
}
