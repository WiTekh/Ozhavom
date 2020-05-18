using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
public class goblinweapon : MonoBehaviour
{
    [SerializeField] private float angle;
    [SerializeField] private PhotonView PV;
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
                    
            if (memoire != 0 && angle > -90 &&  angle <= 90) 
            {
                transformparent.Translate(0.4f,0,0);
                _spriteRenderer.sprite = liste[0];
                
                memoire = 0;
            }
            
            if (memoire != 1 && !(angle > -90 &&  angle <= 90))
            {
                transformparent.Translate(-0.4f,0,0);
                _spriteRenderer.sprite = liste[1];
                memoire = 1;
            }
        }
    }
}
