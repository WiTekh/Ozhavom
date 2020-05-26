using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletPierce : MonoBehaviour
{
    public float dmg = 50;
    public int enemie = 0;

    private void Update()
    {
        transform.Translate(0.4f,0,0);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log(collision.tag);
      
        if (collision.CompareTag("Ennemy") || collision.CompareTag("Boss"))
        {
            Debug.Log($"Dealt {dmg}");
            collision.gameObject.GetComponent<ennemyStats>().health -= dmg;
            enemie++;
        }
     

        if (!collision.CompareTag("Player")&& !collision.CompareTag("Bullet"))
        {
            if (!collision.CompareTag("Ennemy")||(collision.CompareTag("Ennemy") && enemie ==2))
            {
                Destroy(gameObject);
            }
            
        }
      
    }

}
