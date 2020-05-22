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
    [SerializeField] private int playerId;

    public changeVal cV;

    public float hVal;
    public float sVal;

    private void Start()
    {
        cV = GameObject.Find("Canvas").transform.GetChild(8).GetComponent<changeVal>();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        GameObject.Find("Canvas").transform.GetChild(7).GetComponent<Image>().sprite = whichPlayer;

        GameObject.Find("dataHandler").GetComponent<goContainer>().button = gameObject;
    }
}
