using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;
using DG.Tweening;

public class Hover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    Transform text;
    Tuple<float, float> saveText;

    private void Start()
    {
        text = GetComponentInChildren<Transform>();
        saveText = new Tuple<float, float>(text.localScale.x, text.localScale.y);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        text.DOScale(new Vector3(1.1f*text.localScale.x, 1.1f*text.localScale.y), 0.5f);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        text.DOScale(new Vector3(saveText.Item1, saveText.Item2), 1f);
    }
}
