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
    private PhotonView PV;
    private int coin;

    public GameObject dataHandler;

    public GameObject[] drops;

    // Start is called before the first frame update
    void Start()
    {
        freeslot = 0;
        PV = GetComponent<PhotonView>();
        dataHandler = GameObject.Find("varHolder");
        Debug.Log(dataHandler.name);
    }


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            variablesStock stock =  GameObject.Find("varHolder").GetComponent<variablesStock>();
            stock.slots[freeslot] = 0;
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
                }
            }
            else if(_gameObject.CompareTag("itemshop"))
            {
                if (_gameObject.GetComponent<ShopItems>().isweapon &&
                    _gameObject.GetComponent<ShopItems>().prix <= coin && freeslot <= 2)
                {
                    Stats.coinAmount -= _gameObject.GetComponent<ShopItems>().prix;
                    Stats.coinAmount = coin;
                    equipitems();
                }
            }
        }
    }

    private void equipitems()
    {
         switch (_gameObject.GetComponent<ItemInfo>().weaponname)
                    {
                        case "rafale":
                            if (!_rafale.active)
                            {
                                dataHandler.GetComponent<variablesStock>().slots[freeslot] = 0;
                                equipement[freeslot] = "rafale";
                                _rafale.active = true;
                                _rafale.slot = freeslot;
                                _rafale.enabled = true;
                                PhotonNetwork.Destroy(_gameObject);
                                Debug.Log("equiped the rifle");
                                freeslot++; //une fois les test terminer faut rajouter un PhotonNetwork. avant le destroy
                            }

                            break;
                        case "masse":
                            if (!masse.active)
                            {
                                dataHandler.GetComponent<variablesStock>().slots[freeslot] = 1;
                                equipement[freeslot] = "masse";
                                masse.active = true;
                                masse.slot = freeslot;
                                masse.enabled = true;
                                PhotonNetwork.Destroy(_gameObject);
                                Debug.Log("equiped the mass");
                                freeslot++;
                            }
                            break;
                        case "laserbeam":
                            if (!_laserBeam.active)
                            {
                                dataHandler.GetComponent<variablesStock>().slots[freeslot] = 2;
                                equipement[freeslot] = "laserbeam";
                                _laserBeam.active = true;
                                _laserBeam.enabled = true;
                                _laserBeam.slot = freeslot;
                                freeslot++;
                                PhotonNetwork.Destroy(_gameObject);
                                Debug.Log("equiped the laserbeam");
                            }

                            break;
                        case "chargedbeam":
                            if (!_chargedBeam.active)
                            {
                                dataHandler.GetComponent<variablesStock>().slots[freeslot] = 3;
                                equipement[freeslot] = "chargedbeam";
                                _chargedBeam.active = true;
                                _chargedBeam.slot = freeslot;
                                _chargedBeam.enabled = true;
                                freeslot++;
                                PhotonNetwork.Destroy(_gameObject);
                                Debug.Log("equiped the chargedbeam");
                            }

                            break;
                        case "poisondart":
                            if (_poisonDart.active)
                            {
                                dataHandler.GetComponent<variablesStock>().slots[freeslot] = 4;
                                equipement[freeslot] = "poisondart";
                                _poisonDart.active = true;
                                _poisonDart.slot = freeslot;
                                _poisonDart.enabled = true;
                                PhotonNetwork.Destroy(_gameObject);
                                freeslot++;
                            }
                            break;
                        case "aoeheal":
                            if (AoeHeal.active)
                            {
                                dataHandler.GetComponent<variablesStock>().slots[freeslot] = 5;
                                equipement[freeslot] = "aoeheal";
                                AoeHeal.active = true;
                                AoeHeal.slot = freeslot;
                                AoeHeal.enabled = true;
                                PhotonNetwork.Destroy(_gameObject);
                                freeslot++;
                            }
                            break;
                        case "aoeattack":
                            if (AoeDmg.active)
                            {
                                dataHandler.GetComponent<variablesStock>().slots[freeslot] = 6;
                                equipement[freeslot] = "aoeattack";
                                AoeDmg.active = true;
                                AoeDmg.slot = freeslot;
                                AoeDmg.enabled = true;
                                PhotonNetwork.Destroy(_gameObject);
                                freeslot++;
                            }
                            break;
                        case "moreshoot":
                            if (MoreShoot.active)
                            {
                                dataHandler.GetComponent<variablesStock>().slots[freeslot] = 7;
                                equipement[freeslot] = "moreshoot";
                                MoreShoot.active = true;
                                MoreShoot.slot = freeslot;
                                MoreShoot.enabled = true;
                                PhotonNetwork.Destroy(_gameObject);
                                freeslot++;
                            }
                            break;
                        case "mine":
                            if (Mine.active)
                            {
                                dataHandler.GetComponent<variablesStock>().slots[freeslot] = 8;
                                equipement[freeslot] = "mine";
                                Mine.active = true;
                                Mine.slot = freeslot;
                                Mine.enabled = true;
                                PhotonNetwork.Destroy(_gameObject);
                                freeslot++;
                            }
                            break;
                    }
         GameObject.Find("varHolder").GetComponent<variablesStock>().UpdateIcons(freeslot);
    }
}
