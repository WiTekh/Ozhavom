using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class amelioration : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private int arme;

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            string[] equipement = other.gameObject.GetComponent<Equipement>().equipement;
            if (equipement[arme] != "" && other.gameObject.GetComponent<playerStats>().coinAmount<=5)
            {
                other.gameObject.GetComponent<playerStats>().coinAmount -= 5;
                 if (name == "rafale" && other.gameObject.transform.GetChild(4).GetChild(0).GetChild(0).GetComponent<Rafale>().upgrade <4)
                 {
                     other.gameObject.transform.GetChild(4).GetChild(0).GetChild(0).GetComponent<Rafale>()
                         .upgrade++;

                 }
            else if (name == "mine"&& other.gameObject.transform.GetChild(4).GetChild(0).GetChild(0).GetComponent<Mine>().upgrade <4)
                 {
                     other.gameObject.transform.GetChild(4).GetChild(0).GetChild(0).GetComponent<Mine>()
                         .upgrade++;

                 }
           
            else if (name == "laserbeam"&& other.gameObject.transform.GetChild(4).GetChild(0).GetChild(0).GetComponent<LaserBeam>().upgrade <4)
                 {
                     other.gameObject.transform.GetChild(4).GetChild(0).GetChild(0).GetComponent<LaserBeam>()
                         .upgrade++;

                 }
            else if ( "poisondart" == name&& other.gameObject.transform.GetChild(4).GetChild(0).GetChild(0).GetComponent<PoisonDart>().upgrade <4)
                 {
                     other.gameObject.transform.GetChild(4).GetChild(0).GetChild(0).GetComponent<PoisonDart>()
                         .upgrade++;

                 }
            else if ("aoeheal" == name&& other.gameObject.transform.GetChild(4).GetChild(0).GetChild(0).GetComponent<HealAoe>().upgrade <4)
                 {
                     other.gameObject.transform.GetChild(4).GetChild(0).GetChild(0).GetComponent<HealAoe>()
                         .upgrade++;

                 }
            else if ( "aoeattack" == name&& other.gameObject.transform.GetChild(4).GetChild(0).GetChild(0).GetComponent<AttackAoe>().upgrade <4)
                 {
                     other.gameObject.transform.GetChild(4).GetChild(0).GetChild(0).GetComponent<AttackAoe>()
                         .upgrade++;

                 }
            else if ("moreshoot" == name && other.gameObject.transform.GetChild(4).GetChild(0).GetChild(0)
                         .GetComponent<MoreShoot>().upgrade < 4)
                 {
                     other.gameObject.transform.GetChild(4).GetChild(0).GetChild(0).GetComponent<MoreShoot>()
                         .upgrade++;

                 }
                 else if ("seisme" == name&& other.gameObject.transform.GetChild(4).GetChild(0).GetChild(0).GetComponent<Seisme>().upgrade <4)
                 {
                     other.gameObject.transform.GetChild(4).GetChild(0).GetChild(0).GetComponent<Seisme>()
                         .upgrade++;

                 }
            else if ("shield" == name&& other.gameObject.transform.GetChild(4).GetChild(0).GetChild(0).GetComponent<Shield>().upgrade <4)
                 {
                     other.gameObject.transform.GetChild(4).GetChild(0).GetChild(0).GetComponent<Shield>()
                         .upgrade++;

                 }
            else if ("pierce" == name&& other.gameObject.transform.GetChild(4).GetChild(0).GetChild(0).GetComponent<Piercingshot>().upgrade <4)
                 {
                     other.gameObject.transform.GetChild(4).GetChild(0).GetChild(0).GetComponent<Piercingshot>()
                         .upgrade++;

                 }
            }
        }
    }
}
