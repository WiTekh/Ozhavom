using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Equipement : MonoBehaviour
{
    [SerializeField] private int freeslot;
    [SerializeField] private Rafale _rafale;
    [SerializeField] private int count;
    private GameObject _gameObject;
    
    
    // Start is called before the first frame update
    void Start()
    {
        _rafale = transform.GetChild(1).GetChild(4).GetChild(0).GetChild(0).GetComponent<Rafale>(); 
        freeslot = 0; 
    }

    

    // Update is called once per frame
    private void OnCollisionEnter2D (Collision2D col)
    {
        count++;
        _gameObject = col.gameObject;
        if (_gameObject.CompareTag("Weapons"))
        {
            if (freeslot <= 3)
            {
                switch (_gameObject.GetComponent<ItemInfo>().weaponname)
                {
                    case "rafale":
                        if (!_rafale.active)
                        {
                            _rafale.active = true;
                            _rafale.slot = freeslot;
                            _rafale.enabled = true;
                            Destroy(_gameObject);   //une fois les test terminer faut rajouter un PhotonNetwork. avant le destroy
                        }
                        break;
                }
            }
        }
    }
}
