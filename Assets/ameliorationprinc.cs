using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ameliorationprinc : MonoBehaviour
{
  private void OnCollisionEnter2D(Collision2D other)
  {
    if (other.gameObject.CompareTag("Player"))
    {
      if (other.gameObject.GetComponent<playerStats>().coinAmount >=5 && other.gameObject.transform.GetChild(4).GetChild(0).GetChild(0).GetComponent<WeaponShoot>().upgrade <=3)
      {
        other.gameObject.transform.GetChild(4).GetChild(0).GetChild(0).GetComponent<WeaponShoot>().upgrade += 1;
        other.gameObject.GetComponent<playerStats>().coinAmount -= 5;
      }
    }
  }
}
