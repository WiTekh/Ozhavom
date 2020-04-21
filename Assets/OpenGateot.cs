using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenGateot : MonoBehaviour
{
   [SerializeField] private bool Isopen;
   
   [SerializeField]private SpriteRenderer sr;
   private void Start()
   {
      sr = transform.parent.GetComponent<SpriteRenderer>();
   }
   private void Update()
   {
      if (Isopen)
      {
         sr.enabled = false;
      }
   }
   private void OnCollisionEnter2D(Collision2D col)
   {
      if (Isopen && col.gameObject.CompareTag("Player"))
      {
         col.gameObject.transform.Translate(0,-5.5f,0);
      }
   }
}
