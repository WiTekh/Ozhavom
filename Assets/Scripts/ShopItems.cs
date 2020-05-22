using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopItems : MonoBehaviour
{
    public string iteminfo;
    public bool isweapon;
    public int prix = 15;
    
    // Start is called before the first frame update
    void Start()
    {
        if (isweapon)
        {
            iteminfo = transform.GetComponent<ItemInfo>().weaponname;
        }
        
    }
}
