using System.Collections;
using System.Collections.Generic;
using System.IO;
using Photon.Pun;
using UnityEngine;

public class TripleShot : MonoBehaviour
{
    private PhotonView PV;

    [SerializeField] private int firerate;
    [SerializeField] public bool active;
    [SerializeField] public int slot;
    [SerializeField] private int mem;
    private CrabWeapon wr;
    private int fire;

    private GameObject bullet = new GameObject(); 
    // Start is called before the first frame update
    void Start()
    {          
        PV = transform.parent.parent.parent.GetComponent<PhotonView>();

        fire = firerate;
        wr = transform.parent.GetComponent<CrabWeapon>();
    }

    // Update is called once per frame
    void Update()
    {
        if (PV.IsMine)
        {


            mem = wr.memoire;
            if (fire < firerate)
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
    }

    void Fire()
    {
         switch (mem)
            {
                //haut
                case 90:
               
                    bullet = PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "Bullet"), transform.position + new Vector3(-0.1f,0), new Quaternion(0,0,45 + transform.rotation.z,0));
                    
                
                    bullet = PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "Bullet"), transform.position + new Vector3(0.1f,0), transform.rotation);
                   
                
                    bullet = PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "Bullet"), transform.position, transform.rotation);
                   

                    break;
                //gauche
                case 180 :
              
                    bullet = PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "Bullet"), transform.position + new Vector3(0,-0.5f), transform.rotation);
                   
                
                    bullet = PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "Bullet"), transform.position + new Vector3(0,0.5f), transform.rotation);
                  
                
                    bullet = PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "Bullet"), transform.position, transform.rotation);
                   
                    
                    break;
                //bas
                case -90 :
          
                    bullet = PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "Bullet"), transform.position + new Vector3(0.5f,0), transform.rotation);
                    
                
                    bullet = PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "Bullet"), transform.position + new Vector3(-0.5f,0), transform.rotation);
                  
                
                    bullet = PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "Bullet"), transform.position, transform.rotation);
                 
                    break;
                //droite
                case 0:
      
                    bullet = PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "Bullet"), transform.position + new Vector3(0,0.5f), transform.rotation);
                  
                    bullet = PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "Bullet"), transform.position + new Vector3(0,-0.5f), transform.rotation);
             
                    bullet = PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "Bullet"), transform.position, transform.rotation);
                 
                    break;
                    
            }
    }
}
