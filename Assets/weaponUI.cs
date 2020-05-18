using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class weaponUI : MonoBehaviour
{
    public Sprite wSprite;
    public void Update()
    {
        gameObject.GetComponent<Image>().sprite = wSprite;
    }
}
