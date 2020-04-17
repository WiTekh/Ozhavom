using System;
using Photon.Pun.Demo.PunBasics;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class playerStats : MonoBehaviour
{
    public AvatarSetup AS;
    
    public float maxH;
    public float currentH;
    public Slider healthBar;

    [SerializeField]
    private int speed;

    [SerializeField]
    public int coinAmount;
    public TMP_Text coinHeap;


    private PlayerManager _player;

    public void Awake()
    {
        AS = gameObject.GetComponent<AvatarSetup>();

        healthBar = GameObject.Find("Canvas").transform.GetChild(0).GetComponent<Slider>();
        coinHeap = GameObject.Find("Canvas").transform.GetChild(2).GetComponent<TMP_Text>();

        switch (AS.charVal)
        {
            case 0:
                maxH = 100;
                speed = 30;
                break;
            case 1:
                maxH = 200;
                speed = 30;
                break;
            case 2:
                maxH = 300;
                speed = 10;
                break;
            default:
                maxH = 100;
                break;
        }

        currentH = maxH;

    }

    public void Update()
    {
        _player.Health = currentH;
    }
}
