using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class gnollweapon : MonoBehaviour
{
    [SerializeField]private PhotonView PV;
    private int memoire;
    private Transform transformparent;
    [SerializeField] Transform transformplayer;
    private SpriteRenderer _spriteRenderer;

    
    private void Start() // Start function is called before the first frame
    {
        PV = transform.GetComponent<PhotonView>();
        transformparent = transform.parent;
        memoire = 0;
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void FixedUpdate() // called once per frame
    {
        Rotate();
    }
    
    private void Rotate()
    {
        if (PV.IsMine)
        {
            Camera myCam = transform.parent.parent.GetChild(0).gameObject.GetComponent<Camera>();
                    
                    Vector3 dir = Input.mousePosition - myCam.WorldToScreenPoint(transform.position);
                    float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
                    transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
                    dir = Input.mousePosition - myCam.WorldToScreenPoint(transformplayer.position);
                    angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
                  
                    
                    
                    if (memoire != 0 && !(angle > -180 && angle <= -90)) 
                    {
                        if (memoire == 1) 
                            _spriteRenderer.sortingOrder = -1;
                        memoire = 0;
                    }
            
                    if (memoire != 1 && angle > -180 && angle <= -90)
                    {
                        if (memoire == 0)
                            _spriteRenderer.sortingOrder = 1;
                        memoire = 1;
                    }
        }   
    }
}
