using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class changeVal : MonoBehaviour
{
    public float hVal;
    public float sVal;

    public int charVal;

    public Slider healthSlider;
    public Slider speedSlider;

    public bool accessed = false;

    void Start()
    {
        healthSlider = transform.GetChild(0).GetComponent<Slider>();
        speedSlider = transform.GetChild(1).GetComponent<Slider>();
    }
    
    private void Update()
    {
        healthSlider.value = GameObject.Find("dataHandler").GetComponent<goContainer>().HH;
        speedSlider.value = GameObject.Find("dataHandler").GetComponent<goContainer>().SS;
    }
}
