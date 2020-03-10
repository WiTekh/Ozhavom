using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class debugWalls : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void OnCollisionEnter()
    {
        Debug.Log("Entered the upper wall");
    }
}
