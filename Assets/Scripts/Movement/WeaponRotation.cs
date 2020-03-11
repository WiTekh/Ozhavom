using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class WeaponRotation : MonoBehaviour
{
    [SerializeField]private PhotonView PV;
    [SerializeField] int memoire;
    private Transform transformparent;
    [SerializeField] Transform transformplayer;

    private void Start() // Start function is called before the first frame
    {
        transformparent = transform.parent;
        memoire = 0;
    }

    void FixedUpdate() // called once per frame
    {
        if (PV.IsMine)
            Rotate();
    }
    private void Rotate()
    {
        //Will have to change that bc all players have different cameras
        Camera myCam = transform.parent.parent.parent.GetChild(0).gameObject.GetComponent<Camera>();
        
        Vector3 dir = Input.mousePosition - myCam.WorldToScreenPoint(transform.position);
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        dir = Input.mousePosition - myCam.WorldToScreenPoint(transformplayer.position);
        angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        switch (memoire)
        {
            case 0:
                if (angle>45 && angle <135)
                {
                    transformparent.Translate(-0.25f,0.25f,0);
                    memoire = 90;
                }
                else if (angle>135 || angle < -135)
                {
                    transformparent.Translate(-0.5f,0,0);
                    memoire = 180;
                }
                else if(angle <-45 && angle > -135)
                {
                    transformparent.Translate(-0.25f,-0.25f,0);
                    memoire = -90;
                }
                break;
            case 90:
                if (angle<45 && angle >-45)
                {
                    transformparent.Translate(0.25f,-0.25f,0);
                    memoire = 0;
                }
                else if (angle>135 || angle < -135)
                {
                    transformparent.Translate(-0.25f,-0.25f,0);
                    memoire = 180;
                }
                else if(angle <-45 && angle > -135)
                {
                    transformparent.Translate(0,-0.5f,0);
                    memoire = -90;
                }
                break;
            case 180:
                if (angle>45 && angle <135)
                {
                    transformparent.Translate(0.25f,0.25f,0);
                    memoire = 90;
                }
                else if (angle>45 && angle < 45)
                {
                    transformparent.Translate(0.5f,0,0);
                    memoire = 0;
                }
                else if(angle <-45 && angle > -135)
                {
                    transformparent.Translate(0.25f,-0.25f,0);
                    memoire = -90;
                }
                break;
            case -90:
                if (angle<45 && angle >-45)
                {
                    transformparent.Translate(0.25f,0.25f,0);
                    memoire = 0;
                }
                else if (angle>135 || angle < -135)
                {
                    transformparent.Translate(-0.25f,0.25f,0);
                    memoire = 180;
                }
                else if(angle >45&& angle > 135)
                {
                    transformparent.Translate(0,0.5f,0);
                    memoire = 90;
                }
                break;
        }
    }
}
