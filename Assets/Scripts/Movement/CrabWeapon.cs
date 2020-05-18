using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
public class CrabWeapon : MonoBehaviour
{
    [SerializeField] private float angle;
    [SerializeField]private PhotonView PV;
    private int memoire;
    private Transform transformparent;
    [SerializeField] Transform transformplayer;
    private SpriteRenderer _spriteRenderer;
    [SerializeField] public Sprite[] liste;

    
    private void Start() // Start function is called before the first frame
    {
        transformparent = transform.parent;
        memoire = 0;
        _spriteRenderer = GetComponent<SpriteRenderer>();
        PV = transform.GetComponent<PhotonView>();
    }

    void FixedUpdate() // called once per frame
    {
        Rotate();
    }
    
    private void Rotate()
    {
        //Will have to change that bc all players have different cameras
        Camera myCam = transform.parent.parent.GetChild(0).gameObject.GetComponent<Camera>();
        if (PV.IsMine)
        {
            Vector3 dir = Input.mousePosition - myCam.WorldToScreenPoint(transform.position);
            angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
            dir = Input.mousePosition - myCam.WorldToScreenPoint(transformplayer.position);
            angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
                    
                    if (memoire != 0 && angle > -80 && angle <= 0) 
                    {
                        if (memoire == 1) 
                            transformparent.Translate(0.65f,0,0);
                        if (memoire == 3) 
                            transformparent.Translate(0.7f,0.05f,0);
                        _spriteRenderer.sprite = liste[0];
                        memoire = 0;
                    }
            
                    if (memoire != 1 && angle > -180 && angle <= -100)
                    {
                        if (memoire == 0 || memoire == 2) 
                            transformparent.Translate(-0.65f,0,0);
                        if (memoire == 3) 
                            transformparent.Translate(0.05f,0.05f,0);
                        _spriteRenderer.sprite = liste[1];
                        memoire = 1;
                    }
            
                    if (memoire != 2 && angle > 0 && angle <= 90)
                    {
                        if (memoire == 1) 
                            transformparent.Translate(0.65f,0,0);
                        if (memoire == 3) 
                            transformparent.Translate(0.7f,0.05f,0);
                        _spriteRenderer.sprite = liste[2];
                        memoire = 2;
                    }
            
                    if (memoire != 3 && angle > 90 && angle <= 180) 
                    {
                        if (memoire == 0 || memoire == 2) 
                            transformparent.Translate(-0.7f,-0.05f,0);
                        if (memoire == 1) 
                            transformparent.Translate(-0.05f,-0.05f,0);
                        _spriteRenderer.sprite = liste[3];
                        memoire = 3;
                    }
        }

        /*
         0.3,0.1
         -0.35,0.1
         0.3,0.1
         -0.4,0.05
         */
    }
    
}
