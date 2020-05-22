using System;
using Photon.Pun;
using Photon.Pun.Demo.PunBasics;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class playerStats : MonoBehaviour
{
    public AvatarSetup AS;
    
    public float currentH;
    public Slider healthBar;
    public TMP_Text healthDisp;
    [SerializeField] private PhotonView PV;
    private float speed;

    [SerializeField]
    public int coinAmount;

    [SerializeField] private TMP_Text playerName;
    public TMP_Text coinHeap;
    
    public void Awake()
    { 
        AS = gameObject.GetComponent<AvatarSetup>();
    }

    public void Start()
    {
        if (PV.IsMine)
        {
            currentH = AS.maxH;
            speed = AS.speed;
        
            healthBar = GameObject.Find("Canvas").transform.GetChild(0).GetComponent<Slider>();
            coinHeap = GameObject.Find("Canvas").transform.GetChild(0).GetChild(4).GetComponent<TMP_Text>();
            healthDisp = GameObject.Find("Canvas").transform.GetChild(0).GetChild(5).GetComponent<TMP_Text>();
        }
       
        Debug.Log(PhotonNetwork.IsMasterClient);
    }

    public void Update()
    {
        if (PV.IsMine)
        {
            if (currentH <= 0)
            {
                SceneManager.LoadScene("GameOver");
            }
            healthBar.value = currentH / AS.maxH;
            coinHeap.text = coinAmount.ToString();
            healthDisp.text = currentH + "/" + AS.maxH;
        }
        
    }
}
