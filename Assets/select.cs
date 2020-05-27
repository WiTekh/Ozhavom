using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;

public class select : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    
    public void OnPointerEnter(PointerEventData eventData)
    {
        transform.GetChild(0).gameObject.SetActive(true);
        GetComponent<AudioSource>().Play();
        
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("selected : " + name);
        }
    }

    
    public void OnPointerExit(PointerEventData eventData)
    {
        transform.GetChild(0).gameObject.SetActive(false);
    }
}
