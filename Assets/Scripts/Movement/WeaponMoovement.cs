using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class WeaponMoovement : MonoBehaviour
{
    private PhotonView PV;
    
    [SerializeField] int memoire ;  // sert a savoir quel est la dernière direction du personnage

    private Transform transformparent;

    private void Start()
    {
        transformparent = transform.parent;
        PV = GetComponentInParent<PhotonView>();
    }

    // Update is called once per frame
    void Update()
    { 
        if (PV.IsMine)
            weaponposition(ref memoire);
    }

    private void weaponposition(ref int mem)
    {
        Camera myCam = Camera.main;
        Vector3 delta = Input.mousePosition - myCam.WorldToScreenPoint(transformparent.position);
        if ((delta.x >0.25 ||delta.x <-0.25 ||delta.y > 0.25 || delta.y < -0.25)) // si le cursor est sur le sprite on ne fait rien
        {
            float angle = Mathf.Atan2(delta.y,delta.x)*Mathf.Rad2Deg; // on varie la position des sprites en fonction de l'angle l'angle varie de -180° a 180°
          
            if (angle >= -135 && angle < -45 )  // on verifie si c'est la position vers le bas du sprite
            {
                switch (mem)
                {
                    case 0 :
                        mem = -90;
                        transform.Translate(-0.25f,-0.25f,0);
                        break;
                    case 90 :
                        mem = -90;
                        transform.Translate(0,-0.5f,0);
                        break;
                    case 180:
                        mem = -90;
                        transform.Translate(0.25f,-0.25f,0);
                        break;
                }
            }
            else if (angle < 45 && angle >= -45 )  // on verifie si c'est la position vers la droite du sprite
            {
                switch (mem)
                {
                    case 180 :
                        mem = 0;
                        transform.Translate(0.5f,0,0);
                        break;
                    case -90 :
                        mem = 0;
                        transform.Translate(0.25f,0.25f,0);
                        break;
                    case 90:
                        mem = 0;
                        transform.Translate(0.25f,-0.25f,0);
                        break;
                }
            }
            else if (angle < 135 && angle >=45 )  // on verifie si c'est la position vers le haut du sprite
            {
                switch (mem)
                {
                    case 0 :
                        mem = 90;
                        transform.Translate(-0.25f,0.25f,0);
                        break;
                    case -90 :
                        mem = 90;
                        transform.Translate(0,0.5f,0);
                        break;
                    case 180:
                        mem = 90;
                        transform.Translate(0.25f,0.25f,0);
                        break;
                }
            }
            else   // on verifie si c'est la position vers la gauche du sprite
            {
                switch (mem)
                {
                    case 90 :
                        mem = 180;
                        transform.Translate(-0.25f,-0.25f,0);
                        break;
                    case 0 :
                        mem = 180;
                        transform.Translate(-0.5f,0,0);
                        break;
                    case -90:
                        mem = 180;
                        transform.Translate(-0.25f,0.25f,0);
                        break;
                }
            }
        }
    }
}
