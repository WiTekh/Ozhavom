using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class showDungeon : MonoBehaviour
{
    List<Vector3> _rooms = new List<Vector3>();
    void Awake()
    {
        _rooms = transform.parent.GetComponent<miniMap>().rooms;

        Debug.Log($"------------- {transform.localPosition} -------------");
        foreach (Vector3 __room in _rooms)
        {
            Debug.Log($"------------- {__room} -------------");
            if (__room.Equals(transform.localPosition))
                gameObject.SetActive(true);
        }
    }
}