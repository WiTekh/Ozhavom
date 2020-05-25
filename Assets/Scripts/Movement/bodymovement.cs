using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
public class bodymovement : MonoBehaviour
{
    private PhotonView PV;
    [SerializeField] private float angle;
    private SpriteRenderer _spriteRenderer;
    [SerializeField] public Sprite[] liste;
    private Transform transformparent;
 
    // Start is called before the first frame update
    void Start()
    {
        PV = transform.parent.GetComponent<PhotonView>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        transformparent = transform.parent;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (PV.IsMine)
        {
            Camera myCam = transform.parent.GetChild(0).gameObject.GetComponent<Camera>();
            Vector3 delta = Input.mousePosition - myCam.WorldToScreenPoint(transform.position);
            angle = Mathf.Atan2(delta.y, delta.x) * Mathf.Rad2Deg;
            
            
            //face droit
            if (angle > -90 && angle <=0 )
            {
                _spriteRenderer.sprite = liste[0];
            }
            //face gauche
            if (angle > -180 && angle <= -90)
            {
                _spriteRenderer.sprite = liste[1];
            }
            //dos droit
            if (angle > 0 && angle <= 90)
            {
                _spriteRenderer.sprite = liste[2];
            }
            //dos gauche
            if (angle > 90 && angle <=180)
            {
                _spriteRenderer.sprite = liste[3];
            }
        }
    }
}
