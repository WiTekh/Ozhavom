using System;
using DG.Tweening;
using Photon.Pun;
using Photon.Pun.Demo.PunBasics;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Assertions.Must;
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

    public bool paused = false;
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

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!paused)
            {
                for (int i = 0; i < 5; i++)
                {
                    GameObject.Find("Canvas").transform.GetChild(i).gameObject.SetActive(false);
                }

                GameObject.Find("Canvas").transform.GetChild(7).gameObject.SetActive(true);
                transform.GetChild(0).DOMoveZ(-70, 1f);
                transform.GetChild(0).GetComponent<Camera>().orthographic = false;
            }
            else
            {
                for (int i = 0; i < 5; i++)
                {
                    GameObject.Find("Canvas").transform.GetChild(i).gameObject.SetActive(true);
                }

                transform.GetChild(0).transform.position = new Vector3(transform.GetChild(0).transform.position.x, transform.GetChild(0).transform.position.y, -1f);
                GameObject.Find("Canvas").transform.GetChild(7).gameObject.SetActive(false);
                transform.GetChild(0).GetComponent<Camera>().orthographic = true;
            }

            paused = !paused;
        }

        //Cheat Load Stage 2
        if (PhotonNetwork.IsMasterClient && Input.GetKeyDown(KeyCode.L))
        {
            Debug.Log("Loading players into Stage 2");
            gameObject.GetComponent<matrixe>().DestroyDungeon();
            gameObject.GetComponent<matrixe>().Generate(Vector2.zero);
            gameObject.transform.position = Vector2.zero;
            
            //Balancer les joueurs en (0,0)
        }
        
    }
}
