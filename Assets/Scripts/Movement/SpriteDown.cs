﻿using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class SpriteDown : MonoBehaviour
{
    private PhotonView PV;
    private bool active;
    private Renderer _sprite;
    [SerializeField] private float angle;

    // Start is called before the first frame update
    void Start()
    {
        PV = transform.parent.GetComponent<PhotonView>();
        active = false;
        _sprite = GetComponent<Renderer>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (PV.IsMine)
        {
            Camera myCam = transform.parent.GetChild(0).gameObject.GetComponent<Camera>();
            Vector3 delta = Input.mousePosition - myCam.WorldToScreenPoint(transform.position);
            
            //face droit
                 angle = Mathf.Atan2(delta.y, delta.x) * Mathf.Rad2Deg;
                if (!active && angle > -90 && angle <=0 )
                {
                    _sprite.enabled = true;
                    active = true;
                }
                if (active && angle < -90 || angle >= 0)
                {
                    _sprite.enabled = false;
                    active = false;
                }
            
        }
    }
}
