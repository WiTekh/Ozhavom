using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class blocked : MonoBehaviour
{
    // Start is called before the first frame update
    private void OnCollisionStay2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("BadBullet"))
        {
            Destroy(other.gameObject);
        }
    }
}
