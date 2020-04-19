using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PlayerNetworkDeactivate : MonoBehaviour
{


    [SerializeField] private GameObject playerCamera;
    [SerializeField] private MonoBehaviour[] scriptsToIgnore;
    [SerializeField] private GameObject[] objectsToIgnore;
    
    private PhotonView photonView;

    // Use this for initialization
    void Start()
    {
        scriptsToIgnore[1] = transform.GetChild(1).GetChild(5).GetChild(0).GetComponent<WeaponRotation>();
        scriptsToIgnore[2] = transform.GetChild(1).GetChild(5).GetChild(0).GetChild(0).GetComponent<WeaponShoot>();
        scriptsToIgnore[3] = gameObject.GetComponent<playerStats>();

        photonView = GetComponent<PhotonView>();
        Initialize();
    }
    void Initialize()
    {
        if (photonView.IsMine)
        {

        }
        else
        {
            if (playerCamera != null)
                playerCamera.SetActive(false);
            
            foreach (MonoBehaviour item in scriptsToIgnore)
            {
                item.enabled = false;
            }

            foreach (GameObject item in objectsToIgnore)
            {
                
                item.SetActive(false);
            }
        }
    }

    
      
    
}