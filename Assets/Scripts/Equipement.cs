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

    private int drop = 0;
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
    [SerializeField] private Shield Shield;
    [SerializeField] private Seisme Seisme;
    [SerializeField] private InstantHeal InstantHeal;
    [SerializeField] private Piercingshot Piercingshot;
    private AvatarSetup AvatarSetups;
    private PhotonView PV;
    private int coin;

    public variablesStock dataHandler;

    public GameObject[] drops;

    // Start is called before the first frame update
    void Start()
    {
        AvatarSetups = GetComponent<AvatarSetup>();
        freeslot = 0;
        PV = GetComponent<PhotonView>();
        dataHandler = GameObject.Find("varHolder").GetComponent<variablesStock>();
        Debug.Log(dataHandler.name);
        equipement[0] = "";
        equipement[1] = "";
        equipement[2] = "";
    }


    private void Update()
    {
        if (dataHandler.canGive)
        {
            Give(dataHandler.weapon);
            dataHandler.canGive = false;
        }

        if (Input.GetKeyDown(KeyCode.T))
        {
            variablesStock stock =  dataHandler;
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
        
        // c'est pour le drop
        if (Input.GetKeyDown(KeyCode.G))
        {
            drop = 1;
        }

        if (drop >0)
        {
            drop++;
            Drop();
            
        }

        if (drop == 11)
        {
            drop = 0;
        }
    }

    private void OnCollisionEnter2D (Collision2D col)
    {
        if (PV.IsMine)
        {
            if (freeslot == 3 || equipement[freeslot] != "")
            {
                for (int i = 2; i >= 0; i--)
                {
                    if (equipement[i] == "")
                    {
                        freeslot = i;
                    }
                }
            }

            if (equipement[freeslot] == "")
            {
                _gameObject = col.gameObject;
                if (_gameObject.CompareTag("Weapons") && !_gameObject.GetComponent<ItemInfo>().drop)
                {

                    if (freeslot <= 2)
                    {
                        equipitems();
                        freeslot++;
                    }
                }
                else if (_gameObject.CompareTag("itemshop") && !_gameObject.GetComponent<ItemInfo>().drop)
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
                }
            }


            if (_gameObject.CompareTag("itemshop"))
                {
                        if (Stats.currentH < AvatarSetups.maxH && _gameObject.GetComponent<ShopItems>().prix <= coin &&!_gameObject.GetComponent<ShopItems>().isweapon )
                        {
                            if (AvatarSetups.maxH >= Stats.currentH + _gameObject.GetComponent<ShopItems>().heal)
                            {
                                Stats.currentH = Stats.currentH + _gameObject.GetComponent<ShopItems>().heal;
                            }
                            else
                            {
                                Stats.currentH = AvatarSetups.maxH;
                            }

                            PhotonNetwork.Destroy(_gameObject);
                            Stats.coinAmount -= _gameObject.GetComponent<ShopItems>().prix;
                        }
                    }
                }
            
        
    }

    private void Give(int wp)
    {
        int p = 0;
        for (int i = 0; i < dataHandler.slots.Length; i++)
        {
            if (dataHandler.slots[i] != -1)
            {
                p++;
            }
        }

        if (freeslot < 3)
        {
            switch (wp)
            {
                case 0:
                    dataHandler.GetComponent<variablesStock>().slots[freeslot] = 0;
                    equipement[freeslot] = "rafale";
                    _rafale.active = true;
                    _rafale.slot = freeslot;
                    _rafale.enabled = true;
                    PhotonNetwork.Destroy(_gameObject);
                    Debug.Log("Xx_H4CK3RM4N_xX : Gived U the classic pew pew");
                    freeslot++;

                    dataHandler.slots[p] = 0;
                    break;
                case 1:
                    dataHandler.GetComponent<variablesStock>().slots[freeslot] = 1;
                    equipement[freeslot] = "mass";
                    masse.active = true;
                    masse.slot = freeslot;
                    masse.enabled = true;
                    PhotonNetwork.Destroy(_gameObject);
                    Debug.Log("Xx_H4CK3RM4N_xX : Gived U the big bawl");
                    freeslot++;
                    dataHandler.slots[p] = 1;
                    break;
                case 2:
                    equipement[freeslot] = "aoeattack";
                    AoeDmg.active = true;
                    AoeDmg.slot = freeslot;
                    AoeDmg.enabled = true;
                    freeslot++;
                    PhotonNetwork.Destroy(_gameObject);
                    dataHandler.GetComponent<variablesStock>().slots[freeslot] = 6;
                    Debug.Log("Xx_H4CK3RM4N_xX : Gived U the weird thorny thing");
                    dataHandler.slots[p] = 7;
                    break;
                case 3:
                    dataHandler.GetComponent<variablesStock>().slots[freeslot] = 2;
                    equipement[freeslot] = "laserbeam";
                    _laserBeam.active = true;
                    _laserBeam.enabled = true;
                    _laserBeam.slot = freeslot;
                    PhotonNetwork.Destroy(_gameObject);
                    Debug.Log("Xx_H4CK3RM4N_xX : Gived U the big ray stuff");
                    freeslot++;
                    dataHandler.slots[p] = 2;
                    break;
                case 4:
                    equipement[freeslot] = "shield";
                    Shield.active = true;
                    Shield.slot = freeslot;
                    Shield.enabled = true;
                    freeslot++;
                    PhotonNetwork.Destroy(_gameObject);
                    Debug.Log("Xx_H4CK3RM4N_xX : he protec");
                    dataHandler.GetComponent<variablesStock>().slots[freeslot] = 11;
                    dataHandler.slots[p] = 6;
                    break;
                case 5:
                    equipement[freeslot] = "aoeheal";
                    AoeHeal.active = true;
                    AoeHeal.slot = freeslot;
                    AoeHeal.enabled = true;
                    freeslot++;
                    Debug.Log("Xx_H4CK3RM4N_xX : I got yo ass man");
                    PhotonNetwork.Destroy(_gameObject);
                    dataHandler.GetComponent<variablesStock>().slots[freeslot] = 5;
                    dataHandler.slots[p] = 5;
                    break;
                case 6:
                    equipement[freeslot] = "poisondart";
                    _poisonDart.active = true;
                    _poisonDart.slot = freeslot;
                    _poisonDart.enabled = true;
                    freeslot++;
                    Debug.Log("Xx_H4CK3RM4N_xX : Eh! ne confonds pas avec le poisson ^^''");
                    PhotonNetwork.Destroy(_gameObject);
                    dataHandler.GetComponent<variablesStock>().slots[freeslot] = 4;
                    dataHandler.slots[p] = 4;
                    break;
                case 7:
                    equipement[freeslot] = "mine";
                    Mine.active = true;
                    Mine.slot = freeslot;
                    Mine.enabled = true;
                    freeslot++;
                    Debug.Log("Xx_H4CK3RM4N_xX : Yo got ya boom boom");
                    PhotonNetwork.Destroy(_gameObject);
                    dataHandler.GetComponent<variablesStock>().slots[freeslot] = 8;
                    dataHandler.slots[p] = 8;
                    break;
                case 8:
                    equipement[freeslot] = "seisme";
                    Seisme.active = true;
                    Seisme.slot = freeslot;
                    Seisme.enabled = true;
                    freeslot++;
                    dataHandler.slots[p] = 3;
                    PhotonNetwork.Destroy(_gameObject);
                    dataHandler.GetComponent<variablesStock>().slots[freeslot] = 10;
                    Debug.Log("Xx_H4CK3RM4N_xX : You're such a CRACKhead (eheh .. crack ... seism ... ^^')");
                    break;
            }
        }
        
        dataHandler.UpdateIcons(p);
    }
    private void equipitems()
    {
        name = _gameObject.GetComponent<ItemInfo>().weaponname;
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
                Debug.Log("yeet");
                equipement[freeslot] = "moreshoot";
                MoreShoot.active = true;
                MoreShoot.slot = freeslot;
                MoreShoot.enabled = true;
                freeslot++;

                PhotonNetwork.Destroy(_gameObject);
                dataHandler.GetComponent<variablesStock>().slots[freeslot] = 7;

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
                dataHandler.GetComponent<variablesStock>().slots[freeslot] = 9;

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
                dataHandler.GetComponent<variablesStock>().slots[freeslot] = 10;

            }
        }
        else if ("shield" == name)
        {
            if (!Shield.active)
            {
                Debug.Log("yeet");
                equipement[freeslot] = "shield";
                Shield.active = true;
                Shield.slot = freeslot;
                Shield.enabled = true;
                freeslot++;

                PhotonNetwork.Destroy(_gameObject);
                dataHandler.GetComponent<variablesStock>().slots[freeslot] = 11;

            }
        }
        else if ("pierce" == name)
        {
            if (!Piercingshot.active)
            {
                Debug.Log("yeet");
                equipement[freeslot] = "shield";
                Piercingshot.active = true;
                Piercingshot.slot = freeslot;
                Piercingshot.enabled = true;
                freeslot++;

                PhotonNetwork.Destroy(_gameObject);
                dataHandler.GetComponent<variablesStock>().slots[freeslot] = 12;

            }
        }

        if (freeslot >0)
        {
            dataHandler.UpdateIcons(freeslot-1);
        }
        
    }

    void Drop()
    {
        if (Input.GetKeyDown(KeyCode.Z)&&equipement[0]!="")
        {
            
             name = equipement[0];
            if (name == "rafale")
            {
                _rafale.enabled = false;
                _rafale.active = false;
                equipement[0] = "";
                GameObject yes = PhotonNetwork.InstantiateSceneObject(Path.Combine("PhotonPrefabs", "Item"), transform.position, transform.rotation);
                yes.GetComponent<ItemInfo>().drop = true;
                yes.GetComponent<ItemInfo>().weaponname = name;
            }
            else if (name == "mine")
            {
                Mine.enabled = false;
                Mine.active = false;
                equipement[0] = "";
                GameObject yes = PhotonNetwork.InstantiateSceneObject(Path.Combine("PhotonPrefabs", "Item"), transform.position, transform.rotation);
                yes.GetComponent<ItemInfo>().drop = true;
                yes.GetComponent<ItemInfo>().weaponname = name;
            }
            else if (name == "masse")
            {
                masse.enabled = false;
                masse.active = false;
                equipement[0] = "";
                GameObject yes = PhotonNetwork.InstantiateSceneObject(Path.Combine("PhotonPrefabs", "Item"), transform.position, transform.rotation);
                yes.GetComponent<ItemInfo>().drop = true;
                yes.GetComponent<ItemInfo>().weaponname = name;
            }
            else if (name == "laserbeam")
            {
                _laserBeam.enabled = false;
                _laserBeam.active = false;
                equipement[0] = "";
                GameObject yes = PhotonNetwork.InstantiateSceneObject(Path.Combine("PhotonPrefabs", "Item"), transform.position, transform.rotation);
                yes.GetComponent<ItemInfo>().drop = true;
                yes.GetComponent<ItemInfo>().weaponname = name;
            }
            else if ( "poisondart" == name)
            {
                _poisonDart.enabled = false;
                _poisonDart.active = false;
                equipement[0] = "";
                GameObject yes = PhotonNetwork.InstantiateSceneObject(Path.Combine("PhotonPrefabs", "Item"), transform.position, transform.rotation);
                yes.GetComponent<ItemInfo>().drop = true;
                yes.GetComponent<ItemInfo>().weaponname = name;
            }
            else if ("aoeheal" == name)
            {
                AoeHeal.enabled = false;
                AoeHeal.active = false;
                equipement[0] = "";
                GameObject yes = PhotonNetwork.InstantiateSceneObject(Path.Combine("PhotonPrefabs", "Item"), transform.position, transform.rotation);
                yes.GetComponent<ItemInfo>().drop = true;
                yes.GetComponent<ItemInfo>().weaponname = name;
            }
            else if ( "aoeattack" == name)
            {
                AoeDmg.enabled = false;
                AoeDmg.active = false;
               equipement[0] = "";
               GameObject yes = PhotonNetwork.InstantiateSceneObject(Path.Combine("PhotonPrefabs", "Item"), transform.position, transform.rotation);
               yes.GetComponent<ItemInfo>().drop = true;
               yes.GetComponent<ItemInfo>().weaponname = name;
            }
            else if ("moreshoot" == name)
            {
                MoreShoot.enabled = false;
                MoreShoot.active = false;
                equipement[0] = "";
                GameObject yes = PhotonNetwork.InstantiateSceneObject(Path.Combine("PhotonPrefabs", "Item"), transform.position, transform.rotation);
                yes.GetComponent<ItemInfo>().drop = true;
                yes.GetComponent<ItemInfo>().weaponname = name;
                
            }
            else if ("instantheal" == name)
            {
                InstantHeal.enabled = false;
                InstantHeal.active = false;
                equipement[0] = "";
                GameObject yes = PhotonNetwork.InstantiateSceneObject(Path.Combine("PhotonPrefabs", "Item"), transform.position, transform.rotation);
                yes.GetComponent<ItemInfo>().drop = true;
                yes.GetComponent<ItemInfo>().weaponname = name;
            }
            else if ("seisme" == name)
            {
                Seisme.enabled = false;
                Seisme.active = false;
                equipement[0] = "";
                GameObject yes = PhotonNetwork.InstantiateSceneObject(Path.Combine("PhotonPrefabs", "Item"), transform.position, transform.rotation);
                yes.GetComponent<ItemInfo>().drop = true;
                yes.GetComponent<ItemInfo>().weaponname = name;
            }
            else if ("shield" == name)
            {
               Shield.enabled = false;
               Shield.active = false;
               equipement[0] = "";
               GameObject yes = PhotonNetwork.InstantiateSceneObject(Path.Combine("PhotonPrefabs", "Item"), transform.position, transform.rotation);
               yes.GetComponent<ItemInfo>().drop = true;
               yes.GetComponent<ItemInfo>().weaponname = name;
            }
            else if ("pierce" == name)
            {
                Piercingshot.enabled = false;
                Piercingshot.active = false;
                equipement[0] = "";
                GameObject yes = PhotonNetwork.InstantiateSceneObject(Path.Combine("PhotonPrefabs", "Item"), transform.position, transform.rotation);
                yes.GetComponent<ItemInfo>().drop = true;
                yes.GetComponent<ItemInfo>().weaponname = name;
            }
            dataHandler.UpdateIcons(0);
        }
         if (Input.GetKeyDown(KeyCode.E)&&equipement[1]!="")
        {
             name = equipement[1];
            if (name == "rafale")
            {
                _rafale.enabled = false;
                _rafale.active = false;
                equipement[1] = "";
                GameObject yes = PhotonNetwork.InstantiateSceneObject(Path.Combine("PhotonPrefabs", "Item"), transform.position, transform.rotation);
                yes.GetComponent<ItemInfo>().drop = true;
                yes.GetComponent<ItemInfo>().weaponname = name;
            }
            else if (name == "mine")
            {
                Mine.enabled = false;
                Mine.active = false;
                equipement[1] = "";
                GameObject yes = PhotonNetwork.InstantiateSceneObject(Path.Combine("PhotonPrefabs", "Item"), transform.position, transform.rotation);
                yes.GetComponent<ItemInfo>().drop = true;
                yes.GetComponent<ItemInfo>().weaponname = name;
            }
            else if (name == "masse")
            {
                masse.enabled = false;
                masse.active = false;
                equipement[1] = "";
                GameObject yes = PhotonNetwork.InstantiateSceneObject(Path.Combine("PhotonPrefabs", "Item"), transform.position, transform.rotation);
                yes.GetComponent<ItemInfo>().drop = true;
                yes.GetComponent<ItemInfo>().weaponname = name;
            }
            else if (name == "laserbeam")
            {
                _laserBeam.enabled = false;
                _laserBeam.active = false;
                equipement[1] = "";
                GameObject yes = PhotonNetwork.InstantiateSceneObject(Path.Combine("PhotonPrefabs", "Item"), transform.position, transform.rotation);
                yes.GetComponent<ItemInfo>().drop = true;
                yes.GetComponent<ItemInfo>().weaponname = name;
            }
            else if ( "poisondart" == name)
            {
                _poisonDart.enabled = false;
                _poisonDart.active = false;
                equipement[1] = "";
                GameObject yes = PhotonNetwork.InstantiateSceneObject(Path.Combine("PhotonPrefabs", "Item"), transform.position, transform.rotation);
                yes.GetComponent<ItemInfo>().drop = true;
                yes.GetComponent<ItemInfo>().weaponname = name;
            }
            else if ("aoeheal" == name)
            {
                AoeHeal.enabled = false;
                AoeHeal.active = false;
                equipement[1] = "";
                GameObject yes = PhotonNetwork.InstantiateSceneObject(Path.Combine("PhotonPrefabs", "Item"), transform.position, transform.rotation);
                yes.GetComponent<ItemInfo>().drop = true;
                yes.GetComponent<ItemInfo>().weaponname = name;
            }
            else if ( "aoeattack" == name)
            {
                AoeDmg.enabled = false;
                AoeDmg.active = false;
               equipement[1] = "";
               GameObject yes = PhotonNetwork.InstantiateSceneObject(Path.Combine("PhotonPrefabs", "Item"), transform.position, transform.rotation);
               yes.GetComponent<ItemInfo>().drop = true;
               yes.GetComponent<ItemInfo>().weaponname = name;
            }
            else if ("moreshoot" == name)
            {
                MoreShoot.enabled = false;
                MoreShoot.active = false;
                equipement[1] = "";
                GameObject yes = PhotonNetwork.InstantiateSceneObject(Path.Combine("PhotonPrefabs", "Item"), transform.position, transform.rotation);
                yes.GetComponent<ItemInfo>().drop = true;
                yes.GetComponent<ItemInfo>().weaponname = name;
                
            }
            else if ("instantheal" == name)
            {
                InstantHeal.enabled = false;
                InstantHeal.active = false;
                equipement[1] = "";
                GameObject yes = PhotonNetwork.InstantiateSceneObject(Path.Combine("PhotonPrefabs", "Item"), transform.position, transform.rotation);
                yes.GetComponent<ItemInfo>().drop = true;
                yes.GetComponent<ItemInfo>().weaponname = name;
            }
            else if ("seisme" == name)
            {
                Seisme.enabled = false;
                Seisme.active = false;
                equipement[1] = "";
                GameObject yes = PhotonNetwork.InstantiateSceneObject(Path.Combine("PhotonPrefabs", "Item"), transform.position, transform.rotation);
                yes.GetComponent<ItemInfo>().drop = true;
                yes.GetComponent<ItemInfo>().weaponname = name;
            }
            else if ("shield" == name)
            {
               Shield.enabled = false;
               Shield.active = false;
               equipement[1] = "";
               GameObject yes = PhotonNetwork.InstantiateSceneObject(Path.Combine("PhotonPrefabs", "Item"), transform.position, transform.rotation);
               yes.GetComponent<ItemInfo>().drop = true;
               yes.GetComponent<ItemInfo>().weaponname = name;
            }
            else if ("pierce" == name)
            {
                Piercingshot.enabled = false;
                Piercingshot.active = false;
                equipement[1] = "";
                GameObject yes = PhotonNetwork.InstantiateSceneObject(Path.Combine("PhotonPrefabs", "Item"), transform.position, transform.rotation);
                yes.GetComponent<ItemInfo>().drop = true;
                yes.GetComponent<ItemInfo>().weaponname = name;
            }
            dataHandler.UpdateIcons(1);
        }
          if (Input.GetKeyDown(KeyCode.R)&&equipement[2]!="")
        {
             name = equipement[2];
            if (name == "rafale")
            {
                _rafale.enabled = false;
                _rafale.active = false;
                equipement[2] = "";
                GameObject yes = PhotonNetwork.InstantiateSceneObject(Path.Combine("PhotonPrefabs", "Item"), transform.position, transform.rotation);
                yes.GetComponent<ItemInfo>().drop = true;
                yes.GetComponent<ItemInfo>().weaponname = name;
            }
            else if (name == "mine")
            {
                Mine.enabled = false;
                Mine.active = false;
                equipement[2] = "";
                GameObject yes = PhotonNetwork.InstantiateSceneObject(Path.Combine("PhotonPrefabs", "Item"), transform.position, transform.rotation);
                yes.GetComponent<ItemInfo>().drop = true;
                yes.GetComponent<ItemInfo>().weaponname = name;
            }
            else if (name == "masse")
            {
                masse.enabled = false;
                masse.active = false;
                equipement[2] = "";
                GameObject yes = PhotonNetwork.InstantiateSceneObject(Path.Combine("PhotonPrefabs", "Item"), transform.position, transform.rotation);
                yes.GetComponent<ItemInfo>().drop = true;
                yes.GetComponent<ItemInfo>().weaponname = name;
            }
            else if (name == "laserbeam")
            {
                _laserBeam.enabled = false;
                _laserBeam.active = false;
                equipement[2] = "";
                GameObject yes = PhotonNetwork.InstantiateSceneObject(Path.Combine("PhotonPrefabs", "Item"), transform.position, transform.rotation);
                yes.GetComponent<ItemInfo>().drop = true;
                yes.GetComponent<ItemInfo>().weaponname = name;
            }
            else if ( "poisondart" == name)
            {
                _poisonDart.enabled = false;
                _poisonDart.active = false;
                equipement[2] = "";
                GameObject yes = PhotonNetwork.InstantiateSceneObject(Path.Combine("PhotonPrefabs", "Item"), transform.position, transform.rotation);
                yes.GetComponent<ItemInfo>().drop = true;
                yes.GetComponent<ItemInfo>().weaponname = name;
            }
            else if ("aoeheal" == name)
            {
                AoeHeal.enabled = false;
                AoeHeal.active = false;
                equipement[2] = "";
                GameObject yes = PhotonNetwork.InstantiateSceneObject(Path.Combine("PhotonPrefabs", "Item"), transform.position, transform.rotation);
                yes.GetComponent<ItemInfo>().drop = true;
                yes.GetComponent<ItemInfo>().weaponname = name;
            }
            else if ( "aoeattack" == name)
            {
                AoeDmg.enabled = false;
                AoeDmg.active = false;
               equipement[2] = "";
               GameObject yes = PhotonNetwork.InstantiateSceneObject(Path.Combine("PhotonPrefabs", "Item"), transform.position, transform.rotation);
               yes.GetComponent<ItemInfo>().drop = true;
               yes.GetComponent<ItemInfo>().weaponname = name;
            }
            else if ("moreshoot" == name)
            {
                MoreShoot.enabled = false;
                MoreShoot.active = false;
                equipement[2] = "";
                GameObject yes = PhotonNetwork.InstantiateSceneObject(Path.Combine("PhotonPrefabs", "Item"), transform.position, transform.rotation);
                yes.GetComponent<ItemInfo>().drop = true;
                yes.GetComponent<ItemInfo>().weaponname = name;
                
            }
            else if ("instantheal" == name)
            {
                InstantHeal.enabled = false;
                InstantHeal.active = false;
                equipement[2] = "";
                GameObject yes = PhotonNetwork.InstantiateSceneObject(Path.Combine("PhotonPrefabs", "Item"), transform.position, transform.rotation);
                yes.GetComponent<ItemInfo>().drop = true;
                yes.GetComponent<ItemInfo>().weaponname = name;
            }
            else if ("seisme" == name)
            {
                Seisme.enabled = false;
                Seisme.active = false;
                equipement[2] = "";
                GameObject yes = PhotonNetwork.InstantiateSceneObject(Path.Combine("PhotonPrefabs", "Item"), transform.position, transform.rotation);
                yes.GetComponent<ItemInfo>().drop = true;
                yes.GetComponent<ItemInfo>().weaponname = name;
            }
            else if ("shield" == name)
            {
               Shield.enabled = false;
               Shield.active = false;
               equipement[2] = "";
               GameObject yes = PhotonNetwork.InstantiateSceneObject(Path.Combine("PhotonPrefabs", "Item"), transform.position, transform.rotation);
               yes.GetComponent<ItemInfo>().drop = true;
               yes.GetComponent<ItemInfo>().weaponname = name;
            }
            else if ("pierce" == name)
            {
                Piercingshot.enabled = false;
                Piercingshot.active = false;
                equipement[2] = "";
                GameObject yes = PhotonNetwork.InstantiateSceneObject(Path.Combine("PhotonPrefabs", "Item"), transform.position, transform.rotation);
                yes.GetComponent<ItemInfo>().drop = true;
                yes.GetComponent<ItemInfo>().weaponname = name;
            }
            dataHandler.UpdateIcons(2);
        }
    }
}
