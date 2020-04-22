using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;
using UnityEngine.UI;

public class octo_Pattern : MonoBehaviour
{
    public Slider octoHealth;
    
    [SerializeField] private float MaxOH;
    [SerializeField] private float CurrentOH;

    private void Start()
    {
        CurrentOH = MaxOH;
        octoHealth = GameObject.Find("Canvas").transform.GetChild(1).GetComponent<Slider>();
    }

    private void Update()
    {
        octoHealth.value = CurrentOH / MaxOH;
    }
}
