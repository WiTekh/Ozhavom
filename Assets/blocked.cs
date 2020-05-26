using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class blocked : MonoBehaviour
{
    // Start is called before the first frame update
    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("BadBullet"))
        {
            Destroy(other.gameObject);
        }
    }
}
