using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class SpriteUp : MonoBehaviour
{
    private PhotonView PV;
    private bool active;
    [SerializeField] Renderer weapon;
    private Renderer _sprite;
    [SerializeField] private float angle;

    // Start is called before the first frame update
    void Start()
    {
        PV = GetComponentInParent<PhotonView>();
        active = false;
        _sprite = GetComponent<Renderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (PV.IsMine)
        {
            Camera myCam = Camera.main;
            Vector3 delta = Input.mousePosition - myCam.WorldToScreenPoint(transform.position);
            
                angle = Mathf.Atan2(delta.y, delta.x) * Mathf.Rad2Deg;
                if (!active && angle < 135 && angle >= 45)
                {
                    _sprite.enabled = true;
                    weapon.enabled = true;
                    active = true;
                }
                if (active && angle > 135 || angle < 45)
                {
                    _sprite.enabled = false;
                    weapon.enabled = false;
                    active = false;
                }
            
        }
    }
}
