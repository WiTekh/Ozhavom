using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class Equipement : MonoBehaviour
{
    [SerializeField] private int freeslot;
    [SerializeField] private Rafale _rafale;
    private TripleShot tripleShot;
    private Masse masse;
    private GameObject _gameObject;
    private LaserBeam _laserBeam;
    private ChargedBeam _chargedBeam;
    
    
    // Start is called before the first frame update
    void Start()
    {
        Transform transformparent = transform.GetChild(1).GetChild(5).GetChild(0).GetChild(0);
        _rafale = transformparent.GetComponent<Rafale>();
        tripleShot = transformparent.GetComponent<TripleShot>();
        _laserBeam = transformparent.GetComponent<LaserBeam>();
        _chargedBeam = transformparent.GetComponent<ChargedBeam>();
        freeslot = 0; 
    }

    

    // Update is called once per frame
    private void OnCollisionEnter2D (Collision2D col)
    {
        
        _gameObject = col.gameObject;
        if (_gameObject.CompareTag("Weapons"))
        {
            if (freeslot <= 2)
            {
                switch (_gameObject.GetComponent<ItemInfo>().weaponname)
                {
                    case "rafale":
                        if (!_rafale.active)
                        {
                            _rafale.active = true;
                            _rafale.slot = freeslot;
                            _rafale.enabled = true;
                            Destroy(_gameObject);
                            freeslot++; //une fois les test terminer faut rajouter un PhotonNetwork. avant le destroy
                        }
                        break;
                    case "masse":
                        if (!masse.active)
                        {
                            masse.active = true;
                            masse.slot = freeslot;
                            masse.enabled = true;
                            Destroy(_gameObject);
                            freeslot++;
                        }
                        break;
                    case "tripleshots":
                        if (!tripleShot.active)
                        {
                            tripleShot.active = true;
                            tripleShot.slot = freeslot;
                            freeslot++;
                            tripleShot.enabled = true;
                            Destroy(_gameObject);
                        }
                        break;
                    case "laserbeam":
                        if (!_laserBeam.active)
                        {
                            _laserBeam.active = true;
                            _laserBeam.enabled = true;
                            _laserBeam.slot = freeslot;
                            freeslot++;
                            Destroy(_gameObject);
                        }
                        break;
                    case "chargedbeam":
                        if (!_chargedBeam.active)
                        {
                            _chargedBeam.active = true;
                            _chargedBeam.slot = freeslot;
                            _chargedBeam.enabled = true;
                            freeslot++;
                            Destroy(_gameObject);
                        }
                        break;
                }
            }
        }
    }
}
