using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
public class GolemWeapon : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private float angle;
    [SerializeField]private PhotonView PV;
    public int memoire;
    private Transform transformparent;
    [SerializeField] Transform transformplayer;
    private SpriteRenderer _spriteRenderer;

    
    private void Start() // Start function is called before the first frame
    {
        transformparent = transform.parent;
        memoire = 1;
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
                    
            if (memoire != 0 && angle > 0 &&  angle <= 180) 
            {
                transformparent.Translate(1.4f,0,0);
                memoire = 0;
            }
            
            if (memoire != 1 && !(angle > 0 &&  angle <= 180))
            {
                transformparent.Translate(-1.4f,0,0);
                memoire = 1;
            }
        }

        /*
         -0.1,-0.8
         -0.1,0.6
         */
    }
}
