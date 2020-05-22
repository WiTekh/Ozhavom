using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class MoreShootRotation : MonoBehaviour
{
    private PhotonView PV;

    [SerializeField] private Camera myCam;
    // Start is called before the first frame update
    void Start()
    {
        PV = transform.parent.GetComponent<PhotonView>();
    }

    // Update is called once per frame
    void Update()
    {
        if (PV.IsMine)
        {
            Vector3 dir = Input.mousePosition - myCam.WorldToScreenPoint(transform.position);
            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }
       
    }
    
}


