using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
public class crabBodyMvt : MonoBehaviour
{
    private PhotonView PV;
    [SerializeField] private float angle;
    [SerializeField] public Sprite[] liste;
    private Transform parent;

    [SerializeField] private GameObject Head;
    [SerializeField] private GameObject Body;
    
    // Start is called before the first frame update
    void Start()
    {
        PV = transform.parent.GetComponent<PhotonView>();
        parent = transform.parent;

        Head = transform.GetChild(0).gameObject;
        Body = transform.GetChild(1).gameObject;
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
                for (int i = 0; i < Head.transform.childCount; i++)
                {
                    Head.transform.GetChild(i).gameObject.SetActive(false);
                }

                for (int i = 0; i < Body.transform.childCount; i++)
                {
                    Body.transform.GetChild(i).gameObject.SetActive(false);
                }
                
                Head.transform.GetChild(0).gameObject.SetActive(true);
                Body.transform.GetChild(0).gameObject.SetActive(true);
            }
            //face gauche
            if (angle > -180 && angle <= -90)
            {
                for (int i = 0; i < Head.transform.childCount; i++)
                {
                    Head.transform.GetChild(i).gameObject.SetActive(false);
                }

                for (int i = 0; i < Body.transform.childCount; i++)
                {
                    Body.transform.GetChild(i).gameObject.SetActive(false);
                }

                Head.transform.GetChild(1).gameObject.SetActive(true);
                Body.transform.GetChild(1).gameObject.SetActive(true);                
            }
            //dos droit
            if (angle > 0 && angle <= 90)
            {
                for (int i = 0; i < Head.transform.childCount; i++)
                {
                    Head.transform.GetChild(i).gameObject.SetActive(false);
                }

                for (int i = 0; i < Body.transform.childCount; i++)
                {
                    Body.transform.GetChild(i).gameObject.SetActive(false);
                }
                
                Body.transform.GetChild(2).gameObject.SetActive(true);
            }

            //dos gauche
            if (angle > 90 && angle <= 180)
            {
                for (int i = 0; i < Head.transform.childCount; i++)
                {
                    Head.transform.GetChild(i).gameObject.SetActive(false);
                }

                for (int i = 0; i < Body.transform.childCount; i++)
                {
                    Body.transform.GetChild(i).gameObject.SetActive(false);
                }
                
                Body.transform.GetChild(3).gameObject.SetActive(true);
            }
        }
    }
}