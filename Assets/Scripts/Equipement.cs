using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using Photon.Pun;
using UnityEngine;
using UnityEngine.UI;

public class Equipement : MonoBehaviour
{
    [Tooltip("0 -> Z || 1 -> E || 2 -> R")]
    [SerializeField] public string[] equipement = new string[3];

    [SerializeField] private int freeslot;
    [SerializeField] private Rafale _rafale;
    [SerializeField]private Masse masse;
    
    private GameObject _gameObject;
    
    [SerializeField] private LaserBeam _laserBeam;
    [SerializeField] private ChargedBeam _chargedBeam;
    [SerializeField] private PoisonDart _poisonDart;
    [SerializeField] private playerStats Stats;
    [SerializeField] private AttackAoe AoeDmg;
    [SerializeField] private HealAoe AoeHeal;
    [SerializeField] private MoreShoot MoreShoot;
    [SerializeField] private Mine Mine;
    [SerializeField] private Seisme Seisme;
    [SerializeField] private InstantHeal InstantHeal;
    [SerializeField] private Shield Shield;
    private AvatarSetup AvatarSetups;
    private PhotonView PV;
    private int coin;

    public GameObject dataHandler;

    public GameObject[] drops;

    // Start is called before the first frame update
    void Start()
    {
        AvatarSetups = GetComponent<AvatarSetup>();
        freeslot = 0;
        PV = GetComponent<PhotonView>();
        dataHandler = GameObject.Find("varHolder");
        Debug.Log(dataHandler.name);
    }


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            variablesStock stock =  GameObject.Find("varHolder").GetComponent<variablesStock>();
            if (freeslot <3)
            { 
                stock.slots[freeslot] = 0;
                
            }
           
            stock.UpdateIcons(freeslot);
            
            equipement[freeslot] = "rafale";
            _rafale.active = true;
            _rafale.slot = freeslot;
            _rafale.enabled = true;
            PhotonNetwork.Destroy(_gameObject);
            Debug.Log("equiped the rifle");
            freeslot++;
        }
    }

    private void OnCollisionEnter2D (Collision2D col)
    {
        if (PV.IsMine)
        {
            _gameObject = col.gameObject;
            if (_gameObject.CompareTag("Weapons"))
            {
                if (freeslot <= 2)
                {
                   equipitems();
                   freeslot++;

                }
            }
            else if (_gameObject.CompareTag("itemshop"))
            {
                coin = Stats.coinAmount;
                if (_gameObject.GetComponent<ShopItems>().isweapon)
                {
                    if (_gameObject.GetComponent<ShopItems>().prix <= coin && freeslot <= 2)
                    {
                        Debug.Log("I buy this");
                        Stats.coinAmount -= _gameObject.GetComponent<ShopItems>().prix;
                        equipitems();
                    }
                }
                else 
                {
                    if ( Stats.currentH < AvatarSetups.maxH)
                    {
                        if (AvatarSetups.maxH >=  Stats.currentH +_gameObject.GetComponent<ShopItems>().heal)
                        {
                            Stats.currentH = Stats.currentH  + _gameObject.GetComponent<ShopItems>().heal;
                        }
                        else
                        {
                            Stats.currentH  = AvatarSetups.maxH;
                        }
                        PhotonNetwork.Destroy(_gameObject);
                    }
                }
            }
        }
    }

    private void equipitems()
    {
        string name = _gameObject.GetComponent<ItemInfo>().weaponname;
        if (name == "rafale")
        {
            if (!_rafale.active)
            {
                dataHandler.GetComponent<variablesStock>().slots[freeslot] = 0;
                equipement[freeslot] = "rafale";
                _rafale.active = true;
                _rafale.slot = freeslot;
                _rafale.enabled = true;
                PhotonNetwork.Destroy(_gameObject);
                Debug.Log("equiped the rifle");
                freeslot++;

                //une fois les test terminer faut rajouter un PhotonNetwork. avant le destroy
            }
        }
        else if (name == "mine")
        {
            if (!Mine.active)
            {
                equipement[freeslot] = "mine";
                Mine.active = true;
                Mine.slot = freeslot;
                Mine.enabled = true;
                freeslot++;

                PhotonNetwork.Destroy(_gameObject);
                dataHandler.GetComponent<variablesStock>().slots[freeslot] = 8;

            }
        }
        else if (name == "masse")
        {
            if (!masse.active)
            {
                dataHandler.GetComponent<variablesStock>().slots[freeslot] = 1;
                equipement[freeslot] = "masse";
                masse.active = true;
                masse.slot = freeslot;
                masse.enabled = true;
                PhotonNetwork.Destroy(_gameObject);
                freeslot++;

                Debug.Log("equiped the mass");
            }
        }
        else if (name == "laserbeam")
        {
            if (!_laserBeam.active)
            {
                dataHandler.GetComponent<variablesStock>().slots[freeslot] = 2;
                equipement[freeslot] = "laserbeam";
                _laserBeam.active = true;
                _laserBeam.enabled = true;
                _laserBeam.slot = freeslot;
                PhotonNetwork.Destroy(_gameObject);
                freeslot++;

                Debug.Log("equiped the laserbeam");
            }
        }
        else if ( "poisondart" == name)
        {
            if (!_poisonDart.active)
            {
                
                equipement[freeslot] = "poisondart";
                _poisonDart.active = true;
                _poisonDart.slot = freeslot;
                _poisonDart.enabled = true;
                freeslot++;

                PhotonNetwork.Destroy(_gameObject);
                dataHandler.GetComponent<variablesStock>().slots[freeslot] = 4;
            }
        }
        else if ("aoeheal" == name)
        {
            if (!AoeHeal.active)
            {
               
                equipement[freeslot] = "aoeheal";
                AoeHeal.active = true;
                AoeHeal.slot = freeslot;
                AoeHeal.enabled = true;
                freeslot++;
               

                PhotonNetwork.Destroy(_gameObject);
                dataHandler.GetComponent<variablesStock>().slots[freeslot] = 5;
            }
        }
        else if ( "aoeattack" == name) 
        {
            if (!AoeDmg.active)
            {
                equipement[freeslot] = "aoeattack";
                AoeDmg.active = true;
                AoeDmg.slot = freeslot;
                AoeDmg.enabled = true;
                freeslot++;

                PhotonNetwork.Destroy(_gameObject);
                dataHandler.GetComponent<variablesStock>().slots[freeslot] = 6;

            }
        }
        else if ("moreshoot" == name)
        {
            if (!MoreShoot.active)
            {
                equipement[freeslot] = "moreshoot";
                MoreShoot.active = true;
                MoreShoot.slot = freeslot;
                MoreShoot.enabled = true;
                freeslot++;

                PhotonNetwork.Destroy(_gameObject);
                dataHandler.GetComponent<variablesStock>().slots[freeslot] = 7;

            }
        }
        else if ("seisme" == name)
        {
            if (!Seisme.active)
            {
                equipement[freeslot] = "seisme";
                Seisme.active = true;
                Seisme.slot = freeslot;
                Seisme.enabled = true;
                freeslot++;

                PhotonNetwork.Destroy(_gameObject);
                dataHandler.GetComponent<variablesStock>().slots[freeslot] = 9;

            }
        }
        else if ("instantheal" == name)
        {
            if (!InstantHeal.active)
            {
                equipement[freeslot] = "instantheal";
                InstantHeal.active = true;
                InstantHeal.slot = freeslot;
                InstantHeal.enabled = true;
                freeslot++;

                PhotonNetwork.Destroy(_gameObject);
                dataHandler.GetComponent<variablesStock>().slots[freeslot] = 10;

            }
        }
        else if ("shield" == name)
        {
            if (!Shield.active)
            {
                equipement[freeslot] = "shield";
                Shield.active = true;
                Shield.slot = freeslot;
                Shield.enabled = true;
                freeslot++;

                PhotonNetwork.Destroy(_gameObject);
                dataHandler.GetComponent<variablesStock>().slots[freeslot] = 11;

            }
        }

        if (freeslot >0)
        {
            GameObject.Find("varHolder").GetComponent<variablesStock>().UpdateIcons(freeslot-1);
        }
        
    }
}
