using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

public class ItemInfo : MonoBehaviour
{
     [SerializeField] public string weaponname;

     private void Start()
     {
          Random rng = new Random();
          weaponname = WichItem(rng.Next(4));
     }
     private string WichItem(int rng)
     {
          switch (rng)
          {
               case 0:
                    return "rafale";
               case 1:
                    return "masse";
               case 2:
                    return "tripleshots";
               case 3:
                    return "laserbeam";
               default:
                    return "chargedbeam";
          }
     }
}
