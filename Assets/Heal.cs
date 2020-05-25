using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heal : MonoBehaviour
{
    // Start is called before the first frame update
    private int time = 0;

    [SerializeField] public int heal;
    // Start is called before the first frame update
    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            float maxhealth = other.gameObject.GetComponent<AvatarSetup>().maxH;
            float hp = other.gameObject.GetComponent<playerStats>().currentH;
          
                if (maxhealth >=  hp+ heal)
                {
                    other.gameObject.GetComponent<playerStats>().currentH = hp + heal;
                }
                else
                {
                    other.gameObject.GetComponent<playerStats>().currentH += maxhealth - hp;
                }
                
            
            
        }
    }

    private void Update()
    {
        
        if (time == 2)
        {
            Destroy(gameObject);
        }
        time++;
    }
}
