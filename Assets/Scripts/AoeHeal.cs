using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AoeHeal : MonoBehaviour
{
   

    private int time = 0;
    // Start is called before the first frame update
    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            float maxhealth = other.gameObject.GetComponent<AvatarSetup>().maxH;
            float hp = other.gameObject.GetComponent<playerStats>().currentH;
            if (time % 10 == 0)
            {
                if (maxhealth >=  hp+10)
                {
                    other.gameObject.GetComponent<playerStats>().currentH = hp + 10;
                }
                else
                {
                    other.gameObject.GetComponent<playerStats>().currentH += maxhealth - hp;
                }
                
            }
            
        }
    }

    private void Update()
    {
        time++;
        if (time == 50)
        {
            Destroy(gameObject);
        }
    }
}
