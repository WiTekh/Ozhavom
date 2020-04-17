using System;
using Photon.Pun;
using Photon.Pun.Demo.PunBasics;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class playerStats : MonoBehaviour
{
    public AvatarSetup AS;
    
    public float currentH;
    public Slider healthBar;

    [SerializeField]
    private float speed;

    [SerializeField]
    public int coinAmount;

    [SerializeField] private TMP_Text playerName;
    public TMP_Text coinHeap;
    
    public void Awake()
    { 
        AS = gameObject.GetComponent<AvatarSetup>();
        playerName = GameObject.Find("Canvas").transform.GetChild(0).GetChild(5).GetComponent<TMP_Text>();
    }

    public void Start()
    {
        currentH = AS.maxH;
        speed = AS.speed;
        
        healthBar = GameObject.Find("Canvas").transform.GetChild(0).GetComponent<Slider>();
        coinHeap = GameObject.Find("Canvas").transform.GetChild(0).GetChild(4).GetComponent<TMP_Text>();
    }

    public void Update()
    {
        healthBar.value = currentH / AS.maxH;
        coinHeap.text = coinAmount.ToString(); 
    }
}
