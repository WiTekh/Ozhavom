using System.Collections;
using System.Collections.Generic;
using System.IO;
using Photon.Pun;
using UnityEngine;

public class TripleShot : MonoBehaviour
{
    [SerializeField] private int firerate;
    [SerializeField] public bool active;
    [SerializeField] public int slot;
    [SerializeField] private int mem;
    private WeaponRotation wr;
    private int fire;
    private Rigidbody2D rb;

    private GameObject bullet;
    // Start is called before the first frame update
    void Start()
    {
        fire = firerate;
        wr = transform.parent.GetComponent<WeaponRotation>();
    }

    // Update is called once per frame
    void Update()
    {
        mem = wr.memoire;
        if (fire<firerate)
        {
            fire++;
        }
        if (fire >= firerate)
        {
            switch (slot)
            {
                case 0:
                    if (Input.GetKey(KeyCode.Z))
                    {
                        Fire();
                        fire = 0;
                    }
                    break;
                case 1:
                    if (Input.GetKey(KeyCode.E))
                    { 
                        Fire();
                        fire = 0;
                           
                    }
                    break;
                case 2:
                    if (Input.GetKey(KeyCode.R))
                    {
                        Fire();
                        fire = 0;
                    }
                    break;
            }
        }
    }

    void Fire()
    {
         switch (mem)
            {
                //haut
                case 90:
               
                    bullet = PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "Bullet"), transform.position + new Vector3(-0.5f,0), transform.rotation);
                    rb = bullet.GetComponent<Rigidbody2D>();
                    rb.AddForce(transform.TransformDirection(1,1,0)*5f,ForceMode2D.Impulse);
                
                    bullet = PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "Bullet"), transform.position + new Vector3(0.5f,0), transform.rotation);
                    rb = bullet.GetComponent<Rigidbody2D>();
                    rb.AddForce(transform.TransformDirection(1,-1,0)*5f,ForceMode2D.Impulse);
                
                    bullet = PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "Bullet"), transform.position, transform.rotation);
                    rb = bullet.GetComponent<Rigidbody2D>();
                    rb.AddForce(transform.TransformDirection(1,0,0)*10f,ForceMode2D.Impulse);

                    break;
                //gauche
                case 180 :
              
                    bullet = PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "Bullet"), transform.position + new Vector3(0,-0.5f), transform.rotation);
                    rb = bullet.GetComponent<Rigidbody2D>();
                    rb.AddForce(transform.TransformDirection(1,1,0)*5f,ForceMode2D.Impulse);
                
                    bullet = PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "Bullet"), transform.position + new Vector3(0,0.5f), transform.rotation);
                    rb = bullet.GetComponent<Rigidbody2D>();
                    rb.AddForce(transform.TransformDirection(1,-1,0)*5f,ForceMode2D.Impulse);
                
                    bullet = PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "Bullet"), transform.position, transform.rotation);
                    rb = bullet.GetComponent<Rigidbody2D>();
                    rb.AddForce(transform.TransformDirection(1,0,0)*10f,ForceMode2D.Impulse);
                    
                    break;
                //bas
                case -90 :
          
                    bullet = PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "Bullet"), transform.position + new Vector3(0.5f,0), transform.rotation);
                    rb = bullet.GetComponent<Rigidbody2D>();
                    rb.AddForce(transform.TransformDirection(1,1,0)*5f,ForceMode2D.Impulse);
                
                    bullet = PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "Bullet"), transform.position + new Vector3(-0.5f,0), transform.rotation);
                    rb = bullet.GetComponent<Rigidbody2D>();
                    rb.AddForce(transform.TransformDirection(1,-1,0)*5f,ForceMode2D.Impulse);
                
                    bullet = PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "Bullet"), transform.position, transform.rotation);
                    rb = bullet.GetComponent<Rigidbody2D>();
                    rb.AddForce(transform.TransformDirection(1,0,0)*10f,ForceMode2D.Impulse);
                    
                    break;
                //droite
                case 0:
      
                    bullet = PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "Bullet"), transform.position + new Vector3(0,0.5f), transform.rotation);
                    rb = bullet.GetComponent<Rigidbody2D>();
                    rb.AddForce(transform.TransformDirection(1,1,0)*5f,ForceMode2D.Impulse);
                
                    bullet = PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "Bullet"), transform.position + new Vector3(0,-0.5f), transform.rotation);
                    rb = bullet.GetComponent<Rigidbody2D>();
                    rb.AddForce(transform.TransformDirection(1,-1,0)*5f,ForceMode2D.Impulse);
                
                    bullet = PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "Bullet"), transform.position, transform.rotation);
                    rb = bullet.GetComponent<Rigidbody2D>();
                    rb.AddForce(transform.TransformDirection(1,0,0)*10f,ForceMode2D.Impulse);

                    break;
                    
            }
    }
}
