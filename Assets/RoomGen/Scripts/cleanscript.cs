using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEditor;
using UnityEngine;

public class cleanscript : MonoBehaviourPunCallbacks, IPunObservable

{
    [SerializeField] private PhotonView PV;
     public bool top = true;
     public bool bot = true;
     public bool left = true;
     public bool right = true;

     private int tw =0;
     private int bw= 0;
     private int rw = 0;
     private int lw = 0;
    
    
    public bool boss;
    public bool spawn;
    public bool forge;
    public bool shop;
    public bool instructor;
    public bool cook;
    public bool item;

    private void Update()
    {
        if (!PV.IsMine)
        {
            if (tw == 0)
            {
                transform.GetChild(2).GetChild(0).gameObject.SetActive(true);

                if (top)
                {
                    transform.GetChild(3).GetChild(0).gameObject.SetActive(true);
                }
                else
                {
                    transform.GetChild(4).GetChild(0).gameObject.SetActive(true);
                }
            }
            else
            {
                transform.GetChild(2).GetChild(1).gameObject.SetActive(true);

                if (top)
                {
                    transform.GetChild(3).GetChild(1).gameObject.SetActive(true);

                }
                else
                {
                    transform.GetChild(4).GetChild(0).gameObject.SetActive(true);
                }
            }

            if (bw == 0)
            {
                transform.GetChild(2).GetChild(2).gameObject.SetActive(true);

                if (bot)
                {
                    transform.GetChild(3).GetChild(2).gameObject.SetActive(true);

                }
                else
                {
                    transform.GetChild(4).GetChild(1).gameObject.SetActive(true);
                }
            }
            else
            {
                transform.GetChild(2).GetChild(3).gameObject.SetActive(true);

                if (bot)
                {
                    transform.GetChild(3).GetChild(3).gameObject.SetActive(true);

                }
                else
                {
                    transform.GetChild(4).GetChild(2).gameObject.SetActive(true);
                }
            }

            if (lw == 0)
            {
                transform.GetChild(2).GetChild(4).gameObject.SetActive(true);

                if (left)
                {
                    transform.GetChild(3).GetChild(4).gameObject.SetActive(true);

                }
                else
                {
                    transform.GetChild(4).GetChild(3).gameObject.SetActive(true);
                }
            }
            else
            {
                transform.GetChild(2).GetChild(5).gameObject.SetActive(true);

                if (left)
                {
                    transform.GetChild(3).GetChild(5).gameObject.SetActive(true);

                }
                else
                {
                    transform.GetChild(4).GetChild(4).gameObject.SetActive(true);
                }
            }

            if (rw == 0)
            {
                transform.GetChild(2).GetChild(6).gameObject.SetActive(true);

                if (right)
                {
                    transform.GetChild(3).GetChild(6).gameObject.SetActive(true);

                }
                else
                {
                    transform.GetChild(4).GetChild(5).gameObject.SetActive(true);
                }
            }
            else
            {
                transform.GetChild(2).GetChild(7).gameObject.SetActive(true);

                if (right)
                {
                    transform.GetChild(3).GetChild(7).gameObject.SetActive(true);

                }
                else
                {
                    transform.GetChild(4).GetChild(6).gameObject.SetActive(true);
                }
            }

            if (forge)
            {
                transform.GetChild(5).GetChild(1).gameObject.SetActive(true);
            }

            if (shop)
            {
                transform.GetChild(5).GetChild(2).gameObject.SetActive(true);
            }

            if (cook)
            {
                transform.GetChild(5).GetChild(0).gameObject.SetActive(true);
            }

            if (instructor)
            {
                transform.GetChild(5).GetChild(3).gameObject.SetActive(true);
            }
        }
        
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(top);
            stream.SendNext(bot);
            stream.SendNext(left);
            stream.SendNext(right);
            stream.SendNext(tw);
            stream.SendNext(bw);
            stream.SendNext(lw);
            stream.SendNext(rw);
            stream.SendNext(cook);
            stream.SendNext(instructor);
            stream.SendNext(shop);
            stream.SendNext(forge);
            
        }
        else if (stream.IsReading)
        {
            top = (bool) stream.ReceiveNext();
            bot = (bool) stream.ReceiveNext();
            left = (bool) stream.ReceiveNext();
            right = (bool) stream.ReceiveNext();
            tw = (int) stream.ReceiveNext();
            bw = (int) stream.ReceiveNext();
            lw = (int) stream.ReceiveNext();
            rw = (int) stream.ReceiveNext();
            cook = (bool) stream.ReceiveNext();
            instructor = (bool) stream.ReceiveNext();
            shop = (bool) stream.ReceiveNext();
            forge = (bool) stream.ReceiveNext();

        }
    }
}
