using System;
using UnityEngine;
using DG.Tweening;

public class camMove : MonoBehaviour
{
    public Vector2 _spawnOffset;

    private void Start()
    {
        _spawnOffset = GameObject.Find("varHolder").GetComponent<variablesStock>().spawnOffset;
    }

    void Update()
    {
        UpdateCamPos(_spawnOffset);

        Transform p = GameObject.Find("Canvas").transform.GetChild(6);
        
        p.localPosition = new Vector3(transform.position.x/19 * 15, transform.position.y/12 * 15);
    }

    void UpdateCamPos(Vector2 sOff)
    {
        if (!transform.parent.GetComponent<playerStats>().paused)
        {
            Vector3 parent = transform.parent.position;
            Vector3 pos = transform.position;
        
            float X = parent.x - sOff.x;
            float Y = parent.y - sOff.y;
        
            if (X % 19 < 9.5f)
                X = X - X % 19;
            else
                X = X - X % 19 + 19;

            if (Y % 12 < 6f)
                Y = Y - Y % 12;
            else
                Y = Y - Y % 12 + 12;

            transform.DOMove(new Vector3(X + sOff.x, Y + sOff.y, pos.z), 1f);
        }
    }
}
