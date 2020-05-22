using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;
using Image = UnityEngine.UI.Image;

public class displayCharacter : MonoBehaviour, IPointerEnterHandler
{
    [SerializeField] private Sprite whichPlayer;

    private void OnMouseOver()
    {
        Debug.Log("Over!");
        GameObject.Find("Canvas").transform.GetChild(7).GetComponent<Image>().sprite = whichPlayer;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        GameObject.Find("Canvas").transform.GetChild(7).GetComponent<Image>().sprite = whichPlayer;
    }
}
