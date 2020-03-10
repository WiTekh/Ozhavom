using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Joueur : MonoBehaviour
{
   public float hpMax;
   public float atk;
   public float velocity;
   public float atkSpeed;
   public float[] abilities;

   public Joueur(float hpMax, float atk, float velocity, float atkSpeed, float[] abilities)
   {
      if (this.abilities.Length != abilities.Length)
         throw new Exception("Number of abilities invalid");
      
      this.abilities = abilities;
      this.hpMax = hpMax;
      this.atk = atk;
      this.velocity = velocity;
      this.atkSpeed = atkSpeed;
   }
}
