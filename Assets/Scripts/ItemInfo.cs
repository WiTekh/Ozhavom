using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;
using Random = System.Random;

public class ItemInfo : MonoBehaviourPunCallbacks,IPunObservable
{
     [SerializeField] public string weaponname;
     public bool drop = false;
     private PhotonView PV;
     public int setazero = 0;
     private void Start()
     {
          PV = GetComponent<PhotonView>();
          if (PV.IsMine)
          {
               if (!drop)
               {
                    Random rng = new Random();
                    weaponname = WichItem((rng.Next(11)+setazero)%12);
               }
              
              
          }
         
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
                    return "poisondart";
               case 3:
                    return "laserbeam";
               case 4:
                    return "aoeattack";
               case 5:
                    return "aoeheal";
               case 6:
                    return "moreshoot";
               case 7:
                    return "mine";
               case 8:
                    return "instantheal";
               case 9:
                    return "seisme";
               case 10:
                    return "shield";
               case 11:
                    return "pierce";
               default:
                    return "laserbeam";
          }
     }

     public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
     {
          if (stream.IsWriting)
          {
               stream.SendNext(weaponname);
          }
          else if(stream.IsReading);

          {
               weaponname = (string) stream.ReceiveNext();
          }
     }
}
