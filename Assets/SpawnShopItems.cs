using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using Photon.Pun;
public class SpawnShopItems : MonoBehaviour
{
  private void Start()
  {
    float x = 1f;
    float y = 0f;
    
    
    GameObject obj1 = PhotonNetwork.InstantiateSceneObject(Path.Combine("PhotonPrefabs", "itemshop"),
      transform.position + new Vector3(x-1.2f,y , 0), transform.rotation);
    obj1.GetComponent<ItemInfo>().setazero = 7;
    obj1.GetComponent<ShopItems>().isweapon = true;
    
    GameObject obj2 = PhotonNetwork.InstantiateSceneObject(Path.Combine("PhotonPrefabs", "itemshop"),
      transform.position + new Vector3(x-0.3f, y, 0), transform.rotation);
    obj2.GetComponent<ItemInfo>().setazero = 8;
    obj2.GetComponent<ShopItems>().isweapon = true;
    
    
    GameObject obj3 = PhotonNetwork.InstantiateSceneObject(Path.Combine("PhotonPrefabs", "itemshop"),
      transform.position + new Vector3(x+0.6f, y, 0), transform.rotation);
    obj1.GetComponent<ItemInfo>().setazero = 9;
    obj3.GetComponent<ShopItems>().isweapon = true;
    
    
    GameObject obj4 = PhotonNetwork.InstantiateSceneObject(Path.Combine("PhotonPrefabs", "itemshop"),
      transform.position + new Vector3(x+1.5f, y, 0), transform.rotation);
    obj1.GetComponent<ItemInfo>().setazero = 6;
    obj4.GetComponent<ShopItems>().isweapon = true;
    
    
  }
}
